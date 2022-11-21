using System;
using Server;

namespace Server.Items
{
	public class JugOfCider : BaseCraftCider
	{
		public override Item EmptyItem{ get { return new EmptyJug(); } }

		[Constructable]
		public JugOfCider() : base( 0x9C8 )
		{
			this.Name = "Jug of Cider";
			this.Weight = 3.0;
			this.FillFactor = 3;
		}

		public JugOfCider( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}