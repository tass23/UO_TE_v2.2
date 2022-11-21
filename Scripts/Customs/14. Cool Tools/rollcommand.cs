using Server.Engines.PartySystem;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Commands
{
    public class RollCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register( "roll", AccessLevel.Player, new CommandEventHandler( Roll_OnCommand ) );
        }

        [Usage( "Roll" )]
        [Description( "Outputs the result of a roll with a 100-sided dice into the party chat" )]
        public static void Roll_OnCommand( CommandEventArgs e )
        {
            PlayerMobile m = e.Mobile as PlayerMobile;
            if ( m == null )
                return;

            Party party = m.Party as Party;
            if ( party == null )
                return;

            if (party.Count <= 0)
            {
                m.SendMessage( "Your party is empty!" );
                return;
            }

            party.SendPublicMessage( m, string.Format( "«I rolled the dice and I got a {0}.» (Party-ID: {1:X})", Utility.Random( 1, 100 ), party.GetHashCode() ) );

        }
    }
}