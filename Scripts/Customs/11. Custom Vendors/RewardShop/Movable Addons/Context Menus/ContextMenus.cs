using System;
using Server;
using Server.ContextMenus;
using Server.Items;

namespace Solaris.Addons
{
	
	public class MovableAddonConfigEntry : ContextMenuEntry
	{
		Mobile _From;
		MovableAddonKey _Key;

		//3002132 = "configuration"
		public MovableAddonConfigEntry( Mobile from, MovableAddonKey key, int index ) : base( 2132, index )
		{
			_From = from;
			_Key = key;
		}

		public override void OnClick()
		{
			_Key.SendConfigGump( _From );
		}
	}
}