using System;
using Server;

namespace Server.Items
{
	public class ChildBracelet : BaseBracelet
	{
		[Constructable]
		public ChildBracelet() : base( 0x1086 )
		{
			Weight = 0.1;
			Hue = 2402;
			Name = "Captive Child's Bracelet";
		}

		public ChildBracelet( Serial serial ) : base( serial )
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