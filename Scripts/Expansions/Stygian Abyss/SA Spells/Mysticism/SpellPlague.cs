using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Mystic
{
	public class SpellPlagueSpell : MysticSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Spell Plague", "Vas Rel Jux Ort",
				230,
				9022,
				Reagent.DaemonBone,
				Reagent.DragonBlood,
				Reagent.Nightshade,
				Reagent.SulfurousAsh
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.5 ); } }

		public override double RequiredSkill{ get{ return 70.0; } }
		public override int RequiredMana{ get{ return 40; } }

		public SpellPlagueSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( Caster == m || !(m is PlayerMobile || m is BaseCreature) ) // only PlayerMobile and BaseCreature implement blood oath checking
			{
				Caster.SendLocalizedMessage( 1080194 ); // You can't curse that.
			}
			else if ( m_PlagueTable.Contains( Caster ) )
			{
				Caster.SendLocalizedMessage( 1061607 ); // You are already bonded in a Blood Oath.
			}
			else if ( m_PlagueTable.Contains( m ) )
			{
				if ( m.Player )
					Caster.SendLocalizedMessage( 1080195 ); // That player is already bonded in a Blood Oath.
				else
					Caster.SendLocalizedMessage( 1080195 ); // That creature is already bonded in a Blood Oath.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				/* Temporarily creates a dark pact between the caster and the target.
				 * Any damage dealt by the target to the caster is increased, but the target receives the same amount of damage.
				 * The effect lasts for ((Spirit Speak skill level - target's Resist Magic skill level) / 80 ) + 8 seconds.
				 * 
				 * NOTE: The above algorithm must be fixed point, it should be:
				 * ((ss-rm)/8)+8
				 */

				ExpireTimer timer = (ExpireTimer)m_Table[m];
				if ( timer != null )
				timer.DoExpire();

				m_PlagueTable[Caster] = Caster;
				m_PlagueTable[m] = Caster;

				Caster.PlaySound( 0x659 );

				Caster.FixedParticles( 0x375A, 1, 17, 9919, 1161, 7, EffectLayer.Waist );
				Caster.FixedParticles( 0x3728, 1, 13, 9502, 1161, 7, (EffectLayer)255 );

				m.FixedParticles( 0x375A, 1, 17, 9919, 1161, 7, EffectLayer.Waist );
				m.FixedParticles( 0x3728, 1, 13, 9502, 1161, 7, (EffectLayer)255 );

				TimeSpan duration = TimeSpan.FromSeconds( ((GetDamageSkill( Caster ) - GetResistSkill( m )) / 8) + 8 );
				m.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );	//Skill check for gain

				timer = new ExpireTimer ( Caster, m, duration );
				timer.Start ();

				BuffInfo.AddBuff ( Caster, new BuffInfo ( BuffIcon.SpellPlague, 1075659, duration, Caster, m.Name.ToString () ) );
				BuffInfo.AddBuff ( m, new BuffInfo ( BuffIcon.SpellPlague, 1075661, duration, m, Caster.Name.ToString () ) );

				m_Table[m] = timer;

			}

			FinishSequence();
		}

			public static bool RemoveCurse( Mobile m )
			{
			ExpireTimer t = (ExpireTimer)m_Table[m];

				if ( t == null )
					return false;

			t.DoExpire();
			return true;
		}

		private static Hashtable m_PlagueTable = new Hashtable();
		private static Hashtable m_Table = new Hashtable ();

		public static Mobile GetSpellPlague( Mobile m )
		{
			if ( m == null )
				return null;

			Mobile plague = (Mobile)m_PlagueTable[m];

			if ( plague == m )
				plague = null;

			return plague;
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Caster;
			private Mobile m_Target;
			private DateTime m_End;

			public ExpireTimer( Mobile caster, Mobile target, TimeSpan delay ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Caster = caster;
				m_Target = target;
				m_End = DateTime.Now + delay;

				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Caster.Deleted || m_Target.Deleted || !m_Caster.Alive || !m_Target.Alive || DateTime.Now >= m_End )
				{
					DoExpire ();
				}
			}
			public void DoExpire()
			{
				if( m_PlagueTable.Contains( m_Caster ) )
				{
					m_Caster.SendLocalizedMessage( 1080199 ); // Your Blood Oath has been broken.
					m_PlagueTable.Remove( m_Caster );
				}

				if( m_PlagueTable.Contains( m_Target ) )
				{
					m_Target.SendLocalizedMessage( 1080199 ); // Your Blood Oath has been broken.
					m_PlagueTable.Remove( m_Target );
				}

					Stop();

				BuffInfo.RemoveBuff ( m_Caster, BuffIcon.SpellPlague );
				BuffInfo.RemoveBuff ( m_Target, BuffIcon.SpellPlague );

				m_Table.Remove ( m_Caster );
			}
		}

		private class InternalTarget : Target
		{
			private SpellPlagueSpell m_Owner;

			public InternalTarget( SpellPlagueSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
				else
					from.SendLocalizedMessage( 1080194 ); // You can't curse that.
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}