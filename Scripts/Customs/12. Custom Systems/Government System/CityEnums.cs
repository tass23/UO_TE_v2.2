using System;
using Server.Items;

namespace Server
{
	public enum CivicStrutureType
	{
		/*
		*Design Types
		*
		* 1) FieldStone
		* 2) Stone
		* 3) Wooden
		* 4) Plaster
		* 5) Asian
		* 6) Necro
		* 7) SandStone
		* 8) Marble
		*/

		None,
		//City Halls
		FieldStoneCityHall,
		SandstoneCityHall,
		MarbleCityHall,
		NecroCityHall,
		AsianCityHall,
		WoodCityHall,
		StoneCityHall,
		PlasterCityHall,

		//Banks
		FieldStoneCityBank,
		SandstoneCityBank,
		MarbleCityBank,
		NecroCityBank,
		AsianCityBank,
		WoodCityBank,
		StoneCityBank,
		PlasterCityBank,

		//Taverns
		FieldStoneCityTavern,
		SandstoneCityTavern,
		MarbleCityTavern,
		NecroCityTavern,
		AsianCityTavern,
		WoodCityTavern,
		StoneCityTavern,
		PlasterCityTavern,

		//Moongates
		SandstoneCityMoongate,
		MarbleCityMoongate,
		NecroCityMoongate,
		AsianCityMoongate,
		WoodCityMoongate,
		StoneCityMoongate,

		//Stables
		FieldStoneCityStable,
		SandstoneCityStable,
		MarbleCityStable,
		NecroCityStable,
		AsianCityStable,
		WoodCityStable,
		StoneCityStable,
		PlasterCityStable,

		//Healers
		FieldStoneCityHealer,
		SandstoneCityHealer,
		MarbleCityHealer,
		NecroCityHealer,
		AsianCityHealer,
		WoodCityHealer,
		StoneCityHealer,
		PlasterCityHealer,

		// Extra Buildings
		SmallCityGarden,
		MedCityGarden,
		LargeCityGarden,
		SmallCityPark,
		MedCityPark,
		LargeCityPark,
			
		// Marketplaces
		FieldstoneMarket,
		SandstoneMarket,
		MarbleMarket,
		NecroMarket,
		AsianMarket,
		WoodMarket,
		StoneMarket,
		PlasterMarket
			
	}

	public enum CivicSignType
	{
		None,
		Bank,
		Healer,
		Stable,
		Tavern,
		Moongate,
		Garden,
		Park,
		Market
	}
	
	public class CityTypes
	{
		public static Type[] Types = new Type[]
		{
			typeof(FieldstoneCityHallAddon),
			typeof(SandstoneCityHallAddon),
			typeof(MarbleCityHallAddon),
			typeof(NecroCityHallAddon),
			typeof(AsianCityHallAddon),
			typeof(WoodCityHallAddon),
			typeof(StoneCityHallAddon),
			typeof(PlasterCityHallAddon),
			typeof(FieldstoneCityBankAddon),
			typeof(SandstoneCityBankAddon),
			typeof(MarbleCityBankAddon),
			typeof(NecroCityBankAddon),
			typeof(AsianCityBankAddon),
			typeof(WoodCityBankAddon),
			typeof(StoneCityBankAddon),
			typeof(PlasterCityBankAddon),
			typeof(FieldstoneCityTavernAddon),
			typeof(SandstoneCityTavernAddon),
			typeof(MarbleCityTavernAddon),
			typeof(NecroCityTavernAddon),
			typeof(AsianCityTavernAddon),
			typeof(WoodCityTavernAddon),
			typeof(StoneCityTavernAddon),
			typeof(PlasterCityTavernAddon),
			typeof(SandstoneCityMoongateAddon),
			typeof(MarbleCityMoongateAddon),
			typeof(NecroCityMoongateAddon),
			typeof(AsianCityMoongateAddon),
			typeof(WoodCityMoongateAddon),
			typeof(StoneCityMoongateAddon),
			typeof(FieldstoneCityStableAddon),
			typeof(SandstoneCityStableAddon),
			typeof(MarbleCityStableAddon),
			typeof(NecroCityStableAddon),
			typeof(AsianCityStableAddon),
			typeof(WoodCityStableAddon),
			typeof(StoneCityStableAddon),
			typeof(PlasterCityStableAddon),
			typeof(FieldstoneCityHealerAddon),
			typeof(SandstoneCityHealerAddon),
			typeof(MarbleCityHealerAddon),
			typeof(NecroCityHealerAddon),
			typeof(AsianCityHealerAddon),
			typeof(WoodCityHealerAddon),
			typeof(StoneCityHealerAddon),
			typeof(PlasterCityHealerAddon),
			typeof(SmallCityGardenAddon),
			typeof(MedCityGardenAddon),
			typeof(LargeCityGardenAddon),
			typeof(SmallCityParkAddon),
			typeof(MedCityParkAddon),
			typeof(LargeCityParkAddon),
			typeof(FieldstoneCityMarketAddon),
			typeof(SandstoneCityMarketAddon),
			typeof(MarbleCityMarketAddon),
			typeof(NecroCityMarketAddon),
			typeof(AsianCityMarketAddon),
			typeof(WoodCityMarketAddon),
			typeof(StoneCityMarketAddon),
			typeof(PlasterCityMarketAddon)
		};
		
		public static bool IsCityType( Type type )
		{
			for (int i = 0; i < Types.Length; i++ )
			{
				if ( type == Types[i] )
					return true;
				
			}
			return false;
		}
	}
}
