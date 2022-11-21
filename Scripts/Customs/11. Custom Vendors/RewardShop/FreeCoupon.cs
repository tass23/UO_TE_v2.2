using System;
using Server.Network;
using Server.Prompts;
using Server.Items;

namespace Server.Items
{
	public class FreeCoupon : Item
	{
		public override string DefaultName
		{
			get { return "Coupon for a Free Item from the Online Store"; }
		}

		[Constructable]
		public FreeCoupon() : base( 0x14ED )
		{
			base.Weight = 1.0;
			Hue = 1460;
		}

		public FreeCoupon( Serial serial ) : base( serial )
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


