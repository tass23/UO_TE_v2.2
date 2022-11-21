using System;
using Server.Network;
using Server.Items;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Regions;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
	public abstract class Lightsaber : BaseSword	
	{
		private LightSource light;
		public override double DefaultWeight
		{
			get { return 10.0; }
		}

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.RidingSwipe; } }
		
		public override int AosIntelligenceReq{ get{ return 100; } }
		public override int AosMinDamage{ get{ return 25; } }
		public override int AosMaxDamage{ get{ return 40; } }
		public override int AosSpeed{ get{ return 40; } }
		public override float MlSpeed{ get{ return 2.25f; } }

		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 29; } }
		public override int OldSpeed{ get{ return 45; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 0; } }
		public override int InitMaxHits{ get{ return 0; } }

		[Constructable]
		public Lightsaber() : base( 0x0F92 )
		{
			Light = LightType.Circle150;
			DurabilityLevel = WeaponDurabilityLevel.Indestructible;
			Attributes.SpellChanneling = 1;
			LootType = LootType.Blessed;
			Layer = Layer.TwoHanded;
			AccuracyLevel = WeaponAccuracyLevel.Regular;
			//Attributes.LowerManaCost = 15;
			//Attributes.CastRecovery = 5;
			//Attributes.CastSpeed = 5;
		}
		
		public Lightsaber( Serial serial ) : base( serial )
		{
		}

		public override void OnAdded( object parent )
		{
			light = new LightSource();
			light.Layer = Layer.Waist;
			light.Light = LightType.Circle150;

			if( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				from.AddItem( light );
			}
			base.OnAdded( parent );
		}
		
		public override void OnRemoved( object parent )
		{
			if( light != null && parent is Mobile )
			{
				light.Delete();
			}

			base.OnRemoved( parent );
		}
		
		public override void Serialize( GenericWriter writer )
		{ 
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( light );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			light = reader.ReadItem() as LightSource;
		}
	}
	
	public class PadawanLightsaber : Lightsaber
	{
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public PadawanLightsaber()
		{
			Name = "Jedi Lightsaber";
			Hue = 94;
			Attributes.RegenHits = 2;
			Attributes.RegenMana = 2;
			WeaponAttributes.ResistColdBonus = 15;
			Attributes.WeaponSpeed = 5;
			//Attributes.WeaponDamage = 1;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = cold = pois = chaos = nrgy = direct = 0;
			phys = 100;
		}

		public override bool OnEquip( Mobile from )
		{
			if ( from.Karma >= 5000 )
			{
				if ( from != m_Owner )
				{
					if ( m_Owner == null )
					{
						if ( from.Karma >= 5000 )
						{
							from.SendMessage( "The lightsaber binds to you..." );
							m_Owner = from;
							Name = "Jedi Lightsaber";
							EngravedText = "of the Padawan " + m_Owner.Name.ToString();
							from.FixedEffect( 0x375A, 10, 15 );
							from.PlaySound( 0x1E7 );
							return base.OnEquip( from );
						}
						else
						{
							from.SendMessage( "Only a Jedi, strong in the Force can wield this lightsaber." );
						}
					}
					else
					{
						from.SendMessage( "This is not your lightsaber." );
					}
					return false;
				}
				return base.OnEquip( from );
			}
			else
			{
				from.SendMessage( "Only a Jedi, strong in the Force can wield this lightsaber." );
				return false;
			}
			return base.OnEquip( from );
        }

		public override bool Decays
		{
			get{ return false; }
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Owner != null )
				list.Add( 1072304, m_Owner.Name ); 
		}

		public PadawanLightsaber( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write((Mobile)m_Owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            m_Owner = reader.ReadMobile();
		}
	}
	
	public class SithApprenticeLightsaber : Lightsaber
	{
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public SithApprenticeLightsaber()
		{
			Name = "Sith Lightsaber";
			Hue = 37;
			Attributes.RegenHits = 2;
			Attributes.RegenMana = 2;
			WeaponAttributes.ResistColdBonus = 15;
			Attributes.WeaponSpeed = 5;
			//Attributes.WeaponDamage = 1;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = cold = pois = chaos = nrgy = direct = 0;
			phys = 100;
		}

		public override bool OnEquip( Mobile from )
		{
			if ( from.Karma <= -5000 )
			{
				if ( from != m_Owner )
				{
					if ( m_Owner == null )
					{
						if ( from.Karma <= -5000 )
						{
							from.SendMessage( "The lightsaber binds to you..." );
							m_Owner = from;
							Name = "Sith Lightsaber";
							EngravedText = "of the Apprentice " + m_Owner.Name.ToString();
							from.FixedEffect( 0x375A, 10, 15 );
							from.PlaySound( 0x1E7 );
							return base.OnEquip( from );
						}
						else
						{
							from.SendMessage( "Only a Sith can wield this lightsaber." );
						}
					}
					else
					{
						from.SendMessage( "This is not your lightsaber." );
					}
					return false;
				}
				return base.OnEquip( from );
			}
			else
			{
				from.SendMessage( "Only a Sith can wield this lightsaber." );
				return false;
			}
			return base.OnEquip( from );
        }

		public override bool Decays
		{
			get{ return false; }
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Owner != null )
				list.Add( 1072304, m_Owner.Name ); 
		}

		public SithApprenticeLightsaber( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write((Mobile)m_Owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            m_Owner = reader.ReadMobile();
		}
	}
}