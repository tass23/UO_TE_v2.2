using System;

namespace Server.Items
{

public class TangledBramblesN : Item
	{
		[Constructable]
		public TangledBramblesN() : base( 12320 )
		{
			Name = "Brambles";
			Weight = 1.0;
		}

		public TangledBramblesN( Serial serial ) : base( serial )
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
