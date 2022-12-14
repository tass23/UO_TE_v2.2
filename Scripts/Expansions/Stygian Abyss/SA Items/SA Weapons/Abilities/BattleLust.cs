using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server
{
	public class BattleLust
	{
		private static Dictionary<Mobile, BattleLustTimer> m_Table = new Dictionary<Mobile, BattleLustTimer>();

		public static bool UnderBattleLust( Mobile m )
		{
			return m_Table.ContainsKey( m );
		}

		public static int GetBonus( Mobile attacker, Mobile defender )
		{
			if ( !m_Table.ContainsKey( attacker ) )
				return 0;

			int bonus = m_Table[attacker].Bonus * attacker.Aggressed.Count;

			if ( defender is PlayerMobile && bonus > 45 )
				bonus = 45;
			else if ( bonus > 90 )
				bonus = 90;

			return bonus;
		}

		public static void IncreaseBattleLust( Mobile m, int damage )
		{
			if ( damage < 30 )
				return;
			else if ( AosWeaponAttributes.GetValue( m, AosWeaponAttribute.BattleLust ) == 0 )
				return;
			else if ( m_Table.ContainsKey( m ) )
			{
				if ( m_Table[m].CanGain )
				{
					if ( m_Table[m].Bonus < 16 )
						m_Table[m].Bonus++;

					m_Table[m].CanGain = false;
				}
			}
			else
			{
				BattleLustTimer blt = new BattleLustTimer( m, 1 );
				blt.Start();
				m_Table.Add( m, blt );
				m.SendLocalizedMessage( 1113748 ); // The damage you received fuels your battle fury.
			}
		}

		public static bool DecreaseBattleLust( Mobile m )
		{
			if ( m_Table.ContainsKey( m ) )
			{
				m_Table[m].Bonus--;

				if ( m_Table[m].Bonus <= 0 )
				{
					m_Table.Remove( m );

					// No Message?
					//m.SendLocalizedMessage( 0 ); //

					return false;
				}
			}

			return true;
		}

		public class BattleLustTimer : Timer
		{
			private Mobile m_Mobile;
			public int Bonus;
			public bool CanGain;
			private int m_Count;

			public BattleLustTimer( Mobile m, int bonus ) : base( TimeSpan.FromSeconds( 2.0 ), TimeSpan.FromSeconds( 2.0 ) )
			{
				m_Mobile = m;
				Bonus = bonus;
				m_Count = 1;
			}

			protected override void OnTick()
			{
				m_Count %= 3;

				if ( m_Count == 0 )
				{
					if ( !DecreaseBattleLust( m_Mobile ) )
						Stop();
				}
				else
				{
					CanGain = true;
				}

				m_Count++;
			}
		}
	}
}