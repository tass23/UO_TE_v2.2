using System;
using Server;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DrainSpell : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Drain", "Through strength, I gain power.",
		221,
		9032,
		Reagent.GraveDust,
		Reagent.BatWing,
		Reagent.DaemonBlood,
		Reagent.NoxCrystal
		);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }
		
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
        public override double RequiredSkill { get { return 52; } }
        public override int RequiredMana { get { return 20; } }

		public DrainSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

				int toMDrain = 0;
				int toHDrain = 0;

				if ( Core.AOS )
				{
					toMDrain = (int)(GetDamageSkill( Caster ) - GetResistSkill( m ));
					toHDrain = (int)(GetDamageSkill( Caster ) - GetResistSkill( m ));

					if ( !m.Player )
					{
						toMDrain /= 2;
						toHDrain /= 2;
					}

					if ( toMDrain < 0 )
					{
						toMDrain = 0;
					}
					
					if ( toHDrain < 0 )
					{
						toHDrain = 0;
					}
					else if ( toHDrain > m.Hits )
					{
						toHDrain = m.Hits;
					}
					else if ( toMDrain > m.Mana )
					{
						toMDrain = m.Mana;
					}
				}
				else
				{
					if ( CheckResisted( m ) )
						m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					else
						toMDrain = m.Mana;
						toHDrain = m.Hits;
				}
				if ( toHDrain > (Caster.HitsMax - Caster.Hits) )
					toHDrain = Caster.HitsMax - Caster.Hits;
				if ( toMDrain > (Caster.ManaMax - Caster.Mana) )
					toMDrain = Caster.ManaMax - Caster.Mana;

				m.Mana -= toMDrain;
				m.Hits -= toHDrain;
				Caster.Mana += toMDrain;
				Caster.Hits += toHDrain;

				if ( Core.AOS )
				{
					m.FixedParticles( 0x374A, 1, 15, 5054, 23, 7, EffectLayer.Head );
					m.PlaySound( 0x1F9 );

					Caster.FixedParticles( 0x0000, 10, 5, 2054, EffectLayer.Head );
				}
				else
				{
					m.FixedParticles( 0x374A, 10, 15, 5054, EffectLayer.Head );
					m.PlaySound( 0x1F9 );
				}
			}

			FinishSequence();
		}

		public override double GetResistPercent( Mobile target )
		{
			return 98.0;
		}

		private class InternalTarget : Target
		{
			private DrainSpell m_Owner;

			public InternalTarget( DrainSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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