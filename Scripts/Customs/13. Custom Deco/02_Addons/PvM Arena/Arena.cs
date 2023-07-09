//################################################################
//By Rocko Wang
//www.medievaldream.com
//Thanks to "The world of dreams" and Drocket for the idea of this
//################################################################

using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Game.Arena
{
    public enum ArenaFightType
    {
        None,
        SingleFight,
        ChallangeGame,
        PVP
    }

    public class ArenaControlStone : Item
    {
        public static Type[][] m_Types;

        public static void Initialize()
        {
            InitializeMobList();
        }

        public static void InitializeMobList()
        {

        }

        public static Type[] m_ChallangeTypes = new Type[]{
            typeof(HeadlessOne),
            typeof(Harpy),
            typeof(Gargoyle),
            typeof(StoneHarpy),
            typeof(Lich),
            typeof(FireElemental),
            typeof(ElderGazer),
            typeof(OgreLord),
            typeof(Daemon),
        };

        public static double ArenaMangementIntervalInSec = 1.0;
        // general
        private bool m_Running;

        [CommandProperty(AccessLevel.Administrator)]
        public bool Running
        {
            get { return m_Running; }
            set
            {
                if (value && IsValidSetup())
                    m_Running = value;
                else
                    m_Running = false;
            }
        }

        private bool m_Broadcast = false;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Broadcast
        {
            get { return m_Broadcast; }
            set { m_Broadcast = value; }
        }


        public bool IsValidSetup()
        {
            return !(m_X1Y1==Point2D.Zero || m_X2Y2==Point2D.Zero || m_ExpelPoint== Point3D.Zero
                    || m_StartPointOpponent==Point3D.Zero || m_StartPointPlayer==Point3D.Zero
                    || m_StartPointOpponent.X < m_X1Y1.X || m_StartPointOpponent.X > m_X2Y2.X
                    || m_StartPointOpponent.Y < m_X1Y1.Y || m_StartPointOpponent.Y > m_X2Y2.Y
                    || m_StartPointPlayer.X < m_X1Y1.X || m_StartPointPlayer.X > m_X2Y2.X
                    || m_StartPointPlayer.Y < m_X1Y1.Y || m_StartPointPlayer.Y > m_X2Y2.Y);
        }

        // region and starting location
        private Point2D m_X1Y1;
        [CommandProperty(AccessLevel.Administrator)]
        public Point2D X1Y1 { get { return m_X1Y1; } set { m_X1Y1 = value; } }

        private Point2D m_X2Y2;
        [CommandProperty(AccessLevel.Administrator)]
        public Point2D X2Y2 { get { return m_X2Y2; } set { m_X2Y2 = value; } }

        private Point3D m_StartPointPlayer;
        [CommandProperty(AccessLevel.Administrator)]
        public Point3D StartPointPlayer { get { return m_StartPointPlayer; } set { m_StartPointPlayer = value; } }

        private Point3D m_StartPointOpponent;
        [CommandProperty(AccessLevel.Administrator)]
        public Point3D StartPointOpponent { get { return m_StartPointOpponent; } set { m_StartPointOpponent = value; } }

        private Point3D m_ExpelPoint;
        [CommandProperty(AccessLevel.Administrator)]
        public Point3D ExpelPoint { get { return m_ExpelPoint; } set { m_ExpelPoint = value; } }

        // users and mobs
        // this scans the fighting parties to make sure the fight is still going.
        // otherwise it ejects the current player and moves on to next person in line.

        private bool m_InEvent;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool InEvent
        {
            get { return m_InEvent; }
            set { m_InEvent = value; }
        }

        private Type m_EventOpponentType;
        [CommandProperty(AccessLevel.GameMaster)]
        public Type EventOpponentType
        {
            get { return m_EventOpponentType; }
            set { m_EventOpponentType = value; }
        }

        private ManageTimer m_ManageTimer; 

        private static int m_MaxInLine = 5;

        private Mobile m_ActiveUser;

        private Mobile m_ActiveUserMount;

        private Mobile m_NPCOpponent;

        private List<Mobile> m_UsersInLine;

        private ArenaFightType m_FightType;

        private DateTime m_StartTime;

        private Type m_OpponentType;

        private Hashtable m_ScoreTable;

        public Hashtable ScoreTable
        {
            get { return m_ScoreTable; }
            set { m_ScoreTable = value; }
        }

        public Mobile ActiveUser
        {
            get { return m_ActiveUser; }
            set { m_ActiveUser = value; }
        }

        public Mobile ActiveUserMount
        {
            get { return m_ActiveUserMount; }
            set { m_ActiveUserMount = value; }
        }

        public Mobile NPCOpponent
        {
            get { return m_NPCOpponent; }
            set { m_NPCOpponent = value; }
        }

        public List<Mobile> UsersInLine
        {
            get { return m_UsersInLine; }
            set { m_UsersInLine = value; }
        }

        [Constructable]
        public ArenaControlStone()
            : base(0xED4)
        {
            Name = "an arena control stone";
            m_UsersInLine = new List<Mobile>();
            m_ManageTimer = new ManageTimer(this);
            m_ScoreTable = new Hashtable();
            m_Broadcast = false;
            m_Running = false;
        }
        public ArenaControlStone(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(m_Running);
            writer.Write(m_X1Y1);
            writer.Write(m_X2Y2);
            writer.Write(m_StartPointPlayer);
            writer.Write(m_StartPointOpponent);
            writer.Write(m_ExpelPoint);
            writer.Write(m_ScoreTable.Count);
            foreach (DictionaryEntry de in m_ScoreTable)
            {
                writer.Write((Mobile)de.Key);
                writer.Write((int)de.Value);
            }

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    m_Running = reader.ReadBool();
                    m_X1Y1 = reader.ReadPoint2D();
                    m_X2Y2 = reader.ReadPoint2D();
                    m_StartPointPlayer = reader.ReadPoint3D();
                    m_StartPointOpponent = reader.ReadPoint3D();
                    m_ExpelPoint = reader.ReadPoint3D();
                    m_ScoreTable = new Hashtable();
                    int len = reader.ReadInt();
                    for (int i = 0; i < len; i++)
                        m_ScoreTable.Add(reader.ReadMobile(), reader.ReadInt());
                    m_UsersInLine = new List<Mobile>();
                    m_ManageTimer = new ManageTimer(this);
                    break;
            }

            if (m_UsersInLine == null)
                m_UsersInLine = new List<Mobile>();
        }
 
        public override void OnDoubleClick(Mobile from)
        {
            if (!from.Alive)
            {
                from.SendMessage("Bring yourself back to life first is a better idea.");
                return;
            }

            if (!from.InRange(this.GetWorldLocation(), 2))
            {
                from.SendMessage("You are too far away to use the arena control.");
                return;
            }

            if (!m_Running)
            {
                from.SendMessage("The arena is currently under maintainance. Please wait for further notice.");
                return;
            }

            if (m_UsersInLine.Count>= m_MaxInLine)
            {
                from.SendMessage("The arena has been fully booked at the moment. Try back later.");
                return;
            }

            if (m_ActiveUser != null)
            {
                from.SendMessage("The arena seems to be in use by someone else. Please wait until that player finishes.");
                return;
            }

            from.CloseGump(typeof(ArenaRegisterGump));
            from.SendGump(new ArenaRegisterGump(from, this, ArenaFightType.None));
        }

        public void PreparePVMCombat(Mobile from, Type mobType)
        {
            Mobile theMob = (Mobile)Activator.CreateInstance(mobType);
            if (theMob == null || !(theMob is BaseCreature))
            {
                from.SendMessage("That mobile type is not valid.");
                return;
            }

            BaseCreature creature = (BaseCreature)theMob;
            creature.IsArenaMob = true;

            foreach (Item item in creature.Items)
                item.LootType = LootType.Newbied;

            Container pack = creature.Backpack;

            if (pack != null)
            {
                for (int i = pack.Items.Count - 1; i >= 0; --i)
                {
                    if (i >= pack.Items.Count)
                        continue;

                    pack.Items[i].Delete();
                }
            }

            m_ActiveUser = from;
            m_NPCOpponent = theMob;
            m_FightType = ArenaFightType.SingleFight;
            m_OpponentType = mobType;


            from.SendMessage("Now prepare for combat!");
            Timer.DelayCall(TimeSpan.FromSeconds(4), new TimerStateCallback(StartPVMCombat), new object[] { from, theMob });

        }

        private void StartPVMCombat(object state)
        {
            object[] states = (object[])state;
            Mobile from = (Mobile)states[0];
            Mobile theMob = (Mobile)states[1];

            if (from.Deleted)
            {
                CleanUp();
                return;
            }
            if (theMob.Deleted)
            {
                from.SendMessage("There is something wrong with the arena system. Please report to a GM.");
                CleanUp();
                return;
            }

            from.MoveToWorld(StartPointPlayer, Map);
            if (from.Mounted && from.Mount!= null && from.Mount is BaseMount)
                m_ActiveUserMount = (Mobile)(from.Mount);
            theMob.MoveToWorld(StartPointOpponent, Map);
            theMob.Combatant = from;
            if (m_ManageTimer.Running)
                m_ManageTimer.Stop();
            m_ManageTimer.Start();
            m_StartTime = DateTime.Now;
        }

        private void CleanUp()
        {
            m_ManageTimer.Stop();
            if (m_ActiveUser != null && !m_ActiveUser.Deleted)
            {
                m_ActiveUser.MoveToWorld(m_ExpelPoint, Map);
                if (m_ActiveUser.Corpse != null && IsInsideArena(m_ActiveUser.Corpse))
                    m_ActiveUser.Corpse.MoveToWorld(m_ExpelPoint, Map);
            }
            if (m_ActiveUserMount != null && !m_ActiveUserMount.Deleted && m_ActiveUserMount.Map!=Map.Internal)
                m_ActiveUserMount.MoveToWorld(m_ExpelPoint, Map);
            if (m_NPCOpponent != null && !m_NPCOpponent.Deleted)
                m_NPCOpponent.Delete();
            m_ActiveUser = null;
            m_ActiveUserMount = null;
            m_NPCOpponent = null;
            m_FightType = ArenaFightType.None;
        }

        private void RegisterWin()
        {
            if (m_ActiveUser== null || m_ActiveUser.Deleted)
                return;
            m_ActiveUser.SendMessage("Congratulations, you win and score a point!");
            TimeSpan span = DateTime.Now - m_StartTime;
            int seconds = (int)span.TotalSeconds;
            if (m_Broadcast)
                World.Broadcast(0x35, true, "{0} defeated {1} in {2} seconds.", new object[] { m_ActiveUser.Name, m_OpponentType.Name, seconds.ToString() });
            else
                PublicOverheadMessage(MessageType.Regular, 68, false, String.Format("{0} defeated {1} in {2} seconds.", new object[] { m_ActiveUser.Name, m_OpponentType.Name, seconds.ToString() }));
            if (m_ScoreTable.ContainsKey(m_ActiveUser))
                m_ScoreTable[m_ActiveUser] = (int)m_ScoreTable[m_ActiveUser] + 1;
            else
                m_ScoreTable.Add(m_ActiveUser, 1);
        }

        private void RegisterLose()
        {
            if (m_ActiveUser == null || m_ActiveUser.Deleted)
                return;
            m_ActiveUser.SendMessage("You lose. Try your luck next time.");
            TimeSpan span = DateTime.Now - m_StartTime;
            int seconds = (int)span.TotalSeconds;
            if (m_Broadcast)
                World.Broadcast(0x35, true, "{0} is defeated by a {1} in {2} seconds.", new object[] { m_ActiveUser.Name, m_OpponentType.Name, seconds.ToString() });
            else
                PublicOverheadMessage(MessageType.Regular, 68, false, String.Format("{0} is defeated by a {1} in {2} seconds.", new object[] { m_ActiveUser.Name, m_OpponentType.Name, seconds.ToString() }));
            if (m_ScoreTable.ContainsKey(m_ActiveUser))
                m_ScoreTable[m_ActiveUser] = (int)m_ScoreTable[m_ActiveUser] - 1;
            else
                m_ScoreTable.Add(m_ActiveUser, -1);
        }

        private bool PlayerWin()
        {
            if (m_ActiveUser!=null && IsInsideArena(m_ActiveUser) && m_NPCOpponent.Deleted)
                return true;
            else
                return false;
        }

        private bool PlayerLose()
        {
            if (m_ActiveUser==null || !m_ActiveUser.Alive || !IsInsideArena(m_ActiveUser))
                return true;
            else
                return false;
        }

        private bool IsInsideArena(Mobile from)
        {
            Point3D userLocation = from.Location;
            if (m_ActiveUser.Map == this.Map
                && userLocation.X >= m_X1Y1.X && userLocation.X <= m_X2Y2.X
                && userLocation.Y >= m_X1Y1.Y && userLocation.Y <= m_X2Y2.Y
                && userLocation.Z >= m_StartPointPlayer.Z-10 && userLocation.Z <=m_StartPointPlayer.Z+10)
                return true;
            else
                return false;
        }

        private bool IsInsideArena(Item item)
        {
            Point3D userLocation = item.Location;
            if (m_ActiveUser.Map == this.Map
                && userLocation.X >= m_X1Y1.X && userLocation.X <= m_X2Y2.X
                && userLocation.Y >= m_X1Y1.Y && userLocation.Y <= m_X2Y2.Y
                && userLocation.Z >= m_StartPointPlayer.Z - 10 && userLocation.Z <= m_StartPointPlayer.Z + 10)
                return true;
            else
                return false;
        }

        private class ManageTimer : Timer
        {
            private ArenaControlStone m_Arena;
            public ManageTimer(ArenaControlStone arena)
                : base(TimeSpan.FromSeconds(ArenaMangementIntervalInSec), TimeSpan.FromSeconds(ArenaMangementIntervalInSec))
            {
                m_Arena = arena;
            }
            protected override void OnTick()
            {
                if (m_Arena.PlayerWin())
                {
                    m_Arena.RegisterWin();
                    m_Arena.CleanUp();
                }

                if (m_Arena.PlayerLose())
                {
                    m_Arena.RegisterLose();
                    m_Arena.CleanUp();
                }
            }
        }
   }
}