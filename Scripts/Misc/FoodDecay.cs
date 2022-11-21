using System;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	public class FoodDecayTimer : Timer
	{
		public static void Initialize()
		{
			new FoodDecayTimer().Start();
		}

		public FoodDecayTimer() : base( TimeSpan.FromMinutes( 5 ), TimeSpan.FromMinutes( 5 ) )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			FoodDecay();			
		}

		public static void FoodDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HungerDecay( state.Mobile );
				ThirstDecay( state.Mobile );
			}
		}

		public static void HungerDecay( Mobile m )
		{
			if ( m != null && m.Hunger >= 1 )
				m.Hunger -= 1;
//Vampire Adds
			if (m is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) m;
				if (pm.Vampire > 0)
				{
					pm.Hunger = 10;
				}
			}
			
			if (m is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) m;
				if (pm.Werewolf > 0)
				{
					pm.Hunger = 10;
				}
			}
//Vampire Adds
		}

		public static void ThirstDecay( Mobile m )
		{
			if ( m != null && m.Thirst >= 1 )
				m.Thirst -= 1;
//Vampire Adds
			if (m is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) m;
				if (pm.Vampire > 0)
				{
					pm.Thirst = 10;
				}
			}
			
			if (m is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) m;
				if (pm.Werewolf > 0)
				{
					pm.Thirst = 10;
				}
			}
//Vampire Adds
		}
	}
}