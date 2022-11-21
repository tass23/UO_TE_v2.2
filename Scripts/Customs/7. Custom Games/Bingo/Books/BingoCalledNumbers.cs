using System;
using Server;

namespace Server.Items
{
	public class BingoCalledNumbers : BlueBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Bingo Called Numbers", "",
				new BookPageInfo
				(
				                "Keep track of",
                                                                                       "called numbers here.",
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
		public BingoCalledNumbers() : base( true )
		{
			Hue = 0x489;
		}

		public BingoCalledNumbers( Serial serial ) : base( serial )
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