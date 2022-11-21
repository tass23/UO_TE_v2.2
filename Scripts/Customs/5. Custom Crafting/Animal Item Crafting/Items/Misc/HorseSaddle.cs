using System;
using Server;

namespace Server.Items
{
	public class HorseSaddle1 : Item
	{
		public override int LabelNumber{ get{ return 3896; } }
		
		[Constructable]
		public HorseSaddle1() : base( 0xF38 )
		{
			Name = "Horse Saddle";
			Weight = 5; 
		}

		public HorseSaddle1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}