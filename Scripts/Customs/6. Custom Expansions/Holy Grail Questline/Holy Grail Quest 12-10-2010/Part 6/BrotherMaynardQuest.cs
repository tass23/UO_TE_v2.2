using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class BrotherMaynardQuest : BaseQuest
	{
		public override object Title{ get{ return "Brother Maynard"; } }

		public override object Description
		{
			get
			{
				return
					"*You approach Brother Maynard*<BR><BR>" +
					"You seek the Holy Hand Grenade!<BR><BR>" +
					"In order to use this, you must Consult the Book of Armaments!<BR><BR>" +
					"Armaments, Chapter Two, Verses Nine to Twenty-One.<BR>" +
					"And Saint Atila raised the hand grenade up on high, saying," +
					" 'Oh, Lord, bless this thy hand grenade that with it thou mayest blow" +
					" thy enemies to tiny bits, in thy mercy.'  And the Lord did grin, and" +
					"  people did feast upon the lambs, and sloths, and carp, and anchovies.<BR><BR>" +
					"And the Lord spake, saying, 'First shalt thou take out the" +
					" Holy Pin.  Then, shalt thou count to three, no more, no less.  Three" +
					" shalt be the number thou shalt count, and the number of the counting" +
					" shalt be three.  Four shalt thou not count, nor either count thou two," +
					" excepting that thou then proceed to three.  Five is right out.  Once" +
					" the number three, being the third number, be reached, then lobbest thou" +
					" thy Holy Hand Grenade of Antioch towards thou foe, who being naughty" +
					" in my sight, shall snuff it.<BR><BR>" +
					"Amen....<BR><BR>" +
					"Now, lead me to the castle.<BR><BR>";
			}
		}

		public override object Refuse
		{
			get
			{
				return "You weren't paying attantion were you, I knew it, you look confused!";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "Not 'One... two... five!' I said THREE you sot.";
			}
		}

		public BrotherMaynardQuest() : base()
		{
			AddObjective( new EscortObjective( "Throne Room" ) );
			AddReward( new BaseReward( typeof( HolyHandgrenade ), "Holy Hand Grenade" ) );
		}

		public override void GiveRewards()
		{
			base.GiveRewards();
			Owner.SendMessage( "BROTHER MAYNARD: 'What an idiot...King? HA!'", null, 0xEF3 );
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

	public class BrotherMaynard : BaseEscort
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( BrotherMaynardQuest )
			};}
		}

		[Constructable]
		public BrotherMaynard() : base()
		{
			Name = "Brother Maynard";
			Title = "the Scholar";
			NameHue = 68;
		}

		public BrotherMaynard( Serial serial ) : base( serial )
		{
		}
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			Female = false;
			CantWalk = false;
			Race = Race.Human;
			Hue = 0x8400;		
		}

		public override void InitOutfit()
		{
			AddItem( new Backpack() );		
			AddItem( new RobeOfTheEclipse() );
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