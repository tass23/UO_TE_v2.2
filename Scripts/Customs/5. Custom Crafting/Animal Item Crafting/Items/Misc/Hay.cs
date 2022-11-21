using System;
using Server;

namespace Server.Items
{
	public class Hay1 : Item
	{
		public override int LabelNumber{ get{ return 4108; } }
		
		[Constructable]
		public Hay1() : base( 0x100c )
		{
			Name = "Hay";
			Weight = 5; 
		}

		public Hay1( Serial serial ) : base( serial )
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