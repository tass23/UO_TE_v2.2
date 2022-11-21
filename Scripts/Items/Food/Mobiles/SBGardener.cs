using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Items.Crops;

namespace Server.Mobiles
{
	public class SBGardener : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGardener(){}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				//Add( new GenericBuyInfo( typeof( MilkBucket ), 800, 10, 0x0FFA, 0 ) );
				//Add( new GenericBuyInfo( typeof( CheeseForm ), 800, 10, 0x0E78, 0 ) );

				Add( new GenericBuyInfo( "Plant Bowl", typeof( Engines.Plants.PlantBowl ), 2, 20, 0x15FD, 0 ) );
				Add( new GenericBuyInfo( "Fertile Dirt", typeof( FertileDirt ), 10, 20, 0xF81, 0 ) );
				Add( new GenericBuyInfo( "Random Plant Seed", typeof( Engines.Plants.Seed ), 2, 20, 0xDCF, 0 ) );
 				Add( new GenericBuyInfo( typeof( GreaterCurePotion ), 45, 20, 0xF07, 0 ) );
				Add( new GenericBuyInfo( typeof( GreaterPoisonPotion ), 45, 20, 0xF0A, 0 ) );
				Add( new GenericBuyInfo( typeof( GreaterStrengthPotion ), 45, 20, 0xF09, 0 ) );
				Add( new GenericBuyInfo( typeof( GreaterHealPotion ), 45, 20, 0xF0C, 0 ) );
				Add( new GenericBuyInfo( "Boline", typeof( Boline ), 500, 5, 0xEC5, 0 ) );
				Add( new GenericBuyInfo( typeof( VinyardGroundAddonDeed ), 150000, 5, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( typeof( PersonalGardenAddonDeed ), 175000, 5, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( typeof( Scarecrow ), 25000, 5, 0x1E34, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				//Add( typeof( MilkBucket ), 400 );
				//Add( typeof( CheeseForm ), 400 );

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