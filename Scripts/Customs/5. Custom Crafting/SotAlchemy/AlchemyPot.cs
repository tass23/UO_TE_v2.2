using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class AlchemyPot : Container
    {
        [Constructable]
        public AlchemyPot() : base(0xE77)
        {
            Name = "Cauldron";
            Weight = 10.0;
        }

        private bool m_Water;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Water
        {
            get { return m_Water; }
            set { m_Water = value; InvalidateProperties(); }
        }

        private bool m_Ignited;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Ignited
        {
            get { return m_Ignited; }
            set { m_Ignited = value; InvalidateProperties(); }
        }

        private int m_Temperature;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Temperature
        {
            get { return m_Temperature; }
            set
            {
                m_Temperature = value;
                if (m_Temperature < 0)
                    m_Temperature = 0;
                InvalidateProperties();
            }
        }

        private int m_Fuel;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Fuel
        {
            get { return m_Fuel; }
            set
            {
                m_Fuel = value;
                if (m_Fuel == 0 && m_Ignited)
                    m_Ignited = false;
                else if (m_Fuel < 0)
                    m_Fuel = 0;
                InvalidateProperties();
            }
        }

        private int m_Seconds;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Seconds
        {
            get { return m_Seconds; }
            set
            {
                m_Seconds = value;
                InvalidateProperties();
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (!Ignited && !Water && dropped is BaseBeverage)
            {
                BaseBeverage bb = dropped as BaseBeverage;
                if (bb.Quantity > 0 && bb.Content == BeverageType.Water)
                {
                    Water = true;
                    bb.Quantity -= 1;
                    WaterForPot wfp = new WaterForPot();
                    AddItem(wfp);
                    wfp.Movable = false;
                    return false;
                }
            }
            else if (Ignited && CheckFuel(dropped))
                return true;
            else if (dropped is BluePowder)
            {
                Temperature -= dropped.Amount * 10;
                dropped.Delete();
                return true;
            }

            return base.OnDragDrop(from, dropped);
        }

        public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
        {
            if (!Ignited && !Water && item is BaseBeverage)
            {
                BaseBeverage bb = item as BaseBeverage;
                if (bb.Quantity > 0 && bb.Content == BeverageType.Water)
                {
                    Water = true;
                    bb.Quantity -= 1;
                    WaterForPot wfp = new WaterForPot();
                    AddItem(wfp);
                    wfp.Movable = false;
                    return false;
                }
            }
            else if (Ignited && CheckFuel(item))
                return true;
            else if (item is BluePowder)
            {
                Temperature -= item.Amount * 10;
                item.Delete();
                return true;
            }

            return base.OnDragDropInto(from, item, p);
        }

        public bool CheckFuel(Item item)
        {
            if (item is Log)
            {
                Fuel += item.Amount * 2;
                item.Delete();
                return true;
            }
            else return false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
                from.SendMessage("You can't use it in your backpack");
            else if (from.InRange(this.GetWorldLocation(), 2))
            {
                DisplayTo(from);
                from.CloseGump(typeof(AlchemyPotGump));
                from.SendGump(new AlchemyPotGump(from, this));
            }
            else
                from.SendMessage("The cauldron is too far");
        }
        
        public AlchemyPot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

            writer.Write((bool)Water);
            writer.Write((int)Temperature);
            writer.Write((int)Seconds);
            writer.Write((int)Fuel);
            writer.Write((bool)Ignited);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            m_Water = reader.ReadBool();
            m_Temperature = reader.ReadInt();
            m_Seconds = reader.ReadInt();
            m_Fuel = reader.ReadInt();
            m_Ignited = reader.ReadBool();
		}

        private class AlchemyPotTimer : Timer
        {
            private Mobile m_From;
            private AlchemyPot m_AlchemyPot;
            private WaterForPot m_WaterForPot;

            public AlchemyPotTimer(Mobile from, AlchemyPot ap, WaterForPot wfp)
                : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
            {
                Priority = TimerPriority.EveryTick;
                m_From = from;
                m_AlchemyPot = ap;
                m_WaterForPot = wfp;
            }

            protected override void OnTick()
            {
                if (m_AlchemyPot != null && !m_AlchemyPot.Deleted)
                {
                    m_AlchemyPot.Seconds += 1;
                    if (m_AlchemyPot.Fuel > 0)
                    {
                        m_AlchemyPot.Temperature += 10;
                        m_AlchemyPot.Fuel -= 1;
                    }
                    else if (m_AlchemyPot.Temperature > 0)
                        m_AlchemyPot.Temperature -= 5;
                    else if (m_AlchemyPot.Temperature == 0)
                    {
                        m_AlchemyPot.Ignited = false;
                        m_AlchemyPot.Movable = true;
                        m_AlchemyPot.Seconds = 0;
                        Stop();
                    }                    
                    List<Item> ItemsInPot = new List<Item>();
                    List<Item> ToDelete = new List<Item>();
                    foreach (Item item in m_AlchemyPot.Items)
                    {
                        if (!(item is WaterForPot))
                        {
                            ItemsInPot.Add(item);
                            if (item is BaseReagent)
                            {
                                m_From.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                                m_From.PlaySound(0x1E3);
                                ToDelete.Add(item);
                            }
                        }
                    }
                    m_WaterForPot.InfoArray.Add(new AlchemyInfo(m_AlchemyPot.Seconds, m_AlchemyPot.Temperature, ItemsInPot));
                    foreach (Item item in ToDelete)
                        item.Delete();
                    m_From.CloseGump(typeof(AlchemyPotGump));
                    if (m_From.InRange(m_AlchemyPot, 3))
                        m_From.SendGump(new AlchemyPotGump(m_From, m_AlchemyPot));
                }
                else Stop();
            }
        }

        private class AlchemyPotGump : Gump
        {
            private AlchemyPot m_AlchemyPot;
            private Timer m_AlchemyPotTimer;
            private WaterForPot m_WaterForPot;

            public AlchemyPotGump(Mobile from, AlchemyPot ap)
                : base(500, 200)
            {
                m_AlchemyPot = ap;

                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                AddBackground(0, 0, 200, 180, 5054);

                this.AddLabel(10, 10, 0x480, "Fuel: " + m_AlchemyPot.Fuel.ToString());
                this.AddLabel(10, 40, 0x480, "Temperature: " + m_AlchemyPot.Temperature.ToString());
                this.AddLabel(10, 70, 0x480, "Seconds: " + m_AlchemyPot.Seconds.ToString());
                this.AddLabel(10, 100, 0x480, "Status: ");
                if (m_AlchemyPot.Ignited)
                    this.AddLabel(60, 100, 0x480, "ON");
                else
                {
                    this.AddLabel(60, 100, 0x480, "OFF");
                    this.AddButton(10, 150, 4005, 4005, -1, GumpButtonType.Reply, 0);
                    this.AddLabel(45, 150, 0x480, "Ignite");
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                Mobile from = sender.Mobile;
                if (info.ButtonID == -1)
                {
                    if (!m_AlchemyPot.Ignited)
                    {
                        if (!CheckWater())
                        {
                            from.SendMessage("There isn't any water in the cauldron");
                            return;
                        }
                        LoadFuel();
                        if (m_AlchemyPot.Fuel > 0)
                        {
                            m_AlchemyPot.Ignited = true;
                            m_AlchemyPot.Movable = false;
                            m_AlchemyPotTimer = new AlchemyPotTimer(from, m_AlchemyPot, m_WaterForPot);
                            m_AlchemyPotTimer.Start();
                            from.SendMessage("You have ignited the cauldron");
                        }
                        else
                            from.SendMessage("There aren't any logs to ignite the cauldron");
                    }
                    else
                        from.SendMessage("The cauldron is already ignited");
                }
            }

            private void LoadFuel()
            {
                ArrayList FuelList = new ArrayList();
                foreach (Item item in m_AlchemyPot.Items)
                {
                    if (item is Log)
                    {
                        m_AlchemyPot.Fuel += item.Amount * 2;
                        FuelList.Add(item);
                    }
                }

                foreach (Item item in FuelList)
                    item.Delete();
            }

            private bool CheckWater()
            {
                foreach (Item item in m_AlchemyPot.Items)
                {
                    if (item is WaterForPot)
                    {
                        m_WaterForPot = item as WaterForPot;
                        return true;
                    }
                }
                return false;
            }
        }
    }
}