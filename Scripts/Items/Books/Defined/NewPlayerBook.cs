using System;
using Server;

namespace Server.Items
{
	public class NewPlayerBook : RedBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"A Welcome", "Lord Raist",
				new BookPageInfo
				(
					"  Greetings to you,",
					"new member of The",
					"Expanse.",
					"  This book will",
					"serve to give you",
					"some guidance",
					"about this shard",
					"and it's purpose."
				),
				new BookPageInfo
				(
					"Stargates-",
					"Stargates are placed",
					"in strategic locations",
					"around the world.",
					"You can simply dial",
					"in your destination",
					"and step through the",
					"portal."
				),
				new BookPageInfo
				(
					"  You can get to most",
					"dungeons by using",
					"the Stargate system.",
					"Stargates don't cost",
					"anything to use and",
					"several people can go",
					"through at one time."
				),
				new BookPageInfo
				(
					"New Haven-",
					"Here in town there",
					"are lots of things",
					"to do to get you",
					"started on your path",
					"to adventure!",
					"You'll want to start",
					"by picking up the"
				),
				new BookPageInfo
				(
					"various skill",
					"training quests from",
					"the NPCs. These will",
					"help you prepare to",
					"enter the New Player",
					"Dungeon south of town.",
					"You'll also want to",
					"make a stop at the"
				),
				new BookPageInfo
				(
					"Donation Chest to",
					"gather gear that you",
					"will need. "
				),
				new BookPageInfo
				(
					" East of town is Old",
					"Haven, where the dead",
					"control the very streets",
					"and bandits roam the",
					"countryside. Take care"
				),
				new BookPageInfo
				(
					"in those places.",
					" I can tell you're",
					"anxious to get started,",
					"but don't forget",
					"to insure your gear",
					"before you go hunting.",
					" Skill Ball and Stat Ball-",
					" In your backpack"
				),
				new BookPageInfo
				(
					"you will find a stat ball",
					",a 3x GM skill ball and three",
					"+75 bonus skill balls.",
					"These will help you",
					"get ready for adventure",
					"a bit faster than",
					"normal. Double click",
					"your stat ball now",
					"and enter your new stats."
				),
				new BookPageInfo
				(
					"Your stat total must",
					"be equal to 375.",
					" There now, feel",
					"a bit tougher?",
					" Good! Now double",
					"click your skill",
					"ball and select 3",
					"skills you would"
				),
				new BookPageInfo
				(
					"like to set to 100.",
					"Most players tend",
					"to pick the hardest",
					"skills to level,",
					" like taming and",
					"peacemaking.",
					" Once you've chosen",
					"your skills, you can use"
				),
				new BookPageInfo
				(
					"the bonus skill balls",
					"to increase skill points",
					"on other skills at any time.",
					"Now get some gear from the",
					"Donation Chest.",
					"Now lets go get some",
					"training quests.",
					" Just south of the"
				),
				new BookPageInfo
				(
					"entrance to the New",
					"Haven Bank stands",
					"the Focus Instructor,",
					"Sarsmea Smythe.",
					"Double click on her,",
					"read through her",
					"quest instructions",
					"then accept the quest."
				),
				new BookPageInfo
				(
					"Focus is a fairly",
					"easy skill to level",
					"up. You don't need",
					"to do anything for",
					"it to go up. Just",
					"standing around in",
					"town will raise your",
					"Focus skill."
				),
				new BookPageInfo
				(
					"Once you've met the",
					"requirements for",
					"completing the Focus",
					"Quest, turn it in",
					"and check out your",
					"Reward Bag, might be",
					"something useful.",
					"If it's junk, there's"
				),
				new BookPageInfo
				(
					"a trash barrel on",
					"the north side of",
					"the New Haven Bank,",
					"just next to Fraiser",
					"Crane. Fraiser is a",
					"great guy to get to",
					"know. He'll give you",
					"quests for small"
				),
				new BookPageInfo
				(
					"amounts of gold.",
					" All around New Haven",
					"you will find helpful",
					"newbie quests.",
					" In addition",
					"the New Player Dungeon",
					"in the mountains to,",
					"south will help you"
				),
				new BookPageInfo
				(
					"train your skills even",
					"faster than you could",
					"out in the world.",
					" That's about all",
					"you need to know to",
					"get started adventuring.",
					" Here are some handy",
					"commands you'll want"
				),
				new BookPageInfo
				(
					"to learn to use:",
					" [roll will 'roll'",
					"a random number for",
					"party playing.",
					" [Bondtime will",
					"tell you how long",
					"until a pet you tamed"
				),
				new BookPageInfo
				(
					"is ready to bond.",
					" [Chat + message will",
					"allow you to speak in",
					"regional public chat",
					"with anyone else in that",
					"region. [Mail will open",
					"your message inbox and",
					"allow you to send"
				),
				new BookPageInfo
				(
					"new messages.",
					"[Help + message will",
					"allow you to seek help",
					"from a staff member.",
					"Public Chat ([chat) is",
					"only available in New",
					"Haven."
				),
				new BookPageInfo
				(
					"That's about all the",
					"wisdom that I can",
					"give you at this time,",
					"except, don't forget to",
					"register on the forums!"
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public NewPlayerBook() : base( false )
		{
			Hue = 0x89B;
		}

		public NewPlayerBook( Serial serial ) : base( serial )
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