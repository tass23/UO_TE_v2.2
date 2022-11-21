using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Items.Crops;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Misc
{
	public class DeleteUnusedCrops
	{
		public static void Initialize()
		{
			CommandSystem.Register( "DeleteUnusedCrops", AccessLevel.Administrator, new CommandEventHandler( DeleteUnusedCrops_OnCommand ) );
		}

		[Usage( "DeleteUnusedCrops" )]
		[Description( "Deletes unused or not needed items." )]
		public static void DeleteUnusedCrops_OnCommand( CommandEventArgs e )
		{
			int i_Count = 0;
			ArrayList toDelete = new ArrayList();

			try
			{
				foreach ( Item item in World.Items.Values )
				{
					if ( item is AsparagusCrop || item is AsparagusSeedling || item is BananaCrop || item is BananaSeedling || item is BeetCrop || item is BeetSeedling || item is BitterHopsCrop || item is BitterHopsSeedling || item is BlackberryCrop || item is BlackberrySeedling || item is BlackRaspberryCrop || item is BlackRaspberrySeedling ||
						item is BlueberryCrop || item is BlueberrySeedling || item is BroccoliCrop || item is BroccoliSeedling || item is CabbageCrop || item is CabbageSeedling || item is CantaloupeCrop || item is CantaloupeSeedling || item is CarrotCrop || item is CarrotSeedling || item is CauliflowerCrop || item is CauliflowerSeedling ||
						item is CeleryCrop || item is CelerySeedling || item is ChiliPepperCrop || item is ChiliPepperSeedling || item is CoconutCrop || item is CoconutSeedling || item is CornCrop || item is CornSeedling || item is CottonCrop || item is CottonSeedling || item is CranberryCrop || item is CranberrySeedling || item is CucumberCrop || item is CucumberSeedling ||
						item is DateCrop || item is DateSeedling || item is EggplantCrop || item is EggplantSeedling || item is ElvenHopsCrop || item is ElvenHopsSeedling || item is FieldCornCrop || item is FieldCornSeedling || item is FlaxCrop || item is FlaxSeedling || item is GarlicCrop || item is GarlicSeedling || item is GinsengCrop || item is GinsengSeedling ||
						item is GreenBeanCrop || item is GreenBeanSeedling || item is GreenPepperCrop || item is GreenPepperSeedling || item is GreenSquashCrop || item is GreenSquashSeedling || item is HayCrop || item is HaySeedling || item is HoneydewMelonCrop || item is HoneydewMelonSeedling || item is IrishRoseCrop || item is IrishRoseSeedling ||
						item is LettuceCrop || item is LettuceSeedling || item is MandrakeCrop || item is MandrakeSeedling || item is MiniAlmondCrop || item is MiniAlmondSeedling || item is MiniAppleCrop || item is MiniAppleSeedling || item is MiniApricotCrop || item is MiniApricotSeedling || item is MiniAvocadoCrop || item is MiniAvocadoSeedling ||
						item is MiniCherryCrop || item is MiniCherrySeedling || item is MiniCocoaCrop || item is MiniCocoaSeedling || item is MiniCoffeeCrop || item is MiniCoffeeSeedling || item is MiniGrapefruitCrop || item is MiniGrapefruitSeedling || item is MiniKiwiCrop || item is MiniKiwiSeedling || item is MiniMangoCrop || item is MiniMangoSeedling ||
						item is MiniOrangeCrop || item is MiniOrangeSeedling || item is MiniPeachCrop || item is MiniPeachSeedling || item is MiniPearCrop || item is MiniPearSeedling || item is MiniPistacioCrop || item is MiniPistacioSeedling || item is MiniPomegranateCrop || item is MiniPomegranateSeedling || item is NightshadeCrop || item is NightshadeSeedling ||
						item is OatsCrop || item is OatsSeedling || item is OnionCrop || item is OnionSeedling || item is OrangePepperCrop || item is OrangePepperSeedling || item is PansyCrop || item is PansySeedling || item is PeanutCrop || item is PeanutSeedling || item is PeasCrop || item is PeasSeedling || item is PineappleCrop || item is PineappleSeedling ||
						item is PinkCarnationCrop || item is PinkCarnationSeedling || item is PoppyCrop || item is PoppySeedling || item is PotatoCrop || item is PotatoSeedling || item is PumpkinCrop || item is PumpkinSeedling || item is RadishCrop || item is RadishSeedling || item is RedMushroomCrop || item is RedMushroomSeedling ||
						item is RedPepperCrop || item is RedPepperSeedling || item is RedRaspberryCrop || item is RedRaspberrySeedling || item is RedRoseCrop || item is RedRoseSeedling || item is RiceCrop || item is RiceSeedling || item is SmallBananaCrop || item is SmallBananaSeedling || item is SnapdragonCrop || item is SnapdragonSeedling ||
						item is SnowHopsCrop || item is SnowHopsSeedling || item is SnowPeasCrop || item is SnowPeasSeedling || item is SoyCrop || item is SoySeedling || item is SpinachCrop || item is SpinachSeedling || item is SpiritRoseCrop || item is SpiritRoseSeedling || item is SquashCrop || item is SquashSeedling || item is StrawberryCrop || item is StrawberrySeedling ||
						item is SugarcaneCrop || item is SugarcaneSeedling || item is SunFlowerCrop || item is SunFlowerSeedling || item is SweetHopsCrop || item is SweetHopsSeedling || item is SweetPotatoCrop || item is SweetPotatoSeedling || item is TanGingerCrop || item is TanGingerSeedling || item is TanMushroomCrop || item is TanMushroomSeedling ||
						item is TeaCrop || item is TeaSeedling || item is TomatoCrop || item is TomatoSeedling || item is TurnipCrop || item is TurnipSeedling || item is WatermelonCrop || item is WatermelonSeedling || item is WheatCrop || item is WheatSeedling || item is WhiteRoseCrop || item is WhiteRoseSeedling || item is YellowPepperCrop || item is YellowPepperSeedling ||
						item is YellowRoseCrop || item is YellowRoseSeedling
						)
					{
						if ( item.Map == Server.Map.Felucca || item.Map == Server.Map.Trammel || item.Map == Server.Map.Ilshenar || item.Map == Server.Map.Malas || item.Map == Server.Map.Tokuno )
						{
							string sSowerProp = Properties.GetValue( e.Mobile, item, "Sower" );
							if ( sSowerProp == "Sower = (-null-)" || sSowerProp == "Sower = null" || sSowerProp == "null" )
							{
								i_Count++;
								CommandLogging.WriteLine( e.Mobile, "{0} {1} delete {2} [{3}]: {4} ({5} '{6}'))", e.Mobile.AccessLevel, CommandLogging.Format( e.Mobile ), item.Location, item.Map, item.GetType().Name, item.Name, sSowerProp );
								e.Mobile.SendMessage("{0}", sSowerProp);
								toDelete.Add(item);
							}
						}
					}
				}

				for ( int i = 0; i < toDelete.Count; ++i )
				{
					if ( toDelete[i] is Item ) ((Item)toDelete[i]).Delete();
				}

				e.Mobile.SendMessage( i_Count + " Item's deleted." );
			}
			catch (Exception err)
			{
				e.Mobile.SendMessage( "Exception: " + err.Message );
			}
		}
	}
}