using System;using Server;using Server.Mobiles;
using System.Collections;using System.Collections.Generic;
using Server.Network;using Server.Misc;using Server.Gumps;
using Server.Items;
using Server.Multis;
using Server.Targeting;
namespace Server.Items
{
    public class YardTreeMulti : Item
    {
        public ArrayList m_Components;
        public Mobile m_Placer;
        public int m_Value = 0;
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Placer
        {
            get { return m_Placer; }
            set { m_Placer = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Price
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        //PIECES\\		
        private class TreePiece : Item
        {
            YardTreeMulti IsPartOf;
            public TreePiece(int itemID, String name, YardTreeMulti ThisTree)
                : base(itemID)
            {
                Movable = false;
                Name = name;
                IsPartOf = ThisTree;
            }
            public TreePiece(Serial serial)
                : base(serial)
            {
            }
            public override void OnAfterDelete()
            {
                if (IsPartOf != null)
                    IsPartOf.OnAfterDelete();
                else
                    base.OnAfterDelete();
            }
            public override void OnDoubleClick(Mobile from)
            {
                if (IsPartOf != null)
                    IsPartOf.OnDoubleClick(from);
                else
                    base.OnDoubleClick(from);
            }
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0); // version
                writer.Write(IsPartOf);
            }
            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();
                switch (version)
                {
                    case 0:
                    {
                        IsPartOf = reader.ReadItem() as YardTreeMulti;
                        break;
                    }
                }
            }
        }
        //END PIECES\\
        [Constructable]
        public YardTreeMulti(Mobile from, string n, int price, int id, int lowrange, int highrange, Point3D loc)
        {
            m_Value = price;
            m_Placer = from;
            string name = "";
            name = from.Name + "'s " + n;
            Name = name;
            Movable = false;
            MoveToWorld(loc, from.Map);

            m_Components = new ArrayList();
            ItemID = id;
            int lr = lowrange;
            int hr = highrange;

            while (lr > 0)
            {
                AddTreePiece(-lr, +lr, 0, id - lr, name, loc);
                lr--;
            }
            while (hr > 0)
            {
                AddTreePiece(+hr, -hr, 0, id + hr, name, loc);
                hr--;
            }
            m_Components.Add(this);
        }
        private void AddTreePiece(int x, int y, int z, int itemID, string name, Point3D loc)
        {
            PlaceAndAdd(x, y, z, new TreePiece(itemID, name, this), loc);
        }

        private void PlaceAndAdd(int x, int y, int z, Item item, Point3D loc)
        {
            item.MoveToWorld(new Point3D(loc.X + x, loc.Y + y, loc.Z + z), m_Placer.Map);
            m_Components.Add(item);
        }

        public YardTreeMulti(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write(m_Placer);
            writer.Write(m_Value);
            writer.WriteItemList(m_Components);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            switch (version)
            {
                case 0:
                {
                    m_Placer = reader.ReadMobile();
                    m_Value = reader.ReadInt();
                    m_Components = reader.ReadItemList();
                    break;
                }
            }
        }
        public override void OnAfterDelete()
        {
            for (int i = 0; i < m_Components.Count; ++i)
                ((Item)m_Components[i]).Delete();
        }
        public override void OnDoubleClick(Mobile from)
        {
            if (m_Placer == null || m_Placer.Deleted)
                m_Placer = from;
            if (from.InRange(this.GetWorldLocation(), 10))
            {
                if (m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster)
                {
                    Container c = m_Placer.Backpack;
                    if (c != null)
                    {
                        Item item = c.FindItemByType( typeof( RAFTRS ) );						RAFTRS raftrs = item as RAFTRS;
                        if (raftrs != null)
                        {
                            raftrs.CurAmount += m_Value;
                            m_Placer.SendMessage("The item dissolves and gives you a refund of {0} in your RAFT.", m_Value.ToString("#,0"));
                            this.Delete();
                        }
                    }
                    else if (c.TryDropItem(m_Placer, new BankCheck(m_Value), true))
                    {
                        m_Placer.SendMessage("The item dissolves and gives you a bank check refund of {0}.", m_Value.ToString("#,0"));
                        this.Delete();
                    }
                    else
                    {
                        m_Placer.SendMessage("For some reason, the refund didn't work! Please page a GM");
                    }
                }
                else
                {
                    from.SendMessage("Stay out of my yard!");
                }
            }
            else
            {
                from.SendMessage("The item is too far away");
            }
        }
    }
}