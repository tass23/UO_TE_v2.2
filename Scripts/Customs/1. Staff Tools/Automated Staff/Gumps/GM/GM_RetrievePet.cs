
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
using Server.Spells;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Accounting;
using Server.Misc;
using Server.Network;

namespace Server.Gumps
{
    public class GM_RetrievePet : Gump
    {
        Mobile caller;     

        #region GM_RetrievePet Gump Configuration
       
        private const int m_Price = 3500;
        private List<Mobile> m_SummonablePets = new List<Mobile>();

        public GM_RetrievePet(List<Mobile> summonablePets) : base ( 0, 0 )
        {

            m_SummonablePets = summonablePets;

            int petCount = m_SummonablePets.Count;
            int price = m_Price * petCount;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(161, 153, 473, 289, 9200);
			AddItem(156, 415, 6914);
			AddItem(595, 416, 6913);
			AddItem(169, 355, 6920);
			AddItem(599, 153, 6917);
			AddItem(154, 154, 6916);
			AddBackground(196, 182, 412, 235, 5054);
			AddImage(111, 220, 10400);
			AddImage(602, 234, 10411);
			AddButton(612, 337, 22150, 22151, (int)Buttons.Button1, GumpButtonType.Reply, 0);//Cancel
			AddAlphaRegion(196, 181, 411, 237);
			AddBackground(210, 196, 385, 207, 5054);
            AddImageTiled(304, 244, 189, 18, 2621);
            AddLabel(280, 218, 195, @"SUMMON PET(S) TO YOUR LOCATION?");
			AddItem(230, 214, 8406);
			AddItem(533, 335, 8465);
			AddItem(231, 278, 9624);
			AddItem(537, 216, 8471);
			AddItem(534, 282, 8464);
			AddItem(222, 342, 8501);
			AddButton(289, 327, 4005, 4006, (int)Buttons.Button2, GumpButtonType.Reply, 0);//YES
			AddButton(289, 356, 4005, 4006, (int)Buttons.Button3, GumpButtonType.Reply, 0);//NO
            AddLabel(361, 328, 695, @"I'll Happily Pay The Gold!");
            AddLabel(361, 357, 695, @"I'll Find My Pets Myself!");
			AddLabel(327, 357, 232, @"No!!!");
			AddLabel(326, 328, 232, @"Yes!");
            AddImageTiled(304, 261, 189, 39, 2624);
            AddLabel(442, 257, 232, price.ToString());
            AddImageTiled(304, 297, 189, 18, 2627);
            AddLabel(309, 281, 694, @"Gold Coins For Your " + petCount + " Pet(s)");
            AddLabel(316, 257, 694, @"The Charge Will Be:");
            AddImage(304, 243, 2362);
            AddImage(482, 243, 2362);
            AddImage(304, 306, 2362);
            AddImage(482, 306, 2362);
        }

        #endregion Edited By: A.A.R

        public enum Buttons
		{
			Button1,
			Button2,
			Button3,
		}

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile; 
            from.CloseGump(typeof(GM_RetrievePet));

            switch(info.ButtonID)
            {
                case (int)Buttons.Button1:
				    {
                        from.CloseGump(typeof(GM_RetrievePet));
					    break;
				    }
                case (int)Buttons.Button2:
                    {
                        
                        int price = m_Price * m_SummonablePets.Count;

                        if (Banker.Withdraw(from, price))
                        {
                            from.SendLocalizedMessage(1060398, price.ToString()); // Amount charged  
                            from.SendLocalizedMessage(1060022, Banker.GetBalance(from).ToString()); // Amount left, from bank  

                            foreach (Mobile pet in m_SummonablePets)
                            {
                                pet.MoveToWorld(from.Location, from.Map);

                                Effects.SendLocationParticles(EffectItem.Create(from.Location, from.Map, EffectItem.DefaultDuration), 0x3728, 10, 30, 5052);
                                Effects.PlaySound(from.Location, from.Map, 0x201);
                            }
                        }
                        else
                        {
                            from.SendMessage("You do not have enough money in your bank to summon your pets!");
                        }

                        break;
                    }
                        		   		  
				case (int)Buttons.Button3:
				    {
                        from.CloseGump(typeof(GM_RetrievePet));
					    break;
				    }
            }
        }
    }
}