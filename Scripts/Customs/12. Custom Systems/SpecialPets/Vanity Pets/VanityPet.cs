#define XMLSPAWNER
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.ContextMenus;
using Server.Targeting;

using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public class PetLicense : Item
	{
		[Constructable]
		public PetLicense( ) : base( 0xEF3 )
		{
			Weight = 1.0;
			Name = "a vanity pet license";
			LootType = LootType.Blessed;
			Movable = false;
		}
		
		public PetLicense( Serial serial ) : base( serial )
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
	}

	public class VanityPetDeed : Item
   	{
		public string VPType = "";
		private VanityPetDeed m_VPDdeed;
		private int m_VPetType;
		[CommandProperty( AccessLevel.GameMaster)]
		public int VPetType
		{
			get { return m_VPetType; }
			set { m_VPetType = value; InvalidateProperties(); }
		}

		[Constructable]
		public VanityPetDeed(int vpettype) : base( 0x14F0 )
		{
			Movable = true;
			Name = "a vanity pet deed";
			Weight = 1.0;
			Hue = Utility.RandomAnimalHue();	//random( 2301, 18 );
			m_VPetType = vpettype;

			if ( m_VPetType == 1 )
					VPType = "a vanity pet dog";
			else if ( m_VPetType == 2 )
					VPType = "a vanity pet parrot";
		}

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = (PlayerMobile)from;
		#if XMLSPAWNER
			XmlData a = (XmlData)XmlAttach.FindAttachment( from, typeof( XmlData ), "VanityPet" );
			if ( a == null )
			{
				if ( IsChildOf( from.Backpack ) )
				{
					if ( m_VPetType == 1 )
					{
						VanityPetDog vpet = new VanityPetDog();
						vpet.Controlled = true;
						vpet.ControlMaster = from;
						vpet.ControlTarget = from;
						vpet.Hue = this.Hue;
						vpet.MoveToWorld( from.Location, from.Map );
							from.SendMessage( "Here is your new friend!" );
						XmlAttach.AttachTo( from, new XmlData( "VanityPet", "true" ) );
						this.Delete();
					}
					else if ( m_VPetType == 2 )
					{
						VanityPetBird vpet = new VanityPetBird();
						vpet.Controlled = true;
						vpet.ControlMaster = from;
						vpet.ControlTarget = from;
						vpet.Hue = this.Hue;
						vpet.MoveToWorld( from.Location, from.Map );
							from.SendMessage( "Here is your new friend!" );
						XmlAttach.AttachTo( from, new XmlData( "VanityPet", "true" ) );
						this.Delete();
					}
					else
					{
						from.SendMessage( "You have an invalid Vanity Pet Deed. Please speak with a Staff Member to fix the issue." );
						return;
					}
				}
				else
				{
					from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					return;
				}
			}
			else if ( a != null && a.Data != "true" )
			{
				if ( IsChildOf( from.Backpack ) )
				{
					if ( m_VPetType == 1 )
					{
						VanityPetDog vpet = new VanityPetDog();
						vpet.Controlled = true;
						vpet.ControlMaster = from;
						vpet.ControlTarget = from;
						vpet.Hue = this.Hue;
						vpet.MoveToWorld( from.Location, from.Map );
							from.SendMessage( "Here is your new friend!" );
						a.Data = "true";
						this.Delete();
					}
					else if ( m_VPetType == 2 )
					{
						VanityPetBird vpet = new VanityPetBird();
						vpet.Controlled = true;
						vpet.ControlMaster = from;
						vpet.ControlTarget = from;
						vpet.Hue = this.Hue;
						vpet.MoveToWorld( from.Location, from.Map );
							from.SendMessage( "Here is your new friend!" );
						a.Data = "true";
						this.Delete();
					}
					else
					{
						from.SendMessage( "You have an invalid Vanity Pet Deed. Please speak with a Staff Member to fix the issue." );
						return;
					}
				}
				else
				{
					from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					return;
				}
			}
			else
			{
				from.SendMessage("You already have a vanity pet out.");
				return;
			}
		#else
			Item a = from.Backpack.FindItemByType( typeof( PetLicense ) );
			if( a == null )
			{
				if ( IsChildOf( from.Backpack ) )
				{
					if ( m_VPetType == 1 )
					{
						VanityPetDog vpet = new VanityPetDog();
						vpet.Controlled = true;
						vpet.ControlMaster = from;
						vpet.ControlTarget = from;
						vpet.Hue = this.Hue;
						vpet.MoveToWorld( from.Location, from.Map );
							from.SendMessage( "Here is your new friend!" );
						Item item = new PetLicense();
                        from.AddToBackpack(item);
						this.Delete();
					}
					else if ( m_VPetType == 2 )
					{
						VanityPetBird vpet = new VanityPetBird();
						vpet.Controlled = true;
						vpet.ControlMaster = from;
						vpet.ControlTarget = from;
						vpet.Hue = this.Hue;
						vpet.MoveToWorld( from.Location, from.Map );
							from.SendMessage( "Here is your new friend!" );
						Item item = new PetLicense();
                        from.AddToBackpack(item);
						this.Delete();
					}
					else
					{
						from.SendMessage( "You have an invalid Vanity Pet Deed. Please speak with a Staff Member to fix the issue." );
						return;
					}
				}
				else
				{
					from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					return;
				}
			}
			else
			{
				from.SendMessage("You cannot bring out this vanity pet, because you already have one out.");
			}
		#endif
		}

		public VanityPetDeed( Serial serial ) : base( serial )
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

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add("Vanity Pet Type: {0}", VPType);
		}
   	}
}

namespace Server.Mobiles
{
	public class VanityPet : BaseCreature
	{
		public int chance = Utility.RandomMinMax( 2, 6 );
		public const bool UseAnimations = true;

		private DateTime m_Birth;
		
		[CommandProperty( AccessLevel.GameMaster)]
		public DateTime Birth
		{
			get { return m_Birth; }
			set { m_Birth = value; }
		}

		[Constructable]
		public VanityPet() : this( DateTime.MinValue )
		{
		}

		[Constructable]
		public VanityPet( DateTime birth ) : base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			if ( birth != DateTime.MinValue )
				m_Birth = birth;
			else
				m_Birth = DateTime.Now;
		}
		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override bool Unprovokable{ get{ return Core.AOS; } }
		public override bool Uncalmable{ get{ return Core.AOS; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool IsDispellable { get { return false; } }
		public override bool Commandable { get { return false; } }
		public override bool DeleteOnRelease{ get{ return true; } }

		public override void OnStatsQuery( Mobile from )
		{
			if ( from.Map == this.Map && Utility.InUpdateRange( this, from ) && from.CanSee( this ) && from == ControlMaster )
			{
				from.Send( new Server.Network.MobileStatus( from, this ) );
			}
			else
				from.SendLocalizedMessage( 501648 ); // You cannot use this unless you are the owner.
		}

		public VanityPet( Serial serial ) : base( serial )
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
			bool ret = base.CanBeRenamedBy(from);

			if ( (int) from.AccessLevel > (int) AccessLevel.Player )
				ret = true;

			if (from == ControlMaster)
				ret = true;

			return ret;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive && Controlled && from == ControlMaster && from.InRange( this, 10 ) )
				list.Add( new ReleaseEntry( from, this ) );
		}

		public virtual void BeginRelease( Mobile from )
		{
			if ( !Deleted && Controlled && from == ControlMaster && from.CheckAlive() )
				EndRelease( from );
		}

		public virtual void EndRelease( Mobile from )
		{
			Container pack = from.Backpack;

			if ( from == null || (!Deleted && Controlled && from == ControlMaster && from.CheckAlive()) )
			{
			#if XMLSPAWNER
				XmlData a = (XmlData)XmlAttach.FindAttachment( from, typeof( XmlData ), "VanityPet" );
				if ( a != null && a.Data == "true" )
				{
					Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 1, 13, 2100, 3, 5042, 0 );
					PlaySound( 0x201 );
					Delete();
					a.Data = "false";
				}
			#else
				Item a = from.Backpack.FindItemByType( typeof( PetLicense ) );
				if( a != null )
				{
					pack.ConsumeTotal( typeof( PetLicense ), 1 );
					Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 1, 13, 2100, 3, 5042, 0 );
					PlaySound( 0x201 );
					Delete();
				}
			#endif
			}
		}

		private class ReleaseEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private VanityPet m_VanityPet;

			public ReleaseEntry( Mobile from, VanityPet vanitypet ) : base( 6118, 14 )
			{
				m_From = from;
				m_VanityPet = vanitypet;
			}

			public override void OnClick()
			{
				if ( !m_VanityPet.Deleted && m_VanityPet.Controlled && m_From == m_VanityPet.ControlMaster && m_From.CheckAlive() )
					m_VanityPet.BeginRelease( m_From );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version

			writer.Write( (DateTime) m_Birth );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();

			m_Birth = reader.ReadDateTime();	
		}

		public static int GetWeeks( DateTime birth )
		{
			TimeSpan span = DateTime.Now - birth;
			
			return (int) ( span.TotalDays / 7 );		
		}
	}
}