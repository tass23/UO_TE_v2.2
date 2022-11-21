using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class Donuts : Food
	{
		[Constructable]
		public Donuts() : this( 1 )
		{
		}

		[Constructable]
		public Donuts( int amount ) : base( amount, 6867 )
		{
			this.Weight = 2.0;
			this.FillFactor = 3;
		}

		public Donuts( Serial serial ) : base( serial )
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