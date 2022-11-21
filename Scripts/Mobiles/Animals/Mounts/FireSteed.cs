using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a fire steed corpse" )]
	public class FireSteed : BaseMount
	{
        private bool m_BardingExceptional;
        private Mobile m_BardingCrafter;
        private int m_BardingHP;
        private bool m_HasBarding;
        private CraftResource m_BardingResource;
        public Body bodyVal;
        public int idVal;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile BardingCrafter
        {
            get { return m_BardingCrafter; }
            set { m_BardingCrafter = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool BardingExceptional
        {
            get { return m_BardingExceptional; }
            set { m_BardingExceptional = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BardingHP
        {
            get { return m_BardingHP; }
            set { m_BardingHP = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasBarding
        {
            get { return m_HasBarding; }
            set
            {
                m_HasBarding = value;

                if (m_HasBarding)
                {
                    Hue = CraftResources.GetHue(m_BardingResource);
                    BodyValue = 284;
                    ItemID = 0x3E92;
                }
                else
                {
                    Hue = 0;
                    BodyValue = bodyVal;
                    ItemID = idVal;
                }

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource BardingResource
        {
            get { return m_BardingResource; }
            set
            {
                m_BardingResource = value;

                if (m_HasBarding)
                    Hue = CraftResources.GetHue(value);

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BardingMaxHP
        {
            get { return m_BardingExceptional ? 2500 : 1000; }
        }
		[Constructable]
		public FireSteed() : this( "a fire steed" )
		{
		}

		[Constructable]
		public FireSteed( string name ) : base( name, 0xBE, 0x3E9E, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0xA8;

			SetStr( 376, 400 );
			SetDex( 91, 120 );
			SetInt( 291, 300 );

			SetHits( 226, 240 );

			SetDamage( 11, 30 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 80 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 100.0, 120.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 20000;
			Karma = -20000;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 106.0;
		}

		public override void GenerateLoot()
		{
			PackItem( new SulfurousAsh( Utility.RandomMinMax( 151, 300 ) ) );
			PackItem( new Ruby( Utility.RandomMinMax( 16, 30 ) ) );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Daemon | PackInstinct.Equine; } }

		public FireSteed( Serial serial ) : base( serial )
		{
		}
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_HasBarding && m_BardingExceptional && m_BardingCrafter != null)
                list.Add(1060853, m_BardingCrafter.Name); // armor exceptionally crafted by ~1_val~
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

            writer.Write((bool)m_BardingExceptional);
            writer.Write((Mobile)m_BardingCrafter);
            writer.Write((bool)m_HasBarding);
            writer.Write((int)m_BardingHP);
            writer.Write((int)m_BardingResource);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            switch (version)
			{
                case 1:
					{
                        m_BardingExceptional = reader.ReadBool();
                        m_BardingCrafter = reader.ReadMobile();
                        m_HasBarding = reader.ReadBool();
                        m_BardingHP = reader.ReadInt();
                        m_BardingResource = (CraftResource)reader.ReadInt();
                        break;
					}
			}
		}
	}
}