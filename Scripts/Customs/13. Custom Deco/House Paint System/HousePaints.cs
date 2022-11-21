using System;
using Server;

namespace Server.Items
{
    public class WoodHousePaint : HousePaint
    {
        public override bool AllowWood { get { return true; } }

        public override CustomHuePicker CustomHuePicker { get { return CustomHuePicker.WoodPaint; } }

        [Constructable]
        public WoodHousePaint()
        {
            Name = "Wood House Paint";
        }

        public WoodHousePaint(Serial serial)
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

    public class StoneHousePaint : HousePaint
    {
        public override bool AllowStone { get { return true; } }

        public override CustomHuePicker CustomHuePicker { get { return CustomHuePicker.StonePaint; } }

        [Constructable]
        public StoneHousePaint()
        {
            Name = "Stone House Paint";
        }

        public StoneHousePaint(Serial serial)
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

    public class MarbleHousePaint : HousePaint
    {
        public override bool AllowMarble { get { return true; } }

        public override CustomHuePicker CustomHuePicker { get { return CustomHuePicker.MarblePaint; } }

        [Constructable]
        public MarbleHousePaint()
        {
            Name = "Marble House Paint";
        }

        public MarbleHousePaint(Serial serial)
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

    public class PlasterHousePaint : HousePaint
    {
        public override bool AllowPlaster { get { return true; } }

        public override CustomHuePicker CustomHuePicker { get { return CustomHuePicker.PlasterPaint; } }

        [Constructable]
        public PlasterHousePaint()
        {
            Name = "Plaster House Paint";
        }

        public PlasterHousePaint(Serial serial)
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

    public class SandstoneHousePaint : HousePaint
    {
        public override bool AllowSandstone { get { return true; } }

        public override CustomHuePicker CustomHuePicker { get { return CustomHuePicker.SandstonePaint; } }

        [Constructable]
        public SandstoneHousePaint()
        {
            Name = "Sandstone House Paint";
        }

        public SandstoneHousePaint(Serial serial)
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

    public class OtherHousePaint : HousePaint
    {
        public override bool AllowOther { get { return true; } }
        public override bool AllowRepaint { get { return false; } }

        public override CustomHuePicker CustomHuePicker { get { return CustomHuePicker.OtherWallPaint; } }

        [Constructable]
        public OtherHousePaint()
        {
            Name = "Other House Paint";
        }

        public OtherHousePaint(Serial serial)
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

    public class GMHousePaint : HousePaint
    {
        public override bool UsesCharges { get { return false; } }

        public override bool AllowWood { get { return true; } } //Wood walls and doors
        public override bool AllowStone { get { return true; } } //Stone walls and doors
        public override bool AllowMarble { get { return true; } } //Marble walls and doors
        public override bool AllowPlaster { get { return true; } } //Plaster and clay walls and doors
        public override bool AllowSandstone { get { return true; } } //Sandstone walls and doors
        public override bool AllowOther { get { return true; } } //Hide, Paper, Bamboo or Rattan walls and doors

        public override bool AllowRepaint { get { return true; } }

        public override CustomHuePicker CustomHuePicker { get { return CustomHuePicker.GMWallPaint; } }

        [Constructable]
        public GMHousePaint()
        {
            Name = "GM House Paint";
        }

        public GMHousePaint(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel < AccessLevel.GameMaster)
                return;

            base.OnDoubleClick(from);
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