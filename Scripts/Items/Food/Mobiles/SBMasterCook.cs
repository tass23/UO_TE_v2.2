using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBMasterCook : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMasterCook()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( FoodPlate ), 5, 20, 0x9D7, 0 ) );
				Add( new GenericBuyInfo( typeof( BakersBoard ), 10, 20, 0x14EA, 0 ) );
				Add( new GenericBuyInfo( typeof( CooksCauldron ), 10, 20, 0x9ED, 0 ) );
				Add( new GenericBuyInfo( typeof( FryingPan ), 10, 20, 0x9E2, 0 ) );
				Add( new GenericBuyInfo( typeof( FoodDyes ), 100, 5, 0xFA9, 0 ) );
				Add( new GenericBuyInfo( typeof( UncookedPizza), 100, 20, 0x1083, 0 ) );

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