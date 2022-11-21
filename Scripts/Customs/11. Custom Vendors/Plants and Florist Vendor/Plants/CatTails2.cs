using System;

namespace Server.Items
{

public class CatTails2 : Item
	{
		[Constructable]
		public CatTails2() : base( 3256 )
		{
			Name = "CatTails";
			Weight = 1.0;
		}

		public CatTails2( Serial serial ) : base( serial )
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
