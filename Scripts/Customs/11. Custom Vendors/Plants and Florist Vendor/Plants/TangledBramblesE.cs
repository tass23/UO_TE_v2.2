using System;

namespace Server.Items
{

public class TangledBramblesE : Item
	{
		[Constructable]
		public TangledBramblesE() : base( 12322 )
		{
			Name = "Brambles";
			Weight = 1.0;
		}

		public TangledBramblesE( Serial serial ) : base( serial )
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
