using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DarkGuardianSpell : DarkForceSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
            "Force Guardian", "I am one with the Dark Side...",
            260,
            9032,
            false,
            Reagent.BatWing,
            Reagent.DaemonBlood,
            Reagent.PigIron
        );

        public override SpellCircle Circle
        {
            get { return SpellCircle.Eighth; }
        }
		
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
        public override double RequiredSkill { get { return 80; } }
        public override int RequiredMana { get { return 50; } }

        public DarkGuardianSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

		private static Type[] m_Types = new Type[]
		{
			typeof( DarkGuardian )
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
			else if ( Caster.Karma < 5000 && (Caster.Followers + 1) > Caster.FollowersMax )
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
						duration = TimeSpan.FromSeconds( 90 );
					else
						duration = TimeSpan.FromSeconds( Utility.Random( 90, 50 ) );

					SpellHelper.Summon( creature, Caster, 0x215, duration, false, false );
				}
				catch
				{
				}
			}

			FinishSequence();
		}

        public void Target(IPoint3D p)
        {
            Map map = Caster.Map;

            SpellHelper.GetSurfaceTop(ref p);

            if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
            }
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                BaseCreature.Summon(new DarkGuardian(), false, Caster, new Point3D(p), 0x212, TimeSpan.FromSeconds(Utility.Random(80, 40)));
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private DarkGuardianSpell m_Owner;

            public InternalTarget(DarkGuardianSpell owner) : base(12, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IPoint3D)
                    m_Owner.Target((IPoint3D)o);
            }

            protected override void OnTargetOutOfLOS(Mobile from, object o)
            {
                from.SendLocalizedMessage(501943); // Target cannot be seen. Try again.
                from.Target = new InternalTarget(m_Owner);
                from.Target.BeginTimeout(from, TimeoutTime - DateTime.Now);
                m_Owner = null;
            }

            protected override void OnTargetFinish(Mobile from)
            {
                if (m_Owner != null)
                    m_Owner.FinishSequence();
            }
        }
    }
}