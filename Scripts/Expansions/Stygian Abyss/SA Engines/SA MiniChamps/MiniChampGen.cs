using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.SAMiniChamps;
using Server.Commands;

namespace Server.Commands
{
	public class MiniChampGen
	{
		public static void Initialize()
		{
			CommandSystem.Register( "MiniChampGen" , AccessLevel.Administrator, new CommandEventHandler( MiniChampGen_OnCommand ) );
			CommandSystem.Register( "MiniChamp" , AccessLevel.Administrator, new CommandEventHandler( MiniChampGen_OnCommand ) );
		}

		[Usage( "MiniChampGen" )]
		[Aliases( "MiniChamp" )]
		[Description( "Spawns Mini Champs for The Abyss" )]
		public static void MiniChampGen_OnCommand( CommandEventArgs e )
		{
			Map map = e.Mobile.Map;

			if ( map == Map.TerMur )
			{
				e.Mobile.SendMessage( "Mini Champ Spawns generating.." );
				MiniChampSpawn SecretGarden = new MiniChampSpawn();
				MiniChampSpawn StygianDragonLair = new MiniChampSpawn();
				MiniChampSpawn CrimsonVeins = new MiniChampSpawn();
				MiniChampSpawn AbyssalLair = new MiniChampSpawn();
				MiniChampSpawn FireTemple = new MiniChampSpawn();
				MiniChampSpawn LandsOfTheLich = new MiniChampSpawn();
				MiniChampSpawn SkeletalDragon = new MiniChampSpawn();
				MiniChampSpawn EnslavedGoblins = new MiniChampSpawn();
				MiniChampSpawn LavaCaldera = new MiniChampSpawn();
				MiniChampSpawn PassageOfTears = new MiniChampSpawn();
				MiniChampSpawn ClanChitter = new MiniChampSpawn();
				MiniChampSpawn ClanRibbon = new MiniChampSpawn();
				MiniChampSpawn ClanScratch = new MiniChampSpawn();

				SecretGarden.RandomizeType = false;
				SecretGarden.Active = false;
				SecretGarden.ConfinedRoaming = true;
				SecretGarden.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				SecretGarden.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				SecretGarden.MoveToWorld( new Point3D( 435, 701, 48 ), map ); 		//Secret Garden
				e.Mobile.SendMessage( "Secret Garden Mini Champ Generated" );
				
				StygianDragonLair.RandomizeType = false;
				StygianDragonLair.Active = false;
				StygianDragonLair.ConfinedRoaming = true;
				StygianDragonLair.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				StygianDragonLair.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				StygianDragonLair.MoveToWorld( new Point3D( 887, 275, 22 ), map ); 	//Stygian Dragon Lair
				e.Mobile.SendMessage( "Stygian Dragon Lair Mini Champ Generated" );
				
				CrimsonVeins.RandomizeType = false;
				CrimsonVeins.Active = false;
				CrimsonVeins.ConfinedRoaming = true;
				CrimsonVeins.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				CrimsonVeins.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				CrimsonVeins.MoveToWorld( new Point3D( 952, 190, -11 ), map ); 		//Crimson Veins
				e.Mobile.SendMessage( "Crimson Veins Mini Champ Generated" );
				
				AbyssalLair.RandomizeType = false;
				AbyssalLair.Active = false;
				AbyssalLair.ConfinedRoaming = true;
				AbyssalLair.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				AbyssalLair.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				AbyssalLair.MoveToWorld( new Point3D( 976, 406, 73 ), map ); 		//Abyssal Lair Entrance
				e.Mobile.SendMessage( "Abyssal Lair Entrance Mini Champ Generated" );
				
				FireTemple.RandomizeType = false;
				FireTemple.Active = false;
				FireTemple.ConfinedRoaming = true;
				FireTemple.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				FireTemple.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				FireTemple.MoveToWorld( new Point3D( 548, 761, -72 ), map ); 		//Fire Temple Ruins
				e.Mobile.SendMessage( "Fire Temple Ruins Mini Champ Generated" );
				
				LandsOfTheLich.RandomizeType = false;
				LandsOfTheLich.Active = false;
				LandsOfTheLich.ConfinedRoaming = true;
				LandsOfTheLich.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				LandsOfTheLich.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				LandsOfTheLich.MoveToWorld( new Point3D( 534, 660, 28 ), map ); 	//Lands Of The Lich
				e.Mobile.SendMessage( "Lands Of The Lich Mini Champ Generated" );
				
				SkeletalDragon.RandomizeType = false;
				SkeletalDragon.Active = false;
				SkeletalDragon.ConfinedRoaming = true;
				SkeletalDragon.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				SkeletalDragon.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				SkeletalDragon.MoveToWorld( new Point3D( 676, 831, -89 ), map ); 	//Skeletal Dragon
				e.Mobile.SendMessage( "Skeletal Dragon Mini Champ Generated" );
				
				EnslavedGoblins.RandomizeType = false;
				EnslavedGoblins.Active = false;
				EnslavedGoblins.ConfinedRoaming = true;
				EnslavedGoblins.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				EnslavedGoblins.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				EnslavedGoblins.MoveToWorld( new Point3D( 571, 805, -20 ), map ); 	//Enslaved Goblins
				e.Mobile.SendMessage( "Enslaved Goblins Mini Champ Generated" );
				
				LavaCaldera.RandomizeType = false;
				LavaCaldera.Active = false;
				LavaCaldera.ConfinedRoaming = true;
				LavaCaldera.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				LavaCaldera.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				LavaCaldera.MoveToWorld( new Point3D( 581, 898, -53 ), map ); 		//Lava Caldera
				e.Mobile.SendMessage( "Lava Caldera Mini Champ Generated" );
				
				PassageOfTears.RandomizeType = false;
				PassageOfTears.Active = false;
				PassageOfTears.ConfinedRoaming = true;
				PassageOfTears.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				PassageOfTears.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				PassageOfTears.MoveToWorld( new Point3D( 702, 608, -15 ), map ); 		//Passage Of Tears
				e.Mobile.SendMessage( "Passage Of Tears Mini Champ Generated" );
				
				ClanChitter.RandomizeType = false;
				ClanChitter.Active = false;
				ClanChitter.ConfinedRoaming = true;
				ClanChitter.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				ClanChitter.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				ClanChitter.MoveToWorld( new Point3D( 979, 493, 8 ), map ); 		//Clan Chitter
				e.Mobile.SendMessage( "Clan Chitter Mini Champ Generated" );
				
				ClanRibbon.RandomizeType = false;
				ClanRibbon.Active = false;
				ClanRibbon.ConfinedRoaming = true;
				ClanRibbon.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				ClanRibbon.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				ClanRibbon.MoveToWorld( new Point3D( 916, 500, 8 ), map ); 			//Clan Ribbon
				e.Mobile.SendMessage( "Clan Ribbon Mini Champ Generated" );
				
				ClanScratch.RandomizeType = false;
				ClanScratch.Active = false;
				ClanScratch.ConfinedRoaming = true;
				ClanScratch.RestartDelay = TimeSpan.FromMinutes( 10.0 );
				ClanScratch.ExpireDelay = TimeSpan.FromMinutes( 20.0 );
				ClanScratch.MoveToWorld( new Point3D( 950, 553, 8 ), map ); 		//Clan Scratch
				e.Mobile.SendMessage( "Clan Scratch Mini Champ Generated" );

				e.Mobile.SendMessage( "All Abyss Mini Champs have been generated." );
			}
			else
				e.Mobile.SendMessage( "You must be on the Ter Mur map to use this command." ); //Safety measure to make sure Ter Mur is enabled on the server.
		}
	}
}