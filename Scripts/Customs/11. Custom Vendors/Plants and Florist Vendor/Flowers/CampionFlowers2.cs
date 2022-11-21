using System;

namespace Server.Items
{

public class CampionFlowers2 : Item
	{
		[Constructable]
		public CampionFlowers2() : base( 3209 )
		{
			Name = "Campion Flowers";
			Weight = 1.0;
		}

		public CampionFlowers2( Serial serial ) : base( serial )
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
