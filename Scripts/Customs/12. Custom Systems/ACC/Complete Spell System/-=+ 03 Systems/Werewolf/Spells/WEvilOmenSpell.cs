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
	public class WEvilOmenSpell : WerewolfSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Dreadful Howl", "*eerily howls*",
		//SpellCircle.Sixth,
		206,
		9002,
		false,
		Reagent.Bloodmoss,
		Reagent.Nightshade
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Third; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 24; } }
        public override int RequiredMana { get { return 11; } }
		
		public WEvilOmenSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			if ( !(m is BaseCreature || m is PlayerMobile) )
			{
				Caster.SendMessage( "That is immune to your howl." ); // You can't curse that.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				/* Curses the target so that the next harmful event that affects them is magnified.
				 * Damage to the target's hit points is increased 25%,
				 * the poison level of the attack will be 1 higher
				 * and the Resist Magic skill of the target will be fixed on 50.
				 * 
				 * The effect lasts for one harmful event only.
				 */

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.PlaySound( 0x52C );
				m.FixedParticles( 0x3798, 1, 13, 9912, 1150, 7, EffectLayer.Head );
				m.FixedParticles( 0x37BE, 1, 15, 9502, 67, 7, EffectLayer.Waist );

				if ( !m_Table.Contains( m ) )
				{
					SkillMod mod = new DefaultSkillMod( SkillName.MagicResist, false, 50.0 );

					if ( m.Skills[SkillName.MagicResist].Base > 50.0 )
						m.AddSkillMod( mod );

					m_Table[m] = mod;
				}

				TimeSpan duration = TimeSpan.FromSeconds( (Caster.Skills[SkillName.AnimalLore].Value / 12) + 1.0 );

				Timer.DelayCall( duration, new TimerStateCallback( EffectExpire_Callback ), m );

				HarmfulSpell( m );
				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.EvilOmen, 1122606, 1122607, duration, m));

			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		private static void EffectExpire_Callback( object state )
		{
			TryEndEffect((Mobile)state);
		}

		public static bool TryEndEffect(Mobile m)
		{
			SkillMod mod = (SkillMod)m_Table[m];

			if ( mod == null )
				return false;

			m_Table.Remove( m );
			mod.Remove();

			return true;
		}

		private class InternalTarget : Target
		{
			private WEvilOmenSpell m_Owner;

			public InternalTarget(WEvilOmenSpell owner)
				: base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
				else
					from.SendMessage( "That is immune to your howl." ); // You can't curse that.
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}