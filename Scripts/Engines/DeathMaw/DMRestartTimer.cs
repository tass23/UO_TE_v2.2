using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.DMChamps;

namespace Server.Engines.DMChamps
{
	public class DMRestartTimer : Timer
	{
		private DMChampSpawn m_DMSpawn;

		public DMRestartTimer( DMChampSpawn spawn, TimeSpan delay ) : base( delay )
		{
			m_DMSpawn = spawn;
			Priority = TimerPriority.FiveSeconds;
		}

		protected override void OnTick()
		{
			m_DMSpawn.EndRestart();
		}
	}
}