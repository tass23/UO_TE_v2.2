using System;
using Server;

namespace Server.Items
{
	public class WoodenHorseStatue : Item
	{
		[Constructable]
		public WoodenHorseStatue() : this( 1 )
		{
		}

		[Constructable]
		public WoodenHorseStatue( int amount ) : base( 0x3FFE )
		{
			Weight = 10.0;
			Name = "wooden horse statue";
		}

		public WoodenHorseStatue( Serial serial ) : base( serial )
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