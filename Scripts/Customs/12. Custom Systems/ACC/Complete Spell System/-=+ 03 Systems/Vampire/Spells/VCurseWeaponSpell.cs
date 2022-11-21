using System;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;
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

namespace Server.ACC.CSS.Systems.Vampire
{
	public class VCurseWeaponSpell : VampireSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Leech Weapon", "arma lipitoare",
		//SpellCircle.Sixth,
		206,
		9002,
		false,
		Reagent.Bloodmoss,
		Reagent.Nightshade
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Fifth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 73; } }
        public override int RequiredMana { get { return 14; } }
		
		public VCurseWeaponSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

				Caster.PlaySound( 0x3BE );
				Caster.FixedParticles( 0x3779, 1, 15, 9905, 38, 2, EffectLayer.Head );
				Caster.FixedParticles( 0x37B9, 1, 14, 9502, 38, 5, (EffectLayer)255 );
				new SoundEffectTimer( Caster ).Start();

				TimeSpan duration = TimeSpan.FromSeconds( (Caster.Skills[SkillName.SpiritSpeak].Value / 3.4) + 2.0 );


				Timer t = (Timer)m_Table[weapon];

				if ( t != null )
					t.Stop();

				weapon.Cursed = true;

				m_Table[weapon] = t = new ExpireTimer( weapon, duration );

				t.Start();
			}

			FinishSequence();
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
				Effects.PlaySound( m_Weapon.GetWorldLocation(), m_Weapon.Map, 0x1E1 );
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
				m_Mobile.PlaySound( 0x1E1 );
			}
		}
	}
}