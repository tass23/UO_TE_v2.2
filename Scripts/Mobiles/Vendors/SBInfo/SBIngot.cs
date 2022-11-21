using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBIngot : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBIngot()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "1,000 Iron Ingots", typeof( BagOfIronIngots ), 50000, 20, 0xE76, 2401 ) );
				Add( new GenericBuyInfo( "1,000 Dull Copper Ingots", typeof( BagOfDullCopperIngots ), 100000, 20, 0xE76, 2419 ) );
				Add( new GenericBuyInfo( "1,000 Shadow Iron Ingots", typeof( BagOfShadowIronIngots ), 150000, 20, 0xE76, 2406 ) );
				Add( new GenericBuyInfo( "1,000 Copper Ingots", typeof( BagOfCopperIngots ), 200000, 20, 0xE76, 2413 ) );
				Add( new GenericBuyInfo( "1,000 Bronze Ingots", typeof( BagOfBronzeIngots ), 250000, 20, 0xE76, 2418 ) );
				Add( new GenericBuyInfo( "1,000 Gold Ingots", typeof( BagOfGoldIngots ), 300000, 20, 0xE76, 2213 ) );
				Add( new GenericBuyInfo( "1,000 Agapite Ingots", typeof( BagOfAgapiteIngots ), 350000, 20, 0xE76, 2425 ) );
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
