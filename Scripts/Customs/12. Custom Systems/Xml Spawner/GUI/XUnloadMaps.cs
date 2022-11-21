using System;
using System.Collections;
using System.Xml;
using Server.Commands;
using Server.Gumps;
using Server.Network;
using Server.Engines.XmlSpawner2;

namespace Server.Commands
{
    public class XUnload
    {
        public XUnload()
        {
        }

        public static void Initialize() 
        {
            CommandSystem.Register("UnloadTrammel", AccessLevel.Administrator, new CommandEventHandler(UnloadTrammel_OnCommand));
            CommandSystem.Register("UnloadMalas", AccessLevel.Administrator, new CommandEventHandler(UnloadMalas_OnCommand));
            CommandSystem.Register("UnloadIlshenar", AccessLevel.Administrator, new CommandEventHandler(UnloadIlshenar_OnCommand));
            CommandSystem.Register("UnloadTokuno", AccessLevel.Administrator, new CommandEventHandler(UnloadTokuno_OnCommand));
            CommandSystem.Register("UnloadFelucca", AccessLevel.Administrator, new CommandEventHandler(UnloadFelucca_OnCommand));
            CommandSystem.Register("UnloadTermur", AccessLevel.Administrator, new CommandEventHandler(UnloadTermur_OnCommand));
        }

        [Usage("[Unloadtrammel")]
        [Description("Unload Trammel maps with a menu.")] 
        private static void UnloadTrammel_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new XUnloadTrammelGump(e));
        }

        [Usage("[Unloadfelucca")]
        [Description("Unload Felucca maps with a menu.")] 
        private static void UnloadFelucca_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new XUnloadFeluccaGump(e));
        }

        [Usage("[Unloadmalas")]
        [Description("Unload Malas maps with a menu.")] 
        private static void UnloadMalas_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new XUnloadMalasGump(e));
        }

        [Usage("[Unloadilshenar")]
        [Description("Unload Ilshenar maps with a menu.")] 
        private static void UnloadIlshenar_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new XUnloadIlshenarGump(e));
        }

        [Usage("[Unloadtokuno")]
        [Description("Unload Tokuno maps with a menu.")] 
        private static void UnloadTokuno_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new XUnloadTokunoGump(e));
        }

        [Usage("[Unloadtermur")]
        [Description("Unload Termur maps with a menu.")]
        private static void UnloadTermur_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new XUnloadTermurGump(e));
        }
    }
}

namespace Server.Gumps
{
    public class XUnloadTrammelGump : Gump
    {
        private readonly CommandEventArgs m_CommandEventArgs;
        public XUnloadTrammelGump(CommandEventArgs e)
            : base(50,50)
        {
            this.m_CommandEventArgs = e;
            this.Closable = true;
            this.Dragable = true;

            this.AddPage(1);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "TRAMMEL");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Blighted Grove");
            this.AddLabel(35, 76, 200, "Britain Sewer");
            this.AddLabel(35, 101, 200, "Covetous");
            this.AddLabel(35, 126, 200, "Deceit");
            this.AddLabel(35, 151, 200, "Despise");
            this.AddLabel(35, 176, 200, "Destard");
            this.AddLabel(35, 201, 200, "Fire");
            this.AddLabel(35, 226, 200, "Graveyards");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 101);
            this.AddCheck(182, 73, 210, 211, false, 102);
            this.AddCheck(182, 98, 210, 211, false, 103);
            this.AddCheck(182, 123, 210, 211, false, 104);
            this.AddCheck(182, 148, 210, 211, false, 105);
            this.AddCheck(182, 173, 210, 211, false, 106);
            this.AddCheck(182, 198, 210, 211, false, 107);
            this.AddCheck(182, 223, 210, 211, false, 108);

            this.AddLabel(110, 255, 200, "1/4");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2);

            this.AddPage(2);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "TRAMMEL");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Hythloth");
            this.AddLabel(35, 76, 200, "Ice");
            this.AddLabel(35, 101, 200, "Lost Lands");
            this.AddLabel(35, 126, 200, "Orc Caves");
            this.AddLabel(35, 151, 200, "Outdoors");
            this.AddLabel(35, 176, 200, "Painted Caves");
            this.AddLabel(35, 201, 200, "Palace of Paroxysmus");
            this.AddLabel(35, 226, 200, "Prism of Light");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 109);
            this.AddCheck(182, 73, 210, 211, false, 110);
            this.AddCheck(182, 98, 210, 211, false, 111);
            this.AddCheck(182, 123, 210, 211, false, 112);
            this.AddCheck(182, 148, 210, 211, false, 113);
            this.AddCheck(182, 173, 210, 211, false, 114);
            this.AddCheck(182, 198, 210, 211, false, 115);
            this.AddCheck(182, 223, 210, 211, false, 116);

            this.AddLabel(110, 255, 200, "2/4");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 3);
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1);

            this.AddPage(3);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "TRAMMEL");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Sanctuary");
            this.AddLabel(35, 76, 200, "Sea Life");
            this.AddLabel(35, 101, 200, "Shame");
            this.AddLabel(35, 126, 200, "Solen Hive");
            this.AddLabel(35, 151, 200, "Terathan Keep");
            this.AddLabel(35, 176, 200, "Towns Life");
            this.AddLabel(35, 201, 200, "Towns People");
            this.AddLabel(35, 226, 200, "Trinsic Passage");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 117);
            this.AddCheck(182, 73, 210, 211, false, 118);
            this.AddCheck(182, 98, 210, 211, false, 119);
            this.AddCheck(182, 123, 210, 211, false, 120);
            this.AddCheck(182, 148, 210, 211, false, 121);
            this.AddCheck(182, 173, 210, 211, false, 122);
            this.AddCheck(182, 198, 210, 211, false, 123);
            this.AddCheck(182, 223, 210, 211, false, 124);

            this.AddLabel(110, 255, 200, "3/4");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 4);
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 2);

            this.AddPage(4);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "TRAMMEL");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 97, 10003);
            this.AddImageTiled(163, 25, 2, 97, 10003);
            this.AddImageTiled(218, 25, 2, 97, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            //AddImageTiled( 20, 145, 200, 2, 10001 );
            //AddImageTiled( 20, 170, 200, 2, 10001 );
            //AddImageTiled( 20, 195, 200, 2, 10001 );
            //AddImageTiled( 20, 220, 200, 2, 10001 );
            //AddImageTiled( 20, 245, 200, 2, 10001 );

            //Map names
            this.AddLabel(35, 51, 200, "Vendors");
            this.AddLabel(35, 76, 200, "Wild Life");
            this.AddLabel(35, 101, 200, "Wrong");
            //AddLabel( 35, 126, 200, "28" );
            //AddLabel( 35, 151, 200, "29" );
            //AddLabel( 35, 176, 200, "30" );
            //AddLabel( 35, 201, 200, "31" );
            //AddLabel( 35, 226, 200, "32" );

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 125);
            this.AddCheck(182, 73, 210, 211, false, 126);
            this.AddCheck(182, 98, 210, 211, false, 127);
            //AddCheck( 182, 123, 210, 211, false, 128 );
            //AddCheck( 182, 148, 210, 211, false, 129 );
            //AddCheck( 182, 173, 210, 211, false, 130 );
            //AddCheck( 182, 198, 210, 211, false, 131 );
            //AddCheck( 182, 223, 210, 211, false, 132 );

            this.AddLabel(110, 255, 200, "4/4");
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 3);

            //Ok, Cancel
            this.AddButton(55, 280, 247, 249, 1, GumpButtonType.Reply, 0);
            this.AddButton(125, 280, 241, 243, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch( info.ButtonID )
            {
                case 0: // Closed or Cancel
                    {
                        return;
                    }
                default:
                    {
                        // Make sure that the OK, button was pressed
                        if (info.ButtonID == 1)
                        {
                            // Get the array of switches selected
                            ArrayList Selections = new ArrayList(info.Switches);
                            string prefix = Server.Commands.CommandSystem.Prefix;

                            // Now unloading any selected maps

                            if (Selections.Contains(101) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/BlightedGrove.xml", prefix));
                            }
                            if (Selections.Contains(102) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/BritainSewer.xml", prefix));
                            }
                            if (Selections.Contains(103) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Covetous.xml", prefix));
                            }
                            if (Selections.Contains(104) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Deceit.xml", prefix));
                            }
                            if (Selections.Contains(105) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Despise.xml", prefix));
                            }
                            if (Selections.Contains(106) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Destard.xml", prefix));
                            }
                            if (Selections.Contains(107) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Fire.xml", prefix));
                            }
                            if (Selections.Contains(108) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Graveyards.xml", prefix));
                            }
                            if (Selections.Contains(109) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Hythloth.xml", prefix));
                            }
                            if (Selections.Contains(110) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Ice.xml", prefix));
                            }
                            if (Selections.Contains(111) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/LostLands.xml", prefix));
                            }
                            if (Selections.Contains(112) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/OrcCaves.xml", prefix));
                            }
                            if (Selections.Contains(113) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Outdoors.xml", prefix));
                            }
                            if (Selections.Contains(114) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/PaintedCaves.xml", prefix));
                            }
                            if (Selections.Contains(115) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/PalaceOfParoxysmus.xml", prefix));
                            }
                            if (Selections.Contains(116) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/PrismOfLight.xml", prefix));
                            }
                            if (Selections.Contains(117) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Sanctuary.xml", prefix));
                            }
                            if (Selections.Contains(118) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/SeaLife.xml", prefix));
                            }
                            if (Selections.Contains(119) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Shame.xml", prefix));
                            }
                            if (Selections.Contains(120) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/SolenHive.xml", prefix));
                            }
                            if (Selections.Contains(121) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/TerathanKeep.xml", prefix));
                            }
                            if (Selections.Contains(122) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/TownsLife.xml", prefix));
                            }
                            if (Selections.Contains(123) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/TownsPeople.xml", prefix));
                            }
                            if (Selections.Contains(124) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/TrinsicPassage.xml", prefix));
                            }
                            if (Selections.Contains(125) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Vendors.xml", prefix));
                            }
                            if (Selections.Contains(126) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/WildLife.xml", prefix));
                            }
                            if (Selections.Contains(127) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Trammel/Wrong.xml", prefix));
                            }
                        }

                        break;
                    }
            }
        }
    }

    public class XUnloadFeluccaGump : Gump
    {
        private readonly CommandEventArgs m_CommandEventArgs;
        public XUnloadFeluccaGump(CommandEventArgs e)
            : base(50,50)
        {
            this.m_CommandEventArgs = e;
            this.Closable = true;
            this.Dragable = true;

            this.AddPage(1);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "FELUCCA");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Blighted Grove");
            this.AddLabel(35, 76, 200, "Britain Sewer");
            this.AddLabel(35, 101, 200, "Covetous");
            this.AddLabel(35, 126, 200, "Deceit");
            this.AddLabel(35, 151, 200, "Despise");
            this.AddLabel(35, 176, 200, "Destard");
            this.AddLabel(35, 201, 200, "Fire");
            this.AddLabel(35, 226, 200, "Graveyards");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 101);
            this.AddCheck(182, 73, 210, 211, false, 102);
            this.AddCheck(182, 98, 210, 211, false, 103);
            this.AddCheck(182, 123, 210, 211, false, 104);
            this.AddCheck(182, 148, 210, 211, false, 105);
            this.AddCheck(182, 173, 210, 211, false, 106);
            this.AddCheck(182, 198, 210, 211, false, 107);
            this.AddCheck(182, 223, 210, 211, false, 108);

            this.AddLabel(110, 255, 200, "1/4");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2);

            this.AddPage(2);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "FELUCCA");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Hythloth");
            this.AddLabel(35, 76, 200, "Ice");
            this.AddLabel(35, 101, 200, "Khaldun");
            this.AddLabel(35, 126, 200, "Lost Lands");
            this.AddLabel(35, 151, 200, "Orc Caves");
            this.AddLabel(35, 176, 200, "Outdoors");
            this.AddLabel(35, 201, 200, "Painted Caves");
            this.AddLabel(35, 226, 200, "Palace of Paroxysmus");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 109);
            this.AddCheck(182, 73, 210, 211, false, 110);
            this.AddCheck(182, 98, 210, 211, false, 111);
            this.AddCheck(182, 123, 210, 211, false, 112);
            this.AddCheck(182, 148, 210, 211, false, 113);
            this.AddCheck(182, 173, 210, 211, false, 114);
            this.AddCheck(182, 198, 210, 211, false, 115);
            this.AddCheck(182, 223, 210, 211, false, 116);

            this.AddLabel(110, 255, 200, "2/4");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 3);
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1);

            this.AddPage(3);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "FELUCCA");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Prism of Light");
            this.AddLabel(35, 76, 200, "Sanctuary");
            this.AddLabel(35, 101, 200, "Sea Life");
            this.AddLabel(35, 126, 200, "Shame");
            this.AddLabel(35, 151, 200, "Solen Hive");
            this.AddLabel(35, 176, 200, "Terathan Keep");
            this.AddLabel(35, 201, 200, "Towns Life");
            this.AddLabel(35, 226, 200, "Towns People");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 117);
            this.AddCheck(182, 73, 210, 211, false, 118);
            this.AddCheck(182, 98, 210, 211, false, 119);
            this.AddCheck(182, 123, 210, 211, false, 120);
            this.AddCheck(182, 148, 210, 211, false, 121);
            this.AddCheck(182, 173, 210, 211, false, 122);
            this.AddCheck(182, 198, 210, 211, false, 123);
            this.AddCheck(182, 223, 210, 211, false, 124);

            this.AddLabel(110, 255, 200, "3/4");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 4);
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 2);

            this.AddPage(4);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "FELUCCA");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 122, 10003);
            this.AddImageTiled(163, 25, 2, 122, 10003);
            this.AddImageTiled(218, 25, 2, 122, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            //AddImageTiled( 20, 170, 200, 2, 10001 );
            //AddImageTiled( 20, 195, 200, 2, 10001 );
            //AddImageTiled( 20, 220, 200, 2, 10001 );
            //AddImageTiled( 20, 245, 200, 2, 10001 );

            //Map names
            this.AddLabel(35, 51, 200, "Trinsic Passage");
            this.AddLabel(35, 76, 200, "Vendors");
            this.AddLabel(35, 101, 200, "Wild Life");
            this.AddLabel(35, 126, 200, "Wrong");
            //AddLabel( 35, 151, 200, "29" );
            //AddLabel( 35, 176, 200, "30" );
            //AddLabel( 35, 201, 200, "31" );
            //AddLabel( 35, 226, 200, "32" );

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 125);
            this.AddCheck(182, 73, 210, 211, false, 126);
            this.AddCheck(182, 98, 210, 211, false, 127);
            this.AddCheck(182, 123, 210, 211, false, 128);
            //AddCheck( 182, 148, 210, 211, false, 129 );
            //AddCheck( 182, 173, 210, 211, false, 130 );
            //AddCheck( 182, 198, 210, 211, false, 131 );
            //AddCheck( 182, 223, 210, 211, false, 132 );

            this.AddLabel(110, 255, 200, "4/4");
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 3);

            //Ok, Cancel
            this.AddButton(55, 280, 247, 249, 1, GumpButtonType.Reply, 0);
            this.AddButton(125, 280, 241, 243, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch( info.ButtonID )
            {
                case 0: // Closed or Cancel
                    {
                        return;
                    }
                default:
                    {
                        // Make sure that the OK, button was pressed
                        if (info.ButtonID == 1)
                        {
                            // Get the array of switches selected
                            ArrayList Selections = new ArrayList(info.Switches);
                            string prefix = Server.Commands.CommandSystem.Prefix;

                            // Now unloading any selected maps

                            if (Selections.Contains(101) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/BlightedGrove.xml", prefix));
                            }
                            if (Selections.Contains(102) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/BritainSewer.xml", prefix));
                            }
                            if (Selections.Contains(103) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Covetous.xml", prefix));
                            }
                            if (Selections.Contains(104) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Deceit.xml", prefix));
                            }
                            if (Selections.Contains(105) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Despise.xml", prefix));
                            }
                            if (Selections.Contains(106) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Destard.xml", prefix));
                            }
                            if (Selections.Contains(107) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Fire.xml", prefix));
                            }
                            if (Selections.Contains(108) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Graveyards.xml", prefix));
                            }
                            if (Selections.Contains(109) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Hythloth.xml", prefix));
                            }
                            if (Selections.Contains(110) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Ice.xml", prefix));
                            }
                            if (Selections.Contains(111) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Khaldun.xml", prefix));
                            }
                            if (Selections.Contains(112) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/LostLands.xml", prefix));
                            }
                            if (Selections.Contains(113) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/OrcCaves.xml", prefix));
                            }
                            if (Selections.Contains(114) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Outdoors.xml", prefix));
                            }
                            if (Selections.Contains(115) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/PaintedCaves.xml", prefix));
                            }
                            if (Selections.Contains(116) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/PalaceOfParoxysmus.xml", prefix));
                            }
                            if (Selections.Contains(117) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/PrismOfLight.xml", prefix));
                            }
                            if (Selections.Contains(118) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Sanctuary.xml", prefix));
                            }
                            if (Selections.Contains(119) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/SeaLife.xml", prefix));
                            }
                            if (Selections.Contains(120) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Shame.xml", prefix));
                            }
                            if (Selections.Contains(121) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/SolenHive.xml", prefix));
                            }
                            if (Selections.Contains(122) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/TerathanKeep.xml", prefix));
                            }
                            if (Selections.Contains(123) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/TownsLife.xml", prefix));
                            }
                            if (Selections.Contains(124) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/TownsPeople.xml", prefix));
                            }
                            if (Selections.Contains(125) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/TrinsicPassage.xml", prefix));
                            }
                            if (Selections.Contains(126) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Vendors.xml", prefix));
                            }
                            if (Selections.Contains(127) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/WildLife.xml", prefix));
                            }
                            if (Selections.Contains(128) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Felucca/Wrong.xml", prefix));
                            }
                        }

                        break;
                    }
            }
        }
    }

    public class XUnloadIlshenarGump : Gump
    {
        private readonly CommandEventArgs m_CommandEventArgs;
        public XUnloadIlshenarGump(CommandEventArgs e)
            : base(50,50)
        {
            this.m_CommandEventArgs = e;
            this.Closable = true;
            this.Dragable = true;

            this.AddPage(1);

            //fundo cinza
            this.AddBackground(0, 0, 243, 310, 5054);
            //----------
            this.AddLabel(93, 2, 200, "ILSHENAR");
            //fundo branco
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);
            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");
            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(220, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Ancient Lair");
            this.AddLabel(35, 76, 200, "Ankh");
            this.AddLabel(35, 101, 200, "Blood");
            this.AddLabel(35, 126, 200, "Exodus");
            this.AddLabel(35, 151, 200, "Mushroom");
            this.AddLabel(35, 176, 200, "Outdoors");
            this.AddLabel(35, 201, 200, "Ratman cave");
            this.AddLabel(35, 226, 200, "Rock");

            //Options
            this.AddCheck(182, 48, 210, 211, false, 101);
            this.AddCheck(182, 73, 210, 211, false, 102);
            this.AddCheck(182, 98, 210, 211, false, 103);
            this.AddCheck(182, 123, 210, 211, false, 104);
            this.AddCheck(182, 148, 210, 211, false, 105);
            this.AddCheck(182, 173, 210, 211, false, 106);
            this.AddCheck(182, 198, 210, 211, false, 107);
            this.AddCheck(182, 223, 210, 211, false, 108);

            this.AddLabel(110, 255, 200, "1/2");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2);

            this.AddPage(2);

            //fundo cinza
            this.AddBackground(0, 0, 243, 310, 5054);
            //----------
            this.AddLabel(93, 2, 200, "ILSHENAR");
            //fundo branco
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);
            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");
            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 171, 10003);
            this.AddImageTiled(163, 25, 2, 171, 10003);
            this.AddImageTiled(220, 25, 2, 171, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            //AddImageTiled( 20, 220, 200, 2, 10001 );
            //AddImageTiled( 20, 245, 200, 2, 10001 );

            //----------
            this.AddLabel(35, 51, 200, "Sorcerers");
            this.AddLabel(35, 76, 200, "Spectre");
            this.AddLabel(35, 101, 200, "Towns");
            this.AddLabel(35, 126, 200, "Vendors");
            this.AddLabel(35, 151, 200, "Wisp");
            this.AddLabel(35, 176, 200, "Twisted Weald");
            //AddLabel( 35, 201, 200, "15" );
            //AddLabel( 35, 226, 200, "16" );

            //Options
            this.AddCheck(182, 48, 210, 211, false, 109);
            this.AddCheck(182, 73, 210, 211, false, 110);
            this.AddCheck(182, 98, 210, 211, false, 111);
            this.AddCheck(182, 123, 210, 211, false, 112);
            this.AddCheck(182, 148, 210, 211, false, 113);
            this.AddCheck(182, 173, 210, 211, false, 114);
            //AddCheck( 182, 198, 210, 211, false, 115 );
            //AddCheck( 182, 223, 210, 211, false, 116 );

            this.AddLabel(110, 255, 200, "2/2");
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1);

            //Ok, Cancel
            this.AddButton(55, 280, 247, 249, 1, GumpButtonType.Reply, 0);
            this.AddButton(125, 280, 241, 243, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch( info.ButtonID )
            {
                case 0: // Closed or Cancel
                    {
                        return;
                    }

                default:
                    {
                        // Make sure that the OK, button was pressed
                        if (info.ButtonID == 1)
                        {
                            // Get the array of switches selected
                            ArrayList Selections = new ArrayList(info.Switches);
                            string prefix = Server.Commands.CommandSystem.Prefix;

                            // Now unloading any selected maps

                            if (Selections.Contains(101) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Ancientlair.xml", prefix));
                            }
                            if (Selections.Contains(102) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Ankh.xml", prefix));
                            }
                            if (Selections.Contains(103) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Blood.xml", prefix));
                            }
                            if (Selections.Contains(104) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Exodus.xml", prefix));
                            }
                            if (Selections.Contains(105) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Mushroom.xml", prefix));
                            }
                            if (Selections.Contains(106) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Outdoors.xml", prefix));
                            }
                            if (Selections.Contains(107) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Ratmancave.xml", prefix));
                            }
                            if (Selections.Contains(108) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Rock.xml", prefix));
                            }
                            if (Selections.Contains(109) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Sorcerers.xml", prefix));
                            }
                            if (Selections.Contains(110) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Spectre.xml", prefix));
                            }
                            if (Selections.Contains(111) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Towns.xml", prefix));
                            }
                            if (Selections.Contains(112) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Vendors.xml", prefix));
                            }
                            if (Selections.Contains(113) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/Wisp.xml", prefix));
                            }
                            if (Selections.Contains(114) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Ilshenar/TwistedWeald.xml", prefix));
                            }
                        }

                        break;
                    }
            }
        }
    }

    public class XUnloadMalasGump : Gump
    {
        private readonly CommandEventArgs m_CommandEventArgs;
        public XUnloadMalasGump(CommandEventArgs e)
            : base(50,50)
        {
            this.m_CommandEventArgs = e;
            this.Closable = true;
            this.Dragable = true;

            this.AddPage(1);

            //fundo cinza
            //alt era 310
            this.AddBackground(0, 0, 243, 295, 5054);
            //----------
            this.AddLabel(100, 2, 200, "MALAS");
            //fundo branco
            //x, y, largura, altura, item
            //alt era 232
            this.AddImageTiled(10, 20, 220, 235, 3004);
            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");
            //colunas
            //x, y, comprimento, ?, item
            //comp era 222
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(220, 25, 2, 222, 10003);
            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);
            //Map names
            this.AddLabel(35, 51, 200, "Doom");
            this.AddLabel(35, 76, 200, "North");
            this.AddLabel(35, 101, 200, "OrcForts");
            this.AddLabel(35, 126, 200, "South");
            this.AddLabel(35, 151, 200, "Vendors");
            this.AddLabel(35, 176, 200, "Citadel");
            this.AddLabel(35, 201, 200, "Labyrinth");
            this.AddLabel(35, 226, 200, "Bedlam");

            //Options
            this.AddCheck(182, 48, 210, 211, false, 101);
            this.AddCheck(182, 73, 210, 211, false, 102);
            this.AddCheck(182, 98, 210, 211, false, 103);
            this.AddCheck(182, 123, 210, 211, false, 104);
            this.AddCheck(182, 148, 210, 211, false, 105);
            this.AddCheck(182, 173, 210, 211, false, 106);
            this.AddCheck(182, 198, 210, 211, false, 107);
            this.AddCheck(182, 223, 210, 211, false, 108);

            //Ok, Cancel
            // alt era 280
            this.AddButton(55, 265, 247, 249, 1, GumpButtonType.Reply, 0);
            this.AddButton(125, 265, 241, 243, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch( info.ButtonID )
            {
                case 0: // Closed or Cancel
                    {
                        return;
                    }

                default:
                    {
                        // Make sure that the OK, button was pressed
                        if (info.ButtonID == 1)
                        {
                            // Get the array of switches selected
                            ArrayList Selections = new ArrayList(info.Switches);
                            string prefix = Server.Commands.CommandSystem.Prefix;

                            // Now unloading any selected maps

                            if (Selections.Contains(101) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Malas/Doom.xml", prefix));
                            }
                            if (Selections.Contains(102) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Malas/North.xml", prefix));
                            }
                            if (Selections.Contains(103) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Malas/OrcForts.xml", prefix));
                            }
                            if (Selections.Contains(104) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Malas/South.xml", prefix));
                            }
                            if (Selections.Contains(105) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Malas/Vendors.xml", prefix));
                            }
                            if (Selections.Contains(106) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Malas/Citadel.xml", prefix));
                            }
                            if (Selections.Contains(107) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Malas/Labyrinth.xml", prefix));
                            }
                            if (Selections.Contains(108) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Malas/Bedlam.xml", prefix));
                            }
                        }

                        break;
                    }
            }
        }
    }

    public class XUnloadTokunoGump : Gump
    {
        private readonly CommandEventArgs m_CommandEventArgs;
        public XUnloadTokunoGump(CommandEventArgs e)
            : base(50,50)
        {
            this.m_CommandEventArgs = e;
            this.Closable = true;
            this.Dragable = true;

            this.AddPage(1);

            //fundo cinza
            //alt era 310
            this.AddBackground(0, 0, 243, 250, 5054);
            //----------
            this.AddLabel(95, 2, 200, "TOKUNO");
            //fundo branco
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 183, 3004);
            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");
            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 172, 10003);
            this.AddImageTiled(163, 25, 2, 172, 10003);
            this.AddImageTiled(220, 25, 2, 172, 10003);
            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            //Map names
            this.AddLabel(35, 51, 200, "Fan Dancers Dojo");
            this.AddLabel(35, 76, 200, "Outdoors");
            this.AddLabel(35, 101, 200, "Towns Life");
            this.AddLabel(35, 126, 200, "Vendors");
            this.AddLabel(35, 151, 200, "Wild Life");
            this.AddLabel(35, 176, 200, "Yomutso Mines");

            //Options
            this.AddCheck(182, 48, 210, 211, false, 101);
            this.AddCheck(182, 73, 210, 211, false, 102);
            this.AddCheck(182, 98, 210, 211, false, 103);
            this.AddCheck(182, 123, 210, 211, false, 104);
            this.AddCheck(182, 148, 210, 211, false, 105);
            this.AddCheck(182, 173, 210, 211, false, 106);

            //Ok, Cancel
            // alt era 280
            this.AddButton(55, 220, 247, 249, 1, GumpButtonType.Reply, 0);
            this.AddButton(125, 220, 241, 243, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch( info.ButtonID )
            {
                case 0: // Closed or Cancel
                    {
                        return;
                    }

                default:
                    {
                        // Make sure that the OK, button was pressed
                        if (info.ButtonID == 1)
                        {
                            // Get the array of switches selected
                            ArrayList Selections = new ArrayList(info.Switches);
                            string prefix = Server.Commands.CommandSystem.Prefix;

                            // Now unloading any selected maps

                            if (Selections.Contains(101) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Tokuno/FanDancersDojo.xml", prefix));
                            }
                            if (Selections.Contains(102) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Tokuno/Outdoors.xml", prefix));
                            }
                            if (Selections.Contains(103) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Tokuno/TownsLife.xml", prefix));
                            }
                            if (Selections.Contains(104) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Tokuno/Vendors.xml", prefix));
                            }
                            if (Selections.Contains(105) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Tokuno/WildLife.xml", prefix));
                            }
                            if (Selections.Contains(106) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Tokuno/YomutsoMines.xml", prefix));
                            }
                        }

                        break;
                    }
            }
        }
    }

    public class XUnloadTermurGump : Gump
    {
        private readonly CommandEventArgs m_CommandEventArgs;
        public XUnloadTermurGump(CommandEventArgs e)
            : base(50, 50)
        {
            this.m_CommandEventArgs = e;
            this.Closable = true;
            this.Dragable = true;

            this.AddPage(1);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "TER MUR");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(167, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Crimson Veins");
            this.AddLabel(35, 76, 200, "Enslaved Goblins");
            this.AddLabel(35, 101, 200, "Fire Island Ruins");
            this.AddLabel(35, 126, 200, "Fractured City");
            this.AddLabel(35, 151, 200, "Lands of the Lich");
            this.AddLabel(35, 176, 200, "Lava Caldera");
            this.AddLabel(35, 201, 200, "Passage of Tears");
            this.AddLabel(35, 226, 200, "Secret Garden");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 101);
            this.AddCheck(182, 73, 210, 211, false, 102);
            this.AddCheck(182, 98, 210, 211, false, 103);
            this.AddCheck(182, 123, 210, 211, false, 104);
            this.AddCheck(182, 148, 210, 211, false, 105);
            this.AddCheck(182, 173, 210, 211, false, 106);
            this.AddCheck(182, 198, 210, 211, false, 107);
            this.AddCheck(182, 223, 210, 211, false, 108);

            this.AddLabel(110, 255, 200, "1/5");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2);

            this.AddPage(2);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "TER MUR");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(167, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Cavern of the Discarded");
            this.AddLabel(35, 76, 200, "Clan Scratch");
            this.AddLabel(35, 101, 246, "Tomb of Kings");
            this.AddLabel(35, 126, 246, "Underworld");
            this.AddLabel(35, 151, 246, "Abyss");
            this.AddLabel(35, 176, 200, "Atoll Blend");
            this.AddLabel(35, 201, 200, "Chicken Chase");
            this.AddLabel(35, 226, 200, "City Residential");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 109);
            this.AddCheck(182, 73, 210, 211, false, 110);
            this.AddCheck(182, 98, 210, 211, false, 111);
            this.AddCheck(182, 123, 210, 211, false, 112);
            this.AddCheck(182, 148, 210, 211, false, 113);
            this.AddCheck(182, 173, 210, 211, false, 114);
            this.AddCheck(182, 198, 210, 211, false, 115);
            this.AddCheck(182, 223, 210, 211, false, 116);

            this.AddLabel(110, 255, 200, "2/5");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 3);
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1);

            this.AddPage(3);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "TER MUR");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(167, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Coral Desert");
            this.AddLabel(35, 76, 200, "Fisherman's Reach");
            this.AddLabel(35, 101, 200, "Gated Isle");
            this.AddLabel(35, 126, 200, "High Plains");
            this.AddLabel(35, 151, 200, "Kepetch Waste");
            this.AddLabel(35, 176, 200, "Lava Lake");
            this.AddLabel(35, 201, 200, "Lava Pit Pyramid");
            this.AddLabel(35, 226, 200, "Lost Settlement");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 117);
            this.AddCheck(182, 73, 210, 211, false, 118);
            this.AddCheck(182, 98, 210, 211, false, 119);
            this.AddCheck(182, 123, 210, 211, false, 120);
            this.AddCheck(182, 148, 210, 211, false, 121);
            this.AddCheck(182, 173, 210, 211, false, 122);
            this.AddCheck(182, 198, 210, 211, false, 123);
            this.AddCheck(182, 223, 210, 211, false, 124);

            this.AddLabel(110, 255, 200, "3/5");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 4);
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 2);

            this.AddPage(4);

            //grey background
            this.AddBackground(0, 0, 240, 310, 5054);

            //----------
            this.AddLabel(95, 2, 200, "TER MUR");

            //white background
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(167, 27, 200, "Unload");

            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 222, 10003);
            this.AddImageTiled(163, 25, 2, 222, 10003);
            this.AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            this.AddImageTiled(20, 220, 200, 2, 10001);
            this.AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            this.AddLabel(35, 51, 200, "Northern Steppe");
            this.AddLabel(35, 76, 200, "Raptor Isle");
            this.AddLabel(35, 101, 200, "Slith Valley");
            this.AddLabel(35, 126, 200, "Spider Island");
            this.AddLabel(35, 151, 200, "Talon Point");
            this.AddLabel(35, 176, 200, "Treefellow Course");
            this.AddLabel(35, 201, 200, "Void Isle");
            this.AddLabel(35, 226, 200, "Walled Circus");

            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 125);
            this.AddCheck(182, 73, 210, 211, false, 126);
            this.AddCheck(182, 98, 210, 211, false, 127);
            this.AddCheck(182, 123, 210, 211, false, 128);
            this.AddCheck(182, 148, 210, 211, false, 129);
            this.AddCheck(182, 173, 210, 211, false, 130);
            this.AddCheck(182, 198, 210, 211, false, 131);
            this.AddCheck(182, 223, 210, 211, false, 132);

            this.AddLabel(110, 255, 200, "4/5");
            this.AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 5);
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 3);

            this.AddPage(5);

            //fundo cinza
            this.AddBackground(0, 0, 243, 310, 5054);
            //----------
            this.AddLabel(93, 2, 200, "TER MUR");
            //fundo branco
            //x, y, largura, altura, item
            this.AddImageTiled(10, 20, 220, 232, 3004);
            //----------
            this.AddLabel(30, 27, 200, "Map name");
            this.AddLabel(172, 27, 200, "Unload");
            //colunas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 2, 171, 10003);
            this.AddImageTiled(163, 25, 2, 171, 10003);
            this.AddImageTiled(220, 25, 2, 171, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            this.AddImageTiled(20, 25, 200, 2, 10001);
            this.AddImageTiled(20, 45, 200, 2, 10001);
            this.AddImageTiled(20, 70, 200, 2, 10001);
            this.AddImageTiled(20, 95, 200, 2, 10001);
            this.AddImageTiled(20, 120, 200, 2, 10001);
            this.AddImageTiled(20, 145, 200, 2, 10001);
            this.AddImageTiled(20, 170, 200, 2, 10001);
            this.AddImageTiled(20, 195, 200, 2, 10001);
            //AddImageTiled( 20, 220, 200, 2, 10001 );
            //AddImageTiled( 20, 245, 200, 2, 10001 );
            
            //Map names
            this.AddLabel(35, 51, 200, "Waterfall Point");
            this.AddLabel(35, 76, 246, "Shrine of Singularity");
            this.AddLabel(35, 101, 200, "Toxic Desert");
            this.AddLabel(35, 126, 200, "Vendor");
            this.AddLabel(35, 151, 246, "Royal City");
            this.AddLabel(35, 176, 246, "Holy City");
            //AddLabel( 35, 201, 200, "39" );
            //AddLabel( 35, 226, 200, "40" );
          
            //Check boxes
            this.AddCheck(182, 48, 210, 211, false, 133);
            this.AddCheck(182, 73, 210, 211, false, 134);
            this.AddCheck(182, 98, 210, 211, false, 135);
            this.AddCheck(182, 123, 210, 211, false, 136);            
            this.AddCheck(182, 148, 210, 211, false, 137);
            this.AddCheck(182, 173, 210, 211, false, 138); 
            //AddCheck( 182, 198, 210, 211, false, 139 );
            //AddCheck( 182, 223, 210, 211, false, 140 ); 

            this.AddLabel(110, 255, 200, "5/5");
            this.AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 4);

            //Ok, Cancel
            this.AddButton(55, 280, 247, 249, 1, GumpButtonType.Reply, 0);
            this.AddButton(125, 280, 241, 243, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: // Closed or Cancel
                    {
                        return;
                    }

                default:
                    {
                        // Make sure that the OK, button was pressed
                        if (info.ButtonID == 1)
                        {
                            // Get the array of switches selected
                            ArrayList Selections = new ArrayList(info.Switches);
                            string prefix = Server.Commands.CommandSystem.Prefix;

                            // Now unloading any selected maps

                            if (Selections.Contains(101) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/crimsonveins.xml", prefix));
                            }
                            if (Selections.Contains(102) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/enslavedgoblins.xml", prefix));
                            }
                            if (Selections.Contains(103) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/fireislandruins.xml", prefix));
                            }
                            if (Selections.Contains(104) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/fracturedcity.xml", prefix));
                            }
                            if (Selections.Contains(105) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/landsofthelich.xml", prefix));
                            }
                            if (Selections.Contains(106) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/lavacaldera.xml", prefix));
                            }
                            if (Selections.Contains(107) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/passageoftears.xml", prefix));
                            }
                            if (Selections.Contains(108) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/secretgarden.xml", prefix));
                            }
                            if (Selections.Contains(109) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/cavernofthediscarded.xml", prefix));
                            }
                            if (Selections.Contains(110) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/clanscratch.xml", prefix));
                            }
                            if (Selections.Contains(111) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/tombofkings.xml", prefix));
                            }
                            if (Selections.Contains(112) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/underworld.xml", prefix));
                            }
                            if (Selections.Contains(113) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/abyss.xml", prefix));
                            }
                            if (Selections.Contains(114) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/atollblend.xml", prefix));
                            }
                            if (Selections.Contains(115) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/chickenchase.xml", prefix));
                            }
                            if (Selections.Contains(116) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/cityresidential.xml", prefix));
                            }
                            if (Selections.Contains(117) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/coraldesert.xml", prefix));
                            }
                            if (Selections.Contains(118) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/fishermansreach.xml", prefix));
                            }
                            if (Selections.Contains(119) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/gatedisle.xml", prefix));
                            }
                            if (Selections.Contains(120) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/highplains.xml", prefix));
                            }
                            if (Selections.Contains(121) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/kepetchwaste.xml", prefix));
                            }
                            if (Selections.Contains(122) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/lavalake.xml", prefix));
                            }
                            if (Selections.Contains(123) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/lavapitpyramid.xml", prefix));
                            }
                            if (Selections.Contains(124) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/lostsettlement.xml", prefix));
                            }
                            if (Selections.Contains(125) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/northernsteppe.xml", prefix));
                            }
                            if (Selections.Contains(126) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/raptorisle.xml", prefix));
                            }
                            if (Selections.Contains(127) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/slithvalley.xml", prefix));
                            }
                            if (Selections.Contains(128) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/spiderisland.xml", prefix));
                            }
                            if (Selections.Contains(129) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/talonpoint.xml", prefix));
                            }
                            if (Selections.Contains(130) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/treefellowcourse.xml", prefix));
                            }
                            if (Selections.Contains(131) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/voidisle.xml", prefix));
                            }
                            if (Selections.Contains(132) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/walledcircus.xml", prefix));
                            }
                            if (Selections.Contains(133) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/waterfallpoint.xml", prefix));
                            }
                            if (Selections.Contains(134) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/shrine.xml", prefix));
                            }
                            if (Selections.Contains(135) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/toxicdesert.xml", prefix));
                            }
                            if (Selections.Contains(136) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/vendors.xml", prefix));
                            }
                            if (Selections.Contains(137) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/royalcity.xml", prefix));
                            }
                            if (Selections.Contains(138) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlUnload Spawns/Termur/holycity.xml", prefix));
                            }
                        }

                        break;
                    }
            }
        }
    }
}