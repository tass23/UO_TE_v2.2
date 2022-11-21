using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Mystic;

namespace Server.Spells.Mystic
{
    public class StoneFormSpell : MysticTransformationSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Stone Form", "In Rel Ylem",
                230,
                9022,
                Reagent.Bloodmoss,
                Reagent.FertileDirt,
                Reagent.Garlic
            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(2.0); } }

        public override double RequiredSkill { get { return 33.0; } }
        public override int RequiredMana { get { return 11; } }

        public override int Body { get { return 705; } }

        public virtual int SwingSpeedBonus { get { return -10; } }
        public virtual int CastSpeedBonus { get { return -2; } }

        private static Hashtable m_Table = new Hashtable();

        public StoneFormSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public static bool HasEffect(Mobile m)
        {
            return (m_Table[m] != null);
        }

        public override void RemoveEffect(Mobile m)
        {
            ResistanceMod[] mods = (ResistanceMod[])m_Table[m];

            if (mods != null)
            {             
                m_Table.Remove(m);

                m_Table[m] = mods;

                for (int i = 0; i < mods.Length; ++i)
                    m.RemoveResistanceMod(mods[i]);

                m.PlaySound(0x65B);
                m.FixedParticles(0x3728, 1, 13, 9918, 92, 3, EffectLayer.Head);
                m.Delta(MobileDelta.WeaponDamage);
                m.EndAction(typeof(StoneFormSpell));
            }
        }

        public override void DoEffect(Mobile m)
        {
            ResistanceMod[] mods = (ResistanceMod[])m_Table[m];
       
                int otherMod = 0 + (int)(m.Skills[SkillName.Mysticism].Value / 20);
                int admod = 1 + (int)(m.Skills[SkillName.Focus].Value / 20);
                //int casts = (int)AosAttributes.GetValue(Caster, AosAttribute.CastSpeed - 2);
               

                mods = new ResistanceMod[5]
				{
					new ResistanceMod( ResistanceType.Physical, otherMod + admod  ),
					new ResistanceMod( ResistanceType.Fire,		otherMod + admod  ),
				    new ResistanceMod( ResistanceType.Cold,		otherMod + admod  ),
					new ResistanceMod( ResistanceType.Poison,	otherMod + admod  ),
					new ResistanceMod( ResistanceType.Energy,	otherMod + admod  ),
                   // new ResistanceMod( AosAttribute.CastSpeed, casts )
			       
				};

                m_Table[m] = mods;

                for (int i = 0; i < mods.Length; ++i)
                    m.AddResistanceMod(mods[i]);

                m.PlaySound(0x65B);
                m.FixedParticles(0x3728, 1, 13, 9918, 92, 3, EffectLayer.Head);
                m.Delta(MobileDelta.WeaponDamage);
                
                m.EndAction(typeof(StoneFormSpell));
                
            }
        }
    }
//}

 			
		
