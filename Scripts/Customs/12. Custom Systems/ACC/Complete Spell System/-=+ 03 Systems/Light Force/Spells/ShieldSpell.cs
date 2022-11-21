using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Spells;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells.Spellweaving;
using Server.Gumps;

//First Circle: 4
//Second Circle: 6
//Third Circle: 9
//Fourth Circle: 11
//Fifth Circle: 14
//Sixth Circle: 20
//Seventh Circle: 40
//Eight Circle: 50

namespace Server.ACC.CSS.Systems.LightForce
{
	public class ShieldSpell : LightForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Shield", "Defense is better than being reckless.",
		-1,
		9002,
		CReagent.DestroyingAngel,
		Reagent.Bloodmoss
		);
		
		public override SpellCircle Circle
        {
            get { return SpellCircle.Seventh; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
        public override double RequiredSkill { get { return 66; } }
        public override int RequiredMana { get { return 40; } }

		public ShieldSpell( Mobile caster, Item scroll )
			: base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast()
		{
			if ( Caster.Karma < 5000 )
			{
				Caster.SendMessage( "You lack the Jedi power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return false;
			}
			else
			{
				if( m_Table.ContainsKey( Caster ) )
				{
					Caster.SendLocalizedMessage( 501775 ); // This spell is already in effect.
					return false;
				}
				else if( !Caster.CanBeginAction( typeof( ShieldSpell ) ) )
				{
					Caster.SendLocalizedMessage( 1075124 ); // You must wait before casting that spell again.
					return false;
				}
				
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return base.CheckCast();
			}
		}

		public override void OnCast()
		{
			if( CheckSequence() )
			{
				Caster.PlaySound( 0x5BF );
				Caster.FixedParticles( 0x3728, 1, 13, 0x26B8, 0x455, 7, EffectLayer.Waist );
				Caster.FixedParticles( 0x3779, 1, 15, 0x251E, 0x3F, 7, EffectLayer.Waist );

				double skill = Caster.Skills[SkillName.Meditation].Value;

				int damageAbsorb = (int)(18 + ((skill-10)/10)*3 + (FocusLevel * 6));
				Caster.MeleeDamageAbsorb = damageAbsorb;

				TimeSpan duration = TimeSpan.FromSeconds( 60 + (FocusLevel * 12) );

				ExpireTimer t = new ExpireTimer( Caster, duration );
				t.Start();

				m_Table[Caster] = t;

				Caster.BeginAction( typeof( ShieldSpell ) );

				//BuffInfo.AddBuff( Caster, new BuffInfo( BuffIcon.AttuneWeapon, 1075798, duration, Caster, damageAbsorb.ToString() ) );
			}

			FinishSequence();
		}

		private static Dictionary<Mobile, ExpireTimer> m_Table = new Dictionary<Mobile, ExpireTimer>();

		public static void TryAbsorb( Mobile defender, ref int damage )
		{
			if( damage == 0 || !IsAbsorbing( defender ) || defender.MeleeDamageAbsorb <= 0 )
				return;

			int absorbed = Math.Min( damage, defender.MeleeDamageAbsorb );

			damage -= absorbed;
			defender.MeleeDamageAbsorb -= absorbed;

			defender.SendLocalizedMessage( 1075127, String.Format( "{0}\t{1}", absorbed, defender.MeleeDamageAbsorb ) ); // ~1_damage~ point(s) of damage have been absorbed. A total of ~2_remaining~ point(s) of shielding remain.

			if( defender.MeleeDamageAbsorb <= 0 )
				StopAbsorbing( defender, true );
		}

		public static bool IsAbsorbing( Mobile m )
		{
			return m_Table.ContainsKey( m );
		}

		public static void StopAbsorbing( Mobile m, bool message )
		{
			ExpireTimer t;
			if( m_Table.TryGetValue( m, out t ) )
			{
				t.DoExpire( message );
			}
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Mobile;

			public ExpireTimer( Mobile m, TimeSpan delay )
				: base( delay )
			{
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				DoExpire( true );
			}

			public void DoExpire( bool message )
			{
				Stop();

				m_Mobile.MeleeDamageAbsorb = 0;

				if( message )
				{
					m_Mobile.SendLocalizedMessage( 1075126 ); // Your attunement fades.
					m_Mobile.PlaySound( 0x1F8 );
				}

				m_Table.Remove( m_Mobile );

				Timer.DelayCall( TimeSpan.FromSeconds( 60 ), delegate { m_Mobile.EndAction( typeof( ShieldSpell ) ); } );
				BuffInfo.RemoveBuff( m_Mobile, BuffIcon.AttuneWeapon );
			}
		}
	}
}