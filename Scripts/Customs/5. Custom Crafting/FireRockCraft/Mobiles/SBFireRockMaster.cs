/* Created by Hammerhand*/

using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBFireRockMaster: SBInfo
	{
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBFireRockMaster()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>

		{
			public InternalBuyInfo()
			{
                //Add(new CustomGenericBuy2Info(typeof(FireRockCraftTool), 500, 5, 0x1EBC, 1358));
                Add(new CustomGenericBuy2Info(typeof(FirePick), 500, 20, 0xE86, 1358));
                Add(new CustomGenericBuy2Info(typeof(FireRockMiningBook), 15000, 5, 0xFF4, 1358));
			}
		}
        
		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
		}
	 }
  }
}
