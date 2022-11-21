using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Gumps;
using Server.Targeting;
using System.Collections;
using Server.Network;

namespace Server.Spells.Mystic
{
    public class EnchantSpell : MysticSpell
    {
        // Temporarily enchants a held weapon with a hit spell effect.

        public override int RequiredMana { get { return 6; } }
        public override double RequiredSkill { get { return 8; } }

        private static SpellInfo m_Info = new SpellInfo(
                "Enchant", "In Ort Ylem",
                680,
                9022, //change to correct number.
                Reagent.MandrakeRoot,
                Reagent.SulfurousAsh,
                Reagent.SpidersSilk
            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.75); } }

        public EnchantSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            BaseWeapon weapon = Caster.Weapon as BaseWeapon;

            if (weapon == null || weapon is Fists)
            {
                Caster.SendLocalizedMessage(501078); // You must be holding a weapon.
                return;
            }

            if (!Caster.CanBeginAction(typeof(EnchantSpell)))
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
            }
            else
            {
                if (Caster.HasGump(typeof(EnchantGump)))
                    Caster.CloseGump(typeof(EnchantGump));

                Caster.SendGump(new EnchantGump());

                Caster.PlaySound(0x387);
                Caster.FixedParticles(0x3779, 1, 15, 9905, 32, 2, EffectLayer.Head);
                Caster.FixedParticles(0x37B9, 1, 14, 9502, 32, 5, (EffectLayer)255);
             
            }
        }
    }
}


	