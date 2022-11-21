using System;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DarkProjectionDisc : CSpellScroll
    {
        [Constructable]
        public DarkProjectionDisc()
            : base(typeof(DarkProjectionSpell), 0x3194)
        {
            Name = "Force Projection";
            Hue = 1772;
			Stackable = false;
        }

        public DarkProjectionDisc(Serial serial)
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
