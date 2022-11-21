using System;
using Server;
using Server.Network;
using Server.Items;
using Solaris.Addons;


namespace Server.Gumps
{
	public class ConfirmAddonPlaceGump : AddonConfirmationGump
	{
		protected override string _Message{ get{ return "Confirm Placement"; } }
		
		public ConfirmAddonPlaceGump( MovableAddonKey key, Mobile owner ) : base( key, owner )
		{
		}
		
		protected override void Respond( bool confirm )
		{
			if( !_Key.CanCreateAddon( _Owner ) )
			{
				_Owner.SendMessage( "You can no longer do that" );
				_Key.ConfirmCreation( _Owner, false );
			}
			
			_ForceResponseTimer.Stop();
			
			_Key.ConfirmCreation( _Owner, confirm );
		}
	}
}