using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class NeeQuest : BaseQuest
	{
		public override object Title{ get{ return "The Knights Who Say 'Ni'! "; } }

		public override object Description
		{
			get
			{
				return
					"We are the Knights Who Say... Ni!<BR>" +
					"We are the keepers of the sacred words:  Ni, Pen, and Nee-wom!<BR>" +
					"The Knights Who Say Ni demand a sacrifice!<BR><BR>" +
					"You reply: 'Knights of Ni, we are but simple travellers who seek the enchanter who lives beyond these woods.'<BR><BR>" +
					"Ni!  Ni!  Ni!  Nee!<BR>" +
					"We shall say 'ni' again to you if you do not appease us.<BR>" +
					"We want... a shrubbery!<BR>" +
					"Ni!  Ni!<BR><BR>" +
					"<I>*You cower at the repeated words of the knights*</I><BR>" +
					"You say: 'Please, please!  No more!  We shall find a shrubbery!'<BR><BR>" +
					"You must return here with a shrubbery or else you will never pass through this wood alive!<BR>" +
					"One that looks nice.<BR><BR>" +
					"And not too expensive.<BR><BR>" +
					"And it must be from Roger in Trinsic.<BR><BR>" +
					"Now... go!";
			}
		}

		public override object Refuse
		{
			get
			{
				return "Ni!  Ni!  Ni!  Ni!";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "We shall say 'ni' again to you if you do not appease us.";
			}
		}

		public override object Complete
		{
			get
			{
				return "It is a good shrubbery. I like the laurels particularly. But there is one small problem.<BR>" +
					"We are now... no longer the Knights Who Say Ni.<BR><BR>" +
					"We are now the Knights Who Say... Ekke Ekke Ekke Ekke Ptangya Ziiinnggggggg Ni!.<BR><BR>" +
					"Therefore, we must give you a test.<BR>" +
					"Firstly, you must find... another shrubbery!<BR><BR>" +
					"Then, when you have found the shrubbery, you must place it here beside this shrubbery, only slightly higher so you get a two-level effect with a little path running down the middle.<BR><BR>" +
					"Then, when you have found the shrubbery, you must cut down the mightiest tree in the forest... with... a herring!<BR><BR>" +
					"You reply: 'Cut down a tree with a herring?  It can't be done.'<BR><BR>" +
					"Aaaaugh!  Aaaugh! Don't say that word!<BR><BR>" +
					"You reply: 'What word?' <BR><BR>" +
					"I cannot tell, suffice to say is one of the words the Knights of Ni cannot hear.<BR><BR>" +
					"You reply: 'How can we not say the word if you don't tell us what it is?' <BR><BR>" +
					"<I>*AARGH! AARGH!*</I><BR><BR>" +
					"You reply: 'What, `is'?'<BR><BR>" +
					"No, not `is' -- we couldn't get vary far in life not saying `is'.<BR>" +
					"Aaugh!  I said it!  I said it!  Ooh!  I said it again!<BR><BR>" +
					"You reply: <I>'Oh, stop it! Patsy!'</I><BR><BR>" +
					"You proceed back to Arthur's Court as you continue your quest for the Holy Grail.<BR>" +
					"Armed with...a Herring!";
			}
		}
		
		public NeeQuest() : base()
		{
			AddObjective( new ObtainObjective( typeof( AShrubbery ), "A Shrubbery", 1 ) );
			AddReward( new BaseReward( typeof( AHerring ), "'Ni' Item" ) );
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
	
	public class KnightOfNee : MondainQuester
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( NeeQuest )
			};}
		}

		[Constructable]
		public KnightOfNee() : base( "Knights", "of Ni" )
		{			
		}
		
		public KnightOfNee( Serial serial ) : base( serial )
		{
		}
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			Female = false;
			CantWalk = true;
			Race = Race.Human;
			Hue = 0x8400;			
			HairItemID = 0x203B;
			HairHue = 2409;
			FacialHairItemID = 0x203E;
			FacialHairHue = 2409;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );	

			Item helm = new DeerMask();
			helm.Movable = false;
			helm.Hue = 2051;
			AddItem( helm );

			Item leg = new PlateLegs();
			leg.Movable = false;
			leg.Hue = 2051;
			AddItem( leg );

			Item glove = new PlateGloves();
			glove.Movable = false;
			glove.Hue = 2051;
			AddItem( glove );

			Item neck = new PlateGorget();
			neck.Movable = false;
			neck.Hue = 2051;
			AddItem( neck );

			Item sword = new VikingSword();
			sword.Movable = false;
			sword.Hue = 2051;
			AddItem( sword );

			AddItem( new Robe( 2051 ) );
			AddItem( new Cloak( 2051 ) );
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