using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DarkVortexSpell : DarkForceSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
            "Force Vortex", "I am one with the Dark Side...",
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
		
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.5 ); } }
        public override double RequiredSkill { get { return 80; } }
        public override int RequiredMana { get { return 50; } }

        public DarkVortexSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

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
            if (CheckSequence())
                Caster.Target = new InternalTarget(this);
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
                BaseCreature.Summon(new DarkVortex(), false, Caster, new Point3D(p), 0x212, TimeSpan.FromSeconds(Utility.Random(80, 40)));
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private DarkVortexSpell m_Owner;

            public InternalTarget(DarkVortexSpell owner)
                : base(12, true, TargetFlags.None)
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