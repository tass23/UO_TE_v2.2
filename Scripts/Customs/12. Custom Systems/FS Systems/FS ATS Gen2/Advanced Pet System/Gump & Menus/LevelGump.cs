using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
	public class LevelGump : Gump
	{
		private Mobile m_Pet;

		public LevelGump( Mobile pet ) : base( 0, 0 )
		{
			m_Pet = pet;

			BaseCreature bc = (BaseCreature)m_Pet;

			Closable=false;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddPage(1);
			AddBackground(18, 15, 329, 103, 5054);
			AddAlphaRegion(22, 21, 319, 89);
			AddLabel(26, 21, 1149, @"You pet has ability points available. Would you like");
			AddLabel(26, 35, 1149, @"to use them now?");
			AddButton(30, 57, 4023, 4024, 1, GumpButtonType.Reply, 0);
			AddButton(30, 83, 4017, 4018, 0, GumpButtonType.Reply, 0);
			AddLabel(62, 58, 1149, @"Yes, I would like to use them now.");
			AddLabel(62, 84, 1149, @"No, Ill wait till my pets next level.");

		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			BaseCreature bc = (BaseCreature)m_Pet;

			if ( from == null )
				return;

			if ( info.ButtonID == 0 )
			{
				from.SendMessage( "You will get this menu again the next time your pet gains a level." );
			}

			if ( info.ButtonID == 1 )
			{
				from.SendGump( new PetLevelGump( m_Pet ) );
			}
		}
	}
}