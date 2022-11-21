using System;

namespace Server.Items
{

public class HorseShoe : Item
	{
		[Constructable]
		public HorseShoe() : base(0x1DB7)
		{
			Name = "Horse Shoes";
			Weight = 5;
			Hue = 51;
		}

		public HorseShoe(Serial serial) : base(serial)
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
		}
	}
}