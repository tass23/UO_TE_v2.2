using System;

namespace Server.Items
{

public class CampionFlowers1 : Item
	{
		[Constructable]
		public CampionFlowers1() : base( 3203 )
		{
			Name = "Campion Flowers";
			Weight = 1.0;
		}

		public CampionFlowers1( Serial serial ) : base( serial )
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
