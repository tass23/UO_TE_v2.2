//Uncomment this this if you're using RunUO RC1
#define RunUORC1

using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Regions;


namespace Solaris.Addons
{
	//this defines the four directions that movable addons and movable addon components use to orient and re-itemid
	public enum MovableAddonDirection
	{
		North = 0,
		East = 1,
		South = 2,
		West = 3
	}
	
	//defines direction commands.  With bitmasking, can go more than one direction at once:  forward left, up back, back right up, etc.
	[Flags]
	public enum MovableAddonCommand
	{
		Stop = 0x0,
		Forward = 0x1,
		Back = 0x2,
		Left = 0x4,
		Right = 0x8,
		Up = 0x10,
		Down = 0x20
	}
	
	//defines speed and special commands.  The enumerator value also defines the increment size, which can be adjusted to make it go faster or slower
	public enum MovableAddonSpeed
	{
		One = 0,
		
		Slow = 1,
		Regular = 3,
		Fast = 5, 
		
		Turn = 0xFF
	}
	
	
	//the movable addon class is the base class for all addons that can be controlled or moved
	public abstract class MovableAddon : Item
	{
		//this type determines what item is used to power the addon.  Null means no power requirements
		public virtual Type PowerResourceType{ get{ return null; } }
		
		//this indicates how long the addon can be powered on one unit of fuel
		public virtual TimeSpan TimePerPowerResource{ get{ return TimeSpan.Zero; } }
		
		//this duration indicates how long a brand new addon will be powered
		public static TimeSpan InitialPowerTime{ get{ return TimeSpan.FromHours(1); } }
		
		
		//this holds a list of all tiles which the movable addon cannot move over
		protected int[] _NoMoveTiles;
		public virtual int[] NoMoveTiles
		{
			get{ return null; }
		}
		
		//this holds a list of which maps can and cannot support the movable addon.  These are defined in inherited classes
		protected bool[] _UsableMaps;
		public virtual bool[] UsableMaps
		{
			get
			{
				return null;
			}
		}
		
		//this holds a list of all zones in all maps where the addon cannot be built.  These are defined in inherited classes
		protected Rectangle2D[][] _NoBuildZones;
		public virtual Rectangle2D[][] NoBuildZones
		{
			get
			{
				return null;
			}
		}
		
		//direction is defined to allow boat-like driving capability.  initial direction is North
		protected MovableAddonDirection _Direction;
		
		//the delay time used when driving
		protected TimeSpan _Delay;
		
		//the timer class runs the driving update eventsf
		protected Timer _DriveTimer;
		
		//the current command and speed from the main user
		protected MovableAddonCommand _Command;
		protected MovableAddonSpeed _Speed;
		
		public MovableAddonCommand Command
		{
			get{ return _Command; }
			set
			{
				_Command = value;
				
				if( _Command == MovableAddonCommand.Stop )
				{
					StopDriveTimer();
				}
				else
				{
					StartDriveTimer();
				}
			}
		}
		
		public MovableAddonSpeed Speed
		{
			get{ return _Speed; }
			set
			{
				_Speed = value;
				if( _Command != MovableAddonCommand.Stop )
				{
					StopDriveTimer();
					StartDriveTimer();
				}
			}
		}
		
		//this records when the addon was deployed.  Used to calculate how long until it runs out of power
		protected DateTime _LastTimeCheck;
		//when the addon is stored, this time determines how long until the addon runs out of power
		protected TimeSpan _ExhaustPowerTimeSpan;
		
		public TimeSpan ExhaustPowerTimeSpan
		{
			get
			{
				_ExhaustPowerTimeSpan -= ( DateTime.Now - _LastTimeCheck );
				_LastTimeCheck = DateTime.Now;
				
				if( _ExhaustPowerTimeSpan < TimeSpan.Zero )
				{
					_ExhaustPowerTimeSpan = TimeSpan.Zero;
				}
				
				return _ExhaustPowerTimeSpan;
			}
			set
			{
				
				_ExhaustPowerTimeSpan = value;
				
				_LastTimeCheck = DateTime.Now;
			}
			
		}
		
		//this checks if the addon is out of power.  In inherited classes, more actions can be taken to report to user
		public virtual bool OutOfPower
		{
			get
			{
				//if there is no power source defined, then it never runs out of power
				if( PowerResourceType == null )
				{
					return false;
				}
				
				//if there is still time left before power is exhausted
				if( ExhaustPowerTimeSpan > TimeSpan.Zero )
				{
					return false;
				}
				
				//otherwise, out of power.  Stop.
				Command = MovableAddonCommand.Stop;
				return true;
			}
		}
		
		
		
		//the list of all movable addon components within this addon
		protected List<MovableAddonComponent> _Components;

		//This indicates the max and min x,y,z values.  Used for collision detection
		protected Rectangle3D _Extrema;
		
		//a specific movable addon component which is used to receive controls from the user, and drive the movable addon
		protected MovableAddonControlComponent _Controller;
		public MovableAddonControlComponent Controller{ get{ return _Controller; } }
		
		//an item which provides further user control, such as gump use.  This identifies which user has control over the movable addon
		protected MovableAddonKey _Key;
		public MovableAddonKey Key
		{
			get{ return _Key; }
			set
			{
				_Key = value;
			}
		}
		
		//main constructor
		public MovableAddon() : base( 1 )	//use itemid 1
		{
			Movable = false;
			Visible = false;
			
			//default delay: 1 second
			_Delay = TimeSpan.FromSeconds( 1 );
			
			_Components = new List<MovableAddonComponent>();
			
			_Speed = MovableAddonSpeed.Regular;
		}
		
		//serial constructor
		public MovableAddon( Serial serial ) : base( serial )
		{
		}
		
		//this method adds a movable addon component to this movable addon, and does all the registration/placement
		protected void AddComponent( MovableAddonComponent component, int x, int y, int z )
		{
			if( _Components == null )
			{
				_Components = new List<MovableAddonComponent>();
			}
			
			_Components.Add( component );
			component.RegisterToMovableAddon( this, x, y, z );
			
			//update the extrema limit info
			_Extrema.Start = new Point3D( Math.Min( _Extrema.Start.X, x ), Math.Min( _Extrema.Start.Y, y ), Math.Min( _Extrema.Start.Z, z ) );
			
			
			_Extrema.End = new Point3D( Math.Max( _Extrema.End.X, x + 1 ), Math.Max( _Extrema.End.Y, y + 1 ), Math.Max( _Extrema.End.Z, z + 20 ) );
			
			
		}
		
		//this checks if the addon can be created at the specified position and map
		public bool CanCreateAt( Point3D point, Map map )
		{
			if( !UsableMaps[ map.MapIndex ] )
			{
				return false;
			}
			
			Region region = Region.Find( point, map );

			if ( region.IsPartOf( typeof( DungeonRegion ) ) )
			{
				return false;
			}

			foreach( Rectangle2D rect2d in NoBuildZones[ map.MapIndex ] )
			{
				if( rect2d.Contains( point ) )
				{
					return false;
				}
			}
			
			return true;
		}	
		
		//this checks the ground, statics, and items, and finds the higest point that would lie under the addon.  Useful for placing
		public virtual int GetHighestZ( Point3D p, Map map )
		{
			int highz = p.Z;
			
			for( int i = p.X + _Extrema.Start.X; i < p.X + _Extrema.End.X; i++ )
			{
				for( int j = p.Y + _Extrema.Start.Y; j < p.Y + _Extrema.End.Y; j++ )
				{
					highz = Math.Max( highz, map.GetAverageZ( i, j ) );

				}
				
			}
			
			//lift it up by one so that it can sit on top of the map
			return highz + 1;
			
		}	

		
		//this checks if a particular user can use the addon.  It is defined in inherited classes
		public virtual bool CanUse( Mobile from )
		{
			return false;
		}
		
		//this checks if the specified mobile has the key to this addon
		public bool HasKey( Mobile from )
		{
			if( _Key == null || from == null || from.Backpack == null )
			{
				return false;
			}
			
			return _Key.IsChildOf( from.Backpack );
		}
		
		//this method checks if an item or mobile is within the addon
		public virtual bool Contains( IPoint3D ip )
		{
			return _Extrema.Contains( new Point3D( ip.X - X, ip.Y - Y, ip.Z - Z ) );
		}
		
		
		//this handles the check for if the movable addon can be relocated to the specified point
		public bool CanMoveTo( Point3D point )
		{
			return CanMoveTo( point, Map );
		}
		
		public virtual bool CanMoveTo( Point3D p, Map map )
		{
			//don't allow the carpet to travel to places where you can't recall to
			if( !Server.Spells.SpellHelper.CheckTravel( map, p, Server.Spells.TravelCheckType.RecallTo ) )
			{
				return false;
			}
			
			int zcheck = p.Z + _Extrema.Start.Z;
			
			for( int i = p.X + _Extrema.Start.X; i < p.X + _Extrema.End.X; i++ )
			{
				for( int j = p.Y + _Extrema.Start.Y; j < p.Y + _Extrema.End.Y; j++ )
				{
					//first check the map and see if there is a problem there
					if( map.GetAverageZ( i, j ) >= zcheck )
					{
						return false;
					}
					
					//check if the map tile is considered a "no fly tile"
					int tileid = map.Tiles.GetLandTile( i, j ).ID;
					
					foreach( int nomovetile in NoMoveTiles )
					{
						if( nomovetile == tileid )
						{
							return false;
						}
					}
					
					//next check for multi's
					
					//3rd parameter "true" checks multi's as well.
					StaticTile[] tiles = map.Tiles.GetStaticTiles( i, j, true);
						

							
					//check all static tiles
					foreach( StaticTile tile in tiles )
					{
						if( tile.Z + tile.Height >= zcheck )
						{
							return false;
						}
					}
					
			
					
				}
				
			}

			
			//finally, check for items and mobiles
			
			Rectangle2D destrect2d = new Rectangle2D( new Point2D( p.X + _Extrema.Start.X, p.Y + _Extrema.Start.Y ), new Point2D( p.X + _Extrema.End.X, p.Y + _Extrema.End.Y ) );
			
			IPooledEnumerable ie = map.GetMobilesInBounds( destrect2d );
			
			foreach( Mobile m in ie )
			{
				//ignore mobiles already inside the addon
				if( Contains( m ) )
				{
					continue; 
				}
				//mobiles have a height of about 14?
				if( m.Z + 14 >= zcheck && m.Z < p.Z + _Extrema.End.Z )
				{
					return false;
				}
			}
			ie.Free();
			
			ie = map.GetItemsInBounds( destrect2d );
			
			foreach( Item item in ie )
			{
				//ignore items already in the addon, or that are part of the addon
				if( Contains( item ) || item is MovableAddonComponent && _Components.IndexOf( (MovableAddonComponent)item ) != -1 )
				{
					continue;
				}
				
				ItemData itemData = TileData.ItemTable[ item.ItemID & 0x3FFF ];
				TileFlag flags = itemData.Flags;
				
				//if the item is defined as impassible
				if( ( flags & TileFlag.Impassable ) != 0 )
				{
					int height = itemData.CalcHeight + 1;
					
					//this forces it so you can't overlap movable addon components!
					if( item is MovableAddonComponent )
					{
						height = 20;
					}
										
					if( item.Z + height >= zcheck && item.Z < p.Z + _Extrema.End.Z )
					{
				
						return false;
					}
					
				}
				
			}
			ie.Free();
			
			return true;
		}
		
		//this checks if the movable addon can rotate in the specified directioon
		public virtual bool CanRotate( bool clockwise )
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
			
			return CanRotateTo( (MovableAddonDirection)newdirection );
		}
		
		public bool CanRotateTo( MovableAddonDirection direction )
		{
			if( OutOfPower )
			{
				return false;
			}
			
			Rectangle3D tempextrema = _Extrema;
			
			int directiondiff = (int)direction - (int)_Direction;
			
			//clockwise degree rotation
			if( directiondiff == 1 || directiondiff == -3 )
			{
				//rotate extrema accordingly
				_Extrema = new Rectangle3D( new Point3D( -_Extrema.End.Y, _Extrema.Start.X, _Extrema.Start.Z ), new Point3D( -_Extrema.Start.Y, _Extrema.End.X, _Extrema.End.Z ) );
			}
			//counterclockwise
			else if( directiondiff == -1 || directiondiff == 3 )
			{
				_Extrema = new Rectangle3D( new Point3D( _Extrema.Start.Y, -_Extrema.End.X, _Extrema.Start.Z ), new Point3D( _Extrema.End.Y, -_Extrema.Start.X, _Extrema.End.Z ) );				
				
			}
			//about face
			else if( directiondiff == -2 || directiondiff == 2 )
			{
				_Extrema = new Rectangle3D( new Point3D( -_Extrema.End.X, -_Extrema.End.Y, _Extrema.Start.Z ), new Point3D( -_Extrema.Start.X, -_Extrema.Start.Y, _Extrema.End.Z ) );				
				
			}
			
			bool canrotate = CanMoveTo( Location, Map );
			
			_Extrema = tempextrema;
			
			return canrotate;
			
		}
		
		
		//this handles the addon movement operation		
		public bool MoveTo( Point3D point )
		{
			return MoveTo( point, Map );
		}
		
		public bool MoveTo( Point3D point, Map map )
		{
			return MoveTo( point, map, false );
		}
		
		public virtual bool MoveTo( Point3D point, Map map, bool forcemove )
		{
			if( !forcemove && !CanMoveTo( point ) )
			{
				return false;
			}
			
			//check world wrap using boat wrap info and code
			Rectangle2D[] wrap = Server.Multis.BaseBoat.GetWrapFor( map );

			for ( int i = 0; i < wrap.Length; ++i )
			{
				Rectangle2D rect = wrap[i];

				if ( rect.Contains( new Point2D( X, Y ) ) && !rect.Contains( new Point2D( point.X, point.Y ) ) )
				{
					int newx = point.X;
					int newy = point.Y;
					
					if ( newx < rect.X )
						newx = rect.X + rect.Width - 1;
					else if ( newx >= rect.X + rect.Width )
						newx = rect.X;

					if ( newy < rect.Y )
						newy = rect.Y + rect.Height - 1;
					else if ( newy >= rect.Y + rect.Height )
						newy = rect.Y;

					
					return( MoveTo( new Point3D( newx, newy, point.Z ), map, forcemove ) );
				}
			}

			MoveContents( point, map );
			
			MoveToWorld( point, map );
			
			
			return true;
		}
		
		//this method performs the rotate operation
		public virtual bool Rotate( bool clockwise )
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
			
			if( !CanRotateTo( (MovableAddonDirection)newdirection ) )
			{
				return false;
			}
				
			
			_Direction = (MovableAddonDirection)newdirection;
			
			foreach( MovableAddonComponent component in _Components )
			{
				component.Rotate( clockwise );
			}
			
			RotateContents( clockwise );
			
			_Extrema = new Rectangle3D( new Point3D( _Extrema.Start.Y, _Extrema.Start.X, _Extrema.Start.Z ), new Point3D( _Extrema.End.Y, _Extrema.End.X, _Extrema.End.Z ) );
			
			return true;
		}

		
		public void MoveContents( Point3D point )
		{
			MoveContents( point, Map );
			
		}
		
		//This method moves all items and mobiles within the addon to the specified new addon position
		public virtual void MoveContents( Point3D point, Map map )
		{
			Rectangle2D boundsrect = new Rectangle2D( new Point2D ( _Extrema.Start.X + X, _Extrema.Start.Y ), new Point2D ( _Extrema.End.X + X, _Extrema.End.Y + Y ) );
			
			List<IEntity> movables = new List<IEntity>();
			
			IPooledEnumerable ie = map.GetMobilesInBounds( boundsrect );
			
			
			//this checks if 
			bool keyholder = false;
			
			foreach( Mobile m in ie )
			{
				//ignore mobiles already inside the addon
				if( Contains( m ) )
				{
					if( HasKey( m ) )
					{
						keyholder = true;
					}
					movables.Add( m );
				}
			}
			ie.Free();
			
			if( !keyholder )
			{
				Command = MovableAddonCommand.Stop;
			}
			
			
			ie = map.GetItemsInBounds( boundsrect );
			
			foreach( Item item in ie )
			{
				if( item.Movable && Contains( item ) )
				{
					movables.Add( item );
				}
			}
			ie.Free();		
			
			foreach( IEntity ip in movables )
			{
				if( ip is Item )
				{
					((Item)ip).MoveToWorld( new Point3D( point.X - X + ip.X, point.Y - Y + ip.Y, point.Z - Z + ip.Z ), map );
				}
				else if( ip is Mobile )
				{
					((Mobile)ip).MoveToWorld( new Point3D( point.X - X + ip.X, point.Y - Y + ip.Y, point.Z - Z + ip.Z ), map );
				}
			}
			
		
		}
		
		//this method rotates the contents of the movable addon in the specified direction
		public virtual void RotateContents( bool clockwise )
		{
			Rectangle2D boundsrect = new Rectangle2D( new Point2D ( _Extrema.Start.X + X, _Extrema.Start.Y ), new Point2D ( _Extrema.End.X + X, _Extrema.End.Y + Y ) );
			
			List<IEntity> movables = new List<IEntity>();
			
			IPooledEnumerable ie = Map.GetMobilesInBounds( boundsrect );
			
			foreach( Mobile m in ie )
			{
				//ignore mobiles already inside the addon
				if( Contains( m ) )
				{
					movables.Add( m );
				}
			}
			ie.Free();
			
			ie = Map.GetItemsInBounds( boundsrect );
			
			foreach( Item item in ie )
			{
				if( item.Movable && Contains( item ) )
				{
					movables.Add( item );
				}
			}
			ie.Free();		
			
			foreach( IEntity ip in movables )
			{
				int offsety = ( ip.X - X ) * ( clockwise ? 1 : -1 );
				int offsetx = ( ip.Y - Y ) * ( clockwise ? -1 : 1 );
				
				
				if( ip is Item )
				{
					((Item)ip).MoveToWorld( new Point3D( X + offsetx, Y + offsety, Z ), Map );
				}
				else if( ip is Mobile )
				{
					((Mobile)ip).MoveToWorld( new Point3D( X + offsetx, Y + offsety, Z ), Map );
					int direction = ( (int)((Mobile)ip).Direction + ( clockwise ? 2 : -2 ) ) % 8;
					
					((Mobile)ip).Direction = (Direction)direction;
					
					
					
				}
			}
			
		
		}
		
		public virtual bool Refuel( Item item )
		{
			if( item.GetType() == PowerResourceType )
			{
				
				for( int i = 0; i < item.Amount; i++ )
				{
					ExhaustPowerTimeSpan += TimePerPowerResource;
				}
				
				item.Delete();
				
				_Controller.InvalidateProperties();
				
				return true;
			}
			
			return false;
		}
	
		
		//this method starts the drive timer
		public void StartDriveTimer()
		{
			if( _DriveTimer != null )
			{
				_DriveTimer.Stop();
			}
			
			_DriveTimer = Timer.DelayCall( _Delay, _Delay, new TimerCallback( OnTimer ) );
		}

		//this method stops the drive timer
		public void StopDriveTimer()
		{
			if( _DriveTimer != null )
			{
				_DriveTimer.Stop();
			}
			
			_DriveTimer = null;
		}
		
		//the movement control is defined in the inherited classes
		protected virtual void OnTimer()
		{
			if( OutOfPower )
			{
				Command = MovableAddonCommand.Stop;
			}
			
			_Controller.InvalidateProperties();
		}
		
		//this method is called when the specified mobile uses the key while inside the addon
		public virtual void OnUseKeyInAddon( Mobile from )
		{
		}
		
		//this method is called when the specified mobile uses the key while outside the addon
		public virtual void OnUseKeyOutsideAddon( Mobile from )
		{
		}
		
		
		//these are used to update all movable addon components
		public override void OnLocationChange( Point3D oldLoc )
		{
			if ( Deleted )
			{
				return;
			}
			
			foreach( MovableAddonComponent component in _Components )
			{
				component.UpdatePosition();
			}
		}
		
		//these are used to update all movable addon components		
		public override void OnMapChange()
		{
			if ( Deleted )
			{
				return;
			}
			
			foreach ( MovableAddonComponent component in _Components )
			{
				component.UpdatePosition();
			}
		}

		//this cleans up all movable addon components, and removes the reference to this addon in the key item
		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if( _Key != null )
			{
				_Key.RemoveAddon();
			}
			
			foreach ( MovableAddonComponent component in _Components )
			{
				if( component != null )
				{
					component.Delete();
				}
			}
		}
		

				
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			
			writer.Write( _Components.Count );
			
			foreach( MovableAddonComponent component in _Components )
			{
				writer.Write( component );
			}
			
			writer.Write( (int)_Direction );
			writer.Write( _Controller );
			writer.Write( _Key );
			#if( RunUORC1 )
				writer.Write( _Extrema.Start.X );
				writer.Write( _Extrema.Start.Y );
				writer.Write( _Extrema.Start.Z );
				
				writer.Write( _Extrema.End.X );
				writer.Write( _Extrema.End.Y );
				writer.Write( _Extrema.End.Z );
			#else
				/*To RunUO 2.0 RC1 users:  if you're getting an error here, then uncomment line 2 of this file to read:
				
				#define RunUORC1
				
				*/
				writer.Write( _Extrema );
			#endif
			writer.Write( (double)_Delay.Seconds );
			
			writer.Write( ExhaustPowerTimeSpan );
			
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			_Components = new List<MovableAddonComponent>();
			
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			int count = reader.ReadInt();
			for( int i = 0; i < count; i++ )
			{
				_Components.Add( (MovableAddonComponent)reader.ReadItem() );
			}
			
			_Direction = (MovableAddonDirection)reader.ReadInt();
			
			_Controller = (MovableAddonControlComponent)reader.ReadItem();
			_Key = (MovableAddonKey)reader.ReadItem();
			
			#if( RunUORC1 )
				_Extrema = new Rectangle3D( new Point3D( reader.ReadInt(), reader.ReadInt(), reader.ReadInt() ), new Point3D( reader.ReadInt(), reader.ReadInt(), reader.ReadInt() ) );
			#else
				/*To RunUO 2.0 RC1 users:  if you're getting an error here, then uncomment line 2 of this file to read:
				
				#define RunUORC1
				
				*/
				_Extrema = reader.ReadRect3D();
			#endif
			
			_Delay = TimeSpan.FromSeconds( reader.ReadDouble() );
			
			ExhaustPowerTimeSpan = reader.ReadTimeSpan();
			
			_Speed = MovableAddonSpeed.Regular;
		}
		
	}
}