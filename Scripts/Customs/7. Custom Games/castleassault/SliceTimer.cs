using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Engines.WarEvents
{
	public class ECSliceTimer : Timer
	{
		private EvilCastleSpawn spawn;

		public ECSliceTimer( EvilCastleSpawn ecspawn ) : base( TimeSpan.FromSeconds( 1.0 ),  TimeSpan.FromSeconds( 1.0 ) )
		{
			spawn = ecspawn;

			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick()
		{
			spawn.OnSlice();
		}
	}
}