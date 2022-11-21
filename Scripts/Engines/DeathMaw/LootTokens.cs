using System;
using Server;
using Server.Mobiles;
using Server.Accounting;
using Server.Engines.DMChamps;
using Server.Items;
	
namespace Server.Items
{
	public abstract class BaseToken : Item	//This is the BaseToken. It gives you a large round token, easy to spot in a backpack.
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}
		
		public BaseToken() : base( 0x103F ){}
		
		public BaseToken( Serial serial ) : base( serial ) { }
		
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	//New Token Class
	public class DeathMawSpiderToken : BaseToken	//Any new tokens can be added, just change the name from DeathMaw(blah blah) to whatever you want.
	{
		[Constructable]
		public DeathMawSpiderToken()				//Make sure this name matches the token name you created.
		{
			Stackable = false;						//These tokens are not stackable, if set to true, they will stack, but they won't split apart.
			Name = "Death Maw Spider Token";		//This is the name that shows up when you hover over the token in a container.
			Hue = 33;								//The color of the token.
			LootType = LootType.Blessed;			//They don't have to be blessed, you can change the loot type to whatever you want.
			Movable = true;						//This is so they can't drop one and get more.
		}
		public DeathMawSpiderToken( Serial serial ) : base( serial ) { }	//Again, make sure the token name here matches what you created.

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	//End New Token Class	
	public class DeathMawUnholyToken : BaseToken
	{
		[Constructable]
		public DeathMawUnholyToken()
		{
			Stackable = false;
			Name = "Death Maw Unholy Token";
			Hue = 66;
			LootType = LootType.Blessed;
			Movable = true;
		}

		public DeathMawUnholyToken( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class DeathMawDragonToken : BaseToken
	{
		[Constructable]
		public DeathMawDragonToken()
		{
			Stackable = false;
			Name = "Death Maw Dragon Token";
			Hue = 96;
			LootType = LootType.Blessed;
			Movable = true;
		}

		public DeathMawDragonToken( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class DeathMawFeyToken : BaseToken
	{
		[Constructable]
		public DeathMawFeyToken()
		{
			Stackable = false;
			Name = "Death Maw Fey Token";
			Hue = 45;
			LootType = LootType.Blessed;
			Movable = true;
		}

		public DeathMawFeyToken( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class DeathMawDaemonToken : BaseToken
	{
		[Constructable]
		public DeathMawDaemonToken()
		{
			Stackable = false;
			Name = "Death Maw Daemon Token";
			Hue = 320;
			LootType = LootType.Blessed;
			Movable = true;
		}

		public DeathMawDaemonToken( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class DeathMawToken : BaseToken
	{
		[Constructable]
		public DeathMawToken()
		{
			Stackable = false;
			Name = "Death Maw Token";
			Hue = 92;
			LootType = LootType.Blessed;
			Movable = true;
		}

		public DeathMawToken( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class DeathMawElementalToken : BaseToken
	{
		[Constructable]
		public DeathMawElementalToken()				
		{
			Stackable = false;						
			Name = "Death Maw Elemental Token";		
			Hue = 53;								
			LootType = LootType.Blessed;			
			Movable = true;
		}
		public DeathMawElementalToken( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class RainbowMountToken1 : BaseToken
	{
		[Constructable]
		public RainbowMountToken1()
		{
			Stackable = false;
			Name = "Blue Rainbow Mount Token";
			Hue = 1266;
			LootType = LootType.Blessed;

		}

		public RainbowMountToken1( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class RainbowMountToken2 : BaseToken
	{
		[Constructable]
		public RainbowMountToken2()
		{
			Stackable = false;
			Name = "Green Rainbow Mount Token";
			Hue = 1269;
			LootType = LootType.Blessed;

		}

		public RainbowMountToken2( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class RainbowMountToken3 : BaseToken
	{
		[Constructable]
		public RainbowMountToken3()
		{
			Stackable = false;
			Name = "Red Rainbow Mount Token";
			Hue = 1172;
			LootType = LootType.Blessed;

		}

		public RainbowMountToken3( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class RainbowMountToken4 : BaseToken
	{
		[Constructable]
		public RainbowMountToken4()
		{
			Stackable = false;
			Name = "Yellow Rainbow Mount Token";
			Hue = 1281;
			LootType = LootType.Blessed;

		}

		public RainbowMountToken4( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class RainbowMountToken5 : BaseToken
	{
		[Constructable]
		public RainbowMountToken5()
		{
			Stackable = false;
			Name = "Purple Rainbow Mount Token";
			Hue = 6;
			LootType = LootType.Blessed;

		}

		public RainbowMountToken5( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	
	public class RainbowToken : BaseToken
	{
		[Constructable]
		public RainbowToken()
		{
			Stackable = false;
			Name = "Rainbow Mount Quest Token";
			Hue = 1153;
			LootType = LootType.Blessed;

		}

		public RainbowToken( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class NewbieToken : BaseToken
	{
		public virtual int Lifespan{ get{ return 604800; } }
		private int m_Lifespan;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int TimeLeft
		{
			get{ return m_Lifespan; }
			set{ m_Lifespan = value; InvalidateProperties(); }
		}
	
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Dungeon Token" );
				
			if ( Lifespan > 0 )
				list.Add( 1072517, m_Lifespan.ToString() ); // Lifespan: ~1_val~ seconds
		}
		
		[Constructable]
		public NewbieToken()
		{
			if ( Lifespan > 0 )
			{
				m_Lifespan = Lifespan;
				StartTimer();
			}
			Stackable = false;
			Name = "New Player Token";
			Hue = 1777;
			LootType = LootType.Blessed;
			Movable = false;

		}
		
		public NewbieToken( Serial serial ) : base( serial )
		{

		}

		private Timer m_Timer;		
		
		public virtual void StartTimer()
		{
			if ( m_Timer != null )
				return;
				
			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 10 ), TimeSpan.FromSeconds( 10 ), new TimerCallback( Slice ) );
			m_Timer.Priority = TimerPriority.OneSecond;
		}
		
		public virtual void StopTimer()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}
		
		public virtual void Slice()
		{
			m_Lifespan -= 10;
			
			InvalidateProperties();
			
			if ( m_Lifespan <= 0 )
				Decay();
		}
		
		public virtual void Decay()
		{
			if ( RootParent is Mobile )
			{
				Mobile parent = (Mobile) RootParent;
				
				if ( Name == null )
					parent.SendLocalizedMessage( 1072515, "#" + LabelNumber ); // The ~1_name~ expired...
				else
					parent.SendLocalizedMessage( 1072515, Name ); // The ~1_name~ expired...
					
				Effects.SendLocationParticles( EffectItem.Create( parent.Location, parent.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( parent.Location, parent.Map, 0x201 );
			}
			else
			{
				Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( Location, Map, 0x201 );
			}			
			
			StopTimer();
			Delete();
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_Lifespan );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Lifespan = reader.ReadInt();
			
			StartTimer();
		}
	}
	
	public class CrystalPrism : Item	//Can be used to combine tokens into a new token.
	{
		[Constructable]
		public CrystalPrism() : base ( 0x1F1C )	//Looks like a white power crystal.
		{
			Stackable = false;
			Name = "Rainbow Mount Crystal Prism";
			Hue = 1153;
			LootType = LootType.Blessed;

		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}
			
			Container pack = from.Backpack;

			if ( pack == null )
				return;
				
			int tele = pack.ConsumeTotal(
				new Type[]
				{	//Token List for combining.
					typeof( RainbowMountToken1 ),	//Insert token names here to be combined to create a new token or item.
					typeof( RainbowMountToken2 ),	//The number of tokens listed here needs to match how many you want to consume in the next section.
					typeof( RainbowMountToken3 ),
					typeof( RainbowMountToken4 ),
                    typeof( RainbowMountToken5 ),
				},
				new int[]
				{	//Combining list, how many of each token should be used?
					1,	//Each number here indicates how many tokens are needed to complete the combining process, ie if you want 5 of the first token, but 1 of the rest
					1,	//just change the 1 to a 5 down the list accordingly.
					1,	//If you want to consume just 3 tokens, remove the last two entries.
					1,	//The last entry in the list does not need a , so remove it.
                    1
				} );


			switch ( tele )
			{//Case numbering to display messages about what items are missing in the combining process.
				case 0:
				{
					from.SendMessage( "You must have the Blue Rainbow Mount Token to complete the combining process." );	//Each case matches the token that is missing.
					break;
				}
				case 1:
				{
					from.SendMessage( "You must have the Green Rainbow Mount Token to complete the combining process." );
					break;
				}
				case 2:
				{
					from.SendMessage( "You must have the Red Rainbow Mount Token to complete the combining process." );
					break;
				}
				case 3:
				{
					from.SendMessage( "You must have the Yellow Rainbow Mount Token to complete the combining process." );
					break;
				}
				case 4:
				{
					from.SendMessage( "You must have the Purple Rainbow Mount Token to complete the combining process." );
					break;
				}
				default:
				{
					RainbowToken r = new RainbowToken();	//This is the new item that is created and drop on the ground next to the player.

					r.MoveToWorld( from.Location, from.Map );
					from.PlaySound( 0x241 );

					break;
				}

			}
		}
		
		public CrystalPrism( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}