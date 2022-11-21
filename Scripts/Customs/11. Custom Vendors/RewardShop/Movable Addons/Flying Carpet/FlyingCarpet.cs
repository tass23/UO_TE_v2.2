using System;
using System.Collections.Generic;
using System.Threading;
using Server;
using Server.Items;
using Server.Spells;

namespace Solaris.Addons
{
	//the flying carpet is a movable addon that lets the user fly around on a carpet of customizable size and design.
	public class FlyingCarpet : MovableAddon
	{
		//maximum carpt length/width definitions
		public const int MAX_WIDTH = 13;
		public const int MAX_LENGTH = 21;
		
		//maximum elevation of carpet above map
		public const int MAX_ELEVATION = 60;
		
		
		//the default power source of a flying carpet is the chunks of obsidian from the Collector quest
		public override Type PowerResourceType{ get{ return typeof( PowerCrystal ); } }
		
		//each chunk of obsidian gives 24 hours of carpet drive time.  Note this time isn't consumed when the carpet is stored
		public override TimeSpan TimePerPowerResource{ get{ return TimeSpan.FromDays( 1 ); } }
		
		
		
		//definitions for the map tiles which the carpet cannot fly over.  By default, Malas' void tiles (stars) is blocked
		public override int[] NoMoveTiles
		{
			get
			{
				if( _NoMoveTiles == null )
				{
					_NoMoveTiles = new int[]
					{
						506, 507, 508, 509, 510, 511			//void tiles from Malas
					};
					
				}
				return _NoMoveTiles;
			}
					
			
		}

		//definitions for which maps can allow carpets to be built.  By default, Ilshenar is out
		public override bool[] UsableMaps
		{
			get
			{
				if( _UsableMaps == null )
				{
					_UsableMaps = new bool[]
					{
						true,		//felucca
						true,		//trammel
						false,		//ilshenar
						true,		//malas
						true		//tokuno
					};
					
				}
				return _UsableMaps;
			}
		}
		
		//definitions for places where the carpet can't be built
		public override Rectangle2D[][] NoBuildZones
		{
			get
			{
				if( _NoBuildZones == null )
				{
					_NoBuildZones = new Rectangle2D[][]
					{
						//Felucca
						new Rectangle2D[]
						{
						},
						//Trammel
						new Rectangle2D[]
						{
						},
						//Ilshenar
						new Rectangle2D[]
						{
						},
						//Malas
						new Rectangle2D[]
						{
						},
						//Tokuno
						new Rectangle2D[]
						{
						}
					};
					
					
				}
				
				return _NoBuildZones;
			}
		}

		//this determines the maximum height in which the carpet can fly.  It looks at the map below it and adds the max elevation
		public int MaxZHeight
		{
			get
			{
				return Map.GetAverageZ( X, Y ) + MAX_ELEVATION;
			}
		}
		
		//checks if the carpet is out of power.  If so, the genie will report
		public override bool OutOfPower
		{
			get
			{
				if( base.OutOfPower )
				{
					Land();
					
					_Controller.Say( "We've run out of power!" );
					return true;
				}
				return false;
			}
		}
		
		//default constructor: length 7, width 5, type 0
		public FlyingCarpet() : this( 7, 5, 0 )
		{
		}
		
		
		
		public FlyingCarpet( int length, int width, int type ) : this( length, width, type, 0 )
		{
		}
		
		
		public FlyingCarpet( int length, int width, int type, int hue ) : this( length, width, (FlyingCarpetType)type, hue )
		{
		}
		
		public FlyingCarpet( int length, int width, FlyingCarpetType type, int hue ) 
		{
			Hue = hue;
			length = Math.Min( MAX_LENGTH, Math.Max( 3, length ) );
			width = Math.Min( MAX_WIDTH, Math.Max( 3, width ) );
			

			
			//this monstrousity determines what part of the carpet this belongs to
			for( int i = -width / 2; i <= width / 2; i++ )
			{
				for( int j = -length / 2; j <= length / 2; j++ )
				{
					int part = 0;
					int direction = 0;
					
					//west
					if( i == -width / 2 )
					{
						//north
						if( j == -length / 2 )
						{
							part = 2;
							direction = 0;
						}
						//south
						else if( j == length / 2 )
						{
							part = 2;
							direction = 3;
						}
						else
						{
							part = 1;
							direction = 0;
						}
					}
					//east
					else if( i == width / 2 )
					{
						//north
						if( j == -length / 2 )
						{
							part = 2;
							direction = 1;
						}
						//south
						else if( j == length / 2 )
						{
							part = 2;
							direction = 2;
						}
						else
						{
							part = 1;
							direction = 2;
						}
					}
					else
					{
						//north
						if( j == -length / 2 )
						{
							part = 1;
							direction = 1;
						}
						//south
						else if( j == length / 2 )
						{
							part = 1;
							direction = 3;
						}
						
					}
					FlyingCarpetComponent component = new FlyingCarpetComponent( type, part, direction );
					component.Hue = Hue;
					
					AddComponent( component, i, j, 0 );
				}
			}
			
			
			FlyingCarpetGenie genie = new FlyingCarpetGenie( 9616 );	//use efreet frame itemid
			_Controller = genie;		
			AddComponent( genie, 0, length / 2 - 1, 1 );

			
		}
		
		public FlyingCarpet( Serial serial ) : base( serial )
		{
		}

		public override bool CanUse( Mobile from )
		{
			return HasKey( from );
		}
		
				
		//if the player uses the key outside the addon, it will teleport them to the carpet.  Handy if you fall off while it's flying!
		public override void OnUseKeyOutsideAddon( Mobile from )
		{
			//TODO: do proper spell transport check
			//TODO: make sound
			
			if ( Core.AOS && from.Spell is Spell && ((Spell)from.Spell).State == SpellState.Sequencing )
			{
				((Spell)from.Spell).Disturb( DisturbType.NewCast );
				return;
			}

			if ( !from.CheckAlive() )
			{
				return;
			}
			else if ( from.Spell != null && from.Spell.IsCasting )
			{
				from.SendLocalizedMessage( 502642 ); // You are already casting a spell.
				return;
			}

			Server.Spells.Fourth.RecallSpell spell = new Server.Spells.Fourth.RecallSpell( from, null, null, null );

			from.Spell = spell;
			
			spell.State = Server.Spells.SpellState.Sequencing;
 			spell.Effect( Location, Map, true );
 			
 			spell.FinishSequence();
			//from.MoveToWorld( Location, Map );
		}
		
		protected void RenderShadow()
		{
			foreach( MovableAddonComponent component in _Components )
			{
				int shadowz = GetShadowZ( component.X, component.Y );
				
				if( shadowz < Z - 1 )
				{
					Point3D shadowpoint = new Point3D( component.X, component.Y, shadowz );
					
					Effects.SendLocationParticles( EffectItem.Create( shadowpoint, Map, EffectItem.DefaultDuration ), component.ItemID, 1, 21, component.Hue, 1, 0, 0 );
				}
			}
			
		}
		
		private int GetShadowZ( int x, int y )
		{
			int z = this.Map.GetAverageZ( x, y );
			
			
			StaticTile[] tiles = Map.Tiles.GetStaticTiles( x, y, true);
						
			//check all static tiles
			foreach( StaticTile tile in tiles )
			{
				z = Math.Max( z, tile.Z + tile.Height );
			}
					
			
			/*bool canFit = this.Map.CanFit( x, y, z, 6, false, false );

			for ( int i = -3; !canFit && i <= 3; ++i )
			{
				canFit = this.Map.CanFit( x, y, z + i, 6, false, false ) ;

				if ( canFit )
					z += i;
			}*/

			//if( canFit )
				return z;
			//else
				//return -100;		//flag for can't fit shadow

		}
		
		
		protected override void OnTimer()
		{
			base.OnTimer();
			
			int basespeed = Math.Max( (int)_Speed, 1 );
			int longitudinalspeed = 0;
			int lateralspeed = 0;
			int ascentspeed = 0;
			
			
			
			if( ((int)_Command & (int)MovableAddonCommand.Forward ) > 0 )
			{
				longitudinalspeed = basespeed;
			}
			else if( ((int)_Command & (int)MovableAddonCommand.Back ) > 0 )
			{
				longitudinalspeed = -basespeed;
			}
			
			if( ((int)_Command & (int)MovableAddonCommand.Left ) > 0 )
			{
				lateralspeed = basespeed;
			}
			else if( ((int)_Command & (int)MovableAddonCommand.Right ) > 0 )
			{
				lateralspeed = -basespeed;
			}
			
			if( ((int)_Command & (int)MovableAddonCommand.Up ) > 0 )
			{
				ascentspeed = basespeed;
			}
			else if( ((int)_Command & (int)MovableAddonCommand.Down ) > 0 )
			{
				ascentspeed = -basespeed;
			}
			
			//slow movement if you're going only left and right
			if( longitudinalspeed == 0 )
			{
				lateralspeed = Math.Max( -1, Math.Min( 1, lateralspeed ) );
			}
			
			bool canmove = false;
			bool preparetostop = false;
			Point3D moveposition = Location;
			while( !canmove )
			{
			
				moveposition = new Point3D
				(
					X + longitudinalspeed * ( ( _Direction == MovableAddonDirection.East ? 1 : 0 ) + ( _Direction == MovableAddonDirection.West ? -1 : 0 ) )
					  + lateralspeed * ( ( _Direction == MovableAddonDirection.South ? 1 : 0 ) + ( _Direction == MovableAddonDirection.North ? -1 : 0 ) ),
					  
					Y + longitudinalspeed * ( ( _Direction == MovableAddonDirection.South ? 1 : 0 ) + ( _Direction == MovableAddonDirection.North ? -1 : 0 ) )
					  + lateralspeed * ( ( _Direction == MovableAddonDirection.West ? 1 : 0 ) + ( _Direction == MovableAddonDirection.East ? -1 : 0 ) ),
					  
					Math.Min( MaxZHeight, Z + ascentspeed )
				);
				
				canmove = CanMoveTo( moveposition );
				
				if( !canmove )
				{
					longitudinalspeed = Slowdown( longitudinalspeed );
					lateralspeed = Slowdown( lateralspeed );
					ascentspeed = Slowdown( ascentspeed );
					
					
					preparetostop = true;
					if( longitudinalspeed == 0 && lateralspeed == 0 && ascentspeed == 0 )
					{
						break;
					}
				}
			}
			
			if( longitudinalspeed != 0 || lateralspeed != 0 || ascentspeed != 0 )
			{
				MoveTo( moveposition, Map, true );
				
				RenderShadow();
				
				if( _Speed == MovableAddonSpeed.One )
				{
					Command = MovableAddonCommand.Stop;
				}
			}
			
			if( preparetostop )
			{
				((FlyingCarpetGenie)_Controller).Say( "And we've stopped." );
				Command = MovableAddonCommand.Stop;
			}
			
			
		}
		
		//used to decrease the speed until the movement fits within whatever barrier is blocking the path of motion
		protected int Slowdown( int value )
		{
			if( value > 0 )
			{
				return value - 1;
			}
			else if( value < 0 )
			{
				return value + 1;
			}
			return value;
		}
		
		//brings the carpet to the minimum height and stops
		public void Land()
		{
			int landz = int.MinValue;
			
			foreach( MovableAddonComponent component in _Components )
			{
				landz = Math.Max( landz, GetShadowZ( component.X, component.Y ) + 1 );
			}
			
			MoveTo( new Point3D( X, Y, landz ), Map, true );
			
			Command = MovableAddonCommand.Stop;
		}
		
		//brings the carpet to the maximum height and stops
		public void TakeOff()
		{
			MoveTo( new Point3D( X, Y, MaxZHeight ), Map );
			RenderShadow();
			Command = MovableAddonCommand.Stop;
		}
			
		//hues the carpet
		public void SetHue( int hue )
		{
			Hue = hue;
			if( _Key != null )
			{
				_Key.AddonHue = hue;
			}
			foreach( MovableAddonComponent component in _Components )
			{
				if( component != _Controller )
				{
					component.Hue = hue;
				}
			}
			
			((FlyingCarpetGenie)_Controller).Say( "Whoah, fancy!" );
			
		}	
		
		public override bool Refuel( Item resource )
		{
			if( base.Refuel( resource ) )
			{
				((FlyingCarpetGenie)_Controller).Say( "Power up!" );
				return true;
			}
			return false;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
		
	}
}