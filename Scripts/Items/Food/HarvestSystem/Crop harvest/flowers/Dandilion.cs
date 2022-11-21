using System;

namespace Server.Items
{
	public class Dandelion : Item
	{
		[Constructable]
		public Dandelion() : this( 1 ){}

		[Constructable]
		public Dandelion( int amount ) : base( 0x234B )
		{
			Name = "Dandelion";
			Hue = 53;
		}

		public Dandelion( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 