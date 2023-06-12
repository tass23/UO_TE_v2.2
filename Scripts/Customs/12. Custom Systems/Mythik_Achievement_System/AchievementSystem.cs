#define STOREONITEM
using Server;
using Server.SkillHandlers;
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
				Categories.Add(new AchievementCategory(5, 4, "Tailoring"));
					Categories.Add(new AchievementCategory(6, 5, "Hats"));				
					Categories.Add(new AchievementCategory(7, 5, "Shirts"));
						var achieve9 = new ItemCraftedAchievement(200, 6, 0, false, null, 5, "Skullcaps", "Create 5 Skullcaps.", 5, typeof(SkullCap), typeof(RewardScroll));
							Achievements.Add(achieve9);
							Achievements.Add(new ItemCraftedAchievement(201, 6, 1, false, achieve9, 5, "Bandanas", "Create 5 Bandanas.", 5, typeof(Bandana), typeof(RewardScroll)));
						var achieve10 = new ItemCraftedAchievement(202, 7, 2, false, null, 5, "Doublets", "Create 5 Doublets.", 5, typeof(Doublet), typeof(RewardScroll));
							Achievements.Add(achieve10);
							Achievements.Add(new ItemCraftedAchievement(203, 7, 3, false, achieve10, 5, "Shirts", "Create 5 Shirts.", 5, typeof(Shirt), typeof(RewardScroll)));

            Categories.Add(new AchievementCategory(8, 0, "Resource Gathering"));
				Categories.Add(new AchievementCategory(9, 8, "Mining"));
					Categories.Add(new AchievementCategory(10, 9, "Iron Ore"));
						var achieve = new HarvestAchievement(100, 10, 0, false, null, 100, "100 Iron Ore", "Mine 100 Iron Ore.", 5, typeof(IronOre), typeof(RewardScroll));
							Achievements.Add(achieve);
							Achievements.Add(new HarvestAchievement(100, 10, 1, false, achieve, 500, "500 Iron Ore", "Mine 500 Iron Ore.", 5, typeof(IronOre), typeof(RewardScroll)));
					Categories.Add(new AchievementCategory(11, 9, "Dull C./Shadow I."));
						var achieve1 = new HarvestAchievement(101, 11, 0, false, null, 100, "100 Dull Copper Ore", "Mine 100 Dull Copper Ore.", 5, typeof(DullCopperOre), typeof(RewardScroll));
							Achievements.Add(achieve1);
							Achievements.Add(new HarvestAchievement(101, 11, 1, false, achieve1, 500, "500 Dull Copper Ore", "Mine 500 Dull Copper Ore.", 5, typeof(DullCopperOre), typeof(RewardScroll)));
						var achieve2 = new HarvestAchievement(102, 11, 2, false, null, 100, "100 Shadow Iron Ore", "Mine 100 Shadow Iron Ore.", 5, typeof(ShadowIronOre), typeof(RewardScroll));
							Achievements.Add(achieve2);
							Achievements.Add(new HarvestAchievement(102, 11, 3, false, achieve2, 500, "500 Shadow Iron Ore", "Mine 500 Shadow Iron Ore.", 5, typeof(ShadowIronOre), typeof(RewardScroll)));
					Categories.Add(new AchievementCategory(12, 9, "Copper/Bronze"));
						var achieve3 = new HarvestAchievement(103, 12, 0, false, null, 100, "100 Copper Ore", "Mine 100 Copper Ore.", 5, typeof(CopperOre), typeof(RewardScroll));
							Achievements.Add(achieve3);
							Achievements.Add(new HarvestAchievement(103, 12, 1, false, achieve3, 500, "500 Copper Ore", "Mine 500 Copper Ore.", 5, typeof(CopperOre), typeof(RewardScroll)));
						var achieve4 = new HarvestAchievement(104, 12, 2, false, null, 100, "100 Bronze Ore", "Mine 100 Bronze Ore.", 5, typeof(BronzeOre), typeof(RewardScroll));
							Achievements.Add(achieve4);
							Achievements.Add(new HarvestAchievement(104, 12, 3, false, achieve4, 500, "500 Bronze Ore", "Mine 500 Bronze Ore.", 5, typeof(BronzeOre), typeof(RewardScroll)));
					Categories.Add(new AchievementCategory(13, 9, "Golden/Agapite"));
						var achieve5 = new HarvestAchievement(105, 13, 0, false, null, 100, "100 Golden Ore", "Mine 100 Golden Ore.", 5, typeof(GoldOre), typeof(RewardScroll));
							Achievements.Add(achieve5);
							Achievements.Add(new HarvestAchievement(105, 13, 1, false, achieve5, 500, "500 Golden Ore", "Mine 500 Golden Ore.", 5, typeof(GoldOre), typeof(RewardScroll)));
						var achieve6 = new HarvestAchievement(106, 13, 2, false, null, 100, "100 Agapite Ore", "Mine 100 Agapite Ore.", 5, typeof(AgapiteOre), typeof(RewardScroll));
							Achievements.Add(achieve6);
							Achievements.Add(new HarvestAchievement(106, 13, 3, false, achieve6, 500, "500 Agapite Ore", "Mine 500 Agapite Ore.", 5, typeof(AgapiteOre), typeof(RewardScroll)));
					Categories.Add(new AchievementCategory(14, 9, "Verite/Valorite"));
						var achieve7 = new HarvestAchievement(107, 14, 0, false, null, 100, "100 Verite Ore", "Mine 100 Verite Ore.", 5, typeof(VeriteOre), typeof(RewardScroll));
							Achievements.Add(achieve7);
							Achievements.Add(new HarvestAchievement(107, 14, 1, false, achieve7, 500, "500 Verite Ore", "Mine 500 Verite Ore.", 5, typeof(VeriteOre), typeof(RewardScroll)));
						var achieve8 = new HarvestAchievement(108, 14, 2, false, null, 100, "100 Valorite Ore", "Mine 100 Valorite Ore.", 5, typeof(ValoriteOre), typeof(RewardScroll));
							Achievements.Add(achieve8);
							Achievements.Add(new HarvestAchievement(108, 14, 3, false, achieve8, 500, "500 Valorite Ore", "Mine 500 Valorite Ore.", 5, typeof(ValoriteOre), typeof(RewardScroll)));

            Categories.Add(new AchievementCategory( 15, 0, "Hunting"));
				Achievements.Add(new HunterAchievement(300, 15, 0, false, null, 5, "Dog Slayer", "Slay 5 Dogs", 5, typeof(Dog), typeof(RewardScroll)));				
				Achievements.Add(new HunterAchievement(301, 15, 1, false, null, 5, "Cat Slayer", "Slay 5 Cats", 5, typeof(Cat), typeof(RewardScroll)));
				Achievements.Add(new HunterAchievement(302, 15, 2, false, null, 5, "Giant Rat Slayer", "Slay 5 Giant Rats", 5, typeof(GiantRat), typeof(RewardScroll)));
				Achievements.Add(new HunterAchievement(303, 15, 3, false, null, 5, "Imp Slayer", "Slay 5 Imps", 5, typeof(Imp), typeof(RewardScroll)));
				Achievements.Add(new HunterAchievement(304, 15, 4, false, null, 5, "Fire Dragon Slayer", "Slay 5 Fire/Magma/Flame/Flare Dragons", 5, typeof(FireDragon), typeof(RewardScroll)));
				Achievements.Add(new HunterAchievement(305, 15, 5, false, null, 5, "Bird Slayer", "Slay 5 Birds", 5, typeof(Bird), typeof(RewardScroll)));

            Categories.Add(new AchievementCategory( 16, 0, "Character Development"));				
				Categories.Add(new AchievementCategory(17, 16, "Skills"));
					var achieve11 = new SkillGainAchievement(400, 17, 0, false, null, 300, "Tailoring", "Reach Neophyte Skill.", SkillName.Tailoring, 5, typeof(RewardScroll));
						Achievements.Add(achieve11);
						Achievements.Add(new SkillGainAchievement(401, 17, 0, false, achieve11, 400, "Tailoring", "Reach Novice Skill.", SkillName.Tailoring, 5, typeof(RewardScroll)));
					var achieve12 = new SkillGainAchievement(402, 17, 1, false, null, 300, "Begging", "Reach Apprentice Skill.", SkillName.Begging, 5, typeof(RewardScroll));
						Achievements.Add(achieve12);				
						Achievements.Add(new SkillGainAchievement(403, 17, 1, false, achieve12, 300, "Begging", "Reach Apprentice Skill.", SkillName.Begging, 5, typeof(RewardScroll)));
					var achieve13 = new SkillGainAchievement(404, 17, 2, false, null, 300, "Arms Lore", "Reach Neophyte Skill.", SkillName.ArmsLore, 5, typeof(RewardScroll));
						Achievements.Add(achieve13);
						Achievements.Add(new SkillGainAchievement(405, 17, 2, false, achieve13, 300, "Arms Lore", "Reach Neophyte Skill.", SkillName.ArmsLore, 5, typeof(RewardScroll)));

            Categories.Add(new AchievementCategory( 18, 0, "Other"));

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
            var total = AchievementSystemMemoryStone.GetInstance().GetPlayerPointsTotal(player);
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
						player.CloseGump(typeof(AchievementGump));
						player.SendGump(new AchievementGump(achieves, total));
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