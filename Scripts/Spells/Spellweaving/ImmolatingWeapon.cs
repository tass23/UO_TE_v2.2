using System;
using System.Collections;
using Server.Items;

namespace Server.Spells.Spellweaving
{
	public class ImmolatingWeaponSpell : ArcanistSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Immolating Weapon", "Thalshara",
				-1
			);
			
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1 ); } }

		public override double RequiredSkill { get { return 10.0; } }
		public override int RequiredMana { get { return 32; } }

		public ImmolatingWeaponSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			BaseWeapon weapon = Caster.Weapon as BaseWeapon;

			if ( weapon == null || weapon is Fists )
			{
				Caster.SendLocalizedMessage( 1060179 ); // You must be wielding a weapon to use this ability!
			}
			else if ( CheckSequence() )
			{
				Caster.PlaySound( 0x5CA );
				Caster.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
				
				int level = GetFocusLevel( Caster );
				double skill = Caster.Skills[SkillName.Spellweaving].Value;

				TimeSpan duration = TimeSpan.FromSeconds( 10 + Math.Min( (int) ( skill / 24 ), 1 ) + level );
				int damage = 10 + Math.Min( (int) Math.Floor( skill / 24 ), 1 ) + level;
				
				ImmolatingWeaponEntry entry = m_Table[ weapon ] as ImmolatingWeaponEntry;

				if( entry != null )
					entry.Timer.Stop();

				weapon.Immolating = true;

				m_Table[ weapon ] = new ImmolatingWeaponEntry( damage, weapon, duration );
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		public static int GetDamage( BaseWeapon weapon )
		{
			ImmolatingWeaponEntry entry = m_Table[ weapon ] as ImmolatingWeaponEntry;

			if( entry != null )
				return entry.Damage;

			return 0;
		}

		public class ImmolatingWeaponEntry
		{
			public int Damage;
			public Timer Timer;
			
			public ImmolatingWeaponEntry( int damage, BaseWeapon weapon, TimeSpan duration )
			{
				Damage = damage;
				
				Timer = new ExpireTimer( weapon, duration );
				Timer.Start();
			}
		}

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
				m_Weapon.Immolating = false;
				m_Table.Remove( m_Weapon );
			}
		}
	}
}