using System;
using Server;
using Server.Ethics;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Server.Spells;

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
	public class LightMatterSpell : LightForceSpell
	{
		
		private static SpellInfo m_Info = new SpellInfo(
			"Force Matter", "The Force can provide...",
			16,
			false,
			Reagent.Bloodmoss,
			Reagent.SpidersSilk,
			CReagent.DestroyingAngel
		);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }
		
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 80; } }
        public override int RequiredMana { get { return 50; } }

		public LightMatterSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		// NOTE: Creature list based on 1hr of summon/release on OSI.

		private static Type[] m_Types = new Type[]
		{
			typeof( MeerEternal ),
			typeof( MeerMage ),
			typeof( MeerWarrior ),
			typeof( MeerCaptain )
		};

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( Caster.Karma < 5000 )
			{
				Caster.SendMessage( "You lack the Jedi power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else if ( Caster.Karma > 4999 && (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}
			else
			{
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return true;
			}
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				try
				{
					BaseCreature creature = (BaseCreature)Activator.CreateInstance( m_Types[Utility.Random( m_Types.Length )] );

					//creature.ControlSlots = 2;

					TimeSpan duration;

					if ( Core.AOS )
						duration = TimeSpan.FromSeconds( (2 * Caster.Skills.Meditation.Fixed) / 5 );
					else
						duration = TimeSpan.FromSeconds( 4.0 * Caster.Skills[SkillName.Meditation].Value );

					SpellHelper.Summon( creature, Caster, 0x215, duration, false, false );
				}
				catch
				{
				}
			}

			FinishSequence();
		}

		public override TimeSpan GetCastDelay()
		{
			if ( Core.AOS )
				return TimeSpan.FromTicks( base.GetCastDelay().Ticks * 5 );

			return base.GetCastDelay() + TimeSpan.FromSeconds( 6.0 );
		}
	}
}