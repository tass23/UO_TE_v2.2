using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ShrineMantra : BrownBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Shrine of Singularity Mantra", "Naxatillor",
				new BookPageInfo
				(
					"unorus"
                )               				     
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public ShrineMantra() : base( false )
		{
            Hue = 2210;
		}

		public ShrineMantra( Serial serial ) : base( serial )
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