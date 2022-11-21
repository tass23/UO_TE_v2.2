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
	public class WDivineFury : WerewolfSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Beserker", "*howls*",
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
        public override double RequiredSkill { get { return 24; } }
        public override int RequiredMana { get { return 11; } }
		
		public WDivineFury( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Caster.PlaySound( 0x0B3 );
				Caster.FixedParticles( 0x37CC, 1, 31, 9961, 1160, 0, EffectLayer.Waist );
				Caster.FixedParticles( 0x37C4, 1, 31, 9502, 43, 2, EffectLayer.Waist );

				Caster.Stam = Caster.StamMax;

				Timer t = (Timer)m_Table[Caster];

				if ( t != null )
					t.Stop();

				int delay = ComputePowerValue( 10 );

				// TODO: Should caps be applied?
				if ( delay < 7 )
					delay = 7;
				else if ( delay > 24 )
					delay = 24;

				m_Table[Caster] = t = Timer.DelayCall( TimeSpan.FromSeconds( delay ), new TimerStateCallback( Expire_Callback ), Caster );
				Caster.Delta( MobileDelta.WeaponDamage );
				
				BuffInfo.AddBuff(Caster, new BuffInfo(BuffIcon.DivineFury, 1122604, 1122605, TimeSpan.FromSeconds(delay), Caster));
				
				if ( Caster is PlayerMobile )
				{
					((PlayerMobile)Caster).EnemyOfOneType = null;
					((PlayerMobile)Caster).WaitingForEnemy = true;
				}
			}

			FinishSequence();
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

		private static Hashtable m_Table = new Hashtable();

		public static bool UnderEffect( Mobile m )
		{
			return m_Table.Contains( m );
		}

		private static void Expire_Callback( object state )
		{
			Mobile m = (Mobile)state;

			m_Table.Remove( m );

			m.Delta( MobileDelta.WeaponDamage );
			m.PlaySound( 0x1D6 );
			
			if ( m is PlayerMobile )
			{
				((PlayerMobile)m).EnemyOfOneType = null;
				((PlayerMobile)m).WaitingForEnemy = false;
			}
		}
	}
}