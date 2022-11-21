using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Multis;
using Server.Network;

namespace Server.Commands
{
	public class Addon2Static
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Addon2Static", AccessLevel.Administrator, new CommandEventHandler( Addon2Static_OnCommand ) );
		}

		[Usage( "Addon2Static" )]
		[Description( "Statify an Addon structure" )]
		private static void Addon2Static_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage("Please select the Addon structure you want to statify");
			e.Mobile.Target = new AddonSelector();
		}

		///// //// /// // / BEGIN TARGET / // /// //// /////
		private class AddonSelector : Target
		{
			public AddonSelector () : base( -1, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is AddonComponent )
				{
					BaseAddon design = ((AddonComponent)targeted).Addon;
					if ( design.Components.Count > 0 )
					{
						for ( int i = 0; i < design.Components.Count; ++i )
						{
							AddonComponent component = (AddonComponent)((design.Components)[i]);
							Static equivalent = new Static( component.HuedItemID  );  //( component.ItemID );
							equivalent.Location = component.Location;  //component.Location;
							equivalent.Map = component.Map; //component.Map;
							equivalent.Hue = component.Hue; //component.Map;							
						}
					}
					design.Delete();
					from.SendMessage("Addon structure statified. You can now freeze it.");
				}
			}
		}
		///// //// /// // / END TARGET / // /// //// /////
	}
}