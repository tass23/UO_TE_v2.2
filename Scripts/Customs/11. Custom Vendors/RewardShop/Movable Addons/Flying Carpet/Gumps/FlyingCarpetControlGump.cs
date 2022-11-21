using System;
using Server;
using Server.Network;
using Server.Items;
using Solaris.Addons;
using Solaris.CliLocHandler;

namespace Server.Gumps
{
	public class FlyingCarpetControlGump : MovableAddonControlGump
	{
		public FlyingCarpetControlGump( MovableAddon addon, Mobile owner ) : base( addon, owner )
		{
		}
		
		protected override void AddDirectionControls( int x, int y )
		{
			base.AddDirectionControls( x, y );
			
			AddButton( x - 25, y + 30, 0x26AD, 0x26AD, 0x32, GumpButtonType.Reply, 0 );		//take off
			AddButton( x - 25, y + 50, 0x26AC, 0x26AC, 0x30, GumpButtonType.Reply, 0 );		//up
			AddButton( x - 25, y + 70, 0x26B2, 0x26B2, 0x31, GumpButtonType.Reply, 0 );		//down
			AddButton( x - 25, y + 90, 0x26B3, 0x26B3, 0x33, GumpButtonType.Reply, 0 );		//land
			
		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonid = info.ButtonID;

			if( buttonid == 0 || !_Addon.Contains( _Owner ) )
			{
				return;
			}
			
			base.OnResponse( sender, info );
			
			//up
			if( buttonid == 0x30 )
			{
				_Addon.Command = MovableAddonCommand.Up;
			}
			//down
			else if( buttonid == 0x31 )
			{
				_Addon.Command = MovableAddonCommand.Down;
			}
			//take off
			else if( buttonid == 0x32 )
			{
				((FlyingCarpet)_Addon).TakeOff();
			}
			//land
			else if( buttonid == 0x33 )
			{
				((FlyingCarpet)_Addon).Land();
			}
						
			_Owner.SendGump( new FlyingCarpetControlGump( _Addon, _Owner ) );
		}
	}
}