using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.DMChamps;

namespace Server.Engines.DMChamps
{
	public class DMSliceTimer : Timer
	{
		private DMChampSpawn m_DMSpawn;

		public DMSliceTimer( DMChampSpawn spawn ) : base( TimeSpan.FromSeconds( 1.0 ),  TimeSpan.FromSeconds( 1.0 ) )
		{
			m_DMSpawn = spawn;
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick()
		{
			m_DMSpawn.OnSlice();
		}
	}
}