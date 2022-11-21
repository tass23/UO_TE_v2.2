using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBWood : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBWood()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "1,000 Plain Logs", typeof( PlainLogBag ), 50000, 20, 0xE76, 1130 ) );
				Add( new GenericBuyInfo( "1,000 Oak Logs", typeof( OakLogBag ), 100000, 20, 0xE76, 1190 ) );
				Add( new GenericBuyInfo( "1,000 Ash Logs", typeof( AshLogBag ), 150000, 20, 0xE76, 1191 ) );
				Add( new GenericBuyInfo( "1,000 Yew Logs", typeof( YewLogBag ), 200000, 20, 0xE76, 1192 ) );
				Add( new GenericBuyInfo( "1,000 Bloodwood Logs", typeof( BloodwoodLogBag ), 250000, 20, 0xE76, 1194 ) );
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
