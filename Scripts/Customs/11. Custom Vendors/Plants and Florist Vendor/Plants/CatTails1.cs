using System;

namespace Server.Items
{

public class CatTails1 : Item
	{
		[Constructable]
		public CatTails1() : base( 3255 )
		{
			Name = "CatTails";
			Weight = 1.0;
		}

		public CatTails1( Serial serial ) : base( serial )
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
