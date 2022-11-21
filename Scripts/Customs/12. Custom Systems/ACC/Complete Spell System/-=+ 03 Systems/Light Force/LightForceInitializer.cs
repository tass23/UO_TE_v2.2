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

namespace Server.ACC.CSS.Systems.LightForce
{
    public class LightForceIntializer : BaseInitializer
    {
        public static void Configure()
        {
            Register(typeof(ProjectionSpell), "Projection", "Allows the Jedi to focus their Force energy and teleport across distances.", "Spring Water; Petrified Wood", "Mana: 9; Skill: 15", 24000, 9270, School.LightForce);
			Register(typeof(CleanseSpell), "Cleanse", "The target is cured of all poisons and has all negative stat curses removed.", "Spring Water; Destroying Angel; Spider's Silk", "Mana: 40;  Skill: 70", 23014, 9270, School.LightForce );
			Register(typeof(JediGroveSpell), "Jedi Grove", "Creates a grove of trees to grow around the Jedi, restoring hit points and stamina.", "Mandrake Root; Petrified Wood; Spring Water", "Mana: 50; Skill: 85", 2263, 9270, School.LightForce );
			Register(typeof(ForceLightningSpell), "Force Lightning", "Strikes the Target with a bolt of lightning, which deals Energy damage.", "Mandrake Root; Sulfurous Ash", "Mana: 14; Skill: 40", 2269, 9270, School.LightForce);
			Register(typeof(BattleMeditationSpell), "Battle Meditation", "The next target hit becomes the Jedi's Mortal Enemy.  All damage dealt to that creature type is increased, but the Jedi takes extra damage from all other creature types.  Mortal Enemy creature types will highlight Orange to the Jedi.", "Spring Water; Night Shade; Spider's Silk", "Mana: 6; Skill: 10", 2295, 9270, School.LightForce );
			Register(typeof(ForceWeaponSpell), "Force Weapon", "Temporarily enchants the Jedi's lightsaber. The type of damage the lightsaber inflicts when hitting a target will be converted to the target's worst Resistance type.", "Garlic; Black Pearl", "Mana: 6; Skill: 10", 20738, 9270, School.LightForce );
			Register(typeof(BellowSpell), "Bellow", "Temporarily increases the Jedi's swing speed, chance to hit, and damage dealt, while lowering the Jedi's defenses. Upon casting, the Jedi's Stamina is also refreshed.", "Spring Water", "Mana: 6; Skill: 10", 20741, 9270, School.LightForce );
			Register(typeof(ConcealmentSpell), "Concealment", "The Jedi blends seamlessly with the background, becoming invisible to their foes.", "Blood Moss; Nightshade", "Mana: 11; Skill: 25", 2249, 9270, School.LightForce );
			Register(typeof(StealthSpell), "Stealth", "Allows the Jedi to slip into the shadows.", "Spider's Silk; Garlic; Black Pearl", "Mana: 20; Skill: 55",  2283, 9270, School.LightForce );
			Register(typeof(ShieldSpell), "Shield", "Creates a magical shield around the Jedi that absorbs melee damage.", "Blood Moss; Destroying Angel", "Mana: 40; Skill: 70", 2294, 9270, School.LightForce );
			Register(typeof(KinesisSpell), "Kinesis", "Allows the Jedi to manipulate an item at a distance.", "Blood Moss; Mandrake Root", "Mana: 9; Skill: 15", 20496, 9270, School.LightForce);
			Register(typeof(FoldSpaceSpell), "Fold Space", "Summons a Force Spirit Bird to teleport the Jedi to any marked location.", "Sulfurous Ash; Petrified Wood", "Mana: 11; Skill: 30", 23001, 9270, School.LightForce );
			Register(typeof(RemembranceSpell), "Remembrance", "A Jedi uses the Force to make a physical mark to allow them to return to that place in the future.", "Spring Water; Petrified Wood; Destroying Angel", "Mana: 11; Skill: 30", 23012,  9270, School.LightForce );
			Register(typeof(GripSpell), "Grip", "The Jedi uses the Force around them to hold their opponents in place.", "Spring Water; Blood Moss; Spider's Silk", "Mana: 20; Skill: 55", 2293, 5120, School.LightForce );
			Register(typeof(LightPoisonField), "Force Essence", "Projects any toxins in the Jedi's body as a wall towards enemies.", "Blood Moss; Ginseng; Nightshade", "Mana: 11; Skill: 30", 2278, 9270, School.LightForce);
			Register(typeof(LightMatterSpell), "Force Matter", "Allows midi-chlorians to combine into a powerful, random guardian form to fight for you.", "Blood Moss; Spider's Silk; Destroying Angel", "Mana: 50; Skill: 85", 20736, 9270, School.LightForce );
			Register(typeof(ForceTidesSpell), "Force Tides", "Allows the Jedi to send physical tremors through the Force.", "Blood Moss; Ginseng; Mandrake Root; Sulfurous Ash", "Mana: 50; Skill: 85", 2300, 9270, School.LightForce );
            Register(typeof(MindTrickSpell), "Mind Trick", "Can be used either to control a humanoid enemy or to free one that has been controlled.", "Spring Water; Petrified Wood, Spider's Silk, Blood Moss", "Mana: 14; Skill: 45", 2242, 9270, School.LightForce);
			Register(typeof(LightVortexSpell), "Force Vortex", "Draws upon the Force to produce a powerful, yet uncontrollable ally.", "Spring Water; Black Pearl; Garlic", "Mana: 50; Skill: 85", 2285, 9270, School.LightForce );
			Register(typeof(LightGuardianSpell), "Force Guardian", "Summons forth a controllable Ancient Jedi Master to guard your back.", "Spring Water; Blood Moss; Spider's Silk; Black Pearl; Garlic", "Mana: 50; Skill: 85", 2261, 9270, School.LightForce );
			Register(typeof(LightCurseSpell), "Force Subdue", "Allows the Jedi to weaken a much stronger opponent for a limited time.", "Spring Water; Spider's Silk", "Mana: 14; Skill: 40", 2250, 9270, School.LightForce );
			Register(typeof(LightResurrectionSpell), "Force Revival", "Allows the Jedi to draw upon the midi-chlorians to revive a fallen comrade.", "Destroying Angel; Garlic; Sulfurous Ash", "Mana: 50; Skill: 80", 2250, 9270, School.LightForce );
        }
    }
}