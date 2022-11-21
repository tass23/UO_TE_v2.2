using System;
using Server;

namespace Server.Items
{
	public class DecoBloodSmear : Item
	{
		[Constructable]
		public DecoBloodSmear() : base( 0x122f )
		{
			Name = "a blood";
			Movable = false;
		}

		public DecoBloodSmear( Serial serial ) : base( serial )
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