using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBTannerTM : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTannerTM()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
                Add(new GenericBuyInfo(typeof(GargishClothKilt), 10, 20, 0x408, 0));
                Add(new GenericBuyInfo(typeof(GargishClothArms), 37, 20, 0x404, 0));
                Add(new GenericBuyInfo(typeof(GargishClothChest), 47, 20, 0x406, 0));
                Add(new GenericBuyInfo(typeof(GargishClothLegs), 36, 20, 0x40A, 0));
                Add(new GenericBuyInfo(typeof(GargishClothWingArmor), 31, 20, 0x45A4, 0));

                Add( new GenericBuyInfo( typeof( GargishLeatherKilt ), 10, 20, 0x311, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishLeatherArms ), 37, 20, 0x302, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishLeatherChest ), 47, 20, 0x304, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishLeatherLegs ), 36, 20, 0x306, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishLeatherWingArmor ), 31, 20, 0x457E, 0 ) );

                Add(new GenericBuyInfo(typeof(GargishClothKilt), 10, 20, 0x407, 0));
                Add(new GenericBuyInfo(typeof(GargishClothArms), 37, 20, 0x403, 0));
                Add(new GenericBuyInfo(typeof(GargishClothChest), 47, 20, 0x405, 0));
                Add(new GenericBuyInfo(typeof(GargishClothLegs), 36, 20, 0x409, 0));

                Add(new GenericBuyInfo(typeof(GargishLeatherKilt), 10, 20, 0x310, 0));
                Add(new GenericBuyInfo(typeof(GargishLeatherArms), 37, 20, 0x301, 0));
                Add(new GenericBuyInfo(typeof(GargishLeatherChest), 47, 20, 0x303, 0));
                Add(new GenericBuyInfo(typeof(GargishLeatherLegs), 36, 20, 0x305, 0));
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add(typeof(GargishClothKilt), 3);
                Add(typeof(GargishClothArms), 3);
                Add(typeof(GargishClothChest), 7);
				Add(typeof(GargishClothLegs), 5);
                Add(typeof(GargishClothWingArmor), 7);

                Add(typeof(GargishLeatherKilt), 18);
                Add(typeof(GargishLeatherArms), 23);
                Add(typeof(GargishLeatherChest), 15);
                Add(typeof(GargishLeatherLegs), 15);
                Add(typeof(GargishLeatherWingArmor), 18);

                Add(typeof(GargishClothKilt), 43);
                Add(typeof(GargishClothArms), 37);
                Add(typeof(GargishClothChest), 39);
                Add(typeof(GargishClothLegs), 22);

                Add(typeof(GargishLeatherKilt), 31);
                Add(typeof(GargishLeatherArms), 23);
                Add(typeof(GargishLeatherChest), 103);
                Add(typeof(GargishLeatherLegs), 18);
			}
		}
	}
}