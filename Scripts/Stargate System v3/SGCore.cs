// *************************************************************************************
// ***** -------------------------------------------------------------
// ***** Stargate Travel System Version 3.0, written by FingersMcSteal
// ***** -------------------------------------------------------------
// ***** Some of this systems settings are changable from here in the core.
// ***** Each remark statement has limited info about each setting.
// ***** This system is also running on my shard 'YaksUOWorld' if you'd like to
// ***** see it running before you try it out.
// ***** For support please ask in the RunUO forums.
// *************************************************************************************
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

using Server;
using Server.Items;

namespace Server.SG
{
    public class SGCore
    {
        private static string m_SGSavePath = Path.Combine(Core.BaseDirectory, "Data\\Stargate Data"); //Save Directory
        private static string m_SGFileName = "SGData.xml"; // File Name
        private static string m_SGFileNameHTML = "SGAddresses.html"; // File Name

        public static string FilePath = Path.Combine(m_SGSavePath, m_SGFileName);
        public static string FilePathHTML = Path.Combine(m_SGSavePath, m_SGFileNameHTML);

        public static ArrayList SGList = new ArrayList();

        public static bool SGSystemEnabled = true;              // System Startup State, Default Setting is Enabled (True / False)
        public static bool SGSystemHideGate = true;             // Gates added by Admin Menu are Hidden (true / false)
        public static int SGAddonStyles = 3;                    // Amount of addon designs avaliable. *** REQUIRES CORE & GUMP SCRIPT EDITS TO WORK RIGHT ***
        public static int SGSystemBlink = 2;                    // Time between each CCD colour change (seconds) *** This will effect the PULSE timer code in the CCD ***
        public static int SGSystemGateTime = 30;                // Time the Stargate's remain open after dialing (seconds, 30 same as a gate travel spell) *** OK to change here ***

        public static int SGSystemSoundButton = 0x4A;           // Sound, selection buttons
        public static int SGSystemSoundButtonActivate = 0x143;  // Sound, Activate button
        public static int SGSystemSoundGumpOpen = 0x51A;        // Sound, Open gump
        public static int SGSystemSoundWalkInGate = 0x11D;      // Sound, when you enter/exit the gate (active gates only)
        public static int SGSystemSoundGoodToGo = 0x20F;        // Sound, if destinations ok
        public static int SGSystemSoundNoTravel = 0x5C3;        // Sound, if bad destination

        public static void Initialize()
        {
            Server.EventSink.WorldSave += new WorldSaveEventHandler(EventSink_WorldSave);

            Utility.PushColor(ConsoleColor.White);
            Console.Write("Stargate System : ");
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.Green);
            Console.WriteLine("Checking...");
            Utility.PopColor();

            if (!Directory.Exists(m_SGSavePath))
            {
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Data Directory Not Present... Creating Data Folder...");
                Utility.PopColor();

                Directory.CreateDirectory(m_SGSavePath);

                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Stargate Folder Created, No Gates Defined Yet.");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Use [SGAdmin Command When Ready.");
                Utility.PopColor();
            }

            else if (!File.Exists(Path.Combine(m_SGSavePath, m_SGFileName)))
            {
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Directory Present... No Stargate Address File To Load.");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Use [SGAdmin Command When Ready.");
                Utility.PopColor();
            }

            else if (File.Exists(Path.Combine(m_SGSavePath, m_SGFileName)))
            {
                // SGEnterTest();
                SGLoadFile(FilePath);
            }
        }

        public static void SGGenHTML()
        {
            string LineToHTML;
            int SGTotal = SGCore.SGList.Count;
            int SGMax = 15625;

            int gatecounter = 0;
            // Create HTML
            using (StreamWriter op = new StreamWriter(FilePathHTML))
            {
                op.WriteLine("<html>");
                op.WriteLine("<head>");
                op.WriteLine("<title>Stargate v3.0 Address File</title>");
                op.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
                op.WriteLine("</head>");
                op.WriteLine("<body bgcolor=\"#330099\">");
                op.WriteLine("<font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#FFFFFF\">");
                op.WriteLine("<p align=\"center\">");
                op.WriteLine("<b>Stargate System v3.0 Address File</b><br>");
                op.WriteLine("By FingersMcSteal<br><br>");

                op.WriteLine("Shard Stargate Summary Page...<br><br>");

                op.WriteLine("Total Stargates In The Game World : " + SGTotal.ToString() + " Used" + "<br>");
                op.WriteLine("Total Stargate Slots Avaliable : " + ( SGMax - SGTotal) + " / 15625" + "<br>");

                op.WriteLine("</p>");
                op.WriteLine("<div align=\"center\">");
                op.WriteLine("  <table width=\"800\" border=\"0\" align=\"center\">");
                op.WriteLine("    <tr>");
                op.WriteLine("      <td>");

                for (int facet = 1; facet < 6; facet++)
                {
                    for (int c1 = 1; c1 < 6; c1++)
                    {
                        for (int c2 = 1; c2 < 6; c2++)
                        {
                            for (int c3 = 1; c3 < 6; c3++)
                            {
                                for (int c4 = 1; c4 < 6; c4++)
                                {
                                    for (int c5 = 1; c5 < 6; c5++)
                                    {
                                        gatecounter++;
                                        LineToHTML = SGLookForGate(gatecounter, facet, c1, c2, c3, c4, c5);
                                        op.WriteLine(LineToHTML);
                                    }
                                }
                            }
                        }
                    }
                }

                op.WriteLine("      </td>");
                op.WriteLine("    </tr>");
                op.WriteLine("  </table>");
                op.WriteLine("</div>");
                op.WriteLine("</body>");
                op.WriteLine("</html>");
            }
        }

        public static string SGLookForGate(int gatenumber, int checkfacetID, int checkcode1, int checkcode2, int checkcode3, int checkcode4, int checkcode5)
        {
            string LineOutput = "";
            string FIDName = "";

            bool SGExists = false;
            int SGIndex = 0;

            string AddressUsed;

            if (checkfacetID == 1)
            {
                FIDName = "Felucca";
            }
            if (checkfacetID == 2)
            {
                FIDName = "Trammel";
            }
            if (checkfacetID == 3)
            {
                FIDName = "Ilshenar";
            }
            if (checkfacetID == 4)
            {
                FIDName = "Malas";
            }
            if (checkfacetID == 5)
            {
                FIDName = "Tokuno";
            }

            for (int i = 0; i < SGCore.SGList.Count; i++)
            {
                SGEntry sge = (SGEntry)SGCore.SGList[i];

                if (sge.SGFacetCode == checkfacetID && sge.SGAddressCode1 == checkcode1 && sge.SGAddressCode2 == checkcode2 && sge.SGAddressCode3 == checkcode3 && sge.SGAddressCode4 == checkcode4 && sge.SGAddressCode5 == checkcode5)
                {
                    // Found
                    SGExists = true;
                    SGIndex = i;
                }
            }

            if (SGExists)
            {
                AddressUsed = "Valid Address";
                SGEntry sge = (SGEntry)SGCore.SGList[SGIndex];

                string part11 = "<table width=\"100%\" border=\"0\"><tr>";
                string part12 = "</tr></table>";

                string part1 = "<td width=\"110\" bgcolor=\"#009999\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">Stargate : " + gatenumber + "</div></td>";
                string part2 = "<td width=\"70\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + FIDName + "</div></td>";

                string part3 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode1 + "</div></td>";
                string part4 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode2 + "</div></td>";
                string part5 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode3 + "</div></td>";
                string part6 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode4 + "</div></td>";
                string part7 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode5 + "</div></td>";

                string part8 = "<td width=\"130\" bgcolor=\"#009900\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#FFFFFF\">" + AddressUsed + "</div></td>";
                string part9 = "<td width=\"190\" bgcolor=\"#0099CC\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + sge.SGLocationName + "</td>";
                string part10 = "<td width=\"190\" bgcolor=\"#0099CC\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + sge.SGDiscovered + "</td>";

                LineOutput = part11 + part1 + part2 + part3 + part4 + part5 + part6 + part7 + part8 + part9 + part10 + part12;
            }

            if (!SGExists)
            {
                AddressUsed = "Address Avaliable";

                string part11 = "<table width=\"100%\" border=\"0\"><tr>";
                string part12 = "</tr></table>";

                string part1 = "<td width=\"110\" bgcolor=\"#009999\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">Stargate : " + gatenumber + "</div></td>";
                string part2 = "<td width=\"70\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + FIDName + "</div></td>";

                string part3 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode1 + "</div></td>";
                string part4 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode2 + "</div></td>";
                string part5 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode3 + "</div></td>";
                string part6 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode4 + "</div></td>";
                string part7 = "<td width=\"18\" bgcolor=\"#0099CC\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + checkcode5 + "</div></td>";

                string part8 = "<td width=\"130\" bgcolor=\"#990000\"><div align=\"center\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#FFFFFF\">" + AddressUsed + "</div></td>";
                string part9 = "<td width=\"190\" bgcolor=\"#0099CC\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + "----------" + "</td>";
                string part10 = "<td width=\"190\" bgcolor=\"#0099CC\"><font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#000000\">" + "----------" + "</td>";

                LineOutput = part11 + part1 + part2 + part3 + part4 + part5 + part6 + part7 + part8 + part9 + part10 + part12;
            }
            return LineOutput;
        }

        public static void SGEffectInToGate(Mobile from)
        {
            Effects.SendBoltEffect(from, true, 64);
            Effects.PlaySound(from.Location, from.Map, SGSystemSoundWalkInGate);
        }

        public static void SGEffectNorthSouth(int origin)
        {
            SGEntry sgeffect = (SGEntry)SGList[origin];

            int X = sgeffect.SGX;
            int Y = sgeffect.SGY;
            int Z = sgeffect.SGZ;
            int m = sgeffect.SGFacetCode;

            if (m == 1)
            {
                Effects.SendLocationEffect(new Point3D(X, Y - 4, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 3, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 2, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 1, Z), Map.Felucca, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 2, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 3, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 4, Z), Map.Felucca, 0x375A, 15);
            }
            else if (m == 2)
            {
                Effects.SendLocationEffect(new Point3D(X, Y - 4, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 3, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 2, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 1, Z), Map.Trammel, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 2, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 3, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 4, Z), Map.Trammel, 0x375A, 15);
            }
            else if (m == 3)
            {
                Effects.SendLocationEffect(new Point3D(X, Y - 4, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 3, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 2, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 1, Z), Map.Ilshenar, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 2, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 3, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 4, Z), Map.Ilshenar, 0x375A, 15);
            }
            else if (m == 4)
            {
                Effects.SendLocationEffect(new Point3D(X, Y - 4, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 3, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 2, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 1, Z), Map.Malas, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 2, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 3, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 4, Z), Map.Malas, 0x375A, 15);
            }
            else if (m == 5)
            {
                Effects.SendLocationEffect(new Point3D(X, Y - 4, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 3, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 2, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y - 1, Z), Map.Tokuno, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 2, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 3, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X, Y + 4, Z), Map.Tokuno, 0x375A, 15);
            }
        }

        public static void SGEffectEastWest(int origin)
        {
            SGEntry sgeffect = (SGEntry)SGList[origin];

            int X = sgeffect.SGX;
            int Y = sgeffect.SGY;
            int Z = sgeffect.SGZ;
            int m = sgeffect.SGFacetCode;

            if (m == 1)
            {
                Effects.SendLocationEffect(new Point3D(X - 4, Y, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 3, Y, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 2, Y, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 1, Y, Z), Map.Felucca, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 2, Y, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 3, Y, Z), Map.Felucca, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 4, Y, Z), Map.Felucca, 0x375A, 15);
            }
            else if (m == 2)
            {
                Effects.SendLocationEffect(new Point3D(X - 4, Y, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 3, Y, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 2, Y, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 1, Y, Z), Map.Trammel, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 2, Y, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 3, Y, Z), Map.Trammel, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 4, Y, Z), Map.Trammel, 0x375A, 15);
            }
            else if (m == 3)
            {
                Effects.SendLocationEffect(new Point3D(X - 4, Y, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 3, Y, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 2, Y, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 1, Y, Z), Map.Ilshenar, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 2, Y, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 3, Y, Z), Map.Ilshenar, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 4, Y, Z), Map.Ilshenar, 0x375A, 15);
            }
            else if (m == 4)
            {
                Effects.SendLocationEffect(new Point3D(X - 4, Y, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 3, Y, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 2, Y, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 1, Y, Z), Map.Malas, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 2, Y, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 3, Y, Z), Map.Malas, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 4, Y, Z), Map.Malas, 0x375A, 15);
            }
            else if (m == 5)
            {
                Effects.SendLocationEffect(new Point3D(X - 4, Y, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 3, Y, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 2, Y, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X - 1, Y, Z), Map.Tokuno, 0x375A, 15);

                Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 2, Y, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 3, Y, Z), Map.Tokuno, 0x375A, 15);
                Effects.SendLocationEffect(new Point3D(X + 4, Y, Z), Map.Tokuno, 0x375A, 15);
            }
        }

        public static void SGTriggerSave()
        {
            Utility.PushColor(ConsoleColor.White);
            Console.Write("Stargate System : ");
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.Green);
            Console.WriteLine("System Save Command Issued, Forced Save...");
            Utility.PopColor();

            SGSaveFile(FilePath);
        }

        public static void SGTriggerLoad()
        {
            SGGenerate();
        }

        public static void SGDelete()
        {
            Utility.PushColor(ConsoleColor.White);
            Console.Write("Stargate System : ");
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.Green);
            Console.WriteLine("Delete Command Issued");
            Utility.PopColor();

            // ********************************************
            // *** Remove 1st ADDRESS Symbol From World ***
            // ********************************************
            ArrayList SGADSYM1 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGAddressSymbol1)
                    SGADSYM1.Add(item);
            }

            foreach (Item item in SGADSYM1)
                item.Delete();

            if (SGADSYM1.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Address Symbol 1 Removed", SGADSYM1.Count);

            // ********************************************
            // *** Remove 2nd ADDRESS Symbol From World ***
            // ********************************************
            ArrayList SGADSYM2 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGAddressSymbol2)
                    SGADSYM2.Add(item);
            }

            foreach (Item item in SGADSYM2)
                item.Delete();

            if (SGADSYM2.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Address Symbol 2 Removed", SGADSYM2.Count);

            // ********************************************
            // *** Remove 3rd ADDRESS Symbol From World ***
            // ********************************************
            ArrayList SGADSYM3 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGAddressSymbol3)
                    SGADSYM3.Add(item);
            }

            foreach (Item item in SGADSYM3)
                item.Delete();

            if (SGADSYM3.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Address Symbol 3 Removed", SGADSYM3.Count);

            // ********************************************
            // *** Remove 4th ADDRESS Symbol From World ***
            // ********************************************
            ArrayList SGADSYM4 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGAddressSymbol4)
                    SGADSYM4.Add(item);
            }

            foreach (Item item in SGADSYM4)
                item.Delete();

            if (SGADSYM4.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Address Symbol 4 Removed", SGADSYM4.Count);

            // ********************************************
            // *** Remove 5th ADDRESS Symbol From World ***
            // ********************************************
            ArrayList SGADSYM5 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGAddressSymbol5)
                    SGADSYM5.Add(item);
            }

            foreach (Item item in SGADSYM5)
                item.Delete();

            if (SGADSYM5.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Address Symbol 5 Removed", SGADSYM5.Count);

            // ********************************************
            // **** Remove 1st FACET Symbol From World ****
            // ********************************************
            ArrayList SGFASYM1 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGFacetSymbol1)
                    SGFASYM1.Add(item);
            }

            foreach (Item item in SGFASYM1)
                item.Delete();

            if (SGFASYM1.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Facet Symbol 1 Removed", SGFASYM1.Count);

            // ********************************************
            // **** Remove 2nd FACET Symbol From World ****
            // ********************************************
            ArrayList SGFASYM2 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGFacetSymbol2)
                    SGFASYM2.Add(item);
            }

            foreach (Item item in SGFASYM2)
                item.Delete();

            if (SGFASYM2.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Facet Symbol 2 Removed", SGFASYM2.Count);

            // ********************************************
            // **** Remove 3rd FACET Symbol From World ****
            // ********************************************
            ArrayList SGFASYM3 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGFacetSymbol3)
                    SGFASYM3.Add(item);
            }

            foreach (Item item in SGFASYM3)
                item.Delete();

            if (SGFASYM3.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Facet Symbol 3 Removed", SGFASYM3.Count);

            // ********************************************
            // **** Remove 4th FACET Symbol From World ****
            // ********************************************
            ArrayList SGFASYM4 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGFacetSymbol4)
                    SGFASYM4.Add(item);
            }

            foreach (Item item in SGFASYM4)
                item.Delete();

            if (SGFASYM4.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Facet Symbol 4 Removed", SGFASYM4.Count);

            // ********************************************
            // **** Remove 5th FACET Symbol From World ****
            // ********************************************
            ArrayList SGFASYM5 = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGFacetSymbol5)
                    SGFASYM5.Add(item);
            }

            foreach (Item item in SGFASYM5)
                item.Delete();

            if (SGFASYM5.Count > 0)
                World.Broadcast(0x35, true, "{0} SG Facet Symbol 5 Removed", SGFASYM5.Count);

            // ********************************************
            // ******* Remove ALL Crystal Controls ********
            // ********************************************
            ArrayList SGADCRY = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGActivatorDevice)
                    SGADCRY.Add(item);
            }

            foreach (Item item in SGADCRY)
                item.Delete();

            if (SGADCRY.Count > 0)
                World.Broadcast(0x35, true, "{0} Control Crystals", SGADCRY.Count);

            // ********************************************
            // ****** Remove ALL SG Platform Addon's ******
            // ********************************************
            ArrayList SGPlatAddons = new ArrayList();
            foreach (Item item in World.Items.Values)
            {
                if (item is SGLocationAddon1East || item is SGLocationAddon1South || item is SGLocationAddon2East || item is SGLocationAddon2South || item is SGLocationAddon3East || item is SGLocationAddon3South)
                    SGPlatAddons.Add(item);
            }

            foreach (Item item in SGPlatAddons)
                item.Delete();

            if (SGPlatAddons.Count > 0)
                World.Broadcast(0x35, true, "{0} Stargate Platforms Removed", SGPlatAddons.Count);

            // ********************************************
            // *** Remove ALL SGList Entries From Array ***
            // ********************************************
            World.Broadcast(0x35, true, "{0} List Entries Start", SGList.Count);

            SGList.Clear();

            World.Broadcast(0x35, true, "{0} List Entries End", SGList.Count);
        }

        public static void SGEnterTest()
        {
            // ********************************************
            // ******* Test DATA Used While Building ******
            // ********************************************

            Console.WriteLine("");
            Utility.PushColor(ConsoleColor.White);
            Console.Write("Stargate System : ");
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.Green);
            Console.WriteLine("Test Data Added...");
            Utility.PopColor();

            SGList.Add(new SGEntry(5480, 1172, 0, "E", 1, true, false, false, false, "Nobody", "Test Gate 1", 1, 1, 1, 1, 1, 1));
            SGList.Add(new SGEntry(5480, 1179, 0, "E", 1, true, false, false, false, "Nobody", "Test Gate 2", 1, 1, 1, 1, 1, 2));
            SGList.Add(new SGEntry(5487, 1172, 0, "S", 1, true, false, false, false, "Nobody", "Test Gate 3", 1, 1, 1, 1, 1, 3));
            SGList.Add(new SGEntry(5487, 1179, 0, "S", 1, true, false, false, false, "Nobody", "Test Gate 4", 1, 1, 1, 1, 1, 4));
        }

        public static void SGGenerate()
        {
            Utility.PushColor(ConsoleColor.White);
            Console.Write("Stargate System : ");
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.Green);
            Console.WriteLine("System Generate Command Issued, Forced Re-Load...");
            Utility.PopColor();

            // ********************************************
            // *** Generates All Stargate Locations *******
            // ********** From Current XML File ***********
            // ********************************************

            SGLoadFile(FilePath);

        }

        public static void SGInfo()
        {
            World.Broadcast(0x35, true, "{0} Stargate List Entries", SGList.Count);
        }

        public static void SGLoadFile(string FilePath)
        {
            try
            {
                DateTime start = DateTime.Now;
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Startup Running...");
                Utility.PopColor();

                // ********************************************
                // ***** Remove Old SGSystem & Components *****
                // ******* Ready For NEW XML Data Read ********
                // ********************************************
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Removing Previous Components...");
                Utility.PopColor();

                SGDelete();

                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Deletion done, Reloading From Existing File...");
                Utility.PopColor();

                // ********************************************
                // *** Load SGData.XML File, Read Entries In **
                // ********************************************
                int SGXIn = 0;
                int SGYIn = 0;
                int SGZIn = 0;

                int SGStyleIn = 0;
                bool SGCanBeUsedIn = false;
                bool SGBeingUsedIn = false;
                bool SGEnergyIn = false;
                bool SGHiddenIn = false;

                int SGFacetCodeIn = 0;
                int SGAddressCode1In = 0;
                int SGAddressCode2In = 0;
                int SGAddressCode3In = 0;
                int SGAddressCode4In = 0;
                int SGAddressCode5In = 0;

                XmlDocument doc = new XmlDocument();
                doc.Load(FilePath);

                XmlElement root = doc["StargateAddresses"];

                foreach (XmlElement StargateEntry in root.GetElementsByTagName("StargateEntry"))
                {
                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGX").InnerText, out SGXIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGY").InnerText, out SGYIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGZ").InnerText, out SGZIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }

                    string SGFacingIn = StargateEntry.SelectSingleNode("SGFacing").InnerText;

                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGStyle").InnerText, out SGStyleIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!bool.TryParse(StargateEntry.SelectSingleNode("SGCanBeUsed").InnerText, out SGCanBeUsedIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!bool.TryParse(StargateEntry.SelectSingleNode("SGBeingUsed").InnerText, out SGBeingUsedIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!bool.TryParse(StargateEntry.SelectSingleNode("SGEnergy").InnerText, out SGEnergyIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!bool.TryParse(StargateEntry.SelectSingleNode("SGHidden").InnerText, out SGHiddenIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }

                    string SGDiscoveredIn = StargateEntry.SelectSingleNode("SGDiscovered").InnerText;
                    string SGLocationNameIn = StargateEntry.SelectSingleNode("SGLocationName").InnerText;

                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGFacetCode").InnerText, out SGFacetCodeIn))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGAddressCode1").InnerText, out SGAddressCode1In))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGAddressCode2").InnerText, out SGAddressCode2In))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGAddressCode3").InnerText, out SGAddressCode3In))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGAddressCode4").InnerText, out SGAddressCode4In))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }
                    if (!int.TryParse(StargateEntry.SelectSingleNode("SGAddressCode5").InnerText, out SGAddressCode5In))//use TryParse
                    {
                        Console.WriteLine("ERROR Reading Value");
                    }

                    SGList.Add(new SGEntry(SGXIn, SGYIn, SGZIn, SGFacingIn, SGStyleIn, SGCanBeUsedIn, SGBeingUsedIn, SGEnergyIn, SGHiddenIn, SGDiscoveredIn, SGLocationNameIn, SGFacetCodeIn, SGAddressCode1In, SGAddressCode2In, SGAddressCode3In, SGAddressCode4In, SGAddressCode5In));
                }

                // ********************************************
                // *** Build Stargate Platforms Into World ****
                // ********************************************
                SGWorldBuild();

                DateTime end = DateTime.Now;
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Loading Compleated Successfully... ({1} Entries, {0:F1} seconds)", (end - start).TotalSeconds, SGList.Count);
                Utility.PopColor();
            }
            catch
            {
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Red);
                Console.WriteLine("{0} FILE ERROR, CHECK FILE EXISTS OR NOT CORRUPED !!!", m_SGFileName);
                Utility.PopColor();
            }
        }

        public static void SGWorldBuild()
        {
            if (SGList.Count <= 0)
            {
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("No Stargates To Build.");
                Utility.PopColor();
            }
            else
            {
                Utility.PushColor(ConsoleColor.White);
                Console.Write("Stargate System : ");
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("Building {0} Stargates...", SGList.Count);
                Utility.PopColor();
                World.Broadcast(0x35, true, "Building {0} Stargates...", SGList.Count);

                for (int i = 0; i < SGList.Count; i++)
                {
                    
                    SGEntry sgwb = (SGEntry)SGList[i];

                    // ********************************************
                    // *** Check Style And Direction of Platform **
                    // ********************************************
                    if (sgwb.SGStyle == 1)
                    {
                        if (sgwb.SGFacing == "E")
                        {
                            if (sgwb.SGFacetCode == 1)
                            {
                                Item platform = new SGLocationAddon1East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Felucca);
                            }
                            else if (sgwb.SGFacetCode == 2)
                            {
                                Item platform = new SGLocationAddon1East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Trammel);
                            }
                            else if (sgwb.SGFacetCode == 3)
                            {
                                Item platform = new SGLocationAddon1East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Ilshenar);
                            }
                            else if (sgwb.SGFacetCode == 4)
                            {
                                Item platform = new SGLocationAddon1East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Malas);
                            }
                            else if (sgwb.SGFacetCode == 5)
                            {
                                Item platform = new SGLocationAddon1East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Tokuno);
                            }
                        }
                        else
                        {
                            if (sgwb.SGFacetCode == 1)
                            {
                                Item platform = new SGLocationAddon1South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Felucca);
                            }
                            else if (sgwb.SGFacetCode == 2)
                            {
                                Item platform = new SGLocationAddon1South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Trammel);
                            }
                            else if (sgwb.SGFacetCode == 3)
                            {
                                Item platform = new SGLocationAddon1South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Ilshenar);
                            }
                            else if (sgwb.SGFacetCode == 4)
                            {
                                Item platform = new SGLocationAddon1South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Malas);
                            }
                            else if (sgwb.SGFacetCode == 5)
                            {
                                Item platform = new SGLocationAddon1South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Tokuno);
                            }
                        }
                    }

                    if (sgwb.SGStyle == 2)
                    {
                        if (sgwb.SGFacing == "E")
                        {
                            if (sgwb.SGFacetCode == 1)
                            {
                                Item platform = new SGLocationAddon2East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Felucca);
                            }
                            else if (sgwb.SGFacetCode == 2)
                            {
                                Item platform = new SGLocationAddon2East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Trammel);
                            }
                            else if (sgwb.SGFacetCode == 3)
                            {
                                Item platform = new SGLocationAddon2East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Ilshenar);
                            }
                            else if (sgwb.SGFacetCode == 4)
                            {
                                Item platform = new SGLocationAddon2East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Malas);
                            }
                            else if (sgwb.SGFacetCode == 5)
                            {
                                Item platform = new SGLocationAddon2East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Tokuno);
                            }
                        }
                        else
                        {
                            if (sgwb.SGFacetCode == 1)
                            {
                                Item platform = new SGLocationAddon2South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Felucca);
                            }
                            else if (sgwb.SGFacetCode == 2)
                            {
                                Item platform = new SGLocationAddon2South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Trammel);
                            }
                            else if (sgwb.SGFacetCode == 3)
                            {
                                Item platform = new SGLocationAddon2South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Ilshenar);
                            }
                            else if (sgwb.SGFacetCode == 4)
                            {
                                Item platform = new SGLocationAddon2South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Malas);
                            }
                            else if (sgwb.SGFacetCode == 5)
                            {
                                Item platform = new SGLocationAddon2South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Tokuno);
                            }
                        }
                    }

                    if (sgwb.SGStyle == 3)
                    {
                        if (sgwb.SGFacing == "E")
                        {
                            if (sgwb.SGFacetCode == 1)
                            {
                                Item platform = new SGLocationAddon3East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Felucca);
                            }
                            else if (sgwb.SGFacetCode == 2)
                            {
                                Item platform = new SGLocationAddon3East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Trammel);
                            }
                            else if (sgwb.SGFacetCode == 3)
                            {
                                Item platform = new SGLocationAddon3East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Ilshenar);
                            }
                            else if (sgwb.SGFacetCode == 4)
                            {
                                Item platform = new SGLocationAddon3East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Malas);
                            }
                            else if (sgwb.SGFacetCode == 5)
                            {
                                Item platform = new SGLocationAddon3East();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Tokuno);
                            }
                        }
                        else
                        {
                            if (sgwb.SGFacetCode == 1)
                            {
                                Item platform = new SGLocationAddon3South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Felucca);
                            }
                            else if (sgwb.SGFacetCode == 2)
                            {
                                Item platform = new SGLocationAddon3South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Trammel);
                            }
                            else if (sgwb.SGFacetCode == 3)
                            {
                                Item platform = new SGLocationAddon3South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Ilshenar);
                            }
                            else if (sgwb.SGFacetCode == 4)
                            {
                                Item platform = new SGLocationAddon3South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Malas);
                            }
                            else if (sgwb.SGFacetCode == 5)
                            {
                                Item platform = new SGLocationAddon3South();
                                platform.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY, sgwb.SGZ), Map.Tokuno);
                            }
                        }
                    }
                    // ********************************************
                    // ***** Add Facet Symbol According to map ****
                    // ********************************************
                    if (sgwb.SGFacetCode == 1)
                    {
                        if (sgwb.SGFacetCode == 1) { Item sgFIDsym = new SGFacetSymbol1(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGFacetCode == 2) { Item sgFIDsym = new SGFacetSymbol2(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGFacetCode == 3) { Item sgFIDsym = new SGFacetSymbol3(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGFacetCode == 4) { Item sgFIDsym = new SGFacetSymbol4(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGFacetCode == 5) { Item sgFIDsym = new SGFacetSymbol5(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Felucca); }
                    }
                    else if (sgwb.SGFacetCode == 2)
                    {
                        if (sgwb.SGFacetCode == 1) { Item sgFIDsym = new SGFacetSymbol1(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGFacetCode == 2) { Item sgFIDsym = new SGFacetSymbol2(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGFacetCode == 3) { Item sgFIDsym = new SGFacetSymbol3(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGFacetCode == 4) { Item sgFIDsym = new SGFacetSymbol4(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGFacetCode == 5) { Item sgFIDsym = new SGFacetSymbol5(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Trammel); }
                    }
                    else if (sgwb.SGFacetCode == 3)
                    {
                        if (sgwb.SGFacetCode == 1) { Item sgFIDsym = new SGFacetSymbol1(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGFacetCode == 2) { Item sgFIDsym = new SGFacetSymbol2(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGFacetCode == 3) { Item sgFIDsym = new SGFacetSymbol3(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGFacetCode == 4) { Item sgFIDsym = new SGFacetSymbol4(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGFacetCode == 5) { Item sgFIDsym = new SGFacetSymbol5(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Ilshenar); }
                    }
                    else if (sgwb.SGFacetCode == 4)
                    {
                        if (sgwb.SGFacetCode == 1) { Item sgFIDsym = new SGFacetSymbol1(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGFacetCode == 2) { Item sgFIDsym = new SGFacetSymbol2(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGFacetCode == 3) { Item sgFIDsym = new SGFacetSymbol3(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGFacetCode == 4) { Item sgFIDsym = new SGFacetSymbol4(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGFacetCode == 5) { Item sgFIDsym = new SGFacetSymbol5(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Malas); }
                    }
                    else if (sgwb.SGFacetCode == 5)
                    {
                        if (sgwb.SGFacetCode == 1) { Item sgFIDsym = new SGFacetSymbol1(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGFacetCode == 2) { Item sgFIDsym = new SGFacetSymbol2(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGFacetCode == 3) { Item sgFIDsym = new SGFacetSymbol3(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGFacetCode == 4) { Item sgFIDsym = new SGFacetSymbol4(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGFacetCode == 5) { Item sgFIDsym = new SGFacetSymbol5(); sgFIDsym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY - 2, sgwb.SGZ), Map.Tokuno); }
                    }

                    // ********************************************
                    // ***** Add Address Symbols For This Gate ****
                    // ********************************************
                    if (sgwb.SGFacetCode == 1)
                    {
                        // Add 1st CODE symbol to the world
                        if (sgwb.SGAddressCode1 == 1) { Item sgAC1sym = new SGAddressSymbol1(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode1 == 2) { Item sgAC1sym = new SGAddressSymbol2(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode1 == 3) { Item sgAC1sym = new SGAddressSymbol3(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode1 == 4) { Item sgAC1sym = new SGAddressSymbol4(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode1 == 5) { Item sgAC1sym = new SGAddressSymbol5(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }

                        // Add 2nd CODE symbol to the world
                        if (sgwb.SGAddressCode2 == 1) { Item sgAC2sym = new SGAddressSymbol1(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode2 == 2) { Item sgAC2sym = new SGAddressSymbol2(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode2 == 3) { Item sgAC2sym = new SGAddressSymbol3(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode2 == 4) { Item sgAC2sym = new SGAddressSymbol4(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode2 == 5) { Item sgAC2sym = new SGAddressSymbol5(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }

                        // Add 3rd CODE symbol to the world
                        if (sgwb.SGAddressCode3 == 1) { Item sgAC3sym = new SGAddressSymbol1(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode3 == 2) { Item sgAC3sym = new SGAddressSymbol2(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode3 == 3) { Item sgAC3sym = new SGAddressSymbol3(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode3 == 4) { Item sgAC3sym = new SGAddressSymbol4(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode3 == 5) { Item sgAC3sym = new SGAddressSymbol5(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }

                        // Add 4th CODE symbol to the world
                        if (sgwb.SGAddressCode4 == 1) { Item sgAC4sym = new SGAddressSymbol1(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode4 == 2) { Item sgAC4sym = new SGAddressSymbol2(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode4 == 3) { Item sgAC4sym = new SGAddressSymbol3(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode4 == 4) { Item sgAC4sym = new SGAddressSymbol4(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode4 == 5) { Item sgAC4sym = new SGAddressSymbol5(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }

                        // Add 5th CODE symbol to the world
                        if (sgwb.SGAddressCode5 == 1) { Item sgAC5sym = new SGAddressSymbol1(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode5 == 2) { Item sgAC5sym = new SGAddressSymbol2(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode5 == 3) { Item sgAC5sym = new SGAddressSymbol3(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode5 == 4) { Item sgAC5sym = new SGAddressSymbol4(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                        if (sgwb.SGAddressCode5 == 5) { Item sgAC5sym = new SGAddressSymbol5(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Felucca); }
                    }
                    else if (sgwb.SGFacetCode == 2)
                    {
                        // Add 1st CODE symbol to the world
                        if (sgwb.SGAddressCode1 == 1) { Item sgAC1sym = new SGAddressSymbol1(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode1 == 2) { Item sgAC1sym = new SGAddressSymbol2(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode1 == 3) { Item sgAC1sym = new SGAddressSymbol3(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode1 == 4) { Item sgAC1sym = new SGAddressSymbol4(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode1 == 5) { Item sgAC1sym = new SGAddressSymbol5(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }

                        // Add 2nd CODE symbol to the world
                        if (sgwb.SGAddressCode2 == 1) { Item sgAC2sym = new SGAddressSymbol1(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode2 == 2) { Item sgAC2sym = new SGAddressSymbol2(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode2 == 3) { Item sgAC2sym = new SGAddressSymbol3(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode2 == 4) { Item sgAC2sym = new SGAddressSymbol4(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode2 == 5) { Item sgAC2sym = new SGAddressSymbol5(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }

                        // Add 3rd CODE symbol to the world
                        if (sgwb.SGAddressCode3 == 1) { Item sgAC3sym = new SGAddressSymbol1(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode3 == 2) { Item sgAC3sym = new SGAddressSymbol2(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode3 == 3) { Item sgAC3sym = new SGAddressSymbol3(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode3 == 4) { Item sgAC3sym = new SGAddressSymbol4(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode3 == 5) { Item sgAC3sym = new SGAddressSymbol5(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }

                        // Add 4th CODE symbol to the world
                        if (sgwb.SGAddressCode4 == 1) { Item sgAC4sym = new SGAddressSymbol1(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode4 == 2) { Item sgAC4sym = new SGAddressSymbol2(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode4 == 3) { Item sgAC4sym = new SGAddressSymbol3(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode4 == 4) { Item sgAC4sym = new SGAddressSymbol4(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode4 == 5) { Item sgAC4sym = new SGAddressSymbol5(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }

                        // Add 5th CODE symbol to the world
                        if (sgwb.SGAddressCode5 == 1) { Item sgAC5sym = new SGAddressSymbol1(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode5 == 2) { Item sgAC5sym = new SGAddressSymbol2(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode5 == 3) { Item sgAC5sym = new SGAddressSymbol3(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode5 == 4) { Item sgAC5sym = new SGAddressSymbol4(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                        if (sgwb.SGAddressCode5 == 5) { Item sgAC5sym = new SGAddressSymbol5(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Trammel); }
                    }
                    else if (sgwb.SGFacetCode == 3)
                    {
                        // Add 1st CODE symbol to the world
                        if (sgwb.SGAddressCode1 == 1) { Item sgAC1sym = new SGAddressSymbol1(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode1 == 2) { Item sgAC1sym = new SGAddressSymbol2(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode1 == 3) { Item sgAC1sym = new SGAddressSymbol3(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode1 == 4) { Item sgAC1sym = new SGAddressSymbol4(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode1 == 5) { Item sgAC1sym = new SGAddressSymbol5(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }

                        // Add 2nd CODE symbol to the world
                        if (sgwb.SGAddressCode2 == 1) { Item sgAC2sym = new SGAddressSymbol1(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode2 == 2) { Item sgAC2sym = new SGAddressSymbol2(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode2 == 3) { Item sgAC2sym = new SGAddressSymbol3(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode2 == 4) { Item sgAC2sym = new SGAddressSymbol4(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode2 == 5) { Item sgAC2sym = new SGAddressSymbol5(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }

                        // Add 3rd CODE symbol to the world
                        if (sgwb.SGAddressCode3 == 1) { Item sgAC3sym = new SGAddressSymbol1(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode3 == 2) { Item sgAC3sym = new SGAddressSymbol2(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode3 == 3) { Item sgAC3sym = new SGAddressSymbol3(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode3 == 4) { Item sgAC3sym = new SGAddressSymbol4(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode3 == 5) { Item sgAC3sym = new SGAddressSymbol5(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }

                        // Add 4th CODE symbol to the world
                        if (sgwb.SGAddressCode4 == 1) { Item sgAC4sym = new SGAddressSymbol1(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode4 == 2) { Item sgAC4sym = new SGAddressSymbol2(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode4 == 3) { Item sgAC4sym = new SGAddressSymbol3(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode4 == 4) { Item sgAC4sym = new SGAddressSymbol4(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode4 == 5) { Item sgAC4sym = new SGAddressSymbol5(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }

                        // Add 5th CODE symbol to the world
                        if (sgwb.SGAddressCode5 == 1) { Item sgAC5sym = new SGAddressSymbol1(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode5 == 2) { Item sgAC5sym = new SGAddressSymbol2(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode5 == 3) { Item sgAC5sym = new SGAddressSymbol3(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode5 == 4) { Item sgAC5sym = new SGAddressSymbol4(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                        if (sgwb.SGAddressCode5 == 5) { Item sgAC5sym = new SGAddressSymbol5(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Ilshenar); }
                    }
                    else if (sgwb.SGFacetCode == 4)
                    {
                        // Add 1st CODE symbol to the world
                        if (sgwb.SGAddressCode1 == 1) { Item sgAC1sym = new SGAddressSymbol1(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode1 == 2) { Item sgAC1sym = new SGAddressSymbol2(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode1 == 3) { Item sgAC1sym = new SGAddressSymbol3(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode1 == 4) { Item sgAC1sym = new SGAddressSymbol4(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode1 == 5) { Item sgAC1sym = new SGAddressSymbol5(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }

                        // Add 2nd CODE symbol to the world
                        if (sgwb.SGAddressCode2 == 1) { Item sgAC2sym = new SGAddressSymbol1(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode2 == 2) { Item sgAC2sym = new SGAddressSymbol2(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode2 == 3) { Item sgAC2sym = new SGAddressSymbol3(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode2 == 4) { Item sgAC2sym = new SGAddressSymbol4(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode2 == 5) { Item sgAC2sym = new SGAddressSymbol5(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }

                        // Add 3rd CODE symbol to the world
                        if (sgwb.SGAddressCode3 == 1) { Item sgAC3sym = new SGAddressSymbol1(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode3 == 2) { Item sgAC3sym = new SGAddressSymbol2(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode3 == 3) { Item sgAC3sym = new SGAddressSymbol3(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode3 == 4) { Item sgAC3sym = new SGAddressSymbol4(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode3 == 5) { Item sgAC3sym = new SGAddressSymbol5(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }

                        // Add 4th CODE symbol to the world
                        if (sgwb.SGAddressCode4 == 1) { Item sgAC4sym = new SGAddressSymbol1(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode4 == 2) { Item sgAC4sym = new SGAddressSymbol2(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode4 == 3) { Item sgAC4sym = new SGAddressSymbol3(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode4 == 4) { Item sgAC4sym = new SGAddressSymbol4(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode4 == 5) { Item sgAC4sym = new SGAddressSymbol5(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }

                        // Add 5th CODE symbol to the world
                        if (sgwb.SGAddressCode5 == 1) { Item sgAC5sym = new SGAddressSymbol1(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode5 == 2) { Item sgAC5sym = new SGAddressSymbol2(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode5 == 3) { Item sgAC5sym = new SGAddressSymbol3(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode5 == 4) { Item sgAC5sym = new SGAddressSymbol4(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                        if (sgwb.SGAddressCode5 == 5) { Item sgAC5sym = new SGAddressSymbol5(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Malas); }
                    }
                    else if (sgwb.SGFacetCode == 5)
                    {
                        // Add 1st CODE symbol to the world
                        if (sgwb.SGAddressCode1 == 1) { Item sgAC1sym = new SGAddressSymbol1(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode1 == 2) { Item sgAC1sym = new SGAddressSymbol2(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode1 == 3) { Item sgAC1sym = new SGAddressSymbol3(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode1 == 4) { Item sgAC1sym = new SGAddressSymbol4(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode1 == 5) { Item sgAC1sym = new SGAddressSymbol5(); sgAC1sym.MoveToWorld(new Point3D(sgwb.SGX - 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }

                        // Add 2nd CODE symbol to the world
                        if (sgwb.SGAddressCode2 == 1) { Item sgAC2sym = new SGAddressSymbol1(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode2 == 2) { Item sgAC2sym = new SGAddressSymbol2(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode2 == 3) { Item sgAC2sym = new SGAddressSymbol3(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode2 == 4) { Item sgAC2sym = new SGAddressSymbol4(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode2 == 5) { Item sgAC2sym = new SGAddressSymbol5(); sgAC2sym.MoveToWorld(new Point3D(sgwb.SGX - 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }

                        // Add 3rd CODE symbol to the world
                        if (sgwb.SGAddressCode3 == 1) { Item sgAC3sym = new SGAddressSymbol1(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode3 == 2) { Item sgAC3sym = new SGAddressSymbol2(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode3 == 3) { Item sgAC3sym = new SGAddressSymbol3(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode3 == 4) { Item sgAC3sym = new SGAddressSymbol4(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode3 == 5) { Item sgAC3sym = new SGAddressSymbol5(); sgAC3sym.MoveToWorld(new Point3D(sgwb.SGX, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }

                        // Add 4th CODE symbol to the world
                        if (sgwb.SGAddressCode4 == 1) { Item sgAC4sym = new SGAddressSymbol1(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode4 == 2) { Item sgAC4sym = new SGAddressSymbol2(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode4 == 3) { Item sgAC4sym = new SGAddressSymbol3(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode4 == 4) { Item sgAC4sym = new SGAddressSymbol4(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode4 == 5) { Item sgAC4sym = new SGAddressSymbol5(); sgAC4sym.MoveToWorld(new Point3D(sgwb.SGX + 1, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }

                        // Add 5th CODE symbol to the world
                        if (sgwb.SGAddressCode5 == 1) { Item sgAC5sym = new SGAddressSymbol1(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode5 == 2) { Item sgAC5sym = new SGAddressSymbol2(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode5 == 3) { Item sgAC5sym = new SGAddressSymbol3(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode5 == 4) { Item sgAC5sym = new SGAddressSymbol4(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                        if (sgwb.SGAddressCode5 == 5) { Item sgAC5sym = new SGAddressSymbol5(); sgAC5sym.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY + 2, sgwb.SGZ), Map.Tokuno); }
                    }
                    // ********************************************
                    // * Add Crystal Control Device For This Gate *
                    // ********************************************
                    if (sgwb.SGFacetCode == 1)
                    {
                        Item SGACrystal = new SGActivatorDevice(sgwb.SGX, sgwb.SGY, sgwb.SGZ, sgwb.SGFacing, sgwb.SGStyle, sgwb.SGCanBeUsed, sgwb.SGBeingUsed, sgwb.SGEnergy, sgwb.SGHidden, sgwb.SGDiscovered, sgwb.SGLocationName, sgwb.SGFacetCode, sgwb.SGAddressCode1, sgwb.SGAddressCode2, sgwb.SGAddressCode3, sgwb.SGAddressCode4, sgwb.SGAddressCode5);
                        SGACrystal.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY - 2, sgwb.SGZ), Map.Felucca);
                    }
                    else if (sgwb.SGFacetCode == 2)
                    {
                        Item SGACrystal = new SGActivatorDevice(sgwb.SGX, sgwb.SGY, sgwb.SGZ, sgwb.SGFacing, sgwb.SGStyle, sgwb.SGCanBeUsed, sgwb.SGBeingUsed, sgwb.SGEnergy, sgwb.SGHidden, sgwb.SGDiscovered, sgwb.SGLocationName, sgwb.SGFacetCode, sgwb.SGAddressCode1, sgwb.SGAddressCode2, sgwb.SGAddressCode3, sgwb.SGAddressCode4, sgwb.SGAddressCode5);
                        SGACrystal.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY - 2, sgwb.SGZ), Map.Trammel);
                    }
                    else if (sgwb.SGFacetCode == 3)
                    {
                        Item SGACrystal = new SGActivatorDevice(sgwb.SGX, sgwb.SGY, sgwb.SGZ, sgwb.SGFacing, sgwb.SGStyle, sgwb.SGCanBeUsed, sgwb.SGBeingUsed, sgwb.SGEnergy, sgwb.SGHidden, sgwb.SGDiscovered, sgwb.SGLocationName, sgwb.SGFacetCode, sgwb.SGAddressCode1, sgwb.SGAddressCode2, sgwb.SGAddressCode3, sgwb.SGAddressCode4, sgwb.SGAddressCode5);
                        SGACrystal.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY - 2, sgwb.SGZ), Map.Ilshenar);
                    }
                    else if (sgwb.SGFacetCode == 4)
                    {
                        Item SGACrystal = new SGActivatorDevice(sgwb.SGX, sgwb.SGY, sgwb.SGZ, sgwb.SGFacing, sgwb.SGStyle, sgwb.SGCanBeUsed, sgwb.SGBeingUsed, sgwb.SGEnergy, sgwb.SGHidden, sgwb.SGDiscovered, sgwb.SGLocationName, sgwb.SGFacetCode, sgwb.SGAddressCode1, sgwb.SGAddressCode2, sgwb.SGAddressCode3, sgwb.SGAddressCode4, sgwb.SGAddressCode5);
                        SGACrystal.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY - 2, sgwb.SGZ), Map.Malas);
                    }
                    else if (sgwb.SGFacetCode == 5)
                    {
                        Item SGACrystal = new SGActivatorDevice(sgwb.SGX, sgwb.SGY, sgwb.SGZ, sgwb.SGFacing, sgwb.SGStyle, sgwb.SGCanBeUsed, sgwb.SGBeingUsed, sgwb.SGEnergy, sgwb.SGHidden, sgwb.SGDiscovered, sgwb.SGLocationName, sgwb.SGFacetCode, sgwb.SGAddressCode1, sgwb.SGAddressCode2, sgwb.SGAddressCode3, sgwb.SGAddressCode4, sgwb.SGAddressCode5);
                        SGACrystal.MoveToWorld(new Point3D(sgwb.SGX + 2, sgwb.SGY - 2, sgwb.SGZ), Map.Tokuno);
                    }
                }
                // Put correct HUE to Crystals built, System Enabled or Disabled
                if (!SGCore.SGSystemEnabled)
                {
                    // System is Disabled
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
                    // System is Enabled
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
            }
        }

        public static void SGSaveFile(string FilePath)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("");
            Utility.PushColor(ConsoleColor.White);
            Console.Write("Stargate System : ");
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.Green);
            Console.Write("Saving Data...");
     
            XmlDocument doc = new XmlDocument();

            XmlDeclaration Declaration = doc.CreateXmlDeclaration("1.0", "utf-8", "");
            doc.AppendChild(Declaration);
            XmlNode StargateAddresses = doc.CreateNode(XmlNodeType.Element, "StargateAddresses", "");

            foreach (SGEntry entry in SGList)
            {
                XmlNode StargateEntry = doc.CreateNode(XmlNodeType.Element, "StargateEntry", "");

                XmlNode SGX = doc.CreateNode(XmlNodeType.Element, "SGX", "");
                SGX.InnerText = entry.SGX.ToString();
                StargateEntry.AppendChild(SGX);

                XmlNode SGY = doc.CreateNode(XmlNodeType.Element, "SGY", "");
                SGY.InnerText = entry.SGY.ToString();
                StargateEntry.AppendChild(SGY);

                XmlNode SGZ = doc.CreateNode(XmlNodeType.Element, "SGZ", "");
                SGZ.InnerText = entry.SGZ.ToString();
                StargateEntry.AppendChild(SGZ);

                XmlNode SGFacing = doc.CreateNode(XmlNodeType.Element, "SGFacing", "");
                SGFacing.InnerText = entry.SGFacing.ToString();
                StargateEntry.AppendChild(SGFacing);

                XmlNode SGStyle = doc.CreateNode(XmlNodeType.Element, "SGStyle", "");
                SGStyle.InnerText = entry.SGStyle.ToString();
                StargateEntry.AppendChild(SGStyle);

                XmlNode SGCanBeUsed = doc.CreateNode(XmlNodeType.Element, "SGCanBeUsed", "");
                SGCanBeUsed.InnerText = entry.SGCanBeUsed.ToString();
                StargateEntry.AppendChild(SGCanBeUsed);

                XmlNode SGBeingUsed = doc.CreateNode(XmlNodeType.Element, "SGBeingUsed", "");
                SGBeingUsed.InnerText = entry.SGBeingUsed.ToString();
                StargateEntry.AppendChild(SGBeingUsed);

                XmlNode SGEnergy = doc.CreateNode(XmlNodeType.Element, "SGEnergy", "");
                SGEnergy.InnerText = entry.SGEnergy.ToString();
                StargateEntry.AppendChild(SGEnergy);

                XmlNode SGHidden = doc.CreateNode(XmlNodeType.Element, "SGHidden", "");
                SGHidden.InnerText = entry.SGHidden.ToString();
                StargateEntry.AppendChild(SGHidden);

                XmlNode SGDiscovered = doc.CreateNode(XmlNodeType.Element, "SGDiscovered", "");
                SGDiscovered.InnerText = entry.SGDiscovered;
                StargateEntry.AppendChild(SGDiscovered);

                XmlNode SGLocationName = doc.CreateNode(XmlNodeType.Element, "SGLocationName", "");
                SGLocationName.InnerText = entry.SGLocationName;
                StargateEntry.AppendChild(SGLocationName);

                XmlNode SGFacetCode = doc.CreateNode(XmlNodeType.Element, "SGFacetCode", "");
                SGFacetCode.InnerText = entry.SGFacetCode.ToString();
                StargateEntry.AppendChild(SGFacetCode);

                XmlNode SGAddressCode1 = doc.CreateNode(XmlNodeType.Element, "SGAddressCode1", "");
                SGAddressCode1.InnerText = entry.SGAddressCode1.ToString();
                StargateEntry.AppendChild(SGAddressCode1);

                XmlNode SGAddressCode2 = doc.CreateNode(XmlNodeType.Element, "SGAddressCode2", "");
                SGAddressCode2.InnerText = entry.SGAddressCode2.ToString();
                StargateEntry.AppendChild(SGAddressCode2);

                XmlNode SGAddressCode3 = doc.CreateNode(XmlNodeType.Element, "SGAddressCode3", "");
                SGAddressCode3.InnerText = entry.SGAddressCode3.ToString();
                StargateEntry.AppendChild(SGAddressCode3);

                XmlNode SGAddressCode4 = doc.CreateNode(XmlNodeType.Element, "SGAddressCode4", "");
                SGAddressCode4.InnerText = entry.SGAddressCode4.ToString();
                StargateEntry.AppendChild(SGAddressCode4);

                XmlNode SGAddressCode5 = doc.CreateNode(XmlNodeType.Element, "SGAddressCode5", "");
                SGAddressCode5.InnerText = entry.SGAddressCode5.ToString();
                StargateEntry.AppendChild(SGAddressCode5);

                StargateAddresses.AppendChild(StargateEntry);
            }

            doc.AppendChild(StargateAddresses);

            doc.Save(FilePath);

            DateTime end = DateTime.Now;
            Console.WriteLine("done...({1} entries in {0:F1} seconds)", (end - start).TotalSeconds, SGList.Count);
            Utility.PopColor();
        }

        public static void EventSink_WorldSave(WorldSaveEventArgs e)
        {
            SGSaveFile(FilePath);
        }
    }
}