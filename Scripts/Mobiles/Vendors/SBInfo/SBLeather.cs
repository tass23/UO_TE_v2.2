using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBLeather : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLeather()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "1,000 Leather Hides", typeof( LeatherBag ), 50000, 20, 0xE76, 2418 ) );
				Add( new GenericBuyInfo( "1,000 Spined Leather Hides", typeof( SpinedLeatherBag ), 100000, 20, 0xE76, 2220 ) );
				Add( new GenericBuyInfo( "1,000 Horned Leather Hides", typeof( HornedLeatherBag ), 150000, 20, 0xE76, 2117 ) );
				Add( new GenericBuyInfo( "1,000 Barbed Leather Hides", typeof( BarbedLeatherBag ), 200000, 20, 0xE76, 2220 ) );
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
