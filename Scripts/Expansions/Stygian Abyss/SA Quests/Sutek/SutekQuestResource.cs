using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public enum SutekResourceType
	{
        BarrelHoops,
        BarrelStaves,
        Beeswax,
        BlackPowder,
        BluePotion,
        BluePowder,
        Bones,
        BrownStone,
        CopperIngots,
        CopperWire,
        DarkStone,
        Feathers,
        FetidEssence,
        Gears,
        GoldIngots,
        GoldWire,
        IronIngots,
        IronWire,
        Leather,
        MeltedWax,
        OilOfVitriol,
        PowerCrystal,
        PurplePotion,
        RedPotion,
        Rope,
        Scales,
        Shafts,
        SilverIngots,
        SilverWire,
        SpiritEssence,
        Thorns,
        VoidEssence,
        WhitePowder,
        WhiteStone,
        WoodenBoards,
        WoodenLogs,
        YellowPotion,

//        BarrelHoops2,
	}

	public class SutekQuestResource : Item
	{
/*
        m_Table = new Hashtable();    // actual resource, hue, label number, itemid
 
        m_Table[SutekResourceType.BarrelHoops]      = new int[] { (int)SutekResourceType.BarrelHoops, 0, 1011228, 0x1DB7 };
        m_Table[SutekResourceType.BarrelStaves]     = new int[] { (int)SutekResourceType.BarrelStaves, 0, 1015102, 0x1EB1 };
        m_Table[SutekResourceType.Beeswax]          = new int[] { (int)SutekResourceType.Beeswax, 0, 1025154, 0x1422 };
        m_Table[SutekResourceType.BlackPowder]      = new int[] { (int)SutekResourceType.BlackPowder, 1, 1112815, 0x0B48 };
        m_Table[SutekResourceType.BluePotion]       = new int[] { (int)SutekResourceType.BluePotion, 0, 1023848, 0x182A };
        m_Table[SutekResourceType.BluePowder]       = new int[] { (int)SutekResourceType.BluePowder, 0, 1112817, 0x241E };
        m_Table[SutekResourceType.Bones]            = new int[] { (int)SutekResourceType.Bones, 0, 1023786, 0x318C };
        m_Table[SutekResourceType.BrownStone]       = new int[] { (int)SutekResourceType.BrownStone, 2413, 1112814, 0x1772 };
        m_Table[SutekResourceType.CopperIngots]     = new int[] { (int)SutekResourceType.CopperIngots, 0, 1027140, 0x1BE8 };
        m_Table[SutekResourceType.CopperWire]       = new int[] { (int)SutekResourceType.CopperWire, 0, 1026265, 0x1879 };
        m_Table[SutekResourceType.DarkStone]        = new int[] { (int)SutekResourceType.DarkStone, 2406, 1112866, 0x1776 };
        m_Table[SutekResourceType.Feathers]         = new int[] { (int)SutekResourceType.Feathers, 0, 1023578, 0x1BD3 };
        m_Table[SutekResourceType.FetidEssence]     = new int[] { (int)SutekResourceType.FetidEssence, 0, 1031066, 0x2D92 };
        m_Table[SutekResourceType.Gears]            = new int[] { (int)SutekResourceType.Gears, 0, 1024177, 0x1051 };
        m_Table[SutekResourceType.GoldIngots]       = new int[] { (int)SutekResourceType.GoldIngots, 0, 1027146, 0x1BEE };
        m_Table[SutekResourceType.GoldWire]         = new int[] { (int)SutekResourceType.GoldWire, 0, 1026264, 0x1878 };
        m_Table[SutekResourceType.IronIngots]       = new int[] { (int)SutekResourceType.IronIngots, 0, 1027151, 0x1BF4 };
        m_Table[SutekResourceType.IronWire]         = new int[] { (int)SutekResourceType.IronWire, 0, 1026262, 0x1876 };
        m_Table[SutekResourceType.Leather]          = new int[] { (int)SutekResourceType.Leather, 0, 1024216, 0x1078 };
        m_Table[SutekResourceType.MeltedWax]        = new int[] { (int)SutekResourceType.MeltedWax, 0, 1016492, 0x142B };
        m_Table[SutekResourceType.OilOfVitriol]     = new int[] { (int)SutekResourceType.OilOfVitriol, 0, 1077482, 0x098D };
        m_Table[SutekResourceType.PowerCrystal]     = new int[] { (int)SutekResourceType.PowerCrystal, 0, 1112811, 0x1F1C };
        m_Table[SutekResourceType.PurplePotion]     = new int[] { (int)SutekResourceType.PurplePotion, 0, 1023853, 0x1839 };
        m_Table[SutekResourceType.RedPotion]        = new int[] { (int)SutekResourceType.RedPotion, 0, 1023851, 0x1838 };
        m_Table[SutekResourceType.Rope]             = new int[] { (int)SutekResourceType.Rope, 0, 1020934, 0x14F8 };
        m_Table[SutekResourceType.Scales]           = new int[] { (int)SutekResourceType.Scales, 0, 1029905, 0x26B4 };
        m_Table[SutekResourceType.Shafts]           = new int[] { (int)SutekResourceType.Shafts, 0, 1015158, 0x1BD6 };
        m_Table[SutekResourceType.SilverIngots]     = new int[] { (int)SutekResourceType.SilverIngots, 0, 1027158, 0x1BFA };
        m_Table[SutekResourceType.SilverWire]       = new int[] { (int)SutekResourceType.SilverWire, 0, 1026263, 0x1877 };
        m_Table[SutekResourceType.SpiritEssence]    = new int[] { (int)SutekResourceType.SpiritEssence, 1153, 1055029, 0x2D92 };
        m_Table[SutekResourceType.Thorns]           = new int[] { (int)SutekResourceType.Thorns, 0, 1112813,  0x0F42 };
        m_Table[SutekResourceType.VoidEssence]      = new int[] { (int)SutekResourceType.VoidEssence, 1, 1112327, 0x2D92 };
        m_Table[SutekResourceType.WhitePowder]      = new int[] { (int)SutekResourceType.WhitePowder, 0, 1112816, 0x241D };
        m_Table[SutekResourceType.WhiteStone]       = new int[] { (int)SutekResourceType.WhiteStone, 0, 1112813, 0x177A };
        m_Table[SutekResourceType.WoodenBoards]     = new int[] { (int)SutekResourceType.WoodenBoards, 0, 1021189, 0x1BDC };
        m_Table[SutekResourceType.WoodenLogs]       = new int[] { (int)SutekResourceType.WoodenLogs, 0, 1021217, 0x1BDF };
        m_Table[SutekResourceType.YellowPotion]     = new int[] { (int)SutekResourceType.YellowPotion, 0, 1023852, 0x183B };
*/

        private static int[][] m_Table =   // actual resource, hue, label id, graphic
		{
			new int[] { (int)SutekResourceType.BarrelHoops, 0, 1011228, 0x1DB7 },
			new int[] { (int)SutekResourceType.BarrelStaves, 0, 1015102, 0x1EB1 },
			new int[] { (int)SutekResourceType.Beeswax, 0, 1025154, 0x1422 },
			new int[] { (int)SutekResourceType.BlackPowder, 1, 1112815, 0x0B48 },
			new int[] { (int)SutekResourceType.BluePotion, 0, 1023848, 0x182A },
			new int[] { (int)SutekResourceType.BluePowder, 0, 1112817, 0x241E },
			new int[] { (int)SutekResourceType.Bones, 0, 1023786, 0x318C },
			new int[] { (int)SutekResourceType.BrownStone, 2413, 1112814, 0x1772 },
			new int[] { (int)SutekResourceType.CopperIngots, 0, 1027140, 0x1BE8 },
			new int[] { (int)SutekResourceType.CopperWire, 0, 1026265, 0x1879 },
			new int[] { (int)SutekResourceType.DarkStone, 2406, 1112866, 0x1776 },
			new int[] { (int)SutekResourceType.Feathers, 0, 1023578, 0x1BD3 },
			new int[] { (int)SutekResourceType.FetidEssence, 0, 1031066, 0x2D92 },
			new int[] { (int)SutekResourceType.Gears, 0, 1024177, 0x1051 },
			new int[] { (int)SutekResourceType.GoldIngots, 0, 1027146, 0x1BEE },
			new int[] { (int)SutekResourceType.GoldWire, 0, 1026264, 0x1878 },
			new int[] { (int)SutekResourceType.IronIngots, 0, 1027151, 0x1BF4 },
			new int[] { (int)SutekResourceType.IronWire, 0, 1026262, 0x1876 },
			new int[] { (int)SutekResourceType.Leather, 0, 1024216, 0x1078 },
			new int[] { (int)SutekResourceType.MeltedWax, 0, 1016492, 0x142B },
			new int[] { (int)SutekResourceType.OilOfVitriol, 0, 1077482, 0x098D },
			new int[] { (int)SutekResourceType.PowerCrystal, 0, 1112811, 0x1F1C },
			new int[] { (int)SutekResourceType.PurplePotion, 0, 1023853, 0x1839 },
			new int[] { (int)SutekResourceType.RedPotion, 0, 1023851, 0x1838 },
			new int[] { (int)SutekResourceType.Rope, 0, 1020934, 0x14F8 },
			new int[] { (int)SutekResourceType.Scales, 0, 1029905, 0x26B4 },
			new int[] { (int)SutekResourceType.Shafts, 0, 1015158, 0x1BD6 },
			new int[] { (int)SutekResourceType.SilverIngots, 0, 1027158, 0x1BFA },
			new int[] { (int)SutekResourceType.SilverWire, 0, 1026263, 0x1877 },
			new int[] { (int)SutekResourceType.SpiritEssence, 1153, 1055029, 0x2D92 },
			new int[] { (int)SutekResourceType.Thorns, 0, 1112813,  0x0F42 },
			new int[] { (int)SutekResourceType.VoidEssence, 1, 1112327, 0x2D92 },
			new int[] { (int)SutekResourceType.WhitePowder, 0, 1112816, 0x241D },
			new int[] { (int)SutekResourceType.WhiteStone, 0, 1112813, 0x177A },
			new int[] { (int)SutekResourceType.WoodenBoards, 0, 1021189, 0x1BDC },
			new int[] { (int)SutekResourceType.WoodenLogs, 0, 1021217, 0x1BDF },
			new int[] { (int)SutekResourceType.YellowPotion, 0, 1023852, 0x183B },
        };

        public static SutekResourceType GetRandomResource()
        {
            int[] vals = (int[]) Enum.GetValues( typeof(SutekResourceType) );
            return (SutekResourceType)vals[Utility.Random( (int)SutekResourceType.YellowPotion + 1)];
        }

        SutekResourceType m_Type;
        int m_Label;

        [CommandProperty(AccessLevel.GameMaster)]
        public SutekResourceType ResourceType
        {
            get { return m_Type;}
            set { m_Type = value; Update();}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SutekResourceType ActualType
        {
            get { return (SutekResourceType)m_Table[(int)m_Type][0];}
        }

        public static int GetLabelId( SutekResourceType type )
        {
            return m_Table[(int)type][2];
        }

        private void Update()
        {
            int[] vals = m_Table[(int)m_Type];

            Hue = vals[1];
//            m_Label = vals[2];
            ItemID = vals[3];

            InvalidateProperties();
        }

		public override bool DisplayWeight { get { return false; } }

        public override int LabelNumber
        {
            get { return m_Label;}
        }

		[Constructable]
		public SutekQuestResource() : this( SutekResourceType.BarrelHoops )
		{
		}

		[Constructable]
		public SutekQuestResource(SutekResourceType type) : base( 0x1DB7 )
		{
            ResourceType = type;
            Movable = false;
		}
		/*
		[Constructable]
		public SutekQuestResource() : this( SutekResourceType.BarrelStaves )
		{
		}

		[Constructable]
		public SutekQuestResource(SutekResourceType type) : base( 0x1EB1 )
		{
            ResourceType = type;
            Movable = false;
		}
		*/

//		public override void SendPropertiesTo( Mobile from )
//		{
//		}

		public override void GetProperties( ObjectPropertyList list )
        {
        }

		public override void OnSingleClick( Mobile from )
		{
				LabelTo( from, m_Label );
		}

        public SutekQuestResource(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

            writer.Write( (int) m_Type );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            switch (version)
            {
            case 1:
                m_Type = (SutekResourceType) reader.ReadInt();
                break;
            }
		}
	}
}
