using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using System.Collections;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Gumps;

//First Circle: 4
//Second Circle: 6
//Third Circle: 9
//Fourth Circle: 11
//Fifth Circle: 14
//Sixth Circle: 20
//Seventh Circle: 40
//Eight Circle: 50

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WWeakenSpell : WerewolfSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Deafening Howl", "*howls*",
		//SpellCircle.Sixth,
		206,
		9002,
		false,
		Reagent.Bloodmoss,
		Reagent.Nightshade
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Second; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 24; } }
        public override int RequiredMana { get { return 11; } }
		
		public WWeakenSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
		
		public override bool CheckCast()
		{
			PlayerMobile pm = (PlayerMobile) Caster;
			if ( pm.Werewolf == 0 )
			{
				Caster.SendMessage( "Only a werewolf may attempt something like this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			if ( pm.Werewolf == 1 )
			{
				if ( pm.BodyMod != 0x2CF )
				{
					Caster.SendMessage( "You must be in Werewolf form to use this ability." );
					return false;
				}
				else
				{
					return true;
				}
			}				
			else
			{
				Caster.CloseGump( typeof( WerewolfGump ) );
				Caster.SendGump( new WerewolfGump() );
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

				SpellHelper.AddStatCurse( Caster, m, StatType.Str );

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;
				m.Sleep = false; //SA Mysticism Edit

				m.FixedParticles( 0x42CF, 10, 15, 5009, EffectLayer.Waist );
				m.PlaySound( 0x0E6 );

				int percentage = (int)(SpellHelper.GetOffsetScalar( Caster, m, true )*100);
				TimeSpan length = SpellHelper.GetDuration( Caster, m );
				
				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Weaken, 1122610, length, m, percentage.ToString() ) );

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private WWeakenSpell m_Owner;

			public InternalTarget( WWeakenSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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