using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.Quests.Haven;
using Server.Engines.Quests.Necro;
using System.Collections.Generic;

namespace Server.Commands
{
	public class DecorateMLDelete
	{
		public static void Initialize()
		{
			CommandSystem.Register( "DecorateMLDelete", AccessLevel.Administrator, new CommandEventHandler( DecorateMLDelete_OnCommand ) );
		}

		[Usage("DecorateMLDelete")]
		[Description("Deletes Mondain's Legacy world decoration.")]
		private static void DecorateMLDelete_OnCommand(CommandEventArgs e)
		{
			e.Mobile.SendMessage("Deleting Mondain's Legacy world decoration, please wait.");

			DecorateDelete.Generate("Data/Mondain's Legacy/Trammel", Map.Trammel);
			DecorateDelete.Generate("Data/Mondain's Legacy/Felucca", Map.Felucca);
			DecorateDelete.Generate("Data/Mondain's Legacy/Ilshenar", Map.Ilshenar);
			DecorateDelete.Generate("Data/Mondain's Legacy/Malas", Map.Malas);
			DecorateDelete.Generate("Data/Mondain's Legacy/Tokuno", Map.Tokuno);
			
			PeerlessAltar altar;
			PeerlessTeleporter tele;
			PrismOfLightPillar pillar;

			// Bedlam - Malas
			altar = new BedlamAltar();
			FindItem(86, 1627, 0, Map.Malas, altar);
			tele = new PeerlessTeleporter(altar);
			FindItem(99, 1617, 50, Map.Malas, tele);
			tele.Delete();
			altar.Delete();

			// Blighted Grove - Trammel
			altar = new BlightedGroveAltar();
			FindItem(6502, 875, 0, Map.Trammel, altar);
			tele = new PeerlessTeleporter(altar);
			FindItem(6511, 949, 26, Map.Trammel, tele);
			tele.Delete();
			altar.Delete();

			// Blighted Grove - Felucca
			altar = new BlightedGroveAltar();
			FindItem(6502, 875, 0, Map.Felucca, altar);
			tele = new PeerlessTeleporter(altar);
			FindItem(6511, 949, 26, Map.Felucca, tele);
			tele.Delete();
			altar.Delete();

			// Palace of Paroxysmus - Trammel
			altar = new ParoxysmusAltar();
			FindItem(6511, 506, -34, Map.Trammel, altar);
			tele = new PeerlessTeleporter(altar);
			FindItem(6518, 365, 46, Map.Trammel, tele);
			tele.Delete();
			altar.Delete();

			// Palace of Paroxysmus - Felucca
			altar = new ParoxysmusAltar();
			FindItem(6511, 506, -34, Map.Felucca, altar);
			tele = new PeerlessTeleporter(altar);
			FindItem(6518, 365, 46, Map.Felucca, tele);
			tele.Delete();
			altar.Delete();

			// Prism of Light - Trammel
			altar = new PrismOfLightAltar();
			FindItem(6509, 167, 6, Map.Trammel, altar);
			tele = new PeerlessTeleporter(altar);
			tele.ItemID = 0xDDA;
			FindItem(6501, 137, -20, Map.Trammel, tele);
			tele.Delete();
			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x581);
			FindItem(6506, 167, 0, Map.Trammel, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x581);
			FindItem(6509, 164, 0, Map.Trammel, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x581);
			FindItem(6506, 164, 0, Map.Trammel, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x481);
			FindItem(6512, 167, 0, Map.Trammel, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x481);
			FindItem(6509, 170, 0, Map.Trammel, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x481);
			FindItem(6512, 170, 0, Map.Trammel, pillar);
			pillar.Delete();
			altar.Delete();

			// Prism of Light - Felucca
			altar = new PrismOfLightAltar();
			FindItem(6509, 167, 6, Map.Felucca, altar);
			tele = new PeerlessTeleporter(altar);
			tele.ItemID = 0xDDA;
			FindItem(6501, 137, -20, Map.Felucca, tele);
			tele.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x581);
			FindItem(6506, 167, 0, Map.Felucca, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x581);
			FindItem(6509, 164, 0, Map.Felucca, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x581);
			FindItem(6506, 164, 0, Map.Felucca, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x481);
			FindItem(6512, 167, 0, Map.Felucca, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x481);
			FindItem(6509, 170, 0, Map.Felucca, pillar);
			pillar.Delete();

			pillar = new PrismOfLightPillar((PrismOfLightAltar)altar, 0x481);
			FindItem(6512, 170, 0, Map.Felucca, pillar);
			pillar.Delete();
			altar.Delete();

			// Citadel - Malas
			altar = new CitadelAltar();
			FindItem(90, 1884, 0, Map.Malas, altar);
			tele = new PeerlessTeleporter(altar);
			FindItem(114, 1955, 0, Map.Malas, tele);
			tele.Delete();
			altar.Delete();
			
			// Star Wars - Malas
			altar = new SWAltar();
			FindItem( 89, 237, 26, Map.Malas, altar );
			tele = new PeerlessTeleporter( altar );
			FindItem( 114, 1955, 0, Map.Malas, tele );
			tele.Delete();
			altar.Delete();

			// Twisted Weald - Ilshenar
			altar = new TwistedWealdAltar();
			FindItem(2170, 1255, -60, Map.Ilshenar, altar);
			tele = new PeerlessTeleporter(altar);
			FindItem(2139, 1271, -57, Map.Ilshenar, tele);
			tele.Delete();
			altar.Delete();

			e.Mobile.SendMessage("Mondain's Legacy world decoration deleting complete.");
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
