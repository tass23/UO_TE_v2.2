using System;
using Server;

namespace Server.Items
{
	public class BingoPlayerCardRegister : BlueBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Player / Card Number", "For Staff Use",
				new BookPageInfo
				(
				                "Enter player name and",
                                                                                       "card number issued",
                                                                                       "",
                                                                                       "",
                                                                                       "",
                                                                                       "",
                                                                                       "",
                                                                                       "",
                                                                                       ""
				),
				new BookPageInfo
				(
					"",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        ""
				),
				new BookPageInfo
				(
					"",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        ""
				),
				new BookPageInfo
				(
					"",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        ""
				),
                                                                      new BookPageInfo
				(
					"",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        ""
				),
                                                                      new BookPageInfo
				(
					"",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        ""
				),
                                                                      new BookPageInfo
				(
					"",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        ""
				),
                                                                      new BookPageInfo
				(
					"",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        ""
				),
                                                                      new BookPageInfo
				(
					"",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        "",
                                                                                        ""
				)								
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoPlayerCardRegister() : base( true )
		{
			Hue = 0x503;
		}

		public BingoPlayerCardRegister( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}                                                                                        