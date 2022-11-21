// SkittishAnimalAI.cs by Alari (alarihyena@gmail.com)
// changed so that tamed skittish animals don't run away immediately
using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

// Ideas
// When you run on animals the panic
// When if ( distance < 8 && Utility.RandomDouble() * Math.Sqrt( (8 - distance) / 6 ) >= incoming.Skills[SkillName.AnimalTaming].Value )
// More your close, the more it can panic
/*
 * AnimalHunterAI, AnimalHidingAI, AnimalDomesticAI...
 * 
 */ 

namespace Server.Mobiles
{
	public class AnimalSkittishAI : BaseAI
	{
		public AnimalSkittishAI(BaseCreature m) : base (m)
		{
		}

		public override bool DoActionWander()
		{
/*
no idea what to do with this...
distance < 8 && Utility.RandomDouble() * Math.Sqrt( (8 - distance) / 6 ) >=
 incoming.Skills[SkillName.AnimalTaming].Value )

from BaseAI.html:  virtual bool AcquireFocusMob( int iRange, FightMode acqType, bool bPlayerOnly, bool bFacFriend, bool bFacFoe )
 "bFacFriend" means 'faction' and "bFacFoe" means 'non-faction'

... the devs misspelled "acquire", too... ^.^;
*/
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
/*
// this section doesn't work as expected
			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, true, true ) )
			{
				Action = ActionType.Flee;  // Backoff wasn't working quite as well
			}
			else*/ if ( !m_Mobile.Summoned && !m_Mobile.Controlled && hitPercent < 0.1 ) // Less than 10% health
			{
				m_Mobile.DebugSay( "I am low on health!" );
				Action = ActionType.Flee;  // i dunno if there's a "flee faster" =D
			}
			else if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{  // if the mobile initiates combat, flee...
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				
				// forgot to take into account controlled mobiles
				if ( m_Mobile.Controlled )
					Action = ActionType.Combat;
				else
					Action = ActionType.Flee;  // was Combat
			}
			else
			{
				base.DoActionWander();
			}

			return true;
		}

		public override bool DoActionCombat()
		{
			Mobile combatant = m_Mobile.Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != m_Mobile.Map )
			{
				m_Mobile.DebugSay( "My combatant is gone.." );

				Action = ActionType.Wander;

				return true;
			}

			if ( WalkMobileRange( combatant, 1, true, m_Mobile.RangeFight, m_Mobile.RangeFight ) )
			{
				m_Mobile.Direction = m_Mobile.GetDirectionTo( combatant );
			}
			else
			{
				if ( m_Mobile.GetDistanceToSqrt( combatant ) > m_Mobile.RangePerception + 1 )
				{
					if ( m_Mobile.Debug )
						m_Mobile.DebugSay( "I cannot find {0}", combatant.Name );

					Action = ActionType.Wander;

					return true;
				}
				else
				{
					if ( m_Mobile.Debug )
						m_Mobile.DebugSay( "I should be closer to {0}", combatant.Name );
				}
			}

			if ( !m_Mobile.Controlled && !m_Mobile.Summoned )
			{
				double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;

				if ( hitPercent < 0.1 )
				{
					m_Mobile.DebugSay( "I am low on health!" );
					Action = ActionType.Flee;
				}
			}

			return true;
		}

		public override bool DoActionBackoff()
		{
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;

			if ( !m_Mobile.Summoned && !m_Mobile.Controlled && hitPercent < 0.1 ) // Less than 10% health
			{
				Action = ActionType.Flee;
			}
			else
			{
				if (AcquireFocusMob(m_Mobile.RangePerception * 2, FightMode.Closest, true, false , true))
				{
					if ( WalkMobileRange(m_Mobile.FocusMob, 1, false, m_Mobile.RangePerception, m_Mobile.RangePerception * 2) )
					{
						m_Mobile.DebugSay( "Well, here I am safe" );
						Action = ActionType.Wander;
					}					
				}
				else
				{
					m_Mobile.DebugSay( "I have lost my focus, lets relax" );
					Action = ActionType.Wander;
				}
			}

			return true;
		}

		public override bool DoActionFlee()
		{
			AcquireFocusMob(m_Mobile.RangePerception * 2, m_Mobile.FightMode, true, false, true);

			if ( m_Mobile.FocusMob == null )
				m_Mobile.FocusMob = m_Mobile.Combatant;

			return base.DoActionFlee();
		}
	}
}