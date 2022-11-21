using System;
using Server.Network;

namespace Server.Items
{
	public class Apricot : Food
	{
		[Constructable]
		public Apricot() : this( 1 )
		{
		}

		[Constructable]
		public Apricot( int amount ) : base( amount, 0x9D2 )
		{
			FillFactor = 2;
			Hue = 0x31;
			Name = "Apricot";
		}

		public Apricot( Serial serial ) : base( serial )
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