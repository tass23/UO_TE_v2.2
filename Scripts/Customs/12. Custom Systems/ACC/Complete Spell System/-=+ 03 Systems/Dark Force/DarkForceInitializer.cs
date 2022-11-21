using System;
using Server;
using Server.Spells;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Spells.Eighth;
using Server.Spells.Chivalry;
using Server.Spells.Spellweaving;

//First Circle: 	4	.1
//Second Circle: 	6	.1
//Third Circle: 	9	10.1
//Fourth Circle: 	11	24.1
//Fifth Circle: 	14	38.1
//Sixth Circle: 	20	52.1
//Seventh Circle: 	40	66.1
//Eight Circle: 	50	80.1

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DarkForceIntializer : BaseInitializer
    {
        public static void Configure()
        {
			Register(typeof(DarkKinesisSpell), "Kinesis", "Allows the Sith to manipulate an item at a distance.", "Nox Crystal; Daemon Blood", "Mana: 9; Skill: 15", 20496, 9550, School.DarkForce);
			Register(typeof(FearSpell), "Fear", "The Sith uses the power of the Dark Side to intimidate and attempt to frighten their enemy causing the target to possibly flee.", "Grave Dust; Pig Iron; Bat Wing", "Mana: 20; Skill: 55", 20481, 9550, School.DarkForce);
            Register(typeof(DarkProjectionSpell), "Projection", "Allows the Sith to focus their Dark Side energy and teleport across distances.", "Nox Crystal; Daemon Blood", "Mana: 9; Skill: 15", 24000, 9550, School.DarkForce);
			Register(typeof(DarkPoisonSpell), "Poison", "Target is afflicted with poison. The strength of the poison is determined by the Sith's Poison Skill their Focus and the distance from the target.", "Nox Crystal; Daemon Blood", "Mana: 20; Skill: 55", 2276, 9550, School.DarkForce );
			Register(typeof(DarkCleanseSpell), "Cleanse", "The target is cured of all poisons and has all negative stat curses removed.", "Nox Crystal; Daemon Blood", "Mana: 40; Skill: 70", 23014, 9270, School.DarkForce );
            Register(typeof(DarkMindTrickSpell), "Mind Trick", "Can be used either to control a humanoid enemy or to free one that has been controlled.", "Nox Crystal; Pig Iron; Grave Dust; Bat Wing", "Mana: 14; Skill: 40", 2242, 9550, School.DarkForce);
			Register(typeof(DarkStealthSpell), "Stealth", "Allows the Sith to slip into the shadows.", "Grave Dust; Daemon Blood; Bat Wing", "Mana: 20; Skill: 55", 23015, 9550, School.DarkForce );
			Register(typeof(DarkFoldSpaceSpell), "Fold Space", "Summons a Force Spirit Bird to teleport the Sith to any marked location.", "Daemon Blood; Grave Dust", "Mana: 11; Skill: 30", 23001, 9550, School.DarkForce );
			Register(typeof(DarkRemembranceSpell), "Remembrance", "A Sith uses the Dark Side to make a physical mark to allow them to return to that place in the future.", "Nox Crystal; Daemon Blood; Bat Wing", "Mana: 11; Skill: 30", 23012,  9550, School.DarkForce );
			Register(typeof(DarkStrangleSpell), "Choke", "The Sith temporarily chokes off the air supply of the target holding them in place and inflicts them with poison.", "Daemon Blood; Nox Crystal", "Mana: 40; Skill: 70", 2286, 9550, School.DarkForce );
			Register(typeof(DarkChainLightningSpell), "Chain Lightning", "Strikes the target with a bolt of lightning, which deals Energy damage and the bolt jumps to nearby targets.", "Grave Dust; Bat Wing; Daemon Blood; Nox Crystal", "Mana: 20; Skill: 55", 23004, 9550, School.DarkForce);
			Register(typeof(DarkPainSpikeSpell), "Wound", "Temporarily causes intense physical pain to the target as Direct damage.", "Grave Dust; Pig Iron; Daemon Blood", "Mana: 20; Skill: 55", 2282, 9550, School.DarkForce );
			Register(typeof(DarkCurseWeaponSpell), "Curse Weapon", "Temporarily imbues a Sith's lightsaber with a life draining effect.", "Bat Wing; Nox Crystal", "Mana: 9; Skill: 15", 23003, 9550, School.DarkForce );
			Register(typeof(DrainSpell), "Drain", "Sucks the mana and health from the Sith's target and infuses the Sith with them.", "Grave Dust; Daemon Blood; Bat Wing; Nox Crystal", "Mana: 20; Skill: 55", 2270, 9550, School.DarkForce );
			Register(typeof(DarkCurseSpell), "Debilitate", "Draw upon the Dark Side to lower your targets primary stats for a time.", "Nox Crystal; Daemon Blood", "Mana: 14; Skill: 40", 2250, 9550, School.DarkForce );
			Register(typeof(DarkResurrectionSpell), "Force Resurgence", "Draw upon the Dark Side to raise an ally from the veil of death.", "Bat Wing; Daemon Blood; Grave Dust", "Mana: 50; Skill: 85", 2298, 9550, School.DarkForce );			
			Register(typeof(DarkPoisonField), "Force Toxin", "Conjures an energy wall that also poisons enemies.", "Bat Wing; Nox Crystal; Grave Dust", "Mana: 11; Skill: 30", 2278, 9550, School.DarkForce);
			Register(typeof(DarkMatterSpell), "Force Matter", "Allows midi-chlorians to combine into a powerful, random elemental form to fight for you.", "Bat Wing; Daemon Blood; Nox Crystal", "Mana: 50; Skill: 85", 21256, 9550, School.DarkForce );
			Register(typeof(ForceDestructionSpell), "Destruction", "The Dark Side is called upon to injure one's enemies surrounding them.", "Nox Crystal; Grave Dust; Pig Iron", "Mana: 50; Skill: 85", 2251, 9550, School.DarkForce );
			Register(typeof(DarkVortexSpell), "Force Vortex", "Draws upon the Dark Side to produce a powerful, yet uncontrollable ally.", "Bat Wing; Daemon Blood; Pig Iron", "Mana: 50; Skill: 85", 2285, 9550, School.DarkForce );
			Register(typeof(DarkGuardianSpell), "Force Guardian", "Summons forth a controllable Ancient Sith Lord to demolish your enemies.", "Daemon Blood; Grave Dust; Pig Iron; Bat Wing; Nox Crystal", "Mana: 50; Skill: 85", 2261, 9550, School.DarkForce );
        }
    }
}
