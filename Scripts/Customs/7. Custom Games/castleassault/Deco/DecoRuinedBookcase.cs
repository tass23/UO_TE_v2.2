using System;
using Server;

namespace Server.Items
{
	public class DecoRuinedBookcase : Item
	{
		[Constructable]
		public DecoRuinedBookcase() : base( 0xc14 )
		{
			Name = "a ruined bookcase";
			Movable = false;
		}

		public DecoRuinedBookcase( Serial serial ) : base( serial )
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