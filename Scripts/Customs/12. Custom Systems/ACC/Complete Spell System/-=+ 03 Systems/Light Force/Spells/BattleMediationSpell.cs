using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Spells;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;
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
	public class BattleMeditationSpell : LightForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Battle Meditation", "Focusing the Force during combat is important.",
		-1,
		9002,
		false,
		CReagent.SpringWater,
		Reagent.Nightshade,
		Reagent.SpidersSilk
		);
		
		public override SpellCircle Circle
        {
            get { return SpellCircle.Second; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
        public override double RequiredSkill { get { return 0; } }
        public override int RequiredMana { get { return 6; } }
		public override bool BlocksMovement{ get{ return false; } }

		public BattleMeditationSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Caster.PlaySound( 0x0F5 );
				Caster.PlaySound( 0x1ED );
				Caster.FixedParticles( 0x375A, 1, 30, 9966, 33, 2, EffectLayer.Head );
				Caster.FixedParticles( 0x37B9, 1, 30, 9502, 43, 3, EffectLayer.Head );

				Timer t = (Timer)m_Table[Caster];

				if ( t != null )
					t.Stop();

				double delay = (double)ComputePowerValue( 1 ) / 60;

				// TODO: Should caps be applied?
				if ( delay < 1.5 )
					delay = 1.5;
				else if ( delay > 3.5 )
					delay = 3.5;

				m_Table[Caster] = Timer.DelayCall( TimeSpan.FromMinutes( delay ), new TimerStateCallback( Expire_Callback ), Caster );

				if ( Caster is PlayerMobile )
				{
					((PlayerMobile)Caster).EnemyOfOneType = null;
					((PlayerMobile)Caster).WaitingForEnemy = true;

					//BuffInfo.AddBuff ( Caster, new BuffInfo ( BuffIcon.EnemyOfOne, 1075653, 1044111, TimeSpan.FromMinutes ( delay ), Caster ) );
				}
			}

			FinishSequence();
		}
		
		public override bool CheckCast()
		{
			if ( Caster.Karma < 5000 )
			{
				Caster.SendMessage( "You lack the Jedi power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
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

		private static void Expire_Callback( object state )
		{
			Mobile m = (Mobile)state;

			m_Table.Remove( m );

			m.PlaySound( 0x1F8 );

			if ( m is PlayerMobile )
			{
				((PlayerMobile)m).EnemyOfOneType = null;
				((PlayerMobile)m).WaitingForEnemy = false;
			}
		}
	}
}