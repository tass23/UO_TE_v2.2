using System;

namespace Server.Items
{

public class TrimmedHedge1 : Item
	{
		[Constructable]
		public TrimmedHedge1() : base( 3215 )
		{
			Name = "A Finely Trimmed Hedge";
			Weight = 1.0;
		}

		public TrimmedHedge1( Serial serial ) : base( serial )
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
