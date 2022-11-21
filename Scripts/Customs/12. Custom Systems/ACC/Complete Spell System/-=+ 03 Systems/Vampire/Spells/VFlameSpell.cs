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
using Server.Misc;

//First Circle: 4
//Second Circle: 6
//Third Circle: 9
//Fourth Circle: 11
//Fifth Circle: 14
//Sixth Circle: 20
//Seventh Circle: 40
//Eight Circle: 50

namespace Server.ACC.CSS.Systems.Vampire
{
    public class VFlameSpell : VampireSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Blood Drain", "scurgere sange",
        //SpellCircle.Fourth,
        212,
        9041,
        Reagent.MandrakeRoot,
        Reagent.BlackPearl
        );

		public override SpellCircle Circle
        {
            get { return SpellCircle.Seventh; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 85; } }
        public override int RequiredMana { get { return 40; } }

        private static Dictionary<Mobile, SkillMod> m_Table = new Dictionary<Mobile, SkillMod>();
		
		public VFlameSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override bool CheckCast()
		{
			PlayerMobile pm = (PlayerMobile) Caster;
			if ( pm.Vampire == 0 )
			{
				Caster.SendMessage( "Only a vampire may attempt something like this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			
			else
			{
				Caster.CloseGump( typeof( VampireGump ) );
				Caster.SendGump( new VampireGump() );
				return true;
			}
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

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

				double damage;

				if ( Core.AOS )
				{
					damage = GetNewAosDamage( 48, 1, 5, m );
				}
				else
				{
					damage = Utility.Random( 27, 22 );

					if ( CheckResisted( m ) )
					{
						damage *= 0.6;

						m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					}

					damage *= GetDamageScalar( m );
				}
				//int itemID, int speed, int duration, int hue, int renderMode, int effect, EffectLayer layer, int unknown
				m.FixedParticles( 0x1249, 10, 30, 5051, 33, 0, EffectLayer.Waist );
				m.PlaySound( 0x1F1 );

				SpellHelper.Damage( this, m, damage, 0, 100, 0, 0, 0 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private VFlameSpell m_Owner;

			public InternalTarget( VFlameSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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