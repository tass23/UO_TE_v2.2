using System;

namespace Server.Items
{
	public class WinecrafterYeast : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		//string ICommodity.Description { get { return String.Format( Amount == 1 ? "{0} Yeast" : "{0} Yeast", Amount ); } }

		[Constructable]
		public WinecrafterYeast() : this(1) { }

		[Constructable]
		public WinecrafterYeast(int amount) : base(4165)
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
			Name = "Yeast";
		}

		public WinecrafterYeast(Serial serial) : base(serial){}
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int) 0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
	}
}