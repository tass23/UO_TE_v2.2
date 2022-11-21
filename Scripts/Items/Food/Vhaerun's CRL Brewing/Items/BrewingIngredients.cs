using System;

namespace Server.Items
{
	public class Malt : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		//string ICommodity.Description { get { return String.Format( Amount == 1 ? "{0} malt" : "{0} malt", Amount ); } }

		[Constructable]
		public Malt() : this(1) { }

		[Constructable]
		public Malt(int amount) : base(0x103D)
		{
			Name = "Malt";
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public Malt(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int) 0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
	}

	public class BrewersYeast : Item
	{
		[Constructable]
		public BrewersYeast() : this(1) { }

		[Constructable]
		public BrewersYeast(int amount) : base(0x1039)
		{
			Name = "Brewers Yeast";
			Stackable = true;
			Weight = 6.0;
			Amount = amount;
		}

		public BrewersYeast(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int) 0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
	}

	public class Barley : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		//string ICommodity.Description { get { return String.Format( Amount == 1 ? "{0} barley" : "{0} barley", Amount ); } }

		[Constructable]
		public Barley() : this(1) { }

		[Constructable]
		public Barley(int amount) : base(0x103F)
		{
			Name = "Barley";
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public Barley(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int) 0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
	}
}