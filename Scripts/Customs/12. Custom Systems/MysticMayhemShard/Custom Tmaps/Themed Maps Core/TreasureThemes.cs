
using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Misc
{
	//Theme types new ones to be added here or removed.
	public enum ChestThemeType
	{	
		None = 0,
		//Level 2
		Skeleton,
		Lizardmen,
		Ratmen,
		Orc,
		Corpser,
		//Level 3
		Solen,
		Brigand,
		Savage,
		Ettin,
		Ogre,
		FrostTroll,
		Juka,
		Spider,
		//Level 4
		Undead,
		Pirate,
		Ophidian,
		Terathan,
		Reaper,
		Fey,
		Minotaur,
		//Level 5
		Dragon,
		SkeletonDragon,
		Titan,
		Dead,
		Ice,
		//Level 6
		Demon,
		Lich,
		Shadow

		
		
	};

	//overall control class for treasure chest spawn mechanics
	public class TreasureTheme
	{
	
		//spawns strongest mob in given theme based on spot 0 in  creature list or 0/1 for undead theme
		public static BaseCreature SpawnHighestMob(ChestThemeType theme)
		{
			switch (theme)
			{
				default: break;
				case ChestThemeType.Undead:
				{
					int random = Utility.Random(1);
					return (BaseCreature)Activator.CreateInstance( ThemeTypes[(int)theme][random]); 
				}

				case ChestThemeType.Pirate:
				{
					return (BaseCreature)Activator.CreateInstance( ThemeTypes[(int)theme][0]);
				}
	
			}
			return null;
		}

		public static bool GetIsThemed(int level)
		{
			//15% chance of it being set to true
			if (level >= 2 )
			{
				if (Utility.RandomDouble() < .15 ) return true; 
			}
			return false;
		}
	
		public static int GetGuardianSpawn(bool IsThemed, ChestThemeType theme)
		{
			int NormalGuardians = 5; // spawns 4 guardians as if a normal chest
			int ThemeGuardians;

			if(theme == ChestThemeType.Pirate || theme == ChestThemeType.Undead)
			{
				ThemeGuardians = 6; //spawns 5 guardians if is a themed chest of above type because special case of spawning 1 highlevel mob
			}
			else
				ThemeGuardians = 8;

			if (IsThemed == true) return  ThemeGuardians;
			return NormalGuardians;
		}

		//returns a random theme type based on level of map
		public static int GetThemeType(int level)
		{
			int theme = 0;
			if(level == 2) theme = Utility.RandomMinMax(1, 5);
			if(level == 3) theme = Utility.RandomMinMax(6, 13);
			if(level == 4) theme = Utility.RandomMinMax(14, 20);
			if(level == 5) theme = Utility.RandomMinMax(21, 25);
			if(level == 6) theme = Utility.RandomMinMax(26, 28);
			return theme;
		}

		//returns string to send to player when chest is dug
		public static string GetThemeMessage(ChestThemeType type)
		{ 
			if ((int)type >= 0 && (int)type < ThemeMessages.Length ) 
			{
				try
				{
					return ThemeMessages[(int)type ];
				}
				catch {}
				//(Exception ex) { EventSink.InvokeLogException(new LogExceptionEventArgs(ex)); }
			}
			return null;
		}

		
		//Begin Spawn mechanics based on level and if themed and theme type
		private static BaseCreature Spawn( int level, bool IsThemed, ChestThemeType theme, bool guardian )
		{
			//handle standered spawn levels based on chest level
			if (IsThemed == false &&  level >= 0 && level < StanderedTypes.Length )
			{
				try
				{
					return (BaseCreature)Activator.CreateInstance( StanderedTypes[level][Utility.Random( StanderedTypes[level].Length )] );
				}
				catch{}
				// (Exception ex) { EventSink.InvokeLogException(new LogExceptionEventArgs(ex)); }
			}

			//handle ThemeSpawns depending on themeType
			if (IsThemed == true &&  theme >= 0 && (int)theme < ThemeTypes.Length )
			{
				try
				{
					//if not a special case chest spawn random creatures based on list
					if(guardian == false)
					{ 
						return (BaseCreature)Activator.CreateInstance( ThemeTypes[(int)theme][Utility.Random( ThemeTypes[(int)theme].Length )] );
					}
					if(guardian == true)
					{
						//begin special case check for iob themed chests to not spawn level 5 mobs dureing initial guardian spawn.
						if(theme == ChestThemeType.Undead) return (BaseCreature)Activator.CreateInstance(ThemeTypes[(int)theme][Utility.RandomMinMax(2,4) ]) ;
						if(theme == ChestThemeType.Pirate) return (BaseCreature)Activator.CreateInstance(ThemeTypes[(int)theme][Utility.RandomMinMax(1,3) ]) ;
					}
					else
					{
						//if guardian is true but not of above type return random based monster
						return (BaseCreature)Activator.CreateInstance(ThemeTypes[(int)theme][Utility.Random(ThemeTypes[(int)theme].Length) ]) ;
					}
				}
				catch{} 
				//(Exception ex) { EventSink.InvokeLogException(new LogExceptionEventArgs(ex)); }
			}
			return null;
		}

		//Main spawn generation function called from any item that needs to generate treasure type spawn.
		public static void Spawn( int level, Point3D p, Map map, Mobile target, bool IsThemed, ChestThemeType theme, bool guardian, bool guardian2)
		{
			//bool guardian is designator for if theme type has special spawn mechanics and thus does not spawn its highest mobs at random
			//guardian2 is second flag indicateing to spawn highest mob.
			if ( map == null )
				return;
			BaseCreature c = null;
			if(guardian == false) c = Spawn( level, IsThemed, theme, guardian ); 
			if(guardian == true && guardian2 == false) c = Spawn( level, IsThemed, theme, true ); 
			if(guardian == true && guardian2 == true) c = SpawnHighestMob(theme);
			if ( c != null )
			{
				c.Home = p;
				c.RangeHome = 8;

				bool spawned = false;

				for ( int i = 0; !spawned && i < 10; ++i )
				{
					int x = p.X - 3 + Utility.Random( 7 );
					int y = p.Y - 3 + Utility.Random( 7 );

					if ( map.CanSpawnMobile( x, y, p.Z ) )
					{
						c.MoveToWorld( new Point3D( x, y, p.Z ), map );
						spawned = true;
					}
					else
					{
						int z = map.GetAverageZ( x, y );

						if ( map.CanSpawnMobile( x, y, z ) )
						{
							c.MoveToWorld( new Point3D( x, y, z ), map );
							spawned = true;
						}
					}
				}

				if ( !spawned )
					c.Delete();
				else if ( target != null )
					c.Combatant = target;
			}
		}


		//standered treasuremap spawns levels 0-5, add additional ones here
		private static Type[][] StanderedTypes = new Type[][]
		{
			new Type[]{ typeof( Mongbat ), typeof( Skeleton ) },
			new Type[]{ typeof( Mongbat ), typeof( Ratman ), typeof( HeadlessOne ), typeof( Skeleton ), typeof( Zombie ) },
			new Type[]{ typeof( OrcishMage ), typeof( Gargoyle ), typeof( Gazer ), typeof( HellHound ), typeof( EarthElemental ) },
			new Type[]{ typeof( Lich ), typeof( OgreLord ), typeof( DreadSpider ), typeof( AirElemental ), typeof( FireElemental ) },
			new Type[]{ typeof( DreadSpider ), typeof( LichLord ), typeof( Daemon ), typeof( ElderGazer ), typeof( OgreLord ) },
			new Type[]{ typeof( LichLord ), typeof( Daemon ), typeof( ElderGazer ), typeof( PoisonElemental ), typeof( BloodElemental ) },
			new Type[]{ typeof( AncientWyrm ), typeof( Balron ), typeof( BloodElemental ), typeof( PoisonElemental ), typeof( Titan ) }
		};
	
		//ThemedChest spawn levels add additional ones here
		private static Type[][] ThemeTypes = new Type[][]
		{
			new Type[]{ }, // level 0 blank chest no guardians or spawn
				//Level 2
			new Type[]{ typeof( SkeletalKnight ), typeof( BoneKnight ), typeof( BoneKnightLord ) }, // Skeleton
			new Type[]{ typeof( Lizardman ), typeof( Lizardman ), typeof( Lizardman ) }, // Lizardmen
			new Type[]{ typeof( Ratman ), typeof( RatmanMage ), typeof( RatmanArcher ) }, // Ratmen
			new Type[]{ typeof( Orc ), typeof( OrcishLord ), typeof( OrcishMage ) }, // Orc
			new Type[]{ typeof( Corpser ), typeof( WhippingVine ) }, // Corpser
				//Level 3
			new Type[]{ typeof(RedSolenWorker), typeof(RedSolenWarrior), typeof(RedSolenInfiltratorQueen), typeof(RedSolenInfiltratorWarrior) },
			new Type[]{ typeof( Brigand ), typeof( BrigandArcher ) },
			new Type[]{ typeof( Savage ), typeof( SavageRider ), typeof( SavageShaman ) },
			new Type[]{ typeof( Ettin ), typeof( Ettin ), typeof( Ettin ) }, // Ettin 
			new Type[]{ typeof( Ogre ), typeof( Ogre ), typeof( OgreLord ) }, // Ogre
			new Type[]{ typeof( Troll ), typeof( FrostTroll ), typeof( FrostTroll ) }, // FrostTroll
			new Type[]{ typeof( JukaMage ), typeof( JukaWarrior ), typeof( JukaLord ) }, // Juka
			new Type[]{ typeof( DreadSpider ), typeof( GiantBlackWidow ), typeof( GiantSpider ) }, // Spider
				//Level 4
			new Type[]{ typeof( WraithRiderMage ), typeof(WraithRiderWarrior), typeof( BoneMagiLord  ), typeof( SkeletalKnight ), typeof( BoneKnightLord ) },
			new Type[]{ typeof( PirateCaptain2 ), typeof( Pirate ), typeof(PirateDeckHand), typeof( PirateWench ) },
			new Type[]{ typeof( OphidianWarrior ), typeof( OphidianKnight ), typeof( OphidianArchmage ) }, // Ophidian
			new Type[]{ typeof( TerathanDrone ), typeof( TerathanWarrior ), typeof( TerathanAvenger ) }, // Terathan
			new Type[]{ typeof( AncientReaper ), typeof( Reaper ) }, // Reaper
			new Type[]{ typeof( EvilPixie ), typeof( EtherealWarrior ) }, // Fey	
			new Type[]{ typeof( MinotaurCaptain ), typeof( MinotaurScout ), typeof( Minotaur ) }, // Minotaur			
				//Level 5
			new Type[]{ typeof( Dragon ), typeof( Wyvern ), typeof( Drake ) },
			new Type[]{ typeof( SkeletalDragon ), typeof( WraithRiderWarrior ), typeof( WraithRiderMage ) }, // Skeletal
			new Type[]{ typeof( Titan ), typeof( Cyclops ) }, // Titan
			new Type[]{ typeof( RottingCorpse ) }, // Dead
			new Type[]{ typeof( WhiteWyrm ), typeof( IceFiend ) },// Ice
					
				//Level 6
			new Type[]{ typeof( Balron ), typeof( Daemon ), typeof( Succubus ) },
			new Type[]{ typeof( AncientLich ), typeof( LichLord ) }, //Lich
			new Type[]{ typeof( ShadowWyrm ) }, //Shadow
		
			
		};

	
		//Themed warning strings add new ones here
		private static string[] ThemeMessages = new string[]
		{ 
			"", //blank chest no theme set to match ChestThemeType.None
				//Level 2
			"*You hear the clatter and clack of bones closing in around you!*", // Skeleton,
			"The reptilian scourge you have found!", // Lizardmen,
			"The vermin horde descends upon you!", // Ratmen,
			"*A great stink blows the wilderness*", // Orc,
			"*As you attempt to open the chest you become entangled!", // Corpser,
				//Level 3
			"You have dug into a solen hole!",
			"You have sprung a trap and brigands come to protect their treasure!",
			"You have disturbed the sacred burial grounds of the savages!",
			"*You feel the ground shake as you spot the first one of them*", // Ettin,
			"*A stench of rotting meat fills the air*", // Ogre,
			"*You hear heavy footsteps approaching, but are afraid to look*", // FrostTroll
			"*The beating of drums getting closer makes you wonder if this was worth it*", // Juka
			"*You have disturbed a spiders nest!*", // Spider
				//Level 4
			"You have disturbed the resting place of lost souls!",
			"Arrr! The pirate spirits come to life to protect their booty!",
			"The reptilian knights have been sent to protect their treasure!", // Ophidian,
			"*A chill runs up your spine as you feel 1000 beady little eyes upon you!*", // Terathan,
			"*The forest steps in to protect its heritage*", // Reaper,
			"*Plundering treasure is not good for your karma at all*", // Fey,
			"*You hear heavy footsteps approaching, but are afraid to look*", // Minotaur,
				//Level 5
			"The ground shakes beneath you as you have just disturbed the slumber of the dragon kin!",
			"The bones beneath your feet come to life!", //Skeleton
			"You hear heavy footsteps approaching, but are afraid to look", //Titan
			"The dead rise from their graves to protect their treasure!", //Dead
			"The air around you freezes you to the bone!", //Dead
			
				//Level 6
			"You fear that your attempt to plunder this treasure was a bad move indeed.", //Demon
			"Living to tell this tale would have been nice, oh well.", //Lich
			"You cannot quite see what is heading your way, but can definately hear it.", //Shadow	
			
			
		};
	}
}