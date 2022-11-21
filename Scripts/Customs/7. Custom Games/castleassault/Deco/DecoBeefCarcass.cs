using System;
using Server;

namespace Server.Items
{
	public class DecoBeefCarcass : Item
	{
		[Constructable]
		public DecoBeefCarcass() : base( 0x1872 )
		{
			Name = "a beef carcass";
			Movable = false;
		}

		public DecoBeefCarcass( Serial serial ) : base( serial )
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