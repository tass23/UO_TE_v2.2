using System;
using System.Collections;
using System.IO;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Commands;

namespace Server.Commands
{
	public class Emote
	{	
		private static TimeSpan EmoteDelayPlayers = TimeSpan.FromSeconds( 10.0 );
		private static TimeSpan EmoteDelayStaff = TimeSpan.FromSeconds( 1.0 );
		private const bool UseAnimations = true;
	
		public static void Initialize()
		{
			CommandSystem.Register( "Emote", AccessLevel.Player, new CommandEventHandler( Emote_OnCommand ) );
			CommandSystem.Register( "E", AccessLevel.Player, new CommandEventHandler( Emote_OnCommand ) );
		}

	  	[Usage( "Emote <sound>" )] 
	    [Description( "Express your emotions with sounds!" )] 
		public static void Emote_OnCommand( CommandEventArgs e )
		{
			PlayerMobile pm = (PlayerMobile)e.Mobile;
			
			TimeSpan EmoteDelay = (pm.AccessLevel > AccessLevel.Player) ? EmoteDelayStaff : EmoteDelayPlayers;
			
			if( (pm.LastEmote + EmoteDelay) >= DateTime.Now )
			{
				pm.SendMessage( "You must wait a few moments before emoting again." );
				return;
			}
			
			switch( e.ArgString.Trim().ToLower() )
			{
				case "ah":
					pm.PlaySound( pm.Female ? 778 : 1049 );
					pm.Say( "*ah!*" );
					break;
					
				case "ahha":
					pm.PlaySound( pm.Female ? 779 : 1050 );
					pm.Say( "*ahha!*" );
					break;
					
				case "applaud":
					pm.PlaySound( pm.Female ? 780 : 1051 );
					pm.Say( "*applauds*" );
					break;
					
				case "blownose":
					pm.PlaySound( pm.Female ? 781 : 1052 );
					pm.Say( "*blows nose*" );				
					if ( !pm.Mounted && UseAnimations )
						pm.Animate( 34, 5, 1, true, false, 0 );
					break;
					
				case "bscough":
					pm.PlaySound( pm.Female ? 786 : 1057 );
					pm.Say( "*bs cough*" );
					break;
					
				case "burp":
					pm.PlaySound( pm.Female ? 782 : 1053 );
					pm.Say( "*burp!*" );
					if ( !pm.Mounted && UseAnimations )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
					
				case "cheer":
					pm.PlaySound( pm.Female ? 783 : 1054 );
					pm.Say( "*cheers*" );
					break;
					
				case "clap":
					pm.PlaySound( pm.Female ? 780 : 1051 );
					pm.Say( "*claps*" );
					break;
					
				case "clearthroat":
					pm.PlaySound( pm.Female ? 784 : 1055 );
					pm.Say( "*clears throat*" );
					if ( !pm.Mounted && UseAnimations )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
					
				case "cough":
					pm.PlaySound( pm.Female ? 785 : 1056 );
					pm.Say( "*cough*" );				
					if ( !pm.Mounted && UseAnimations )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
					
				case "cry":
					pm.PlaySound( pm.Female ? 787 : 1058 );
					pm.Say( "*cries*" );
					break;
					
				case "fart":
					pm.PlaySound( pm.Female ? 792 : 1064 );
					pm.Say( "*farts*" );
					break;
					
				case "gasp":
					pm.PlaySound( pm.Female ? 793 : 1065 );
					pm.Say( "*gasps*" );
					break;
					
				case "giggle":
					pm.PlaySound( pm.Female ? 794 : 1066 );
					pm.Say( "*giggles*" );
					break;
					
				case "groan":
					pm.PlaySound( pm.Female ? 795 : 1067 );
					pm.Say( "*groans*" );
					break;
					
				case "growl":
					pm.PlaySound( pm.Female ? 796 : 1068 );
					pm.Say( "*growls*" );
					break;
					
				case "hey":
					pm.PlaySound( pm.Female ? 797 : 1069 );
					pm.Say( "*hey!*" );
					break;
					
				case "hiccup":
					pm.PlaySound( pm.Female ? 798 : 1070 );
					pm.Say( "*hiccups*" );
					break;
					
				case "huh":
					pm.PlaySound( pm.Female ? 799 : 1071 );
					pm.Say( "*huh?*" );
					break;
					
				case "kiss":
					pm.PlaySound( pm.Female ? 800 : 1072 );
					pm.Say( "*kisses*" );
					break;

				case "knock":
					pm.PlaySound( pm.Female ? 296 : 296 );
					pm.Say( "*gently knocks*" );
					pm.PlaySound( pm.Female ? 296 : 296 );
					pm.PlaySound( pm.Female ? 296 : 296 );
					break;
					
				case "laugh":
					pm.PlaySound( pm.Female ? 794 : 1073 );
					pm.Say( "*laughs*" );
					break;
					
				case "lol":
					pm.PlaySound( pm.Female ? 794 : 1073 );
					pm.Say( "*lol*" );
					break;
					
				case "no":
					pm.PlaySound( pm.Female ? 802 : 1074 );
					pm.Say( "*no!*" );
					break;
					
				case "oh":
					pm.PlaySound( pm.Female ? 803 : 1075 );
					pm.Say( "*oh!*" );
					break;
					
				case "oooh":
					pm.PlaySound( pm.Female ? 811 : 1085 );
					pm.Say( "*oooh!*" );
					break;
					
				case "oops":
					pm.PlaySound( pm.Female ? 812 : 1086 );
					pm.Say( "*oops!*" );
					break;
					
				case "puke":
					pm.PlaySound( pm.Female ? 813 : 1087 );
					pm.Say( "*pukes!*" );
					break;
					
				case "scream":
					pm.PlaySound( pm.Female ? 814 : 1088 );
					pm.Say( "*aaahh!*" );
					break;
					
				case "shh":
				case "shush":
					pm.PlaySound( pm.Female ? 815 : 1089 );
					pm.Say( "*shh!*" );
					break;
					
				case "sigh":
					pm.PlaySound( pm.Female ? 816 : 1090 );
					pm.Say( "*sigh*" );
					break;
					
				case "sneeze":
					pm.PlaySound( pm.Female ? 817 : 1091 );
					pm.Say( "*sneezes*" );
					if ( !pm.Mounted && UseAnimations )
						pm.Animate( 32, 5, 1, true, false, 0 );
					break;
					
				case "sniff":
					pm.PlaySound( pm.Female ? 818 : 1092 );
					pm.Say( "*sniffs*" );
					if( !pm.Mounted && UseAnimations )
						pm.Animate( 34, 5, 1, true, false, 0 );
					break;
					
				case "snore":
					pm.PlaySound( pm.Female ? 819 : 1093 );
					pm.Say( "*snores*" );
					break;
					
				case "spit":
					pm.PlaySound( pm.Female ? 820 : 1094 );
					pm.Say( "*spits*" );
					if ( !pm.Mounted && UseAnimations )
						pm.Animate( 6, 5, 1, true, false, 0 );
					break;
					
				case "whistle":
					pm.PlaySound( pm.Female ? 821 : 1095 );	
					pm.Say( "*whistles*" );
					if ( !pm.Mounted && UseAnimations )
						pm.Animate( 5, 5, 1, true, false, 0 );
					break;
					
				case "woohoo":
					pm.PlaySound( pm.Female ? 783 : 1054 );
					pm.Say( "*woohoo*" );
					break;
					
				case "yawn":
					pm.PlaySound( pm.Female ? 822 : 1096 );
					pm.Say( "*yawns*" );
					if ( !pm.Mounted && UseAnimations )
						pm.Animate( 17, 5, 1, true, false, 0 );
					break;
					
				case "yea":
				case "yeah":
					pm.PlaySound( pm.Female ? 823 : 1097 );
					pm.Say( "*yeah!*" );
					break;
					
				case "yell":
					pm.PlaySound( pm.Female ? 824 : 1098 );
					pm.Say( "*yells*" );
					break;
					
				default:
					pm.SendMessage( "Unrecognized emote. The choices are: ah, ahha, applaud, blownose, bscough, burp, cheer, clap, clearthroat, cough, cry, fart, gasp, giggle, groan, growl, hey, hiccup, huh, kiss, knock, laugh, lol, no, oh, oooh, oops, puke, scream, shh, shush, sigh, sneeze, sniff, snore, spit, whistle, woohoo, yawn, yea, yeah, yell." );
					return;
			}
			
			pm.LastEmote = DateTime.Now;
		} 
	}
}
