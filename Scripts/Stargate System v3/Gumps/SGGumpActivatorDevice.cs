using System;
using System.Collections;

using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.SG
{
    public class SGGumpActivatorDevice : Gump
    {
        private SGActivatorDevice m_sgactivatordevice;

        public int sgTOselectioncounter;
        public int reset = 1;

        public int sgTOfacetid;
        public int sgTOaddress1;
        public int sgTOaddress2;
        public int sgTOaddress3;
        public int sgTOaddress4;
        public int sgTOaddress5;

        public int sgDESTINATIONfacet;
        public int sgDESTINATIONaddress1;
        public int sgDESTINATIONaddress2;
        public int sgDESTINATIONaddress3;
        public int sgDESTINATIONaddress4;
        public int sgDESTINATIONaddress5;

        public int sgDESTINATIONlocX;
        public int sgDESTINATIONlocY;
        public int sgDESTINATIONlocZ;
        public string sgDESTINATIONfacing;

        public bool sgDESTINATIONbusyused;
        public bool sgDESTINATIONbusyenergy;

        public int sgOnThisFacet = 0;
        public int sgOnThisHidden = 0;

        

        public SGGumpActivatorDevice(Mobile from, SGActivatorDevice m, int sgCURRENTsc, int sgCURRENTfacet, int sgCURRENTadd1, int sgCURRENTadd2, int sgCURRENTadd3, int sgCURRENTadd4, int sgCURRENTadd5) : base(100, 100)
        {
            m_sgactivatordevice = m;

            int SGSYB1 = 3676;
            int SGSYB2 = 3679;
            int SGSYB3 = 3682;
            int SGSYB4 = 3685;
            int SGSYB5 = 3688;

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddImage(0, 0, 30500);

            // THIS GATE ADDRESS
            this.AddItem(20, 40, m.SGAFacetNumber * 3 - 3 + SGSYB1, 1360);

            this.AddItem(80, 40, m.SGAAddressCode1 * 3 - 3 + SGSYB1, 1372);
            this.AddItem(120, 40, m.SGAAddressCode2 * 3 - 3 + SGSYB1, 1372);
            this.AddItem(160, 40, m.SGAAddressCode3 * 3 - 3 + SGSYB1, 1372);
            this.AddItem(200, 40, m.SGAAddressCode4 * 3 - 3 + SGSYB1, 1372);
            this.AddItem(240, 40, m.SGAAddressCode5 * 3 - 3 + SGSYB1, 1372);

            // Dial Code Selection Icons
            // Facet option, starts with the facet the player is currently on
            if (sgCURRENTfacet == 1)
            {
                this.AddItem(365, 43, sgCURRENTfacet * 3 - 3 + SGSYB1, 1360);
                this.AddLabel(305, 40, 35, @"Felucca");
            }
            else if (sgCURRENTfacet == 2)
            {
                this.AddItem(365, 43, sgCURRENTfacet * 3 - 3 + SGSYB1, 1360);
                this.AddLabel(305, 40, 70, @"Trammel");
            }
            else if (sgCURRENTfacet == 3)
            {
                this.AddItem(365, 43, sgCURRENTfacet * 3 - 3 + SGSYB1, 1360);
                this.AddLabel(305, 40, 55, @"Ilshenar");
            }
            else if (sgCURRENTfacet == 4)
            {
                this.AddItem(365, 43, sgCURRENTfacet * 3 - 3 + SGSYB1, 1360);
                this.AddLabel(305, 40, 1265, @"Malas");
            }
            else if (sgCURRENTfacet == 5)
            {
                this.AddItem(365, 43, sgCURRENTfacet * 3 - 3 + SGSYB1, 1360);
                this.AddLabel(305, 40, 1152, @"Tokuno");
            }

            this.AddItem(387, 106, SGSYB1, 1195);
            this.AddItem(394, 161, SGSYB2, 1195);
            this.AddItem(392, 218, SGSYB3, 1195); // this
            this.AddItem(386, 288, SGSYB4, 1195); // this
            this.AddItem(371, 345, SGSYB5, 1195);

            // Gate Selected By User
            this.AddItem(30, 125, sgCURRENTfacet * 3 - 3 + SGSYB1, 1360);

            if (sgCURRENTsc == 1)
            {
                this.AddItem(90, 125, sgCURRENTadd1 * 3 - 3 + SGSYB1, 1153);
                this.AddItem(130, 125, sgCURRENTadd2 * 3 - 3 + SGSYB1, 33);
                this.AddItem(170, 125, sgCURRENTadd3 * 3 - 3 + SGSYB1, 33);
                this.AddItem(210, 125, sgCURRENTadd4 * 3 - 3 + SGSYB1, 33);
                this.AddItem(250, 125, sgCURRENTadd5 * 3 - 3 + SGSYB1, 33);
            }
            else if (sgCURRENTsc == 2)
            {
                this.AddItem(90, 125, sgCURRENTadd1 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(130, 125, sgCURRENTadd2 * 3 - 3 + SGSYB1, 1153);
                this.AddItem(170, 125, sgCURRENTadd3 * 3 - 3 + SGSYB1, 33);
                this.AddItem(210, 125, sgCURRENTadd4 * 3 - 3 + SGSYB1, 33);
                this.AddItem(250, 125, sgCURRENTadd5 * 3 - 3 + SGSYB1, 33);
            }
            else if (sgCURRENTsc == 3)
            {
                this.AddItem(90, 125, sgCURRENTadd1 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(130, 125, sgCURRENTadd2 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(170, 125, sgCURRENTadd3 * 3 - 3 + SGSYB1, 1153);
                this.AddItem(210, 125, sgCURRENTadd4 * 3 - 3 + SGSYB1, 33);
                this.AddItem(250, 125, sgCURRENTadd5 * 3 - 3 + SGSYB1, 33);
            }
            else if (sgCURRENTsc == 4)
            {
                this.AddItem(90, 125, sgCURRENTadd1 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(130, 125, sgCURRENTadd2 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(170, 125, sgCURRENTadd3 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(210, 125, sgCURRENTadd4 * 3 - 3 + SGSYB1, 1153);
                this.AddItem(250, 125, sgCURRENTadd5 * 3 - 3 + SGSYB1, 33);
            }
            else if (sgCURRENTsc == 5)
            {
                this.AddItem(90, 125, sgCURRENTadd1 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(130, 125, sgCURRENTadd2 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(170, 125, sgCURRENTadd3 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(210, 125, sgCURRENTadd4 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(250, 125, sgCURRENTadd5 * 3 - 3 + SGSYB1, 1153);
            }
            else if (sgCURRENTsc >= 6)
            {
                this.AddItem(90, 125, sgCURRENTadd1 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(130, 125, sgCURRENTadd2 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(170, 125, sgCURRENTadd3 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(210, 125, sgCURRENTadd4 * 3 - 3 + SGSYB1, 1372);
                this.AddItem(250, 125, sgCURRENTadd5 * 3 - 3 + SGSYB1, 1372);
            }
            else
            {
                this.AddItem(90, 125, sgCURRENTadd1 * 3 - 3 + SGSYB1, 33);
                this.AddItem(130, 125, sgCURRENTadd2 * 3 - 3 + SGSYB1, 33);
                this.AddItem(170, 125, sgCURRENTadd3 * 3 - 3 + SGSYB1, 33);
                this.AddItem(210, 125, sgCURRENTadd4 * 3 - 3 + SGSYB1, 33);
                this.AddItem(250, 125, sgCURRENTadd5 * 3 - 3 + SGSYB1, 33);
            }

            sgTOselectioncounter = sgCURRENTsc;
            sgTOfacetid = sgCURRENTfacet;
            sgTOaddress1 = sgCURRENTadd1;
            sgTOaddress2 = sgCURRENTadd2;
            sgTOaddress3 = sgCURRENTadd3;
            sgTOaddress4 = sgCURRENTadd4;
            sgTOaddress5 = sgCURRENTadd5;
            

            // Labels on the gate
            this.AddLabel(15, 20, 1153, @"Stargate Address :");
            this.AddLabel(140, 20, 1160, m.SGALocationName);

            this.AddLabel(25, 75, 1153, @"Discovered By :");
            this.AddLabel(127, 75, 45, m.SGADiscovererName);

            this.AddLabel(305, 20, 90, @"Destination Facet");
            this.AddLabel(330, 85, 90, @"Symbol Selector");
            this.AddLabel(35, 104, 90, @"Destination Selection...");
            this.AddLabel(65, 373, 60, @"Activate");
            this.AddLabel(65, 395, 90, @"Reset");
            this.AddLabel(65, 417, 32, @"Cancel");

            this.AddLabel(60, 160, 64, @"Stargates On This Facet :");
            this.AddLabel(60, 180, 64, @"Hidden Locations :");

            this.AddImage(178, 384, 1287);
            this.AddLabel(160, 402, 1161, @"Stargate System v3.0");
            this.AddLabel(171, 418, 1161, @"By FingersMcSteal");

            this.AddButton(410, 40, 5600, 5604, 0, GumpButtonType.Reply, 0); // Facet Code Up +1
            this.AddButton(410, 65, 5602, 5606, 1, GumpButtonType.Reply, 0); // Facet Code Down -1
            this.AddButton(392, 130, 4026, 4028, 2, GumpButtonType.Reply, 0); // Selection 1
            this.AddButton(400, 185, 4026, 4028, 3, GumpButtonType.Reply, 0); // Selection 2
            this.AddButton(400, 254, 4026, 4028, 4, GumpButtonType.Reply, 0); // Selection 3
            this.AddButton(392, 313, 4026, 4028, 5, GumpButtonType.Reply, 0); // Selection 4
            this.AddButton(377, 381, 4026, 4028, 6, GumpButtonType.Reply, 0); // Selection 5
            this.AddButton(30, 371, 4005, 4007, 7, GumpButtonType.Reply, 0); // ACTIVATE
            this.AddButton(30, 393, 4029, 4031, 8, GumpButtonType.Reply, 0); // RESET
            this.AddButton(30, 415, 4017, 4019, 9, GumpButtonType.Reply, 0); // CANCEL
            this.AddButton(387, 419, 22153, 22155, 10, GumpButtonType.Reply, 0); // Help
            this.AddLabel(407, 418, 1153, @"Help");

            // Administrator & GM Functions
            if (from.AccessLevel >= AccessLevel.GameMaster)
            {
                this.AddLabel(65, 351, 1153, @"Administrator & GM Functions");
                this.AddButton(30, 349, 4026, 4028, 11, GumpButtonType.Reply, 0);
            }

            // Facet List Window
            string NewList = "<BASEFONT COLOR=#00BBFF>The Following Stargates On Your Selected Destination Facet Are Listed Below...<BR><BR><BASEFONT COLOR=#00EEFF>";
            for (int i = 0; i < SGCore.SGList.Count; i++)
            {
                SGEntry sge = (SGEntry)SGCore.SGList[i];
                if (sge.SGFacetCode == sgCURRENTfacet)
                {
                    sgOnThisFacet++;
                    if (!sge.SGHidden)
                    {
                        NewList = (string.Format(NewList) + sge.SGAddressCode1.ToString() + "," + sge.SGAddressCode2.ToString() + "," + sge.SGAddressCode3.ToString() + "," + sge.SGAddressCode4.ToString() + "," + sge.SGAddressCode5.ToString() + " : " + sge.SGLocationName) + "<BR>";
                    }
                    else
                    {
                        sgOnThisHidden++;
                        NewList = (string.Format(NewList)) + "<BASEFONT COLOR=#FF0000>Undiscovered Location ???<BASEFONT COLOR=#00EEFF><BR>";
                    }
                }
            }
            this.AddLabel(225, 160, 1160, sgOnThisFacet.ToString() );
            this.AddLabel(180, 180, 33, sgOnThisHidden.ToString() );

            this.AddHtml(30, 200, 315, 140, NewList, (bool)false, (bool)true);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            int destIndex = 0; // index of destination
            int fromIndex = 0; // index of origin

            if (!from.InRange(m_sgactivatordevice.GetWorldLocation(), 1) || from.Map != m_sgactivatordevice.Map)
            {
                from.SendMessage(88, "You are too far away from the crystal Control Device to use it.");
                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                from.CloseGump(typeof(SGGumpActivatorDevice));
            }
            else
            {
                // Allow use of the gate

                #region selection1
                if (sgTOselectioncounter == 1)
                {
                    switch (info.ButtonID)
                    {
                        case 0:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid + 1;
                                if (sgTOfacetid >= 5)
                                {
                                    sgTOfacetid = 5;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 1:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid - 1;
                                if (sgTOfacetid <= 1)
                                {
                                    sgTOfacetid = 1;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 2:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = 1;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 3:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = 2;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 4:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = 3;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 5:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = 4;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 6:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = 5;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 7:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButtonActivate);
                                from.CloseGump(typeof(SGGumpActivatorDevice));

                                // check I can go from here
                                bool stage1check = false;

                                for (int i = 0; i < SGCore.SGList.Count; i++)
                                {
                                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                                    {
                                        if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                        {
                                            // this gate
                                            fromIndex = i;
                                            if (!sge.SGEnergy)
                                            {
                                                // i can continue
                                                stage1check = true;
                                            }
                                            else
                                            {
                                                from.SendMessage(77, "Someone opened this gate before you, please wait a moment.");

                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                        }
                                    }
                                }

                                if (stage1check)
                                {
                                    // find destination
                                    for (int i = 0; i < SGCore.SGList.Count; i++)
                                    {
                                        SGEntry sge = (SGEntry)SGCore.SGList[i];
                                        if (sge.SGFacetCode == sgTOfacetid && sge.SGAddressCode1 == sgTOaddress1 && sge.SGAddressCode2 == sgTOaddress2 && sge.SGAddressCode3 == sgTOaddress3 && sge.SGAddressCode4 == sgTOaddress4 && sge.SGAddressCode5 == sgTOaddress5)
                                        {
                                            // match
                                            if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                            {
                                                // Already here (Code selected was same code)
                                                from.SendMessage(77, "Your already there.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                            else
                                            {
                                                // match and different from where you are
                                                destIndex = i;

                                                sgDESTINATIONlocX = sge.SGX;
                                                sgDESTINATIONlocY = sge.SGY;
                                                sgDESTINATIONlocZ = sge.SGZ;
                                                sgDESTINATIONfacing = sge.SGFacing;

                                                sgDESTINATIONfacet = sge.SGFacetCode;
                                                sgDESTINATIONaddress1 = sge.SGAddressCode1;
                                                sgDESTINATIONaddress2 = sge.SGAddressCode2;
                                                sgDESTINATIONaddress3 = sge.SGAddressCode3;
                                                sgDESTINATIONaddress4 = sge.SGAddressCode4;
                                                sgDESTINATIONaddress5 = sge.SGAddressCode5;

                                                sgDESTINATIONbusyused = sge.SGBeingUsed;
                                                sgDESTINATIONbusyenergy = sge.SGEnergy;

                                                if (!sgDESTINATIONbusyenergy)
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundGoodToGo);

                                                    // setup energy bits
                                                    SGEntry sgeDestination = (SGEntry)SGCore.SGList[destIndex];
                                                    sgeDestination.SGEnergy = true; // turn ON energy bit at DESTINATION gate (stops outbound calls)
                                                    SGEntry sgeOrigin = (SGEntry)SGCore.SGList[fromIndex];
                                                    sgeOrigin.SGEnergy = true; // turn ON energy HERE (stops inbounds)

                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer HERE
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter

                                                    // place SGTeleport Tile into doorway TO THE DESTINATION Location (FROM HERE TO x,y,z)
                                                    if (sgDESTINATIONfacet == 1)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                    }

                                                    if (sgDESTINATIONfacet == 2)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                    }
                                                    if (sgDESTINATIONfacet == 3)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 4)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 5)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundNoTravel);
                                                    from.SendMessage(77, "The destination is in use, please wait a moment and try again.");
                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer HERE
                                                }
                                            }
                                        }
                                        m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                        m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                        m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                    }
                                }
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 8:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "You reset the dialing sequence");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = reset;
                                int sgTOaddress2 = reset;
                                int sgTOaddress3 = reset;
                                int sgTOaddress4 = reset;
                                int sgTOaddress5 = reset;
                                sgTOselectioncounter = 1;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 9:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "Cancel selected, you leave the device alone.");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 10:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "Help Option selected");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.CloseGump(typeof(SGGumpHelp));
                                from.SendGump(new SGGumpHelp());
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 11:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                from.SendGump(new SGGumpActivatorRemoval(m_sgactivatordevice));
                            }
                            break;
                    }
                }
                #endregion selection1

                #region selection2
                else if (sgTOselectioncounter == 2)
                {
                    switch (info.ButtonID)
                    {
                        case 0:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid + 1;
                                if (sgTOfacetid >= 5)
                                {
                                    sgTOfacetid = 5;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 1:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid - 1;
                                if (sgTOfacetid <= 1)
                                {
                                    sgTOfacetid = 1;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 2:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress2 = 1;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 3:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress2 = 2;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 4:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress2 = 3;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 5:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress2 = 4;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 6:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress2 = 5;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 7:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButtonActivate);
                                from.CloseGump(typeof(SGGumpActivatorDevice));

                                // check I can go from here
                                bool stage1check = false;

                                for (int i = 0; i < SGCore.SGList.Count; i++)
                                {
                                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                                    {
                                        if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                        {
                                            // this gate
                                            fromIndex = i;
                                            if (!sge.SGEnergy)
                                            {
                                                // i can continue
                                                stage1check = true;
                                            }
                                            else
                                            {
                                                from.SendMessage(77, "Someone opened this gate before you, please wait a moment.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                        }
                                    }
                                }

                                if (stage1check)
                                {
                                    // find destination
                                    for (int i = 0; i < SGCore.SGList.Count; i++)
                                    {
                                        SGEntry sge = (SGEntry)SGCore.SGList[i];
                                        if (sge.SGFacetCode == sgTOfacetid && sge.SGAddressCode1 == sgTOaddress1 && sge.SGAddressCode2 == sgTOaddress2 && sge.SGAddressCode3 == sgTOaddress3 && sge.SGAddressCode4 == sgTOaddress4 && sge.SGAddressCode5 == sgTOaddress5)
                                        {
                                            // match
                                            if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                            {
                                                // Already here (Code selected was same code)
                                                from.SendMessage(77, "Your already there.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                            else
                                            {
                                                // match and different from where you are
                                                destIndex = i;

                                                sgDESTINATIONlocX = sge.SGX;
                                                sgDESTINATIONlocY = sge.SGY;
                                                sgDESTINATIONlocZ = sge.SGZ;
                                                sgDESTINATIONfacing = sge.SGFacing;

                                                sgDESTINATIONfacet = sge.SGFacetCode;
                                                sgDESTINATIONaddress1 = sge.SGAddressCode1;
                                                sgDESTINATIONaddress2 = sge.SGAddressCode2;
                                                sgDESTINATIONaddress3 = sge.SGAddressCode3;
                                                sgDESTINATIONaddress4 = sge.SGAddressCode4;
                                                sgDESTINATIONaddress5 = sge.SGAddressCode5;

                                                sgDESTINATIONbusyused = sge.SGBeingUsed;
                                                sgDESTINATIONbusyenergy = sge.SGEnergy;

                                                if (!sgDESTINATIONbusyenergy)
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundGoodToGo);

                                                    // setup energy bits
                                                    SGEntry sgeDestination = (SGEntry)SGCore.SGList[destIndex];
                                                    sgeDestination.SGEnergy = true; // turn ON energy bit at DESTINATION gate (stops outbound calls)
                                                    SGEntry sgeOrigin = (SGEntry)SGCore.SGList[fromIndex];
                                                    sgeOrigin.SGEnergy = true; // turn ON energy HERE (stops inbounds)

                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counters

                                                    // place SGTeleport Tile into doorway TO THE DESTINATION Location (FROM HERE TO x,y,z)
                                                    if (sgDESTINATIONfacet == 1)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                    }

                                                    if (sgDESTINATIONfacet == 2)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                    }
                                                    if (sgDESTINATIONfacet == 3)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 4)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 5)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }

                                                    }
                                                    // check direction of DESTINATION GATE, place energy tiles
                                                }
                                                else
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundNoTravel);
                                                    from.SendMessage(77, "The destination is in use, please wait a moment and try again.");
                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                                }
                                            }
                                        }
                                        m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                        m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                        m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                    }
                                }
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 8:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "You reset the dialing sequence");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = reset;
                                int sgTOaddress2 = reset;
                                int sgTOaddress3 = reset;
                                int sgTOaddress4 = reset;
                                int sgTOaddress5 = reset;
                                sgTOselectioncounter = 1;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 9:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 10:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "help");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.CloseGump(typeof(SGGumpHelp));
                                from.SendGump(new SGGumpHelp());
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 11:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                from.SendGump(new SGGumpActivatorRemoval(m_sgactivatordevice));
                            }
                            break;
                    }
                }
                #endregion selection2

                #region selection3
                else if (sgTOselectioncounter == 3)
                {
                    switch (info.ButtonID)
                    {
                        case 0:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid + 1;
                                if (sgTOfacetid >= 5)
                                {
                                    sgTOfacetid = 5;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 1:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid - 1;
                                if (sgTOfacetid <= 1)
                                {
                                    sgTOfacetid = 1;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 2:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress3 = 1;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 3:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress3 = 2;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 4:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress3 = 3;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 5:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress3 = 4;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 6:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress3 = 5;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 7:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButtonActivate);
                                from.CloseGump(typeof(SGGumpActivatorDevice));

                                // check I can go from here
                                bool stage1check = false;

                                for (int i = 0; i < SGCore.SGList.Count; i++)
                                {
                                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                                    {
                                        if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                        {
                                            // this gate
                                            fromIndex = i;
                                            if (!sge.SGEnergy)
                                            {
                                                // i can continue
                                                stage1check = true;
                                            }
                                            else
                                            {
                                                from.SendMessage(77, "Someone opened this gate before you, please wait a moment.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                        }
                                    }
                                }

                                if (stage1check)
                                {
                                    // find destination
                                    for (int i = 0; i < SGCore.SGList.Count; i++)
                                    {
                                        SGEntry sge = (SGEntry)SGCore.SGList[i];
                                        if (sge.SGFacetCode == sgTOfacetid && sge.SGAddressCode1 == sgTOaddress1 && sge.SGAddressCode2 == sgTOaddress2 && sge.SGAddressCode3 == sgTOaddress3 && sge.SGAddressCode4 == sgTOaddress4 && sge.SGAddressCode5 == sgTOaddress5)
                                        {
                                            // match
                                            if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                            {
                                                // Already here (Code selected was same code)
                                                from.SendMessage(77, "Your already there.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                            else
                                            {
                                                // match and different from where you are
                                                destIndex = i;

                                                sgDESTINATIONlocX = sge.SGX;
                                                sgDESTINATIONlocY = sge.SGY;
                                                sgDESTINATIONlocZ = sge.SGZ;
                                                sgDESTINATIONfacing = sge.SGFacing;

                                                sgDESTINATIONfacet = sge.SGFacetCode;
                                                sgDESTINATIONaddress1 = sge.SGAddressCode1;
                                                sgDESTINATIONaddress2 = sge.SGAddressCode2;
                                                sgDESTINATIONaddress3 = sge.SGAddressCode3;
                                                sgDESTINATIONaddress4 = sge.SGAddressCode4;
                                                sgDESTINATIONaddress5 = sge.SGAddressCode5;

                                                sgDESTINATIONbusyused = sge.SGBeingUsed;
                                                sgDESTINATIONbusyenergy = sge.SGEnergy;

                                                if (!sgDESTINATIONbusyenergy)
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundGoodToGo);

                                                    // setup energy bits
                                                    SGEntry sgeDestination = (SGEntry)SGCore.SGList[destIndex];
                                                    sgeDestination.SGEnergy = true; // turn ON energy bit at DESTINATION gate (stops outbound calls)
                                                    SGEntry sgeOrigin = (SGEntry)SGCore.SGList[fromIndex];
                                                    sgeOrigin.SGEnergy = true; // turn ON energy HERE (stops inbounds)

                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter

                                                    // place SGTeleport Tile into doorway TO THE DESTINATION Location (FROM HERE TO x,y,z)
                                                    if (sgDESTINATIONfacet == 1)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                    }

                                                    if (sgDESTINATIONfacet == 2)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                    }
                                                    if (sgDESTINATIONfacet == 3)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 4)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 5)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }

                                                    }
                                                    // check direction of DESTINATION GATE, place energy tiles
                                                }
                                                else
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundNoTravel);
                                                    from.SendMessage(77, "The destination is in use, please wait a moment and try again.");
                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                                }
                                            }
                                        }
                                        m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                        m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                        m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                    }
                                }
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 8:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "You reset the dialing sequence");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = reset;
                                int sgTOaddress2 = reset;
                                int sgTOaddress3 = reset;
                                int sgTOaddress4 = reset;
                                int sgTOaddress5 = reset;
                                sgTOselectioncounter = 1;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 9:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 10:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "help");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.CloseGump(typeof(SGGumpHelp));
                                from.SendGump(new SGGumpHelp());
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 11:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                from.SendGump(new SGGumpActivatorRemoval(m_sgactivatordevice));
                            }
                            break;
                    }
                }
                #endregion selection3

                #region selection4
                else if (sgTOselectioncounter == 4)
                {
                    switch (info.ButtonID)
                    {
                        case 0:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid + 1;
                                if (sgTOfacetid >= 5)
                                {
                                    sgTOfacetid = 5;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 1:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid - 1;
                                if (sgTOfacetid <= 1)
                                {
                                    sgTOfacetid = 1;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 2:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress4 = 1;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 3:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress4 = 2;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 4:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress4 = 3;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 5:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress4 = 4;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 6:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress4 = 5;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 7:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButtonActivate);
                                from.CloseGump(typeof(SGGumpActivatorDevice));

                                // check I can go from here
                                bool stage1check = false;

                                for (int i = 0; i < SGCore.SGList.Count; i++)
                                {
                                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                                    {
                                        if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                        {
                                            // this gate
                                            fromIndex = i;
                                            if (!sge.SGEnergy)
                                            {
                                                // i can continue
                                                stage1check = true;
                                            }
                                            else
                                            {
                                                from.SendMessage(77, "Someone opened this gate before you, please wait a moment.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                        }
                                    }
                                }

                                if (stage1check)
                                {
                                    // find destination
                                    for (int i = 0; i < SGCore.SGList.Count; i++)
                                    {
                                        SGEntry sge = (SGEntry)SGCore.SGList[i];
                                        if (sge.SGFacetCode == sgTOfacetid && sge.SGAddressCode1 == sgTOaddress1 && sge.SGAddressCode2 == sgTOaddress2 && sge.SGAddressCode3 == sgTOaddress3 && sge.SGAddressCode4 == sgTOaddress4 && sge.SGAddressCode5 == sgTOaddress5)
                                        {
                                            // match
                                            if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                            {
                                                // Already here (Code selected was same code)
                                                from.SendMessage(77, "Your already there.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                            else
                                            {
                                                // match and different from where you are
                                                destIndex = i;

                                                sgDESTINATIONlocX = sge.SGX;
                                                sgDESTINATIONlocY = sge.SGY;
                                                sgDESTINATIONlocZ = sge.SGZ;
                                                sgDESTINATIONfacing = sge.SGFacing;

                                                sgDESTINATIONfacet = sge.SGFacetCode;
                                                sgDESTINATIONaddress1 = sge.SGAddressCode1;
                                                sgDESTINATIONaddress2 = sge.SGAddressCode2;
                                                sgDESTINATIONaddress3 = sge.SGAddressCode3;
                                                sgDESTINATIONaddress4 = sge.SGAddressCode4;
                                                sgDESTINATIONaddress5 = sge.SGAddressCode5;

                                                sgDESTINATIONbusyused = sge.SGBeingUsed;
                                                sgDESTINATIONbusyenergy = sge.SGEnergy;

                                                if (!sgDESTINATIONbusyenergy)
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundGoodToGo);

                                                    // setup energy bits
                                                    SGEntry sgeDestination = (SGEntry)SGCore.SGList[destIndex];
                                                    sgeDestination.SGEnergy = true; // turn ON energy bit at DESTINATION gate (stops outbound calls)
                                                    SGEntry sgeOrigin = (SGEntry)SGCore.SGList[fromIndex];
                                                    sgeOrigin.SGEnergy = true; // turn ON energy HERE (stops inbounds)

                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter

                                                    // place SGTeleport Tile into doorway TO THE DESTINATION Location (FROM HERE TO x,y,z)
                                                    if (sgDESTINATIONfacet == 1)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                    }

                                                    if (sgDESTINATIONfacet == 2)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                    }
                                                    if (sgDESTINATIONfacet == 3)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 4)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 5)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }

                                                    }
                                                    // check direction of DESTINATION GATE, place energy tiles
                                                }
                                                else
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundNoTravel);
                                                    from.SendMessage(77, "The destination is in use, please wait a moment and try again.");
                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                                }
                                            }
                                        }
                                        m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                        m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                        m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                    }
                                }
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 8:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "You reset the dialing sequence");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = reset;
                                int sgTOaddress2 = reset;
                                int sgTOaddress3 = reset;
                                int sgTOaddress4 = reset;
                                int sgTOaddress5 = reset;
                                sgTOselectioncounter = 1;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 9:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 10:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "help");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.CloseGump(typeof(SGGumpHelp));
                                from.SendGump(new SGGumpHelp());
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 11:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                from.SendGump(new SGGumpActivatorRemoval(m_sgactivatordevice));
                            }
                            break;
                    }
                }
                #endregion selection4

                #region selection5
                else if (sgTOselectioncounter == 5)
                {
                    switch (info.ButtonID)
                    {
                        case 0:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid + 1;
                                if (sgTOfacetid >= 5)
                                {
                                    sgTOfacetid = 5;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 1:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid - 1;
                                if (sgTOfacetid <= 1)
                                {
                                    sgTOfacetid = 1;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 2:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress5 = 1;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 3:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress5 = 2;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 4:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress5 = 3;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 5:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress5 = 4;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 6:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress5 = 5;
                                sgTOselectioncounter++;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 7:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButtonActivate);
                                from.CloseGump(typeof(SGGumpActivatorDevice));

                                // check I can go from here
                                bool stage1check = false;

                                for (int i = 0; i < SGCore.SGList.Count; i++)
                                {
                                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                                    {
                                        if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                        {
                                            // this gate
                                            fromIndex = i;
                                            if (!sge.SGEnergy)
                                            {
                                                // i can continue
                                                stage1check = true;
                                            }
                                            else
                                            {
                                                from.SendMessage(77, "Someone opened this gate before you, please wait a moment.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                        }
                                    }
                                }

                                if (stage1check)
                                {
                                    // find destination
                                    for (int i = 0; i < SGCore.SGList.Count; i++)
                                    {
                                        SGEntry sge = (SGEntry)SGCore.SGList[i];
                                        if (sge.SGFacetCode == sgTOfacetid && sge.SGAddressCode1 == sgTOaddress1 && sge.SGAddressCode2 == sgTOaddress2 && sge.SGAddressCode3 == sgTOaddress3 && sge.SGAddressCode4 == sgTOaddress4 && sge.SGAddressCode5 == sgTOaddress5)
                                        {
                                            // match
                                            if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                            {
                                                // Already here (Code selected was same code)
                                                from.SendMessage(77, "Your already there.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                            else
                                            {
                                                // match and different from where you are
                                                destIndex = i;

                                                sgDESTINATIONlocX = sge.SGX;
                                                sgDESTINATIONlocY = sge.SGY;
                                                sgDESTINATIONlocZ = sge.SGZ;
                                                sgDESTINATIONfacing = sge.SGFacing;

                                                sgDESTINATIONfacet = sge.SGFacetCode;
                                                sgDESTINATIONaddress1 = sge.SGAddressCode1;
                                                sgDESTINATIONaddress2 = sge.SGAddressCode2;
                                                sgDESTINATIONaddress3 = sge.SGAddressCode3;
                                                sgDESTINATIONaddress4 = sge.SGAddressCode4;
                                                sgDESTINATIONaddress5 = sge.SGAddressCode5;

                                                sgDESTINATIONbusyused = sge.SGBeingUsed;
                                                sgDESTINATIONbusyenergy = sge.SGEnergy;

                                                if (!sgDESTINATIONbusyenergy)
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundGoodToGo);

                                                    // setup energy bits
                                                    SGEntry sgeDestination = (SGEntry)SGCore.SGList[destIndex];
                                                    sgeDestination.SGEnergy = true; // turn ON energy bit at DESTINATION gate (stops outbound calls)
                                                    SGEntry sgeOrigin = (SGEntry)SGCore.SGList[fromIndex];
                                                    sgeOrigin.SGEnergy = true; // turn ON energy HERE (stops inbounds)

                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter

                                                    // place SGTeleport Tile into doorway TO THE DESTINATION Location (FROM HERE TO x,y,z)
                                                    if (sgDESTINATIONfacet == 1)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                    }

                                                    if (sgDESTINATIONfacet == 2)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                    }
                                                    if (sgDESTINATIONfacet == 3)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 4)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 5)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundNoTravel);
                                                    from.SendMessage(77, "The destination is in use, please wait a moment and try again.");
                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                                }
                                            }
                                        }
                                        m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                        m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                        m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                    }
                                }
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 8:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "You reset the dialing sequence");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = reset;
                                int sgTOaddress2 = reset;
                                int sgTOaddress3 = reset;
                                int sgTOaddress4 = reset;
                                int sgTOaddress5 = reset;
                                sgTOselectioncounter = 1;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 9:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 10:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "help");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.CloseGump(typeof(SGGumpHelp));
                                from.SendGump(new SGGumpHelp());
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 11:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                from.SendGump(new SGGumpActivatorRemoval(m_sgactivatordevice));
                            }
                            break;
                    }
                }
                #endregion selection5

                #region selection6
                else if (sgTOselectioncounter == 6)
                {
                    switch (info.ButtonID)
                    {
                        case 0:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid + 1;
                                if (sgTOfacetid >= 5)
                                {
                                    sgTOfacetid = 5;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 1:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                sgTOfacetid = sgTOfacetid - 1;
                                if (sgTOfacetid <= 1)
                                {
                                    sgTOfacetid = 1;
                                }
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 2:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.PublicOverheadMessage(MessageType.Whisper, 0, false, "Activate / Reset or Cancel");
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 3:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.PublicOverheadMessage(MessageType.Whisper, 0, false, "Activate / Reset or Cancel");
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 4:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.PublicOverheadMessage(MessageType.Whisper, 0, false, "Activate / Reset or Cancel");
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 5:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.PublicOverheadMessage(MessageType.Whisper, 0, false, "Activate / Reset or Cancel");
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 6:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.PublicOverheadMessage(MessageType.Whisper, 0, false, "Activate / Reset or Cancel");
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 7:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButtonActivate);
                                from.CloseGump(typeof(SGGumpActivatorDevice));

                                // check I can go from here
                                bool stage1check = false;

                                for (int i = 0; i < SGCore.SGList.Count; i++)
                                {
                                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                                    {
                                        if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                        {
                                            // this gate
                                            fromIndex = i;
                                            if (!sge.SGEnergy)
                                            {
                                                // i can continue
                                                stage1check = true;
                                            }
                                            else
                                            {
                                                from.SendMessage(77, "Someone opened this gate before you, please wait a moment.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                        }
                                    }
                                }

                                if (stage1check)
                                {
                                    // find destination
                                    for (int i = 0; i < SGCore.SGList.Count; i++)
                                    {
                                        SGEntry sge = (SGEntry)SGCore.SGList[i];
                                        if (sge.SGFacetCode == sgTOfacetid && sge.SGAddressCode1 == sgTOaddress1 && sge.SGAddressCode2 == sgTOaddress2 && sge.SGAddressCode3 == sgTOaddress3 && sge.SGAddressCode4 == sgTOaddress4 && sge.SGAddressCode5 == sgTOaddress5)
                                        {
                                            // match
                                            if (sge.SGFacetCode == m_sgactivatordevice.SGAFacetNumber && sge.SGAddressCode1 == m_sgactivatordevice.SGAAddressCode1 && sge.SGAddressCode2 == m_sgactivatordevice.SGAAddressCode2 && sge.SGAddressCode3 == m_sgactivatordevice.SGAAddressCode3 && sge.SGAddressCode4 == m_sgactivatordevice.SGAAddressCode4 && sge.SGAddressCode5 == m_sgactivatordevice.SGAAddressCode5)
                                            {
                                                // Already here (Code selected was same code)
                                                from.SendMessage(77, "Your already at the location you dialed.");
                                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                            }
                                            else
                                            {
                                                // match and different from where you are
                                                destIndex = i;

                                                sgDESTINATIONlocX = sge.SGX;
                                                sgDESTINATIONlocY = sge.SGY;
                                                sgDESTINATIONlocZ = sge.SGZ;
                                                sgDESTINATIONfacing = sge.SGFacing;

                                                sgDESTINATIONfacet = sge.SGFacetCode;
                                                sgDESTINATIONaddress1 = sge.SGAddressCode1;
                                                sgDESTINATIONaddress2 = sge.SGAddressCode2;
                                                sgDESTINATIONaddress3 = sge.SGAddressCode3;
                                                sgDESTINATIONaddress4 = sge.SGAddressCode4;
                                                sgDESTINATIONaddress5 = sge.SGAddressCode5;

                                                sgDESTINATIONbusyused = sge.SGBeingUsed;
                                                sgDESTINATIONbusyenergy = sge.SGEnergy;

                                                if (!sgDESTINATIONbusyenergy)
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundGoodToGo);
                                                    // No energy field at DESTINATION gate, set bits
                                                    SGEntry sgeDestination = (SGEntry)SGCore.SGList[destIndex];
                                                    sgeDestination.SGEnergy = true; // turn ON energy bit at DESTINATION gate (stops outbound calls)
                                                    SGEntry sgeOrigin = (SGEntry)SGCore.SGList[fromIndex];
                                                    sgeOrigin.SGEnergy = true; // turn ON energy HERE (stops inbounds)

                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter

                                                    // place SGTeleport Tile into doorway TO THE DESTINATION Location (FROM HERE TO x,y,z)
                                                    if (sgDESTINATIONfacet == 1)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Felucca);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Felucca);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Felucca);
                                                        }
                                                    }

                                                    if (sgDESTINATIONfacet == 2)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Trammel);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Trammel);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Trammel);
                                                        }
                                                    }
                                                    if (sgDESTINATIONfacet == 3)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Ilshenar);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Ilshenar);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Ilshenar);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 4)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Malas);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Malas);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Malas);
                                                        }

                                                    }
                                                    if (sgDESTINATIONfacet == 5)
                                                    {
                                                        // Tile FROM Here TO Destination
                                                        SGFieldTile SGTileFROM = new SGFieldTile(fromIndex, destIndex, new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                        SGTileFROM.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        // Tile FROM Destination To Here
                                                        SGFieldTile SGTileBACK = new SGFieldTile(fromIndex, destIndex, new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        SGTileBACK.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);

                                                        // ORIGIN ENERGY FIELD
                                                        if (m_sgactivatordevice.SGAFacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldEast SGEnergyTile2 = new SGEFieldEast(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        else if (m_sgactivatordevice.SGAFacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(fromIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile1.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 15), m_sgactivatordevice.Map);
                                                            SGEFieldSouth SGEnergyTile2 = new SGEFieldSouth(m_sgactivatordevice.Location, m_sgactivatordevice.Map);
                                                            SGEnergyTile2.MoveToWorld(new Point3D(m_sgactivatordevice.X - 2, m_sgactivatordevice.Y + 2, m_sgactivatordevice.Z + 5), m_sgactivatordevice.Map);
                                                        }
                                                        // DESTINATION ENERGY FIELD
                                                        if (sgDESTINATIONfacing == "E")
                                                        {
                                                            SGCore.SGEffectEastWest(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldEast SGEnergyTile1d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldEast SGEnergyTile2d = new SGEFieldEast(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }
                                                        else if (sgDESTINATIONfacing == "S")
                                                        {
                                                            SGCore.SGEffectNorthSouth(destIndex);
                                                            // place energy field in doorway
                                                            SGEFieldSouth SGEnergyTile1d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile1d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 5), Map.Tokuno);
                                                            SGEFieldSouth SGEnergyTile2d = new SGEFieldSouth(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ), Map.Tokuno);
                                                            SGEnergyTile2d.MoveToWorld(new Point3D(sgDESTINATIONlocX, sgDESTINATIONlocY, sgDESTINATIONlocZ + 15), Map.Tokuno);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundNoTravel);
                                                    from.SendMessage(77, "The destination is in use, please wait a moment and try again.");
                                                    m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                                    m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                                    m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                                }
                                            }
                                        }
                                        m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                        m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                        m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                    }
                                }
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 8:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "You reset the dialing sequence");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                int sgTOaddress1 = reset;
                                int sgTOaddress2 = reset;
                                int sgTOaddress3 = reset;
                                int sgTOaddress4 = reset;
                                int sgTOaddress5 = reset;
                                sgTOselectioncounter = 1;
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 9:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                            }
                            break;

                        case 10:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.SendMessage(77, "help");
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                from.CloseGump(typeof(SGGumpHelp));
                                from.SendGump(new SGGumpHelp());
                                from.SendGump(new SGGumpActivatorDevice(from, m_sgactivatordevice, sgTOselectioncounter, sgTOfacetid, sgTOaddress1, sgTOaddress2, sgTOaddress3, sgTOaddress4, sgTOaddress5));
                            }
                            break;

                        case 11:
                            {
                                Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, SGCore.SGSystemSoundButton);
                                from.CloseGump(typeof(SGGumpActivatorDevice));
                                m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                                m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                                m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                                from.SendGump(new SGGumpActivatorRemoval(m_sgactivatordevice));
                            }
                            break;
                    }
                }
            }
            #endregion selection6
        }
    }

    public class SGEFieldEast : SGEnergyFieldEast
    {
        public SGEFieldEast(Point3D from, Map map) : base(from, map)
        {
            Map = map;
            InternalTimer t = new InternalTimer(this);
            t.Start();
        }

        public SGEFieldEast(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            Delete();
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item) : base(TimeSpan.FromSeconds(SGCore.SGSystemGateTime))
            {
                Priority = TimerPriority.OneSecond;
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }

    public class SGEFieldSouth : SGEnergyFieldSouth
    {
        public SGEFieldSouth(Point3D from, Map map) : base(from, map)
        {
            Map = map;
            InternalTimer t = new InternalTimer(this);
            t.Start();
        }

        public SGEFieldSouth(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            Delete();
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item) : base(TimeSpan.FromSeconds(SGCore.SGSystemGateTime))
            {
                Priority = TimerPriority.OneSecond;
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }

    public class SGFieldTile : SGTransportTile
    {
        private int m_FromID;
        private int m_ToID;

        public int FromID
        {
            get { return m_FromID; }
            set { m_FromID = value; InvalidateProperties(); }
        }

        public int ToID
        {
            get { return m_ToID; }
            set { m_ToID = value; InvalidateProperties(); }
        }

        public SGFieldTile(int frm, int des, Point3D from, Map map) : base(from, map)
        {
            m_FromID = frm;
            m_ToID = des;
            Map = map;
            InternalTimer t = new InternalTimer(this, this);
            t.Start();
        }

        public SGFieldTile(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            Delete();
        }

        public class InternalTimer : Timer
        {
            private SGFieldTile m_sgfieldtile;
            private Item m_Item;

            public InternalTimer(SGFieldTile m, Item item) : base(TimeSpan.FromSeconds(SGCore.SGSystemGateTime))
            {
                m_sgfieldtile = m;
                m_Item = item;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                SGEntry sgorigin = (SGEntry)SGCore.SGList[m_sgfieldtile.FromID];
                SGEntry sgdest = (SGEntry)SGCore.SGList[m_sgfieldtile.ToID];

                sgorigin.SGEnergy = false;
                sgdest.SGEnergy = false;

                m_Item.Delete();
            }
        }
    }
}