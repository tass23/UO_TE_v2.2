using System;
using Server;

namespace Server.Items
{
	public class WaterLoggedNotebook : RedBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Gravewater Expedition", "Jonesy Ford",
				new BookPageInfo
				(
					"  My father was",
					"determined to locate a",
					"map, he said, contained",
					"the location of a",
					"shipwreck, the remains",
					"of the fated HMS",
					"Effort."
				),
				new BookPageInfo
				(
					"  After a lengthy",
					"round of drinks at",
					"Marion's bar, with a",
					"fellow we didn't",
					"know, the hiding place",
					"of this map was",
					"revealed."
				),
				new BookPageInfo
				(
					"  The next day,",
					"I met Will Stray..."
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public WaterLoggedNotebook() : base( false )
		{
			Hue = 642;
		}

		public WaterLoggedNotebook( Serial serial ) : base( serial )
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