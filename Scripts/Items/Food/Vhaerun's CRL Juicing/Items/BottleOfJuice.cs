using System;
using Server;

namespace Server.Items
{
	public class BottleOfJuice : BaseCraftJuice
	{
		public override Item EmptyItem{ get { return new EmptyJuiceBottle(); } }

		[Constructable]
		public BottleOfJuice() : base( 0x99B )
		{
			this.Weight = 0.2;
			this.FillFactor = 3;
			this.Hue = 0x23;
		}

		public BottleOfJuice( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}