using System;

namespace Server.Items
{

public class BramblesLarge : Item
	{
		[Constructable]
		public BramblesLarge() : base( 3391 )
		{
			Name = "Brambles";
			Weight = 1.0;
		}

		public BramblesLarge( Serial serial ) : base( serial )
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
