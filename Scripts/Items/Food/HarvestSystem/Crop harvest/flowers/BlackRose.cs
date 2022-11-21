using System;

namespace Server.Items
{
	public class BlackRose : Item
	{
		[Constructable]
		public BlackRose() : this( 1 ){}

		[Constructable]
		public BlackRose( int amount ) : base( 0x234B )
		{
			Name = "Black Rose";
			Hue = 2019;
		}

		public BlackRose( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 