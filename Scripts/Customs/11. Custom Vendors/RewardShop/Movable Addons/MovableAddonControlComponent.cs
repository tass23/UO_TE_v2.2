using System;
using Server;
using Server.Items;
using Server.Network;
using Solaris.CliLocHandler;

namespace Solaris.Addons
{
	//this component is used to relay commands and controls from the user to the movable addon
	public abstract class MovableAddonControlComponent : MovableAddonComponent
	{
		public override bool HandlesOnSpeech{ get{ return true; } }

		
		public MovableAddonControlComponent( int itemid ) : this( new int[]{ itemid, itemid, itemid, itemid } )
		{
		}
		
		public MovableAddonControlComponent( int[] itemids ) : this( itemids, 0 )
		{
		}
		
		//master constructor
		public MovableAddonControlComponent( int[] itemids, int direction ) : base( itemids, direction )
		{
			
		}
		
		public MovableAddonControlComponent( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( _Addon != null && _Addon.HasKey( from ) )
			{
				if( _Addon.Contains( from ) )
				{
					OnUseControlInAddon( from );
				}
				else
				{
					OnUseControlOutsideAddon( from );
				}
			}
			
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if( _Addon.PowerResourceType != null )
			{
				TimeSpan powerleft =  _Addon.ExhaustPowerTimeSpan;
				
				list.Add( 1076228, "{0}\t{1}", "Power Source:", CliLoc.GetName( _Addon.PowerResourceType ) );
				
				if( powerleft > TimeSpan.Zero )
				{
					list.Add( 1060663, "Time left until power expires\t{0}", TimeLeft( powerleft ) );
				}
				else
				{
					list.Add( 1070722, "Out of power" );
				}
				
			}
		}
		
		protected string TimeLeft( TimeSpan remaining )
		{
			string timeleft = "";
			if( remaining.Days > 0 )
			{
				timeleft += String.Format( "{0}d ", remaining.Days );
			}
			remaining -= TimeSpan.FromDays( remaining.Days );
			
			if( remaining.Hours > 0 )
			{
				timeleft += String.Format( "{0}h ", remaining.Hours );
			}
			remaining -= TimeSpan.FromHours( remaining.Hours );
			
			if( remaining.Minutes > 0 )
			{
				timeleft += String.Format( "{0}m ", remaining.Minutes );
			}
			remaining -= TimeSpan.FromMinutes( remaining.Minutes );
			
			if( remaining.Seconds > 0 )
			{
				timeleft += String.Format( "{0}s", remaining.Seconds );
			}
			
			return timeleft;
		}
		
		//speech control is handled in the inherited classes
		public override void OnSpeech( SpeechEventArgs e )
		{
		}		
		
		public void Say( string message )
		{
			PublicOverheadMessage( MessageType.Regular, 0x3B2, false, message );
			
		}
		
		public void Say( int number, string args )
		{
			PublicOverheadMessage( MessageType.Regular, 0x3B2, number, args );
		}

		
		public virtual void OnUseControlInAddon( Mobile from )
		{
		}
		
		public virtual void OnUseControlOutsideAddon( Mobile from )
		{
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