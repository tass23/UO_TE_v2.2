using System;

namespace Server.Items
{
	public class MaryannNote : Item
	{

		[Constructable]
		public MaryannNote( ) : base( 0xEF3 )
		{
			Weight = 1.0;
			Name = "Note to Maryann";
		}
		
		public MaryannNote( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}