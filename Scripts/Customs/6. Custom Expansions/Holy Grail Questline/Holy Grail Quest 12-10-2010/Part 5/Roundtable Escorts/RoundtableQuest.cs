using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class RoundtableQuest : BaseQuest
	{
		public override object Title{ get{ return "Knight's of the Round Table"; } }

		public override object Description
		{
			get
			{
				return
					"*the knight looks startled as you approach*<BR><BR>" +
					"My lieg. I have searched across the lands in search of the Holy Grail!<BR><BR>" +
					"Take me to my King's Throne Room. I have important information to give him.<BR>" +
					"By the way, you wont tell him where you found me will you?<BR>" +
					"I really have been out searching for the Grail, honest!<BR>" +
					"I will give you what you seek when we arrive in safe passage.";
			}
		}

		public override object Refuse{ get{ return "I shall continue to seek the Grail. Good bye."; } }
		public override object Uncomplete{ get{ return "The Throne Room is inside the castle, quickly now!"; } }

		public RoundtableQuest() : base()
		{
			AddObjective( new EscortObjective( "Throne Room" ) );
			AddReward( new BaseReward( typeof( GrailToken ), "Grail Report" ) );
		}

		public override void GiveRewards()
		{
			base.GiveRewards();
			Owner.SendMessage( "You receive a Grail Report for returning one of the Knight's of the Round Table back to the castle.", null, 0x23 ); // You have demonstrated your compassion!  Your kind actions have been noted.
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