using System;
using Server;

namespace Server.Items
{
	public class DecoStump: Item
	{
		[Constructable]
		public DecoStump() : base( 0xe56 )
		{
			Name = "a stump";
			Movable = false;
		}

		public DecoStump( Serial serial ) : base( serial )
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