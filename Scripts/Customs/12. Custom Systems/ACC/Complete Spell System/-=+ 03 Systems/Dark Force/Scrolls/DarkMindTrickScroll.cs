using System;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DarkMindTrickDisc : CSpellScroll
    {
        [Constructable]
        public DarkMindTrickDisc()
            : base(typeof(DarkMindTrickSpell), 0x3194)
        {
            Name = "Force Mind Trick";
            Hue = 1772;
			Stackable = false;
        }

        public DarkMindTrickDisc(Serial serial)
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
