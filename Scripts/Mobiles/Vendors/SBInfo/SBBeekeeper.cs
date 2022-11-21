using System;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Apiculture;

namespace Server.Mobiles
{
	public class SBBeekeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBeekeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( JarHoney ), 3, 20, 0x9EC, 0 ) );
				//Add( new GenericBuyInfo( typeof( Beeswax ), 2, 20, 0x1422, 0 ) );
				Add( new GenericBuyInfo( "A Beehive", typeof( BeeHiveDeed ), 2000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "A Beehive House", typeof( BeeHiveHouseDeed ), 2500, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Wax Crafting Beehive", typeof( apiBeeHiveDeed ), 3500, 10, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( typeof( HiveTool ), 100, 20, 2549, 0 ) );
				Add( new GenericBuyInfo( typeof( apiWaxProcessingPot ), 400, 20, 2532, 0 ) );
				Add( new GenericBuyInfo( typeof( WaxCraftingPot ), 250, 20, 5162, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( JarHoney ), 1 );
				Add( typeof( Beeswax ), 1 );
				Add( typeof( apiBeeHiveDeed ), 1000 );
				Add( typeof( HiveTool ), 50 );
				Add( typeof( apiWaxProcessingPot ), 125 );
				Add( typeof( WaxCraftingPot ), 100 );
			}
		}
	}
}