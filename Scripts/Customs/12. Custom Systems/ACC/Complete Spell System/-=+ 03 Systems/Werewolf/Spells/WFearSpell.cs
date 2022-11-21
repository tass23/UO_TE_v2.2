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
	public class WFearSpell : WerewolfSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Horrific Howl", "*growls*",
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
		
		public WFearSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
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

        public override void OnCast()
        {
            if (CheckSequence())
            {
                List<Mobile> targets = new List<Mobile>();

                foreach (Mobile m in Caster.GetMobilesInRange(5))
                {
                    if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false))
                        targets.Add(m);
                }

                Caster.PlaySound(0x633);
                Caster.FixedParticles(0x3818, 1, 25, 9922, 14, 3, EffectLayer.Head);
                IEntity from = new Entity(Serial.Zero, new Point3D(Caster.X, Caster.Y, Caster.Z), Caster.Map);
                IEntity to = new Entity(Serial.Zero, new Point3D(Caster.X, Caster.Y, Caster.Z + 32), Caster.Map);
                Effects.SendMovingParticles(from, to, 0x19AB, 1, 0, false, false, 33, 3, 9501, 1, 0, EffectLayer.Head, 0x100);


                int dispelSkill = Caster.Int;

                double mag = Caster.Skills.AnimalLore.Value;

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
                                    Effects.PlaySound(m, m.Map, 0x573);

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
                                    m.PlaySound(m.Female ? 0x573 : 0x573);
                                    m.BeginFlee(TimeSpan.FromSeconds(15.0));
                                }
                            }
                        }
                    }
                }
            }

            FinishSequence();
        }
    }
}