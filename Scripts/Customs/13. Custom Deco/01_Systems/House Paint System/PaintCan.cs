using System;
using Server;
using Server.Mobiles;
using Server.Multis;
using Server.Targeting;

namespace Server.Items
{
    public class PaintCan : Item
    {
        private bool m_Redyable;
        private int m_DyedHue;
        private string Brand = "Lokai's";
        public override int LabelNumber { get { return 1016211; } }
        private int m_Uses;

        public virtual CustomHuePicker CustomHuePicker { get { return null; } }

        public virtual bool AllowRepaint { get { return false; } }
        public virtual bool AllowHouse { get { return false; } }
        public virtual bool UsesCharges { get { return true; } }

        public virtual bool AllowWood { get { return false; } } //Wood walls and doors
        public virtual bool AllowStone { get { return false; } } //Stone walls and doors
        public virtual bool AllowMarble { get { return false; } } //Marble walls and doors
        public virtual bool AllowPlaster { get { return false; } } //Plaster and clay walls and doors
        public virtual bool AllowSandstone { get { return false; } } //Sandstone walls and doors
        public virtual bool AllowOther { get { return false; } } //Hide, Paper, Bamboo or Rattan walls and doors

        public override bool DisplayWeight { get { return false; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write((int)m_Uses);
            writer.Write((bool)m_Redyable);
            writer.Write((int)m_DyedHue);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Uses = reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
                        m_Redyable = reader.ReadBool();
                        m_DyedHue = reader.ReadInt();

                        break;
                    }
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add(Brand);
            list.Add(Name);
            if (UsesCharges)
            {
                if (m_Uses > 0) list.Add("Uses left: {0}", m_Uses);
                else list.Add("** Empty **");
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Uses { get { return m_Uses; } set { m_Uses = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Redyable { get { return m_Redyable; } set { m_Redyable = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DyedHue { get { return m_DyedHue; } set { if (m_Redyable) { m_DyedHue = value; Hue = value; } } }

        [Constructable]
        public PaintCan()
            : this(50)
        {
        }

        [Constructable]
        public PaintCan(int uses)
            : base(0xFAB)
        {
            m_Uses = uses;
            Weight = 6.0;
            m_Redyable = true;
        }

        public PaintCan(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(this.GetWorldLocation(), 1))
            {
                from.SendMessage("Target what you want to paint.");
                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
        }

        private class InternalTarget : Target
        {
            private PaintCan m_Can;

            public InternalTarget(PaintCan can)
                : base(3, false, TargetFlags.None)
            {
                m_Can = can;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                bool GM = from.AccessLevel >= AccessLevel.GameMaster;

                IPoint3D p3d;
                int id = 0;
                PaintedItem pi = null;
                BaseHouse house = null;
                if (m_Can.UsesCharges && m_Can.Uses < 1)
                {
                    from.SendMessage("Your paint can is empty.");
                    return;
                }
                try
                {
                    p3d = targeted as IPoint3D;
                    house = Multis.BaseHouse.FindHouseAt(new Point3D(p3d), from.Map, p3d.Z);
                }
                catch
                {
                    from.SendMessage("TEMP: Unable to find house.");
                }
                finally
                {
                    if ((house != null && from == house.Owner) || GM)
                    {
                        if (targeted is PaintedItem)
                        {
                            pi = targeted as PaintedItem;
                            id = pi.ItemID;
                            if (m_Can.CanPaint(id))
                                if (m_Can.AllowRepaint)
                                {
                                    if (!GM && house.LockDowns.Contains(pi)) house.LockDowns.Remove(pi);

                                    if (GM || house == BaseHouse.FindHouseAt(pi))
                                    {
                                        if (!GM) house.LockDown(from, pi);
                                        pi.Hue = m_Can.DyedHue;
                                        if (m_Can.UsesCharges) m_Can.Uses--;
                                        from.PlaySound(0x23E);
                                        //from.SendMessage("TEMP: Successfully painted over PaintedItem.");
                                    }
                                    else
                                        from.SendMessage("You can only paint your house.");
                                }
                                else
                                    from.SendMessage("This surface may not be repainted.");
                            else
                                from.SendMessage("Unable to paint that using this type of paint.");
                        }
                        else
                            if (targeted is BaseMulti)
                            {
                                BaseMulti target = targeted as BaseMulti;
                                id = target.ItemID;

                                if (m_Can.AllowHouse && m_Can.CanPaint(id))
                                {
                                    pi = new PaintedItem(target.ItemID);
                                    pi.Hue = m_Can.DyedHue;
                                    pi.MoveToWorld(target.Location, target.Map);
                                    pi.X = target.X;
                                    pi.Y = target.Y;
                                    pi.Z = target.Z + 1;
                                    pi.Movable = true;
                                    if (!GM) house.LockDown(from, pi);
                                    from.PlaySound(0x23E);
                                    if (m_Can.UsesCharges) m_Can.Uses--;
                                    //from.SendMessage("TEMP: Successfully painted over BaseMulti.");
                                    target.Delete();
                                }
                                else
                                    from.SendMessage("Unable to paint that using this type of paint.");
                            }
                            else
                                if (targeted is StaticTarget)
                                {
                                    StaticTarget stat = targeted as StaticTarget;
                                    id = stat.ItemID;
                                    if (m_Can.CanPaint(id) && from.Map != null)
                                    {
                                        IPoint3D p = targeted as IPoint3D;

                                        if (p != null)
                                        {
                                            pi = new PaintedItem(stat.ItemID);

                                            if (p is Item)
                                            {
                                                p = ((Item)p).GetWorldTop();
                                                //from.SendMessage("TEMP: Set Point3D to ((Item)p).GetWorldTop().");
                                            }
                                            else
                                            {
                                                p = new Point3D(stat.X, stat.Y, stat.Z - pi.ItemData.CalcHeight);
                                                //from.SendMessage("TEMP: IPoint3D was not an Item!");
                                            }
                                            pi.Hue = m_Can.DyedHue;
                                            if (GM) pi.Movable = false;
                                            else pi.Movable = true;

                                            if (!pi.Deleted)
                                            {
                                                pi.MoveToWorld(new Point3D(p), from.Map);
                                                if (!GM) house.LockDown(from, pi);
                                                //from.SendMessage("TEMP: Successfully painted over StaticTarget.");
                                                from.PlaySound(0x23E);
                                                if (m_Can.UsesCharges) m_Can.Uses--;
                                            }
                                            else
                                                from.SendMessage("The item you were about to paint was deleted?!");
                                        }
                                        else
                                            from.SendMessage("Huh?! Where did it go??");
                                    }
                                    else
                                        from.SendMessage("Unable to paint that using this type of paint.");
                                }
                                else
                                    if (GM && targeted is Item)
                                    {
                                        Item item = targeted as Item;
                                        id = item.ItemID;
                                        if (m_Can.CanPaint(id) && from.Map != null)
                                        {
                                            pi = new PaintedItem(item.ItemID);
                                            pi.Hue = m_Can.DyedHue;
                                            pi.Movable = false;
                                            pi.MoveToWorld(item.Location, item.Map);
                                            from.PlaySound(0x23E);
                                            if (m_Can.UsesCharges) m_Can.Uses--;
                                            //from.SendMessage("TEMP: Successfully painted over Item.");
                                        }
                                        else
                                            from.SendMessage("Unable to paint that item using this type of paint.", id.ToString());
                                    }
                                    else
                                        from.SendMessage("Failed to target a paintable item.");
                    }
                    else
                        from.SendMessage("You must be standing in, and targeting a house you own.");
                }
            }
        }

        private bool IsIn(int num, int low, int high)
        {
            return Utility.NumberBetween((double)num, low, high, 1.0);
        }

        private bool IsIn(int num, int val)
        {
            return (num == val);
        }

        public bool CanPaint(int itemID)
        {
            int num = itemID;
            if (IsIn(num, 6, 25) || IsIn(num, 144, 194) || IsIn(num, 411, 419) ||
                IsIn(num, 421, 432) || IsIn(num, 541, 555) || IsIn(num, 820, 851) ||
                IsIn(num, 947, 950) || IsIn(num, 1701, 1732) || IsIn(num, 1749, 1780) ||
                IsIn(num, 1848, 1864) || IsIn(num, 1971, 1974) || IsIn(num, 1981, 1990) ||
                IsIn(num, 1993, 2000) || IsIn(num, 2101, 2120) || IsIn(num, 2140, 2165) ||
                IsIn(num, 2167, 2250) || IsIn(num, 8584, 8668) || IsIn(num, 9343, 9368) ||
                IsIn(num, 10003, 10015) || IsIn(num, 10067, 10082) || IsIn(num, 10335, 10349) ||
                IsIn(num, 10640, 10655) || IsIn(num, 10757, 10780) || IsIn(num, 11540, 11549) ||
                IsIn(num, 11574, 11593) || IsIn(num, 11771, 11774)) return AllowWood;

            if (IsIn(num, 26, 143) || IsIn(num, 197, 247) || IsIn(num, 463, 491) ||
                IsIn(num, 577, 586) || IsIn(num, 711, 729) || IsIn(num, 761, 819) ||
                IsIn(num, 852, 869) || IsIn(num, 951, 1026) || IsIn(num, 1822, 1823) ||
                IsIn(num, 1846, 1847) || IsIn(num, 1865, 1896) || IsIn(num, 1922, 1970) ||
                IsIn(num, 2006, 2016) || IsIn(num, 2100) || IsIn(num, 2166) ||
                IsIn(num, 2325, 2328) || IsIn(num, 8669, 8680) || IsIn(num, 9535, 9555) ||
                IsIn(num, 10656, 10687)) return AllowStone;

            if (IsIn(num, 248, 294) || IsIn(num, 657, 704) || IsIn(num, 1080, 1107) ||
                IsIn(num, 1801, 1821) || IsIn(num, 9484, 9518) || IsIn(num, 9532, 9534) ||
                IsIn(num, 9936, 9945) || IsIn(num, 11130, 11136) || IsIn(num, 11177, 11214) ||
                IsIn(num, 11503, 11512) || IsIn(num, 11619, 11630) || IsIn(num, 11711, 11728)) return AllowMarble;

            if (IsIn(num, 295, 343) || IsIn(num, 511, 524) || IsIn(num, 895, 928) ||
                IsIn(num, 9369, 9388) || IsIn(num, 9519, 9531) || IsIn(num, 10552, 10587) ||
                IsIn(num, 10716, 10748) || IsIn(num, 10800, 10811)) return AllowPlaster;

            if (IsIn(num,344,410) || IsIn(num,420) || IsIn(num,433,436) || 
                IsIn(num,458) || IsIn(num,492,495) || IsIn(num,588,603) || 
                IsIn(num,1897,1921) ) return AllowSandstone;

            if (IsIn(num, 437, 457) || IsIn(num, 496, 505) || IsIn(num, 527, 537) ||
                IsIn(num, 556, 569) || IsIn(num, 734, 748) || IsIn(num, 872, 881) ||
                IsIn(num, 1685, 1700) || IsIn(num, 10376, 10403)) return AllowOther;

            return false;
        }
    }
}