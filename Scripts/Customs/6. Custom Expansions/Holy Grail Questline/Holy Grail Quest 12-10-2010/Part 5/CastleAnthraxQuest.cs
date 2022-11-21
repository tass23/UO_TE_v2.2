using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class CastleAnthraxQuest : BaseQuest
	{
		public override object Title{ get{ return "The Castle Anthrax"; } }

		public override object Description
		{
			get
			{
				return
					"Welcome gentle Sir. We welcome to the Castle Anthrax.<BR><BR>" +
					"You say: 'The Castle Anthrax?'" +
					"Yes... oh, it's not a very good name?  Oh! but we are nice and we shall attend to your every, every need!<BR><BR>" +
					"You say: 'You are the keepers of the Holy Grail? I am here to rescue the purest of my knights, Sir Galahad!'<BR><BR>" +
					"The what? The Grail? Oh, but you are tired, and you must rest awhile.  Midget! Crepper! Prepare a bed for our guest.<BR><BR>" +
					"You reply: 'Well, look, I-I-uh--I must find Sir Galahad!'<BR><BR>" +
					"The beds here are warm and soft -- and...What is your name, handsome knight?<BR><BR>" +
					"You say: 'I am Arthur, King of the Brits, defender...<BR><BR>" +
					"Mine is Zoot... just Zoot. You would not be so ungallant as to refuse our hospitality?<BR>" +
					"Oh, but you are wounded! You must see the doctors immediately!  No, no, please, lie down.<BR><BR>" +
					"You say: 'L-look, I have seen it!  It is here, in the--Look, please!  In God's name, show me the Grail!<BR><BR>" +
					"Oh, come come, you must try to rest!  Doctor Piglet,  Doctor Winston, practice your art.<BR><BR>" +
					"You say: 'They're doctors?!'<BR><BR>" +
					"Uh, they've had a basic medical training, yes. Try to relax.<BR><BR>" +
					"You say: 'Sir Galahad! The Holy Grail! His last message said he saw it! The Grail is here!'<BR><BR>" +
					"Oh that is our beacon, which, I just remembered, is grail-shaped.  It's not the first time we've had this problem.<BR>" +
					"Oh, wicked, bad, naughty, evil girls!  Oh, they are naughty people, and they must pay the penalty -- and here in Castle Anthrax, we" +
					" have but one punishment for setting alight the grail-shaped beacon.  You must tie them down on a bed and spank them!<BR><BR>" +
					"You say: 'I have no time for this, Sir Galahad is in peril! I must have proof there is no Grail here!<BR><BR>" +
					"If you will not do as we ask, then you will have to make your way through the castle to find Sir Galahad.<BR><BR>" +
					"You say: 'I shall go in there and face the peril. I can defeat them! There's only a hundred and fifty of them!'";
			}
		}

		public override object Refuse
		{
			get
			{
				return "We knew you were not up to the task *giggle*";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "Yes you were, you were in terrible peril. Get back in there!";
			}
		}

		public override object Complete
		{
			get
			{
				return "Yes! You tackle us single-handed!.<BR>" +
					"Now won't you rest awhile?<BR><BR>" +
					"FINE!<BR><BR>" +
					"Be that way...but you will return wont you????<BR>" +
					"Here is your 'Grail'.<BR><BR>" +
					"... and bring your friends!!!!";
			}
		}

		public CastleAnthraxQuest() : base()
		{
			AddObjective( new SlayObjective( typeof( CastleWomen ), "Anthrax Women", 30 ) );
			AddReward( new BaseReward( typeof( FakeGrail ), "'The' Grail" ) );
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class ZootQuester : MondainQuester
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( CastleAnthraxQuest )
			};}
		}
		
		[Constructable]
		public ZootQuester() : base( "Zoot", "the temptress" )
		{
		}
		
		public ZootQuester( Serial serial ) : base( serial )
		{
		}
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			Female = true;
			CantWalk = true;
			Race = Race.Human;
			Hue = 0x8400;
			HairItemID = 0x203C;
			HairHue = 2356;
		}

		public override void InitOutfit()
		{
			AddItem( new Backpack() );
			AddItem( new PlainDress(1166) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}