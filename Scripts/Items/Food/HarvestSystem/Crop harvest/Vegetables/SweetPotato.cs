using System;
using Server.Network;

namespace Server.Items
{
	public class SweetPotato : Food
	{
		[Constructable]
		public SweetPotato() : this( 1 )
		{
		}

		[Constructable]
		public SweetPotato( int amount ) : base( amount, 0xC64 )
		{
			FillFactor = 2;
			Name = "Sweet Potato";
			Hue = 0x45E;
		}

		public SweetPotato( Serial serial ) : base( serial )
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