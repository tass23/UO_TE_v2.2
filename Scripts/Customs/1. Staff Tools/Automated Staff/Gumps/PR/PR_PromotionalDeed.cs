
#region Automated Server Staff Acknowledgements
/*
    This Automated NPC System Idea Originated
    With A Script Coded By: Tresdni from the
    RunUO (www.runuo.com) Forums; It Is Named:
    
       **Completely Automated Staff Team** 
 http://www.runuo.com/community/threads/completely-automated-staff-team-oh-yes-i-did.460720/
 
    This Released Version Of The Script Named
    Above Is My Own Variation On What I Think
    Might Complete The System Tresdni Started.
    
    The Code In This Script File Is Annoted. I
    Have Regioned Out Most Areas And Outlined
    Others So That You Know What Code Can Be
    Copy And Pasted To Other Scripts To Add The
    Same Functionality For Another System. 
 
    The Author Of Each Line Of Code Varies, I
    Got The Shell Of This Script From Tresdni,
    However A Lot Has Come From Many Other 
    Sources Over The Years; I Have A Library
    Of Annotated Methods I've Been Working On,
    That Help Me Build The Scripts I Upload.
 
    A Special Thank You Goes Out To The Following
    People For Helping Me Complete This System
    Addition To The Completely Automated Staff Team,
    Written By: Tresdni:
 
    THANK YOU GUYS!! THE HELP WAS MUCH APPRECIATED
                   -Sythen (A.A.R)-
    ______________________________________________
    ** JamzeMcC | Morexton | Soteric | James420 **
    ----------------------------------------------
 */
#endregion Edited By: A.A.R

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.ContextMenus;
using Server.Multis;
using Server.Regions;
using Server.Engines.CannedEvil;
using Server.Spells;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Accounting;
using Server.Prompts;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
    public class PromotionalDeed_PR : Item
    {
        [Constructable]
        public PromotionalDeed_PR(): base(0x14EE)
        {
            Name = "A Gift Deed";
            Weight = 1.0;
            LootType = LootType.Blessed;
            Hue = 0x3;
        }

        public override void OnDoubleClick(Mobile from)
        {
            Item pgd = from.Backpack.FindItemByType(typeof(PromotionalDeed_PR));
            if (pgd != null)
            {
                if (this.ItemID == 0x14EE) this.ItemID = 0x14F0;
                else if (this.ItemID == 0x14F0) this.ItemID = 0x14EE;

                from.SendGump(new PromotionalGift_PR(from, this));

                if (this.ItemID == 0x14EE)
                    from.CloseGump(typeof(PromotionalGift_PR));
            }
            else
            {
                if (!IsChildOf(from.Backpack))
                {
                    from.SendMessage("The Deed's Owner Shouldn't Have Dropped This!");
                    this.Delete();
                }
            }
        }

        public override void OnRemoved(object parent)
        {
            Mobile m = null;

            if (parent is Item)
                m = ((Item)parent).RootParent as Mobile;
            else if (parent is Mobile)
                m = (Mobile)parent;

            if (m != null)
                m.CloseGump(typeof(PromotionalGift_PR));
        }

        public PromotionalDeed_PR(Serial serial): base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class PromotionalGift_PR : Gump
    {     
        private Item m_Deed;
        private Mobile m_Mobile;

        #region Promotional Gift Gump Configuration

        public PromotionalGift_PR(Mobile from, Item deed): base(0, 0)
        {
            m_Mobile = from;
            m_Deed = deed;
            {
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;

                AddPage(0);

                AddBackground(188, 91, 417, 430, 5054);
                AddAlphaRegion(218, 129, 356, 319);
                AddButton(227, 141, 2328, 2329, (int)Buttons.Button1, GumpButtonType.Reply, 1);
                AddButton(356, 141, 2328, 2329, (int)Buttons.Button2, GumpButtonType.Reply, 2);
                AddButton(484, 141, 2328, 2329, (int)Buttons.Button3, GumpButtonType.Reply, 3);
                AddButton(227, 219, 2328, 2329, (int)Buttons.Button4, GumpButtonType.Reply, 4);
                AddButton(356, 219, 2328, 2329, (int)Buttons.Button5, GumpButtonType.Reply, 5);
                AddButton(484, 219, 2328, 2329, (int)Buttons.Button6, GumpButtonType.Reply, 6);
                AddButton(227, 300, 2328, 2329, (int)Buttons.Button7, GumpButtonType.Reply, 7);
                AddButton(356, 300, 2328, 2329, (int)Buttons.Button8, GumpButtonType.Reply, 8);
                AddButton(484, 300, 2328, 2329, (int)Buttons.Button9, GumpButtonType.Reply, 9);
                AddButton(227, 378, 2328, 2329, (int)Buttons.Button10, GumpButtonType.Reply, 10);
                AddButton(356, 378, 2328, 2329, (int)Buttons.Button11, GumpButtonType.Reply, 11);
                AddButton(484, 378, 2328, 2329, (int)Buttons.Button12, GumpButtonType.Reply, 12);
                AddLabel(232, 132, 195, @"JAN    01");
                AddLabel(361, 132, 195, @"FEB    02");
                AddLabel(487, 132, 195, @"MAR    03");
                AddLabel(231, 211, 195, @"APR    04");
                AddLabel(359, 211, 195, @"MAY    05");
                AddLabel(487, 211, 195, @"JUN    06");
                AddLabel(232, 292, 195, @"JUL    07");
                AddLabel(360, 292, 195, @"AUG    08");
                AddLabel(488, 292, 195, @"SEP    09");
                AddLabel(233, 370, 195, @"OCT    10");
                AddLabel(363, 370, 195, @"NOV    11");
                AddLabel(490, 370, 195, @"DEC    12");
                AddLabel(356, 458, 190, @"PROMOTIONAL GIFT ITEMS");
                AddLabel(231, 458, 232, @"2011");
                AddLabel(422, 471, 695, @"The Expanse");
                AddItem(261, 452, 9002);
                AddLabel(326, 95, 190, @"PLEASE SELECT A GIFT");
                AddButton(528, 455, 9005, 9004, (int)Buttons.Button0, GumpButtonType.Reply, 0);               
            }
        }

        #endregion Edited By: A.A.R

        public enum Buttons
        {
            Button0,
            Button1,
            Button2,
            Button3,
            Button4,
            Button5,
            Button6,
            Button7,
            Button8,
            Button9,
            Button10,
            Button11,
            Button12,            
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.Button0:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }

                case (int)Buttons.Button1:
                    {
                        Item item = new Dagger();
                        from.AddToBackpack(item);
                        from.SendMessage("Your Reward Has Been Chosen, Enjoy!");
                        from.CloseGump(typeof(PromotionalGift_PR));
                        m_Deed.Delete();
                        break;
                    }

                case (int)Buttons.Button2:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button3:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button4:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button5:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button6:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button7:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button8:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button9:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button10:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button11:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
                case (int)Buttons.Button12:
                    {
                        from.CloseGump(typeof(PromotionalGift_PR));
                        break;
                    }
            }
        }
    }
}


   