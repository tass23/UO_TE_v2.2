using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class LambentLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1161, 1259, 1358, 2907 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public LambentLightsaber()
		{
			Name = "Lambent crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2116;
			SkillBonuses.SetValues( 0, SkillName.Meditation, 10.0 );
			SkillBonuses.SetValues( 0, SkillName.Focus, 10.0 );
			SkillBonuses.SetValues( 0, SkillName.DetectHidden, 20.0 );
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
							Name = "Lambent crystal Lightsaber";
							EngravedText = "of the Jedi Sentinal " + m_Owner.Name.ToString();
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

		public LambentLightsaber( Serial serial ) : base( serial )
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

	public class LavaLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1161, 1259, 1358, 2907 /*UO-The Expanse Custom Hues*/
		};
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public LavaLightsaber()
		{
			Name = "Lava crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 39;
			WeaponAttributes.HitFireArea = 15;
			WeaponAttributes.HitFireball = 15;
			Attributes.WeaponDamage = 5;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = 100;
			cold = chaos = direct = nrgy = phys = pois = 0;
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
							Name = "Lava crystal Lightsaber";
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

		public LavaLightsaber( Serial serial ) : base( serial )
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

	public class SolariLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1161, 1259, 1358, 2907 /*UO-The Expanse Custom Hues*/
		};
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public SolariLightsaber()
		{
			Name = "Solari crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 44;
			SkillBonuses.SetValues( 0, SkillName.Meditation, 10.0 );
			SkillBonuses.SetValues( 0, SkillName.Focus, 10.0 );
			SkillBonuses.SetValues( 0, SkillName.DetectHidden, 20.0 );
			SkillBonuses.SetValues( 0, SkillName.Magery, 20.0 );
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 50;
			fire = 50;
			cold = chaos = direct = phys = pois = 0;
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
							Name = "Solari crystal Lightsaber";
							EngravedText = "of the Jedi Sentinal " + m_Owner.Name.ToString();
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

		public SolariLightsaber( Serial serial ) : base( serial )
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

	public class VelmoriteLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1161, 1259, 1358, 2907 /*UO-The Expanse Custom Hues*/
		};
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public VelmoriteLightsaber()
		{
			Name = "Velmorite crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 48;
			Attributes.ReflectPhysical = Utility.RandomMinMax(20, 31);
			Attributes.WeaponSpeed = 50;
			Attributes.WeaponDamage = 45;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 50;
			fire = 50;
			cold = chaos = direct = phys = pois = 0;
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
							Name = "Velmorite crystal Lightsaber";
							EngravedText = "of the Jedi Exile " + m_Owner.Name.ToString();
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

		public VelmoriteLightsaber( Serial serial ) : base( serial )
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