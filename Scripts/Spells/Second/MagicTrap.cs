using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Second
{
    public class MagicTrapSpell : MagerySpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Magic Trap", "In Jux",
                212,
                9001,
                Reagent.Garlic,
                Reagent.SpidersSilk,
                Reagent.SulfurousAsh
            );

        public override SpellCircle Circle { get { return SpellCircle.Second; } }

        public MagicTrapSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(TrapableContainer item)
        {
            if (!Caster.CanSee(item))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (item.TrapType != TrapType.None && item.TrapType != TrapType.MagicTrap)
            {
                base.DoFizzle();
            }
            else if (Core.AOS && !item.IsChildOf(Caster.Backpack))
            {
                Caster.SendLocalizedMessage(1045158); // You must have the item in your backpack to target it.
            }
            else if (CheckSequence())
            {
                Point3D loc;

                SpellHelper.Turn(Caster, item);

                item.TrapType = TrapType.MagicTrap;
                item.TrapPower = Core.AOS ? Utility.RandomMinMax(10, 50) : 1;
                item.TrapLevel = 0;

                if (!item.IsChildOf(Caster.Backpack))
                    loc = item.GetWorldLocation();
                else
                    loc = Caster.Location;

                loc.X++;
                NewTimer(item, loc, Caster.Map, 0);
                loc.X--;
                loc.Y--;
                NewTimer(item, loc, Caster.Map, .75);
                loc.X--;
                loc.Y++;
                NewTimer(item, loc, Caster.Map, 1.5);
                loc.X++;
                loc.Y++;
                NewTimer(item, loc, Caster.Map, 2.25);
                Effects.PlaySound(loc, item.Map, 0x1EF);
            }
            FinishSequence();
        }

        private void NewTimer(Item item, Point3D loc, Map map, double secs)
        {
            Timer t = new ParticleTimer(item, loc, Caster.Map);
            t.Delay = TimeSpan.FromSeconds(secs);
            t.Start();
        }
        private class ParticleTimer : Timer
        {
            private Item m_item;
            private Point3D m_loc;
            private Map m_map;
            public ParticleTimer(Item item, Point3D loc, Map map)
                : base(TimeSpan.FromSeconds(.75))
            {
                m_loc = loc;
                m_item = item;
                m_map = map;
            }
            protected override void OnTick()
            {
                Effects.SendLocationParticles(EffectItem.Create(
                                    new Point3D(
                                        m_loc.X,
                                        m_loc.Y,
                                        m_loc.Z),
                                    m_map,
                                    EffectItem.DefaultDuration),
                                0x376A,
                                9,
                                10,
                                9502);
            }
        }

        private class InternalTarget : Target
        {
            private MagicTrapSpell m_Owner;

            public InternalTarget(MagicTrapSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is TrapableContainer)
                {
                    m_Owner.Target((TrapableContainer)o);
                }
                else
                {
                    from.PrivateOverheadMessage(MessageType.Regular, 0x88, 501767, from.NetState);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}