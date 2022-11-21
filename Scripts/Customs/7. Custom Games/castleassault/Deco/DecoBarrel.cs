using System;
using Server;

namespace Server.Items
{
	public class DecoBarrel : Item
	{
		[Constructable]
		public DecoBarrel() : base( 0xe77 )
		{
			Name = "a barrel";
			Movable = false;
		}

		public DecoBarrel( Serial serial ) : base( serial )
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