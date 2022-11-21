using System; 
using System.Collections; 
using Server.Mobiles;
using Server.Gumps;
using Server.Commands;

namespace Server.Scripts.Commands 
{ 
	public class NotoCommand
	{ 
		public static void Initialize() 
		{ 
			CommandSystem.Register( "Notoriety", AccessLevel.Player, new CommandEventHandler( Notoriety_OnCommand ) );
			CommandSystem.Register( "Noto", AccessLevel.Player, new CommandEventHandler( Notoriety_OnCommand ) );
			CommandSystem.Register( "NotoGump", AccessLevel.Player, new CommandEventHandler( NotoGump_OnCommand ) );
			CommandSystem.Register( "NGump", AccessLevel.Player, new CommandEventHandler( NotoGump_OnCommand ) );
		}    
      
		[Usage( "Notoriety" )] 
		[Description( "Displays your Fame/Karma/Kills. (also Noto)" )]
		public static void Notoriety_OnCommand( CommandEventArgs e ) 
		{
			Mobile from = e.Mobile;
			if ( from != null )
			{
				from.SendMessage( "The Number of Kills you have is '{0}'", from.Kills );
				from.SendMessage( "Your Karma is '{0}'", from.Karma );
				from.SendMessage( "Your Fame is '{0}'", from.Fame );
			}
		}

		[Usage( "NotoGump" )]
		[Description( "Displays your Fame/Karma/Kills via a gump. (also NGump)" )]
		public static void NotoGump_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( from != null )
			{
				from.CloseGump( typeof( NotoGump ) );
				from.SendGump( new NotoGump( from ) );
			}
		}
	}
}

namespace Server.Gumps
{
	public class NotoGump : Gump
	{
		Mobile From;
		public NotoGump( Mobile m ) : base( 0, 0 )
		{
			From = m;
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(0, 0, 172, 123, 9270);
			AddLabel(50, 10, 1160, @"NOTORIETY"); 	
			AddLabel(15, 35, 1149, @"FAME:");
			AddLabel(85, 35, 1149, String.Format("{0}",From.Fame));
			AddLabel(15, 60, 1149, @"KARMA:");
			AddLabel( 85, 60, ( From.Karma >= 0 ? 1165 : 1164 ), String.Format( "{0}", From.Karma ) );
			AddLabel(15, 85, 1149, @"KILLS:");
			AddLabel( 85, 85, ( From.Karma < 5 ? 1149 : 1164 ), String.Format( "{0}", From.Kills ) );
		}
	}
}