using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using System.IO;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Commands;
using Server.Multis;
using Server.Items;
using Server.Gumps;
using Server.Targeting;
		
namespace Server.Gumps
{
	public class KarmaGump : Gump
	{
		public KarmaGump( Mobile m ) : base( 0, 0 )
		{
			Closable = true;
			Disposable = false;
			Dragable = true;
			Resizable = false;
			AddPage( 0 );
			AddImage( 0, 0, 103, 1480 );
			AddImage( 18, 13, 5037, 11 );

			if ( m != null )
			{
				if ( m.Karma > 4999 && m.Title == "the Jedi" | m.Title == "the Jedi Master" )
					AddLabel( 37, 11, 1085, "Jedi Karma" );
				else if ( m.Karma < -4999 && m.Title == "the Sith Apprentice" | m.Title == "the Sith Lord" )
					AddLabel( 37, 11, 1085, "Sith Karma" );
				else if ( m.Karma > -5000 && m.Karma < 5000 && m.Title == "the Jedi Exile" )
					AddLabel( 37, 11, 1085, "Exile Karma" );
				else
					AddLabel( 37, 11, 1085, "Your Karma" );
			}

			AddLabel( 15, 35, 1153, "Fame: " );
			AddLabel( 67, 35, 1085, String.Format( "{0}",m.Fame ) );
			AddLabel( 15, 60, 1153, "Karma: " );
			AddLabel( 67, 60, 1085, String.Format( "{0}", m.Karma ) );
		}
	}
}