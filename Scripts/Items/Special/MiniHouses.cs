using System;
using Server;

namespace Server.Items
{
    public class MiniHouseAddon : BaseAddon
    {
        private MiniHouseType m_Type;

        [CommandProperty(AccessLevel.GameMaster)]
        public MiniHouseType Type
        {
            get { return m_Type; }
            set { m_Type = value; Construct(); }
        }

        public override BaseAddonDeed Deed { get { return new MiniHouseDeed(m_Type); } }
        public override int LabelNumber { get { return MiniHouseInfo.GetInfo(m_Type).LabelNumber; } }

        [Constructable]
        public MiniHouseAddon()
            : this(MiniHouseType.StoneAndPlaster)
        {
        }

        [Constructable]
        public MiniHouseAddon(MiniHouseType type)
        {
            m_Type = type;

            Construct();
        }

        public void Construct()
        {
            foreach (AddonComponent c in Components)
            {
                c.Addon = null;
                c.Delete();
            }

            Components.Clear();

            MiniHouseInfo info = MiniHouseInfo.GetInfo(m_Type);

            if (m_Type == MiniHouseType.GingerBreadHouse)
            {
                AddComponent(new AddonComponent(info.Graphics[0]), 0, 0, 0);
                AddComponent(new AddonComponent(info.Graphics[1]), 1, 0, 0);
                AddComponent(new AddonComponent(info.Graphics[2]), 1, -1, 0);
                return;
            }

            int size = (int)Math.Sqrt(info.Graphics.Length);
            int num = 0;

            for (int y = 0; y < size; ++y)
                for (int x = 0; x < size; ++x)
                    if (info.Graphics[num] != 0x1) // Veteran Rewards Mod
                        AddComponent(new AddonComponent(info.Graphics[num++]), size - x - 1, size - y - 1, 0);
        }

        public MiniHouseAddon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_Type);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Type = (MiniHouseType)reader.ReadInt();
                        break;
                    }
            }
        }
    }

    public class MiniHouseDeed : BaseAddonDeed
    {
        private MiniHouseType m_Type;
        public static MiniHouseType[] MiniHouseDeedCommon = new MiniHouseType[]
		{
			MiniHouseType.Brick, MiniHouseType.FieldStone, MiniHouseType.SmallBrick,
			MiniHouseType.LargeHouseWithPatio, MiniHouseType.SmallMarbleWorkshop, MiniHouseType.SmallStoneWorkshop,
			MiniHouseType.TwoStoryLogCabin, MiniHouseType.TwoStoryStoneAndPlaster, MiniHouseType.TwoStoryWoodAndPlaster,
			MiniHouseType.Wooden, MiniHouseType.StoneAndPlaster			
		};
        public static MiniHouseType[] MiniHouseDeedSemiRare = new MiniHouseType[]
		{
			MiniHouseType.SmallStoneTower, MiniHouseType.TwoStoryVilla, MiniHouseType.MarbleHouseWithPatio,
			MiniHouseType.WoodAndPlaster, MiniHouseType.SandstoneHouseWithPatio
		};
        public static MiniHouseType[] MiniHouseDeedRare = new MiniHouseType[]
		{
			MiniHouseType.ThatchedRoof
		};
        public static MiniHouseType[] MiniHouseDeedVeryRare = new MiniHouseType[]
		{
			MiniHouseType.SmallStoneKeep, MiniHouseType.Tower
		};
        public static MiniHouseType[] MiniHouseDeedUltraRare = new MiniHouseType[]
		{
			MiniHouseType.ChurchAtNight, MiniHouseType.Castle 
		};

        [CommandProperty(AccessLevel.GameMaster)]
        public MiniHouseType Type
        {
            get { return m_Type; }
            set
            {
                m_Type = (value == MiniHouseType.Random) ? RandomMiniHouseDeed() : value;
                InvalidateProperties();
            }
        }

        public override BaseAddon Addon { get { return new MiniHouseAddon(m_Type); } }

        public override int LabelNumber
        {
            get
            {
                switch (m_Type)
                {
                    case MiniHouseType.GingerBreadHouse: return 1077394; // a Gingerbread House Deed
                    default: return 1062096;  // a mini house deed
                }
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(MiniHouseInfo.GetInfo(m_Type).LabelNumber);
        }

        [Constructable]
        public MiniHouseDeed()
            : this(MiniHouseType.Random)
        {
        }

        [Constructable]
        public MiniHouseDeed(MiniHouseType type)
        {
            m_Type = (type == MiniHouseType.Random) ? RandomMiniHouseDeed() : type;
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public MiniHouseDeed(Serial serial)
            : base(serial)
        {
        }

        private MiniHouseType RandomMiniHouseDeed()
        {
            Double rnd = Utility.RandomDouble();
            if (rnd < .86)
                return MiniHouseDeedCommon[Utility.Random(MiniHouseDeedCommon.Length)];
            else if (rnd < .965)
                return MiniHouseDeedSemiRare[Utility.Random(MiniHouseDeedSemiRare.Length)];
            else if (rnd < .985)
                return MiniHouseDeedRare[Utility.Random(MiniHouseDeedRare.Length)];
            else if (rnd < .995)
                return MiniHouseDeedVeryRare[Utility.Random(MiniHouseDeedVeryRare.Length)];
            else
                return MiniHouseDeedUltraRare[Utility.Random(MiniHouseDeedUltraRare.Length)];
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_Type);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Type = (MiniHouseType)reader.ReadInt();
                        break;
                    }
            }

            if (Weight == 0.0)
                Weight = 1.0;
        }
    }

    public enum MiniHouseType
    {
        StoneAndPlaster,
        FieldStone,
        SmallBrick,
        Wooden,
        WoodAndPlaster,
        ThatchedRoof,
        Brick,
        TwoStoryWoodAndPlaster,
        TwoStoryStoneAndPlaster,
        Tower,
        SmallStoneKeep,
        Castle,
        LargeHouseWithPatio,
        MarbleHouseWithPatio,
        SmallStoneTower,
        TwoStoryLogCabin,
        TwoStoryVilla,
        SandstoneHouseWithPatio,
        SmallStoneWorkshop,
        SmallMarbleWorkshop,
        MalasMountainPass,	//Veteran reward house
        ChurchAtNight,		//Veteran reward house
        GingerBreadHouse,
        Random
    }

    public class MiniHouseInfo
    {
        private int[] m_Graphics;
        private int m_LabelNumber;

        public int[] Graphics { get { return m_Graphics; } }
        public int LabelNumber { get { return m_LabelNumber; } }

        public MiniHouseInfo(int start, int count, int labelNumber)
        {
            m_Graphics = new int[count];

            for (int i = 0; i < count; ++i)
                m_Graphics[i] = start + i;

            m_LabelNumber = labelNumber;
        }

        public MiniHouseInfo(int labelNumber, params int[] graphics)
        {
            m_LabelNumber = labelNumber;
            m_Graphics = graphics;
        }

        private static MiniHouseInfo[] m_Info = new MiniHouseInfo[]
			{
				/* Stone and plaster house           */ new MiniHouseInfo( 0x22C4, 1, 1011303 ),
				/* Field stone house                 */ new MiniHouseInfo( 0x22DE, 1, 1011304 ),
				/* Small brick house                 */ new MiniHouseInfo( 0x22DF, 1, 1011305 ),
				/* Wooden house                      */ new MiniHouseInfo( 0x22C9, 1, 1011306 ),
				/* Wood and plaster house            */ new MiniHouseInfo( 0x22E0, 1, 1011307 ),
				/* Thatched-roof cottage             */ new MiniHouseInfo( 0x22E1, 1, 1011308 ),
				/* Brick house                       */ new MiniHouseInfo( 1011309, 0x22CD, 0x22CB, 0x22CC, 0x22CA ),
				/* Two-story wood and plaster house  */ new MiniHouseInfo( 1011310, 0x2301, 0x2302, 0x2304, 0x2303 ),
				/* Two-story stone and plaster house */ new MiniHouseInfo( 1011311, 0x22FC, 0x22FD, 0x22FF, 0x22FE ),
				/* Tower                             */ new MiniHouseInfo( 1011312, 0x22F7, 0x22F8, 0x22FA, 0x22F9 ),
				/* Small stone keep                  */ new MiniHouseInfo( 0x22E6, 9, 1011313 ),
				/* Castle                            */ new MiniHouseInfo( 1011314, 0x22CE, 0x22D0, 0x22D2, 0x22D7, 0x22CF, 0x22D1, 0x22D4, 0x22D9, 0x22D3, 0x22D5, 0x22D6, 0x22DB, 0x22D8, 0x22DA, 0x22DC, 0x22DD ),
				/* Large house with patio            */ new MiniHouseInfo( 0x22E2, 4, 1011315 ),
				/* Marble house with patio           */ new MiniHouseInfo( 0x22EF, 4, 1011316 ),
				/* Small stone tower                 */ new MiniHouseInfo( 0x22F5, 1, 1011317 ),
				/* Two-story log cabin               */ new MiniHouseInfo( 0x22FB, 1, 1011318 ),
				/* Two-story villa                   */ new MiniHouseInfo( 0x2300, 1, 1011319 ),
				/* Sandstone house with patio        */ new MiniHouseInfo( 0x22F3, 1, 1011320 ),
				/* Small stone workshop              */ new MiniHouseInfo( 0x22F6, 1, 1011321 ),
				/* Small marble workshop             */ new MiniHouseInfo( 0x22F4, 1, 1011322 ),
				/* Malas Mountain Pass               */ new MiniHouseInfo( 1062692, 0x2316, 0x2315, 0x2314, 0x2313 ),
				/* Church At Night                   */ new MiniHouseInfo( 1072215, 0x2318, 0x2317, 0x2319, 0x1 ),
				/* Gingerbread House                 */ new MiniHouseInfo( 1077395, 0x2BE5, 0x2BE6, 0x2BE7 )

			};

        public static MiniHouseInfo GetInfo(MiniHouseType type)
        {
            int v = (int)type;

            if (v < 0 || v >= m_Info.Length)
                v = 0;

            return m_Info[v];
        }
    }
}