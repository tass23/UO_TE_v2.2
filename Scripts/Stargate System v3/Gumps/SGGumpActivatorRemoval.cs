using System;
using System.Collections;

using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.SG
{
    public class SGGumpActivatorRemoval : Gump
    {
        private SGActivatorDevice m_sgactivatordevice;

        public SGGumpActivatorRemoval(SGActivatorDevice m) : base(150, 50)
        {
            m_sgactivatordevice = m;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 398, 149, 9270);
            this.AddLabel(75, 15, 1153, @"Stargate Crystal Control Device Panel");
            this.AddLabel(20, 40, 54, @"To remove this Crystal Control Device from the stargate");
            this.AddLabel(20, 60, 54, @"system correctly please use the remove option.");
            this.AddLabel(60, 85, 69, @"Remove THIS Device");
            this.AddLabel(60, 107, 33, @"Exit / Cancel");
            this.AddButton(20, 83, 4002, 4004, 0, GumpButtonType.Reply, 0); // Remove
            this.AddButton(20, 105, 4020, 4022, 1, GumpButtonType.Reply, 0); // Exit / Cancel
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0: // Remove
                    {
                        if (!SGCore.SGSystemEnabled)
                        {
                            from.SendMessage(77, "Crystal & Address removed from the stargate system...");
                            from.CloseGump(typeof(SGGumpActivatorRemoval));

                            int removeIndex = 0;
                            for (int i = 0; i < SGCore.SGList.Count; i++)
                            {
                                SGEntry sge = (SGEntry)SGCore.SGList[i];
                                {
                                    if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                    {
                                        // this gate
                                        removeIndex = i;
                                    }
                                }
                            }
                            // Remove Address Entry
                            SGEntry sgeToBeRemoved = (SGEntry)SGCore.SGList[removeIndex];
                            SGCore.SGList.Remove(sgeToBeRemoved);

                            // ***************************************************
                            // * Force a Rebuild of Entire Shard Stargate System *
                            // ***************************************************

                            // Trigger Save to Update Stargate XML File (With Your Removed Gate OUT)
                            SGCore.SGTriggerSave();
                            // Trigger Load to Put Shards Stargates Back (With Your Removed Gate OUT)
                            SGCore.SGTriggerLoad();
                        }
                        else
                        {
                            from.SendMessage(77, "OPERATION ABORTED !!! Stargate System Still ACTIVE, Gates can only be removed IF the stargate system is DISABLED.");
                            from.SendMessage(77, "Use [SGAdmin option to DISABLE the system 1st");
                        }  
                    }
                    break;

                case 1: // Exit
                    {
                        from.SendMessage(77, "Exit Crystal Device Admin, This Device Remains In The System.");
                    }
                    break;
            }
        }
    }
}