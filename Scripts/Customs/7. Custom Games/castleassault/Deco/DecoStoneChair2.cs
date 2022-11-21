using System;
using Server;

namespace Server.Items
{
	public class DecoStoneChair2 : Item
	{
		[Constructable]
		public DecoStoneChair2() : base( 0x1218 )
		{
			Name = "a stone chair";
			Movable = false;
		}

		public DecoStoneChair2( Serial serial ) : base( serial )
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