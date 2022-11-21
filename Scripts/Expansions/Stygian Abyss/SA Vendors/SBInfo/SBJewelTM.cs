using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBJewelTM: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBJewelTM()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GargishBracelet ), 27, 20, 0x4211, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishRing ), 26, 20, 0x4212, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishNecklace ), 27, 20, 0x4210, 0 ) );


			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				
				Add( typeof( GargishBracelet ), 13 );
				Add( typeof( GargishRing ), 10 );
				Add( typeof( GargishNecklace ), 13 );

				
			}
		}
	}
}
