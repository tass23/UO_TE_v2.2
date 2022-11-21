using System;

namespace Server.ACC.CSS.Systems.LightForce
{
    public class MindTrickDisc : CSpellScroll
    {
        [Constructable]
        public MindTrickDisc()
            : base(typeof(MindTrickSpell), 0x01CB)
        {
            Name = "Force Mind Trick";
            Hue = 1185;
			Stackable = false;
        }

        public MindTrickDisc(Serial serial)
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
