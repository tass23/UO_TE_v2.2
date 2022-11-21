using System;
using Server;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Solaris.Addons;
using Solaris.CliLocHandler;

namespace Server.Gumps
{
	public abstract class MovableAddonControlGump : Gump
	{
		protected virtual int _Width{ get{ return 350; } }
		protected virtual int _Height{ get{ return 450; } }
		
		protected MovableAddon _Addon;
		protected Mobile _Owner;
		
		protected int _Y = 20;
		protected int _X = 20;
		
		public MovableAddonControlGump( MovableAddon addon, Mobile owner ) : base( 100, 100 )
		{
			
			_Owner = owner;
			_Addon = addon;
			
			if( _Owner == null || _Addon == null )
			{
				return;
			}

			_Owner.CloseGump( typeof( MovableAddonControlGump ) );
						
			AddPage(0);
			AddImage( 0, 0, 0x24F4 );
			
            AddDirectionControls( _X + 30, _Y + 5 );
            
            AddSpeedControls( _X + 150, _Y + 20 );
            
            if( _Addon.PowerResourceType != null )
            {
	            AddPowerRefillControls( _X + 100, _Y += 105 );
            }
		}
		
		protected virtual void AddDirectionControls( int x, int y )
		{
			AddButton( x + 30, y, 0x1194, 0x1194, 0x11, GumpButtonType.Reply, 0 ); 			//forward
			AddButton( x + 30, y + 60, 0x1198, 0x1198, 0x12, GumpButtonType.Reply, 0 ); 	//back
			AddButton( x, y + 30, 0x119A, 0x119A, 0x14, GumpButtonType.Reply, 0 ); 			//left
			AddButton( x + 60, y + 30, 0x1196, 0x1196, 0x18, GumpButtonType.Reply, 0 ); 	//right
			
			AddButton( x, y, 0x119B, 0x119B, 0x15, GumpButtonType.Reply, 0 ); 				//forward left
			AddButton( x + 60, y, 0x1195, 0x1195, 0x19, GumpButtonType.Reply, 0 ); 			//forward right
			AddButton( x, y + 60, 0x1199, 0x1199, 0x16, GumpButtonType.Reply, 0 ); 			//back left
			AddButton( x + 60, y + 60, 0x1197, 0x1197, 0x1A, GumpButtonType.Reply, 0 ); 	//back right
			
			AddButton( x + 40, y + 40, 0x2A62, 0x2A62, 0x10, GumpButtonType.Reply, 0 );		//stop
			
			AddButton( x - 20, y, 0x5786, 0x5786, 0x20, GumpButtonType.Reply, 0 );		//turn left
			AddButton( x + 110, y, 0x5781, 0x5781, 0x21, GumpButtonType.Reply, 0 );		//turn right
			
		}
		
		protected virtual void AddSpeedControls( int x, int y )
		{
			//one
			AddButton( x, y, ( _Addon.Speed == MovableAddonSpeed.One ? 0x767 : 0x768 ), 0x767, 0x100, GumpButtonType.Reply, 0 );			
			AddLabel( x + 20, y, 1152, "One" );
			
			//slow
			AddButton( x, y + 20, ( _Addon.Speed == MovableAddonSpeed.Slow ? 0x767 : 0x768 ), 0x767, 0x101, GumpButtonType.Reply, 0 );			
			AddLabel( x + 20, y + 20, 1152, "Slow" );
			
			//medium			
			AddButton( x, y + 40, ( _Addon.Speed == MovableAddonSpeed.Regular ? 0x767 : 0x768 ), 0x767, 0x102, GumpButtonType.Reply, 0 );		
			AddLabel( x + 20, y + 40, 1152, "Regular" );
			
			//fast		
			AddButton( x, y + 60, ( _Addon.Speed == MovableAddonSpeed.Fast ? 0x767 : 0x768 ), 0x767, 0x103, GumpButtonType.Reply, 0 );		
			AddLabel( x + 20, y + 60, 1152, "Fast" );
			
			//0x767, 0x768
		}
		
		protected virtual void AddPowerRefillControls( int x, int y )
		{
			AddLabel( x, y + 5, 1152, "Add Power" ); 
			AddButton( x + 70, y, 0x2A4E, 0x2A4E, 0x300, GumpButtonType.Reply, 0 );		
		}
			
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonid = info.ButtonID;
			
			//direction commands
			if( buttonid >= 0x10 && buttonid < 0x20 )
			{
				_Addon.Command = (MovableAddonCommand)( buttonid - 0x10 );
			}
			
			//turn left command
			if( buttonid == 0x20 )
			{
				_Addon.Rotate( false );
			}
			else if( buttonid == 0x21 )
			{
				_Addon.Rotate( true );
			}
			
			//"one" speed
			if( buttonid == 0x100 )
			{
				_Addon.Speed = MovableAddonSpeed.One;
			}
			//slow speed
			else if( buttonid == 0x101 )
			{
				_Addon.Speed = MovableAddonSpeed.Slow;
			}
			//regular speed
			else if( buttonid == 0x102 )
			{
				_Addon.Speed = MovableAddonSpeed.Regular;
			}
			//fast speed
			else if( buttonid == 0x103 )
			{
				_Addon.Speed = MovableAddonSpeed.Fast;
			}
			
			//add resource
			if( buttonid == 0x300 && _Addon.PowerResourceType != null )
			{
				_Owner.SendMessage( "Please select the resource to power this." );
				_Owner.Target = new AddResourceTarget( _Addon );
			}
		}
		
		class AddResourceTarget : Target
		{
			MovableAddon _Addon;
			
			public AddResourceTarget( MovableAddon addon ) : base ( -1, false, TargetFlags.None ) 
			{
				_Addon = addon;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if( targeted.GetType() != _Addon.PowerResourceType )
				{
					from.SendMessage( "That cannot be used for power.  You need " + CliLoc.GetName( _Addon.PowerResourceType ) + "." );
					return;
				}
				
				if( !((Item)targeted).IsChildOf( from.Backpack ) )
				{
					from.SendMessage( "That must be in your backpack" );
					return;
				}
				
				_Addon.Refuel( (Item)targeted );
			}			
		}
	}
}