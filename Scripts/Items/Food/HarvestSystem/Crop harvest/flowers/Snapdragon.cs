using System;

namespace Server.Items
{
	public class Snapdragon : Item
	{
		[Constructable]
		public Snapdragon() : this( 1 ){}

		[Constructable]
		public Snapdragon( int amount ) : base( 0x234B )
		{
			Name = "Snapdragon";
			Hue = 283;
		}

		public Snapdragon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 