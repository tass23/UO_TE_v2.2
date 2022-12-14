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
    public class VPsychicSpell : VampireSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Psychic Link", "psihic",
        //SpellCircle.Fourth,
        212,
        9041,
        Reagent.MandrakeRoot,
        Reagent.BlackPearl
        );

		public override SpellCircle Circle
        {
            get { return SpellCircle.Fifth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
        public override double RequiredSkill { get { return 52; } }
        public override int RequiredMana { get { return 14; } }

        private static Dictionary<Mobile, SkillMod> m_Table = new Dictionary<Mobile, SkillMod>();
		
		public VPsychicSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
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

		private void AosDelay_Callback( object state )
		{
			object[] states = (object[])state;
			Mobile caster = (Mobile)states[0];
			Mobile target = (Mobile)states[1];
			Mobile defender = (Mobile)states[2];
			int damage = (int)states[3];

			if ( caster.HarmfulCheck( defender ) )
			{
				SpellHelper.Damage( this, target, Utility.RandomMinMax( damage, damage + 4 ), 0, 0, 100, 0, 0 );
				
				target.FixedParticles( 0x2AE5, 10, 10, 5031, 1172, 0, EffectLayer.Head );
				target.PlaySound( 0x1E8 );
			}
		}

		public override bool DelayedDamage{ get{ return !Core.AOS; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( Core.AOS )
			{
				if ( Caster.CanBeHarmful( m ) && CheckSequence() )
				{
					Mobile from = Caster, target = m;

					SpellHelper.Turn( from, target );

					SpellHelper.CheckReflect( (int)this.Circle, ref from, ref target );

					int damage = (int)((Caster.Skills[SkillName.Focus].Value + Caster.Int) / 5);

					if ( damage > 60 )
						damage = 60;

					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ),
						new TimerStateCallback( AosDelay_Callback ),
						new object[]{ Caster, target, m, damage } );
				}
			}
			else if ( CheckHSequence( m ) )
			{
				Mobile from = Caster, target = m;

				SpellHelper.Turn( from, target );

				SpellHelper.CheckReflect( (int)this.Circle, ref from, ref target );

				// Algorithm: (highestStat - lowestStat) / 2 [- 50% if resisted]

				int highestStat = target.Str, lowestStat = target.Str;

				if ( target.Dex > highestStat )
					highestStat = target.Dex;

				if ( target.Dex < lowestStat )
					lowestStat = target.Dex;

				if ( target.Int > highestStat )
					highestStat = target.Int;

				if ( target.Int < lowestStat )
					lowestStat = target.Int;

				if ( highestStat > 150 )
					highestStat = 150;

				if ( lowestStat > 150 ) 
					lowestStat = 150;

				double damage = GetDamageScalar(m)*(highestStat - lowestStat) / 4;//less damage

				if ( damage > 45 )
					damage = 45;

				if ( CheckResisted( target ) )
				{
					damage /= 2;
					target.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
				}

				from.FixedParticles( 0x374A, 10, 15, 2038, EffectLayer.Head );

				target.FixedParticles( 0x374A, 10, 15, 5038, EffectLayer.Head );
				target.PlaySound( 0x213 );

				SpellHelper.Damage( this, target, damage, 0, 0, 100, 0, 0 );
			}

			FinishSequence();
		}

		public override double GetSlayerDamageScalar( Mobile target )
		{
			return 1.0; //This spell isn't affected by slayer spellbooks
		}

		private class InternalTarget : Target
		{
			private VPsychicSpell m_Owner;

			public InternalTarget( VPsychicSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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