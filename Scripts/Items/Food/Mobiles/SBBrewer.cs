using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBBrewer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBrewer() { }

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Keg ), 50, 20, 0xE7F, 0 ) );
				Add( new GenericBuyInfo( typeof( Malt ), 100, 20, 0x103D, 0 ) );
				Add( new GenericBuyInfo( typeof( Barley ), 100, 20, 0x103F, 0 ) );
				Add( new GenericBuyInfo( typeof( BrewersYeast ), 200, 20, 0x103F, 0 ) );
				Add( new GenericBuyInfo( typeof( BagOfSugar ), 200, 20, 4165, 0 ) );
				Add( new GenericBuyInfo( typeof( EmptyAleBottle ), 5, 20, 0x99B, 0 ) );
				Add( new GenericBuyInfo( typeof( EmptyMeadBottle ), 5, 20, 0x99B, 0 ) );
				Add( new GenericBuyInfo( typeof( EmptyWineBottle ), 5, 20, 0xF0E, 0 ) );
				Add( new GenericBuyInfo( typeof( EmptyJug ), 10, 20, 0x9C8, 0 ) );
				Add( new GenericBuyInfo( typeof( WinecraftersTools ), 5000, 5, 0xF00, 0 ) );
				Add( new GenericBuyInfo( typeof( BrewersTools ), 5000, 20, 0x1EBC, 0 ) );
				Add( new GenericBuyInfo( "Vineyard Label", typeof( VinyardLabelMaker ), 1000, 5, 0xFC0, 0 ) );
				Add( new GenericBuyInfo( typeof( BreweryLabelMaker ), 1000, 20, 0xFBF, 0 ) );
				Add( new GenericBuyInfo( typeof( GrapevinePlacementTool ), 10000, 5, 0xD1A, 0 ) );
				Add( new GenericBuyInfo( "Vineyard Deed", typeof( VinyardGroundAddonDeed ), 100000, 5, 0x14F0, 0 ) );
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