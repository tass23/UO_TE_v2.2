using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Engines.SAMiniChamps;

namespace Server.Engines.SAMiniChamps
{
    public class MiniChampSpawnController : Item
    {
        bool m_Active;

        private ArrayList m_DungeonsSpawn;

        private TimeSpan m_ExpireDelay;
        private TimeSpan m_RestartDelay;

        private TimeSpan m_RandomizeDelay;

        private int m_ActiveAltars;

        private MiniRandomizeTimer m_Timer;

        public struct SpawnRecord
        {
            public int type, x, y, z;

            public SpawnRecord(int type, int x, int y, int z)
            {
                this.type = type;
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        private SpawnRecord[] m_Dungeons = new SpawnRecord[]
		{
			new SpawnRecord( (int)MiniChampType.SecretGarden, 435, 701, 48 ),
			new SpawnRecord( (int)MiniChampType.StygianDragonLair, 887, 275, 22 ), 
			new SpawnRecord( (int)MiniChampType.CrimsonVeins, 975, 161, 9 ), 
			new SpawnRecord( (int)MiniChampType.AbyssalLair, 990, 329, 30 ), 
			new SpawnRecord( (int)MiniChampType.FireTemple, 548, 761, -72 ), 
			new SpawnRecord( (int)MiniChampType.LandsOfTheLich, 534, 660, 28 ),
        	new SpawnRecord( (int)MiniChampType.SkeletalDragon, 676, 831, -89 ),
        	new SpawnRecord( (int)MiniChampType.EnslavedGoblins, 571, 805, -20 ),
        	new SpawnRecord( (int)MiniChampType.LavaCaldera, 581, 898, -53 ),
        	new SpawnRecord( (int)MiniChampType.PassageOfTears, 695, 587, 5 ),
        	new SpawnRecord( (int)MiniChampType.ClanChitter, 979, 493, 8 ),
        	new SpawnRecord( (int)MiniChampType.ClanRibbon, 916, 500, 2 ),
        	new SpawnRecord( (int)MiniChampType.ClanScratch, 950, 553, 4 ),
		};

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active
        {
            get
            {
                return m_Active;
            }
            set
            {
                if (value)
                    Start();
                else
                    Stop();

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ActiveAltars
        {
            get
            {
                return m_ActiveAltars;
            }
            set
            {
                m_ActiveAltars = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan ExpireDelay
        {
            get
            {
                return m_ExpireDelay;
            }
            set
            {
                m_ExpireDelay = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan RestartDelay
        {
            get
            {
                return m_RestartDelay;
            }
            set
            {
                m_RestartDelay = value;

            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan RandomizeDelay
        {
            get
            {
                return m_RandomizeDelay;
            }
            set
            {
                m_RandomizeDelay = value;
            }
        }

        [Constructable]
        public MiniChampSpawnController()
            : base(0x1B7A)
        {
            if (Check())
            {
                World.Broadcast(0x35, true, "Another mini champ spawn controller exist in the world !");
                Delete();
                return;
            }

            Visible = false;
            Movable = false;
            Name = "mini spawn controller";

            m_Active = false;

            m_DungeonsSpawn = new ArrayList();

            m_ActiveAltars = 13;

            m_ExpireDelay = TimeSpan.FromMinutes(10.0);
            m_RestartDelay = TimeSpan.FromMinutes(5.0);

            m_RandomizeDelay = TimeSpan.FromHours(72.0);

            DeleteAll();
            Generate();

            World.Broadcast(0x35, true, "Mini Champ Spawn generation complete. Old spawns removed.");
        }

        private bool Check()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is MiniChampSpawnController && !item.Deleted && item != this)
                    return true;
            }

            return false;
        }

        private void DeleteAll()
        {
            ArrayList list = new ArrayList();

            foreach (Item item in World.Items.Values)
            {
                if (item is MiniChampSpawn && !item.Deleted)
                    list.Add(item);
            }

            foreach (MiniChampSpawn cs in list)
            {
                cs.Delete();
            }
        }

        private MiniChampSpawn CreateAltar(SpawnRecord r, Map m, bool restartdisable)
        {
            MiniChampSpawn cs = new MiniChampSpawn();

            Point3D loc = new Point3D(r.x, r.y, r.z);

            if (r.type == 0xff)
            {
                cs.RandomizeType = true;

                switch (Utility.Random(5))
                {
                    case 0: cs.Type = MiniChampType.SecretGarden; break;
                    case 1: cs.Type = MiniChampType.StygianDragonLair; break;
                    case 2: cs.Type = MiniChampType.CrimsonVeins; break;
                    case 3: cs.Type = MiniChampType.AbyssalLair; break;
                    case 4: cs.Type = MiniChampType.FireTemple; break;
                }
            }
            else
            {
                cs.RandomizeType = false;
                cs.Type = (MiniChampType)r.type;
            }

            cs.MoveToWorld(loc, m);

            return cs;
        }

        private void Generate()
        {
            int i = 0;

            for (i = 0; i < m_Dungeons.Length; i++)
            {
                MiniChampSpawn cs = CreateAltar(m_Dungeons[i], Map.TerMur, true);

                m_DungeonsSpawn.Add(cs);
            }
        }

        public MiniChampSpawnController(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_Active)
            {
                list.Add(1060742); // active
            }
            else
            {
                list.Add(1060743); // inactive
            }
        }

        public override void OnDelete()
        {
            Stop();

            base.OnDelete();
        }

        public void Start()
        {
            if (m_Active || Deleted) return;

            m_Active = true;

            if (m_Timer == null)
            {
                m_Timer = new MiniRandomizeTimer(this, m_RandomizeDelay);
            }

            //Randomize(m_DungeonsSpawn);

            m_Timer.Start();
        }

        public void Stop()
        {
            if (!m_Active || Deleted) return;

            m_Active = false;

            if (m_Timer != null) m_Timer.Stop();
        }

        public void Randomize(ArrayList list)
        {
            foreach (MiniChampSpawn cs in list)
            {
                if (!cs.Deleted && cs.Active)
                    cs.Active = false;
            }

            for (int i = 0; i < m_ActiveAltars; i++)
            {
                int trynum = 0;

                while (trynum < 9)
                {
                    int index = Utility.Random(list.Count);

                    if (!((MiniChampSpawn)list[index]).Active)
                    {
                        ((MiniChampSpawn)list[index]).Active = true;
                        break;
                    }

                    trynum++;
                }
            }
        }

        public void Slice()
        {
            if (!m_Active || Deleted)
            {
                if (m_Timer != null) m_Timer.Stop();
                return;
            }

            m_Timer.Start();
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendGump(new PropertiesGump(from, this));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

            writer.Write(m_Active);

            writer.WriteItemList(m_DungeonsSpawn);

            writer.Write(m_RandomizeDelay);

            writer.Write(m_ActiveAltars);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Active = reader.ReadBool();

                        m_DungeonsSpawn = reader.ReadItemList();

                        m_RandomizeDelay = reader.ReadTimeSpan();

                        m_ActiveAltars = reader.ReadInt();

                        m_ExpireDelay = TimeSpan.FromMinutes(10.0);
                        m_RestartDelay = TimeSpan.FromMinutes(5.0);

                        if (m_Active)
                        {
                            m_Timer = new MiniRandomizeTimer(this, m_RandomizeDelay);

                            m_Timer.Start();
                        }
                        break;
                    }
            }
        }
    }
}