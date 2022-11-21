// 	RunUO 2.0 SVN Version	
using Server;
using System;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Multis;
using Server.Engines.Plants;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;


namespace Server.Items
{

    public class WaterSprinkler : Item, ISecurable
    {
        private int m_fill;
        private SecureLevel m_Level;

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Fill
        {
            get { return m_fill; }
            set { m_fill = value; }
        }

        [Constructable]
        public WaterSprinkler()
            : base(0x14E7)
        {
            Weight = 1.0;
            Name = "Water Sprinkler";
            Hue = 195;
        }

        public WaterSprinkler(Serial serial)
            : base(serial)
        {
        }

        public bool CanBeWatered(PlantItem plant)
        {
            return plant.PlantStatus < PlantStatus.DecorativePlant && plant.PlantSystem.Water <= 1;
        }


        public override void OnDoubleClick(Mobile from)
        {
            BaseHouse house = BaseHouse.FindHouseAt(from);
            if (house == null)
                from.SendLocalizedMessage(1005525);//That is not in your house
            else if (this.Movable)
                from.SendMessage("This must be locked down to use!");
            else
            {
                if (this.IsAccessibleTo(from))
                {
                    if (m_fill != 6)
                        from.SendMessage("You must completely fill this before you can use it!");
                    else
                    {
                        Point3D p = new Point3D(this.Location);
                        Map map = this.Map;
                        IPooledEnumerable eable = map.GetItemsInRange(p, 18);
                        bool found = false;


                        foreach (Item item in eable)
                        {
                            if (house.IsInside(item) && item is PlantItem && item.IsLockedDown)
                            {
                                PlantItem plant = (PlantItem)item;
                                if (CanBeWatered(plant))
                                {
                                    plant.PlantSystem.Water = 2;
                                    found = true;
                                }
                            }
                        }
                        if (found)
                        {
                            from.SendMessage("Your dry plants have been watered.");
                            from.PlaySound(0x12);
                            m_fill = 0;
                        }
                        else
                            from.SendMessage("You have no plants that need watering!");
                    }
                }
                else
                    from.SendMessage("You may not access this!");
            }
        }



        public void Pour(Mobile from, Item item)
        {
            if (item is BaseBeverage)
            {
                BaseBeverage beverage = (BaseBeverage)item;
                if (beverage.IsEmpty || !beverage.Pourable || beverage.Content != BeverageType.Water)
                {
                    from.SendMessage("You can only put water in here!");
                    return;
                }

                if (m_fill < 6)
                {
                    m_fill++;
                    beverage.Quantity--;
                    from.PlaySound(0x4E);

                    if (m_fill == 6)
                        from.SendMessage("You completely fill the sprinkler.");
                    else
                        from.SendMessage("You dump some water into the sprinkler");
                }
                else
                    from.SendMessage("It's already full.");


            }
        }


        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            SetSecureLevelEntry.AddTo(from, this, list);
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            writer.Write(m_fill);
            writer.Write((int)m_Level);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_fill = reader.ReadInt();
            m_Level = (SecureLevel)reader.ReadInt();
        }

    }


    public class SprinklerContainer : BaseBeverage
    {

        public override int MaxQuantity { get { return 4; } }

        public override int ComputeItemID() { return 0x142B; }

        [Constructable]
        public SprinklerContainer()
        {
            Weight = 5.0;
            Name = "Sprinkler Filler";
        }

        [Constructable]
        public SprinklerContainer(BeverageType type)
            : base(type)
        {
            Weight = 5.0;
        }

        public SprinklerContainer(Serial serial)
            : base(serial)
        {

        }


        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            else if (Quantity > 0)
            {
                from.SendMessage("Select the sprinkler you wish to fill");
                from.Target = new SprinklerTarget(this);
            }
            else
            {
                from.BeginTarget(-1, true, TargetFlags.None, new TargetCallback(Fill_OnTarget));
                SendLocalizedMessageTo(from, 500837); // Fill from what?
            }

        }

        private class SprinklerTarget : Target
        {
            private SprinklerContainer m_cont;

            public SprinklerTarget(SprinklerContainer cont)
                : base(1, false, TargetFlags.None)
            {
                m_cont = cont;
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (target is WaterSprinkler)
                {
                    WaterSprinkler sprink = (WaterSprinkler)target;
                    sprink.Pour(from, m_cont);
                }
                else
                    from.SendMessage("This can only be used on water sprinklers!");
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
            writer.Write((int)Quantity);
            writer.Write((int)Content);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            Quantity = reader.ReadInt();
            Content = (BeverageType)reader.ReadInt();

        }
    }


}
