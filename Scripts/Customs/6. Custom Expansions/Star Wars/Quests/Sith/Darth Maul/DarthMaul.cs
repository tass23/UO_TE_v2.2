using System;
using Server;

namespace Server.Items
{
	public class DarthMaul : Item
	{
		public override double DefaultWeight
		{
			get { return 50; }
		}

		[Constructable]
		public DarthMaul() : base( 0x1CC0 )
		{
			Name = "Darth Maul";
		}

		public DarthMaul( Serial serial ) : base( serial )
		{
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