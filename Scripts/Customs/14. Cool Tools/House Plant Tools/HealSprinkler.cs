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

    public class HealSprinkler : Item, ISecurable
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
        public HealSprinkler()
            : base(0x14E7)
        {
            Weight = 1.0;
            Name = "Heal Sprinkler";
            Hue = 45;
        }

        public HealSprinkler(Serial serial)
            : base(serial)
        {
        }

        public bool CanBeHealed(PlantItem plant)
        {
            return plant.PlantStatus < PlantStatus.DecorativePlant && plant.PlantSystem.Disease + plant.PlantSystem.Poison > plant.PlantSystem.HealPotion && plant.PlantSystem.HealPotion < 2;
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
                    Point3D p = new Point3D(this.Location);
                    Map map = this.Map;
                    IPooledEnumerable eable = map.GetItemsInRange(p, 18);
                    bool found = false;


                    foreach (Item item in eable)
                    {
                        if (house.IsInside(item) && item is PlantItem && item.IsLockedDown)
                        {
                            PlantItem plant = (PlantItem)item;

                            if (CanBeHealed(plant))
                            {
                                plant.PlantSystem.HealPotion += (((plant.PlantSystem.Disease + plant.PlantSystem.Poison) % 3) - plant.PlantSystem.HealPotion) + 1;
                                found = true;
                            }
                        }
                    }
                    if (found)
                    {
                        from.SendMessage("Your ill plants have been healed.");
                        from.PlaySound(0x12);
                        m_fill = 0;
                    }
                    else
                        from.SendMessage("You have no plants that need healing!");

                }
                else
                    from.SendMessage("You may not access this!");
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

}