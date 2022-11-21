using System;
using Server;
using Server.Network;
using Server.Items;
using Solaris.Addons;


namespace Server.Gumps
{
	public class AddonConfirmationGump : Gump
	{
		protected virtual string _Message{ get{ return "Confirm?"; } }
			
		protected virtual int _Width{ get{ return 170; } }
		protected virtual int _Height{ get{ return 100; } }
		
		protected MovableAddonKey _Key;
		protected Mobile _Owner;
		
		protected Timer _ForceResponseTimer;
		
		protected int _Y = 20;
		protected int _X = 20;
		
		public AddonConfirmationGump( MovableAddonKey key, Mobile owner ) : base( 100, 100 )
		{
			_Owner = owner;
			_Key = key;
			
			if( _Owner == null || _Key == null )
			{
				return;
			}

			_Owner.CloseGump( typeof( AddonConfirmationGump ) );			
						
			AddPage(0);
			AddBackground(0, 0, _Width, _Height, 5054);
            AddImageTiled(11, 10, _Width - 23, _Height - 20, 2624);
            AddAlphaRegion(11, 10, _Width - 22, _Height - 20);

			AddLabel( _X + 20, _Y, 88, _Message );
            
			AddButton( _X, _Y += 30, 0xF9, 0xF8, 1, GumpButtonType.Reply, 0 );
			AddButton( _X + 70, _Y, 0xF3, 0xF1, 0, GumpButtonType.Reply, 0 );
			
			//have timer force response if the user doesn't respond within the required time
			_ForceResponseTimer = Timer.DelayCall( TimeSpan.FromSeconds( 20 ), TimeSpan.FromSeconds( 20.0 ), new TimerCallback( OnTimer ) );
			
		}
		
		protected virtual void Respond( bool confirm )
		{
		}
		
		protected void OnTimer()
		{
			_Owner.SendMessage( "Confirmation took too long.  Please try again." );
			_Owner.CloseGump( typeof( AddonConfirmationGump ) );

			Respond( false );
		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Respond( info.ButtonID == 1 );
		}
		
		
	}
	
	
}