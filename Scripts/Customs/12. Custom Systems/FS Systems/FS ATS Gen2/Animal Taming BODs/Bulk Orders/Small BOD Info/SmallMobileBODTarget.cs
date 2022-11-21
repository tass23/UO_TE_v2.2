using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.BulkOrders
{
	public class SmallMobileBODTarget : Target
	{
		private SmallMobileBOD m_Deed;

		public SmallMobileBODTarget( SmallMobileBOD deed ) : base( 18, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( m_Deed.Deleted || !m_Deed.IsChildOf( from.Backpack ) )
				return;
		
			if ( targeted is BaseCreature )
			{
          			BaseCreature c = (BaseCreature)targeted;
				if ( c.IsBonded == true && c.ControlMaster == from )
				{
					from.SendMessage( "You cannot bring yourself to get rid of just a trusted friend." );
				}
				else if ( c.Summoned == true )
				{
					from.SendMessage( "You cannot add summoned creatures to this bulk order." );
				}
				else if ( c.ControlMaster == from )
				{
					m_Deed.EndMobileCombine( from, targeted );
				}
				else
				{
					from.SendMessage( "Only pets that you control can be added to this deed." );
				}
			}
		}
	}
}