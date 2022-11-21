using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Accounting;
using Server.Items;
using Server.Mobiles;

#region About This Reward System
/*
 >>>>>>>>>>>>>>>>>>>>EDITED BY: A.A.R (AKA SythenSE)<<<<<<<<<<<<<<<<<<<<<
 This Reward System Will Replace The Default EA/OSI Veteran Reward System.
 It Is Genericly Named So That Admins Can Use It On Their Servers As-Is.
 I Did Not Code This System - Its The Same Default Veteran Reward System.
 All I Did Was Rename It To RewardSystem And Give It New Gumps I Designed.
 
 Please Note: All Reward Items Will Need To Be Edited So That References
 Made To 'VeteranReward' Are Changed To 'RewardSystem'. I Did This So You
 Can Use This System For More Than Just Veteran Rewards Making It Generic.
*/
#endregion About This Reward System

namespace Server.Engines.RewardSystem
{
    public class RewardSystem
    {
        private static RewardCategory[] m_Categories;
        private static RewardList[] m_Lists;

        public static RewardCategory[] Categories
        {
            get
            {
                if (m_Categories == null)
                    SetupRewardTables();

                return m_Categories;
            }
        }

        public static RewardList[] Lists
        {
            get
            {
                if (m_Lists == null)
                    SetupRewardTables();

                return m_Lists;
            }
        }

        #region Reward System Toggle

        public static bool Enabled = false; // Change To Disable This Reward System
        public static TimeSpan RewardInterval = TimeSpan.FromDays(30.0);
        public static bool SkillCapRewards = false; // Change To Enable Skill Cap Bonus

        #endregion Reward System Toggle

        public static bool HasAccess(Mobile mob, RewardCategory category)
        {
            List<RewardEntry> entries = category.Entries;

            for (int j = 0; j < entries.Count; ++j)
            {
                if (RewardSystem.HasAccess(mob, entries[j]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasAccess(Mobile mob, RewardEntry entry)
        {
            if (Core.Expansion < entry.RequiredExpansion)
            {
                return false;
            }

            TimeSpan ts;
            return HasAccess(mob, entry.List, out ts);
        }

        public static bool HasAccess(Mobile mob, RewardList list, out TimeSpan ts)
        {
            if (list == null)
            {
                ts = TimeSpan.Zero;
                return false;
            }

            Account acct = mob.Account as Account;

            if (acct == null)
            {
                ts = TimeSpan.Zero;
                return false;
            }

            TimeSpan totalTime = (DateTime.Now - acct.Created);

            ts = (list.Age - totalTime);

            if (ts <= TimeSpan.Zero)
                return true;

            return false;
        }

        public static int GetRewardLevel(Mobile mob)
        {
            Account acct = mob.Account as Account;

            if (acct == null)
                return 0;

            return GetRewardLevel(acct);
        }

        public static int GetRewardLevel(Account acct)
        {
            TimeSpan totalTime = (DateTime.Now - acct.Created);

            int level = (int)(totalTime.TotalDays / RewardInterval.TotalDays);

            if (level < 0)
                level = 0;

            return level;
        }

        public static bool HasHalfLevel(Mobile mob)
        {
            Account acct = mob.Account as Account;

            if (acct == null)
                return false;

            return HasHalfLevel(acct);
        }

        public static bool HasHalfLevel(Account acct)
        {
            TimeSpan totalTime = (DateTime.Now - acct.Created);

            Double level = (totalTime.TotalDays / RewardInterval.TotalDays);

            return level >= 0.5;
        }

        public static bool ConsumeRewardPoint(Mobile mob)
        {
            int cur, max;

            ComputeRewardInfo(mob, out cur, out max);

            if (cur >= max)
                return false;

            Account acct = mob.Account as Account;

            if (acct == null)
                return false;

            acct.SetTag("numRewardsChosen", (cur + 1).ToString());

            return true;
        }

        public static void ComputeRewardInfo(Mobile mob, out int cur, out int max)
        {
            int level;

            ComputeRewardInfo(mob, out cur, out max, out level);
        }

        public static void ComputeRewardInfo(Mobile mob, out int cur, out int max, out int level)
        {
            Account acct = mob.Account as Account;

            if (acct == null)
            {
                cur = max = level = 0;
                return;
            }

            level = GetRewardLevel(acct);

            if (level == 0)
            {
                cur = max = 0;
                return;
            }

            string tag = acct.GetTag("numRewardsChosen");

            if (String.IsNullOrEmpty(tag))
                cur = 0;
            else
                cur = Utility.ToInt32(tag);

            if (level >= 6)
                max = 9 + ((level - 6) * 2);
            else
                max = 2 + level;
        }

        public static bool CheckIsUsableBy(Mobile from, Item item, object[] args)
        {
            if (m_Lists == null)
                SetupRewardTables();

            bool isRelaxedRules = (item is DyeTub || item is MonsterStatuette);

            Type type = item.GetType();

            for (int i = 0; i < m_Lists.Length; ++i)
            {
                RewardList list = m_Lists[i];
                RewardEntry[] entries = list.Entries;
                TimeSpan ts;

                for (int j = 0; j < entries.Length; ++j)
                {
                    if (entries[j].ItemType == type)
                    {
                        if (args == null && entries[j].Args.Length == 0)
                        {
                            if ((!isRelaxedRules || i > 0) && !HasAccess(from, list, out ts))
                            {
                                from.SendLocalizedMessage(1008126, true, Math.Ceiling(ts.TotalDays / 30.0).ToString()); // Your account is not old enough to use this item. Months until you can use this item : 
                                return false;
                            }

                            return true;
                        }

                        if (args.Length == entries[j].Args.Length)
                        {
                            bool match = true;

                            for (int k = 0; match && k < args.Length; ++k)
                                match = (args[k].Equals(entries[j].Args[k]));

                            if (match)
                            {
                                if ((!isRelaxedRules || i > 0) && !HasAccess(from, list, out ts))
                                {
                                    from.SendLocalizedMessage(1008126, true, Math.Ceiling(ts.TotalDays / 30.0).ToString()); // Your account is not old enough to use this item. Months until you can use this item : 
                                    return false;
                                }

                                return true;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public static int GetRewardYearLabel(Item item, object[] args)
        {
            int level = GetRewardYear(item, args);
            int cliloc = 1076216 + level;
            if (level > 9)
                cliloc += 4231;
            return cliloc;
        }

        public static int GetRewardYear(Item item, object[] args)
        {
            if (m_Lists == null)
                SetupRewardTables();

            Type type = item.GetType();

            for (int i = 0; i < m_Lists.Length; ++i)
            {
                RewardList list = m_Lists[i];
                RewardEntry[] entries = list.Entries;

                for (int j = 0; j < entries.Length; ++j)
                {
                    if (entries[j].ItemType == type)
                    {
                        if (args == null && entries[j].Args.Length == 0)
                            return i + 1;

                        if (args.Length == entries[j].Args.Length)
                        {
                            bool match = true;

                            for (int k = 0; match && k < args.Length; ++k)
                                match = (args[k].Equals(entries[j].Args[k]));

                            if (match)
                                return i + 1;
                        }
                    }
                }
            }

            return 0;
        }

        public static void SetupRewardTables()
        {
            #region Enter Reward Categories Here

            RewardCategory PlayerEnhancements = new RewardCategory("PLAYER ENHANCEMENTS");
            RewardCategory VeteranRewards = new RewardCategory("VETERAN REWARDS");
            RewardCategory ContributorRewards = new RewardCategory("CONTRIBUTOR REWARDS");

            m_Categories = new RewardCategory[]
			{
				PlayerEnhancements,
				VeteranRewards,
				ContributorRewards
			};

            #endregion Enter Reward Categories Here

            m_Lists = new RewardList[]
				{

	        #region Register Reward Items Here!!

                #region Player Enhancements

                new RewardList ( RewardInterval, 0, new RewardEntry[]
                {
                //new RewardEntry( PlayerEnhancements, "A New Player Mug", typeof( IrishDrinkingMug ) )


                } ),

                #endregion Player Enhancements

                #region Veteran Rewards

                new RewardList ( RewardInterval, 1, new RewardEntry[]
                {
                //new RewardEntry( VeteranRewards, "A Veteran Mug", typeof( IrishDrinkingMug ) )
      
                } ),

                #endregion Veteran Rewards

                #region Contributor Rewards

                new RewardList ( RewardInterval, 0, new RewardEntry[]
                {
                //new RewardEntry( ContributorRewards, "A Contributor Mug", typeof( IrishDrinkingMug ) )

                } )

                #endregion Contributor Rewards
  
            #endregion Register Reward Items Here!!				

				};
        }

        public static void Initialize()
        {
            if (Enabled)
                EventSink.Login += new LoginEventHandler(EventSink_Login);
        }

        private static void EventSink_Login(LoginEventArgs e)
        {
            if (!e.Mobile.Alive)
                return;

            int cur, max, level;

            ComputeRewardInfo(e.Mobile, out cur, out max, out level);
            
            if (e.Mobile.SkillsCap == 7000 || e.Mobile.SkillsCap == 7050 || e.Mobile.SkillsCap == 7100 || e.Mobile.SkillsCap == 7150 || e.Mobile.SkillsCap == 7200)
            {
                if (level > 4)
                    level = 4;
                else if (level < 0)
                    level = 0;

                if (SkillCapRewards)
                    e.Mobile.SkillsCap = 7000 + (level * 50);
                else
                    e.Mobile.SkillsCap = 7000;
            }

            if (Core.ML && e.Mobile is PlayerMobile && !((PlayerMobile)e.Mobile).HasStatReward && HasHalfLevel(e.Mobile))
            {
                ((PlayerMobile)e.Mobile).HasStatReward = true;
                e.Mobile.StatCap += 5;
            }

            if (cur < max)
                e.Mobile.SendGump(new RewardNoticeGump(e.Mobile));
            
        }
    }

    public interface IRewardItem
    {
        bool IsRewardItem { get; set; }
    }
}