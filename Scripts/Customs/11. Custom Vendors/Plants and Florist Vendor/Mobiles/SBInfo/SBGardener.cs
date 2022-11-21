using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBQualityGardener : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBQualityGardener()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{

					Add( new GenericBuyInfo( typeof( BambooCluster ), 1500, 100, 9324, 0 ) );
					Add( new GenericBuyInfo( typeof( BambooEast ), 1500, 100, 9327, 0 ) );
					Add( new GenericBuyInfo( typeof( BambooNorth ), 1500, 100, 9325, 0 ) );
					Add( new GenericBuyInfo( typeof( BambooSouth ), 1500, 100, 9326, 0 ) );
					Add( new GenericBuyInfo( typeof( BambooWest ), 1500, 100, 9328, 0 ) );
					Add( new GenericBuyInfo( typeof( BladePlant ), 1500, 100, 3219, 0 ) );
					Add( new GenericBuyInfo( typeof( BramblesLarge ), 1500, 100, 3391, 0 ) );
					Add( new GenericBuyInfo( typeof( BramblesSmall ), 1500, 100, 3392, 0 ) );
					Add( new GenericBuyInfo( typeof( Bulrushes ), 1500, 100, 3220, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus1 ), 1500, 100, 3365, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus2 ), 1500, 100, 3366, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus3 ), 1500, 100, 3367, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus4 ), 1500, 100, 3368, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus5 ), 1500, 100, 3370, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus6 ), 1500, 100, 3372, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus7 ), 1500, 100, 3374, 0 ) );
					Add( new GenericBuyInfo( typeof( CatTails1 ), 1500, 100, 3255, 0 ) );
					Add( new GenericBuyInfo( typeof( CatTails2 ), 1500, 100, 3256, 0 ) );
					Add( new GenericBuyInfo( typeof( ElephantEar ), 1500, 100, 3223, 0 ) );
					Add( new GenericBuyInfo( typeof( FanPlant ), 1500, 100, 3224, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern1 ), 1500, 100, 3231, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern2 ), 1500, 100, 3232, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern3 ), 1500, 100, 3234, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern4 ), 1500, 100, 3235, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern5 ), 1500, 100, 3236, 0 ) );
					Add( new GenericBuyInfo( typeof( JuniperBush ), 1500, 100, 3272, 0 ) );
					Add( new GenericBuyInfo( typeof( LargeFern ), 1500, 100, 3233, 0 ) );
					Add( new GenericBuyInfo( typeof( Mushrooms1 ), 1500, 100, 3347, 0 ) );
					Add( new GenericBuyInfo( typeof( PampasGrass1 ), 1500, 100, 3237, 0 ) );
					Add( new GenericBuyInfo( typeof( PampasGrass2 ), 1500, 100, 3268, 0 ) );
					Add( new GenericBuyInfo( typeof( Rushes ), 1500, 100, 3239, 0 ) );
					Add( new GenericBuyInfo( typeof( SnakePlant ), 1500, 100, 3241, 0 ) );
					Add( new GenericBuyInfo( typeof( TangledBramblesE ), 1500, 100, 12322, 0 ) );
					Add( new GenericBuyInfo( typeof( TangledBramblesN ), 1500, 100, 12320, 0 ) );
					Add( new GenericBuyInfo( typeof( TangledBramblesS ), 1500, 100, 12321, 0 ) );
					Add( new GenericBuyInfo( typeof( TangledBramblesW ), 1500, 100, 12323, 0 ) );
					Add( new GenericBuyInfo( typeof( TrimmedHedge1 ), 1500, 100, 3215, 0 ) );
					Add( new GenericBuyInfo( typeof( CureSprinkler ), 50000, 20, 0x14E7, 39 ) );
					Add( new GenericBuyInfo( typeof( HealSprinkler ), 50000, 20, 0x14E7, 45 ) );
					Add( new GenericBuyInfo( typeof( PoisonSprinkler ), 50000, 20, 0x14E7, 65 ) );
					Add( new GenericBuyInfo( typeof( StrengthSprinkler ), 50000, 20, 0x14E7, 996 ) );
					Add( new GenericBuyInfo( typeof( WaterSprinkler ), 10000, 20, 0x14E7, 195 ) );
					Add( new GenericBuyInfo( typeof( SprinklerContainer ), 2500, 20, 0x142B, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{

					Add( typeof( FertileDirt ), 2 );

			}
		}
	}
}
