// Raspberries originally by Kajuk
using System;
using Server.Network;

namespace Server.Items
{
	public class RedRaspberry : Food
	{
		[Constructable]
		public RedRaspberry() : this( 1 )
		{
			Weight = 0.5;
			Hue = 0x26;
			Name = "red raspberry";
		}

		[Constructable]
		public RedRaspberry( int amount ) : base( amount, 0x9D1 )
		{
			Weight = 0.5;
			FillFactor = 2;
			Hue = 0x26;
			Name = "Red Raspberry";
		}

		public RedRaspberry( Serial serial ) : base( serial )
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

	public class BlackRaspberry : Food
	{
		[Constructable]
		public BlackRaspberry() : this( 1 )
		{
			Weight = 0.5;
			Hue = 1175;
			Name = "black raspberry";
		}

		[Constructable]
		public BlackRaspberry( int amount ) : base( amount, 0x9D1 )
		{
			Weight = 0.5;
			FillFactor = 2;
			Hue = 1175;
			Name = "Black Raspberry";
		}

		public BlackRaspberry( Serial serial ) : base( serial )
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
