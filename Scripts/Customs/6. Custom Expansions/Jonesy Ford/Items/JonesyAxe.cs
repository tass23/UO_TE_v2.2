using System;
using Server;

namespace Server.Items
{
	public class JonesyAxe : Item
	{
		[Constructable]
		public JonesyAxe() : this( 180 )
		{
		}

		[Constructable]
		public JonesyAxe( int uses ) : base( 0xE86 )
		{
			Name = "Jonesy's Lucky Pickaxe";
			Weight = 11.0;
			Hue = 0x973;
		}

		public JonesyAxe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}