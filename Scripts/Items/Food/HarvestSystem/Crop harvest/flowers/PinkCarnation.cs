using System;

namespace Server.Items
{
	public class PinkCarnation : Item
	{
		[Constructable]
		public PinkCarnation() : this( 1 ){}

		[Constructable]
		public PinkCarnation( int amount ) : base( 0x234B )
		{
			Name = "Pink Carnation";
			Hue = 26;
		}

		public PinkCarnation( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
} 