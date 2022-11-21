using System;
using Server;

namespace Server.Items
{
	public class TrickOrTreatBook2012 : RedBook
	{
		[Constructable]
		public TrickOrTreatBook2012() : base( "Trick or Treat - 2012", "Raist", 3, false )
		{
			ItemID = 0x225A;

			Pages[0].Lines = new string[]
				{
					"   Happy Halloween!  ",
					"from The Expanse!",
					"  Here is a trick or",
					"treat goodie bag.",
					"  You can use this",
					"bag to go trick or",
					"treating at the",
					"town vendors."					
				};

			Pages[1].Lines = new string[]
				{
					"  To do this, simply ",
					"walk up to any vendor",
					"in any town and say",
					"'trick or treat'.  The",
					"vendor's will give you",
					"goodies and you may",
					"even receive a special",
					"Halloween novelty item."
				};

			Pages[2].Lines = new string[]
				{
					"We have added some",
					"new items so go and",
					"  Have Fun! ",
					"Happy Halloween everyone!!"
				};
		}

		public TrickOrTreatBook2012( Serial serial ) : base( serial )
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
