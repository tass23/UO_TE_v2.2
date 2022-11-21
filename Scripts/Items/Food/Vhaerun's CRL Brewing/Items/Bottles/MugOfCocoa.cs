using System;
using Server;

namespace Server.Items
{
	public class MugOfCocoa : BaseCraftCocoa
	{
		public override Item EmptyItem{ get { return new PewterMug(); } }

		[Constructable]
		public MugOfCocoa() : base( 0xFFF )
		{
			this.Name = "Mug of Cocoa";
			this.Weight = 3.0;
			this.FillFactor = 3;
		}

		public MugOfCocoa( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}