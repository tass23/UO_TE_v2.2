using System;
using Server;

namespace Server.Items
{
	public class HorseSaddle2 : Item
	{
		public override int LabelNumber{ get{ return 3895; } }
		
		[Constructable]
		public HorseSaddle2() : base( 0xF37 )
		{
			Name = "Horse Saddle";
			Weight = 5; 
		}

		public HorseSaddle2( Serial serial ) : base( serial )
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