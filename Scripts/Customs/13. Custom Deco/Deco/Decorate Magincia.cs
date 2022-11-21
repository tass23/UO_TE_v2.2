using System;
using Server;
using System.IO;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.Engines.Quests;
using System.Collections;
using System.Collections.Generic;

namespace Server.Commands
{
    public static class Magincia
    {
        public static void Initialize()
        {
            CommandSystem.Register("DecorateMagincia", AccessLevel.Administrator, new CommandEventHandler(DecorateMagincia_OnCommand));
            CommandSystem.Register("MaginciaDelete", AccessLevel.Administrator, new CommandEventHandler(MaginciaDelete_OnCommand));
        }

        [Usage("DecorateMagincia")]
        [Description("Generates Magincia.")]
        private static void DecorateMagincia_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Generating Magincia, please wait.");

            Decorate.Generate("Data/Decoration/Magincia/Trammel", Map.Trammel);
            Decorate.Generate("Data/Decoration/Magincia/Fel", Map.Felucca);

            e.Mobile.SendMessage("Magincia Decoration Complete.");
        }

        [Usage("MaginciaDelete")]
        [Description("Deletes Magincia world decoration.")]
        private static void MaginciaDelete_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Deleting Magincia world decoration, please wait.");

            DecorateDelete.Generate("Data/Decoration/Magincia/Trammel", Map.Trammel);
            DecorateDelete.Generate("Data/Decoration/Magincia/Fel", Map.Felucca);

            e.Mobile.SendMessage("Magincia world decoration deleting complete.");
        }

        private static Queue<Item> m_DeleteQueue = new Queue<Item>();

        public static bool FindItem(int x, int y, int z, Map map, Item test)
        {
            return FindItem(new Point3D(x, y, z), map, test);
        }

        public static bool FindItem(Point3D p, Map map, Item test)
        {
            bool result = false;

            IPooledEnumerable eable = map.GetItemsInRange(p);

            foreach (Item item in eable)
            {
                if (item.Z == p.Z && item.ItemID == test.ItemID)
                {
                    m_DeleteQueue.Enqueue(item);
                    result = true;
                }
            }

            eable.Free();

            while (m_DeleteQueue.Count > 0)
                m_DeleteQueue.Dequeue().Delete();

            return result;
        }
    }
}