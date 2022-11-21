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
	public class WCorpseSkinSpell : WerewolfSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Toughen Skin", "*barks*",
		//SpellCircle.Sixth,
		206,
		9002,
		false,
		Reagent.Bloodmoss,
		Reagent.Nightshade
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Fourth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 24; } }
        public override int RequiredMana { get { return 11; } }
		
		public WCorpseSkinSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				/* Transmogrifies the flesh of the target creature or player to resemble rotted corpse flesh,
				 * making them more vulnerable to Fire and Poison damage,
				 * but increasing their resistance to Physical and Cold damage.
				 * 
				 * The effect lasts for ((Spirit Speak skill level - target's Resist Magic skill level) / 25 ) + 40 seconds.
				 * 
				 * NOTE: Algorithm above is fixed point, should be:
				 * ((ss-mr)/2.5) + 40
				 * 
				 * NOTE: Resistance is not checked if targeting yourself
				 */

				ExpireTimer timer = (ExpireTimer)m_Table[m];

				if ( timer != null )
					timer.DoExpire();
				else
					m.SendMessage( "Your skin hardens." ); // Your skin turns dry and corpselike.

				 if ( m.Spell != null )
					m.Spell.OnCasterHurt();
				m.FixedParticles( 0x37CC, 1, 15, 9913, 67, 7, EffectLayer.Waist );
				m.PlaySound( 0x087 );

				double ss = GetDamageSkill( Caster );
				double mr = ( Caster == m ? 0.0 : GetResistSkill( m ) );
				m.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );	//Skill check for gain

				TimeSpan duration = TimeSpan.FromSeconds( ((ss - mr) / 2.5) + 40.0 );

				ResistanceMod[] mods = new ResistanceMod[5]
					{
						new ResistanceMod( ResistanceType.Fire, +10 ),
						new ResistanceMod( ResistanceType.Poison, -15 ),
						new ResistanceMod( ResistanceType.Cold, +10 ),
						new ResistanceMod( ResistanceType.Physical, +10 ),
						new ResistanceMod( ResistanceType.Energy, -15 )
					};

				timer = new ExpireTimer( m, mods, duration );
				timer.Start();
				
				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.CorpseSkin, 1122602, duration, m ) );

				m_Table[m] = timer;

				for ( int i = 0; i < mods.Length; ++i )
					m.AddResistanceMod( mods[i] );

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool RemoveCurse( Mobile m )
		{
			ExpireTimer t = (ExpireTimer)m_Table[m];

			if ( t == null )
				return false;

			m.SendLocalizedMessage( 1061688 ); // Your skin returns to normal.
			t.DoExpire();
			return true;
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Mobile;
			private ResistanceMod[] m_Mods;

			public ExpireTimer( Mobile m, ResistanceMod[] mods, TimeSpan delay ) : base( delay )
			{
				m_Mobile = m;
				m_Mods = mods;
			}

			public void DoExpire()
			{
				for ( int i = 0; i < m_Mods.Length; ++i )
					m_Mobile.RemoveResistanceMod( m_Mods[i] );

				Stop();
				BuffInfo.RemoveBuff( m_Mobile, BuffIcon.CorpseSkin );
				m_Table.Remove( m_Mobile );
				m_Mobile.PlaySound( 0x088 );
			}

			protected override void OnTick()
			{
				m_Mobile.SendLocalizedMessage( 1061688 ); // Your skin returns to normal.
				DoExpire();
			}
		}

		private class InternalTarget : Target
		{
			private WCorpseSkinSpell m_Owner;

			public InternalTarget( WCorpseSkinSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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