using System;

namespace Server.Items
{

public class RosesofTrinsic1 : Item
	{
		[Constructable]
		public RosesofTrinsic1() : base( 9036 )
		{
			Name = "Roses of Trinsic";
			Weight = 1.0;
		}

		public RosesofTrinsic1( Serial serial ) : base( serial )
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
