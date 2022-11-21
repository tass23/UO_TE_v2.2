using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles
{
	public class SBCityManager : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBCityManager() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( "Asian City Hall Deed", typeof( AsianCityHallDeed ), 250000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Necro City Hall Deed", typeof( NecroCityHallDeed ), 250000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Stone City Hall Deed", typeof( StoneCityHallDeed ), 250000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "FieldStone City Hall Deed", typeof( FieldStoneCityHallDeed ), 250000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Plaster City Hall Deed", typeof( PlasterCityHallDeed ), 250000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Wood City Hall Deed", typeof( WoodCityHallDeed ), 250000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Marble City Hall Deed", typeof( MarbleCityHallDeed ), 250000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Sandstone City Hall Deed", typeof( SandstoneCityHallDeed ), 250000, 10, 0x14F0, 0 ) );
				
				Add( new GenericBuyInfo( "Asian City Bank Deed", typeof( AsianCityBankDeed ), 50000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Necro City Bank Deed", typeof( NecroCityBankDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Stone City Bank Deed", typeof( StoneCityBankDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Fieldstone City Bank Deed", typeof( FieldstoneCityBankDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Plaster City Bank Deed", typeof( PlasterCityBankDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Wood City Bank Deed", typeof( WoodCityBankDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Marble City Bank Deed", typeof( MarbleCityBankDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Sandstone City Bank Deed", typeof( SandstoneCityBankDeed ), 50000, 10, 0x14F0, 0 ) ); 
				
				Add( new GenericBuyInfo( "Asian City Healer Deed", typeof( AsianCityHealerDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Necro City Healer Deed", typeof( NecroCityHealerDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Stone City Healer Deed", typeof( StoneCityHealerDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Fieldstone City Healer Deed", typeof( FieldstoneCityHealerDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Plaster City Healer Deed", typeof( PlasterCityHealerDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Wood City Healer Deed", typeof( WoodCityHealerDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Marble City Healer Deed", typeof( MarbleCityHealerDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Sandstone City Healer Deed", typeof( SandstoneCityHealerDeed ), 55000, 10, 0x14F0, 0 ) ); 
				
				Add( new GenericBuyInfo( "Asian City Moongate Deed", typeof( AsianCityMoongateDeed ), 25000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Necro City Moongate Deed", typeof( NecroCityMoongateDeed ), 25000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Marble City Moongate Deed", typeof( MarbleCityMoongateDeed ), 25000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Sandstone City Moongate Deed", typeof( SandstoneCityMoongateDeed ), 25000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Stone City Moongate Deed", typeof( StoneCityMoongateDeed ), 25000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Wood City Moongate Deed", typeof( WoodCityMoongateDeed ), 25000, 10, 0x14F0, 0 ) ); 
				
				Add( new GenericBuyInfo( "Asian City Stable Deed", typeof( AsianCityStableDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Necro City Stable Deed", typeof( NecroCityStableDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Stone City Stable Deed", typeof( StoneCityStableDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Fieldstone City Stable Deed", typeof( FieldstoneCityStableDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Plaster City Stable Deed", typeof( PlasterCityStableDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Wood City Stable Deed", typeof( WoodCityStableDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Marble City Stable Deed", typeof( MarbleCityStableDeed ), 55000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Sandstone City Stable Deed", typeof( SandstoneCityStableDeed ), 55000, 10, 0x14F0, 0 ) ); 
				
				Add( new GenericBuyInfo( "Asian City Tavern Deed", typeof( AsianCityTavernDeed ), 70000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Necro City Tavern Deed", typeof( NecroCityTavernDeed ), 70000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Stone City Tavern Deed", typeof( StoneCityTavernDeed ), 70000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Fieldstone City Tavern Deed", typeof( FieldstoneCityTavernDeed ), 70000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Plaster City Tavern Deed", typeof( PlasterCityTavernDeed ), 70000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Wood City Tavern Deed", typeof( WoodCityTavernDeed ), 70000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Marble City Tavern Deed", typeof( MarbleCityTavernDeed ), 70000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Sandstone City Tavern Deed", typeof( SandstoneCityTavernDeed ), 70000, 10, 0x14F0, 0 ) ); 
				
				Add( new GenericBuyInfo( "Small City Park Deed", typeof( SmallCityParkDeed ), 20000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Medium City Park Deed", typeof( MediumCityParkDeed ), 35000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Large City Park Deed", typeof( LargeCityParkDeed ), 50000, 10, 0x14F0, 0 ) ); 
				
				Add( new GenericBuyInfo( "Small City Garden Deed", typeof( SmallCityGardenDeed ), 20000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Medium City Garden Deed", typeof( MediumCityGardenDeed ), 35000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Large City Garden Deed", typeof( LargeCityGardenDeed ), 50000, 10, 0x14F0, 0 ) ); 
				
				Add( new GenericBuyInfo( "City Contract of Employment", typeof( CityContractOfEmployment ), 1000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "City Resource Box Deed", typeof( CityResourceBoxDeed ), 5000, 10, 0x14F0, 0 ) ); 
				
				Add( new GenericBuyInfo( "Asian City Market Deed", typeof( AsianCityMarketDeed ), 50000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Necro City Market Deed", typeof( NecroCityMarketDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Stone City Market Deed", typeof( StoneCityMarketDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Fieldstone City Market Deed", typeof( FieldStoneCityMarketDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Plaster City Market Deed", typeof( PlasterCityMarketDeed ), 50000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Wood City Market Deed", typeof( WoodCityMarketDeed ), 50000, 10, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( "Marble City Market Deed", typeof( MarbleCityMarketDeed ), 50000, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Sandstone City Market Deed", typeof( SandstoneCityMarketDeed ), 50000, 10, 0x14F0, 0 ) ); 
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				/*Add( typeof( BreadLoaf ), 3 ); 
				Add( typeof( CheeseWheel ), 12 ); 
				Add( typeof( FrenchBread ), 1 ); 
				Add( typeof( FriedEggs ), 4 ); 
				Add( typeof( Cake ), 5 ); 
				Add( typeof( Cookies ), 3 ); 
				Add( typeof( Muffins ), 2 ); 
				Add( typeof( CheesePizza ), 7 ); 
				Add( typeof( ApplePie ), 5 ); 
				Add( typeof( PeachCobbler ), 5 ); 
				Add( typeof( Quiche ), 12 ); 
				Add( typeof( Dough ), 4 ); 
				Add( typeof( JarHoney ), 1 ); 
				Add( typeof( Pitcher ), 5 );
				Add( typeof( SackFlour ), 1 ); 
				Add( typeof( Eggs ), 1 ); */
			} 
		} 
	} 
	
	
	
	
	
	
	
}
