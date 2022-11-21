using System;
using Server;

namespace Server
{
	/* !!!READ HERE!!! before editing this file! A few words of advice. First you can disable breeding and keep leveling 
	system however you cannot disable leveling and keep breeding. Breeding system depends on the leveling system. Please 
	read all notes i have left thur out this file for further help.*/

	public class FSATS
	{
		/***********************************************
		************************************************
		***  Custom Creature Settings. Here you can  ***
		***  adjust what pets do not require tame-   ***
		***  ing skill, Pack Animals, and Creatres   ***
		***  that you do not wish to level/breed     ***
		************************************************
		***********************************************/	

		//Add all pets that do not require any tameing to control/ride
		public static string[] NoTamingNeeded = new string[]
		{
			"Horse",
			"RidableLlama",
			"ForestOstard",
			"DesertOstard",
			"SwampDragon",
			"Ridgeback",
			"PackHorse",
			"PackLlama",
			"Beetle"
		};

		//Add all pack animals for your server here.
		public static string[] PackAnimals = new string[]
		{
			"PackHorse",
			"PackLlama",
			"Beetle"
		};

		//Add all creatures you do not wish to level / breed
		public static string[] NoLevelCreatures = new string[]
		{
			"Golem",
			"CoMWarHorse",
			"MinaxWarHorse",
			"SLWarHorse",
			"TBWarHorse",
			"FactionWarHorse",
			"BBC",
			"BioCreature",
			"BioMount",
			"Vampire0",
			"Vampire1",
			"Vampire2",
			"Vampire3",
			"Vampire4",
			"Vampire5",
			"VampireSlayer",
			"RidableEnergyElemental",
			"Treepecker",
			"MiningBeetle",
			"RainbowUnicorn",
			"RainbowSwampDragon",
			"RainbowSteed",
			"RainbowBeetle",
			"RainbowCuSidhe",
			"RainbowDesertOstard",
			"RainbowForestOstard",
			"RainbowFrenziedOstard",
			"RainbowHellSteed",
			"RainbowHiryu",
			"RainbowKirin",
			"RainbowLlama",
			"RainbowPolarBear",
			"RainbowRidgeback",
			"RainbowSavageRidgeback",
			"RainbowScaledSwampDragon",
			"DeaditePet",
			"HousePet",
			"VanityPetDog",
			"VanityPetBird"
		};

		//Creatures you always want as Female
		public static string[] AlwaysFemale = new string[]
		{
			"Cow",
			"Hind"
		};

		//Creatures You always want as Male
		public static string[] AlwaysMale = new string[]
		{
			"Bull",
			"GreatHart"
		};

		/***********************************************
		************************************************
		***  Here you can turn on or off features    ***
		***  of the FSATS system. Such as Bio system ***
		***  Breeding, Leveling, and Taming BODS you ***
		***  can pick and choice system you want     ***
		************************************************
		***********************************************/

		// Turns on/off Shrink System
		public static readonly bool EnableShrinkSystem = true;

		// Turns on/off Taming Craft
		public static readonly bool EnableTamingCraft = true;

		// Turns on/off Bio Engineering
		public static readonly bool EnableBioEngineer = true;

		// Turns on/off Pet Breeding
		public static readonly bool EnablePetBreeding = true;

		// Turns on/off Pet Leveling (NOTE: If you disable this disable breeding as well.)
		public static readonly bool EnablePetLeveling = true;

		// Turns on/off Taming BODs
		public static readonly bool EnableTamingBODs = true;

		// Enable Bio Shrink
		public static readonly bool EnableBioShrink = false;

		/***********************************************
		************************************************
		***  Below you can set the caps for normal   ***
		***  creatures stats. This keeps them from   ***
		***  being uber with the pet leveling system ***
		***  they cannot pass these caps below.      ***
		************************************************
		***********************************************/

		//Normal Creatures STR Cap
		public static readonly int NormalSTR = 1950;

		//Normal Creatures DEX Cap
		public static readonly int NormalDEX = 350;

		//Normal Creatures INT Cap
		public static readonly int NormalINT = 1500;

		//Normal Creatures HITS Cap
		public static readonly int NormalHITS = 1850;

		//Normal Creatures STAM Cap
		public static readonly int NormalSTAM = 700;

		//Normal Creatures MANA Cap
		public static readonly int NormalMANA = 1500;

		//Normal Creatures Min Damage Cap
		public static readonly int NormalMinDam = 30;

		//Normal Creatures Max Damage Cap
		public static readonly int NormalMaxDam = 38;

		//Normal Creatures PhysResist Cap
		public static readonly int NormalPhys = 75;

		//Normal Creatures PhysFire Cap
		public static readonly int NormalFire = 75;

		//Normal Creatures PhysCold Cap
		public static readonly int NormalCold = 75;

		//Normal Creatures PhysEnergy Cap
		public static readonly int NormalEnergy = 75;

		//Normal Creatures PhysPoison Cap
		public static readonly int NormalPoison = 75;

		//Normal Creatures V Armor Cap
		public static readonly int NormalVArmor = 70;

		/***********************************************
		************************************************
		***  Below you can set the caps for bio      ***
		***  creatures stats. This keeps them from   ***
		***  being uber with the pet leveling system ***
		***  they cannot pass these caps below.      ***
		************************************************
		***********************************************/

		//Bio Creatures STR Cap
		public static readonly int BioSTR = 3000;

		//Bio Creatures DEX Cap
		public static readonly int BioDEX = 275;

		//Bio Creatures INT Cap
		public static readonly int BioINT = 2750;

		//Bio Creatures HITS Cap
		public static readonly int BioHITS = 2300;

		//Bio Creatures STAM Cap
		public static readonly int BioSTAM = 400;

		//Bio Creatures MANA Cap
		public static readonly int BioMANA = 2150;

		//Bio Creatures Min Damage Cap
		public static readonly int BioMinDam = 33;

		//Bio Creatures Max Damage Cap
		public static readonly int BioMaxDam = 38;

		//Bio Creatures PhysResist Cap
		public static readonly int BioPhys = 75;

		//Bio Creatures PhysFire Cap
		public static readonly int BioFire = 75;

		//Bio Creatures PhysCold Cap
		public static readonly int BioCold = 75;

		//Bio Creatures PhysEnergy Cap
		public static readonly int BioEnergy = 75;

		//Bio Creatures PhysPoison Cap
		public static readonly int BioPoison = 75;

		//Bio Creatures V Armor Cap
		public static readonly int BioVArmor = 70;
		
		/***********************************************
		************************************************
		***  Below you can set the caps for elemental***
		***  creatures stats. This keeps them from   ***
		***  being uber with the pet leveling system ***
		***  they cannot pass these caps below.      ***
		************************************************
		***********************************************/
		
		//Normal Creatures Fire Damage Cap
		public static readonly int NormalFDam = 10;

		//Normal Creatures Cold Damage Cap
		public static readonly int NormalCDam = 10;

		//Normal Creatures Poison Damage Cap
		public static readonly int NormalPDam = 10;

		//Normal Creatures Energy Damage Cap
		public static readonly int NormalEDam = 10;
	}
}