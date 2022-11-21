using System;

namespace Server.Items
{
	public class SpiritRose : Item
	{
		[Constructable]
		public SpiritRose() : this( 1 ){}

		[Constructable]
		public SpiritRose( int amount ) : base( 0x234B )
		{
			Name = "Spirit Rose";
			Hue = 1465;
		}

		public SpiritRose( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 