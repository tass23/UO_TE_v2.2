using System;
using Server;

namespace Server.Items
{
	public class DecoStoneTable2 : Item
	{
		[Constructable]
		public DecoStoneTable2() : base( 0x1203 )
		{
			Name = "a stone table";
			Movable = false;
		}

		public DecoStoneTable2(Serial serial ) : base( serial )
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