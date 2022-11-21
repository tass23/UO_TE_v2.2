using System;

namespace Server.Items
{

public class JuniperBush : Item
	{
		[Constructable]
		public JuniperBush() : base( 3272 )
		{
			Name = "Juniper Bush";
			Weight = 1.0;
		}

		public JuniperBush( Serial serial ) : base( serial )
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
