using System;
using System.Collections;
using Server.Mobiles;
using Server.Spells.Necromancy;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
	/// <summary>
	/// Make your opponent bleed profusely with this wicked use of your weapon.
	/// When successful, the target will bleed for several seconds, taking damage as time passes for up to ten seconds.
	/// The rate of damage slows down as time passes, and the blood loss can be completely staunched with the use of bandages. 
	/// </summary>
	public class BleedAttack : WeaponAbility
	{
		public BleedAttack()
		{
		}

		public override int BaseMana{ get{ return 30; } }

		public override void OnHit( Mobile attacker, Mobile defender, int damage )
		{
			if ( !Validate( attacker ) || !CheckMana( attacker, true ) )
				return;

			ClearCurrentAbility( attacker );

			// Necromancers under Lich or Wraith Form are immune to Bleed Attacks.
			TransformContext context = TransformationSpellHelper.GetContext( defender );

			if ( (context != null && ( context.Type == typeof( LichFormSpell ) || context.Type == typeof( WraithFormSpell ))) ||
				(defender is BaseCreature && ((BaseCreature)defender).BleedImmune) )
			{
				attacker.SendLocalizedMessage( 1062052 ); // Your target is not affected by the bleed attack!
				return;
			}

			attacker.SendLocalizedMessage( 1060159 ); // Your target is bleeding!
			defender.SendLocalizedMessage( 1060160 ); // You are bleeding!

			if ( defender is PlayerMobile )
			{
				defender.LocalOverheadMessage( MessageType.Regular, 0x21, 1060757 ); // You are bleeding profusely
				defender.NonlocalOverheadMessage( MessageType.Regular, 0x21, 1060758, defender.Name ); // ~1_NAME~ is bleeding profusely
			}

			defender.PlaySound( 0x133 );
			defender.FixedParticles( 0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist );

			BeginBleed( defender, attacker, (AosWeaponAttributes.GetValue( attacker, AosWeaponAttribute.BloodDrinker ) != 0) );
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool IsBleeding( Mobile m )
		{
			return m_Table.Contains( m );
		}

		#region SA
		public static void BeginBleed( Mobile m, Mobile from, bool leeches )
		{
		#endregion
			Timer t = (Timer)m_Table[m];

			if ( t != null )
				t.Stop();

			#region SA
			t = new InternalTimer( from, m, leeches );
			#endregion
			m_Table[m] = t;

			t.Start();
		}

		#region SA
		public static void DoBleed( Mobile m, Mobile from, int level, bool leeches )
		{
		#endregion
			if ( m.Alive )
			{
				int damage = Utility.RandomMinMax( level, level * 2 );

				#region SA
				if ( leeches )
				{
					from.Hits += damage;
					from.SendLocalizedMessage( 1113606 ); // The blood drinker effect heals you.
				}
				#endregion

				if ( !m.Player )
					damage *= 2;

				m.PlaySound( 0x133 );
				m.Damage( damage, from );

				Blood blood = new Blood();

				blood.ItemID = Utility.Random( 0x122A, 5 );

				blood.MoveToWorld( m.Location, m.Map );
			}
			else
			{
				EndBleed( m, false );
			}
		}

		public static void EndBleed( Mobile m, bool message )
		{
			Timer t = (Timer)m_Table[m];

			if ( t == null )
				return;

			t.Stop();
			m_Table.Remove( m );

			if ( message )
				m.SendLocalizedMessage( 1060167 ); // The bleeding wounds have healed, you are no longer bleeding!
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;
			private Mobile m_Mobile;

			#region SA
			private bool m_Leeches;
			#endregion

			private int m_Count;

			#region SA
			public InternalTimer( Mobile from, Mobile m, bool leeches ) : base( TimeSpan.FromSeconds( 2.0 ), TimeSpan.FromSeconds( 2.0 ) )
			{
			#endregion
				m_From = from;
				m_Mobile = m;

				#region SA
				m_Leeches = leeches;
				#endregion

				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				#region SA
				DoBleed( m_Mobile, m_From, 5 - m_Count, m_Leeches );
				#endregion

				if ( ++m_Count == 5 )
					EndBleed( m_Mobile, true );
			}
		}
	}
}