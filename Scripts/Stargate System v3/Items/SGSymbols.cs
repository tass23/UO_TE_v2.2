using System;

namespace Server.Items
{
    public class SGAddressSymbol1 : Item
    {
        [Constructable]
        public SGAddressSymbol1()
            : base(3676)
        {
            Movable = false;
            Name = "Symbol 1";
            Hue = 1151;
        }

        public SGAddressSymbol1(Serial serial)
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

    public class SGAddressSymbol2 : Item
    {
        [Constructable]
        public SGAddressSymbol2()
            : base(3679)
        {
            Movable = false;
            Name = "Symbol 2";
            Hue = 1151;
        }

        public SGAddressSymbol2(Serial serial)
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

    public class SGAddressSymbol3 : Item
    {
        [Constructable]
        public SGAddressSymbol3()
            : base(3682)
        {
            Movable = false;
            Name = "Symbol 3";
            Hue = 1151;
        }

        public SGAddressSymbol3(Serial serial)
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

    public class SGAddressSymbol4 : Item
    {
        [Constructable]
        public SGAddressSymbol4()
            : base(3685)
        {
            Movable = false;
            Name = "Symbol 4";
            Hue = 1151;
        }

        public SGAddressSymbol4(Serial serial)
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

    public class SGAddressSymbol5 : Item
    {
        [Constructable]
        public SGAddressSymbol5()
            : base(3688)
        {
            Movable = false;
            Name = "Symbol 5";
            Hue = 1151;
        }

        public SGAddressSymbol5(Serial serial)
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

    public class SGFacetSymbol1 : Item
    {
        [Constructable]
        public SGFacetSymbol1()
            : base(3676)
        {
            Movable = false;
            Name = "Symbol 1";
            Hue = 1360;
        }

        public SGFacetSymbol1(Serial serial)
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

    public class SGFacetSymbol2 : Item
    {
        [Constructable]
        public SGFacetSymbol2()
            : base(3679)
        {
            Movable = false;
            Name = "Symbol 2";
            Hue = 1360;
        }

        public SGFacetSymbol2(Serial serial)
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

    public class SGFacetSymbol3 : Item
    {
        [Constructable]
        public SGFacetSymbol3()
            : base(3682)
        {
            Movable = false;
            Name = "Symbol 3";
            Hue = 1360;
        }

        public SGFacetSymbol3(Serial serial)
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

    public class SGFacetSymbol4 : Item
    {
        [Constructable]
        public SGFacetSymbol4()
            : base(3685)
        {
            Movable = false;
            Name = "Symbol 4";
            Hue = 1360;
        }

        public SGFacetSymbol4(Serial serial)
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

    public class SGFacetSymbol5 : Item
    {
        [Constructable]
        public SGFacetSymbol5()
            : base(3688)
        {
            Movable = false;
            Name = "Symbol 5";
            Hue = 1360;
        }

        public SGFacetSymbol5(Serial serial)
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