using System;

namespace Server.Items
{
	public class IrishRose : Item
	{
		[Constructable]
		public IrishRose() : this( 1 ){}

		[Constructable]
		public IrishRose( int amount ) : base( 0x234B )
		{
			Name = "Irish Rose";
			Hue = 25;
		}

		public IrishRose( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 