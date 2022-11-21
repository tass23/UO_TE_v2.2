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

namespace Server.ACC.CSS.Systems.Vampire
{
	public class VBloodOathSpell : VampireSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Cursed Blood", "sange blestemat",
		//SpellCircle.Sixth,
		206,
		9002,
		false,
		Reagent.Bloodmoss,
		Reagent.Nightshade
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Sixth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 84; } }
        public override int RequiredMana { get { return 20; } }
		
		public VBloodOathSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

		public void Target( Mobile m )
		{
			if ( Caster == m || !(m is PlayerMobile || m is BaseCreature) ) // only PlayerMobile and BaseCreature implement blood oath checking
			{
				Caster.SendLocalizedMessage( 1060508 ); // You can't curse that.
			}
			else if ( m_OathTable.Contains( Caster ) )
			{
				Caster.SendMessage( "You have already cursed your blood." ); // You are already bonded in a Blood Oath.
			}
			else if ( m_OathTable.Contains( m ) )
			{
				if ( m.Player )
					Caster.SendMessage( "That player has already cursed their blood." ); // That player is already bonded in a Blood Oath.
				else
					Caster.SendMessage( "That creature's blood has already been cursed." ); // That creature is already bonded in a Blood Oath.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				/* Temporarily creates a dark pact between the caster and the target.
				 * Any damage dealt by the target to the caster is increased, but the target receives the same amount of damage.
				 * The effect lasts for ((Spirit Speak skill level - target's Resist Magic skill level) / 80 ) + 8 seconds.
				 * 
				 * NOTE: The above algorithm must be fixed point, it should be:
				 * ((ss-rm)/8)+8
				 */

				ExpireTimer timer = (ExpireTimer)m_Table[m];
				if ( timer != null )
				timer.DoExpire();

				m_OathTable[Caster] = Caster;
				m_OathTable[m] = Caster;

				 if ( m.Spell != null )
					m.Spell.OnCasterHurt();
				Caster.PlaySound( 0x231 );

				Caster.FixedParticles( 0x375A, 1, 17, 9919, 38, 7, EffectLayer.Waist );
				Caster.FixedParticles( 0x3728, 1, 13, 9502, 38, 7, (EffectLayer)255 );

				m.FixedParticles( 0x375A, 1, 17, 9919, 38, 7, EffectLayer.Waist );
				m.FixedParticles( 0x3728, 1, 13, 9502, 38, 7, (EffectLayer)255 );

				TimeSpan duration = TimeSpan.FromSeconds( ((GetDamageSkill( Caster ) - GetResistSkill( m )) / 8) + 8 );
				m.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );	//Skill check for gain

				timer = new ExpireTimer ( Caster, m, duration );
				timer.Start ();

				BuffInfo.AddBuff ( Caster, new BuffInfo ( BuffIcon.BloodOathCaster, 1122594, duration, Caster, m.Name.ToString () ) );
				BuffInfo.AddBuff ( m, new BuffInfo ( BuffIcon.BloodOathCaster, 1122595, duration, m, Caster.Name.ToString () ) );
				
				m_Table[m] = timer;
				HarmfulSpell( m );
			}

			FinishSequence();
		}

			public static bool RemoveCurse( Mobile m )
			{
			ExpireTimer t = (ExpireTimer)m_Table[m];

				if ( t == null )
					return false;

			t.DoExpire();
			return true;
		}

		private static Hashtable m_OathTable = new Hashtable();
		private static Hashtable m_Table = new Hashtable ();

		public static Mobile GetBloodOath( Mobile m )
		{
			if ( m == null )
				return null;

			Mobile oath = (Mobile)m_OathTable[m];

			if ( oath == m )
				oath = null;

			return oath;
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Caster;
			private Mobile m_Target;
			private DateTime m_End;

			public ExpireTimer( Mobile caster, Mobile target, TimeSpan delay ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Caster = caster;
				m_Target = target;
				m_End = DateTime.Now + delay;

				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Caster.Deleted || m_Target.Deleted || !m_Caster.Alive || !m_Target.Alive || DateTime.Now >= m_End )
				{
					DoExpire ();
				}
			}
			public void DoExpire()
			{
				if( m_OathTable.Contains( m_Caster ) )
				{
					m_Caster.SendMessage( "The curse of your blood has been broken." ); // Your Blood Oath has been broken.
					m_OathTable.Remove ( m_Caster );
				}

				if( m_OathTable.Contains( m_Target ) )
				{
					m_Target.SendMessage( "The curse of your blood has been broken." ); // Your Blood Oath has been broken.
					m_OathTable.Remove ( m_Target );
				}

				Stop ();
				
				BuffInfo.RemoveBuff ( m_Caster, BuffIcon.BloodOathCaster );
				BuffInfo.RemoveBuff ( m_Target, BuffIcon.BloodOathCaster );

				m_Table.Remove ( m_Caster );
			}
		}

		private class InternalTarget : Target
		{
			private VBloodOathSpell m_Owner;

			public InternalTarget( VBloodOathSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
				else
					from.SendLocalizedMessage( 1060508 ); // You can't curse that.
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}