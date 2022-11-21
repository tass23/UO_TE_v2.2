using System;
using Server;

namespace Server.Items
{
    public class OphidianWarriorStatuette : BaseStatuette
    {

        [Constructable]
        public OphidianWarriorStatuette()
            : base(0x25AD)
        {
            Name = "Ophidian Warrior";
            Weight = 5.0;
        }

        public OphidianWarriorStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0x27B, 0x27C, 0x27D, 0x27E, 0x27F
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

    public class OphidianArchMageStatuette : BaseStatuette
    {

        [Constructable]
        public OphidianArchMageStatuette()
            : base(0x25A9)
        {
            Name = "Ophidian Arch Mage";
            Weight = 5.0;
        }

        public OphidianArchMageStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0x280, 0x281, 0x282, 0x283, 0x284
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

            if (ItemID == 0x25AB)
                ItemID = 0x25A9;
        }
    }

    public class OphidianKnightStatuette : BaseStatuette
    {

        [Constructable]
        public OphidianKnightStatuette()
            : base(0x25AA)
        {
            Name = "Ophidian Knight";
            Weight = 5.0;
        }

        public OphidianKnightStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0x27B, 0x27C, 0x27D, 0x27E, 0x27F
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

    public class OphidianMageStatuette : BaseStatuette
    {

        [Constructable]
        public OphidianMageStatuette()
            : base(0x25AB)
        {
            Name = "Ophidian Mage";
            Weight = 5.0;
        }

        public OphidianMageStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0x280, 0x281, 0x282, 0x283, 0x284
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

    public class MinotaurStatuette : BaseStatuette
    {

        [Constructable]
        public MinotaurStatuette()
            : base(0x2D89)
        {
            Name = "Minotaur Statuette";
            Weight = 1.0;
        }

        public MinotaurStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0x596, 0x597, 0x598, 0x599, 0x59A, 0x59B, 0x59C, 0x59D
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

    public class DreadSpiderStatuette : BaseStatuette
    {

        [Constructable]
        public DreadSpiderStatuette()
            : base(0x25C4)
        {
            Name = "Dread Spider";
            Weight = 5.0;
        }

        public DreadSpiderStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0x493, 0x494, 0x495, 0x496, 0x497
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

    public class SlimeStatuette : BaseStatuette
    {

        [Constructable]
        public SlimeStatuette()
            : base(0x20E8)
        {
            Hue = Utility.RandomList(0x899, 0x8A2, 0x8B0);
            Name = "Slime Statuette";
            Weight = 1.0;
        }

        public SlimeStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
			0x1C9, 0x1CA, 0x1CB, 0x1CC, 0x1CD
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

    public class DreadHornStatuette : BaseStatuette
    {

        [Constructable]
        public DreadHornStatuette()
            : base(0x2D83)
        {
            Name = "Dread Horn";
            Weight = 5.0;
        }

        public DreadHornStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0xA8, 0xA9, 0xAA, 0xAB, 0xAC
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

    public class RedDeathStatuette : BaseStatuette
    {

        [Constructable]
        public RedDeathStatuette()
            : base(0x2617)
        {
            Name = "Red Death";
            Weight = 5.0;
        }

        public RedDeathStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0xE5, 0xE6, 0xE7, 0xE8, 0xE9
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

    public class PlagueBeastStatuette : BaseStatuette
    {

        [Constructable]
        public PlagueBeastStatuette()
            : base(0x2613)
        {
            Name = "Plague Beast";
            Weight = 5.0;
        }

        public PlagueBeastStatuette(Serial serial)
            : base(serial)
        {
        }

        private static int[] m_Sounds = new int[]
		{
            0x1BF, 0x1C0, 0x1C1, 0x1C2
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
