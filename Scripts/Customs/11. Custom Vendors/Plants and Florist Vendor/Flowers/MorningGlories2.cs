using System;

namespace Server.Items
{

public class MorningGlories2 : Item
	{
		[Constructable]
		public MorningGlories2() : base( 7949 )
		{
			Name = "Morning Glories";
			Weight = 1.0;
		}

		public MorningGlories2( Serial serial ) : base( serial )
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
