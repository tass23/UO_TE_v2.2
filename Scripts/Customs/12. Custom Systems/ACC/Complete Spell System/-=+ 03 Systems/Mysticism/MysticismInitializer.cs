using System;
using Server;

namespace Server.ACC.CSS.Systems.Mysticism
{
    public class MysticismInitializer : BaseInitializer
    {
		public static void Configure()
		{
            Register( typeof( MysticismEnchantSpell ), "Enchant", "Enchants a item magically.", "BlackPearl; MandrakeRoot", "", 24003,  5120, School.Mysticism );
            Register( typeof( HealingStoneSpell ), "Healing Stone", "Conjures a healing stone that will instantly heal the caster when used.", "Bone; Garlic; Ginseng; SpidersSilk", "Mana: 4; Skill: 0", 24001, 5120, School.Mysticism );
            Register( typeof( NetherBoltSpell ), "Nether Bolt", "Fires a bolt of nether energy at the target.", "BlackPearl; SulfurousAsh", "Mana: 4; Skill: 0", 24000, 5120, School.Mysticism );
            Register( typeof( PurgeMagicSpell ), "Purge Magic", "Attempts to remove a beneficial ward from the target.", "Garlic; MandrakeRoot; SulfurousAsh; FertileDirt", "Mana: 6;  Skill: 8", 24002, 5120, School.Mysticism );
            Register( typeof( EagleStrikeSpell ), "Eagle Strike", "Conjures a magical eagle that will attack the target.", "Bloodmoss; Bone; MandrakeRoot; SpidersSilk", "Mana: 9;  Skill: 20",    24005,  5120, School.Mysticism );
            Register( typeof( SleepSpell ), "Sleep", "Puts target to sleep.", "Bloodmoss; Garlic; SulfurousAsh; DragonBlood", "Mana: 9;  Skill: 20",    24004,  5120, School.Mysticism );
            Register( typeof( StoneFormSpell ), "Stone Form", "Turns target into stone.", "Bloodmoss; FertileDirt; Garlic",  "Mana: 11; Skill: 33", 24007,  5120, School.Mysticism );
            Register( typeof( AnimatedWeaponSpell ), "Animated Weapon", "Summons magical weapon.", "BlackPearl; MandrakeRoot; Nightshade; Bone", "Mana: 11; Skill: 33", 24006,  5120, School.Mysticism );
            Register( typeof( SpellTriggerSpell ), "Spell Trigger", "Allows caster to put a spell on a stone that will be ready to be used instantly later.", "Garlic; MandrakeRoot; SpidersSilk; DragonBlood", "Mana: 14; Skill: 45", 24008,  5120, School.Mysticism );
            Register( typeof( MassSleepSpell ), "Mass Sleep", "Puts an area to sleep.", "MandrakeRoot; Nightshade; SulfurousAsh; Bloodmoss", "Mana: 14; Skill: 45", 24009,  5120, School.Mysticism );
            Register( typeof( BombardSpell ), "Bombard", "Hurls a magical boulder at the target.", "Bloodmoss; Garlic; SulfurousAsh; DragonBlood", "Mana: 20; Skill: 58", 24011,  5120, School.Mysticism );
            Register( typeof( CleansingWindsSpell ), "Cleansing Winds", "Neutralizes poisons.", "Garlic; Ginseng; MandrakeRoot; DragonBlood", "Mana: 20; Skill: 58", 24010,  5120, School.Mysticism );
            Register( typeof( SpellPlagueSpell ), "Spell Plague", "Temporarily creates a dark pact between the caster and the target.", "DaemonBone; DragonBlood; Nightshade; SulfurousAsh", "Mana: 40; Skill: 70", 24012,  5120, School.Mysticism );
            Register( typeof( HailStormSpell ), "Hail Storm", "Summons hail to kill foes.", "BlackPearl; Bloodmoss; MandrakeRoot; DragonBlood", "Mana: 40; Skill: 70", 24013,  5120, School.Mysticism );        
            Register( typeof( NetherCycloneSpell ), "Nether Cyclone", "Hurls a magical boulder at the target.", "MandrakeRoot; Nightshade; SulfurousAsh; Bloodmoss",          "Mana: 50; Skill: 83", 24014,  5120, School.Mysticism );
            Register( typeof( RisingColossusSpell ), "Rising Colossus", "Summons a magical weapon to fight your foes.", "DaemonBone; DragonBlood; FertileDirt; Nightshade", "Mana: 50; Skill: 83", 24015,  5120, School.Mysticism );            
		}
	}
}
