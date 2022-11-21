using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class AnkarresLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public AnkarresLightsaber()
		{
			Name = "Ankarres crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1095;
			Attributes.RegenHits = 5;
			SkillBonuses.SetValues( 0, SkillName.Healing, 10.0 );
			//Attributes.WeaponSpeed = 15;
			//WeaponAttributes.ResistColdBonus = 10;
			//Attributes.WeaponDamage = 3;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = cold = pois = chaos = direct = 0;
			phys = 50;
			nrgy = 50;
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
							Name = "Ankarres crystal Lightsaber";
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

		public AnkarresLightsaber( Serial serial ) : base( serial )
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
	
	public class BaasWisdomLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public BaasWisdomLightsaber()
		{
			Name = "Baas' Wisdom crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1100;
			Attributes.RegenMana = 4;
			WeaponAttributes.HitColdArea = 40;
			//Attributes.WeaponSpeed = 10;
			//Attributes.WeaponDamage = 5;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = 70;
			phys = 30;
			pois = fire = nrgy = chaos = direct = 0;
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
							Name = "Baas' Wisdom crystal Lightsaber";
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

		public BaasWisdomLightsaber( Serial serial ) : base( serial )
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
	
	public class KenobiLegacyLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public KenobiLegacyLightsaber()
		{
			Name = "Kenobi's Legacy crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1185;
			Attributes.RegenStam = 2;
			WeaponAttributes.HitMagicArrow = 15;
			//Attributes.WeaponSpeed = 5;
			//Attributes.WeaponDamage = 10;			
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
							Name = "Kenobi's Legacy crystal Lightsaber";
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

		public KenobiLegacyLightsaber( Serial serial ) : base( serial )
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
	
	public class KraytDragonLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public KraytDragonLightsaber()
		{
			Name = "Krayt dragon pearl Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1366;
			Attributes.WeaponDamage = 35;
			WeaponAttributes.HitFireball = 25;
			//Attributes.RegenStam = 4;
			//Attributes.WeaponSpeed = 25;			
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
							Name = "Krayt dragon pearl Lightsaber";
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

		public KraytDragonLightsaber( Serial serial ) : base( serial )
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
	
	public class PermafrostLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public PermafrostLightsaber()
		{
			Name = "Permafrost crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1100;
			Attributes.WeaponDamage = 50;
			WeaponAttributes.HitColdArea = 75;
			//Attributes.WeaponSpeed = 5;			
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = 90;
			phys = 10;
			pois = fire = nrgy = chaos = direct = 0;
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
							Name = "Permafrost crystal Lightsaber";
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

		public PermafrostLightsaber( Serial serial ) : base( serial )
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

	public class UpariLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public UpariLightsaber()
		{
			Name = "Upari crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			Hue = 1488;
			WeaponAttributes.HitLeechMana = 40;
			WeaponAttributes.HitLowerDefend = 10;
			//Attributes.RegenStam = 2;			
			//Attributes.WeaponDamage = 50;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = 30;
			phys = 70;
			pois = fire = nrgy = chaos = direct = 0;
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
							Name = "Upaari crystal Lightsaber";
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

		public UpariLightsaber( Serial serial ) : base( serial )
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