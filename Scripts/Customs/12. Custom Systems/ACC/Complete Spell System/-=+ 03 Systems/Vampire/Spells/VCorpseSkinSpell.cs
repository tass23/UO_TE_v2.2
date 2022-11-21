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
using System.Collections;

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
    public class VCorpseSkinSpell : VampireSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Harden Flesh", "harden carne",
        //SpellCircle.Fourth,
        212,
        9041,
        Reagent.MandrakeRoot,
        Reagent.BlackPearl
        );

		public override SpellCircle Circle
        {
            get { return SpellCircle.Second; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 43; } }
        public override int RequiredMana { get { return 6; } }
		
		public VCorpseSkinSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
					m.SendMessage( "Your skin hardens, raising some of your resistances." ); // Your skin turns dry and corpselike.

				 if ( m.Spell != null )
					m.Spell.OnCasterHurt();
				m.FixedParticles( 0x37CC, 1, 15, 9913, 1580, 7, EffectLayer.Waist );
				m.PlaySound( 0x1E5 );

				double ss = GetDamageSkill( Caster );
				double mr = ( Caster == m ? 0.0 : GetResistSkill( m ) );
				m.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );	//Skill check for gain

				TimeSpan duration = TimeSpan.FromSeconds( ((ss - mr) / 2.5) + 40.0 );

				ResistanceMod[] mods = new ResistanceMod[5]
					{
						new ResistanceMod( ResistanceType.Fire, +5 ),
						new ResistanceMod( ResistanceType.Poison, -10 ),
						new ResistanceMod( ResistanceType.Cold, +5 ),
						new ResistanceMod( ResistanceType.Energy, -1 ),
						new ResistanceMod( ResistanceType.Physical, +5 )
					};

				timer = new ExpireTimer( m, mods, duration );
				timer.Start();
				
				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.CorpseSkin, 1122598, duration, m ) );

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
			}

			protected override void OnTick()
			{
				m_Mobile.SendLocalizedMessage( 1061688 ); // Your skin returns to normal.
				DoExpire();
			}
		}

		private class InternalTarget : Target
		{
			private VCorpseSkinSpell m_Owner;

			public InternalTarget( VCorpseSkinSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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