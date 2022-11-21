using System;
using Server;

namespace Server.Items
{
	public class TopQuesterStatuette : BaseStatuette
    {
		
        [Constructable]
        public TopQuesterStatuette()
            : base(0x42C5)
        {
            Name = "Top Quester";
            Weight = 1.0;
        }

        public TopQuesterStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0x65B, 0x635, 0x5CE, 0x569, 0x568, 0x567, 0x566, 0x557
		};

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (TurnedOn && IsLockedDown && (!m.Hidden || m.AccessLevel == AccessLevel.Player) && Utility.InRange(m.Location, this.Location, 2) && !Utility.InRange(oldLocation, this.Location, 2))
                Effects.PlaySound(Location, Map, m_Sounds[Utility.Random(m_Sounds.Length)]);

            base.OnMovement(m, oldLocation);
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