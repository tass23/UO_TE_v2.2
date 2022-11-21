using System;

namespace Server.ACC.CSS.Systems.LightForce
{
    public class ProjectionDisc : CSpellScroll
    {
        [Constructable]
        public ProjectionDisc()
            : base(typeof(ProjectionSpell), 0x01CB )
        {
            Name = "Force Projection";
            Hue = 1185;
			Stackable = false;
        }

        public ProjectionDisc(Serial serial)
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
