using System;

namespace Server.Items
{

public class SingleRoseOfTrinsic : Item
	{
		[Constructable]
		public SingleRoseOfTrinsic() : base( 9035 )
		{
			Name = "A Rose of Trinsic";
			Weight = 1.0;
		}

		public SingleRoseOfTrinsic( Serial serial ) : base( serial )
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
