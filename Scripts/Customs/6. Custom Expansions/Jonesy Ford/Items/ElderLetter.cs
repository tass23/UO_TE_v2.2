using System;

namespace Server.Items
{
	public class ElderLetter : Item
	{
		[Constructable]
		public ElderLetter( ) : base( 0xEF3 )
		{
			Weight = 1.0;
			Name = "Letter to Village Elder";
		}
		
		public ElderLetter( Serial serial ) : base( serial )
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