using System;

namespace Server.Items
{

public class TangledBramblesS : Item
	{
		[Constructable]
		public TangledBramblesS() : base( 12321 )
		{
			Name = "Brambles";
			Weight = 1.0;
		}

		public TangledBramblesS( Serial serial ) : base( serial )
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
