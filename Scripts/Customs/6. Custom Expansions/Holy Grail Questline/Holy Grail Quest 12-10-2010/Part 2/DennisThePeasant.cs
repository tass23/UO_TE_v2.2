using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class DennisThePeasant : BaseQuest
	{
		public override object Title{ get{ return "Dennis the 'Peasant'"; } }

		public override object Description
		{
			get
			{
				return
					"You say: 'Old woman! Oh my, Old Man, sorry. What knight lives in that castle over there?<BR><BR>" +
					"I'm thirty seven -- I'm not old!<BR><BR>" +
					"You say: 'Well, I can't just call you 'Man'.'<BR><BR>" +
					"Well, you could say `Dennis'.<BR><BR>" +
					"You say: 'Well, I didn't know you were called 'Dennis'.'<BR><BR>" +
					"Well, you didn't bother to find out, did you?<BR>" +
					"What I object to is you automatically treat me like an inferior!<BR><BR>" +
					"You say: 'Well, I AM here before you as your king, Arthur, King of the Britons...'<BR><BR>" +
					"Oh king, eh, very nice.  An' how'd he get that, eh?  By exploitin' the workers -- by 'angin' on to outdated imperialist dogma" +
      				" which perpetuates the economic an' social differences in our society!<BR>" +
      				"And who are the Britons?<BR><BR>" +
					"You say: 'Well, we all are. We're all Britons and I, Arthur, am your king. Please, please good Dennis. I am in haste." +
					" Who lives in that castle?'<BR><BR>" +
					"No one lives there. We're an anarcho-syndicalist commune. We take in turns to act as a sort of executive officer for the week.<BR>" +
					"But all the decisions of that officer have to be ratified at a special biweekly meeting.<BR>" +
					"By a simple majority in the case of purely internal affairs,--<BR><BR>" +
					"You say: 'Be quiet! I order you to be quiet!'<BR><BR>" +
					"--but by a two-thirds majority in the case of more-- Order, eh -- who does he think he is?<BR><BR>" +
					"You say: 'I am here as YOUR King!'<BR><BR>" +
					"Well, I didn't vote for you.<BR><BR>" +
					"You say: 'You don't vote for kings. Now come along with me I shall escort you back to the castle!'";
			}
		}
		
		public override object Refuse{ get{ return "I didn't know we had a king. I thought we were an autonomous collective.<I>*goes back to the slopping mud*</I> There's some lovely filth down here!"; } }
		public override object Uncomplete{ get{ return "Oh! Come and see the violence inherent in the system! HELP! HELP! I'm being repressed!"; } }
				
		public DennisThePeasant() : base()
		{								
			AddObjective( new EscortObjective( "Throne Room" ) );
			AddReward( new BaseReward( typeof( DennisTribute ), "Dennis' Tribute" ) );
		}
		
		public override void GiveRewards()
		{			
			base.GiveRewards();
			Owner.SendMessage( "DENNIS:'Oh, what a give away. Did you here that, did you here that, eh? That's what I'm on about -- did you see him repressing me, you saw it didn't you?'", null, 0xEF3 ); // You have demonstrated your compassion!  Your kind actions have been noted.
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
	
	public class DennisEscort : BaseEscort
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( DennisThePeasant )
			};}
		}
		
		[Constructable]
		public DennisEscort() : base()
		{
			Name = "Dennis";
			Title = "the Peasant";
			NameHue = 68;
		}
		
		public DennisEscort( Serial serial ) : base( serial )
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
			HairHue = 915;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );			
			AddItem( new Shirt( 1023 ) );	
			AddItem( new ShortPants( 1023 ) );
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