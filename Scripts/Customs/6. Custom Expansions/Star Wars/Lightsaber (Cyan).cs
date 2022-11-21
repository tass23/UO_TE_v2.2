using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class MantleLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1173, 1366, 1391, 2908 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public MantleLightsaber()
		{
			Name = "Mantle of the Force Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1366;
			WeaponAttributes.HitLeechStam = Utility.RandomMinMax(75, 85);
			Attributes.WeaponSpeed = Utility.RandomMinMax(30, 45);
			Attributes.WeaponDamage = Utility.RandomMinMax(25, 50);
			WeaponAttributes.HitLeechHits = Utility.RandomMinMax(55, 75);
			Attributes.RegenStam = Utility.RandomMinMax(5, 7);
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
							Name = "Mantle of the Force Lightsaber";
							EngravedText = "of the Jedi Knight "+ m_Owner.Name.ToString();
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

		public MantleLightsaber( Serial serial ) : base( serial )
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

	public class MeditationLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1173, 1366, 1391, 2908 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public MeditationLightsaber()
		{
			Name = "Meditation crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1328;
			SkillBonuses.SetValues( 0, SkillName.Meditation, 15.0 );
			SkillBonuses.SetValues( 0, SkillName.Focus, 15.0 );
			SkillBonuses.SetValues( 0, SkillName.Magery, 5.0 );
			WeaponAttributes.HitLeechStam = Utility.RandomMinMax(50, 58);
			WeaponAttributes.HitLeechHits = Utility.RandomMinMax(55, 59);
			WeaponAttributes.HitLeechMana = Utility.RandomMinMax(35, 41);
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
							Name = "Meditation crystal Lightsaber";
							EngravedText = "of the Jedi Knight "+ m_Owner.Name.ToString();
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

		public MeditationLightsaber( Serial serial ) : base( serial )
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