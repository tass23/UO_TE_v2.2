using System;

namespace Server.ACC.CSS.Systems.LightForce
{
    public class BattleMeditationDisc : CSpellScroll
    {
        [Constructable]
        public BattleMeditationDisc()
            : this(1)
        {
        }

        [Constructable]
        public BattleMeditationDisc(int amount)
            : base(typeof(BattleMeditationSpell), 0x01CB, amount)
        {
            Name = "Battle Meditation";
            Hue = 1185;
        }

        public BattleMeditationDisc(Serial serial)
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
