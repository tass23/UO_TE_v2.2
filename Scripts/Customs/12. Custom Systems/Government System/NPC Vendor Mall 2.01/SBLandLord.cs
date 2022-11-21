using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBLandLord : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLandLord()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				
				Add( new GenericBuyInfo( "1041243", typeof( ContractOfEmployment ), 1025, 20, 0x14F0, 0 ) );
				
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
