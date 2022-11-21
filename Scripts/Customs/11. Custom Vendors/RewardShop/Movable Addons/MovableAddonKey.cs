using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Gumps;
using Server.ContextMenus;

using Solaris.Addons;

//using Server.ContextMenus;

namespace Server.Items
{
	//the "key" device that lets a user control a movable adon
	public abstract class MovableAddonKey : Item
	{
		protected virtual string _CantCreateMessage{ get{ return "No man jeez!"; } }
		protected virtual string _PlaceTargetMessage{ get{ return "Where do you wish to place this?"; } }
		protected virtual string _PlaceConfirmMessage{ get{ return "This has now been placed."; } }

		//this type determines what item is used to modify the addon
		public virtual Type ModifyResourceType{ get{ return null; } }
		
				
		protected MovableAddon _Addon;
		public MovableAddon Addon{ get{ return _Addon; } set{ _Addon = value; } }

		//this stores the hue of the addon
		public int AddonHue;		
		
		//this stores the time left until the addon runs out of power 
		public TimeSpan ExhaustPowerTimeSpan;
		
				
		[Constructable]
		public MovableAddonKey()
		{
			ExhaustPowerTimeSpan = MovableAddon.InitialPowerTime;
		}
		
		public MovableAddonKey( Serial serial ) : base( serial )
		{
		}
		
		//this method checks whether a user can create the movable addon.  defined in the inherited classes
		public virtual bool CanCreateAddon( Mobile from )
		{
			return false;
		}
		
		
		
		public override void OnDoubleClick( Mobile from )
		{
			//if the addon doesn't exist
			if( Addon == null )
			{
				//and the user can create it
				if(  CanCreateAddon( from ) )
				{
					StartCreation( from );
				}
				else
				{
					from.SendMessage( _CantCreateMessage );
					
				}
			}
			//if the addon exists
			else
			{
				if( Addon.CanUse( from ) )
				{
					if( Addon.Contains( from ) )
					{
						Addon.OnUseKeyInAddon( from );
					}
					else
					{
						Addon.OnUseKeyOutsideAddon( from );
					}
				}
			}
		}
		
		//this method is called when a user initiates a creation request.  it brings up a target cursor for them to choose a location to place the corresponding movable addon
		public virtual void StartCreation( Mobile from )
		{
			from.SendMessage( _PlaceTargetMessage );
			from.Target = new PlaceMovableAddonTarget( this );
		}
		
		//note: this is executed after the inherited class creates the movable addon to its liking
		public virtual void FinishCreation( Mobile from, Point3D point )
		{
			//the inherited class should create the addon and then call this method.  If there's a problem, this method will show
			if( _Addon == null )
			{
				from.SendMessage( "There was an error generating this addon.  Consult staff!" );
				return;
			}
			
			_Addon.ExhaustPowerTimeSpan = ExhaustPowerTimeSpan;
			point = new Point3D( point.X, point.Y, _Addon.GetHighestZ( point, from.Map ) );
			
			bool trashaddon = false;
			
			
			if( !from.InRange( point, 10 ) )
			{
				from.SendMessage( "You are out of range." );
				trashaddon = true;
			}
			else if( !CanCreateAddon( from ) )
			{
				from.SendMessage( "You can no longer do that" );
				trashaddon = true;
			}
			else if( !_Addon.CanCreateAt( point, from.Map ) )
			{
				from.SendMessage( "That is not a valid location." );
				trashaddon = true;
			}
			else if( !_Addon.CanMoveTo( point, from.Map ) )
			{
				from.SendMessage( "That cannot fit there.  Please try again." );
				trashaddon = true;
			}
			else
			{
				_Addon.MoveTo( point, Map, true );
				from.SendGump( new ConfirmAddonPlaceGump( this, from ) );
			}
			
			if( trashaddon )
			{
				_Addon.Delete();
				_Addon = null;
			}
			
		}
		
		//finally, this occurs after the user has confirmed that they wish to create their movable addon where they selected
		public virtual void ConfirmCreation( Mobile from, bool confirmed )
		{
			if( !confirmed )
			{
				if( _Addon != null )
				{
					from.SendMessage( "Placement cancelled" );
					_Addon.Delete();
					_Addon = null;
				}
			}
			else
			{
				_Addon.Key = this;
				
				
				AddonHue = _Addon.Hue;
				from.SendMessage( _PlaceConfirmMessage );
				InvalidateProperties();
			}
		}
		
		public virtual void RemoveAddon()
		{
			ExhaustPowerTimeSpan =  _Addon.ExhaustPowerTimeSpan;
			
			if( !Addon.Deleted )
			{
				Addon.Delete();
			}
			
			
			Addon = null;
			
			InvalidateProperties();
		}
		
		public override void OnAfterDelete()
		{
			base.OnAfterDelete();
			
			if( _Addon != null )
			{
				_Addon.Delete();
			}
			
		}
		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( IsChildOf( from.Backpack ) )
			{
				list.Add( new MovableAddonConfigEntry( from, this, 1 ) );
			}
		}

		public virtual void SendConfigGump( Mobile from )
		{
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			
			writer.Write( _Addon );
			writer.Write( AddonHue );
			writer.Write( ExhaustPowerTimeSpan );
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			_Addon = (MovableAddon)reader.ReadItem();
			AddonHue = reader.ReadInt();
			ExhaustPowerTimeSpan = reader.ReadTimeSpan();
		}
		
		
		//targeting device for placing a movable addon
		class PlaceMovableAddonTarget : Target
		{
			MovableAddonKey _Key;
			
			public PlaceMovableAddonTarget( MovableAddonKey key ) : base ( -1, true, TargetFlags.None ) 
			{
				_Key = key;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
			
				if( targeted is IPoint3D )
				{
					IPoint3D ip = (IPoint3D)targeted;
					
					Point3D targlocation = new Point3D( ip.X, ip.Y, ip.Z );
					
					_Key.FinishCreation( from, targlocation );
				}
				else
				{
					//TODO: error message?
					
				}
				
			}
			
		}
		
		

		
	}
	
	
}