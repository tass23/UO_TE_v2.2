using System;
using Server;

namespace Server.Items
{
	public class MugOfTea : BaseCraftTea
	{
		public override Item EmptyItem{ get { return new PewterMug(); } }

		[Constructable]
		public MugOfTea() : base( 0xFFF )
		{
			this.Name = "Mug of Tea";
			this.Weight = 3.0;
			this.FillFactor = 3;
		}

		public MugOfTea( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}