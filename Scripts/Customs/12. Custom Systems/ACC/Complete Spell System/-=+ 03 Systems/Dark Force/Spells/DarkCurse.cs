using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class Debilitate : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
			"Debilitate", "Let the Dark Side overpower you...",
			212,
			9041,
			Reagent.NoxCrystal,
			Reagent.DaemonBlood
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }
		
        public override double CastDelay { get { return 1.5; } }
        public override double RequiredSkill { get { return 85; } }
        public override int RequiredMana { get { return 0; } }

		public Debilitate( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		private static Hashtable m_UnderEffect = new Hashtable();

		public static void RemoveEffect( object state )
		{
			Mobile m = (Mobile)state;

			m_UnderEffect.Remove( m );

			m.UpdateResistances();
		}

		public static bool UnderEffect( Mobile m )
		{
			return m_UnderEffect.Contains( m );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				SpellHelper.AddStatCurse( Caster, m, StatType.Str ); SpellHelper.DisableSkillCheck = true;
				SpellHelper.AddStatCurse( Caster, m, StatType.Dex );
				SpellHelper.AddStatCurse( Caster, m, StatType.Int ); SpellHelper.DisableSkillCheck = false;

				Timer t = (Timer)m_UnderEffect[m];

				if ( Caster.Player && m.Player /*&& Caster != m */ && t == null )	//On OSI you CAN curse yourself and get this effect.
				{
					TimeSpan duration = SpellHelper.GetDuration( Caster, m );
					m_UnderEffect[m] = t = Timer.DelayCall( duration, new TimerStateCallback( RemoveEffect ), m );
					m.UpdateResistances();
				}

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;
				m.Sleep = false; //SA Mysticism Edit

				m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
				m.PlaySound( 0x1E1 );

				int percentage = (int)(SpellHelper.GetOffsetScalar(Caster, m, true) * 100);
				TimeSpan length = SpellHelper.GetDuration(Caster, m);

				string args = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", percentage, percentage, percentage, 10, 10, 10, 10);

				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Curse, 1075835, 1075836, length, m, args.ToString()));
				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Weaken, 1075837, length, m, percentage.ToString()));

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private Debilitate m_Owner;

			public InternalTarget( Debilitate owner ) : base( Core.ML? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}