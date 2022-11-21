using System;

namespace Server.Items
{
	[Furniture]
	[Flipable(0x118B, 0x118C)]
	public class OperatingTable : Item
	{
		[Constructable]
		public OperatingTable() : base(0x118B)
		{
			Name = "Operating Table";
			Hue = 1;
			Movable = false;
			Weight = 25.0;
		}

		public OperatingTable(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 26.0 )
				Weight = 25.0;
		}
	}
}