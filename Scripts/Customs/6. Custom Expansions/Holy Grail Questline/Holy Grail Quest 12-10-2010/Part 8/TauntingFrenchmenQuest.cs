using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class TauntingFrenchmenQuest : BaseQuest
	{
		public override object Title{ get{ return "The Castle Aaaaarrrrrrggghhh"; } }

		public override object Description
		{
			get
			{
				return
					"<I>*you see a group of french guards peering over the castle walls*</I><BR><BR>" +
					"You say: 'The Castle Aaaaarrrrrrggghhh.  Our quest is at an end!  God be praised! Almighty God, we thank Thee that Thou hast --- AAUGH!'<BR><BR>" +
					"'Allo, daffy English kaniggets and Monsieur Arthur-King, who is afraid of a duck, you know!  So, we French fellows out-wit you a second time!<BR><BR>" +
					"You say: 'How dare you profane this place with your presence!?  I command you, in the name of the Knights of Camelot, to open the doors of this sacred castle, to which God himself has guided us!'<BR><BR>" +
					"How you English say, I ocne more time-a unclog my nose in your direction, sons of a window-dresser!  So, you think you could" +
					" out-clever us French folk with your silly knees-bent running about advancing behavior! You heaving lot of second-hand electric donkey bottom biters.<BR><BR>" +
					"You say: 'In the name of the Lord, we demand entrance to this sacred castle!'<BR><BR>" +
					"No chance, English bed-wetting types.  I burst my pimples at you and call your daughter an unrequested silly thing.  You tiny-brained wipers of other people's bottoms!<BR><BR>" +
					"You say: 'If you do not open this door, we shall take this castle by force!'<BR><BR>" +
					"<I>*twong  baaaa* The frenchmen launch livestock over the walls at you*</I><BR><BR>" +
					"You say: 'In the name of God and the glory of our-- Right! That settles it!'<BR><BR>" +
					"Yes, this time and try any more or we fire arrows at the tops of your heads and make castanets out of your coconuts already!  Ha ha!<BR><BR>" +
      				"You say: 'Walk away.  Just ignore them.'<BR><BR>" +
      				"No, remain you illegitimate faced buggerfuls!  And, if you think you got nasty taunting this time, you ain't heard nothing yet!<BR>" +
					"Daffy English kaniggets!  Thpppt!<BR><BR>" +
					"You say: 'We shall attack at once!'<BR><BR>" ;
			}
		}

		public override object Refuse{ get{ return "I blow my nose at you, so-called Arthur-king, you and all your silly English kaniggets.  Thppppt!"; } }
		public override object Uncomplete{ get{ return "You don't frighten us, English pig-dogs!"; } }

		public TauntingFrenchmenQuest() : base()
		{
			AddObjective( new SlayObjective( typeof( FrenchmenGuards ), "Taunting Frenchmen", 20 ) );
			AddReward( new BaseReward( typeof( CowBola ), "A Gift from the Guards" ) );
		}

		public override void GiveRewards()
		{
			base.GiveRewards();
			Owner.SendMessage( "FRENCH GUARD: 'I don't want to talk to you no more, you empty headed animal food trough whopper!  I fart in your general direction!'", null, 0xEF3 ); // You have demonstrated your compassion!  Your kind actions have been noted.
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

	public class FrenchmenQuester : MondainQuester
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( TauntingFrenchmenQuest )
			};}
		}

		[Constructable]
		public FrenchmenQuester() : base( "Castle Guard", "- a taunting Frenchman" )
		{
		}

		public FrenchmenQuester( Serial serial ) : base( serial )
		{
		}

		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			Female = false;
			CantWalk = false;
			Race = Race.Human;
			Hue = 0x8400;	
			HairItemID = 0x2045;
			HairHue = 2408;
		}

		public override void InitOutfit()
		{
			AddItem( new Backpack() );

			Item helm = new ChainCoif();
			helm.Movable = false;
			helm.Hue = 2408;
			AddItem( helm );

			Item leg = new LeatherLegs();
			leg.Movable = false;
			leg.Hue = 2408;
			AddItem( leg );

			Item chest = new LeatherChest();
			chest.Movable = false;
			chest.Hue = 2408;
			AddItem( chest );

			Item arms = new LeatherArms();
			arms.Movable = false;
			arms.Hue = 2408;
			AddItem( arms );

			Item glove = new PlateGloves();
			glove.Movable = false;
			glove.Hue = 2408;
			AddItem( glove );

			Item neck = new PlateGorget();
			neck.Movable = false;
			neck.Hue = 2408;
			AddItem( neck );

			AddItem( new ThighBoots(2408) );
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