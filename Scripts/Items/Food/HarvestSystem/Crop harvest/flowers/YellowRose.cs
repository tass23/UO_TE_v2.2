using System;

namespace Server.Items
{
	public class YellowRose : Item
	{
		[Constructable]
		public YellowRose() : this( 1 ){}

		[Constructable]
		public YellowRose( int amount ) : base( 0x234B )
		{
			Name = "Yellow Rose";
			Hue = 53;
		}

		public YellowRose( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 