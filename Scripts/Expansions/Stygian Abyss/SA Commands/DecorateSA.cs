using System;
using System.IO;
using System.Xml;
using Server.Commands;
using Server.Engines.Quests;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server
{
	public static class StygianAbyss
	{
        private static bool m_StygianDragonLair;
        private static bool m_MedusasLair;
 
        public static bool StygianDragonLair
        {
            get
            {
                return m_StygianDragonLair;
            }
            set
            {
                m_StygianDragonLair = value;
            }
        }
        public static bool MedusasLair
        {
            get
            {
                return m_MedusasLair;
            }
            set
            {
                m_MedusasLair = value;
            }
        }

		public static void Initialize()
		{
			CommandSystem.Register( "DecorateSA", AccessLevel.Administrator, new CommandEventHandler( DecorateSA_OnCommand ) );
			CommandSystem.Register( "SettingsSA", AccessLevel.Administrator, new CommandEventHandler( SettingsSA_OnCommand ) );
			
			LoadSettings();
		}
		
        public static bool FindItem(int x, int y, int z, Map map, Item test)
        {
            return FindItem(new Point3D(x, y, z), map, test);
        }

        public static bool FindItem(Point3D p, Map map, Item test)
        {
            IPooledEnumerable eable = map.GetItemsInRange(p);

            foreach (Item item in eable)
            {
                if (item.Z == p.Z && item.ItemID == test.ItemID)
                {
                    eable.Free();
                    return true;
                }
            }
			
            eable.Free();
            return false;
        }

        public static void LoadSettings()
        {
            if (!Directory.Exists("Data/Stygian Abyss"))
                Directory.CreateDirectory("Data/Stygian Abyss");
				
            if (!File.Exists("Data/Stygian Abyss/Settings.xml"))
                File.Create("Data/Stygian Abyss/Settings.xml");
				
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Path.Combine(Core.BaseDirectory, "Data/Stygian Abyss/Settings.xml"));
				
                XmlElement root = doc["Settings"];
				
                if (root == null)
                    return;

                ReadNode(root, "StygianDragonLair", ref m_StygianDragonLair);
                ReadNode(root, "MedusasLair", ref m_MedusasLair);

            }
            catch
            {
            }
        }

        public static void SaveSetings()
        {
            if (!Directory.Exists("Data/Stygian Abyss"))
                Directory.CreateDirectory("Data/Stygian Abyss");
				
            if (!File.Exists("Data/Stygian Abyss/Settings.xml"))
                File.Create("Data/Stygian Abyss/Settings.xml");
			
            try
            { 
                XmlDocument doc = new XmlDocument();
                doc.Load(Path.Combine(Core.BaseDirectory, "Data/Stygian Abyss/Settings.xml"));
				
                XmlElement root = doc["Settings"];
				
                if (root == null)
                    return;

                UpdateNode(root, "StygianDragonLair", m_StygianDragonLair);
                UpdateNode(root, "MedusasLair", m_MedusasLair);
				
                doc.Save("Data/Stygian Abyss/Settings.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while updating 'Settings.xml': {0}", e);
            }
        }

        public static void ReadNode(XmlElement root, string dungeon, ref bool val)
        {
            if (root == null)
                return;
			
            foreach (XmlElement element in root.SelectNodes(dungeon))
            { 
                if (element.HasAttribute("active"))
                    val = XmlConvert.ToBoolean(element.GetAttribute("active"));
            }
        }

        public static void UpdateNode(XmlElement root, string dungeon, bool val)
        {
            if (root == null)
                return;
			
            foreach (XmlElement element in root.SelectNodes(dungeon))
            { 
                if (element.HasAttribute("active"))
                    element.SetAttribute("active", XmlConvert.ToString(val));	
            }
        }

		[Usage( "DecorateSA" )]
		[Description( "Generates Stygian Abyss world decoration." )]
		private static void DecorateSA_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage( "Generating Stygian Abyss world decoration, please wait." );
			
			Decorate.Generate( "Data/Stygian Abyss/Ter Mur", Map.TerMur );
			Decorate.Generate( "Data/Stygian Abyss/Trammel", Map.Trammel );
			Decorate.Generate( "Data/Stygian Abyss/Felucca", Map.Felucca );
			
            PeerlessAltar altar;
            PeerlessTeleporter tele;
            StygianDragonBrazier brazier;
            MedusaNest nest;
			
            // Stygian Dragon Lair - Abyss
            altar = new StygianDragonAltar();

            if (!FindItem(363, 157, 5, Map.TerMur, altar))
            {
                altar.MoveToWorld(new Point3D(363, 157, 0), Map.TerMur);
                tele = new PeerlessTeleporter(altar);
                tele.PointDest = altar.ExitDest;
               
                tele.MoveToWorld(new Point3D(305, 159, 105), Map.TerMur);

                brazier = new StygianDragonBrazier((StygianDragonAltar)altar, 0x207B);
                brazier.MoveToWorld(new Point3D(362, 156, 5), Map.TerMur);

                brazier = new StygianDragonBrazier((StygianDragonAltar)altar, 0x207B);
                brazier.MoveToWorld(new Point3D(364, 156, 7), Map.TerMur);

                brazier = new StygianDragonBrazier((StygianDragonAltar)altar, 0x207B);
                brazier.MoveToWorld(new Point3D(364, 158, 7), Map.TerMur);

                brazier = new StygianDragonBrazier((StygianDragonAltar)altar, 0x207B);
                brazier.MoveToWorld(new Point3D(362, 158, 7), Map.TerMur);
            }

            //Medusa Lair - Abyss
            altar = new MedusaAltar();

            if (!FindItem(822, 756, 56, Map.TerMur, altar))
            {
                altar.MoveToWorld(new Point3D(822, 756, 56), Map.TerMur);
                tele = new PeerlessTeleporter(altar);
                tele.PointDest = altar.ExitDest;
              
                tele.MoveToWorld(new Point3D(840, 926, -5), Map.TerMur);

                nest = new MedusaNest((MedusaAltar)altar, 0x207B);
                nest.MoveToWorld(new Point3D(821, 755, 56), Map.TerMur);

                nest = new MedusaNest((MedusaAltar)altar, 0x207B);
                nest.MoveToWorld(new Point3D(823, 755, 56), Map.TerMur);

                nest = new MedusaNest((MedusaAltar)altar, 0x207B);
                nest.MoveToWorld(new Point3D(821, 757, 56), Map.TerMur);

                nest = new MedusaNest((MedusaAltar)altar, 0x207B);
                nest.MoveToWorld(new Point3D(823, 757, 56), Map.TerMur);
            }
			
			e.Mobile.SendMessage( "Stygian Abyss world generation complete." );
		}
		
		[Usage( "SettingsSA" )]
		[Description( "Stygian Abyss Settings." )]
		private static void SettingsSA_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new SAGump() );
		}
	}
	
    public class SAGump : Gump
    { 
        public SAGump()
            : base(50, 50)
        {
            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;
			
            AddPage(0);			
            AddBackground(0, 0, 308, 390, 0x2454);
			
            // title
            AddLabel(125, 10, 150, "Settings");
            AddImage(256, 5, 0x9E1);
			
            // dungeons			

            AddButton(20, 285, StygianAbyss.StygianDragonLair ? 0x939 : 0x938, StygianAbyss.StygianDragonLair ? 0x939 : 0x938, 1, GumpButtonType.Reply, 0);
            AddButton(20, 310, StygianAbyss.MedusasLair ? 0x939 : 0x938, StygianAbyss.MedusasLair ? 0x939 : 0x938, 2, GumpButtonType.Reply, 0);

			

            AddLabel(45, 281, 0x226, "StygianDragonLair");
            AddLabel(45, 306, 0x226, "MedusasLair");

			
            // legend
            AddLabel(243, 205, 0x226, "Legend:");
			
            AddImage(218, 235, 0x938);
            AddLabel(243, 231, 0x226, "disabled");
            AddImage(218, 260, 0x939);
            AddLabel(243, 256, 0x226, "enabled");
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            switch ( info.ButtonID )
            {
                case 0:
                    StygianAbyss.SaveSetings();
                    break; 
                case 1:
                    StygianAbyss.StygianDragonLair = !StygianAbyss.StygianDragonLair;
                    break;
                case 2:
                    StygianAbyss.MedusasLair = !StygianAbyss.MedusasLair;
                    break;
            }
			
            if (info.ButtonID > 0)
                sender.Mobile.SendGump(new SAGump());
        }
    }
}