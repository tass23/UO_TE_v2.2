using System;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class FearDisc : CSpellScroll
    {
        [Constructable]
        public FearDisc()
            : base(typeof(FearSpell), 0x3194)
        {
            Name = "Force Fear";
            Hue = 1772;
			Stackable = false;
        }

        public FearDisc(Serial serial)
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
