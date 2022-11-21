using System;

namespace Server.Items
{
	public class FordLetter : Item
	{
		[Constructable]
		public FordLetter( ) : base( 0xEF3 )
		{
			Weight = 1.0;
			Name = "Letter To Hank Ford";
		}

		public FordLetter( Serial serial ) : base( serial )
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