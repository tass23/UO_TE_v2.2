using System;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.ContextMenus
{
	public class StairRefundEntry : ContextMenuEntry
	{
		private Mobile m_From;
		private YardStair m_Stair;
		private int value = 0;

		public StairRefundEntry( Mobile from, YardStair stair, int price) : base( 6104, 9 )
		{
			m_From = from;
			m_Stair = stair;
			value = price;
		}
		public override void OnClick()
		{
			Container c = m_From.Backpack;
			Gold t = new Gold( value );
			if( c.TryDropItem( m_From, t, true ) )
			{
				m_Stair.Delete();
				m_From.SendMessage( "The item disolves and gives you a refund" );
			}
			else
			{
				t.Delete();
				m_From.SendMessage("For some reason, the refund didn't work!  Please page a GM");
			}
		}
	}
}