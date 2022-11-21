// Created By Lucid Nagual - Admin of The Conjuring
// This was pieced together based on several scripts that I found.

using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Mobiles;


namespace Server.Mobiles
{
	public class Stripper : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }
		private static bool m_Talked;

		string[] kfcsay = new string[]
		{
		 "I strip for gold!!!",
		 "Hello, have I seen you somewhere before?",
		 "Don't you say hi?",
		 "I strip for gold!!!",
		 "I love a man dressed in armor.",
		 "I strip for gold!!!",
		 "Don't you say hi?",
		 "Do you like to dance? I do",
		 "I strip for gold!!!",
		 "Don't you say hi?",
		 "I strip for gold!!!",
		 "I like to take my clothes off for a price.",
		};

		[Constructable]
		public Stripper() : base( AIType.AI_Melee, FightMode.None, 10, 1, 0.5, 2 )
		{
			SpeechHue = Utility.RandomDyedHue();
			InitStats( 50, 50, 25 );
			//Title = "The Stripper";
			Hue = Utility.RandomSkinHue();
			Body = 0x191;
			Name = "The Stripper"; //NameList.RandomName( "female" );
			Fame = 100;
			Karma = 0;
			Blessed = true;			
			
			AddItem( new BunsHair( Utility.RandomRedHue() ) );
			AddItem( new Backpack() );
				switch ( Utility.Random( 2 ) )
				{
					case 0: AddItem( new Bonnet( Utility.RandomBlueHue() ) ); break;
					default: case 1: AddItem( new FeatheredHat( Utility.RandomBlueHue() ) ); break;
				}
				switch ( Utility.Random( 2 ) )
				{
					case 0: AddItem( new ThighBoots( Utility.RandomGreenHue() ) ); break;
					default: case 1: AddItem( new Sandals( Utility.RandomGreenHue() ) ); break;
				}
			AddItem( new FancyShirt( Utility.RandomBlueHue() ) );
			AddItem( new Doublet( Utility.RandomBlueHue() ) );
			AddItem( new Cloak( Utility.RandomGreenHue() ) );
			Item gloves = new LeatherGloves();
			gloves.Hue = Utility.RandomBlueHue();
			AddItem( gloves );
			AddItem( new ShortPants( Utility.RandomBlueHue() ) );
			AddItem( new StrippersLingerie());
			Item skirt;
				switch ( Utility.Random( 2 ) )
				{
					case 0: skirt = new Skirt(); break;
					default: case 1: skirt = new Kilt(); break;
				}
			skirt.Hue = Utility.RandomGreenHue();
			AddItem( skirt );
			AddItem( new SilverRing() );
			AddItem( new SilverEarrings() );
			AddItem( new SilverBracelet() );
			AddItem( new SilverNecklace() ); 
		}
		
		private bool m_HasStripped;
		private DateTime m_NextDress;
		public override bool ShowFameTitle{ get{ return false; } }

		public override void OnThink()
		{
			if ( this.Deleted )
				return;
			if ( DateTime.Now > m_NextDress )
			{
				Container pack = this.Backpack;
				int a = pack.GetAmount( typeof( BaseClothing ) );
				if ( a > 0 )
				{
					for (int i = 0; i < a; i++ )
					{
						Item item = pack.FindItemByType( typeof( BaseClothing ) );
						if ( item != null )
						EquipItem( item );
					}
				}
				if ( pack.GetAmount( typeof( BaseArmor ) ) > 0 )
				{
					EquipItem( pack.FindItemByType( typeof( BaseArmor ) ) );
				}
				if ( this.FindItemOnLayer( Layer.Hair ) is LongHair )
                    		{
                    			Item item = FindItemOnLayer( Layer.Hair );
                    			item.Delete();                    									
                    			AddItem( new BunsHair( Utility.RandomRedHue() ) );
                    		}
                    		this.Frozen = false;
                    		m_HasStripped = false;
			}
			base.OnThink();
		}

		private class SpamTimer : Timer 
		{ 
			public SpamTimer() : base( TimeSpan.FromSeconds( 8 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
			} 

			protected override void OnTick() 
			{ 
				m_Talked = false; 
			} 
		} 

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 20 ) )
			return true;
			return base.HandlesOnSpeech( from );
		}

		public override void OnMovement( Mobile m, Point3D oldLocation ) 
		{                                                    
			if( m_Talked == false ) 
			{ 
				if ( m.InRange( this, 4 ) ) 
				{                
					m_Talked = true; 
					SayRandom( kfcsay, this ); 
					this.Move( GetDirectionTo( m.Location ) ); 
					// Start timer to prevent spam 
					SpamTimer t = new SpamTimer(); 
					t.Start(); 
				} 
			}
	  	}

		private static void SayRandom( string[] say, Mobile m ) 
		{ 
			m.Say( say[Utility.Random( say.Length )] ); 
		} 

		public override void OnSpeech( SpeechEventArgs args )
		{
			string said = args.Speech.ToLower();
			Mobile from = args.Mobile;
				switch ( said )
				{
					case ( "take it off" ):
					{
						goto case "dance";
					}
					case ( "hello" ):
					{
						goto case "dance";
					}
					case ( "hi" ):
					{
						goto case "dance";
					}
					case ( "hail" ):
					{
						goto case "dance";
					}
					case ( "hey" ):
					{
						goto case "dance";
					}
					case ( "dance" ):
					{
						Say( String.Format( "Hello, {0}!", args.Mobile.Name ) ); //Npc says hello to the player
						Say( String.Format( "{0}, say 'strip' if you want to have some fun!", args.Mobile.Name ) ); 
						break;
					}
					case ( "strip" ):
					{
						Say( String.Format( "I'd love to dance for you, {0}, I will take off most of my clothes for 500 gp, or all my clothes for 1000 gp!", from.Name ) );
                        break;
                	}
					case ( "goodbye" ):
					{
                        goto case "bye";
					}
					case ( "bye" ):
					{
                        Say( String.Format( "See ya later big boy!") ); //Npc says bye
               			from.PlaySound( 0x145 ); 
              			from.Animate( 9, 1, 1, true, false, 0 ); 
                    	new StrippersLingerie().MoveToWorld( from.Location, from.Map );
						from.PublicOverheadMessage( MessageType.Emote, from.SpeechHue, false, "*The Stripper throws something at you as you leave*" );
						//virtual bool DropToWorld( Mobile from, Point3D p )
						//virtual bool OnDroppedToWorld( Mobile from, Point3D p )  	
						Item skirt = new LeatherSkirt();
						skirt.Hue = 2989;
						skirt.LootType = LootType.Blessed;
						AddItem( skirt );
						Item top = new LeatherBustierArms();
						top.Hue = 2989;
						top.LootType = LootType.Blessed;
						AddItem( top );
						break;
					}
				}
			}
				
		public override bool OnDragDrop( Mobile from, Item dropped ) //OnGoldGiven
		{			
			if ( dropped.Amount >= 1000 )
				{
					dropped.Delete();
					m_HasStripped = true;
					new StripTimer( this, true, 10 ).Start();
					Say( String.Format( "Strips for {0}.", from.Name ) );
					Frozen = true;
					m_NextDress = DateTime.Now + TimeSpan.FromSeconds( 40.0 + (5.0 * Utility.RandomDouble()) );
				}
			else if ( dropped.Amount >= 500 )
				{
					dropped.Delete();
					m_HasStripped = true;
					new StripTimer( this, false, 10 ).Start();
					Say( String.Format( "Strips for {0}.", from.Name ) );
					Frozen = true;
					m_NextDress = DateTime.Now + TimeSpan.FromSeconds( 25.0 + (5.0 * Utility.RandomDouble()) );
				}
			else
				{
					Say( String.Format( "That is not what I asked for! What a turn off!! *throws down the gold*" ) ); //Npc says hello to the player
					return false;
				}

			return base.OnDragDrop( from, dropped );
		}
	

		public Stripper( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		private class StripTimer : Timer
        {
			private Stripper from;
			private int c;
			private bool d_all; 
		   
        public StripTimer( Stripper stripper, bool all,  int count ) : base( TimeSpan.FromSeconds( 2 ), TimeSpan.FromSeconds( 1.8 ), 20 )
        {
			Priority = TimerPriority.FiftyMS;
            from = stripper; 
            c = count;
            d_all = all;
        }

		protected override void OnTick()
        {
        	if ( from.Deleted || from == null )
        	Stop();
			//need to clean from here down.
         		if ( from != null && !from.Deleted )
                		{
                			if ( c > 5 )
                			{                				
						c--;
						((BaseCreature)from).DebugSay("Oh no. {0} Don't touch.", c);
						switch ( Utility.Random( 6 ) )
						{
							case 0: from.Animate( 32, 5, 1, true, false, 0 ); break;
							case 1: from.Animate( 17, 5, 1, true, false, 0 ); break;
							case 2: from.Animate( 30, 5, 1, true, false, 0 ); break;
							case 3: from.Animate( 16, 5, 1, true, false, 0 ); break;
							case 4: from.Animate( 19, 5, 1, true, false, 0 ); break;
							case 5: from.Animate( 12, 5, 1, true, false, 0 ); break;
						}												
					}							                 			
                			else if ( from.FindItemOnLayer( Layer.Cloak ) is BaseClothing )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.Cloak );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 32, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.Gloves ) is BaseArmor )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.Gloves );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 17, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.Helm ) is BaseClothing )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.Helm );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 30, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.Hair ) is BunsHair )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.Hair );
                    				item.Delete();
                    				from.AddItem( new LongHair( Utility.RandomRedHue() ) );
                    				from.Animate( 16, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.MiddleTorso ) is BaseClothing )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.MiddleTorso );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 12, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.OuterLegs ) is BaseClothing )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.OuterLegs );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 19, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.Shirt ) is BaseClothing )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.Shirt );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 13, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.Pants ) is BaseClothing )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.Pants );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 30, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.Shoes ) is BaseShoes )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.Shoes );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 32, 5, 1, true, false, 0 );
                    			}
                    			else if ( from.FindItemOnLayer( Layer.InnerTorso ) is BaseArmor && d_all == true )
                    			{
                    				Item item = from.FindItemOnLayer( Layer.InnerTorso );
                    				from.PlaceInBackpack( item );
                    				from.Animate( 17, 5, 1, true, false, 0 );
									from.MovingParticles( from, 0x1C19, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
                    			}
                    			else if ( c > 0 )
                    			{                    				
						c--;
						
						switch ( Utility.Random( 6 ) )
						{
							case 0: from.Animate( 32, 5, 1, true, false, 0 ); break;
							case 1: from.Animate( 17, 5, 1, true, false, 0 ); break;
							case 2: from.Animate( 30, 5, 1, true, false, 0 ); break;
							case 3: from.Animate( 16, 5, 1, true, false, 0 ); break;
							case 4: from.Animate( 19, 5, 1, true, false, 0 ); break;
							case 5: from.Animate( 12, 5, 1, true, false, 0 ); break;
						}						
                    				//
                    				//from.PublicOverheadMessage( MessageType.Emote, from.SpeechHue, true, "Шлет воздушный поцелуй" );
                    			}
                    			else 
                    			{
                    				from.Animate( 32, 5, 1, true, false, 0 );
                    				switch ( Utility.Random( 2 ) )
						{
							case 0: from.PublicOverheadMessage( MessageType.Emote, from.SpeechHue, false, "*Slaps her ass*" ); break;
							case 1: from.PublicOverheadMessage( MessageType.Emote, from.SpeechHue, false, "*Wants her hair pulled*" ); break;
						}
					}
            		}
			}
		}
	}
}


//--Stripers Lingerie---------------------------------------------------------------------

namespace Server.Items
{
	[FlipableAttribute( 0x1c06, 0x1c07 )]
	public class StrippersLingerie : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 25; } }
		public override int OldStrReq{ get{ return 15; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		public override bool AllowMaleWearer{ get{ return false; } }

		[Constructable]
		public StrippersLingerie() : base( 0x1C06 )
		{
			Name = "Stripper's Lingerie";
      		Hue = Utility.RandomList( 0x1, 0x17, 0xEE  );
			Weight = 1.0;
		}

		public StrippersLingerie( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}

