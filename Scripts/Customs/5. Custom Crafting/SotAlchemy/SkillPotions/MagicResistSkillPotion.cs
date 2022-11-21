using System;
using Server;

namespace Server.Items
{
    public class MagicResistSkillPotion : BaseSkillPotion
    {
        [Constructable]
        public MagicResistSkillPotion()
            : base(PotionEffect.MagicResistSkill, 10.0, 10.0, SkillName.MagicResist)
        {
            Name = "Magic Resistance Potion";
            Hue = 0x184;
        }

        [Constructable]
        public MagicResistSkillPotion(double duration, double effect)
            : base(PotionEffect.MagicResistSkill, duration, effect, SkillName.MagicResist)
        {
            Name = "Magic Resistance Potion";
            Hue = 0x184;
        }

        public MagicResistSkillPotion(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}