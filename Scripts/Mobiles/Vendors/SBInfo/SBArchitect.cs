using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBArchitect : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBArchitect()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "1041280", typeof( InteriorDecorator ), 2000, 20, 0xFC1, 0 ) );
				Add( new GenericBuyInfo( "Improved Decorator", typeof( ImprovedDecorator ), 8000, 20, 0xFC1, 0 ) );
				if ( Core.AOS )
					Add( new GenericBuyInfo( "1060651", typeof( HousePlacementTool ), 627, 20, 0x14F6, 0 ));
				Add( new GenericBuyInfo( "Yard Wand", typeof( YardWand ), 9000, 5, 9569, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( InteriorDecorator ), 1000 );

				if ( Core.AOS )
					Add( typeof( HousePlacementTool ), 301 );
				Add( typeof( YardWand ), 4500 );
			}
		}
	}
}