using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class FearSpell : DarkForceSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Fear", "Through power, I gain victory.",
        230,
        9022,
        Reagent.GraveDust,
        Reagent.PigIron,
        Reagent.BatWing
        );

        public override Server.Spells.SpellCircle Circle
        {
            get { return SpellCircle.Sixth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.5 ); } }
        public override double RequiredSkill { get { return 52; } }
        public override int RequiredMana { get { return 20; } }
		
        public FearSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                List<Mobile> targets = new List<Mobile>();

                foreach (Mobile m in Caster.GetMobilesInRange(8))
                {
                    if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false))
                        targets.Add(m);
                }

                Caster.PlaySound(0x245);
                Caster.PlaySound(0x3EA);
                Caster.FixedParticles(0x2109, 1, 25, 9922, 14, 3, EffectLayer.Head);
                IEntity from = new Entity(Serial.Zero, new Point3D(Caster.X, Caster.Y, Caster.Z), Caster.Map);
                IEntity to = new Entity(Serial.Zero, new Point3D(Caster.X, Caster.Y, Caster.Z + 32), Caster.Map);
                Effects.SendMovingParticles(from, to, 0x3192, 1, 0, false, false, 33, 3, 1478, 1, 0, EffectLayer.Head, 0x100);


                int dispelSkill = Caster.Int;

                double mag = Caster.Skills.Magery.Value;

                for (int i = 0; i < targets.Count; ++i)
                {
                    if (targets[i] is BaseCreature)
                    {
                        BaseCreature m = targets[i] as BaseCreature;

                        if (m != null)
                        {
                            bool dispellable = m.Summoned && !m.IsAnimatedDead;

                            if (dispellable)
                            {
                                double dispelChance = (50.0 + ((100 * (mag - m.DispelDifficulty)) / (m.DispelFocus * 2))) / 100;
                                dispelChance *= dispelSkill / 100.0;

                                if (dispelChance > Utility.RandomDouble())
                                {
                                    Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                                    Effects.PlaySound(m, m.Map, 0x201);

                                    m.Delete();
                                    continue;
                                }
                            }

                            bool evil = !m.Controlled && !m.Blessed;

                            if (evil)
                            {

                                double fleeChance = (100 - Math.Sqrt(m.Fame / 2)) * mag * dispelSkill;
                                fleeChance /= 1000000;

                                if (fleeChance > Utility.RandomDouble())
                                {
                                    m.PlaySound(m.Female ? 814 : 1088);
                                    m.BeginFlee(TimeSpan.FromSeconds(15.0));
                                }
                            }
                        }
                    }
                }
            }

            FinishSequence();
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
    }
}
