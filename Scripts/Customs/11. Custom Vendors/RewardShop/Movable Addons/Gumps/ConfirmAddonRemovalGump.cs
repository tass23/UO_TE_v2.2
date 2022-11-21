using System;
using Server;
using Server.Network;
using Server.Items;
using Solaris.Addons;


namespace Server.Gumps
{
	public class ConfirmAddonRemovalGump : AddonConfirmationGump
	{
		protected override string _Message{ get{ return "Confirm Removal"; } }
		
		public ConfirmAddonRemovalGump( MovableAddonKey key, Mobile owner ) : base( key, owner )
		{
		}
		
		protected override void Respond( bool confirm )
		{
			if( !_Key.CanCreateAddon( _Owner ) )
			{
				_Owner.SendMessage( "You can no longer do that" );
			}
			
			_ForceResponseTimer.Stop();
			
			if( confirm )
			{
				_Key.RemoveAddon();
			}
		}
	}
}