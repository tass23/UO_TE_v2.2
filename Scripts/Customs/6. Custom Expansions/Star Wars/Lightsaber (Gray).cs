using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class BlackwingLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1102, 1151, 1154, 1301 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public BlackwingLightsaber()
		{
			Name = "Blackwing crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1175;
			WeaponAttributes.HitLeechStam = 25;
			WeaponAttributes.HitLeechHits = 25;
			WeaponAttributes.HitLeechMana = 25;
			Attributes.WeaponSpeed = 50;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{

			chaos = 50;
			pois = 50;
			cold = fire = direct = phys = nrgy = 0;
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
							Name = "Blackwing crystal Lightsaber";
							EngravedText = "of the Sith Apprentice " + m_Owner.Name.ToString();
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

		public BlackwingLightsaber( Serial serial ) : base( serial )
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

	public class LignanLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1102, 1151, 1154, 1301 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public LignanLightsaber()
		{
			Name = "Lignan crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			Hue = 840;
			WeaponAttributes.HitLeechHits = 50;
			WeaponAttributes.HitLeechMana = 50;
			WeaponAttributes.HitLeechStam = 50;
			Attributes.RegenHits = 5;
			Attributes.WeaponSpeed = 45;
			Attributes.WeaponDamage = 80;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{

			chaos = 15;
			pois = 15;
			cold = 15;
			fire = 15;
			direct = 15;
			phys = 10;
			nrgy = 15;
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
							Name = "Lignan crystal Lightsaber";
							EngravedText = "of the Sith Apprentice " + m_Owner.Name.ToString();
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

		public LignanLightsaber( Serial serial ) : base( serial )
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

	public class StygiumLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1102, 1151, 1154, 1301 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public StygiumLightsaber()
		{
			Name = "Stygium crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2406;
			SkillBonuses.SetValues( 0, SkillName.Stealth, 25.0 );
			SkillBonuses.SetValues( 0, SkillName.Hiding, 25.0 );
			Attributes.WeaponDamage = 15;
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
							Name = "Stygium crystal Lightsaber";
							EngravedText = "of the Sith Apprentice " + m_Owner.Name.ToString();
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

		public StygiumLightsaber( Serial serial ) : base( serial )
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