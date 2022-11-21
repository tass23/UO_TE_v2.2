using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class BeeHiveHouseTimer : Timer
	{
		private BeeHiveHouse m_TimerBeeHiveHouseVar;
		public int m_ApiRisqueMaladie;
		public int m_ApiProductivity;
		public int RucheActive;

		public BeeHiveHouseTimer( BeeHiveHouse m_TimerBeeHiveHouse ) : base( TimeSpan.FromSeconds(1),TimeSpan.FromMinutes (120.0),96)
		{
			m_TimerBeeHiveHouseVar = m_TimerBeeHiveHouse;
		}

		protected override void OnTick()
		{
			m_ApiRisqueMaladie = Utility.Random( 1, 100 );
			m_ApiProductivity = (int)(m_TimerBeeHiveHouseVar.LoreSkill /20);
			++RucheActive;
			ArrayList list2 = new ArrayList();
			foreach ( Item itembee2 in m_TimerBeeHiveHouseVar.GetItemsInRange( 0 ) )
			{
				if (itembee2 is BeeSwarm) list2.Add( itembee2 );
			}
			if (list2.Count == 0) 
			{
				Item itembee3 = new BeeSwarm();
				itembee3.MoveToWorld( m_TimerBeeHiveHouseVar.Location, m_TimerBeeHiveHouseVar.Map );
				if (itembee3 != null && itembee3.Map == Map.Internal) itembee3.Delete();
			}
			m_TimerBeeHiveHouseVar.PublicOverheadMessage( MessageType.Regular, 0x35, false, string.Format("Bzzzzzz" ));
			if (m_TimerBeeHiveHouseVar.m_HiveFull == true )
			{
				for ( int i = 0; i < m_ApiProductivity; ++i )
				{
					switch ( Utility.Random( 10 ))
					{
						case 0: ++m_TimerBeeHiveHouseVar.m_HiveHoney; break;
						case 1: ++m_TimerBeeHiveHouseVar.m_HiveWaxes; break;
					}
				}
			}
			if (m_TimerBeeHiveHouseVar.m_HiveFull == false )
			{
				m_TimerBeeHiveHouseVar.m_HiveFull = true;
				m_TimerBeeHiveHouseVar.Name = "A working BeeHiveHouse";
			}
			if (m_ApiRisqueMaladie > 95)
			{
				m_TimerBeeHiveHouseVar.m_HiveSick = true;
				m_TimerBeeHiveHouseVar.Name = "A sick BeeHiveHouse";
				Stop();
			}
			if (RucheActive >=96)
			{
				m_TimerBeeHiveHouseVar.m_HiveFull = false;
				m_TimerBeeHiveHouseVar.Name = "A sleeping BeeHiveHouse";
				Stop();
			}
		}
	}
}