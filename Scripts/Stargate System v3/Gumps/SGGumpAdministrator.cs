using System;
using System.Collections;

using Server;
using System.Text;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.SG
{
    public class SGGumpAdministrator : Gump
    {
        public int sgMAX = 15625; // Amount of Unique gate addresses per server
        public int sggatecounter;
        public int sgaddonid;
        public string sgdir;
        public string sgplatname;
        public int facetid;
        public int addcode1;
        public int addcode2;
        public int addcode3;
        public int addcode4;
        public int addcode5;

        public SGGumpAdministrator(Mobile from, int sggatecounterS, int sgaddonidS, string sgdirS, string sgplatnameS, int facetidS, int addcode1S, int addcode2S, int addcode3S, int addcode4S, int addcode5S) : base(30, 30)
        {
            sggatecounter = sggatecounterS;
            sgaddonid = sgaddonidS;
            sgdir = sgdirS;
            sgplatname = sgplatnameS;
            facetid = facetidS;
            addcode1 = addcode1S;
            addcode2 = addcode2S;
            addcode3 = addcode3S;
            addcode4 = addcode4S;
            addcode5 = addcode5S;

            if (sgdir == "S")
            {
                sgdir = "South";
            }
            if (sgdir == "E")
            {
                sgdir = "East";
            }

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 710, 200, 9270);

            // ADD LABELS TO THE GUMP
            this.AddLabel(235, 10, 1153, @"Stargate System v3.0 Administration Panel");
            this.AddLabel(15, 30, 54, @"Stargates On This Server :");
            this.AddLabel(250, 30, 54, @"Stargate Locations Avaliable :");
            this.AddLabel(15, 50, 54, @"Current Facet / Map :");
            this.AddLabel(15, 70, 54, @"Platform (Addon Style/Type) :");
            this.AddLabel(15, 90, 54, @"Platform Direction (South/East) :");
            this.AddLabel(15, 110, 54, @"Platform Location Name :");
            this.AddLabel(270, 70, 54, @"Add Gate Now");
            this.AddLabel(485, 50, 1153, @"System Control Options");
            this.AddLabel(465, 70, 80, @"Enable / Disable All Shard Stargates");
            this.AddLabel(465, 114, 33, @"Force XML Address File Save");
            this.AddLabel(465, 136, 80, @"Force XML Address File Re-Load");
            this.AddLabel(465, 158, 80, @"Exit Administration Panel");
            this.AddLabel(465, 92, 80, @"Generate HTML Address File");
            this.AddLabel(510, 30, 54, @"Status :");
            this.AddLabel(618, 10, 1160, @"Information");

            // Add values to the gump, passed each time the gump gets re-sent
            this.AddLabel(190, 30, 64, SGCore.SGList.Count.ToString());
            this.AddLabel(450, 30, 64, (sgMAX - SGCore.SGList.Count).ToString());
            this.AddLabel(160, 50, 90, from.Map.ToString());
            this.AddLabel(215, 70, 90, sgaddonid.ToString());
            this.AddLabel(240, 90, 90, sgdir);
            this.AddTextEntry(180, 110, 213, 20, 90, 21, sgplatname); // NAME INPUT BOX

            // Add digit for each selected location dial code
            this.AddLabel(40, 165, 90, addcode1.ToString());
            this.AddLabel(100, 165, 90, addcode2.ToString());
            this.AddLabel(160, 165, 90, addcode3.ToString());
            this.AddLabel(220, 165, 90, addcode4.ToString());
            this.AddLabel(280, 165, 90, addcode5.ToString());

            // Draw each symbol based on the dial code values
            this.AddItem(20, 130, addcode1 * 3 - 3 + 3676, 1153);
            this.AddItem(80, 130, addcode2 * 3 - 3 + 3676, 1153);
            this.AddItem(140, 130, addcode3 * 3 - 3 + 3676, 1153);
            this.AddItem(200, 130, addcode4 * 3 - 3 + 3676, 1153);
            this.AddItem(260, 130, addcode5 * 3 - 3 + 3676, 1153);

            // Check system status and display correct value
            if (!SGCore.SGSystemEnabled)
            {
                this.AddLabel(570, 30, 33, @"Disabled");
            }
            else
            {
                this.AddLabel(570, 30, 64, @"Enabled");
            }

            // Button Controls
            this.AddButton(226, 72, 5603, 5607, 1, GumpButtonType.Reply, 0); // Platform Addon -1
            this.AddButton(247, 72, 5601, 5605, 2, GumpButtonType.Reply, 0); // Platform Addon +1
            this.AddButton(283, 92, 5603, 5607, 3, GumpButtonType.Reply, 0); // Platform Direction -1
            this.AddButton(304, 92, 5601, 5605, 4, GumpButtonType.Reply, 0); // Platform Direction +1

            this.AddButton(15, 165, 5603, 5607, 5, GumpButtonType.Reply, 0); // Dial code 1 -1
            this.AddButton(55, 165, 5601, 5605, 6, GumpButtonType.Reply, 0); // Dial code 1 +1
            this.AddButton(75, 165, 5603, 5607, 7, GumpButtonType.Reply, 0); // Dial code 2 -1
            this.AddButton(115, 165, 5601, 5605, 8, GumpButtonType.Reply, 0); // Dial code 2 +1
            this.AddButton(135, 165, 5603, 5607, 9, GumpButtonType.Reply, 0); // Dial code 3 -1
            this.AddButton(175, 165, 5601, 5605, 10, GumpButtonType.Reply, 0); // Dial code 3 +1
            this.AddButton(195, 165, 5603, 5607, 11, GumpButtonType.Reply, 0); // Dial code 4 -1
            this.AddButton(235, 165, 5601, 5605, 12, GumpButtonType.Reply, 0); // Dial code 4 +1
            this.AddButton(255, 165, 5603, 5607, 13, GumpButtonType.Reply, 0); // Dial code 5 -1
            this.AddButton(295, 165, 5601, 5605, 14, GumpButtonType.Reply, 0); // Dial code 5 +1

            this.AddButton(360, 69, 4023, 4025, 15, GumpButtonType.Reply, 0); // Add Gate
            this.AddButton(430, 69, 4026, 4028, 16, GumpButtonType.Reply, 0); // Enable / Disable
            this.AddButton(430, 113, 4029, 4031, 17, GumpButtonType.Reply, 0); // SAVE XML
            this.AddButton(430, 135, 4011, 4013, 18, GumpButtonType.Reply, 0); // LOAD XML
            this.AddButton(430, 157, 4017, 4019, 19, GumpButtonType.Reply, 0); // EXIT

            this.AddButton(595, 12, 22153, 22155, 20, GumpButtonType.Reply, 0); // HELP
            this.AddButton(430, 91, 4029, 4031, 21, GumpButtonType.Reply, 0); // Generate HTML file
        }

        private string GetString(RelayInfo info, int id)
        {
            TextRelay t = info.GetTextEntry(id);
            return (t == null ? null : t.Text.Trim());
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            
            bool CanBeUsed = SGCore.SGSystemEnabled;
            bool BeingUsed = false;
            bool Energy = false;
            bool HideAdd = SGCore.SGSystemHideGate;
            string DiscoverName = "NooneYet"; // Just a temp filler (Avoid a null string)

            // Get value contained in the text input box
            string sgplatname = GetString(info, 21);

            switch (info.ButtonID)
            {
                case 1:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        sgaddonid = sgaddonid - 1;
                        if (sgaddonid <= 1)
                        {
                            sgaddonid = 1;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));   
                    }
                    break;

                case 2:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        sgaddonid = sgaddonid + 1;
                        if (sgaddonid >= SGCore.SGAddonStyles)
                        {
                            sgaddonid = SGCore.SGAddonStyles;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;

                case 3:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        if (sgdir == "East")
                        {
                            sgdir = "S";
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5)); 
                    }
                    break;

                case 4:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        if (sgdir == "South")
                        {
                            sgdir = "E";
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));      
                    }
                    break;

                case 5:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode1 = addcode1 - 1;
                        if (addcode1 <= 1)
                        {
                            addcode1 = 1;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;

                case 6:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode1 = addcode1 + 1;
                        if (addcode1 >= 5)
                        {
                            addcode1 = 5;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;

                case 7:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode2 = addcode2 - 1;
                        if (addcode2 <= 1)
                        {
                            addcode2 = 1;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));   
                    }
                    break;

                case 8:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode2 = addcode2 + 1;
                        if (addcode2 >= 5)
                        {
                            addcode2 = 5;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));     
                    }
                    break;

                case 9:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode3 = addcode3 - 1;
                        if (addcode3 <= 1)
                        {
                            addcode3 = 1;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));      
                    }
                    break;

                case 10:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode3 = addcode3 + 1;
                        if (addcode3 >= 5)
                        {
                            addcode3 = 5;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));   
                    }
                    break;

                case 11:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode4 = addcode4 - 1;
                        if (addcode4 <= 1)
                        {
                            addcode4 = 1;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));   
                    }
                    break;

                case 12:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode4 = addcode4 + 1;
                        if (addcode4 >= 5)
                        {
                            addcode4 = 5;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));   
                    }
                    break;

                case 13:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode5 = addcode5 - 1;
                        if (addcode5 <= 1)
                        {
                            addcode5 = 1;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));      
                    }
                    break;

                case 14:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        addcode5 = addcode5 + 1;
                        if (addcode5 >= 5)
                        {
                            addcode5 = 5;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;

                case 15:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));

                        bool dupedgate = false;

                        // Check which map the person adding a gate is actually on
                        string whichmap = Convert.ToString(from.Map);
                        if (whichmap == "Felucca")
                        {
                            facetid = 1;
                        }
                        if (whichmap == "Trammel")
                        {
                            facetid = 2;
                        }
                        if (whichmap == "Ilshenar")
                        {
                            facetid = 3;
                        }
                        if (whichmap == "Malas")
                        {
                            facetid = 4;
                        }
                        if (whichmap == "Tokuno")
                        {
                            facetid = 5;
                        }

                        // Check for duplicated addresses in the array
                        for (int i = 0; i < SGCore.SGList.Count; i++)
                        {
                            SGEntry sge = (SGEntry)SGCore.SGList[i];
                            {
                                if (sge.SGFacetCode == facetid && sge.SGAddressCode1 == addcode1 && sge.SGAddressCode2 == addcode2 && sge.SGAddressCode3 == addcode3 && sge.SGAddressCode4 == addcode4 && sge.SGAddressCode5 == addcode5)
                                {
                                    dupedgate = true;
                                }
                            }
                        }

                        if (!dupedgate)
                        {
                            

                            if (sgdir == "South" && sgaddonid ==1)
                            {
                                sgdir = "S";
                                Item SGASouth = new SGLocationAddon1South();
                                SGASouth.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
                            }
                            if (sgdir == "East" && sgaddonid == 1)
                            {
                                sgdir = "E";
                                Item SGAEast = new SGLocationAddon1East();
                                SGAEast.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
                            }

                            if (sgdir == "South" && sgaddonid == 2)
                            {
                                sgdir = "S";
                                Item SGASouth = new SGLocationAddon2South();
                                SGASouth.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
                            }
                            if (sgdir == "East" && sgaddonid == 2)
                            {
                                sgdir = "E";
                                Item SGAEast = new SGLocationAddon2East();
                                SGAEast.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
                            }

                            if (sgdir == "South" && sgaddonid == 3)
                            {
                                sgdir = "S";
                                Item SGASouth = new SGLocationAddon3South();
                                SGASouth.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
                            }
                            if (sgdir == "East" && sgaddonid == 3)
                            {
                                sgdir = "E";
                                Item SGAEast = new SGLocationAddon3East();
                                SGAEast.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
                            }

                            Item SGACrystal = new SGActivatorDevice(from.X, from.Y, from.Z, sgdir, sgaddonid, CanBeUsed, BeingUsed, Energy, HideAdd, DiscoverName, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5);
                            SGACrystal.MoveToWorld(new Point3D(from.X + 2, from.Y - 2, from.Z), from.Map);

                            // Add facet symbol to world for this stargate control crystal
                            if (facetid == 1) { Item sgFIDsym = new SGFacetSymbol1(); sgFIDsym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y - 2, from.Location.Z), from.Map); }
                            if (facetid == 2) { Item sgFIDsym = new SGFacetSymbol2(); sgFIDsym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y - 2, from.Location.Z), from.Map); }
                            if (facetid == 3) { Item sgFIDsym = new SGFacetSymbol3(); sgFIDsym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y - 2, from.Location.Z), from.Map); }
                            if (facetid == 4) { Item sgFIDsym = new SGFacetSymbol4(); sgFIDsym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y - 2, from.Location.Z), from.Map); }
                            if (facetid == 5) { Item sgFIDsym = new SGFacetSymbol5(); sgFIDsym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y - 2, from.Location.Z), from.Map); }

                            // Add 1st CODE symbol to the world
                            if (addcode1 == 1) { Item sgAC1sym = new SGAddressSymbol1(); sgAC1sym.MoveToWorld(new Point3D(from.Location.X - 2, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode1 == 2) { Item sgAC1sym = new SGAddressSymbol2(); sgAC1sym.MoveToWorld(new Point3D(from.Location.X - 2, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode1 == 3) { Item sgAC1sym = new SGAddressSymbol3(); sgAC1sym.MoveToWorld(new Point3D(from.Location.X - 2, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode1 == 4) { Item sgAC1sym = new SGAddressSymbol4(); sgAC1sym.MoveToWorld(new Point3D(from.Location.X - 2, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode1 == 5) { Item sgAC1sym = new SGAddressSymbol5(); sgAC1sym.MoveToWorld(new Point3D(from.Location.X - 2, from.Location.Y + 2, from.Location.Z), from.Map); }

                            // Add 2nd CODE symbol to the world
                            if (addcode2 == 1) { Item sgAC2sym = new SGAddressSymbol1(); sgAC2sym.MoveToWorld(new Point3D(from.Location.X - 1, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode2 == 2) { Item sgAC2sym = new SGAddressSymbol2(); sgAC2sym.MoveToWorld(new Point3D(from.Location.X - 1, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode2 == 3) { Item sgAC2sym = new SGAddressSymbol3(); sgAC2sym.MoveToWorld(new Point3D(from.Location.X - 1, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode2 == 4) { Item sgAC2sym = new SGAddressSymbol4(); sgAC2sym.MoveToWorld(new Point3D(from.Location.X - 1, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode2 == 5) { Item sgAC2sym = new SGAddressSymbol5(); sgAC2sym.MoveToWorld(new Point3D(from.Location.X - 1, from.Location.Y + 2, from.Location.Z), from.Map); }

                            // Add 3rd CODE symbol to the world
                            if (addcode3 == 1) { Item sgAC3sym = new SGAddressSymbol1(); sgAC3sym.MoveToWorld(new Point3D(from.Location.X, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode3 == 2) { Item sgAC3sym = new SGAddressSymbol2(); sgAC3sym.MoveToWorld(new Point3D(from.Location.X, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode3 == 3) { Item sgAC3sym = new SGAddressSymbol3(); sgAC3sym.MoveToWorld(new Point3D(from.Location.X, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode3 == 4) { Item sgAC3sym = new SGAddressSymbol4(); sgAC3sym.MoveToWorld(new Point3D(from.Location.X, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode3 == 5) { Item sgAC3sym = new SGAddressSymbol5(); sgAC3sym.MoveToWorld(new Point3D(from.Location.X, from.Location.Y + 2, from.Location.Z), from.Map); }

                            // Add 4th CODE symbol to the world
                            if (addcode4 == 1) { Item sgAC4sym = new SGAddressSymbol1(); sgAC4sym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode4 == 2) { Item sgAC4sym = new SGAddressSymbol2(); sgAC4sym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode4 == 3) { Item sgAC4sym = new SGAddressSymbol3(); sgAC4sym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode4 == 4) { Item sgAC4sym = new SGAddressSymbol4(); sgAC4sym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode4 == 5) { Item sgAC4sym = new SGAddressSymbol5(); sgAC4sym.MoveToWorld(new Point3D(from.Location.X + 1, from.Location.Y + 2, from.Location.Z), from.Map); }

                            // Add 5th CODE symbol to the world
                            if (addcode5 == 1) { Item sgAC5sym = new SGAddressSymbol1(); sgAC5sym.MoveToWorld(new Point3D(from.Location.X + 2, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode5 == 2) { Item sgAC5sym = new SGAddressSymbol2(); sgAC5sym.MoveToWorld(new Point3D(from.Location.X + 2, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode5 == 3) { Item sgAC5sym = new SGAddressSymbol3(); sgAC5sym.MoveToWorld(new Point3D(from.Location.X + 2, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode5 == 4) { Item sgAC5sym = new SGAddressSymbol4(); sgAC5sym.MoveToWorld(new Point3D(from.Location.X + 2, from.Location.Y + 2, from.Location.Z), from.Map); }
                            if (addcode5 == 5) { Item sgAC5sym = new SGAddressSymbol5(); sgAC5sym.MoveToWorld(new Point3D(from.Location.X + 2, from.Location.Y + 2, from.Location.Z), from.Map); }

                            SGCore.SGList.Add(new SGEntry(from.X, from.Y, from.Z, sgdir, sgaddonid, SGCore.SGSystemEnabled, false, false, SGCore.SGSystemHideGate, "Nobody", sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                            from.Z = from.Z + 5;
                            from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));

                            if (!SGCore.SGSystemEnabled)
                            {
                                for (int i = 0; i < SGCore.SGList.Count; i++)
                                {
                                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                                    {
                                        sge.SGCanBeUsed = false;
                                        sge.SGBeingUsed = false;
                                        sge.SGEnergy = false;
                                    }
                                }
                                // Hue Control Crystals To DISABLED
                                ArrayList SGADevice = new ArrayList();
                                foreach (Item item in World.Items.Values)
                                {
                                    if (item is SGActivatorDevice)
                                        SGADevice.Add(item);
                                }

                                foreach (Item item in SGADevice)
                                    (item).Hue = 39;
                            }
                            else if (SGCore.SGSystemEnabled)
                            {
                                for (int i = 0; i < SGCore.SGList.Count; i++)
                                {
                                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                                    {
                                        sge.SGCanBeUsed = true;
                                        sge.SGBeingUsed = false;
                                        sge.SGEnergy = false;
                                    }
                                }
                                // Hue Control Crystals To ENABLED
                                ArrayList SGADevice = new ArrayList();
                                foreach (Item item in World.Items.Values)
                                {
                                    if (item is SGActivatorDevice)
                                        SGADevice.Add(item);
                                }

                                foreach (Item item in SGADevice)
                                    (item).Hue = 2963;
                            }
                        }
                        else
                        {
                            // duped gate address
                            from.SendMessage(88, "ABORTED !!!, This gate address is already used on this facet, Try a different address for your gate.");
                            from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                        }
                    }
                    break;

                case 16:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));

                        if (!SGCore.SGSystemEnabled)
                        {
                            SGCore.SGSystemEnabled = true;
                            from.SendMessage(77, "Stargate System enabled.");
                            for (int i = 0; i < SGCore.SGList.Count; i++)
                            {
                                SGEntry sge = (SGEntry)SGCore.SGList[i];
                                {
                                    sge.SGCanBeUsed = true;
                                    sge.SGBeingUsed = false;
                                    sge.SGEnergy = false;
                                }
                            }
                            // Hue Control Crystals To NORMAL
                            ArrayList SGADevice = new ArrayList();
                            foreach (Item item in World.Items.Values)
                            {
                                if (item is SGActivatorDevice)
                                    SGADevice.Add(item);
                            }

                            foreach (Item item in SGADevice)
                                (item).Hue = 2963;
                        }
                        else
                        {
                            SGCore.SGSystemEnabled = false;
                            from.SendMessage(77, "Stargate System disabled, any open gates will close down after they are used.");
                            for (int i = 0; i < SGCore.SGList.Count; i++)
                            {
                                SGEntry sge = (SGEntry)SGCore.SGList[i];
                                {
                                    sge.SGCanBeUsed = false;
                                    sge.SGBeingUsed = false;
                                    sge.SGEnergy = false;
                                }
                            }
                            // Hue Control Crystals To DISABLED
                            ArrayList SGADevice = new ArrayList();
                            foreach (Item item in World.Items.Values)
                            {
                                if (item is SGActivatorDevice)
                                    SGADevice.Add(item);
                            }

                            foreach (Item item in SGADevice)
                                (item).Hue = 39;
                        }
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;
                case 17:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        from.SendMessage(77, "Saving XML File, Overwriting old XML file...");
                        SGCore.SGTriggerSave();
                        from.SendMessage(77, "Save Done.");
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;

                case 18:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));
                        SGCore.SGTriggerLoad();
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;

                case 19:
                    {
                        from.SendMessage(77, "Exit Stargate Admin");
                        from.CloseGump(typeof(SGGumpAdministrator));
                    }
                    break;

                case 20:
                    {
                        from.SendMessage(77, "Administrator Help & Information Option Selected...");
                        from.CloseGump(typeof(SGGumpAdministratorInfo));
                        from.SendGump(new SGGumpAdministratorInfo());

                        from.CloseGump(typeof(SGGumpAdministrator));
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;

                case 21:
                    {
                        from.CloseGump(typeof(SGGumpAdministrator));

                        from.SendMessage(77, "Generate HTML Address File...");
                        SGCore.SGGenHTML();
                        from.SendMessage(77, "Done.");
                        
                        from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
                    }
                    break;
            }
        }
    }
}