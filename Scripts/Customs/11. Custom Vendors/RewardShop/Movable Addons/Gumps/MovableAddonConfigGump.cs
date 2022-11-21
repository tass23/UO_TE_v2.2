using System;
using Server;
using Server.Network;
using Server.Items;
using Solaris.Addons;
using Solaris.CliLocHandler;

namespace Server.Gumps
{
	public abstract class MovableAddonConfigGump : Gump
	{
		protected virtual int _Width{ get{ return 350; } }
		protected virtual int _Height{ get{ return 450; } }
		
		protected MovableAddonKey _Key;
		protected Mobile _Owner;
		
		protected int _Y = 20;
		protected int _X = 20;
		
		public MovableAddonConfigGump( MovableAddonKey key, Mobile owner ) : base( 100, 100 )
		{
			
			_Owner = owner;
			_Key = key;
			
			if( _Owner == null || _Key == null )
			{
				return;
			}

			_Owner.CloseGump( typeof( MovableAddonConfigGump ) );			
						
			AddPage(0);
			AddBackground(0, 0, _Width, _Height, 5054);
            AddImageTiled(11, 10, _Width - 23, _Height - 20, 2624);
            AddAlphaRegion(11, 10, _Width - 22, _Height - 20);
		}
		
		protected bool Purchase( Type type, int amount )
		{
			Item resource = _Owner.Backpack.FindItemByType( type, true );
			
			if( resource == null || resource.Amount < amount )
			{
				_Owner.SendMessage( "You need more " + CliLoc.GetName( type ) + "." );
				return false;
			}
			
			resource.Consume( amount );
			return true;
		}
	}
}