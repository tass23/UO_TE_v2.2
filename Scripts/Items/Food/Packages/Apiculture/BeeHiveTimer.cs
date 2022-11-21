using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class BeeHiveTimer : Timer
	{
		private BeeHive m_TimerBeeHiveVar;
		public int m_ApiRisqueMaladie;
		public int m_ApiProductivity;
		public int RucheActive;

		public BeeHiveTimer( BeeHive m_TimerBeeHive ) : base( TimeSpan.FromSeconds(1),TimeSpan.FromMinutes (60.0),96)
		{
			m_TimerBeeHiveVar = m_TimerBeeHive;
		}

		protected override void OnTick()
		{
			m_ApiProductivity = 10;
			ArrayList list2 = new ArrayList();
			foreach ( Item itembee2 in m_TimerBeeHiveVar.GetItemsInRange( 0 ) )
			{
				if (itembee2 is BeeSwarm) list2.Add( itembee2 );
			}
			if (list2.Count == 0)
			{
				Item itembee3 = new BeeSwarm();
				itembee3.MoveToWorld( m_TimerBeeHiveVar.Location, m_TimerBeeHiveVar.Map );
				if (itembee3 != null && itembee3.Map == Map.Internal) itembee3.Delete();
			}
			m_TimerBeeHiveVar.PublicOverheadMessage( MessageType.Regular, 0x35, false, string.Format("Bzzzzzz" ));
			if (m_TimerBeeHiveVar.m_HiveFull == true )
			{
				for ( int i = 0; i < m_ApiProductivity; ++i )
				{
					switch ( Utility.Random( 10 ))
					{
						case 0: ++m_TimerBeeHiveVar.m_HiveHoney; break;
						case 1: ++m_TimerBeeHiveVar.m_HiveWaxes; break;
					}
				}
			}
			if (m_TimerBeeHiveVar.m_HiveFull == false )
			{
				m_TimerBeeHiveVar.m_HiveFull = true;
				m_TimerBeeHiveVar.Name = "A working beehive";
				++m_TimerBeeHiveVar.m_HiveHoney;
				++m_TimerBeeHiveVar.m_HiveWaxes;
			}
		}
	}
}