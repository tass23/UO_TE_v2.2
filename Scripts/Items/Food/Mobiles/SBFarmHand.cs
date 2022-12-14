using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Items.Crops;

namespace Server.Mobiles
{
	public class SBFarmHand : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBFarmHand(){}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Apple ), 3, 20, 0x9D0, 0 ) );
				Add( new GenericBuyInfo( typeof( Grapes ), 3, 20, 0x9D1, 0 ) );
				Add( new GenericBuyInfo( typeof( Watermelon ), 7, 20, 0xC5C, 0 ) );
				Add( new GenericBuyInfo( typeof( YellowGourd ), 3, 20, 0xC64, 0 ) );
				Add( new GenericBuyInfo( typeof( Pumpkin ), 11, 20, 0xC6A, 0 ) );
				Add( new GenericBuyInfo( typeof( Onion ), 3, 20, 0xC6D, 0 ) );
				Add( new GenericBuyInfo( typeof( Lettuce ), 5, 20, 0xC70, 0 ) );
				Add( new GenericBuyInfo( typeof( Squash ), 3, 20, 0xC72, 0 ) );
				Add( new GenericBuyInfo( typeof( HoneydewMelon ), 7, 20, 0xC74, 0 ) );
				Add( new GenericBuyInfo( typeof( Carrot ), 3, 20, 0xC77, 0 ) );
				Add( new GenericBuyInfo( typeof( Cantaloupe ), 6, 20, 0xC79, 0 ) );
				Add( new GenericBuyInfo( typeof( Cabbage ), 5, 20, 0xC7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Lemon ), 3, 20, 0x1728, 0 ) );
				Add( new GenericBuyInfo( typeof( Lime ), 3, 20, 0x172A, 0 ) );
				Add( new GenericBuyInfo( typeof( Peach ), 3, 20, 0x9D2, 0 ) );
				Add( new GenericBuyInfo( typeof( Pear ), 3, 20, 0x994, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Apple ), 1 );
				Add( typeof( Grapes ), 1 );
				Add( typeof( Watermelon ), 3 );
				Add( typeof( YellowGourd ), 1 );
				Add( typeof( Pumpkin ), 5 );
				Add( typeof( Onion ), 1 );
				Add( typeof( Lettuce ), 2 );
				Add( typeof( Squash ), 1 );
				Add( typeof( Carrot ), 1 );
				Add( typeof( HoneydewMelon ), 3 );
				Add( typeof( Cantaloupe ), 3 );
				Add( typeof( Cabbage ), 2 );
				Add( typeof( Lemon ), 1 );
				Add( typeof( Lime ), 1 );
				Add( typeof( Peach ), 1 );
				Add( typeof( Pear ), 1 );
			}
		}
	}
}