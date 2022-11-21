using System; 
using System.Collections.Generic; 
using Server.Items;


namespace Server.Mobiles 
{ 
	public class SBDoomArtifactBuyer : SBInfo
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBDoomArtifactBuyer()
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
	                                          Add( typeof( SpiritOfTheTotem ), 10000 );
	                                          Add( typeof( HuntersHeaddress ), 10000 );
	                                          Add( typeof( DivineCountenance ), 10000 );
                                              Add( typeof( Aegis ), 10000 );
                                              Add( typeof( ArcaneShield ), 10000 );
	                                          Add( typeof( HatOfTheMagi ), 10000 );
	                                          Add( typeof( AxeOfTheHeavens ), 10000 );
	                                          Add( typeof( BoneCrusher ), 10000 );
	                                          Add( typeof( LegacyOfTheDreadLord ), 10000 );
	                                          Add( typeof( TheBeserkersMaul ), 10000 );
	                                          Add( typeof( TitansHammer ), 10000 );
	                                          Add( typeof( BladeOfInsanity ), 10000 );
	                                          Add( typeof( BreathOfTheDead ), 10000 );
	                                          Add( typeof( SerpentsFang ), 10000 );
	                                          Add( typeof( TheDragonSlayer ), 10000 );
	                                          Add( typeof( ZyronicClaw ), 10000 );
	                                          Add( typeof( BladeOfTheRighteous ), 10000 );
	                                          Add( typeof( Frostbringer ), 10000 );
	                                          Add( typeof( StaffOfTheMagi ), 10000 );
	                                          Add( typeof( TheTaskmaster ), 10000 );
	                                          Add( typeof( ArmorOfFortune ), 10000 );
	                                          Add( typeof( HolyKnightsBreastplate ), 10000 );
	                                          Add( typeof( LeggingsOfBane ), 10000 );
	                                          Add( typeof( ShadowDancerLeggings ), 10000 );
	                                          Add( typeof( GauntletsOfNobility ), 10000 );
	                                          Add( typeof( InquisitorsResolution ), 10000 );
	                                          Add( typeof( MidnightBracers ), 10000 );
	                                          Add( typeof( TunicOfFire ), 10000 );
	                                          Add( typeof( HelmOfInsight ), 10000 );
	                                          Add( typeof( JackalsCollar ), 10000 );
	                                          Add( typeof( OrnateCrownOfTheHarrower ), 10000 );
	                                          Add( typeof( VoiceOfTheFallenKing ), 10000 );
	                                          Add( typeof( BraceletOfHealth ), 10000 );
	                                          Add( typeof( RingOfTheVile ), 10000 );
	                                          Add( typeof( OrnamentOfTheMagician ), 10000 );
	                                          Add( typeof( RingOfTheElements ), 10000 );

            		                 }
		} 
	} 
}