using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class HolyGrailQuest1 : BaseQuest
	{
		public override bool ForceRemember{ get{ return true; } }
		public override QuestChain ChainID{ get{ return QuestChain.HolyGrailQuest; } }
		public override Type NextQuest{ get{ return typeof( HolyGrailQuest2 ); } }
		public override object Title{ get{ return "The Holy Grail-Scene 1"; } }

		public override object Description
		{
			get
			{
				return
					"I am Arthur, King of the Britons.<BR><BR>" +
					"I have devoted my days in search of the rarest of items....The Holy Grail!<BR><BR>" +
					"But....well....It's not that I am afraid or anything like that out among my people.<BR><BR>" +
					"They adore me...HONEST! And I am really quite busy with..stuff...you know, 'KING' stuff. Well, no you wouldn't know I suppose." +
					" Which is fine, you being one of my subjects and all.<BR><BR>" +
					"So I have a proposal. I would like you to set out on my Quest to find the Grail. It will be <I>QUITE</I> easy and VERY safe I am sure, well pretty sure anyway.<BR><BR>" +
					"All you have to do is pretend to be me, King Arthur.<BR>" +
					"Simple enough really....don't really think anyone will know the difference, not that they DON'T adore me or anything.<BR><BR>" +
					"Just to be sure I will have you go speak to Bedevere the Guard at the stables in Vesper.<BR>" +
					"Yes....yes, that should be a good test. You need a Mighty Steed for this quest and they guard our finest stallions.<BR><BR>" +
					"If you fool, er convince them then you should be relatively fine..I think.<BR><BR>" +
					"Once you have your steed, return to me so I can be sure they gave you the mightiest steed in my Kingdom." +
					"I don't actually have anything for you to ride, so take these for now. I am sure they will do the trick.<BR><BR>" +
					"Now...off you go! Get moving, you have no time to waste!";
			}
		}

		public override object Refuse
		{
			get
			{
				return "<I>*I will never find a sucker, I mean subject to take on this task at this rate!*</I>";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "You silly sot! Go speak with Bedemir the Guard so you can get to Grail Hunting!";
			}
		}

		public override object Complete
		{
			get
			{
				return "*you hand Arthur the tiny 'mighty steed' statuette*<BR><BR>" +
					"Well, I certainly didn't see this one coming. He told you they were COMPLETELY out of 'Mighty Steeds'?<BR>" +
					"Not even a horse? The 3 legged mule wasn't available either, eh? Well this is most disappointing.<BR>" +
					"But we mustn't be delayed in our Quest for the Holy Grail!<BR>" +
					"And you wouldn't look very King-ish trying to ride that ridiculous thing anyway, now would you?<BR><BR>" +
					"Here take these back....yes...much better!<BR>" +
					"Go ahead and try them out again! Excellent! Much superior to some foul smelling horse!";
			}
		}
		
		public HolyGrailQuest1() : base()
		{
			AddObjective( new ObtainObjective( typeof( FineSteed ), "A 'Mighty Steed'", 1 ) );
			AddObjective( new ObtainObjective( typeof( HorseCoconuts ), "Coconuts of Clapping", 1 ) );
			AddReward( new BaseReward( typeof( HorseCoconuts2 ), "Scene 1 Completed" ) );
		}

		HorseCoconuts horse;

		public override void OnAccept()
		{
			horse = new HorseCoconuts();
			horse.QuestItem = false;

			if ( Owner.PlaceInBackpack( horse ) )
				base.OnAccept();
			else
			{
				horse.Delete();
				Owner.SendLocalizedMessage( 1075574 ); // Could not create all the necessary items. Your quest has not advanced.
			}
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
	
	public class KingArthurQuester : MondainQuester
	{
		public override Type[] Quests
		{
			get{ return new Type[] 
			{
				typeof( HolyGrailQuest1 )
			};}
		}
		
		[Constructable]
		public KingArthurQuester() : base( "Arthur", "King of the Brits" )
		{
		}
		
		public KingArthurQuester( Serial serial ) : base( serial )
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
			HairHue = 1854;
			FacialHairItemID = 0x204B;
			FacialHairHue = 1854;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );	
			AddItem( new Server.Items.KingArthurCrown() );
			
			Item leg = new StuddedLegs();
			leg.Movable = false;
			leg.Hue = 1836;
			AddItem( leg );
			
			Item glove = new StuddedGloves();
			glove.Movable = false;
			glove.Hue = 1836;
			AddItem( glove );
			
			Item neck = new StuddedGorget();
			neck.Movable = false;
			neck.Hue = 1836;
			AddItem( neck );

			Item arms = new StuddedArms();
			arms.Movable = false;
			arms.Hue = 1836;
			AddItem( arms );
			
			Item chest = new StuddedChest();
			chest.Movable = false;
			chest.Hue = 1836;
			AddItem( chest );
			
			AddItem( new Surcoat(1154) );	
			AddItem( new Boots( 1836 ) );
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