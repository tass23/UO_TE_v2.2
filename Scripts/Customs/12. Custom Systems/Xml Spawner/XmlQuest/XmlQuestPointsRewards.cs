using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections;

/*
** XmlQuestPointsRewards
** ArteGordon
** updated 9/18/05
**
** this class lets you specify rewards that can be purchased for XmlQuestPoints quest Credits.
** The items will be displayed in the QuestPointsRewardGump that is opened by the QuestPointsRewardStone
*/

namespace Server.Engines.XmlSpawner2
{
    public class XmlQuestPointsRewards
    {
        public int Cost;       // cost of the reward in credits
        public Type  RewardType;   // this will be used to create an instance of the reward
        public string Name;         // used to describe the reward in the gump
        public int ItemID;     // used for display purposes
        public object [] RewardArgs; // arguments passed to the reward constructor
        public int MinPoints;   // the minimum points requirement for the reward

        private static ArrayList    PointsRewardList = new ArrayList();
        
        public static ArrayList RewardsList { get { return PointsRewardList; } }
        
        public XmlQuestPointsRewards(int minpoints, Type reward, string name, int cost, int id, object[] args)
        {
            RewardType = reward;
            Cost = cost;
            ItemID = id;
            Name = name;
            RewardArgs = args;
            MinPoints = minpoints;
        }
        
        public static void Initialize()
        {
            // these are items as rewards. Note that the args list must match a constructor for the reward type specified.
            //PointsRewardList.Add( new XmlQuestPointsRewards( 1000, typeof(PowerScroll), "105 Swordsmanship powerscroll", 1000, 0x14F0, new object[] { SkillName.Swords, 105 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Swordsmanship powerscroll", 2000, 0x14F0, new object[] { SkillName.Swords, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Swordsmanship powerscroll", 4000, 0x14F0, new object[] { SkillName.Swords, 115 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Swordsmanship powerscroll", 6000, 0x14F0, new object[] { SkillName.Swords, 120 }));
            //PointsRewardList.Add( new XmlQuestPointsRewards( 1000, typeof(PowerScroll), "105 Fencing powerscroll", 1000, 0x14F0, new object[] { SkillName.Fencing, 105 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Fencing powerscroll", 2000, 0x14F0, new object[] { SkillName.Fencing, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Fencing powerscroll", 4000, 0x14F0, new object[] { SkillName.Fencing, 115 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Fencing powerscroll", 6000, 0x14F0, new object[] { SkillName.Fencing, 120 }));
            //PointsRewardList.Add( new XmlQuestPointsRewards( 1000, typeof(PowerScroll), "105 Macing powerscroll", 1000, 0x14F0, new object[] { SkillName.Macing, 105 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Macing powerscroll", 2000, 0x14F0, new object[] { SkillName.Macing, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Macing powerscroll", 4000, 0x14F0, new object[] { SkillName.Macing, 115 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Macing powerscroll", 6000, 0x14F0, new object[] { SkillName.Macing, 120 }));
            //PointsRewardList.Add( new XmlQuestPointsRewards( 1000, typeof(PowerScroll), "105 Animal Taming powerscroll", 1000, 0x14F0, new object[] { SkillName.AnimalTaming, 105 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Animal Taming powerscroll", 2000, 0x14F0, new object[] { SkillName.AnimalTaming, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Animal Taming powerscroll", 4000, 0x14F0, new object[] { SkillName.AnimalTaming, 115 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Animal Taming powerscroll", 6000, 0x14F0, new object[] { SkillName.AnimalTaming, 120 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Blacksmithing powerscroll", 2000, 0x14F0, new object[] { SkillName.Blacksmith, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Blacksmithing powerscroll", 4000, 0x14F0, new object[] { SkillName.Blacksmith, 115 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Blacksmithing powerscroll", 6000, 0x14F0, new object[] { SkillName.Blacksmith, 120 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Carpentry powerscroll", 2000, 0x14F0, new object[] { SkillName.Carpentry, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Carpentry powerscroll", 4000, 0x14F0, new object[] { SkillName.Carpentry, 115 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Carpentry powerscroll", 6000, 0x14F0, new object[] { SkillName.Carpentry, 120 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Tinkering powerscroll", 2000, 0x14F0, new object[] { SkillName.Tinkering, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Tinkering powerscroll", 4000, 0x14F0, new object[] { SkillName.Tinkering, 115 }));
			PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Tinkering powerscroll", 6000, 0x14F0, new object[] { SkillName.Tinkering, 120 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Tailoring powerscroll", 2000, 0x14F0, new object[] { SkillName.Tailoring, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Tailoring powerscroll", 4000, 0x14F0, new object[] { SkillName.Tailoring, 115 }));
			PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Tailoring powerscroll", 6000, 0x14F0, new object[] { SkillName.Tailoring, 120 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 2000, typeof(PowerScroll), "110 Inscription powerscroll", 2000, 0x14F0, new object[] { SkillName.Inscribe, 110 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 4000, typeof(PowerScroll), "115 Inscription powerscroll", 4000, 0x14F0, new object[] { SkillName.Inscribe, 115 }));
			PointsRewardList.Add( new XmlQuestPointsRewards( 6000, typeof(PowerScroll), "120 Inscription powerscroll", 6000, 0x14F0, new object[] { SkillName.Inscribe, 120 }));
            //PointsRewardList.Add( new XmlQuestPointsRewards( 10, typeof(RewardScrollDeed), "Reward Scroll", 10, 0x2D51, new object[] { RewardScrollDeed, 1 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 500, typeof(AncientSmithyHammer), "+20 Ancient Smithy Hammer, 50 uses", 500, 0x13E4, new object[] { 20, 50 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 200, typeof(ColoredAnvil), "Colored Anvil", 400, 0xFAF, null ));
            PointsRewardList.Add( new XmlQuestPointsRewards( 100, typeof(PowderOfTemperament), "Powder Of Temperament, 10 uses", 300, 4102, new object[] { 10 }));
            PointsRewardList.Add( new XmlQuestPointsRewards( 100, typeof(LeatherGlovesOfMining), "+20 Leather Gloves Of Mining", 200, 0x13c6, new object[] { 20 }));

            // this is an example of adding a mobile as a reward
            PointsRewardList.Add( new XmlQuestPointsRewards( 2500, typeof(EtherealHiryu),"Ethereal Hiryu", 2500, 10090, null));

            // this is an example of adding an attachment as a reward
            //PointsRewardList.Add( new XmlQuestPointsRewards( 0, typeof(XmlEnemyMastery), "+200% Balron Mastery for 1 day", 2, 0, new object[] { "Balron", 50, 200, 1440.0 }));
            //PointsRewardList.Add( new XmlQuestPointsRewards( 0, typeof(XmlStr), "+20 Strength for 1 day", 10, 0, new object[] { 20, 86400.0 }));
        }

    }
}
