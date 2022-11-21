using System;

namespace Server.Items
{

public class WhiteLillies : Item
	{
		[Constructable]
		public WhiteLillies() : base( 3211 )
		{
			Name = "White Lillies";
			Weight = 1.0;
		}

		public WhiteLillies( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
