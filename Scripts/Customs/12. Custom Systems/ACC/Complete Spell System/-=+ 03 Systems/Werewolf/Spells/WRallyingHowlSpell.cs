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
	public class WRallyingHowlSpell : WerewolfSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Rallying Howl", "*howls*",
		//SpellCircle.Sixth,
		206,
		9002,
		false,
		Reagent.Bloodmoss,
		Reagent.Nightshade
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Eighth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 24; } }
        public override int RequiredMana { get { return 11; } }
		
		public WRallyingHowlSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			if( CheckSequence() )
			{
				ArrayList targets = new ArrayList();

				foreach ( Mobile m in Caster.GetMobilesInRange( 3 ) )
				{
					if ( Caster.CanBeBeneficial( m, false, true ) && !(m is Golem) )
						targets.Add( m );
				}

				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = (Mobile)targets[i];

					TimeSpan duration = TimeSpan.FromSeconds( Caster.Skills[SkillName.Anatomy].Value * 0.2 );
					int rounds = (int)( Caster.Skills[SkillName.Anatomy].Value * .15 );

					new ExpireTimer( m, 0, rounds, TimeSpan.FromSeconds( 3 ) ).Start();
					
					m.PlaySound( 0x0A5 );
					m.FixedParticles( 0x3979, 9, 32, 5030, 0x21, 3, EffectLayer.Waist );
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

		private class ExpireTimer : Timer
		{
			private Mobile m_Mobile;
			private int m_Round;
			private int m_Totalrounds;

			public ExpireTimer( Mobile m, int round, int totalrounds, TimeSpan delay ) : base( delay )
			{
				m_Mobile = m;
				m_Round = round;
				m_Totalrounds = totalrounds;
			}

			protected override void OnTick()
			{
				if ( m_Mobile != null )
				{

					m_Mobile.Hits += 10;
					m_Mobile.Stam += 10;

					if ( m_Round >= m_Totalrounds )
					{
						m_Mobile.SendMessage( "The werewolf's rallying howl fades away." );

					}
					else
					{
						m_Round += 1;
						new ExpireTimer( m_Mobile, m_Round, m_Totalrounds, TimeSpan.FromSeconds( 2 ) ).Start();
					}
				}
			}
		}
	}
}