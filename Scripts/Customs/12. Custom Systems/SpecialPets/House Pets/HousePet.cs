using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Multis;
using Server.Items;
using Server.Gumps;
using Server.Misc;
using Server.ContextMenus;

namespace Server.Mobiles
{
	public class HousePet : BaseCreature
	{
		public int chance = Utility.RandomMinMax( 2, 6 );

		public override bool NoHouseRestrictions{ get{ return true; } }
		public bool SkillModded;

		[CommandProperty(AccessLevel.GameMaster)]
        public string HPetType
        {
            get { return m_HPetType; }
            set { m_HPetType = value; InvalidateProperties(); }
        }
		
		public string m_HPetType = "";

		private DateTime m_Birth;
		[CommandProperty( AccessLevel.GameMaster)]
		public DateTime Birth
		{
			get { return m_Birth; }
			set { m_Birth = value; }
		} 
		
		[Constructable]
		public HousePet() : this( DateTime.MinValue, null, null, 0 )
		{
		}
		
		[Constructable]
		public HousePet( DateTime birth, string name, string m_HPetType, int hue ) : base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			Name = "a house pet";
			Title = "";
			SetStr( 1, 5 );
			SetDex( 25, 30 );
			SetInt( 2 );
			
			SetHits( 1, Str );
			SetStam( 25, Dex );
			SetMana( 0 );

			SetResistance( ResistanceType.Physical, 2 );

			CantWalk = true;
			Blessed = true;
			
			if ( birth != DateTime.MinValue )
				m_Birth = birth;
			else
				m_Birth = DateTime.Now;
				
			if ( name != null )
				Name = name;
				
			if ( hue > 0 )
				Hue = hue;

			if ( m_HPetType != null )
				Title = m_HPetType;
		}

		public override void OnStatsQuery( Mobile from )
		{
			if ( from.Map == this.Map && Utility.InUpdateRange( this, from ) && from.CanSee( this ) )
			{
				BaseHouse house = BaseHouse.FindHouseAt( this );

				if ( house != null && house.IsCoOwner( from ) )
					from.SendMessage( "As the house owner, you may rename this House Pet." );
					
				from.Send( new Server.Network.MobileStatus( from, this ) );
			}
		}

		public HousePet( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			int weeks = GetWeeks( m_Birth );
			
			if ( weeks == 1 )
				list.Add( 1072626 ); // 1 week old
			else if ( weeks > 1 )
				list.Add( 1072627, weeks.ToString() ); // ~1_AGE~ weeks old
			else
				list.Add( 1114374 ); //Love that new Pet smell err look.
		}
		
		public override bool CanBeRenamedBy( Mobile from )
		{
			if ( (int) from.AccessLevel > (int) AccessLevel.Player )
				return true;
		
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsCoOwner( from ) )
				return true;
			else
				return false;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( m_HPetType == "the house dog" )
			{
				if ( dropped is PremiumDogFood )
				{
					dropped.Delete();

					switch ( Utility.Random( 6 ) )
					{
						case 0:
						{
							Say("I went running through the streets of  " + "#" + " earlier and had so much fun!", Utility.RandomMinMax( 1012003, 1012010 ));
							if ( chance > 3 )
							{
								from.AddSkillMod( new TimedSkillMod( SkillName.Herding, true, 10, TimeSpan.FromMinutes( Utility.Random( 1, 2 ) )));
								from.SendMessage("You have been granted Doggie Dexterity!");
							}
							break;	//"House Pet: Doggie Dexterity"
						}
						case 1:
						{
							Say("Do you smell that?");
							if ( chance > 3 )
							{
								from.AddSkillMod( new TimedSkillMod( SkillName.DetectHidden, true, 10, TimeSpan.FromMinutes( Utility.Random( 1, 2 ) )));
								from.SendMessage("You have been granted Doggie Detection!");
							}
							break;	//"House Pet: Doggie Detection"
						}
						case 2:
						{
							Say("I caught my tail this morning.");
							if ( chance > 3 )
							{
								from.AddSkillMod( new TimedSkillMod( SkillName.Snooping, true, 10, TimeSpan.FromMinutes( Utility.Random( 1, 2 ) )));
								from.SendMessage("You have been granted Doggie Discovery!");
							}
							break;	//"House Pet: Doggie Discovery"
						}
						case 3:
						{
							Say("How can I follow you to the bathroom from here?!");
							if ( chance > 3 )
							{
								from.AddSkillMod( new TimedSkillMod( SkillName.Tracking, true, 10, TimeSpan.FromMinutes( Utility.Random( 1, 2 ) )));
								from.SendMessage("You have been granted Doggie Directions!");
							}
							break;	//"House Pet: Doggie Directions"
						}
						case 4:
						{
							Say("...and then he said 'Fetch'! How degrading! Right?");
							if ( chance > 3 )
							{
								from.AddSkillMod( new TimedSkillMod( SkillName.Veterinary, true, 10, TimeSpan.FromMinutes( Utility.Random( 1, 2 ) )));
								from.SendMessage("You have been granted Doggie Discord!");
							}
							break;	//"House Pet: Doggie Discord"
						}
						case 5:
						{
							Say("I did not chew your boots. Mondain was here and...");
							if ( chance > 3 )
							{
								from.AddSkillMod( new TimedSkillMod( SkillName.Begging, true, 10, TimeSpan.FromMinutes( Utility.Random( 1, 2 ) )));
								from.SendMessage("You have been granted Doggie Destitution!");
							}
							break;	//"House Pet: Doggie Destitution"
						}
					}

					PlaySound( Utility.RandomMinMax( 0x85, 0x89 ) );
					return true;
				}
				else
					return false;
			}
			else if ( m_HPetType == "the house bird" )
			{
				if ( dropped is PremiumParrotFood )
				{
					dropped.Delete();

					switch ( Utility.Random( 6 ) )
					{
						case 0: Say("Soaring over Moonglow earlier, I noticed it was very busy!"); break;
						case 1: Say("I will not repeat you. I am not a parrot!"); break;
						case 2: Say("To beak, or not to beak..."); break;
						case 3: Say("If you are not flying, you are crying!"); break;
						case 4: Say("Some pigeons are baaaad news, but most sparrows are alright."); break;
						case 5: Say("Feather for your thoughts? Not one of mine, of course though!"); break;
					}
					
					PlaySound( Utility.RandomMinMax( 0x19, 0x01D ) );
					return true;
				}
				else
					return false;
			}
			else
			{
				from.SendMessage("This pet will only eat certain food!");
				return false;
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (DateTime) m_Birth );
			writer.Write( m_HPetType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_Birth = reader.ReadDateTime();
			m_HPetType = reader.ReadString();
		}
		
		public static int GetWeeks( DateTime birth )
		{
			TimeSpan span = DateTime.Now - birth;
			
			return (int) ( span.TotalDays / 7 );		
		}
	}

	public class HouseDog : HousePet
	{
		[CommandProperty(AccessLevel.GameMaster)]
        public string HPetType
        {
            get { return m_HPetType; }
            set { m_HPetType = value; InvalidateProperties(); }
        }
		
		public string m_HPetType = "";

		private DateTime m_Birth;
		[CommandProperty( AccessLevel.GameMaster)]
		public DateTime Birth
		{
			get { return m_Birth; }
			set { m_Birth = value; }
		} 
		
		[Constructable]
		public HouseDog() : this( DateTime.MinValue, null, null, 0 )
		{
		}
		public HouseDog( DateTime birth, string name, string m_HPetType, int hue ) : base()
		{
			Name = "a house dog";
            Title = "";			

			/*
			if ( Utility.RandomDouble() < 0.25 )
			{
				Body = 27;
			}
			else if ( Utility.RandomDouble() < 0.15 )
			{
				Body = 217;
			}
			else if ( Utility.RandomDouble() < 0.10 )
			{
				Body = 277;
			}
			else
			{
				Body = 27;
			}
			*/ 
			
			BaseSoundID = 0x087;
			
			SetStr( 1, 5 );
			SetDex( 25, 30 );
			SetInt( 2 );
			
			SetHits( 1, Str );
			SetStam( 25, Dex );
			SetMana( 0 );

			SetResistance( ResistanceType.Physical, 2 );

			CantWalk = true;
			Blessed = true;
			
			if ( birth != DateTime.MinValue )
				m_Birth = birth;
			else
				m_Birth = DateTime.Now;
				
			if ( name != null )
				Name = name;
				
			if ( hue > 0 )
				Hue = hue;

			if ( m_HPetType != null )
				Title = m_HPetType;
		}

		public HouseDog( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			int weeks = GetWeeks( m_Birth );
			
			if ( weeks == 1 )
				list.Add( 1072626 ); // 1 week old
			else if ( weeks > 1 )
				list.Add( 1072627, weeks.ToString() ); // ~1_AGE~ weeks old
			else
				list.Add( 1114374 ); //Love that new Pet smell err look.
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				Say( e.Speech );
				PlaySound( 0x086 );
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (DateTime) m_Birth );
			writer.Write( m_HPetType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_Birth = reader.ReadDateTime();
			m_HPetType = reader.ReadString();
		}
		
		public static int GetWeeks( DateTime birth )
		{
			TimeSpan span = DateTime.Now - birth;
			
			return (int) ( span.TotalDays / 7 );		
		}
	}

	public class HouseBird : HousePet
	{
		public override bool NoHouseRestrictions{ get{ return true; } }

		[CommandProperty(AccessLevel.GameMaster)]
        public string HPetType
        {
            get { return m_HPetType; }
            set { m_HPetType = value; InvalidateProperties(); }
        }
		
		public string m_HPetType = "";

		private DateTime m_Birth;
		[CommandProperty( AccessLevel.GameMaster)]
		public DateTime Birth
		{
			get { return m_Birth; }
			set { m_Birth = value; }
		} 
		
		[Constructable]
		public HouseBird() : this( DateTime.MinValue, null, null, 0 )
		{
		}
		public HouseBird( DateTime birth, string name, string m_HPetType, int hue ) : base()
		{
			Name = "a pet bird";
            Title = "";	

			/*
			if ( Utility.RandomDouble() < 0.25 )
			{
				Body = 0x11A;
			}
			else if ( Utility.RandomDouble() < 0.15 )
			{
				Body = 0x5;
			}
			else if ( Utility.RandomDouble() < 0.10 )
			{
				Body = 0x6;
			}
			else
			{
				Body = 0x11A;
			}
			*/ 
			
			BaseSoundID = 0x087;
			
			SetStr( 1, 5 );
			SetDex( 25, 30 );
			SetInt( 2 );
			
			SetHits( 1, Str );
			SetStam( 25, Dex );
			SetMana( 0 );

			SetResistance( ResistanceType.Physical, 2 );

			CantWalk = true;
			Blessed = true;
			
			if ( birth != DateTime.MinValue )
				m_Birth = birth;
			else
				m_Birth = DateTime.Now;
				
			if ( name != null )
				Name = name;
				
			if ( hue > 0 )
				Hue = hue;

			if ( m_HPetType != null )
				Title = m_HPetType;
		}
		
		public override void OnStatsQuery( Mobile from )
		{
			if ( from.Map == this.Map && Utility.InUpdateRange( this, from ) && from.CanSee( this ) )
			{
				BaseHouse house = BaseHouse.FindHouseAt( this );

				if ( house != null && house.IsCoOwner( from ) )
					from.SendMessage( "As the house owner, you may rename this House Pet." );
					
				from.Send( new Server.Network.MobileStatus( from, this ) );
			}
		}

		public HouseBird( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			int weeks = GetWeeks( m_Birth );
			
			if ( weeks == 1 )
				list.Add( 1072626 ); // 1 week old
			else if ( weeks > 1 )
				list.Add( 1072627, weeks.ToString() ); // ~1_AGE~ weeks old
			else
				list.Add( 1114374 ); //Love that new Pet smell err look.
		}
		
		public override bool CanBeRenamedBy( Mobile from )
		{
			if ( (int) from.AccessLevel > (int) AccessLevel.Player )
				return true;
		
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsCoOwner( from ) )
				return true;
			else
				return false;
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				Say( e.Speech );
				PlaySound( 0x086 );
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (DateTime) m_Birth );
			writer.Write( m_HPetType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_Birth = reader.ReadDateTime();
			m_HPetType = reader.ReadString();
		}
		
		public static int GetWeeks( DateTime birth )
		{
			TimeSpan span = DateTime.Now - birth;
			
			return (int) ( span.TotalDays / 7 );		
		}
	}
}