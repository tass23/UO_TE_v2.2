using System;
using Server;

namespace Server.Items
{
	public class DecoGruesome : Item
	{
		[Constructable]
		public DecoGruesome() : base( 0x429 )
		{
			Movable = false;
		}

		public DecoGruesome( Serial serial ) : base( serial )
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