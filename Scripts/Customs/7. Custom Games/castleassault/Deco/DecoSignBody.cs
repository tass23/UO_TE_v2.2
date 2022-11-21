using System;
using Server;

namespace Server.Items
{
	public class DecoSignBody : Item
	{
		[Constructable]
		public DecoSignBody() : base( 0x1b1d )
		{
			Name = "Dead hero";
			Movable = false;
		}

		public DecoSignBody( Serial serial ) : base( serial )
		{
		}

		public override bool Decays
		{
			get	{ return false;	}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}