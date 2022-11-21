using System;
using Server;
using Server.Network;
using Server.Items;
using Solaris.Addons;
using Solaris.CliLocHandler;

namespace Server.Gumps
{
	public class FlyingCarpetConfigGump : MovableAddonConfigGump
	{
		
		FlyingCarpetMagicLamp _Lamp;
		
		public int DesignCost
		{
			get
			{
				return _Lamp.CarpetWidth * _Lamp.CarpetLength;
			}
		}
		
		public int LengthUpgradeCost
		{
			get
			{
				return _Lamp.CarpetWidth * ( _Lamp.CarpetLength + 2 ) * 2;
			}
		}
		
		public int WidthUpgradeCost
		{
			get
			{
				return ( _Lamp.CarpetWidth  + 2 ) * _Lamp.CarpetLength * 2;
			}
		}
		
		public int LengthDowngradeCost
		{
			get
			{
				return _Lamp.CarpetWidth * ( _Lamp.CarpetLength - 2 ) * 2;
			}
		}
		
		public int WidthDowngradeCost
		{
			get
			{
				return ( _Lamp.CarpetWidth  - 2 ) * _Lamp.CarpetLength * 2;
			}
		}
		
		
		
		
		public FlyingCarpetConfigGump( MovableAddonKey key, Mobile owner ) : base( key, owner )
		{
			
			if( _Owner == null || _Key == null )
			{
				return;
			}
			
			_Lamp = (FlyingCarpetMagicLamp)key;
			
			AddLabel( _X, _Y, 88, "Flying Carpet Configuration" );
			
			AddLabel( _X, _Y += 50, 1152, "Current Carpet Layout" );
			
			AddLabel( _X, _Y += 50, 1152, "Length: " + _Lamp.CarpetLength.ToString() );
			AddLabel( _X + 150, _Y, 1152, "Width: " + _Lamp.CarpetWidth.ToString() );
			AddLabel( _X, _Y += 40, 1152, "Design: " );
			
			AddItem( _X + 100, _Y, FlyingCarpetComponent.CarpetIDs[ (int)_Lamp.CarpetType ][0][0], _Key.AddonHue );
			
			AddLabel( _X, _Y += 50, 1152, "Available Modifications/Upgrades:" );
			
			AddLabel( _X + 10, _Y += 20, 1152, String.Format( "(cost requirement: {0})", CliLoc.GetName( _Lamp.ModifyResourceType ) ) );
			
			
			AddLabel( _X, _Y += 50, 1152, "Modification" );
			AddLabel( _X + 200, _Y, 1152, "Cost" );
			
			AddLabel( _X, _Y += 30, 1152, "Change carpet design" );
			AddLabel( _X + 200, _Y, 1152, DesignCost.ToString() );

			if( _Lamp.Addon == null )
			{
				AddButton( _X + 250, _Y, 0x15E1, 0x15E5, 1, GumpButtonType.Reply, 0 ); 
			}
			
			if( _Lamp.CarpetLength < FlyingCarpet.MAX_LENGTH )
			{
				
				AddLabel( _X, _Y += 20, 1152, "Increase length" );
				AddLabel( _X + 200, _Y, 1152, LengthUpgradeCost.ToString() );
					
				if( _Lamp.Addon == null )
				{
					AddButton( _X + 250, _Y, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0 ); 
				}
			}
			
			if( _Lamp.CarpetWidth < FlyingCarpet.MAX_WIDTH )
			{
				
				AddLabel( _X, _Y += 20, 1152, "Increase width" );
				AddLabel( _X + 200, _Y, 1152, WidthUpgradeCost.ToString() );
				
				if( _Lamp.Addon == null )
				{
					AddButton( _X + 250, _Y, 0x15E1, 0x15E5, 3, GumpButtonType.Reply, 0 ); 
				}
			}
			
			if( _Lamp.CarpetLength > 3 )
			{
				
				AddLabel( _X, _Y += 20, 1152, "Decrease length" );
				AddLabel( _X + 200, _Y, 1152, LengthDowngradeCost.ToString() );
					
				if( _Lamp.Addon == null )
				{
					AddButton( _X + 250, _Y, 0x15E1, 0x15E5, 4, GumpButtonType.Reply, 0 ); 
				}
			}
			
			if( _Lamp.CarpetWidth > 3 )
			{
				
				AddLabel( _X, _Y += 20, 1152, "Decrease width" );
				AddLabel( _X + 200, _Y, 1152, WidthDowngradeCost.ToString() );
				
				if( _Lamp.Addon == null )
				{
					AddButton( _X + 250, _Y, 0x15E1, 0x15E5, 5, GumpButtonType.Reply, 0 ); 
				}
			}
			
			if( _Lamp.Addon != null )
			{
				AddLabel( _X, _Y += 30, 88, "To make upgrades, store your carpet" );
			}
			
			
			
			
			
		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonid = info.ButtonID;
			
			if( buttonid == 0 || !_Lamp.IsChildOf( _Owner.Backpack ) )
			{
				return;
			}
				
			if( _Lamp.Addon != null )
			{
				_Owner.SendMessage( "You cannot make changes to your carpet until you have stored it." );
				return;
			}
			
			//Change carpet design
			if( buttonid == 1 )
			{
				_Owner.SendGump( new SelectCarpetDesignGump( _Key, _Owner, DesignCost ) );
				return;
			}
			
			//increase length
			if( buttonid == 2 )
			{
				if( Purchase( _Lamp.ModifyResourceType, LengthUpgradeCost ) )
				{
					_Lamp.CarpetLength += 2;
					_Owner.SendMessage( "You have increased your carpet's length." );
				}
			}
			
			//increase width
			if( buttonid == 3 )
			{
				if( Purchase( _Lamp.ModifyResourceType, WidthUpgradeCost ) )
				{
					_Lamp.CarpetWidth += 2;
					_Owner.SendMessage( "You have increased your carpet's width." );
				}
			}
			
			//decrease length
			if( buttonid == 4 )
			{
				if( Purchase( _Lamp.ModifyResourceType, LengthDowngradeCost ) )
				{
					_Lamp.CarpetLength -= 2;
					_Owner.SendMessage( "You have decreased your carpet's length." );
				}
			}
			
			//decrease width
			if( buttonid == 5 )
			{
				if( Purchase( _Lamp.ModifyResourceType, WidthDowngradeCost ) )
				{
					_Lamp.CarpetWidth -= 2;
					_Owner.SendMessage( "You have decreased your carpet's width." );
				}
			}			
			
			_Owner.SendGump( new FlyingCarpetConfigGump( _Key, _Owner ) );
			
		}
	}
	
	
}