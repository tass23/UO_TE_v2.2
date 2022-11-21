using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class ShrubberyQuest : BaseQuest
	{
		public override object Title{ get{ return "Roger the Shrubber"; } }

		public override object Description
		{
			get
			{
				return
					"So you SAY you are King of the Brits, eh?<BR>" +
					"Are you saying 'nee' to that old woman?<BR><BR>" +
					"Ah, now we see the violence inherent in the system.<BR><BR>" +
					"Oh, what sad times are these when passing ruffians can say `Nee'" +
					" at will to old ladies.  There is a pestilence upon this land, nothing" +
					" is sacred.  Even those who arrange and design shrubberies are under" +
					" considerable economic stress at this period in history.<BR><BR>" +
					"Yes, shrubberies are my trade -- I am a shrubber.<BR>" +
					"My name is Roger the Shrubber.  I arrange, design, and sell shrubberies.<BR><BR>" +
					"The Knights Who Say Ni have sent you in search of a shrubbery, a fine shrubbery.<BR><BR>" +
					"I shall design a fine shrubbery for you, unless I hear you say...`Ni'.<BR><BR>" +
					"Bring me 40 fertile dirt, an Inshave and a Dovetail Saw so I may properly prepare a shrubbery of superior design.<BR>" +
					"And promise to NEVER gain to say...'Ni'!";
			}
		}

		public override object Refuse
		{
			get
			{
				return "I see the continuing oppression and economic stress at this period in history shall continue.";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "'King of the Brits' eh? To 'uppity' to get your hands dirty?";
			}
		}

		public override object Complete
		{
			get
			{
				return "Here is your finely designed shrubbery.";
			}
		}

		public ShrubberyQuest() : base()
		{
			AddObjective( new ObtainObjective( typeof( FertileDirt ), "Fertile Dirt", 40 ) );
			AddObjective( new ObtainObjective( typeof( Inshave ), "Inshave", 1 ) );
			AddObjective( new ObtainObjective( typeof( DovetailSaw ), "Dovetail Saw", 1 ) );
			AddReward( new BaseReward( typeof( AShrubbery ), "A Shrubbery" ) ); 
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

	public class RogerTheShrubber : MondainQuester
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( ShrubberyQuest )
			};}
		}

		[Constructable]
		public RogerTheShrubber() : base( "Roger", "the Shrubber" )
		{
		}

		public RogerTheShrubber( Serial serial ) : base( serial )
		{
		}

		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			Female = false;
			CantWalk = false;
			Race = Race.Human;
			Hue = 0x8400;
			HairItemID = 0x203B;
			HairHue = 1881;
		}

		public override void InitOutfit()
		{
			AddItem( new Backpack() );
			AddItem( new Shoes( 1328 ) );
			AddItem( new Shirt() );
			AddItem( new ShortPants( 1328 ) );
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