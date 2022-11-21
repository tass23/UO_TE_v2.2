using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Engines.SAMiniChamps
{
	public class MiniSliceTimer : Timer
	{
		private MiniChampSpawn m_MiniSpawn;

		public MiniSliceTimer( MiniChampSpawn spawn ) : base( TimeSpan.FromSeconds( 1.0 ),  TimeSpan.FromSeconds( 1.0 ) )
		{
			m_MiniSpawn = spawn;
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick()
		{
			m_MiniSpawn.OnSlice();
		}
	}
}