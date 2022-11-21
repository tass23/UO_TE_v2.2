using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBMinorArtifactBuyer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMinorArtifactBuyer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/*
			public InternalBuyInfo()
			{
                                                                      
			}
			*/
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			                 Add( typeof( GoldBricks ), 12000 );
                             Add( typeof( PhillipsWoodenSteed ), 12000 );
			                 Add( typeof( AlchemistsBauble ), 12000 );
                             Add( typeof( ArcticDeathDealer ), 12000 );
			                 Add( typeof( BlazeOfDeath ), 12000 );
                             Add( typeof( BowOfTheJukaKing ), 12000 );
			                 Add( typeof( BurglarsBandana ), 12000 );
                             Add( typeof( CavortingClub ), 12000 );
			                 Add( typeof( EnchantedTitanLegBone ), 12000 );
                             Add( typeof( GwennosHarp ), 12000 );
			                 Add( typeof( IolosLute ),  12000 );
                             Add( typeof( LunaLance ), 12000 );
			                 Add( typeof( NightsKiss ), 12000 ); 
                             Add( typeof( NoxRangersHeavyCrossbow ), 12000 );
			                 Add( typeof( OrcishVisage ), 12000 );
                             Add( typeof( PolarBearMask ), 12000 );
			                 Add( typeof( ShieldOfInvulnerability ), 12000 );
                             Add( typeof( StaffOfPower ), 12000 );
			                 Add( typeof( VioletCourage ), 12000 ); 
                             Add( typeof( HeartOfTheLion ), 12000 ); 
			                 Add( typeof( WrathOfTheDryad ), 12000 );
                             Add( typeof( PixieSwatter ), 12000 ); 
			                 Add( typeof( GlovesOfThePugilist ), 12000 ); 
                             Add( typeof( AdmiralsHeartyRum ), 3 );
                             Add( typeof( CandelabraOfSouls ), 12000 );
                             Add( typeof( GhostShipAnchor ), 12000 );
                             Add( typeof( ShipModelOfTheHMSCape ), 12000 );
                             Add( typeof( CaptainQuacklebushsCutlass ), 12000 );
                             Add( typeof( DreadPirateHat ), 12000 );
                             Add( typeof( SeahorseStatuette ), 12000 );
			}
		}
	}
}