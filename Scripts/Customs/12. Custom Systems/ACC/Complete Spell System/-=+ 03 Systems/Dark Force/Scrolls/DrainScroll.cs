using System;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DrainDisc : CSpellScroll
    {
        [Constructable]
        public DrainDisc()
            : base(typeof(DrainSpell), 0x3194)
        {
            Name = "Force Drain";
            Hue = 1772;
			Stackable = false;
        }

        public DrainDisc(Serial serial)
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
