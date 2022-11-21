using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class BridgeEscortQuest : BaseQuest
	{
		public override object Title{ get{ return "Old Man from Scene 24"; } }

		public override object Description
		{
			get
			{
				return
					"*You approach the Old Man from Scene 24*<BR><BR>" +
					"Soooo...you have passed the tests and crossed the Bridge of Death...at least for now!" +
					"I suppose you want the Bridge Token to give to the King?<BR>" +
					"Well just ya hang on! You need to get me out of here safely first.<BR>" +
					"These Frenchmen are ruthless and taunting!<BR>" +
					"Now, take me to the castle.";      				
			}
		}

		public override object Refuse
		{
			get
			{
				return "You are a silly sot! I should have smote you at first site!";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "You have got to be kidding? Take me to the castle ya' bleedin' idiot.";
			}
		}

		public BridgeEscortQuest() : base()
		{
			AddObjective( new EscortObjective( "Throne Room" ) );
			AddReward( new BaseReward( typeof( BridgeToken ), "Bridge Token" ) );
		}

		public override void GiveRewards()
		{
			base.GiveRewards();
			Owner.SendMessage( "OLD MAN FROM SCENE 24: 'I should have launched him into Gorge of Eternal Peril!'", null, 0xEF3 );
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

	public class BridgeEscort : BaseEscort
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( BridgeEscortQuest )
			};}
		}

		[Constructable]
		public BridgeEscort() : base()
		{
			Name = "Old Man";
			Title = "from Scene 24";
			NameHue = 68;
		}

		public BridgeEscort( Serial serial ) : base( serial )
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
			HairHue = 0x740;
		}

		public override void InitOutfit()
		{
			AddItem( new Backpack() );		
			AddItem( new Shoes( 0x727 ) );
			AddItem( new Robe() );
			AddItem( new Server.Items.NorseHelm() );
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