using System; 
using Server; 
using Server.Gumps; 
using System.Collections;
using Server.Network;
using Server.Multis; 
using Server.Menus; 
using Server.Menus.Questions;
using Server.Items;
using Server.Mobiles;

namespace Server.ContextMenus
{
	public class PetMenu : ContextMenuEntry
	{
		private Mobile m_From;
		private BaseCreature m_Bc;

		public PetMenu( Mobile from, BaseCreature Bc ) : base( 5031, 5 )
		{
			m_From = from;
			m_Bc = Bc;
		}

		public override void OnClick()
		{
			m_From.SendGump( new PetStatGump( m_Bc, m_From ) );
		}
	}
}