using System;
using Server;

namespace Server.Items
{
	public class CBookShippingInstructions : BaseBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Shipping Instructions", "Toht",

			new BookPageInfo
			(
				"When you have received",
				"the medallion from my",
				"courier, take the boat",
				"and sail to the small",
				"island directly EAST of",
				"your camp. I shall be",
				"waiting there while the",
				"troops are loading the"
			),
			new BookPageInfo
			(
				"ship. Do not waste",
				"time. When you receive",
				"the medallion, sail",
				"immediately lest one of",
				"Jonesy's lackies gets",
				"to you first."
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public CBookShippingInstructions() : base( 0x2D9D, false )
		{
			Hue = 368;
		}

		public CBookShippingInstructions( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}