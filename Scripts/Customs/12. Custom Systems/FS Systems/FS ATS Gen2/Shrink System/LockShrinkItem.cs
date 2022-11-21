using System; 
using Server; 
using Server.Gumps; 
using System.Collections;
using Server.Network;
using Server.Multis; 
using Server.Menus; 
using Server.Menus.Questions;
using Server.Items;

namespace Server.ContextMenus
{
	public class LockShrinkItem : ContextMenuEntry
	{
		private Mobile m_From;
		private ShrinkItemX m_ShrinkItem;

		public LockShrinkItem( Mobile from, ShrinkItemX shrink ) : base( 2029, 5 )
		{
			m_From = from;
			m_ShrinkItem = shrink;
		}

		public override void OnClick()
		{
			if ( m_From != m_ShrinkItem.PetOwner )
			{
				m_From.SendMessage( "You do not own this pet." );
			}
			else
			{
				m_ShrinkItem.Lock = true;
				m_From.SendMessage( 38, "You have locked the pet so only you can reclaim it." );
			}
		}
	}
}