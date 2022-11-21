using System;
using Server;
using Server.Mobiles; 
using Server.Gumps; 
using Server.Targeting;
using Server.Items;

namespace Server.Commands
{
	public class ShrinkLockDown
	{
		public static void Initialize()
		{
			CommandSystem.Register( "ShrinkLockDown", AccessLevel.Administrator, new CommandEventHandler( ShrinkLockDown_OnCommand ) );
			CommandSystem.Register( "ShrinkRelease", AccessLevel.Administrator, new CommandEventHandler( ShrinkRelease_OnCommand ) );
		}

		[Usage( "ShrinkLockDown" )]
		[Description( "Disables all shrinkitems in the world." )]
		private static void ShrinkLockDown_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				int shrinkCount = 0;

				foreach ( Item item in World.Items.Values )
				{
					if ( item is ShrinkItemX )
					{
						shrinkCount += 1;
					}
				}

				e.Mobile.SendMessage( "{0} ShrinkItems have been disabled.", shrinkCount );
         			World.Broadcast( 0x35, true, "A server wide shrinkitem lockout has initiated" );
         			World.Broadcast( 0x35, true, "All shrunken pets have been disabled untill further notice." );

				foreach ( Item item in World.Items.Values )
				{
					if ( item is ShrinkItemX )
					{
						ShrinkItemX si = (ShrinkItemX)item;
						si.Disabled = true;
					}
				}
			}
		}

		[Usage( "ShrinkRelease" )]
		[Description( "Re-enables all disabled shrink items in the world." )]
		private static void ShrinkRelease_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				int shrinkCount = 0;

				foreach ( Item item in World.Items.Values )
				{
					if ( item is ShrinkItemX )
					{
						shrinkCount += 1;
					}
				}

				e.Mobile.SendMessage( "{0} ShrinkItems have been enabled.", shrinkCount );
         			World.Broadcast( 0x35, true, "The server wide shrinkitem lockout has been lifted." );
         			World.Broadcast( 0x35, true, "You may once again unshrink all shrunken pets." );

				foreach ( Item item in World.Items.Values )
				{
					if ( item is ShrinkItemX )
					{
						ShrinkItemX si = (ShrinkItemX)item;
						si.Disabled = false;
					}
				}
			}
		}
	}
}