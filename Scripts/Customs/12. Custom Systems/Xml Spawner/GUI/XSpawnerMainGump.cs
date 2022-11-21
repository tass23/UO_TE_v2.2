using System;
using System.Collections;
using System.IO;
using Server;
using Server.Mobiles; 
using Server.Items;
using Server.Commands; 
using Server.Network;
using Server.Gumps;

namespace Server.Commands 
{
	public class XSpawnerCommand
	{
		public XSpawnerCommand()
		{
		}

		public static void Initialize() 
		{ 
			CommandSystem.Register( "XSpawner", AccessLevel.Administrator, new CommandEventHandler( XSpawner_OnCommand ) );
			CommandSystem.Register( "Spawner", AccessLevel.Administrator, new CommandEventHandler( XSpawner_OnCommand ) ); 
		}
 
		[Usage( "just use [XSpawner" )]
		[Aliases( "Spawner" )]
		[Description( "Main Gump to access Xml Spawner functions." )]
		private static void XSpawner_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new XSpawnerMainGump( e ) );
		}
	}
}

namespace Server.Gumps
{
	public class XSpawnerMainGump : Gump
	{
		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}

		private CommandEventArgs m_CommandEventArgs;
		public XSpawnerMainGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			AddBackground( 0, 0, 267, 450, 5054 );

			AddHtml( 8, 8, 250, 42, "    PREMIUM SPAWNER (Based)<BR>" + "by Nerun" + "      Modded by: Demented           Rev.8", true, false );

			AddBlackAlpha( 8, 58, 250, 50 );

			AddBlackAlpha( 8, 116, 250, 130 );

			AddBlackAlpha( 8, 254, 250, 130 );

			AddButton( 220, 405, 0x158A, 0x158B, 10000, GumpButtonType.Reply, 1 ); //Quit Button
//Options---------------------
			AddLabel( 10, 60, 52, "WORLD CREATION OPTIONS" );

			AddLabel( 45, 80, 52, "Create World gump" );
			AddButton( 25, 80, 0x845, 0x846, 10001, GumpButtonType.Reply, 0 );

			AddLabel( 10, 118, 52, "SPAWN OPTIONS" );

			AddLabel( 45, 138, 52, "Trammel" );
			AddButton( 25, 138, 0x845, 0x846, 10002, GumpButtonType.Reply, 0 );

			AddLabel( 45, 158, 52, "Felucca" );
			AddButton( 25, 158, 0x845, 0x846, 10003, GumpButtonType.Reply, 0 );

			AddLabel( 45, 178, 52, "Ilshenar" );
			AddButton( 25, 178, 0x845, 0x846, 10004, GumpButtonType.Reply, 0 );

			AddLabel( 45, 198, 52, "Malas" );
			AddButton( 25, 198, 0x845, 0x846, 10104, GumpButtonType.Reply, 0 );

			AddLabel( 45, 218, 52, "Tokuno" );
			AddButton( 25, 218, 0x845, 0x846, 10105, GumpButtonType.Reply, 0 );

			AddLabel( 165, 138, 52, "Ter Mur" );
			AddButton( 145, 138, 0x845, 0x846, 10106, GumpButtonType.Reply, 0 );

			AddLabel( 10, 256, 52, "UNLOAD SPAWNS" );

			AddLabel( 45, 278, 52, "Trammel" );
			AddButton( 25, 278, 0x845, 0x846, 10005, GumpButtonType.Reply, 0 );

			AddLabel( 45, 298, 52, "Felucca" );
			AddButton( 25, 298, 0x845, 0x846, 10006, GumpButtonType.Reply, 0 );

			AddLabel( 45, 318, 52, "Ilshenar" );
			AddButton( 25, 318, 0x845, 0x846, 10007, GumpButtonType.Reply, 0 );

			AddLabel( 45, 338, 52, "Malas" );
			AddButton( 25, 338, 0x845, 0x846, 10107, GumpButtonType.Reply, 0 );

			AddLabel( 45, 358, 52, "Tokuno" );
			AddButton( 25, 358, 0x845, 0x846, 10108, GumpButtonType.Reply, 0 );

			AddLabel( 165, 278, 52, "Ter Mur" );
			AddButton( 145, 278, 0x845, 0x846, 10109, GumpButtonType.Reply, 0 );

			AddLabel( 120, 410, 200, "1/3" );
			AddButton( 145, 410, 0x15E1, 0x15E5, 0, GumpButtonType.Page, 2 );
// -----------------------------------------
			AddPage(2);

			AddBackground( 0, 0, 267, 450, 5054 );

			AddHtml( 8, 8, 250, 42, "    PREMIUM SPAWNER (Based)<BR>" + "by Nerun" + "      Modded by: Demented           Rev.8", true, false );

			AddBlackAlpha( 8, 58, 250, 70 );

			AddBlackAlpha( 8, 133, 250, 90 );

			AddBlackAlpha( 8, 229, 250, 150 );

			AddButton( 220, 405, 0x158A, 0x158B, 10000, GumpButtonType.Reply, 1 ); //Quit Button
//Options---------------------
			AddLabel( 10, 60, 52, "SAVE OPTIONS" );
			AddLabel( 10, 135, 52, "REMOVE OPTIONS" );
			AddLabel( 10, 230, 52, "EDIT OPTIONS" );

			AddLabel( 45, 80, 52, "Save All spawns (World.xml)" );
			AddButton( 25, 80, 0x845, 0x846, 10008, GumpButtonType.Reply, 0 );

//			AddLabel( 45, 100, 52, "Save 'By Map' spawns (byhand.map)" );
//			AddButton( 25, 100, 0x845, 0x846, 10009, GumpButtonType.Reply, 0 );

//			AddLabel( 45, 120, 52, "Save spawns inside Region" );
//			AddButton( 25, 120, 0x845, 0x846, 10010, GumpButtonType.Reply, 0 );

//			AddLabel( 45, 140, 52, "Save spawns by Coordinates" );
//			AddButton( 25, 140, 0x845, 0x846, 10011, GumpButtonType.Reply, 0 );

			AddLabel( 45, 158, 52, "Remove All spawners (all facets)" );
			AddButton( 25, 158, 0x845, 0x846, 10012, GumpButtonType.Reply, 0 );

			AddLabel( 45, 178, 52, "Remove All spawners (current map)" );
			AddButton( 25, 178, 0x845, 0x846, 10013, GumpButtonType.Reply, 0 );

//			AddLabel( 45, 238, 52, "Remove spawners by ID" );
//			AddButton( 25, 238, 0x845, 0x846, 10014, GumpButtonType.Reply, 0 );

//			AddLabel( 45, 258, 52, "Remove spawners by Coordinates" );
//			AddButton( 25, 258, 0x845, 0x846, 10015, GumpButtonType.Reply, 0 );

//			AddLabel( 45, 278, 52, "Remove spawners inside Region" );
//			AddButton( 25, 278, 0x845, 0x846, 10016, GumpButtonType.Reply, 0 );

			AddLabel( 45, 250, 52, "Show All Spawners" );
			AddButton( 25, 250, 0x845, 0x846, 10015, GumpButtonType.Reply, 0 );
			
			AddLabel( 45, 270, 52, "Hide All Spawners" );
			AddButton( 25, 270, 0x845, 0x846, 10016, GumpButtonType.Reply, 0 );
			
			AddLabel( 45, 290, 52, "Add Dialog to Spawner" );
			AddButton( 25, 290, 0x845, 0x846, 10017, GumpButtonType.Reply, 0 );

			AddLabel( 45, 198, 52, "Clear All Facets" );
			AddButton( 25, 198, 0x845, 0x846, 10018, GumpButtonType.Reply, 0 );

			AddLabel( 45, 310, 52, "Set my own body to GM Style" );
			AddButton( 25, 310, 0x845, 0x846, 10019, GumpButtonType.Reply, 0 );

			AddLabel( 120, 410, 200, "2/3" );
			AddButton( 100, 410, 0x15E3, 0x15E7, 0, GumpButtonType.Page, 1 );
			AddButton( 145, 410, 0x15E1, 0x15E5, 0, GumpButtonType.Page, 3 );
// -----------------------------------------
			AddPage(3);

			AddBackground( 0, 0, 267, 450, 5054 );

			AddHtml( 8, 8, 250, 42, "    PREMIUM SPAWNER (Based)<BR>" + "by Nerun" + "      Modded by: Demented         Rev.8", true, false );

			AddBlackAlpha( 8, 58, 250, 70 );

			//AddBlackAlpha( 8, 116, 250, 70 );

			AddButton( 220, 405, 0x158A, 0x158B, 10000, GumpButtonType.Reply, 1 ); //Quit Button
//Options---------------------
			AddLabel( 10, 60, 52, "CONVERSION UTILITY" );
			//AddLabel( 10, 118, 52, "SMART PLAYER RANGE SENSITIVE" );

			AddLabel( 45, 80, 52, "RunUO Spawns to XSpawner" );
			AddButton( 25, 80, 0x845, 0x846, 10200, GumpButtonType.Reply, 0 );

			//AddLabel( 45, 138, 52, "Import MSF Map" );
			//AddButton( 25, 138, 0x845, 0x846, 10301, GumpButtonType.Reply, 0 );

			//AddLabel( 45, 158, 52, "Import Spawner Map" );
			//AddButton( 25, 158, 0x845, 0x846, 10302, GumpButtonType.Reply, 0 );
			
			//AddLabel( 45, 168, 52, "Turn Off Spawners" );
			//AddButton( 25, 168, 0x845, 0x846, 10303, GumpButtonType.Reply, 0 );
			
			//AddLabel( 45, 188, 52, "Turn On Spawners" );
			//AddButton( 25, 188, 0x845, 0x846, 10304, GumpButtonType.Reply, 0 );

			AddLabel( 120, 410, 200, "3/3" );
			AddButton( 100, 410, 0x15E3, 0x15E7, 0, GumpButtonType.Page, 2 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			string prefix = Server.Commands.CommandSystem.Prefix;

			switch( info.ButtonID )
			{
				case 10000:
				{
					//Quit
					break;
				}
				case 10001:
				{
					CommandSystem.Handle( from, String.Format( "{0}createworld", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10002:
				{
					CommandSystem.Handle( from, String.Format( "{0}spawntrammel", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10003:
				{
					CommandSystem.Handle( from, String.Format( "{0}spawnfelucca", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10004:
				{
					CommandSystem.Handle( from, String.Format( "{0}spawnilshenar", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10005:
				{
					CommandSystem.Handle( from, String.Format( "{0}unloadtrammel", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10006:
				{
					CommandSystem.Handle( from, String.Format( "{0}unloadfelucca", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10007:
				{
					CommandSystem.Handle( from, String.Format( "{0}unloadilshenar", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10008:
				{
					CommandSystem.Handle( from, String.Format( "{0}XmlSpawnerSaveAll World.xml", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
//				case 10009:
//				{
//					CommandSystem.Handle( from, String.Format( "{0}spawngen savebyhand", prefix ) );
//					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
//					break;
//				}
//				case 10010:
//				{
//					CommandSystem.Handle( from, String.Format( "{0}GumpSaveRegion", prefix ) );
//					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
//					break;
//				}
//				case 10011:
//				{
//					CommandSystem.Handle( from, String.Format( "{0}GumpSaveCoordinate", prefix ) );
//					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
//					break;
//				}
				case 10012:
				{
					CommandSystem.Handle( from, String.Format( "{0}XmlSpawnerWipeAll", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10013:
				{
					CommandSystem.Handle( from, String.Format( "{0}XmlSpawnerWipe", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
//				case 10014:
//				{
//					CommandSystem.Handle( from, String.Format( "{0}GumpRemoveID", prefix ) );
//					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
//					break;
//				}
//				case 10015:
//				{
//					CommandSystem.Handle( from, String.Format( "{0}GumpRemoveCoordinate", prefix ) );
//					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
//					break;
//				}
//				case 10016:
//				{
//					CommandSystem.Handle( from, String.Format( "{0}GumpRemoveRegion", prefix ) );
//					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
//					break;
//				}
				case 10015:
				{
					CommandSystem.Handle( from, String.Format( "{0}XmlSpawnerShowAll", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10016:
				{
					CommandSystem.Handle( from, String.Format( "{0}XmlSpawnerHideAll", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10017:
				{
					CommandSystem.Handle( from, String.Format( "{0}XmlEdit", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10018:
				{
					CommandSystem.Handle( from, String.Format( "{0}clearall", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10019:
				{
					CommandSystem.Handle( from, String.Format( "{0}gmbody", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10104:
				{
					CommandSystem.Handle( from, String.Format( "{0}spawnmalas", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10105:
				{
					CommandSystem.Handle( from, String.Format( "{0}spawntokuno", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10106:
				{
					CommandSystem.Handle( from, String.Format( "{0}spawntermur", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10107:
				{
					CommandSystem.Handle( from, String.Format( "{0}unloadmalas", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10108:
				{
					CommandSystem.Handle( from, String.Format( "{0}unloadtokuno", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10109:
				{
					CommandSystem.Handle( from, String.Format( "{0}unloadtermur", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				case 10200:
				{
					CommandSystem.Handle( from, String.Format( "{0}rse", prefix ) );
					CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
					break;
				}
				//case 10301:
				//{
				//	CommandSystem.Handle( from, String.Format( "(0)XmlImportMSF", prefix ) );
				//	CommandSystem.Handle( from, String.Format( "(0)spawner", prefix ) );
				//}
				//case 10302:
				//{
				//	CommandSystem.Handle( from, String.Format( "(0)XmlImportSpawners", prefix ) );
				//	CommandSystem.Handle( from, String.Format( "(0)spawner", prefix ) );
				//}
				//case 10303:
				//{
				//	CommandSystem.Handle( from, String.Format( "(0)StopAllRegionSpawns", prefix ) );
				//	CommandSystem.Handle( from, String.Format( "(0)spawner", prefix ) );
				//}	
				//case 10304:
				//{
				//	CommandSystem.Handle( from, String.Format( "(0)StartAllRegionSpawns", prefix ) );
				//	CommandSystem.Handle( from, String.Format( "(0)spawner", prefix ) );
				//}				
			}
		}
	}
}