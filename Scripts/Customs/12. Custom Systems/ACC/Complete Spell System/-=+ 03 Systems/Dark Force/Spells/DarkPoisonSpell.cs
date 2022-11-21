using System;
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
	public class DarkPoisonSpell : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Poison", "Power is what the Dark Side truly is all about.",
		203,
		9051,
		Reagent.DaemonBlood,
		Reagent.NoxCrystal
		);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }
		
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
        public override double RequiredSkill { get { return 52; } }
        public override int RequiredMana { get { return 20; } }

		public DarkPoisonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;
				m.Sleep = false; //SA Mysticism Edit

				if ( CheckResisted( m ) )
				{
					m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
				}
				else
				{
					int level;

					if ( Core.AOS )
					{
						if ( Caster.InRange( m, 2 ) )
						{
							int total = (Caster.Skills.Meditation.Fixed + Caster.Skills.Poisoning.Fixed) / 2;

							if ( total >= 1000 )
								level = 3;
							else if ( total > 850 )
								level = 2;
							else if ( total > 650 )
								level = 1;
							else
								level = 0;
						}
						else
						{
							level = 0;
						}
					}
					else
					{
						double total = Caster.Skills[SkillName.Meditation].Value + Caster.Skills[SkillName.Poisoning].Value;

						double dist = Caster.GetDistanceToSqrt( m );

						if ( dist >= 3.0 )
							total -= (dist - 3.0) * 10.0;

						if ( total >= 200.0 && 1 > Utility.Random( 10 ) )
							level = 3;
						else if ( total > (Core.AOS ? 170.1 : 170.0) )
							level = 2;
						else if ( total > (Core.AOS ? 130.1 : 130.0) )
							level = 1;
						else
							level = 0;
					}

					m.ApplyPoison( Caster, Poison.GetPoison( level ) );
				}

				m.FixedParticles( 0x374A, 10, 15, 5021, EffectLayer.Waist );
				m.PlaySound( 0x205 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private DarkPoisonSpell m_Owner;

			public InternalTarget( DarkPoisonSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}