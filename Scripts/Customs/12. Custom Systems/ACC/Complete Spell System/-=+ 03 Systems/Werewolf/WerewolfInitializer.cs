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

namespace Server.ACC.CSS.Systems.Werewolf
{
    public class WerewolfIntializer : BaseInitializer
    {
        public static void Configure()
        {
            Register(typeof(WConcealmentSpell), "Lurking", "You harness the power from your Wolf form and blend into your surroundings.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2279, 9270, School.Werewolf);
            Register(typeof(WEvilOmenSpell), "Dreadful Howl", "Your howl penetrates your target's defenses causing the next harmful event against them to be magnified.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2250, 9270, School.Werewolf);
            Register(typeof(WPainSpikeSpell), "Piercing Howl", "Your howl causes intense physical pain within your target's ears.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2251, 9270, School.Werewolf);
            Register(typeof(WParalyzeSpell), "Paralyzing Howl", "Your howl causes your target to become paralyzed with fear.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2301, 2286, School.Werewolf);
            Register(typeof(WWeakenSpell), "Deafening Howl", "Your howl causes your target to lose courage, which lowers their strength.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2277, 9270, School.Werewolf);
            Register(typeof(WCorpseSkinSpell), "Toughen Skin", "You bark which raises your: Fire, Cold and Physical resists by 10, but lowers your Poison and Energy resists by 15.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2255, 9270, School.Werewolf);
            Register(typeof(WStrengthSpell), "Inciting Howl", "Your howl lifts your spirits and invigorates you making you feel stronger.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2248, 9270, School.Werewolf);
            Register(typeof(WDivineFury), "Berserker", "Your howl sends you into a crazed state causing extra damage to one type of target.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2299, 9270, School.Werewolf);
            Register(typeof(WRallyingHowlSpell), "Rallying Howl", "Your howl exhilarates your party members, which regenerates their health and stamina for a time.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2284, 9270, School.Werewolf);
            Register(typeof(WEarthquakeSpell), "Rampaging Howl", "Your howl makes the very ground beneath your feet tremble and shake causing damage to enemies nearby.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2303, 9270, School.Werewolf);
            Register(typeof(WFearSpell), "Horrific Howl", "Your howl causes certain enemies nearby to flee in terror.", "Mandrake Root; Black Pearl", "Mana: 9; Skill: 15", 2270, 9270, School.Werewolf);
        }
    }
}