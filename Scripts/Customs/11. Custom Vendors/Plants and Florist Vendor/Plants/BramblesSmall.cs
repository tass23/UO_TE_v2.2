using System;

namespace Server.Items
{

public class BramblesSmall : Item
	{
		[Constructable]
		public BramblesSmall() : base( 3392 )
		{
			Name = "Brambles";
			Weight = 1.0;
		}

		public BramblesSmall( Serial serial ) : base( serial )
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
