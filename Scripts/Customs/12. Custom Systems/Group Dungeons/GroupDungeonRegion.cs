//////////////////////////////////////////////////////////////////
//                     Group Dungeons 1.6 Final                 //
//                           -The Jedi-                         //
//                          RunUO 2.0 RC1                       //
//////////////////////////////////////////////////////////////////

using Server;
using System;
using Server.Items;
using Server.Spells;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Spells.Eighth;
using Server.Spells.Chivalry;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using Server.Engines.PartySystem;
using System.Collections.Generic;
using Server.SkillHandlers;

namespace Server.Regions
{
    public class PlayerMovementEntry
    {
        private PlayerMobile m_Player;
        private DateTime m_LastMovement;

        public PlayerMobile Player { get { return m_Player; } set { m_Player = value; } }
        public DateTime LastMovement { get { return m_LastMovement; } set { m_LastMovement = value; } }

        public PlayerMovementEntry(PlayerMobile p)
        {
            m_Player = p;
            m_LastMovement = DateTime.Now;
        }
    }

    public class AFKTimer : Timer
    {
        private GroupDungeonRegion m_Dreg;

        public AFKTimer(GroupDungeonRegion reg) : base(TimeSpan.FromSeconds(30))
        { 
            m_Dreg = reg; 
        }

        protected override void OnTick()
        {
            //check the movement lists
            if (m_Dreg.PlayerMovements != null)
            {
                for (int i = 0; i < m_Dreg.PlayerMovements.Count; i++)
                {
                    PlayerMovementEntry p = (PlayerMovementEntry)m_Dreg.PlayerMovements[i];
                    if (p.Player != null)
                    {
                        if (DateTime.Now - p.LastMovement >= m_Dreg.AFKTime)
                        {
                            //a player in the list has sat still for 5 min or more, so kick
                            if (Region.Find(p.Player.Location, p.Player.Map) == m_Dreg)
                                p.Player.SendMessage(34, "You are now being kicked out of the dungeon for being AFK: Away From Keyboard.");
                            Timer.DelayCall(TimeSpan.FromSeconds(2), new TimerStateCallback(GroupDungeonRegion.KickCallBack), new object[] { p.Player, m_Dreg.Stone });
                            p.Player = null;
                        }
                    }
                }
            }
            this.Start(); //start the timer again
        }
    }



	public class GroupDungeonRegion : Region
	{
		/// <summary>
		/// ///////////////////////////////////////////////////////////
        /// This is where we set all the flags and values for the dungeon system.
        /// ///////////////////////////////////////////////////////////
		/// </summary>
        private static TimeSpan m_RespawnDelay = TimeSpan.FromHours(3); // Default dungeon respawn timer is 3 hours.
        private static TimeSpan m_RezTimer = TimeSpan.FromSeconds(30); //Default to kick the dead group is 30 seconds.
        private static TimeSpan m_KickTimer = TimeSpan.FromMinutes(10); //Default to kick the winning group is 10 minutes.
        private static TimeSpan m_AFKTime = TimeSpan.FromMinutes(5); //Default to kick a player for being AFK is 5 minutes.
        private static bool m_UseRezTimer = true; //Default is to move the corpses of a wiped group. (true)
        private static bool m_BlessCorpses = true; //Default is to bless the corpses when they are moved. (true)
        private static bool m_UseGlobalRespawn = true; //Default is to set all spawners to the global spawn time. (true)
        private static bool m_AllowPvP = false; //Default is pvp disabled. (false)
        private static bool m_AllowFactionBeneficial = true; // default is to let differing factions perform beneficial acts. (true)

        private GroupDungeonStone m_Stone;
        private List<PlayerMovementEntry> m_PlayerMovements;
        private AFKTimer m_AFKTimer;

        #region Get/Set and Constructors

        public TimeSpan AFKTime { get { return m_AFKTime; } }
        
        public GroupDungeonStone Stone
		{
			get{ return m_Stone; }            
		}

        public List<PlayerMovementEntry> PlayerMovements
        {
            get { return m_PlayerMovements; }
            set { m_PlayerMovements = value; }
        }

        public GroupDungeonRegion(GroupDungeonStone stone, Map map, string name, Rectangle2D area)
            : base(name, map, 0, area)
        {
            //Link the region to a control stone, and vise-versa.
            m_Stone = stone; stone.IRegion = this;
            m_PlayerMovements = new List<PlayerMovementEntry>();
            m_AFKTimer = new AFKTimer(this);
        }

        #endregion
		#region Prevent Mark
		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
            if ((s is RecallSpell || s is MarkSpell || s is GateTravelSpell || s is SacredJourneySpell) && m.AccessLevel == AccessLevel.Player)
			{
				m.SendMessage( "You cannot cast that spell here." );
				return false;
			}
			else
			{
				return base.OnBeginSpellCast( m, s );
			}
		}
		#endregion
        #region Methods

        public void DungeonReset() 
        {
            IPooledEnumerable eable = m_Stone.GetItemsInRange(m_Stone.Size);

            ArrayList trash = new ArrayList();

            //Here we set all spawners to global values(if bool)
            if (this != null && m_Stone != null) // error check & global bool
            {
                // Find all spawners within the dungeon, and set their values.
                foreach (Item s in eable)
                {
                    if (m_UseGlobalRespawn) // control spawners
                    {
                        if (s is Spawner)
                        {
                            Spawner sp = (Spawner)s;
                            sp.MaxDelay = sp.MinDelay = m_RespawnDelay;
                            sp.Respawn();
                        }
                        else if (s is XmlSpawner) //// comment out if XMLSpawner is not installed
                        {
                            XmlSpawner sp = (XmlSpawner)s;
                            sp.MaxDelay = sp.MinDelay = m_RespawnDelay;
                            sp.Respawn();
                        }
                        else if (s is PremiumSpawner) //// comment out if PremiumSpawner is not installed
                        {
                            PremiumSpawner sp = (PremiumSpawner)s;
                            sp.MaxDelay = sp.MinDelay = m_RespawnDelay;
                            sp.Respawn();
                        }
                    }
                        // add trash to the list
                    if (s.Movable || s is Corpse)
                    {
                        trash.Add(s);
                    }
                        // Lock Dungeon Doors.
                    else if (s is DungeonDoor)
                    {
                        DungeonDoor dd = (DungeonDoor)s;
                        dd.Locked = true;
                    }
                }
            }

            // delete trash and corpses
            for (int i = 0; i < trash.Count; i++)
                ((Item)trash[i]).Delete();

            // Reset AFK list and restart the timers.
            if (m_PlayerMovements != null)
                m_PlayerMovements.Clear();
            if (m_AFKTimer != null)
            {
                if (m_AFKTimer.Running)
                    m_AFKTimer.Stop();
                m_AFKTimer.Start();
            }
        }

        public bool CanEnter(Mobile m)
        {
            if (m is PlayerMobile && m_Stone != null)
            {
                PlayerMobile pm = (PlayerMobile)m;

                if (m.AccessLevel > AccessLevel.Player) // Staff can always enter.
                    return true;

                // If the dungeon is full and this is a player, don't allow entrance.
                if (CountPlayers() >= m_Stone.MaxPlayers && m is PlayerMobile)
                {
                    m.SendMessage(34, "{0} is full right now. Please try again later.", m_Stone.DungeonName);
                    return false;
                }

                // If the player is not in the allowed skills window, don't allow.
                /*
				if (pm.SkillsTotal > m_Stone.MaxSkills)
                {
                    pm.SendMessage(34, "You are too highly skilled to enter {0}.", m_Stone.DungeonName);
                    return false;
                }
                if (pm.SkillsTotal < m_Stone.MinSkills)
                {
                    pm.SendMessage(34, "You are not skilled enough to enter {0}.", m_Stone.DungeonName);
                    return false;
                }
				*/

                //
                //          They have passed the restrictions, check for party now
                //

                Party p = Party.Get(pm);
                
                //
                // if count = 0 let them in. if not, check if either has a party. (no = no enter)
                // if they do have one, check to see if it is the same.???
                if (CountPlayers() > 0)
                {
                    if (p == null) // no party so cant enter
                    {
                        pm.SendMessage(34, "You are not in a party, and the dungeon is occupied.");
                        return false;
                    }
                    else
                    {
                        foreach (Mobile mobs in m_Stone.GetMobilesInRange(m_Stone.Size))
                        {
                            for (int i = 0; i < p.Members.Count; i++)
                            {
                                PartyMemberInfo pmem = (PartyMemberInfo)p.Members[i];
                                if (pmem.Mobile == mobs)
                                    return true;
                            }
                        }
                        pm.SendMessage(34, "You must join the party inside to enter.");
                        return false;
                    }
                }
            }

            // Otherwise we are good.
            return true;
        }

        public int CountPlayers()
        {
            int count = 0;

            if (m_Stone != null && this != null) // error check (else return "empty dungeon")
            {
                // Find all Players within the dungeon, and count them if they are not staff.
                foreach (Mobile m in m_Stone.GetMobilesInRange(m_Stone.Size))
                {
                    if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player)
                        count++;
                }
            }

            return count;
        }

        #endregion

        #region Overrides

        public override bool AllowSpawn()
        {
            return true;
        }

        public override bool AllowHarmful(Mobile from, Mobile target)
        {
            if (from is PlayerMobile && target is PlayerMobile && !m_AllowPvP)
                return false;
            return base.AllowHarmful(from, target);
        }

        public override bool AllowBeneficial(Mobile from, Mobile target)
        {
            if (from is PlayerMobile && target is PlayerMobile && m_AllowFactionBeneficial)
                return true;
            return base.AllowBeneficial(from, target);
        }

        public override void OnExit( Mobile m )
		{
            if (m is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)m;

                base.OnExit(pm);

                if (m_Stone != null)
                {
                    pm.SendMessage(34, "You have left {0}.", m_Stone.DungeonName);

                    // check to see if any dead players are stranded inside.
                    int count = 0;
                    foreach (Mobile m2 in m_Stone.GetMobilesInRange(m_Stone.Size))
                    {
                        if (m2 is PlayerMobile && m2.Alive && m2 != m && (m2.AccessLevel == AccessLevel.Player)) 
                            count++;
                    }

                    // find all players and invoke kick.

                    if (count == 0)
                    {
                        foreach (Mobile m2 in m_Stone.GetMobilesInRange(m_Stone.Size))
                        {
                            if (m2 is PlayerMobile && m2.AccessLevel == AccessLevel.Player)
                                Timer.DelayCall(TimeSpan.FromSeconds(5), new TimerStateCallback(GroupWipe), new object[] { m2, m_Stone });
                        }
                    }
                }
                else
                    pm.SendMessage(38, "ERROR: Dungeon not set up, Contact Staff."); //Not linked to a stone.
            }
		}

		public override void OnEnter( Mobile m )
		{
            if (m is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)m;

                if (pm != null)
                {
                    if (m_Stone != null)
                    {
                        pm.SendMessage(34, "You have entered {0}.", m_Stone.DungeonName);

                        //if entering an empty dungeon, reset it.
                        if (CountPlayers() <= 1 && m.AccessLevel == AccessLevel.Player)
                             this.DungeonReset();
                         
                        OnLocationChanged(m, m.Location);
                    }
                    else
                        pm.SendMessage(38, "ERROR: Dungeon not set up, Contact Staff."); //Not linked to a stone.
                }
            }

            base.OnEnter(m);
        }

        public override void OnDeath(Mobile m)
        {
            // Check to see if the dungeon has been cleared, then kick callback.
            if (m is BaseCreature)
            {
                int count = 0;
                foreach (Mobile m2 in m_Stone.GetMobilesInRange(m_Stone.Size))
                {
                    if (m2 is BaseCreature && m2 != m) // count living creatures
                        count++;
                }

                if (count <= 0) // All dead
                {
                    // find all players and invoke kick.
                    foreach (Mobile m2 in m_Stone.GetMobilesInRange(m_Stone.Size))
                    {
                        if (m2.Player) // call dungeon finish
                            Timer.DelayCall(TimeSpan.FromSeconds(5), new TimerStateCallback(DungeonCleared), new object[] { m2, m_Stone });
                    }
                }
            }

            // Check to see if the party has been cleared, then kick callback.
            else if (m is PlayerMobile)
            {
                int count = 0;
                foreach (Mobile m2 in m_Stone.GetMobilesInRange(m_Stone.Size))
                {
                    if (m2 is PlayerMobile && m2.Alive && m2 != m && (m2.AccessLevel == AccessLevel.Player)) // count living non-staff players
                        count++;
                }

                // find all players and invoke kick.
                
                if (count <= 0)
                {
                    foreach (Mobile m2 in m_Stone.GetMobilesInRange(m_Stone.Size))
                    {
                        if (m2 is PlayerMobile && m2.AccessLevel == AccessLevel.Player)
                            Timer.DelayCall(TimeSpan.FromSeconds(5), new TimerStateCallback(GroupWipe), new object[] { m2, m_Stone });
                    }
                }
            }

            //return base.OnDeath(m);
        }

        /*public override void OnLocationChanged(Mobile m, Point3D oldLocation)
        {
            /*
             * if they are a player, remove any old entries for that player
             * and add a new one for this moment
             * 
            if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player)
            {
                if (m_PlayerMovements != null)
                {
                    if (m_PlayerMovements.Count > 0)
                    {
                        for (int i = 0; i < m_PlayerMovements.Count; i++)
                        {
                            if (m_PlayerMovements[i].Player == m)
                                m_PlayerMovements[i].LastMovement = DateTime.Now;
                            else
                                m_PlayerMovements.Add(new PlayerMovementEntry((PlayerMobile)m));
                        }
                    }
                    else
                        m_PlayerMovements.Add(new PlayerMovementEntry((PlayerMobile)m));
                }
            }
        }*/

        public override void OnUnregister()
        {
            m_PlayerMovements.Clear();
            m_AFKTimer.Stop();
            base.OnUnregister();
        }

        #endregion

        #region Callback methods

        public static void GroupWipe(object state)
        {
            object[] args = (object[])state; // Get the parameters.. mobile and dungeon stone.
            Mobile m = (Mobile)args[0];
            GroupDungeonStone stone = (GroupDungeonStone)args[1];

            //we decided everyone was dead, so grab every player, tell them the news, bless corpses,
            //then delay the kick method.
            if (m.Player && m.AccessLevel == AccessLevel.Player)
            {
                PlayerMobile pm = (PlayerMobile)m;
                pm.SendMessage(34,"Your group has been defeated. You, any pets, and your corpse will be telported out of the instance in {0} minutes, {1} seconds.", m_RezTimer.Minutes, m_RezTimer.Seconds);
                
                if (m_BlessCorpses) // if global flag is true, bless the corpse
                    pm.Corpse.LootType = LootType.Blessed;
                
                Timer.DelayCall(m_RezTimer, new TimerStateCallback(KickCallBack), new object[] { m, stone }); 
            }
        }

        public static void DungeonCleared(object state)
        {
            object[] args = (object[])state; // Get the parameters.. mobile and dungeon stone.
            Mobile m = (Mobile)args[0];
            GroupDungeonStone stone = (GroupDungeonStone)args[1];

            //we decided everyone was dead, so grab every player, tell them the news, bless corpses,
            //then delay the kick method.
            if (m.Player && m.AccessLevel == AccessLevel.Player)
            {
                PlayerMobile pm = (PlayerMobile)m;
                pm.SendMessage(34, "Your group has conquered {0}! You will be telported out of the instance in {1} minutes, {2} seconds.", stone.DungeonName, m_KickTimer.Minutes, m_KickTimer.Seconds);
                Timer.DelayCall(m_KickTimer, new TimerStateCallback(KickCallBack), new object[] { m, stone });
            }
        }

        public static void KickCallBack(object state)
        {
            object[] args = (object[])state; // Get the parameters.. mobile and dungeon stone.
            Mobile m = (Mobile)args[0];
            GroupDungeonStone stone = (GroupDungeonStone)args[1];

            // If the corpse still exists and the player is still a player,
            // And is still in the region
            // then move them both to the entrance point.
            if (m.Player)
            {
                Point3D pt = stone.EntrancePoint;
                Map map = stone.EntranceMap;
                Region reg = Region.Find(m.Location, m.Map);



                if (map != Map.Internal && pt != Point3D.Zero)
                {
                    // Only move if they are still there.
                    if (reg == stone.IRegion)
                    {
                        //Kick any corpses
                        if (m.Corpse != null)
                            m.Corpse.MoveToWorld(pt, map);
                        
                        // Kick any pets
                        ArrayList petlist = new ArrayList();
                        foreach (Mobile pet in stone.GetMobilesInRange(stone.Size))
                        {
                            if (pet is BaseCreature && ((BaseCreature)pet).ControlMaster == m)
                            {
                                petlist.Add(pet);
                            }
                        }
                        foreach (Mobile pet in petlist)
                            pet.MoveToWorld(pt, map);

                        //kick player
                        m.MoveToWorld(pt, map);
                    }
                }
            }
        }

        #endregion

    }
}
