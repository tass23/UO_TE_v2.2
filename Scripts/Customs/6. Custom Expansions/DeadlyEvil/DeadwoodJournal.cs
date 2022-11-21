using System;
using Server;

namespace Server.Items
{
	public class CBookDeadwoodjournal : BaseBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Deadwood Journal", "Unknown",
			new BookPageInfo
			(
				"Head North-West from",
				"the Stone stairs, carry",
				"ye forth onward until",
				"upon the map a starry",
				"void appears. Turn",
				"hence northward and hug",
				"the curved contour",
				"where the trees meet"
			),
			new BookPageInfo
			(
				"the stars. Edge your",
				"way along until you",
				"find an abandoned ruin.",
				"Be it a stout-hearted",
				"soul who ventures",
				"inside, for it is said",
				"the most unholy of",
				"unholy resides within."
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public CBookDeadwoodjournal() : base( 0x2253, false )
		{
			Name = "Deadwood Journal";
			Hue = 321;
		}

		public CBookDeadwoodjournal( Serial serial ) : base( serial )
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