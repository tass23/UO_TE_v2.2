using System;

namespace Server.Items
{
	public class Poppy : Item
	{
		[Constructable]
		public Poppy() : this( 1 ){}

		[Constructable]
		public Poppy( int amount ) : base( 0x234B )
		{
			Name = "Poppy";
			Hue = 56;
		}

		public Poppy( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 