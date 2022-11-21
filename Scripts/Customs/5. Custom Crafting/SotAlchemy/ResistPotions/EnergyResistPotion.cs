using System;
using Server;

namespace Server.Items
{
    public class EnergyResistPotion : BaseResistPotion
    {
        [Constructable]
        public EnergyResistPotion()
            : base(PotionEffect.EnergyResist, 10.0, 10, ResistanceType.Energy)
        {
            Name = "Energy Resistance Potion";
            Hue = 0x490;
        }

        [Constructable]
        public EnergyResistPotion(double duration, int effect)
            : base(PotionEffect.EnergyResist, duration, effect, ResistanceType.Energy)
        {
            Name = "Energy Resistance Potion";
            Hue = 0x490;
        }

        public EnergyResistPotion(Serial serial)
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