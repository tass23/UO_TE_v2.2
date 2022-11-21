using System;
using Server;
using Server.Ethics;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Server.Spells;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkMatterSpell : DarkForceSpell
	{
		
		private static SpellInfo m_Info = new SpellInfo(
			"Force Matter", "The Dark Side provides...",
			16,
			false,
			Reagent.BatWing,
			Reagent.DaemonBlood,
			Reagent.NoxCrystal
		);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }
		
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
        public override double RequiredSkill { get { return 80; } }
        public override int RequiredMana { get { return 50; } }

		public DarkMatterSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		private static Type[] m_Types = new Type[]
		{
			typeof( AcidElemental ),
			typeof( BloodElemental ),
			typeof( Efreet ),
			typeof( ToxicElemental )
		};

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( Caster.Karma > 4999 )
			{
				Caster.SendMessage( "You lack the Sith power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else if ( Caster.Karma < 5000 && (Caster.Followers + 2) > Caster.FollowersMax )
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