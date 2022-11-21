using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Prompts;
using Server.Network;
using Server.Regions;

namespace Server.Gumps
{
	public class CityCorpseFeeGump : Gump
	{
		private CityResurrectionStone m_Stone;

		public CityCorpseFeeGump( CityResurrectionStone stone ) : base( 50, 50 )
		{
			m_Stone = stone;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(17, 12, 300, 139, 5120);
			AddItem(7, 15, 2);
			AddItem(28, 15, 3);
			AddHtml( 76, 18, 234, 66, @"<BASEFONT COLOR=WHITE><CENTER>This city has a fee to retrive your corpse.<BR>Woul you like to get your corpse?</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddLabel(29, 92, 1149, @"Cost: " + stone.Sign.Stone.CorpseFee.ToString() );
			AddButton(82, 115, 247, 248, 1, GumpButtonType.Reply, 0);
			AddButton(190, 115, 241, 242, 2, GumpButtonType.Reply, 0);

		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 ) // Accept
         		{
				if ( Banker.Withdraw( from, m_Stone.Sign.Stone.CorpseFee ) )
				{
					Item corpse = from.Corpse;

                     if ( corpse != null )
                     	
					{
                     	
                     	if ( m_Stone.RegCorpse( from ) )
                     	{
                     	
                     		m_Stone.Sign.Stone.CityTreasury += m_Stone.Sign.Stone.CorpseFee;
							corpse.MoveToWorld( from.Location, from.Map );
							from.SendMessage( "Your corpse has been found." );
							Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 10, 30, 5052 );
							Effects.PlaySound( from.Location, from.Map, 0x201 );
                     	}
                     	else
                     		from.SendMessage( "You may only do this 3 times a week. No Money taken." );
					}
					else
					{
						from.SendMessage( "Your corpse could not be located." );
						from.SendMessage( "No money was taken from your back account since corpse was not found." );
					}
				}
				else
				{
					from.SendMessage( "You do not have enough money in your back account." );
				}
			}
		}
	}
}
