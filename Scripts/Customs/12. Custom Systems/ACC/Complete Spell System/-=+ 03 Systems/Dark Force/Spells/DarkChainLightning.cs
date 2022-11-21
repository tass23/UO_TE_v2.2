using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DarkChainLightningSpell : DarkForceSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Chain Lightning", "Perfection is a goal, not a state of being.",
        209,
        9022,
        false,
        Reagent.GraveDust,
        Reagent.BatWing,
        Reagent.DaemonBlood,
        Reagent.NoxCrystal
        );

        public override SpellCircle Circle { get { return SpellCircle.Sixth; } }
		
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
        public override double RequiredSkill { get { return 80; } }
        public override int RequiredMana { get { return 50; } }

        public DarkChainLightningSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

		public override bool CheckCast()
		{
			if ( Caster.Karma > 4999 )
			{
				Caster.SendMessage( "You lack the Sith power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else
			{
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return true;
			}
		}
		
        public override bool DelayedDamage { get { return true; } }

        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                if (p is Item)
                    p = ((Item)p).GetWorldLocation();

                List<Mobile> targets = new List<Mobile>();

                Map map = Caster.Map;

                bool playerVsPlayer = false;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 5);

                    foreach (Mobile m in eable)
                    {
                        if (Core.AOS && m == Caster)
                            continue;

                        if (SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false))
                        {
                            if (Core.AOS && !Caster.InLOS(m))
                                continue;

                            targets.Add(m);

                            if (m.Player)
                                playerVsPlayer = true;
                        }
                    }

                    eable.Free();
                }

                double damage;

                if (Core.AOS)
                    damage = GetNewAosDamage(51, 1, 5, playerVsPlayer);
                else
                    damage = Utility.Random(427, 442);

                if (targets.Count > 0)
                {
                    if (Core.AOS && targets.Count > 2)
                        damage = (damage * 4) / targets.Count;
                    else if (!Core.AOS)
                        damage /= targets.Count;

                    for (int i = 0; i < targets.Count; ++i)
                    {
                        Mobile m = targets[i];

                        double toDeal = damage;

                        if (!Core.AOS && CheckResisted(m))
                        {
                            toDeal *= 0.5;

                            m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }

                        Caster.DoHarmful(m);
                        SpellHelper.Damage(this, m, toDeal, 0, 0, 0, 0, 100);

                        m.BoltEffect(0);
                    }
                }
                else
                {
                    Caster.PlaySound(0x29);
                }
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private DarkChainLightningSpell m_Owner;

            public InternalTarget(DarkChainLightningSpell owner)
                : base(Core.ML ? 10 : 12, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                IPoint3D p = o as IPoint3D;

                if (p != null)
                    m_Owner.Target(p);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}