using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBHomeDeco : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHomeDeco()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "Sarcophagus North", typeof( SleeperSarcophagusFacingNorthAddonDeed ), 1750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Sarcophagus West", typeof( SleeperSarcophagusFacingWestAddonDeed ), 1750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Coffin North", typeof( SleeperCoffinFacingNorthAddonDeed ), 1250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Coffin West", typeof( SleeperCoffinFacingWestAddonDeed ), 1250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Casket North", typeof( SleeperCasketFacingNorthAddonDeed ), 1550000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Casket West", typeof( SleeperCasketFacingWestAddonDeed ), 1550000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Futon North", typeof( SleeperFutonNSAddonDeed ), 550000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Futon West", typeof( SleeperFutonEWAddonDeed ), 550000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Tall Elven Bed South", typeof( SleeperTallElvenSouthDeed ), 1650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Tall Elven Bed East", typeof( SleeperTallElvenEastDeed ), 1650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Single Bed South", typeof( SleeperSmallSouthDeed ), 650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Single Bed East", typeof( SleeperSmallEWAddonDeed ), 650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Fur East", typeof( SleeperFurEWAddonDeed ), 1250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Fur North", typeof( SleeperFurNSAddonDeed ), 1250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Elven Single Bed North", typeof( SleeperElvenSouthDeed ), 1350000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Elven Single Bed East", typeof( SleeperElvenEWAddonDeed ), 1350000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Double Bed East with Sheets", typeof( SleeperEWSpecialAddonDeed ), 2250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Corner Fireplace with Chair", typeof( hex_CornerFireplaceAddonDeed ), 3550000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Corner Fireplace", typeof( hex_fireplaceMPAddonDeed ), 3250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Corner Fireplace with Throne East", typeof( hex_FirepEastAddonDeed ), 3750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Corner Fireplace with Throne South", typeof( hex_FirepSouthAddonDeed ), 3750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Stone Archway East", typeof( hex_GatewayEastAddonDeed ), 1250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Stone Archway South", typeof( hex_GatewaySouthAddonDeed ), 1250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Small Fireplace", typeof( hex_SmallFireplaceAddonDeed ), 650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Large Kitchen Stove East", typeof( StoveBigEastAddonDeed ), 1650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Large Kitchen Stove South", typeof( StoveBigSouthAddonDeed ), 1650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Small Kitchen Stove East", typeof( hex_StoveEastAddonDeed ), 1250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Small Kitchen Stove South", typeof( hex_StoveSouthAddonDeed ), 1250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Waterfall East", typeof( hex_WaterFallEastAddonDeed ), 2650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Waterfall South", typeof( hex_WaterFallSouthAddonDeed ), 2650000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Waterfall Pond South", typeof( hex_WaterfallPondAddonDeed ), 3250000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Graveyard", typeof( HeXgraveyAddonDeed ), 450000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Black Well", typeof( BlackWellAddonDeed ), 450000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Brown Well", typeof( BrownWellAddonDeed ), 450000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Marble Well", typeof( MarbleWellAddonDeed ), 450000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Red Well", typeof( RedWellAddonDeed ), 450000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Stone Well", typeof( StoneWellAddonDeed ), 450000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Wood Well", typeof( WoodWellAddonDeed ), 450000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Big Screen East", typeof( BigScreenTVEastAddonDeed ), 4750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Big Screen South", typeof( BigScreenTVSouthAddonDeed ), 4750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Piano", typeof( PianoAddonDeed ), 3750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Shower South", typeof( ShowerSouthAddonDeed ), 2850000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Shower East", typeof( ShowerEastAddonDeed ), 2850000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Bath Tub South", typeof( BathTubSouthAddonDeed ), 2550000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Bath Tub East", typeof( BathTubEastAddonDeed ), 2550000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Jacuzzi South", typeof( JacuzziSouthAddonDeed ), 2750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Jacuzzi East", typeof( JacuzziEastAddonDeed ), 2750000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Mystical Armoire", typeof( MysticalArmoire ), 150000, 5, 0x2857, 0 ) );
				//Add( new GenericBuyInfo( "Flying Carpet Magic Lamp", typeof( FlyingCarpetMagicLamp ), 10650000, 5, 0x14F2, 0 ) );
				//Add( new GenericBuyInfo( "Flying Carpet Dye Tub", typeof( FlyingCarpetDyeTub ), 1550000, 5, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "Sheets", typeof( Sheets ), 200, 20, 2706, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( SleeperSarcophagusFacingNorthAddonDeed ), 175000 );
				Add( typeof( SleeperSarcophagusFacingWestAddonDeed ), 175000 );
				Add( typeof( SleeperCoffinFacingNorthAddonDeed ), 125000 );
				Add( typeof( SleeperCoffinFacingWestAddonDeed ), 125000 );
				Add( typeof( SleeperCasketFacingNorthAddonDeed ), 155000 );
				Add( typeof( SleeperCasketFacingWestAddonDeed ), 155000 );
				Add( typeof( SleeperFutonNSAddonDeed ), 55000 );
				Add( typeof( SleeperFutonEWAddonDeed ), 55000 );
				Add( typeof( SleeperTallElvenSouthDeed ), 165000 );
				Add( typeof( SleeperTallElvenEastDeed ), 165000 );
				Add( typeof( SleeperSmallSouthDeed ), 65000 );
				Add( typeof( SleeperSmallEWAddonDeed ), 65000 );
				Add( typeof( SleeperFurEWAddonDeed ), 125000 );
				Add( typeof( SleeperFurNSAddonDeed ), 125000 );
				Add( typeof( SleeperElvenSouthDeed ), 135000 );
				Add( typeof( SleeperElvenEWAddonDeed ), 135000 );
				Add( typeof( SleeperEWSpecialAddonDeed ), 225000 );
				Add( typeof( hex_CornerFireplaceAddonDeed ), 355000 );
				Add( typeof( hex_fireplaceMPAddonDeed ), 325000 );
				Add( typeof( hex_FirepEastAddonDeed ), 375000 );
				Add( typeof( hex_FirepSouthAddonDeed ), 375000 );
				Add( typeof( hex_GatewayEastAddonDeed ), 125000 );
				Add( typeof( hex_GatewaySouthAddonDeed ), 125000 );
				Add( typeof( hex_SmallFireplaceAddonDeed ), 65000 );
				Add( typeof( StoveBigEastAddonDeed ), 165000 );
				Add( typeof( StoveBigEastAddonDeed ), 165000 );
				Add( typeof( hex_StoveEastAddonDeed ), 125000 );
				Add( typeof( hex_StoveSouthAddonDeed ), 125000 );
				Add( typeof( hex_WaterFallEastAddonDeed ), 265000 );
				Add( typeof( hex_WaterFallSouthAddonDeed ), 265000 );
				Add( typeof( hex_WaterfallPondAddonDeed ), 325000 );
				Add( typeof( HeXgraveyAddonDeed ), 465000 );
				Add( typeof( BlackWellAddonDeed ), 45000 );
				Add( typeof( BrownWellAddonDeed ), 45000 );
				Add( typeof( MarbleWellAddonDeed ), 45000 );
				Add( typeof( RedWellAddonDeed ), 45000 );
				Add( typeof( StoneWellAddonDeed ), 45000 );
				Add( typeof( WoodWellAddonDeed ), 45000 );
				Add( typeof( BigScreenTVEastAddonDeed ), 475000 );
				Add( typeof( BigScreenTVSouthAddonDeed ), 475000 );
				Add( typeof( PianoAddonDeed ), 375000 );
				//Add( typeof( FlyingCarpetMagicLamp ), 1065000 );
				//Add( typeof( FlyingCarpetDyeTub ), 155000 );
			}
		}
	}
}
