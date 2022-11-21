using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBMapmaker : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMapmaker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BlankMap ), 5, 40, 0x14EC, 0 ) );
				Add( new GenericBuyInfo( typeof( MapmakersPen ), 8, 20, 0x0FBF, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 12, 40, 0xEF3, 0 ) );
		        Add( new GenericBuyInfo( typeof( TreasureMap ), 1000, 20, 0x14EC, 0, new object[] { 1, Map.Trammel } ) );
		        Add( new GenericBuyInfo( typeof( TreasureMap ), 2000, 20, 0x14EC, 0, new object[] { 2, Map.Trammel } ) );
                Add( new GenericBuyInfo( typeof( TreasureMap ), 3000, 20, 0x14EC, 0, new object[] { 3, Map.Trammel } ) );
                Add( new GenericBuyInfo( typeof( TreasureMap ), 4000, 20, 0x14EC, 0, new object[] { 4, Map.Trammel } ) );
                //Add( new GenericBuyInfo( typeof( TreasureMap ), 5000, 20, 0x14EC, 0, new object[] { 5, Map.Trammel } ) );
				//Add( new GenericBuyInfo( typeof( TreasureMap ), 50000, 20, 0x14EC, 0, new object[] { 6, Map.Trammel } ) );
		        Add( new GenericBuyInfo( typeof( TreasureMap ), 1000, 20, 0x14EC, 37, new object[] { 1, Map.Felucca } ) );
		        Add( new GenericBuyInfo( typeof( TreasureMap ), 2000, 20, 0x14EC, 37, new object[] { 2, Map.Felucca } ) );
		        Add( new GenericBuyInfo( typeof( TreasureMap ), 3000, 20, 0x14EC, 37, new object[] { 3, Map.Felucca } ) );
		        Add( new GenericBuyInfo( typeof( TreasureMap ), 4000, 20, 0x14EC, 37, new object[] { 4, Map.Felucca } ) );
		        //Add( new GenericBuyInfo( typeof( TreasureMap ), 5000, 20, 0x14EC, 37, new object[] { 5, Map.Felucca } ) );
				//Add( new GenericBuyInfo( typeof( TreasureMap ), 50000, 20, 0x14EC, 37, new object[] { 6, Map.Felucca } ) );
				
				for ( int i = 0; i < PresetMapEntry.Table.Length; ++i )
					Add( new PresetMapBuyInfo( PresetMapEntry.Table[i], Utility.RandomMinMax( 7, 10 ), 20 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BlankScroll ), 6 );
				Add( typeof( MapmakersPen ), 4 );
				Add( typeof( BlankMap ), 2 );
				Add( typeof( CityMap ), 3 );
				Add( typeof( LocalMap ), 3 );
				Add( typeof( WorldMap ), 3 );
				Add( typeof( PresetMapEntry ), 3 );
				//TODO: Buy back maps that the mapmaker sells!!!
			}
		}
	}
}