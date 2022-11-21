using System;

namespace Server.Items
{
	public class JonesyLetter : Item
	{
		[Constructable]
		public JonesyLetter( ) : base( 0xEF3 )
		{
			Weight = 1.0;
			Name = "Letter to Jonesy";
		}

		public JonesyLetter( Serial serial ) : base( serial )
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