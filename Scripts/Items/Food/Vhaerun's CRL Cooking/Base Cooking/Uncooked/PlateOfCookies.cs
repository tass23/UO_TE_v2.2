using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class PlateOfCookies : Food
	{
		[Constructable]
		public PlateOfCookies() : this( 1 )
		{
		}

		[Constructable]
		public PlateOfCookies( int amount ) : base( amount, 0x160C )
		{
			this.Weight = 0.2;
			this.FillFactor = 1;
			this.Name = "plate of cookies";
		}

		public PlateOfCookies( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}
	}
}