#define STOREONITEM
using Server;
using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.Commands;
using Server.Misc;

//#if STOREONITEM
//#else
//using Scripts.Mythik.Mobiles;
//#endif

using Scripts.Mythik.Systems.Achievements.Gumps;

namespace Scripts.Mythik.Systems.Achievements
{
    //TODO
    //subcategories X
    //page limit?
    // Achievement prereq achieve before showing X
    //TODO Skill gain achieves needs event X
    //TODO ITEM crafted event sink X
    //TODO SKILL USE 
    //TODO HousePlaced event sink
    /*thought of eating a lemon (and other foods), consume pots,
     *  craft a home, 
     *  own home (more for larger homes), 
     *  loot x amount of gold, 
     *  find a uni, 
     *  kill each mob in the game,
     *   enter an event,
     *    tame all tamables,
     *     use a max powerscroll (or skill stone), 
     *     ride each type of mount
     */
    public class AchievementSystem
    {
        public class AchievementCategory
        {
            public int ID { get; set; }
            public int Parent { get; set; }
            public string Name;


            public AchievementCategory(int id, int parent, string v3)
            {
                ID = id;
                Parent = parent;
                Name = v3;
            }
        }
        public static List<BaseAchievement> Achievements = new List<BaseAchievement>();
        public static List<AchievementCategory> Categories = new List<AchievementCategory>();

        public static void Initialize()
        {
            Categories.Add(new AchievementCategory(1, 0, "Exploration"));
				Categories.Add(new AchievementCategory(2, 1, "Towns"));
				Categories.Add(new AchievementCategory(3, 1, "Dungeons"));
					Achievements.Add(new DiscoveryAchievement(0, 2, 0x14EB, false, null, "Minoc", "Discover Minoc Township", 5, "Minoc"));
					Achievements.Add(new DiscoveryAchievement(1, 2, 0x14EB, false, null, "Yew", "Discover Yew Township", 5, "Yew"));
					Achievements.Add(new DiscoveryAchievement(2, 2, 0x14EB, false, null, "Trinsic", "Discover Trinsic Township", 5, "Trinsic"));
					Achievements.Add(new DiscoveryAchievement(3, 2, 0x14EB, false, null, "Cove", "Discover Cove Township", 5, "Cove"));
					Achievements.Add(new DiscoveryAchievement(4, 3, 0x14EB, false, null, "Wrong", "Discover dungeon of Wrong", 5, "Wrong"));
					Achievements.Add(new DiscoveryAchievement(5, 3, 0x14EB, false, null, "Shame", "Discover dungeon of Shame", 5, "Shame"));

            Categories.Add(new AchievementCategory(4, 0, "Crafting"));
				Categories.Add(new AchievementCategory(15, 4, "General"));
					Achievements.Add(new ItemCraftedAchievement(200, 15, 0, false, null, 1, "Cut a bolt.", "Simply cut a Bolt of Cloth to make Cut Cloth.", 5, typeof(Cloth), typeof(RewardScroll)));
            Categories.Add(new AchievementCategory(5, 0, "Resource Gathering"));
				Categories.Add(new AchievementCategory(6, 5, "Mining"));
					Categories.Add(new AchievementCategory(7, 6, "Iron Ore"));
						var achieve = new HarvestAchievement(100, 7, 0, false, null, 100, "100 Iron Ore", "Mine up a bonus resource and increase the Iron Ore total to reach the goal.", 5, typeof(IronOre), typeof(RewardScroll));
						Achievements.Add(achieve);
						Achievements.Add(new HarvestAchievement(100, 7, 1, false, achieve, 500, "500 Iron Ore", "Mine 500 Iron Ore for each bonus received.", 5, typeof(IronOre), typeof(RewardScroll)));
					Categories.Add(new AchievementCategory(8, 6, "Dull C./Shadow I."));
						var achieve1 = new HarvestAchievement(101, 8, 0, false, null, 100, "100 Dull Copper Ore", "Mine 100 Dull Copper Ore for each bonus received.", 5, typeof(DullCopperOre), typeof(RewardScroll));
						Achievements.Add(achieve1);
						Achievements.Add(new HarvestAchievement(101, 8, 1, false, achieve1, 500, "500 Dull Copper Ore", "Mine 500 Dull Copper Ore for each bonus received.", 5, typeof(DullCopperOre), typeof(RewardScroll)));
						var achieve2 = new HarvestAchievement(102, 8, 2, false, null, 100, "100 Shadow Iron Ore", "Mine 100 Shadow Iron Ore for each bonus received.", 5, typeof(ShadowIronOre), typeof(RewardScroll));
						Achievements.Add(achieve2);
						Achievements.Add(new HarvestAchievement(102, 8, 3, false, achieve2, 500, "500 Shadow Iron Ore", "Mine 500 Shadow Iron Ore for each bonus received.", 5, typeof(ShadowIronOre), typeof(RewardScroll)));
					Categories.Add(new AchievementCategory(9, 6, "Copper/Bronze"));
						var achieve3 = new HarvestAchievement(103, 9, 0, false, null, 100, "100 Copper Ore", "Mine 100 Copper Ore for each bonus received.", 5, typeof(CopperOre), typeof(RewardScroll));
						Achievements.Add(achieve3);
						Achievements.Add(new HarvestAchievement(103, 9, 1, false, achieve3, 500, "500 Copper Ore", "Mine 500 Copper Ore for each bonus received.", 5, typeof(CopperOre), typeof(RewardScroll)));
						var achieve4 = new HarvestAchievement(104, 9, 2, false, null, 100, "100 Bronze Ore", "Mine 100 Bronze Ore for each bonus received.", 5, typeof(BronzeOre), typeof(RewardScroll));
						Achievements.Add(achieve4);
						Achievements.Add(new HarvestAchievement(104, 9, 3, false, achieve4, 500, "500 Bronze Ore", "Mine 500 Bronze Ore for each bonus received.", 5, typeof(BronzeOre), typeof(RewardScroll)));
					Categories.Add(new AchievementCategory(10, 6, "Golden/Agapite"));
						var achieve5 = new HarvestAchievement(105, 10, 0, false, null, 100, "100 Golden Ore", "Mine 100 Golden Ore for each bonus received.", 5, typeof(GoldOre), typeof(RewardScroll));
						Achievements.Add(achieve5);
						Achievements.Add(new HarvestAchievement(105, 10, 1, false, achieve5, 500, "500 Golden Ore", "Mine 500 Golden Ore for each bonus received.", 5, typeof(GoldOre), typeof(RewardScroll)));
						var achieve6 = new HarvestAchievement(106, 10, 2, false, null, 100, "100 Agapite Ore", "Mine 100 Agapite Ore for each bonus received.", 5, typeof(AgapiteOre), typeof(RewardScroll));
						Achievements.Add(achieve6);
						Achievements.Add(new HarvestAchievement(106, 10, 3, false, achieve6, 500, "500 Agapite Ore", "Mine 500 Agapite Ore for each bonus received.", 5, typeof(AgapiteOre), typeof(RewardScroll)));
					Categories.Add(new AchievementCategory(11, 6, "Verite/Valorite"));
						var achieve7 = new HarvestAchievement(107, 11, 0, false, null, 100, "100 Verite Ore", "Mine 100 Verite Ore for each bonus received.", 5, typeof(VeriteOre), typeof(RewardScroll));
						Achievements.Add(achieve7);
						Achievements.Add(new HarvestAchievement(107, 11, 1, false, achieve7, 500, "500 Verite Ore", "Mine 500 Verite Ore for each bonus received.", 5, typeof(VeriteOre), typeof(RewardScroll)));
						var achieve8 = new HarvestAchievement(108, 11, 2, false, null, 100, "100 Valorite Ore", "Mine 100 Valorite Ore for each bonus received.", 5, typeof(ValoriteOre), typeof(RewardScroll));
						Achievements.Add(achieve8);
						Achievements.Add(new HarvestAchievement(108, 11, 3, false, achieve8, 500, "500 Valorite Ore", "Mine 500 Valorite Ore for each bonus received.", 5, typeof(ValoriteOre), typeof(RewardScroll)));

            Categories.Add(new AchievementCategory( 12, 0, "Hunting"));
				Achievements.Add(new HunterAchievement(300, 12, 0x25D1, false, null, 5, "Dog Slayer", "Slay 5 Dogs", 5, typeof(Dog)));
				Achievements.Add(new HunterAchievement(301, 12, 0x25D1, false, null, 50, "Dragon Slayer", "Slay 50 Dragon", 5, typeof(Dragon)));

            Categories.Add(new AchievementCategory( 13, 0, "Character Development"));

            Categories.Add(new AchievementCategory( 14, 0, "Other"));

            CommandSystem.Register("tears", AccessLevel.Player, new CommandEventHandler(OpenGump));
        }

        private static void OpenGump(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if(player != null)
            {
		#if STOREONITEM
				if (!AchievementSystemMemoryStone.GetInstance().Achievements.ContainsKey(player.Serial))
					AchievementSystemMemoryStone.GetInstance().Achievements.Add(player.Serial, new Dictionary<int, AchieveData>());
				var achieves = AchievementSystemMemoryStone.GetInstance().Achievements[player.Serial];
                var total = AchievementSystemMemoryStone.GetInstance().GetPlayerPointsTotal(player);
		#else
                var achieves = (player as MythikPlayerMobile).Achievements;
                var total = (player as MythikPlayerMobile).AchievementPointsTotal;
		#endif
                e.Mobile.SendGump(new AchievementGump(achieves, total));
            }
        }

        internal static void SetAchievementStatus(PlayerMobile player, BaseAchievement ach, int progress)
        {
		#if STOREONITEM
           if (!AchievementSystemMemoryStone.GetInstance().Achievements.ContainsKey(player.Serial))
                AchievementSystemMemoryStone.GetInstance().Achievements.Add(player.Serial, new Dictionary<int, AchieveData>());
            var achieves = AchievementSystemMemoryStone.GetInstance().Achievements[player.Serial]; 
		#else
            var achieves = (player as MythikPlayerMobile).Achievements;
		#endif
            if (achieves.ContainsKey(ach.ID))
            {
                if (achieves[ach.ID].Progress >= ach.CompletionTotal)
                    return;
                achieves[ach.ID].Progress += progress;
            }
            else
            {
                achieves.Add(ach.ID, new AchieveData() { Progress = progress });
            }

            if (achieves[ach.ID].Progress >= ach.CompletionTotal)
            {
                player.SendGump(new AchievementObtainedGump(ach),false);
                achieves[ach.ID].CompletedOn = DateTime.Now;
		#if STOREONITEM
                AchievementSystemMemoryStone.GetInstance().AddPoints(player,ach.RewardPoints);
		#else
                (player as MythikPlayerMobile).AchievementPointsTotal += ach.RewardPoints;
		#endif
                if (ach.RewardItems != null && ach.RewardItems.Length > 0)
                {
                    try
                    {
                        player.SendAsciiMessage("You have received an award for completing this achievement!");
                        var item = (Item)Activator.CreateInstance(ach.RewardItems[0]);
                        if (!WeightOverloading.IsOverloaded(player))
                            player.Backpack.DropItem(item);
                        else
                            player.BankBox.DropItem(item);
                    }
                    catch { }
                }
            }
        }
    }
}