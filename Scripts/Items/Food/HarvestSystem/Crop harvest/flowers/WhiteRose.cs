using System;

namespace Server.Items
{
	public class WhiteRose : Item
	{
		[Constructable]
		public WhiteRose() : this( 1 ){}

		[Constructable]
		public WhiteRose( int amount ) : base( 0x234B )
		{
			Name = "White Rose";
			Hue = 2101;
		}

		public WhiteRose( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 