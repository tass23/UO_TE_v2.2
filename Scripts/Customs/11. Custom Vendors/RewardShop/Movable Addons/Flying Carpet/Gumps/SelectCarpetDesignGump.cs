using System;
using Server;
using Server.Network;
using Server.Items;
using Solaris.Addons;


namespace Server.Gumps
{
	public class SelectCarpetDesignGump : MovableAddonConfigGump
	{
		protected override int _Width{ get{ return 150; } }
		protected override int _Height{ get{ return 40 + Enum.GetNames( typeof( FlyingCarpetType ) ).Length * 50; } }
		
		protected int _Cost;
		
		public SelectCarpetDesignGump( MovableAddonKey key, Mobile owner, int cost ) : base( key, owner )
		{
			
			_Cost = cost;
			
			if( _Owner == null || _Key == null )
			{
				return;
			}

			_Owner.CloseGump( typeof( SelectCarpetDesignGump ) );			
			
			foreach( int carpettype in Enum.GetValues( typeof( FlyingCarpetType ) ) )
			{
				AddItem( _X, _Y, FlyingCarpetComponent.CarpetIDs[ carpettype ][0][0], _Key.AddonHue );
				AddButton( _X + 50, _Y + 25, 0x15E1, 0x15E5, carpettype + 1, GumpButtonType.Reply, 0 ); 
				
				_Y += 50;
			}
						
		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonid = info.ButtonID - 1;
			
			if( buttonid  >= 0 && buttonid <= Enum.GetNames( typeof( FlyingCarpetType ) ).Length ) 
			{
				if( Purchase( _Key.ModifyResourceType, _Cost ) )
				{
					((FlyingCarpetMagicLamp)_Key).CarpetType = (FlyingCarpetType)buttonid;
					_Owner.SendMessage( "You have changed the carpet design" );
				}
				
			}
			
			_Owner.SendGump( new FlyingCarpetConfigGump( _Key, _Owner ) );
		}
		
		

		
	}
}