using System;
using Server;

namespace Server.Items
{
	public class GaramonsBook : RedBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"A Journal", "Garamon",
				new BookPageInfo
				(
					"Today I have hope again.",
					"It has been too many",
					"days since my brother",
					"Tyball and I inadvertent",
					"ly released the Slasher",
					"of Veils in this world.",
                    "How could we have known",
                    "our research in planar",
                    "travel would have such",
                    "dire consequences?"
                ), 
                new BookPageInfo
                (
                    "But we have devised a",
                    "plan. We completed the",
                    "construction of a cham",
                    "ber of virtue. Tonight,",
                    "I will lure the Slasher",
                    "of Veils inside it so",
                    "it's virtuous energies",
                    "may weaken the beast!"
                ),
				new BookPageInfo
				(
					"Tyball will lock us in",
					"while I open a rip back",
					"to the Slasher's own",
					"plane, and lead him",
					"through. A portal already",
                    "awaits me in that foul",
                    "place, which will lead me",
                    "back here to safety."
				),
                new BookPageInfo
                (
                    "If all goes according to",
                    "plan, we will have undone",
                    "the wrong we brought onto",
                    "Britannia."
                ),
                new BookPageInfo
                (
                    "We will have redeemed",
                    "ourselves. May the Virtues",
                    "give us strength..."
                )
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public GaramonsBook() : base( false )
		{
		}

		public GaramonsBook( Serial serial ) : base( serial )
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

    public class GaramonsBook1 : BrownBook
    {
        public static readonly BookContent Content = new BookContent
            (
                "A Journal", "Tyball",
                new BookPageInfo
                (
                    "He speaks to me...",
                    "I can hear him. The",
                    "beast... He knows of",
                    "our plan. But how?",
                    "Such wonders, such",
                    "powers and such wealth",
                    "he promises. But at what"
                ),
                new BookPageInfo
                (
                    "cost? That it could ask",
                    "me to sacrifice my",
                    "brother I cannot. I will",
                    "not. No knowledge, no",
                    "greatness could warrant",
                    "such infamy.",

                    "But the seed of a doubt"
                ),
                new BookPageInfo
                (
                    "is gnawing at me. Did",
                    "the Slasher make the",
                    "same offer to my",
                    "brother? Is he playing",
                    "us against the other?",
                    "Would Garamon even enter",
                    "tain the thought? No, not",
                    "my virtuous brother if he"
                ),
                new BookPageInfo
                (
                    "did, I would need to",
                    "strike first, only to",
                    "protect myself of course.",

                    "I must banish the doubts",
                    "from my mind. They are",
                    "like poison. We cannot let",
                    "this fiend divide us."
                )
            );

        public override BookContent DefaultContent { get { return Content; } }

        [Constructable]
        public GaramonsBook1(): base(false)
        {
        }

        public GaramonsBook1(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}