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
	public class DarkCurseWeaponSpell : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Curse Weapon", "Allow me to show you the power of the Dark Side.",
		203,
		9031,
		Reagent.BatWing,
		Reagent.NoxCrystal
		);
		
		public override SpellCircle Circle { get { return SpellCircle.Third; } }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 10; } }
        public override int RequiredMana { get { return 9; } }

		public DarkCurseWeaponSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			BaseWeapon weapon = Caster.Weapon as BaseWeapon;

			if ( weapon == null || weapon is Fists )
			{
				Caster.SendLocalizedMessage( 501078 ); // You must be holding a weapon.
			}
			else if ( CheckSequence() )
			{
				/* Temporarily imbues a weapon with a life draining effect.
				 * Half the damage that the weapon inflicts is added to the necromancer's health.
				 * The effects lasts for (Spirit Speak skill level / 34) + 1 seconds.
				 * 
				 * NOTE: Above algorithm is fixed point, should be :
				 * (Spirit Speak skill level / 3.4) + 1
				 * 
				 * TODO: What happens if you curse a weapon then give it to someone else? Should they get the drain effect?
				 */

				Caster.PlaySound( 0x387 );
				Caster.FixedParticles( 0x3779, 1, 15, 9905, 32, 2, EffectLayer.Head );
				Caster.FixedParticles( 0x37B9, 1, 14, 9502, 32, 5, (EffectLayer)255 );
				new SoundEffectTimer( Caster ).Start();

				TimeSpan duration = TimeSpan.FromSeconds( (Caster.Skills[SkillName.Focus].Value / 3.4) + 1.0 );


				Timer t = (Timer)m_Table[weapon];

				if ( t != null )
					t.Stop();

				weapon.Cursed = true;

				m_Table[weapon] = t = new ExpireTimer( weapon, duration );

				t.Start();
			}

			FinishSequence();
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
		
		private static Hashtable m_Table = new Hashtable();

		private class ExpireTimer : Timer
		{
			private BaseWeapon m_Weapon;

			public ExpireTimer( BaseWeapon weapon, TimeSpan delay ) : base( delay )
			{
				m_Weapon = weapon;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Weapon.Cursed = false;
				Effects.PlaySound( m_Weapon.GetWorldLocation(), m_Weapon.Map, 0xFA );
				m_Table.Remove( this );
			}
		}

		private class SoundEffectTimer : Timer
		{
			private Mobile m_Mobile;

			public SoundEffectTimer( Mobile m ) : base( TimeSpan.FromSeconds( 0.75 ) )
			{
				m_Mobile = m;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_Mobile.PlaySound( 0xFA );
			}
		}
	}
}