using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute(0x364B, 0x369B)]
	public class FireDemonStatue : Item
	{
		[Constructable]
		public FireDemonStatue() : base(0x364B)
		{
			Movable = true;
			Name = "Fire Demon Statue";
		}

		public FireDemonStatue(Serial serial) : base(serial)
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

			if ( Weight == 6.0 )
				Weight = 10.0;
		}
	}

	public class SpikeColumn : Item
	{
		[Constructable]
		public SpikeColumn() : base(0x364C)
		{
			Movable = true;
                        Name = "Spike Column";
		}

		public SpikeColumn(Serial serial) : base(serial)
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

			if ( Weight == 6.0 )
				Weight = 10.0;
		}
	}
	
	[FlipableAttribute(0x364D, 0x369C)]
	public class SpikePost : Item
	{
		[Constructable]
		public SpikePost() : base(0x364D)
		{
			Movable = true;
			Name = "Spike Post";
		}

		public SpikePost(Serial serial) : base(serial)
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

			if ( Weight == 6.0 )
				Weight = 10.0;
		}
	}
	
	public class ObsidianRock : Item
	{
		[Constructable]
		public ObsidianRock() : base(0x364E)
		{
			Movable = true;
			Name = "Obsidian Rock";
		}

		public ObsidianRock(Serial serial) : base(serial)
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

			if ( Weight == 6.0 )
				Weight = 10.0;
		}
	}
	
	public class ObsidianPillar : Item
	{
		[Constructable]
		public ObsidianPillar() : base(0x364F)
		{
			Movable = true;
			Name = "Obsidian Pillar";
		}

		public ObsidianPillar(Serial serial) : base(serial)
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

			if ( Weight == 6.0 )
				Weight = 10.0;
		}
	}
	
	public class ShadowPillar : Item
	{
		[Constructable]
		public ShadowPillar() : base(0x3650)
		{
			Movable = true;
			Name = "Shadow Pillar";
		}

		public ShadowPillar(Serial serial) : base(serial)
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

			if ( Weight == 6.0 )
				Weight = 10.0;
		}
	}
  }
