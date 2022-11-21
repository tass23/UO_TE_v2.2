using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Engines.SAMiniChamps
{
	public class MiniRandomizeTimer : Timer
	{
		private MiniChampSpawnController m_Controller;

		public MiniRandomizeTimer( MiniChampSpawnController controller, TimeSpan delay ) : base( delay )
		{
			m_Controller = controller;
			Priority = TimerPriority.FiveSeconds;
		}

		protected override void OnTick()
		{
			m_Controller.Slice();
		}
	}
}