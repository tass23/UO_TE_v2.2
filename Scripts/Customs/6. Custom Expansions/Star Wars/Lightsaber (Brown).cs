using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class UlricRedemptionLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1867, 2110 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public UlricRedemptionLightsaber()
		{
			Name = "Ulric's Redemption Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1867;
			WeaponAttributes.HitLeechHits = 55;
			Attributes.RegenStam = 5;
			Attributes.WeaponSpeed = 45;
			Attributes.WeaponDamage = 55;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{

			chaos = 50;
			fire = 50;
			cold = nrgy = direct = phys = pois = 0;
		}

		public override bool OnEquip( Mobile from )
		{
			if ( from.Karma >= -5000 && from.Karma <= 5000 )
			{
				if ( from != m_Owner )
				{
					if ( m_Owner == null )
					{
						if ( from.Karma >= -5000 && from.Karma <= 5000 )
						{
							from.SendMessage( "The lightsaber binds to you..." );
							m_Owner = from;
							Name = "Ulric's Redemption Lightsaber";
							EngravedText = "of the Jedi Exile "+ m_Owner.Name.ToString();
							from.FixedEffect( 0x375A, 10, 15 );
							from.PlaySound( 0x1E7 );
							return base.OnEquip( from );
						}
						else
						{
							from.SendMessage( "Only a Jedi Exile, can wield this lightsaber." );
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
				from.SendMessage( "Only a Jedi Exile, can wield this lightsaber." );
				return false;
			}
			return base.OnEquip( from );
        }

		public override bool Decays
		{
			get{ return false; }
		}

		public UlricRedemptionLightsaber( Serial serial ) : base( serial )
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

	public class VexxtalLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1867, 2110 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public VexxtalLightsaber()
		{
			Name = "Vexxtal crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2110;
			WeaponAttributes.HitLeechHits = 100;
			Attributes.RegenStam = 4;
			Attributes.RegenHits = 4;
			WeaponAttributes.HitHarm = 50;
			WeaponAttributes.HitPoisonArea = 50;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{

			chaos = 50;
			pois = 50;
			cold = nrgy = direct = phys = fire = 0;
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
							Name = "Vexxtal crystal Lightsaber";
							EngravedText = "of the Sith Apprentice "+ m_Owner.Name.ToString();
							from.FixedEffect( 0x375A, 10, 15 );
							from.PlaySound( 0x1E7 );
							return base.OnEquip( from );
						}
						else
						{
							from.SendMessage( "Only a Sith, can wield this lightsaber." );
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
				from.SendMessage( "Only a Sith, can wield this lightsaber." );
				return false;
			}
			return base.OnEquip( from );
        }

		public override bool Decays
		{
			get{ return false; }
		}

		public VexxtalLightsaber( Serial serial ) : base( serial )
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