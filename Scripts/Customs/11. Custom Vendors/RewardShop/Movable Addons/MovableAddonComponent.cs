using System;
using Server;
using Server.Items;

namespace Solaris.Addons
{
	public class MovableAddonComponent : Item
	{
		protected MovableAddon _Addon;
		
		protected int _OffsetX;
		protected int _OffsetY;
		protected int _OffsetZ;
		
		//define direction: default north
		protected MovableAddonDirection _Direction;
		
		public MovableAddon Addon{ get{ return _Addon; } }
		
		public MovableAddonDirection Direction
		{
			get{ return _Direction; }
			set
			{
				_Direction = value;
				ItemID = _DirectionID[ (int)_Direction ];
				
				//resync with addon
				UpdatePosition();
			}
		}
				
		
		//this stores the ItemID's for the various directions this can face
		protected int[] _DirectionID;
		
					
		
		public MovableAddonComponent( int itemid ) : this( new int[]{ itemid, itemid, itemid, itemid } )
		{
		}
		
		public MovableAddonComponent( int[] itemids ) : this( itemids, 0 )
		{
		}
		
		public MovableAddonComponent( int[] itemids, int direction ) : base( itemids[direction] )
		{
			_DirectionID = itemids;
			_Direction = (MovableAddonDirection)direction;
			
			Movable = false;
		}
		
		public MovableAddonComponent( Serial serial ) : base( serial )
		{
		}
		
		
		public void RegisterToMovableAddon( MovableAddon addon, int x, int y, int z )
		{
			_Addon = addon;
			
			_OffsetX = x;
			_OffsetY = y;
			_OffsetZ = z;
			
			UpdatePosition();
		}

		public void UpdatePosition()
		{
			if( _Addon != null )
			{
				MoveToWorld( new Point3D( _Addon.X + _OffsetX, _Addon.Y + _OffsetY, _Addon.Z + _OffsetZ ), _Addon.Map );
			}
			else
			{
				Delete();
			}
			
			
		}
		
		public void Rotate( bool clockwise )
		{
			int newdirection = (int)_Direction + (clockwise ? 1 : -1 );
			
			if( newdirection > 3 )
			{
				newdirection = 0;
			}
			if( newdirection < 0 )
			{
				newdirection = 3;
			}
			
			//determine x-y rotation based on previous direction
			int temp = _OffsetY;
			_OffsetY = _OffsetX * ( clockwise ? 1 : -1 );
			_OffsetX = temp * ( clockwise ? -1 : 1 );
			
			Direction = (MovableAddonDirection)newdirection;
		}
		
		public override void OnLocationChange( Point3D old )
		{
			if ( _Addon != null )
			{
				_Addon.Location = new Point3D( X - _OffsetX, Y - _OffsetY, Z - _OffsetZ );
			}
		}

		public override void OnMapChange()
		{
			if ( _Addon != null )
			{
				_Addon.Map = Map;
			}
		}
		
		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if ( _Addon != null )
			{
				_Addon.Delete();
			}
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			
			writer.Write( _Addon );
			writer.Write( _OffsetX );
			writer.Write( _OffsetY );
			writer.Write( _OffsetZ );
			
			writer.Write( (int)_Direction );
			
			for( int i = 0; i < 4; i++ )
			{		
				writer.Write( _DirectionID[ i ] );
			}
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			_DirectionID = new int[4];
			
			int version = reader.ReadInt();
			
			_Addon = (MovableAddon)reader.ReadItem();
			
			_OffsetX = reader.ReadInt();
			_OffsetY = reader.ReadInt();
			_OffsetZ = reader.ReadInt();
			
			_Direction = (MovableAddonDirection)reader.ReadInt();
			
			for( int i = 0; i < 4; i++ )
			{
				_DirectionID[i] = reader.ReadInt();
			}
			
		}
		
		
	}
		
	
}