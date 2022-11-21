using System;
using Server;

namespace Server.Items
{
	public class Organics : Item
	{
		[Constructable]
		public Organics() : this( 1 )
		{
		}

		[Constructable]
		public Organics( int amount ) : base( 0xE1F )
		{
			Stackable = true;
			Weight = 0.0;
			Amount = amount;
			Name = "organic material";
			Hue = 543;
		}

		public Organics( Serial serial ) : base( serial )
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