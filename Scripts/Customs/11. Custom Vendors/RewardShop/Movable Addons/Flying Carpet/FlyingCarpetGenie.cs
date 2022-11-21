using System;
using Server;
using Server.Items;
using Server.Gumps;

namespace Solaris.Addons
{
	//this guy sits at the back of the carpet and listens to commands from the player who holds magic lamp
	public class FlyingCarpetGenie : MovableAddonControlComponent
	{
		
		public FlyingCarpetGenie( int itemid ) : this( new int[]{ itemid, itemid, itemid, itemid } )
		{
		}
		
		public FlyingCarpetGenie( int[] itemids ) : this( itemids, 0 )
		{
		}
		
		//master constructor
		public FlyingCarpetGenie( int[] itemids, int direction ) : base( itemids, direction )
		{
			Name = "Flying Carpet Genie";
		}
		
		public FlyingCarpetGenie( Serial serial ) : base( serial )
		{
		}
		
		
		
		

		public override void OnUseControlInAddon( Mobile from )
		{
			//bring up gump that allows control and refueling power
			from.SendGump( new FlyingCarpetControlGump( _Addon, from ) );
		}
		
		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled )
			{
				Mobile m = e.Mobile;
				
				if( !_Addon.Contains( m ) || !_Addon.HasKey( m ) )
				{
					return; 
				}
				
				MovableAddonCommand newcommand = MovableAddonCommand.Stop;
				MovableAddonSpeed newspeed = MovableAddonSpeed.Regular;

				bool foundacommand = false;
				
				foreach( string command in Enum.GetNames( typeof( MovableAddonCommand ) ) )
				{
					//if a command is detected
					if( e.Speech.ToLower().IndexOf( command.ToLower() ) != -1 )
					{
						foundacommand = true;
						//special case: stop command
						if( (MovableAddonCommand)Enum.Parse( typeof( MovableAddonCommand ), command ) == MovableAddonCommand.Stop )
						{
							newcommand = MovableAddonCommand.Stop;
							break;
						}
						else
						{
							//stack up all commands togetherr
							newcommand = (MovableAddonCommand)( (int)newcommand + (int)Enum.Parse( typeof( MovableAddonCommand ), command ) );
						}
						
					}
				}
					
				if( foundacommand )
				{
					foreach( string speed in Enum.GetNames( typeof( MovableAddonSpeed ) ) )
					{
						//if a command is detected
						if( e.Speech.ToLower().IndexOf( speed.ToLower() ) != -1 )
						{
							
							newspeed = (MovableAddonSpeed)Enum.Parse( typeof( MovableAddonSpeed ), speed );
							break;
						}
					}
					
					Say( "Your wish is my command!" );
					
					if( newspeed == MovableAddonSpeed.Turn )
					{
						if( newcommand == MovableAddonCommand.Left )
						{
							_Addon.Rotate( false );
						}
						else if( newcommand == MovableAddonCommand.Right )
						{
							_Addon.Rotate( true );
						}
						else
						{
							Say( "Um.. wait, what?" );
						}
						
					}
					else
					{
						_Addon.Speed = newspeed;
						_Addon.Command = newcommand;
					}
					
					e.Handled = true;
				}
				else
				{
					//special commands
					
					if( e.Speech.ToLower().IndexOf( "come about" ) != -1 )
					{
						Say( "Whoah, here we go!" );
						_Addon.Rotate( false );					
						_Addon.Rotate( false );
						e.Handled = true;
					}
					else if( e.Speech.ToLower().IndexOf( "land" ) != -1 )
					{
						Say( "Prepare to land.. watch out below!" );
						((FlyingCarpet)_Addon).Land();
						e.Handled = true;
					}
					else if( e.Speech.ToLower().IndexOf( "take off" ) != -1 )
					{
						Say( "Make rockets go now!" );
						((FlyingCarpet)_Addon).TakeOff();
						e.Handled = true;
					}
					
					
				}

			}
		}
						
		
		public override void OnUseControlOutsideAddon( Mobile from )
		{
			//TODO: check carpet for contents before furling
			
			
			from.SendMessage( "You are about to furl your carpet.  Please confirm." );
			from.SendGump( new ConfirmAddonRemovalGump( _Addon.Key, from ) );
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