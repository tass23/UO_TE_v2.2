using System;

namespace Server.Items
{
	public class RedRose : Item
	{
		[Constructable]
		public RedRose() : this( 1 ){}

		[Constructable]
		public RedRose( int amount ) : base( 0x234B )
		{
			Name = "Red Rose";
			Hue = 33;
		}

		public RedRose( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 