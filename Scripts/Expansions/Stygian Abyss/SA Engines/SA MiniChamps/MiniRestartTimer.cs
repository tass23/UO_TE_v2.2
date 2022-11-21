using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Engines.SAMiniChamps
{
	public class MiniRestartTimer : Timer
	{
		private MiniChampSpawn m_MiniSpawn;

		public MiniRestartTimer( MiniChampSpawn spawn, TimeSpan delay ) : base( delay )
		{
			m_MiniSpawn = spawn;
			Priority = TimerPriority.FiveSeconds;
		}

		protected override void OnTick()
		{
			m_MiniSpawn.EndRestart();
		}
	}
}