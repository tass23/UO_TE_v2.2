
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Engines.PartySystem;
using System.Data;
using System.Xml;
using Server.Engines.XmlSpawner2;


namespace Server.Items
{
    public class QuestBookBag : Pouch
    {
        private PlayerMobile m_Owner;

        [CommandProperty(AccessLevel.Administrator)]
        public PlayerMobile M_Owner { get { return m_Owner; } set { m_Owner = value; InvalidateProperties(); } }		

        private double m_Redux;
        [CommandProperty(AccessLevel.GameMaster)]

        public int ReduxPercent
        {
            get { return (int)(m_Redux * 100); }
            set
            {
                value = 100;
                /*if (value < 0)
                    value = 100;
                if (value > 100)
                    value = 100;*/
                m_Redux = ((double)value) / 100;/*
                if (Parent is Item)
                {
                    (Parent as Item).UpdateTotals();
                    (Parent as Item).InvalidateProperties();
                }
                else if (Parent is Mobile)
                    (Parent as Mobile).UpdateTotals();
                else*/
                    UpdateTotals();
                //InvalidateProperties();
            }
        }
        public override string DefaultName { get { return "QuestBook Backpack"; } }

        [Constructable]
        public QuestBookBag(): base()
        {
            ReduxPercent = 100;
            Hue = 1100;
            MaxItems = 125;
            LootType = LootType.Blessed;
            ItemID = 3701;
        }

        public QuestBookBag(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write((double)m_Redux);
            writer.Write(M_Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_Redux = reader.ReadDouble();
            this.M_Owner = reader.ReadMobile() as PlayerMobile;
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is Server.Items.XmlQuestHolder || dropped is Server.Items.QuestHolder || dropped is Server.Items.SimpleNote || dropped is Server.Items.QuestNote) 
                return base.OnDragDrop(from, dropped);
            else
            {
                from.SendMessage("You can only place questbooks into here");
                return false;
            }
        }

        public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
        {

            if (item is Server.Items.XmlQuestHolder || item is Server.Items.QuestHolder || item is Server.Items.SimpleNote || item is Server.Items.QuestNote) 
                return base.OnDragDropInto(from, item, p);
                else
            {
                from.SendMessage("You can only place questbooks into here");
                    return false;
                }
        }
        
        public override void OnDoubleClick(Mobile from)
        {
            if (!(from is PlayerMobile)) return;
            if (this.IsChildOf(from.Backpack))
            {
                if (M_Owner == null)
                {
                    M_Owner = from as PlayerMobile;
                    Name = from.Name + "'s Questbook Backpack";
                    base.OnDoubleClick(from);
                }
                else if ((from == M_Owner) || (from.AccessLevel >= AccessLevel.GameMaster))
                {
                    base.OnDoubleClick(from);
                }
                else
                    from.SendMessage(1173, "This is not your Questbook Backpack");
            }
            else
                from.SendMessage(1173, "This must be in your backpack");

        }
        
        public override void OnItemLifted(Mobile from, Item item)
        {
            base.OnItemLifted(from, item);

            if (from is PlayerMobile)
            {
                if (from is PlayerMobile && M_Owner == null)
                {
                    M_Owner = from as PlayerMobile;
                    LootType = LootType.Blessed;
                    Name = from.Name + "'s Questbook Backpack";
                }
                 else if (from.AccessLevel >= AccessLevel.GameMaster)
                {
                    base.OnItemLifted(from, item);
                }
                else if (M_Owner != from)
                {
                    from.SendMessage(1173, "This is not your Questbook Backpack");
                    M_Owner.AddToBackpack(this);
                }
            }
        }
        
        public override bool OnDroppedToWorld(Mobile from, Point3D point)
        {
            bool returnvalue = base.OnDroppedToWorld(from, point);
            from.SendGump(new XmlConfirmDeleteGump(from, this));
            ReduxPercent = 100;
            return false;
        }

        public override void GetProperties(ObjectPropertyList list)
        {

            list.Add(Name);
            if (LootType == LootType.Blessed)
            {
                list.Add(1038021);
            }

        }
        public override void OnAdded(object target)
        {
            base.OnAdded(target);

            if ((target != null) && target is Container)
            {
                // find the parent of the container
                // note, the only valid additions are to the player pack.  Anything else is invalid.  
                // This is to avoid exploits involving storage or transfer of questtokens
                object parentOfTarget = ((Container)target).Parent;

                    if ((parentOfTarget != null) && (parentOfTarget is PlayerMobile) && M_Owner != null)
                    {
                        //m_Owner = RootParentEntity as PlayerMobile;
                        if (RootParentEntity == null)
                        {

                        }                            
                        else
                            if ((parentOfTarget != M_Owner) || (target is Mobile) || (target is BankBox))//
                            {
                                // tried to give it to another player or placed it in the players bankbox. try to return it to the owners pack
                                M_Owner.AddToBackpack(this);
                                this.ReduxPercent = 100;                            }
                    }
                    else
                    {
                        if ((RootParentEntity != null) && (M_Owner != null))
                        {
                            // try to return it to the owners pack
                            M_Owner.AddToBackpack(this);
                            this.ReduxPercent = 100;
                        }
                       
                    }
            }
        }
        public override void UpdateTotal(Item sender, TotalType type, int delta)
        {
            base.UpdateTotal(sender, type, delta);
            if (type == TotalType.Weight)
            {
                if (Parent is Item)
                    (Parent as Item).UpdateTotal(sender, type, (int)(delta * m_Redux) * -1);
                else if (Parent is Mobile)
                    (Parent as Mobile).UpdateTotal(sender, type, (int)(delta * m_Redux) * -1);
            }
        }

        public override int GetTotal(TotalType type)
        {
            if (type == TotalType.Weight)
                return (int)(base.GetTotal(type) * (1.0 - m_Redux));
            return base.GetTotal(type);
        }
    }
}