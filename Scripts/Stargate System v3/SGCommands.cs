using System;

using Server;
using Server.Gumps;
using Server.Commands;      // 2.0
using Server.SG;

namespace Server.Commands
{
    public class SGCommands
    {
        // ************************
        // Stargate System Commands
        // ************************
        
        public static void Initialize()
        {
            CommandSystem.Register("SGAdmin", AccessLevel.Administrator, new CommandEventHandler(SGAdmin_OnCommand));
            CommandSystem.Register("SGDelete", AccessLevel.Administrator, new CommandEventHandler(SGDelete_OnCommand));
            CommandSystem.Register("SGGenerate", AccessLevel.Administrator, new CommandEventHandler(SGGenerate_OnCommand));
            CommandSystem.Register("SGInfo", AccessLevel.Administrator, new CommandEventHandler(SGInfo_OnCommand));
        }

        [Usage("SGAdmin")]
        [Description("Access's The Stargate Administration Gump")]
        public static void SGAdmin_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            int sggatecounter = 0;
            int sgaddonid = 1;
            string sgdir = "S";
            string sgplatname = "Default Unnamed";
            int facetid = 1;
            int addcode1 = 1;
            int addcode2 = 2;
            int addcode3 = 3;
            int addcode4 = 4;
            int addcode5 = 5;

            from.SendGump(new SGGumpAdministrator(from, sggatecounter, sgaddonid, sgdir, sgplatname, facetid, addcode1, addcode2, addcode3, addcode4, addcode5));
        }

        [Usage("SGDelete")]
        [Description("Deletes All Stargate v3.0 Components Including Platform Addon's")]
        public static void SGDelete_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            from.SendMessage(89, "Deleting Stargate v3.0 Components");
            SGCore.SGDelete();
        }

        [Usage("SGGenerate")]
        [Description("Generates All Stargate v3.0 Components (Forced Re-load of Current XML File)")]
        public static void SGGenerate_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            from.SendMessage(89, "Generating Stargate v3.0 Components From Current XML File");
            SGCore.SGGenerate();
        }

        [Usage("SGInfo")]
        [Description("World Broadcasts Stargate Count Information To Shard")]
        public static void SGInfo_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            from.SendMessage(89, "Stargate Information...");
            SGCore.SGInfo();
        }
    }
}