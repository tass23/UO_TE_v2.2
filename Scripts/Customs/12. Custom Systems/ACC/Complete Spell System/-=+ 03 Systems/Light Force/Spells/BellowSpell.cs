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
	public class BellowSpell : LightForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Bellow", "A Jedi can focus their Force energy, even in combat.",
		-1,
		9002,
		CReagent.SpringWater
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Second; }
        }
		
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
        public override double RequiredSkill { get { return 0; } }
        public override int RequiredMana { get { return 6; } }
		public override bool BlocksMovement{ get{ return false; } }

		public BellowSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Caster.PlaySound( 0x20F );
				Caster.PlaySound( Caster.Female ? 0x338 : 0x44A );
				Caster.FixedParticles( 0x376A, 1, 31, 9961, 1160, 0, EffectLayer.Waist );
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

				//BuffInfo.AddBuff(Caster, new BuffInfo(BuffIcon.DivineFury, 1060589, 1075634, TimeSpan.FromSeconds(delay), Caster));
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

		public static bool UnderEffect( Mobile m )
		{
			return m_Table.Contains( m );
		}

		private static void Expire_Callback( object state )
		{
			Mobile m = (Mobile)state;

			m_Table.Remove( m );

			m.Delta( MobileDelta.WeaponDamage );
			m.PlaySound( 0xF8 );
		}
	}
}