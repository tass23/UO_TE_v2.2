using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class AdeganLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public AdeganLightsaber()
		{
			Name = "Green Adegan crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1067;
			WeaponAttributes.HitLowerAttack = 40;
			WeaponAttributes.ResistColdBonus = 15;
			Attributes.WeaponDamage = 35;
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
							Name = "Green Adegan crystal Lightsaber";
							EngravedText = "of the Jedi Consular " + m_Owner.Name.ToString();
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

		public AdeganLightsaber( Serial serial ) : base( serial )
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

	public class AllyaRedemptionLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public AllyaRedemptionLightsaber()
		{
			Name = "Allya's Redemption crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1075;
			WeaponAttributes.HitPoisonArea = 75;
			WeaponAttributes.ResistPoisonBonus = 80;
			WeaponAttributes.HitLowerDefend = 15;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 60;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = 80;
			phys = 20;
			cold = fire = nrgy = chaos = direct = 0;
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
							Name = "Allya's Redemption crystal Lightsaber";
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
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Owner != null )
				list.Add( 1072304, m_Owner.Name ); 
		}

		public AllyaRedemptionLightsaber( Serial serial ) : base( serial )
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

	public class BondaraFollyLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public BondaraFollyLightsaber()
		{
			Name = "Bondara's Folly crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1162;
			Attributes.WeaponSpeed = 45;
			Attributes.WeaponDamage = 75;
			//WeaponAttributes.HitLeechHits = 100;
			//Attributes.RegenHits = 5;
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
							Name = "Bondara's Folly crystal Lightsaber";
							EngravedText = "of the Jedi Consular " + m_Owner.Name.ToString();
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

		public BondaraFollyLightsaber( Serial serial ) : base( serial )
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

	public class DagobahLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public DagobahLightsaber()
		{
			Name = "Dawn of Dagobah crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1062;
			WeaponAttributes.HitPoisonArea = 80;
			WeaponAttributes.ResistPoisonBonus = 100;
			WeaponAttributes.HitLeechHits = 50;
			WeaponAttributes.HitLowerDefend = 25;
			Attributes.WeaponSpeed = 45;
			Attributes.WeaponDamage = 75;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = 90;
			phys = 10;
			cold = fire = nrgy = chaos = direct = 0;
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
							Name = "Dawn of Dagobah crystal Lightsaber";
							EngravedText = "of the Jedi Consular " + m_Owner.Name.ToString();
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

		public DagobahLightsaber( Serial serial ) : base( serial )
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

	public class SunriderLightsaber : Lightsaber
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public SunriderLightsaber()
		{
			Name = "Sunrider's Destiny crystal Lightsaber";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1094;
			WeaponAttributes.HitLeechStam = 100;
			Attributes.RegenStam = 4;
			Attributes.WeaponSpeed = 30;
			Attributes.WeaponDamage = 50;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 20;
			phys = 30;
			direct = 50;
			cold = fire = pois = chaos = 0;
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
							Name = "Sunrider's Destiny crystal Lightsaber";
							EngravedText = "of the Jedi Consular " + m_Owner.Name.ToString();
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

		public SunriderLightsaber( Serial serial ) : base( serial )
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