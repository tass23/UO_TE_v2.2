using System;

namespace Server.Items
{
	public class Pansy : Item
	{
		[Constructable]
		public Pansy() : this( 1 ){}

		[Constructable]
		public Pansy( int amount ) : base( 0x234B )
		{
			Name = "Pansy";
			Hue = 13;
		}

		public Pansy( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 