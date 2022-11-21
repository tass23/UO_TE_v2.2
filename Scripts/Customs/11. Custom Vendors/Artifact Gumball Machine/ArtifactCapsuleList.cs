/*Monolith-KHzspeed 2011
ArtifactGumballRewards*/

using System;
using System.Reflection;
using System.IO;
using Server;
using Server.Items;

namespace Server
{
	public class ArtifactList
	{
		static int index;
		static Type type;

		public static Type[] ArtifactTypes = new Type[]
		{
			typeof( PolarBearMask ),
			typeof( PixieSwatter ),
			typeof( NightsKiss ),
			typeof( LunaLance ),
			typeof( GoldBricks ),
			typeof( HeartOfTheLion ),
			typeof( RewardScroll ),
			typeof( SpiritOfTheTotem ),
			typeof( HuntersHeaddress ),
			typeof( DivineCountenance ),
			typeof( Aegis ),
			typeof( ArcaneShield ),
			typeof( HatOfTheMagi ),
			typeof( AxeOfTheHeavens ),
			typeof( BoneCrusher ),
			typeof( LegacyOfTheDreadLord ),
			typeof( TheBeserkersMaul ),
			typeof( TitansHammer ),
			typeof( BladeOfInsanity ),
			typeof( BreathOfTheDead ),
			typeof( SerpentsFang ),
			typeof( TheDragonSlayer ),
			typeof( ZyronicClaw ),
			typeof( BladeOfTheRighteous ),
			typeof( Frostbringer ),
			typeof( StaffOfTheMagi ),
			typeof( TheTaskmaster ),
			typeof( ArmorOfFortune ),
			typeof( HolyKnightsBreastplate ),
			typeof( LeggingsOfBane ),
			typeof( ShadowDancerLeggings ),
			typeof( GauntletsOfNobility ),
			typeof( InquisitorsResolution ),
			typeof( MidnightBracers ),
			typeof( TunicOfFire ),
			typeof( HelmOfInsight ),
			typeof( JackalsCollar ),
			typeof( OrnateCrownOfTheHarrower ),
			typeof( VoiceOfTheFallenKing ),
			typeof( BraceletOfHealth ),
			typeof( RingOfTheVile ),
			typeof( OrnamentOfTheMagician ),
			typeof( RingOfTheElements ),
			typeof( PhillipsWoodenSteed ),
			typeof( AlchemistsBauble ),
			typeof( ArcticDeathDealer ),
			typeof( BlazeOfDeath ),
			typeof( BowOfTheJukaKing ),
			typeof( BurglarsBandana ),
			typeof( CavortingClub ),
			typeof( EnchantedTitanLegBone ),
			typeof( GwennosHarp ),
			typeof( IolosLute ),
			typeof( NoxRangersHeavyCrossbow ),
			typeof( OrcishVisage ),
			typeof( ShieldOfInvulnerability ),
			typeof( StaffOfPower ),
			typeof( VioletCourage ),
			typeof( WrathOfTheDryad ),
			typeof( GlovesOfThePugilist ),
			typeof( AdmiralsHeartyRum ),
			typeof( CandelabraOfSouls ),
			typeof( GhostShipAnchor ),
			typeof( ShipModelOfTheHMSCape ),
			typeof( CaptainQuacklebushsCutlass ),
			typeof( DreadPirateHat ),
			typeof( SeahorseStatuette ),
		};
		public static Type[] Artifacts{ get{ return ArtifactTypes; } }

		public static Item RandomArtifact()
		{
			index = Utility.Random( ArtifactTypes.Length );
			type = ArtifactTypes[index];
			return Activator.CreateInstance( type )as Item;
		}
	}
}