using System;
using System.IO;
using System.Text;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.SAMiniChamps;
using Server.Commands;

namespace Server.Commands
{
	public class GenMiniChampController
	{
		public static void Initialize()
		{
			CommandSystem.Register( "GenMiniChampController" , AccessLevel.Administrator, new CommandEventHandler( GenMiniChampController_OnCommand ) );
			CommandSystem.Register( "GenMCC" , AccessLevel.Administrator, new CommandEventHandler( GenMiniChampController_OnCommand ) );
		}

		[Usage( "GenMiniChampController" )]
		[Aliases( "GenMCC" )]
		[Description( "Install Mini Champ Spawn Controller at 1417 1695." )]
		public static void GenMiniChampController_OnCommand( CommandEventArgs e )
		{
			Map map1 = Map.Felucca;

			MiniChampSpawnController controller = new MiniChampSpawnController();

			controller.Active = true;
			controller.MoveToWorld( new Point3D( 1417, 1695, 0 ), map1 );

			e.Mobile.SendMessage( "Done. Look for Mini Champ Spawn Controller at 1417 1695." );
		}
	}
}