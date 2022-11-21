using System;
using System.Collections;
using Server.Prompts;
using Server.Mobiles;

namespace Server.Misc
{
	public class NameCheck
	{
		private static ArrayList AllowedDupes = new ArrayList(
			new string[]
				{
					"Your",
					"allowed duplicate",
					"names",
					"Here",
					"like this",
					"case is ignored"
				}
			);

		private static ArrayList Disallowed = new ArrayList(
			new string[]
				{
					"Your",
					"disallowed",
					"names",
					"Here",
					"like this",
					"case is ignored"
				}
			);

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( Check );
		}

		private static void Check( LoginEventArgs args )
		{
			if( !CheckDupe( args.Mobile, args.Mobile.RawName ) )
				args.Mobile.Prompt = new ChangeNamePrompt( args.Mobile );
		}

		public static bool CheckDupe( Mobile m, string name )
		{
			if( m == null || name == null || name.Length == 0 )
				return false;

			string nameToLower = name.ToLower();

			if( nameToLower == "generic player" )
				return false;

			if( AllowedDupes.Contains( nameToLower ) )
				return true;

			if( Disallowed.Contains( nameToLower ) )
				return false;

			if( !NameVerification.Validate( name, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote ) )
				return false;

			foreach( Mobile mob in World.Mobiles.Values )
			{
				if( mob is PlayerMobile && mob != m && mob.RawName != null && mob.RawName.ToLower() == nameToLower )
				{
					return false;
				}
			}

			return true;
		}

		public class ChangeNamePrompt : Prompt
		{
			public ChangeNamePrompt( Mobile from )
			{
				from.SendMessage( "Your chosen name {0} is already in use or is unacceptable for use on this shard.", from.Name );
				from.SendMessage( "Please type a new name now." );
				from.Name = "Generic Player";
			}

			public override void OnCancel( Mobile from )
			{
				if( from == null)
					return;
				from.Prompt = new ChangeNamePrompt( from );
			}

			public override void OnResponse( Mobile from, string text )
			{
				if( from == null)
				{
					return;
				}

				if( text != null )
				{
					text = text.Trim();
				}
				else
				{
					from.Prompt = new ChangeNamePrompt( from );
				}

				if ( text != "" )
				{
					if( CheckDupe( from, text ) && NameVerification.Validate( text, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote ) )
					{
						from.SendMessage( "Your name is now {0}.", text );
						from.Name = text;
						return;
					}
				}

				from.Prompt = new ChangeNamePrompt( from );
			}
		}
   	}
}