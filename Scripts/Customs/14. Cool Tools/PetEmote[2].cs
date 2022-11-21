// Originally PetSpeak by FLuXx()
//Modded by Rhexy for emoting & disallowing squelched players from use.
using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Commands;


namespace Server.Commands
{
	public class PetThisCmd
	{
		public static void Initialize()
		{
			Register( "PetEmote", AccessLevel.Player, new CommandEventHandler( PetEmote_OnCommand ) );
			Register( "PE", AccessLevel.Player, new CommandEventHandler( PetEmote_OnCommand ) );
		}
		
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
			CommandSystem.Register( command, access, handler );
		}
		
		[Usage( "PetEmote <text>" )]
		[Description( "Allows owner to roleplay thier pet" )]
		public static void PetEmote_OnCommand( CommandEventArgs e )
		{ 
			if ( e.Mobile.Squelched )
			{
				e.Mobile.SendMessage( "You are squelched and cant use pet emoting." );
			}
			else 
			{
				string toEmote = "*" + e.ArgString.Trim() + "*";
			
				if( toEmote.Length > 0 )
					e.Mobile.Target = new EmoteThisTarget( toEmote );
				else
					e.Mobile.SendMessage( "Format: PetEmote \"<text>\"" );
			}
		}
		private class EmoteThisTarget : Target
		{
			private string m_toEmote;
			
			public EmoteThisTarget( string s ) : base( -1, false, TargetFlags.None )
			{
				m_toEmote = s ;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if( targeted is BaseCreature )
				{
					BaseCreature targ = (BaseCreature)targeted;
					
					if( targ.ControlMaster == from )
					{
						CommandLogging.WriteLine( from, "{0} {1} forcing speech on {2}", from.AccessLevel, CommandLogging.Format( from ), CommandLogging.Format( targ ) );
						targ.Emote( m_toEmote );
					}
					else
					{
						from.SendMessage( "You do not own this pet." );
					}
				}
			}
		}
	}
}