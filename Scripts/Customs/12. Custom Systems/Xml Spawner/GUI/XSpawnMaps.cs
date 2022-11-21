using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml;
using Server.Accounting;
using Server;
using Server.Mobiles; 
using Server.Items;
using Server.Commands;
using Server.Commands.Generic;
using Server.Engines.XmlSpawner2;
using Server.Network;
using Server.Gumps;

namespace Server.Commands
{
	public class XSpawn
	{
		public XSpawn()
		{
		}

		public static void Initialize()
		{ 
			CommandSystem.Register( "SpawnTrammel", AccessLevel.Administrator, new CommandEventHandler( SpawnTrammel_OnCommand ) );
			CommandSystem.Register( "SpawnFelucca", AccessLevel.Administrator, new CommandEventHandler( SpawnFelucca_OnCommand ) );
			CommandSystem.Register( "SpawnMalas", AccessLevel.Administrator, new CommandEventHandler( SpawnMalas_OnCommand ) );
			CommandSystem.Register( "SpawnIlshenar", AccessLevel.Administrator, new CommandEventHandler( SpawnIlshenar_OnCommand ) );
			CommandSystem.Register( "SpawnTokuno", AccessLevel.Administrator, new CommandEventHandler( SpawnTokuno_OnCommand ) );
            CommandSystem.Register( "SpawnTermur", AccessLevel.Administrator, new CommandEventHandler( SpawnTermur_OnCommand ));
		}

		[Usage( "[spawntrammel" )]
		[Description( "Spawn Trammel with a menu." )] 
		private static void SpawnTrammel_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new XTrammelGump( e ) );
		}

		[Usage( "[spawnfelucca" )]
		[Description( "Spawn Felucca with a menu." )] 
		private static void SpawnFelucca_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new XFeluccaGump( e ) );
		}

		[Usage( "[spawnmalas" )]
		[Description( "Spawn Malas with a menu." )] 
		private static void SpawnMalas_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new XMalasGump( e ) );
		}

		[Usage( "[spawnilshenar" )]
		[Description( "Spawn Ilshenar with a menu." )] 
		private static void SpawnIlshenar_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new XIlshenarGump( e ) );
		}

		[Usage( "[spawntokuno" )]
		[Description( "Spawn Tokuno with a menu." )] 
		private static void SpawnTokuno_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new XTokunoGump( e ) );
		}
        
        [Usage("[spawntermur")]
        [Description("Spawn Termur with a menu.")]
        private static void SpawnTermur_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new XTermurGump(e));
        }
	}
}

namespace Server.Gumps
{

	public class XTrammelGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public XTrammelGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "TRAMMEL" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 246, "Blighted Grove" );
			AddLabel( 35, 76, 200, "Britain Sewer" );
			AddLabel( 35, 101, 200, "Covetous" );
			AddLabel( 35, 126, 200, "Deceit" );
			AddLabel( 35, 151, 200, "Despise" );
			AddLabel( 35, 176, 200, "Destard" );
			AddLabel( 35, 201, 200, "Fire" );
			AddLabel( 35, 226, 200, "Graveyards" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );
			AddCheck( 182, 198, 210, 211, true, 107 );
			AddCheck( 182, 223, 210, 211, true, 108 );

			AddLabel( 110, 255, 200, "1/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2 );

			AddPage(2);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "TRAMMEL" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Hythloth" );
			AddLabel( 35, 76, 200, "Ice" );
			AddLabel( 35, 101, 200, "Lost Lands" );
			AddLabel( 35, 126, 200, "Orc Caves" );
			AddLabel( 35, 151, 200, "Outdoors" );
			AddLabel( 35, 176, 246, "Painted Caves" );
			AddLabel( 35, 201, 246, "Palace of Paroxysmus" );
			AddLabel( 35, 226, 246, "Prism of Light" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 109 );
			AddCheck( 182, 73, 210, 211, true, 110 );
			AddCheck( 182, 98, 210, 211, true, 111 );
			AddCheck( 182, 123, 210, 211, true, 112 );
			AddCheck( 182, 148, 210, 211, true, 113 );
			AddCheck( 182, 173, 210, 211, true, 114 );
			AddCheck( 182, 198, 210, 211, true, 115 );
			AddCheck( 182, 223, 210, 211, true, 116 );

			AddLabel( 110, 255, 200, "2/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 3 );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1 );

			AddPage(3);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "TRAMMEL" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 246, "Sanctuary" );
			AddLabel( 35, 76, 200, "Sea Life" );
			AddLabel( 35, 101, 200, "Shame" );
			AddLabel( 35, 126, 200, "Solen Hive" );
			AddLabel( 35, 151, 200, "Terathan Keep" );
			AddLabel( 35, 176, 200, "Towns Life" );
			AddLabel( 35, 201, 200, "Towns People" );
			AddLabel( 35, 226, 200, "Trinsic Passage" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 117 );
			AddCheck( 182, 73, 210, 211, true, 118 );
			AddCheck( 182, 98, 210, 211, true, 119 );
			AddCheck( 182, 123, 210, 211, true, 120 );
			AddCheck( 182, 148, 210, 211, true, 121 );
			AddCheck( 182, 173, 210, 211, true, 122 );
			AddCheck( 182, 198, 210, 211, true, 123 );
			AddCheck( 182, 223, 210, 211, true, 124 );

			AddLabel( 110, 255, 200, "3/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 4 );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 2 );

			AddPage(4);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "TRAMMEL" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 97, 10003 );
			AddImageTiled( 163, 25, 2, 97, 10003 );
			AddImageTiled( 218, 25, 2, 97, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			//AddImageTiled( 20, 145, 200, 2, 10001 );
			//AddImageTiled( 20, 170, 200, 2, 10001 );
			//AddImageTiled( 20, 195, 200, 2, 10001 );
			//AddImageTiled( 20, 220, 200, 2, 10001 );
			//AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Vendors" );
			AddLabel( 35, 76, 200, "Wild Life" );
			AddLabel( 35, 101, 200, "Wrong" );
			//AddLabel( 35, 126, 200, "28" );
			//AddLabel( 35, 151, 200, "29" );
			//AddLabel( 35, 176, 200, "30" );
			//AddLabel( 35, 201, 200, "31" );
			//AddLabel( 35, 226, 200, "32" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 125 );
			AddCheck( 182, 73, 210, 211, true, 126 );
			AddCheck( 182, 98, 210, 211, true, 127 );
			//AddCheck( 182, 123, 210, 211, true, 128 );
			//AddCheck( 182, 148, 210, 211, true, 129 );
			//AddCheck( 182, 173, 210, 211, true, 130 );
			//AddCheck( 182, 198, 210, 211, true, 131 );
			//AddCheck( 182, 223, 210, 211, true, 132 );

			AddLabel( 110, 255, 200, "4/4" );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 3 );

			//Ok, Cancel
			AddButton( 55, 280, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 280, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
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
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						from.Say( "SPAWNING TRAMMEL..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/BlightedGrove.xml", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/BritainSewer.xml", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Covetous.xml", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Deceit.xml", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Despise.xml", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Destard.xml", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Fire.xml", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Graveyards.xml", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Hythloth.xml", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Ice.xml", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/LostLands.xml", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/OrcCaves.xml", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Outdoors.xml", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/PaintedCaves.xml", prefix ) );
						}
						if( Selections.Contains( 115 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/PalaceOfParoxysmus.xml", prefix ) );
						}
						if( Selections.Contains( 116 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/PrismOfLight.xml", prefix ) );
						}
						if( Selections.Contains( 117 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Sanctuary.xml", prefix ) );
						}
						if( Selections.Contains( 118 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/SeaLife.xml", prefix ) );
						}
						if( Selections.Contains( 119 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Shame.xml", prefix ) );
						}
						if( Selections.Contains( 120 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/SolenHive.xml", prefix ) );
						}
						if( Selections.Contains( 121 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/TerathanKeep.xml", prefix ) );
						}
						if( Selections.Contains( 122 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/TownsLife.xml", prefix ) );
						}
						if( Selections.Contains( 123 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/TownsPeople.xml", prefix ) );
						}
						if( Selections.Contains( 124 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/TrinsicPassage.xml", prefix ) );
						}
						if( Selections.Contains( 125 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Vendors.xml", prefix ) );
						}
						if( Selections.Contains( 126 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/WildLife.xml", prefix ) );
						}
						if( Selections.Contains( 127 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Trammel/Wrong.xml", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );
					break;
				}
			}
		}
	}

	public class XFeluccaGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public XFeluccaGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "FELUCCA" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 246, "Blighted Grove" );
			AddLabel( 35, 76, 200, "Britain Sewer" );
			AddLabel( 35, 101, 200, "Covetous" );
			AddLabel( 35, 126, 200, "Deceit" );
			AddLabel( 35, 151, 200, "Despise" );
			AddLabel( 35, 176, 200, "Destard" );
			AddLabel( 35, 201, 200, "Fire" );
			AddLabel( 35, 226, 200, "Graveyards" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );
			AddCheck( 182, 198, 210, 211, true, 107 );
			AddCheck( 182, 223, 210, 211, true, 108 );

			AddLabel( 110, 255, 200, "1/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2 );

			AddPage(2);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "FELUCCA" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Hythloth" );
			AddLabel( 35, 76, 200, "Ice" );
			AddLabel( 35, 101, 200, "Khaldun" );
			AddLabel( 35, 126, 200, "Lost Lands" );
			AddLabel( 35, 151, 200, "Orc Caves" );
			AddLabel( 35, 176, 200, "Outdoors" );
			AddLabel( 35, 201, 246, "Painted Caves" );
			AddLabel( 35, 226, 246, "Palace of Paroxysmus" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 109 );
			AddCheck( 182, 73, 210, 211, true, 110 );
			AddCheck( 182, 98, 210, 211, true, 111 );
			AddCheck( 182, 123, 210, 211, true, 112 );
			AddCheck( 182, 148, 210, 211, true, 113 );
			AddCheck( 182, 173, 210, 211, true, 114 );
			AddCheck( 182, 198, 210, 211, true, 115 );
			AddCheck( 182, 223, 210, 211, true, 116 );

			AddLabel( 110, 255, 200, "2/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 3 );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1 );

			AddPage(3);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "FELUCCA" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 246, "Prism of Light" );
			AddLabel( 35, 76, 246, "Sanctuary" );
			AddLabel( 35, 101, 200, "Sea Life" );
			AddLabel( 35, 126, 200, "Shame" );
			AddLabel( 35, 151, 200, "Solen Hive" );
			AddLabel( 35, 176, 200, "Terathan Keep" );
			AddLabel( 35, 201, 200, "Towns Life" );
			AddLabel( 35, 226, 200, "Towns People" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 117 );
			AddCheck( 182, 73, 210, 211, true, 118 );
			AddCheck( 182, 98, 210, 211, true, 119 );
			AddCheck( 182, 123, 210, 211, true, 120 );
			AddCheck( 182, 148, 210, 211, true, 121 );
			AddCheck( 182, 173, 210, 211, true, 122 );
			AddCheck( 182, 198, 210, 211, true, 123 );
			AddCheck( 182, 223, 210, 211, true, 124 );

			AddLabel( 110, 255, 200, "3/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 4 );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 2 );

			AddPage(4);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "FELUCCA" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 122, 10003 );
			AddImageTiled( 163, 25, 2, 122, 10003 );
			AddImageTiled( 218, 25, 2, 122, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			//AddImageTiled( 20, 170, 200, 2, 10001 );
			//AddImageTiled( 20, 195, 200, 2, 10001 );
			//AddImageTiled( 20, 220, 200, 2, 10001 );
			//AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Trinsic Passage" );
			AddLabel( 35, 76, 200, "Vendors" );
			AddLabel( 35, 101, 200, "Wild Life" );
			AddLabel( 35, 126, 200, "Wrong" );
			//AddLabel( 35, 151, 200, "29" );
			//AddLabel( 35, 176, 200, "30" );
			//AddLabel( 35, 201, 200, "31" );
			//AddLabel( 35, 226, 200, "32" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 125 );
			AddCheck( 182, 73, 210, 211, true, 126 );
			AddCheck( 182, 98, 210, 211, true, 127 );
			AddCheck( 182, 123, 210, 211, true, 128 );
			//AddCheck( 182, 148, 210, 211, true, 129 );
			//AddCheck( 182, 173, 210, 211, true, 130 );
			//AddCheck( 182, 198, 210, 211, true, 131 );
			//AddCheck( 182, 223, 210, 211, true, 132 );

			AddLabel( 110, 255, 200, "4/4" );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 3 );

			//Ok, Cancel
			AddButton( 55, 280, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 280, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
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
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						from.Say( "SPAWNING FELUCCA..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/BlightedGrove.xml", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/BritainSewer.xml", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Covetous.xml", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Deceit.xml", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Despise.xml", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Destard.xml", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Fire.xml", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Graveyards.xml", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Hythloth.xml", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Ice.xml", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Khaldun.xml", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/LostLands.xml", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/OrcCaves.xml", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Outdoors.xml", prefix ) );
						}
						if( Selections.Contains( 115 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/PaintedCaves.xml", prefix ) );
						}
						if( Selections.Contains( 116 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/PalaceOfParoxysmus.xml", prefix ) );
						}
						if( Selections.Contains( 117 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/PrismOfLight.xml", prefix ) );
						}
						if( Selections.Contains( 118 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Sanctuary.xml", prefix ) );
						}
						if( Selections.Contains( 119 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/SeaLife.xml", prefix ) );
						}
						if( Selections.Contains( 120 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Shame.xml", prefix ) );
						}
						if( Selections.Contains( 121 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/SolenHive.xml", prefix ) );
						}
						if( Selections.Contains( 122 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/TerathanKeep.xml", prefix ) );
						}
						if( Selections.Contains( 123 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/TownsLife.xml", prefix ) );
						}
						if( Selections.Contains( 124 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/TownsPeople.xml", prefix ) );
						}
						if( Selections.Contains( 125 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/TrinsicPassage.xml", prefix ) );
						}
						if( Selections.Contains( 126 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Vendors.xml", prefix ) );
						}
						if( Selections.Contains( 127 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/WildLife.xml", prefix ) );
						}
						if( Selections.Contains( 128 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Felucca/Wrong.xml", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );
					break;
				}
			}
		}
	}

	public class XIlshenarGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public XIlshenarGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//fundo cinza
			AddBackground( 0, 0, 243, 310, 5054 );
			//----------
			AddLabel( 93, 2, 200, "ILSHENAR" );
			//fundo branco
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );
			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );
			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 220, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Ancient Lair" );
			AddLabel( 35, 76, 200, "Ankh" );
			AddLabel( 35, 101, 200, "Blood" );
			AddLabel( 35, 126, 200, "Exodus" );
			AddLabel( 35, 151, 200, "Mushroom" );
			AddLabel( 35, 176, 200, "Outdoors" );
			AddLabel( 35, 201, 200, "Ratman cave" );
			AddLabel( 35, 226, 200, "Rock" );

			//Options
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );
			AddCheck( 182, 198, 210, 211, true, 107 );
			AddCheck( 182, 223, 210, 211, true, 108 );

			AddLabel( 110, 255, 200, "1/2" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2 );

			AddPage(2);

			//fundo cinza
			AddBackground( 0, 0, 243, 310, 5054 );
			//----------
			AddLabel( 93, 2, 200, "ILSHENAR" );
			//fundo branco
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );
			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );
			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 171, 10003 );
			AddImageTiled( 163, 25, 2, 171, 10003 );
			AddImageTiled( 220, 25, 2, 171, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			//AddImageTiled( 20, 220, 200, 2, 10001 );
			//AddImageTiled( 20, 245, 200, 2, 10001 );

			//----------
			AddLabel( 35, 51, 200, "Sorcerers" );
			AddLabel( 35, 76, 200, "Spectre" );
			AddLabel( 35, 101, 200, "Towns" );
			AddLabel( 35, 126, 200, "Vendors" );
			AddLabel( 35, 151, 200, "Wisp" );
			AddLabel( 35, 176, 246, "Twisted Weald" );
			//AddLabel( 35, 201, 200, "15" );
			//AddLabel( 35, 226, 200, "16" );

			//Options
			AddCheck( 182, 48, 210, 211, true, 109 );
			AddCheck( 182, 73, 210, 211, true, 110 );
			AddCheck( 182, 98, 210, 211, true, 111 );
			AddCheck( 182, 123, 210, 211, true, 112 );
			AddCheck( 182, 148, 210, 211, true, 113 );
			AddCheck( 182, 173, 210, 211, true, 114 );
			//AddCheck( 182, 198, 210, 211, true, 115 );
			//AddCheck( 182, 223, 210, 211, true, 116 );

			AddLabel( 110, 255, 200, "2/2" );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1 );

			//Ok, Cancel
			AddButton( 55, 280, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 280, 241, 243, 0, GumpButtonType.Reply, 0 );

		}

		public override void OnResponse( NetState state, RelayInfo info )
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
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						from.Say( "SPAWNING ILSHENAR..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Ancientlair.xml", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Ankh.xml", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Blood.xml", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Exodus.xml", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Mushroom.xml", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Outdoors.xml", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Ratmancave.xml", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Rock.xml", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Sorcerers.xml", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Spectre.xml", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Towns.xml", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Vendors.xml", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/Wisp.xml", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Ilshenar/TwistedWeald.xml", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );

					break;
				}
			}
		}
	}

	public class XMalasGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public XMalasGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//fundo cinza
			//alt era 310
			AddBackground( 0, 0, 243, 295, 5054 );

			//----------

			AddLabel( 100, 2, 200, "MALAS" );
			//fundo branco
			//x, y, largura, altura, item
			//alt era 232
			AddImageTiled( 10, 20, 220, 235, 3004 );

			//----------

			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );
			//colunas
			//x, y, comprimento, ?, item
			//comp era 222
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 220, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
                        AddImageTiled(20, 220, 200, 2, 10001);
                        AddImageTiled(20, 245, 200, 2, 10001);

			//Map names
			AddLabel( 35, 51, 200, "Doom" );
			AddLabel( 35, 76, 200, "North" );
			AddLabel( 35, 101, 200, "OrcForts" );
			AddLabel( 35, 126, 200, "South" );
			AddLabel( 35, 151, 200, "Vendors" );
			AddLabel( 35, 176, 246, "Citadel" );
                        AddLabel(35, 201, 246, "Labyrinth");
                        AddLabel(35, 226, 246, "Bedlam");

			//Options
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );
                        AddCheck(182, 198, 210, 211, true, 107);
                        AddCheck(182, 223, 210, 211, true, 108);

			//Ok, Cancel
			// alt era 280
			AddButton( 55, 265, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 265, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
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
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						from.Say( "SPAWNING MALAS..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Malas/Doom.xml", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Malas/North.xml", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Malas/OrcForts.xml", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Malas/South.xml", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Malas/Vendors.xml", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Malas/Citadel.xml", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Malas/Labyrinth.xml", prefix ) );
                        }
                        if (Selections.Contains(108) == true)
                        {
                            CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Malas/Bedlam.xml", prefix));
                        }
					}

					from.Say( "Spawn generation completed!" );

					break;
				}
			}
		}
	}

	public class XTokunoGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public XTokunoGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//fundo cinza
			//alt era 310
			AddBackground( 0, 0, 243, 250, 5054 );
			//----------
			AddLabel( 95, 2, 200, "TOKUNO" );
			//fundo branco
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 183, 3004 );
			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 167, 27, 200, "Spawn It" );
			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 172, 10003 );
			AddImageTiled( 163, 25, 2, 172, 10003 );
			AddImageTiled( 220, 25, 2, 172, 10003 );
			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			//Map names
			AddLabel( 35, 51, 200, "Fan Dancers Dojo" );
			AddLabel( 35, 76, 200, "Outdoors" );
			AddLabel( 35, 101, 200, "Towns Life" );
			AddLabel( 35, 126, 200, "Vendors" );
			AddLabel( 35, 151, 200, "Wild Life" );
			AddLabel( 35, 176, 200, "Yomutso Mines" );

			//Options
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );

			//Ok, Cancel
			// alt era 280
			AddButton( 55, 220, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 220, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
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
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						from.Say( "SPAWNING TOKUNO..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Tokuno/FanDancersDojo.xml", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Tokuno/Outdoors.xml", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Tokuno/TownsLife.xml", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Tokuno/Vendors.xml", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Tokuno/WildLife.xml", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}XmlLoad Spawns/Tokuno/YomutsoMines.xml", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );

					break;
				}
			}
		}
	}

    public class XTermurGump : Gump
    {
        private CommandEventArgs m_CommandEventArgs;
        public XTermurGump(CommandEventArgs e)
            : base(50, 50)
        {
            m_CommandEventArgs = e;
            Closable = true;
            Dragable = true;

            AddPage(1);

            //grey background
            AddBackground(0, 0, 240, 310, 5054);

            //----------
            AddLabel(95, 2, 200, "TER MUR");

            //white background
            //x, y, largura, altura, item
            AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            AddLabel(30, 27, 200, "Map name");
            AddLabel(167, 27, 200, "Spawn It");

            //colunas
            //x, y, comprimento, ?, item
            AddImageTiled(20, 25, 2, 222, 10003);
            AddImageTiled(163, 25, 2, 222, 10003);
            AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            AddImageTiled(20, 25, 200, 2, 10001);
            AddImageTiled(20, 45, 200, 2, 10001);
            AddImageTiled(20, 70, 200, 2, 10001);
            AddImageTiled(20, 95, 200, 2, 10001);
            AddImageTiled(20, 120, 200, 2, 10001);
            AddImageTiled(20, 145, 200, 2, 10001);
            AddImageTiled(20, 170, 200, 2, 10001);
            AddImageTiled(20, 195, 200, 2, 10001);
            AddImageTiled(20, 220, 200, 2, 10001);
            AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            AddLabel(35, 51, 200, "Crimson Veins");
            AddLabel(35, 76, 200, "Enslaved Goblins");
            AddLabel(35, 101, 200, "Fire Island Ruins");
            AddLabel(35, 126, 200, "Fractured City");
            AddLabel(35, 151, 200, "Lands of the Lich");
            AddLabel(35, 176, 200, "Lava Caldera");
            AddLabel(35, 201, 200, "Passage of Tears");
            AddLabel(35, 226, 200, "Secret Garden");

            //Check boxes
            AddCheck(182, 48, 210, 211, true, 101);
            AddCheck(182, 73, 210, 211, true, 102);
            AddCheck(182, 98, 210, 211, true, 103);
            AddCheck(182, 123, 210, 211, true, 104);
            AddCheck(182, 148, 210, 211, true, 105);
            AddCheck(182, 173, 210, 211, true, 106);
            AddCheck(182, 198, 210, 211, true, 107);
            AddCheck(182, 223, 210, 211, true, 108);

            AddLabel(110, 255, 200, "1/5");
            AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2);

            AddPage(2);

            //grey background
            AddBackground(0, 0, 240, 310, 5054);

            //----------
            AddLabel(95, 2, 200, "TER MUR");

            //white background
            //x, y, largura, altura, item
            AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            AddLabel(30, 27, 200, "Map name");
            AddLabel(167, 27, 200, "Spawn It");

            //colunas
            //x, y, comprimento, ?, item
            AddImageTiled(20, 25, 2, 222, 10003);
            AddImageTiled(163, 25, 2, 222, 10003);
            AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            AddImageTiled(20, 25, 200, 2, 10001);
            AddImageTiled(20, 45, 200, 2, 10001);
            AddImageTiled(20, 70, 200, 2, 10001);
            AddImageTiled(20, 95, 200, 2, 10001);
            AddImageTiled(20, 120, 200, 2, 10001);
            AddImageTiled(20, 145, 200, 2, 10001);
            AddImageTiled(20, 170, 200, 2, 10001);
            AddImageTiled(20, 195, 200, 2, 10001);
            AddImageTiled(20, 220, 200, 2, 10001);
            AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            AddLabel(35, 51, 200, "Cavern of the Discarded");
            AddLabel(35, 76, 200, "Clan Scratch");
            AddLabel(35, 101, 246, "Tomb of Kings");
            AddLabel(35, 126, 246, "Underworld");
            AddLabel(35, 151, 246, "Abyss");
            AddLabel(35, 176, 200, "Atoll Blend");
            AddLabel(35, 201, 200, "Chicken Chase");
            AddLabel(35, 226, 200, "City Residential");

            //Check boxes
            AddCheck(182, 48, 210, 211, true, 109);
            AddCheck(182, 73, 210, 211, true, 110);
            AddCheck(182, 98, 210, 211, true, 111);
            AddCheck(182, 123, 210, 211, true, 112);
            AddCheck(182, 148, 210, 211, true, 113);
            AddCheck(182, 173, 210, 211, true, 114);
            AddCheck(182, 198, 210, 211, true, 115);
            AddCheck(182, 223, 210, 211, true, 116);

            AddLabel(110, 255, 200, "2/5");
            AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 3);
            AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1);

            AddPage(3);

            //grey background
            AddBackground(0, 0, 240, 310, 5054);

            //----------
            AddLabel(95, 2, 200, "TER MUR");

            //white background
            //x, y, largura, altura, item
            AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            AddLabel(30, 27, 200, "Map name");
            AddLabel(167, 27, 200, "Spawn It");

            //colunas
            //x, y, comprimento, ?, item
            AddImageTiled(20, 25, 2, 222, 10003);
            AddImageTiled(163, 25, 2, 222, 10003);
            AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            AddImageTiled(20, 25, 200, 2, 10001);
            AddImageTiled(20, 45, 200, 2, 10001);
            AddImageTiled(20, 70, 200, 2, 10001);
            AddImageTiled(20, 95, 200, 2, 10001);
            AddImageTiled(20, 120, 200, 2, 10001);
            AddImageTiled(20, 145, 200, 2, 10001);
            AddImageTiled(20, 170, 200, 2, 10001);
            AddImageTiled(20, 195, 200, 2, 10001);
            AddImageTiled(20, 220, 200, 2, 10001);
            AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            AddLabel(35, 51, 200, "Coral Desert");
            AddLabel(35, 76, 200, "Fisherman's Reach");
            AddLabel(35, 101, 200, "Gated Isle");
            AddLabel(35, 126, 200, "High Plains");
            AddLabel(35, 151, 200, "Kepetch Waste");
            AddLabel(35, 176, 200, "Lava Lake");
            AddLabel(35, 201, 200, "Lava Pit Pyramid");
            AddLabel(35, 226, 200, "Lost Settlement");

            //Check boxes
            AddCheck(182, 48, 210, 211, true, 117);
            AddCheck(182, 73, 210, 211, true, 118);
            AddCheck(182, 98, 210, 211, true, 119);
            AddCheck(182, 123, 210, 211, true, 120);
            AddCheck(182, 148, 210, 211, true, 121);
            AddCheck(182, 173, 210, 211, true, 122);
            AddCheck(182, 198, 210, 211, true, 123);
            AddCheck(182, 223, 210, 211, true, 124);

            AddLabel(110, 255, 200, "3/5");
            AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 4);
            AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 2);

            AddPage(4);

            //grey background
            AddBackground(0, 0, 240, 310, 5054);

            //----------
            AddLabel(95, 2, 200, "TER MUR");

            //white background
            //x, y, largura, altura, item
            AddImageTiled(10, 20, 220, 232, 3004);

            //----------
            AddLabel(30, 27, 200, "Map name");
            AddLabel(167, 27, 200, "Spawn It");

            //colunas
            //x, y, comprimento, ?, item
            AddImageTiled(20, 25, 2, 222, 10003);
            AddImageTiled(163, 25, 2, 222, 10003);
            AddImageTiled(218, 25, 2, 222, 10003);

            //Linhas
            //x, y, comprimento, ?, item
            AddImageTiled(20, 25, 200, 2, 10001);
            AddImageTiled(20, 45, 200, 2, 10001);
            AddImageTiled(20, 70, 200, 2, 10001);
            AddImageTiled(20, 95, 200, 2, 10001);
            AddImageTiled(20, 120, 200, 2, 10001);
            AddImageTiled(20, 145, 200, 2, 10001);
            AddImageTiled(20, 170, 200, 2, 10001);
            AddImageTiled(20, 195, 200, 2, 10001);
            AddImageTiled(20, 220, 200, 2, 10001);
            AddImageTiled(20, 245, 200, 2, 10001);

            //Map names
            AddLabel(35, 51, 200, "Northern Steppe");
            AddLabel(35, 76, 200, "Raptor Isle");
            AddLabel(35, 101, 200, "Slith Valley");
            AddLabel(35, 126, 200, "Spider Island");
            AddLabel(35, 151, 200, "Talon Point" );
            AddLabel(35, 176, 200, "Treefellow Course" );
            AddLabel(35, 201, 200, "Void Isle" );
            AddLabel(35, 226, 200, "Walled Circus");

            //Check boxes
            AddCheck(182, 48, 210, 211, true, 125);
            AddCheck(182, 73, 210, 211, true, 126);
            AddCheck(182, 98, 210, 211, true, 127);
            AddCheck(182, 123, 210, 211, true, 128);
            AddCheck(182, 148, 210, 211, true, 129 );
            AddCheck(182, 173, 210, 211, true, 130 );
            AddCheck(182, 198, 210, 211, true, 131 );
            AddCheck(182, 223, 210, 211, true, 132 );

            AddLabel(110, 255, 200, "4/5");
            AddButton(200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 5);
            AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 3);

            AddPage(5);

            //fundo cinza
	    AddBackground( 0, 0, 243, 310, 5054 );
	    //----------
	    AddLabel( 93, 2, 200, "TER MUR" );
	    //fundo branco
	    //x, y, largura, altura, item
	    AddImageTiled( 10, 20, 220, 232, 3004 );
	    //----------
	    AddLabel( 30, 27, 200, "Map name" );
	    AddLabel( 167, 27, 200, "Spawn It" );
	    //colunas
	    //x, y, comprimento, ?, item
	    AddImageTiled( 20, 25, 2, 171, 10003 );
	    AddImageTiled( 163, 25, 2, 171, 10003 );
            AddImageTiled( 220, 25, 2, 171, 10003 );

	    //Linhas
	    //x, y, comprimento, ?, item
	    AddImageTiled( 20, 25, 200, 2, 10001 );
	    AddImageTiled( 20, 45, 200, 2, 10001 );
	    AddImageTiled( 20, 70, 200, 2, 10001 );
	    AddImageTiled( 20, 95, 200, 2, 10001 );
	    AddImageTiled( 20, 120, 200, 2, 10001 );
	    AddImageTiled( 20, 145, 200, 2, 10001 );
	    AddImageTiled( 20, 170, 200, 2, 10001 );
	    AddImageTiled( 20, 195, 200, 2, 10001 );
	    //AddImageTiled( 20, 220, 200, 2, 10001 );
	    //AddImageTiled( 20, 245, 200, 2, 10001 );

            //Map names
            AddLabel(35, 51, 200, "Waterfall Point");
            AddLabel(35, 76, 246, "Shrine of Singularity");
            AddLabel(35, 101, 200, "Toxic Desert");
            AddLabel(35, 126, 200, "Vendor");
            AddLabel(35, 151, 246, "Royal City" );
            AddLabel(35, 176, 246, "Holy City" );
            //AddLabel( 35, 201, 200, "39" );
	    //AddLabel( 35, 226, 200, "40" );   
          
            //Check boxes
            AddCheck(182, 48, 210, 211, true, 133);
            AddCheck(182, 73, 210, 211, true, 134);
            AddCheck(182, 98, 210, 211, true, 135);
            AddCheck(182, 123, 210, 211, true, 136);            
            AddCheck(182, 148, 210, 211, true, 137 );
            AddCheck(182, 173, 210, 211, true, 138 );
            //AddCheck( 182, 198, 210, 211, true, 139 );
	    //AddCheck( 182, 223, 210, 211, true, 140 ); 
           
            AddLabel(110, 255, 200, "5/5");
            AddButton(10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 4);

            //Ok, Cancel
            AddButton(55, 280, 247, 249, 1, GumpButtonType.Reply, 0);
            AddButton(125, 280, 241, 243, 0, GumpButtonType.Reply, 0);
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

                            from.Say("SPAWNING Ter Mur...");

                            // Now spawn any selected maps

                            if (Selections.Contains(101) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/crimsonveins.xml", prefix));
                            }
                            if (Selections.Contains(102) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/enslavedgoblins.xml", prefix));
                            }
                            if (Selections.Contains(103) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/fireislandruins.xml", prefix));
                            }
                            if (Selections.Contains(104) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/fracturedcity.xml", prefix));
                            }
                            if (Selections.Contains(105) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/landsofthelich.xml", prefix));
                            }
                            if (Selections.Contains(106) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/lavacaldera.xml", prefix));
                            }
                            if (Selections.Contains(107) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/passageoftears.xml", prefix));
                            }
                            if (Selections.Contains(108) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/secretgarden.xml", prefix));
                            }
                            if (Selections.Contains(109) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/cavernofthediscarded.xml", prefix));
                            }
                            if (Selections.Contains(110) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/clanscratch.xml", prefix));
                            }
                            if (Selections.Contains(111) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/tombofkings.xml", prefix));
                            }
                            if (Selections.Contains(112) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/underworld.xml", prefix));
                            }
                            if (Selections.Contains(113) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/abyss.xml", prefix));
                            }
                            if (Selections.Contains(114) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/atollblend.xml", prefix));
                            }
                            if (Selections.Contains(115) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/chickenchase.xml", prefix));
                            }
                            if (Selections.Contains(116) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/cityresidential.xml", prefix));
                            }
                            if (Selections.Contains(117) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/coraldesert.xml", prefix));
                            }
                            if (Selections.Contains(118) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/fishermansreach.xml", prefix));
                            }
                            if (Selections.Contains(119) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/gatedisle.xml", prefix));
                            }
                            if (Selections.Contains(120) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/highplains.xml", prefix));
                            }
                            if (Selections.Contains(121) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/kepetchwaste.xml", prefix));
                            }
                            if (Selections.Contains(122) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/lavalake.xml", prefix));
                            }
                            if (Selections.Contains(123) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/lavapitpyramid.xml", prefix));
                            }
                            if (Selections.Contains(124) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/lostsettlement.xml", prefix));
                            }
                            if (Selections.Contains(125) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/northernsteppe.xml", prefix));
                            }
                            if (Selections.Contains(126) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/raptorisle.xml", prefix));
                            }
                            if (Selections.Contains(127) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/slithvalley.xml", prefix));
                            }
                            if (Selections.Contains(128) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/spiderisland.xml", prefix));
                            }
                            if (Selections.Contains(129) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/talonpoint.xml", prefix));
                            }
                            if (Selections.Contains(130) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/treefellowcourse.xml", prefix));
                            }
                            if (Selections.Contains(131) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/voidisle.xml", prefix));
                            }
                            if (Selections.Contains(132) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/walledcircus.xml", prefix));
                            }
                            if (Selections.Contains(133) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/waterfallpoint.xml", prefix));
                            }
                            if (Selections.Contains(134) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/shrine.xml", prefix));
                            }
                            if (Selections.Contains(135) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/toxicdesert.xml", prefix));
                            }
                            if (Selections.Contains(136) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/vendors.xml", prefix));
                            }
                            if (Selections.Contains(137) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/royalcity.xml", prefix));
                            }
                            if (Selections.Contains(138) == true)
                            {
                                CommandSystem.Handle(from, String.Format("{0}XmlLoad Spawns/Termur/holycity.xml", prefix));
                            }
                        }

                        from.Say("Spawn generation completed!");
                        break;
                    }
            }
        }
    }
}