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

namespace Server.ACC.CSS.Systems.Vampire
{
    public class VampireIntializer : BaseInitializer
    {
        public static void Configure()
        {
            Register(typeof(VStrengthSpell), "Super Strength", "You harness the power within your blood and have super human strength.", "Mandrake Root; Black Pearl", "Mana: 15; Skill: 25", 2257, 9270, School.Vampire);
            Register(typeof(VKinesisSpell), "Telekinesis", "You harness the power within your blood to manipulate an ojbect.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 18", 2260, 9270, School.Vampire);
            Register(typeof(VPsychicSpell), "Psychic Link", "You harness the power within your blood to mentally attack your target.", "Mandrake Root; Black Pearl", "Mana: 14; Skill: 52", 2242, 9270, School.Vampire);
            Register(typeof(VFlameSpell), "Blood Drain", "You summon an ancient Iron Maiden to drain your target's blood, severely damaging them.", "Mandrake Root; Black Pearl", "Mana: 40; Skill: 85", 2273, 9270, School.Vampire);
            Register(typeof(VParalyzeFieldSpell), "Pertrify Field", "You harness the power within your blood and use your gaze to create a wall which will paralyze enemies in terror.", "Mandrake Root; Black Pearl", "Mana: 11; Skill: 49", 2286, 9270, School.Vampire);
            Register(typeof(VPoisonSpell), "Miasma", "You harness the power within your blood and hurl a cloud of noxious gas at your target.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 67", 2278, 9270, School.Vampire);
            Register(typeof(VConcealmentSpell), "Shadow Vale", "You harness the power from your Fog form and blend into your surroundings.", "Mandrake Root; Black Pearl", "Mana: 20; Skill: 90", 2302, 9270, School.Vampire);
            Register(typeof(VCorpseSkinSpell), "Harden Flesh", "You harness the power within your blood and fortify your flesh, increasing your fire, cold and physical resists.", "Mandrake Root; Black Pearl", "Mana: 6; Skill: 43", 2295, 9270, School.Vampire);
            Register(typeof(VBloodOathSpell), "Cursed Blood", "You harness the power within your blood and curse your target's blood causing you both to take equal damage temporarily.", "Mandrake Root; Black Pearl", "Mana: 20; Skill: 84", 2290, 9270, School.Vampire);
            Register(typeof(VCurseWeaponSpell), "Leech Weapon", "You harness the power within your blood and invigorate your weapon with life leeching properties temporarily.", "Mandrake Root; Black Pearl", "Mana: 14; Skill: 73", 2251, 9270, School.Vampire);
            Register(typeof(VAnimateDeadSpell), "Raise Undead", "You harness the power within your blood and raise an undead familiar.", "Mandrake Root; Black Pearl", "Mana: 50; Skill: 98", 2266, 9270, School.Vampire);
            Register(typeof(VWitherSpell), "Vampiric Presence", "You harness the power within your blood and enemies around you suffer energy damage.", "Mandrake Root; Black Pearl", "Mana: 20; Skill: 99", 2276, 9270, School.Vampire);
        }
    }
}