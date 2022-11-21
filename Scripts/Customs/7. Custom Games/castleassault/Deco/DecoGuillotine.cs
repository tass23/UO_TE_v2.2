using System;
using Server;

namespace Server.Items
{
	public class DecoGuillotine : Item
	{
		[Constructable]
		public DecoGuillotine() : base( 0x1230 )
		{
			Name = "a guillotine";
			Movable = false;
		}

		public DecoGuillotine( Serial serial ) : base( serial )
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