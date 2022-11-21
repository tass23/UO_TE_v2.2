using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkPainSpikeSpell : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Force Wound", "Perfect strength is a demonstration of power for a Sith.",
		203,
		9031,
		Reagent.GraveDust,
		Reagent.PigIron,
		Reagent.DaemonBlood
		);
		
		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.5 ); } }
        public override double RequiredSkill { get { return 52; } }
        public override int RequiredMana { get { return 20; } }

		public DarkPainSpikeSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool CheckCast()
		{
			if ( Caster.Karma > 4999 )
			{
				Caster.SendMessage( "You lack the Sith power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else
			{
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return true;
			}
		}
		
		public override bool DelayedDamage{ get{ return false; } }

		public void Target( Mobile m )
		{
			if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				//SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m ); //Irrelevent asfter AoS

				/* Temporarily causes intense physical pain to the target, dealing direct damage.
				 * After 10 seconds the spell wears off, and if the target is still alive, 
				 * some of the Hit Points lost through Pain Spike are restored.
				 */

				m.FixedParticles( 0x37C4, 1, 8, 9916, 39, 3, EffectLayer.Head );
				m.FixedParticles( 0x37C4, 1, 8, 9502, 39, 4, EffectLayer.Head );
				m.PlaySound( 0x210 );

				double damage = ((GetDamageSkill( Caster ) - GetResistSkill( m )) / 5) + (m.Player ? 25 : 35);
				m.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );	//Skill check for gain

				if ( damage < 5 )
					damage = 5;

				TimeSpan buffTime = TimeSpan.FromSeconds( 10.0 );

				if( m_Table.Contains( m ) )
				{
					damage = Utility.RandomMinMax( 3, 7 );
					Timer t = m_Table[m] as Timer;

					if( t != null )
					{
						t.Delay += TimeSpan.FromSeconds( 2.0 );

						buffTime = t.Next - DateTime.Now;
					}
				}
				else
				{
					new InternalTimer( m, damage ).Start();
				}

				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.PainSpike, 1075667, buffTime, m, Convert.ToString( (int)damage ) ) );

				Misc.WeightOverloading.DFA = Misc.DFAlgorithm.PainSpike;
				m.Damage( (int) damage, Caster );
				SpellHelper.DoLeech( (int)damage, Caster, m );
				Misc.WeightOverloading.DFA = Misc.DFAlgorithm.Standard;

				//SpellHelper.Damage( this, m, damage, 100, 0, 0, 0, 0, Misc.DFAlgorithm.PainSpike );
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile;
			private int m_ToRestore;

			public InternalTimer( Mobile m, double toRestore ) : base( TimeSpan.FromSeconds( 10.0 ) )
			{
				Priority = TimerPriority.OneSecond;

				m_Mobile = m;
				m_ToRestore = (int)toRestore;

				m_Table[m] = this;
			}

			protected override void OnTick()
			{
				m_Table.Remove( m_Mobile );

				if ( m_Mobile.Alive && !m_Mobile.IsDeadBondedPet )
					m_Mobile.Hits += m_ToRestore;

				BuffInfo.RemoveBuff( m_Mobile, BuffIcon.PainSpike );
			}
		}

		private class InternalTarget : Target
		{
			private DarkPainSpikeSpell m_Owner;

			public InternalTarget( DarkPainSpikeSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}