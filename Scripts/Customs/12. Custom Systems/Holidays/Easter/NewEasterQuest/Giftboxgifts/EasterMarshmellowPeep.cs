using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class EasterMarshmellowPeep : Food
	{
		[Constructable]
		public EasterMarshmellowPeep() : this( 1 )
		{
		}

		[Constructable]
		public EasterMarshmellowPeep( int amount ) : base( amount, 0x20D1 )
		{
			this.Weight = 1;
			this.FillFactor = 2;
			this.Name = "Easter Marshmellow Peep";
			this.Hue = 2056;
		}

		public EasterMarshmellowPeep( Serial serial ) : base( serial )
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