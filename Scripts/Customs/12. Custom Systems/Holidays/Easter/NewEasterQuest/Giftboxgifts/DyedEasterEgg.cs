using System;
using Server.Network;

namespace Server.Items
{

	public class Dyedeggeaster2 : Item
	{

		[Constructable]
		public Dyedeggeaster2() : base( 0x1728 )
		{
			Name = "Dyed Easter Egg";
			this.Weight = 1.0;
			Hue = Utility.RandomList ( 1167, 21, 91, 1166, 1360, 2084, 1993, 2961, 2962 );
		}

		public Dyedeggeaster2( Serial serial ) : base( serial )
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