using System;
using Server;

namespace Server.Items
{
    public class UnknownPotion : BasePotion
    {
        [Constructable]
        public UnknownPotion()
            : base(0xF06, PotionEffect.Unknown)
        {
            Name = "Unknown Potion";
            Hue = 0x343;
        }

        public UnknownPotion(Serial serial)
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

        public override void Drink(Mobile from)
        {
            from.SendMessage("Brrrr... what a swill!");
            BasePotion.PlayDrinkEffect(from);
            this.Consume();
        }
    }
}