using System;
using Server;

namespace Server.Items
{
	public class AncientCrown : Item
	{
		[Constructable]
		public AncientCrown() : base(12649)
		{
			Name = "an ancient crown";
			Hue = 1266;
		}
		
		public AncientCrown( Serial serial ) : base( serial )
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