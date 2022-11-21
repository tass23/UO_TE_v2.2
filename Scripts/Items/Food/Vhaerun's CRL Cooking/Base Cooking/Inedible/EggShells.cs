using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Eggshells : Item
	{
		[Constructable]
		public Eggshells() : this( 0 )
		{
		}

		[Constructable]
		public Eggshells( int hue ) : base( 0x9b4 )
		{
			Hue = hue;
			Weight = 0.5;
		}

		public Eggshells( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}