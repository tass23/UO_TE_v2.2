using System;
using Server;

namespace Server.Items
{
	#region Bingo Card 1
	public class BingoCard1 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 1", "",				
			new BookPageInfo
			(
				"B   I   N   G   O ",
				"9  18  32  56  63",
				"14  21  34  55  73",
				"12  30  F  48  74",
				"8  28  42  49  71",
				"7  23  36  46  65"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard1() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
	#region Bingo Card 2
	public class BingoCard2 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 2", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"6  30  41   51  73",
				"9  22  37  47  64",
				"10  17  F   49  68",
				"7  24  32  57  67",
				"5  18  39  50  75"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard2() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 3
	public class BingoCard3 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 3", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"2  25  36  57  68",
				"14  18  35  50  72",
				"10  21  F   49  71",
				"11  17  33  51  62",
				"7  16  40  46  73"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard3() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard3( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 4
	public class BingoCard4 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 4", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"12  20 44  56  71",
				"5  18  41  55  66",
				"1  29   F  54  72",
				"8  17  32  60  62",
				"9  19  42  48  73"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard4() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard4( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 5
	public class BingoCard5 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 5", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"13  22  42  56  65",
				"10  20  35  55  71",
				"8  24   F  58  63",
				"11  28  31  60  64",
				"3  23  33  59  73"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard5() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard5( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 6
	public class BingoCard6 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 6", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"10  27  37  58  69",
				"11  18  40  53  70",
				"9  25  F  59  73",
				"6  21  41  52  64",
				"12  24  33  51  62"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard6() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard6( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 7
	public class BingoCard7 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 7", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"5  26  39  47  64",
				"3  23  31  50  72",
				"14  18   F  59  70",
				"10  21  34  49  69",
				"6  29  44  57  67"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard7() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard7( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 8
	public class BingoCard8 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 8", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"4  22  38  46  71",
				"9  25  31  48  65",
				"12  18   F  49  70",
				"2  26  33  53  61",
				"11  20  36  50  63"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard8() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard8( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 9
	public class BingoCard9 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 9", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"12  28  41  52  63",
				"2  21  40  48  66",
				"9  20   F  56  65",
				"10  19  45  49  67",
				"8  21  32  59  70"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard9() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard9( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 10
	public class BingoCard10 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 10", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"10  28  42  53  74",
				"9  27  45  57  65",
				"3  21   F   51  70",
				"5  29  31   55  63",
				"12  24  44  58  71"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard10() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard10( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 11
	public class BingoCard11 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 11", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"1  23  38  58  71",
				"11  29  32  51  61",
				"2  19   F  49  64",
				"7  21  42  48  67",
				"5  20  36  54  65"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard11() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard11( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 12
	public class BingoCard12 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 12", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"11  20  45  48  65",
				"7  26  31  52  74",
				"3  24   F  49  63",
				"2  29  32  56  73",
				"5  22  34  54  67"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard12() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard12( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 13
	public class BingoCard13 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 13", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"2   21  31  60  69",
				"12  23  34  52  73",
				"9   18   F  56  74",
				"14  22  38  47  71",
				"8   16  41   50  61"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard13() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard13( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 14
	public class BingoCard14 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 14", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"8  26  31  51  66",
				"9  21  33  58  62",
				"14  28  F  55  69",
				"4  18  34  48  70",
				"7  19  36  57  74"				  
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard14() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard14( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 15
	public class BingoCard15 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 15", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"10  28  42  55  72",
				"2  26  33  54  62",
				"12  29  F   51  64",
				"14  18  34  53  67",
				"8  20  35  58  68"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard15() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard15( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 16
	public class BingoCard16 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 16", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"5  28  40  55  66",
				"4  22  42  56  65",
				"13  17   F  46  68",
				"12  29  37  51  61",
				"6  23  35  49  63"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard16() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard16( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 17
	public class BingoCard17 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 17", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"8  21  38  47  63",
				"11  18  36  48  62",
				"13  25  F  60  67",
				"4  24  35  58  65",
				"12  27  32  49  68"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard17() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard17( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
    #region Bingo Card 18
	public class BingoCard18 : BlueBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bingo Card 18", "",				
			new BookPageInfo
			(
				"B   I   N   G   O",
				"9  26  43  58  73",
				"11  23  38  59  71",
				"10  18   F  55  64",
				"5  28  33  56  66",
				"3  21  44  54  65"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BingoCard18() : base( false )
		{
			Hue = 0x47E;
		}

		public BingoCard18( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion	
}