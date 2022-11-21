using System;
using Server.Network;

namespace Server.Items
{

	public class eggeaster : Item
	{

		[Constructable]
		public eggeaster() : base( 0x9B5 )
		{
			Name = "dyed easter eggs";
			this.Weight = 1.0;
			Hue = Utility.RandomList ( 21, 91, 1152, 1260, 1167, 220, 33, 31, 78 );
		}

		public eggeaster( Serial serial ) : base( serial )
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