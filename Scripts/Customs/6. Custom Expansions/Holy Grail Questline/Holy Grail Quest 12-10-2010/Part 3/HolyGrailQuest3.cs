using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class HolyGrailQuest3 : BaseQuest
	{
		public override bool ForceRemember{ get{ return true; } }
		public override QuestChain ChainID{ get{ return QuestChain.HolyGrailQuest; } }
		public override Type NextQuest{ get{ return typeof( HolyGrailQuest4 ); } }
		public override object Title{ get{ return "The Holy Grail-Scene 3"; } }

		public override object Description
		{
			get
			{
				return
					"It is time to test your skills in battle.<BR><BR>" +
					"There is a bridge near the cave of Destard guarded by....<I>THE BLACK KNIGHT!</I><BR><BR>" +
					"It's really quite a lovely bridge. There are birds and the lovely sound of water trickling below, across the rockey bed...<BR><BR>" +
					"Nevermind that...! You must go there are defeat this so called 'Knight'.<BR><BR>" +
					"Really should be quite easy.<BR><BR>" +
					"I don't think the mighty steed I gave you will be of much help in this battle though. I'd stay on foot.<BR><BR>" +
					"Bring me back his helmet so I know you have defeated him in a great battle that will reflect" +
					" upon my glorious conquests across the lands!";
			}
		}

		public override object Refuse
		{
			get
			{
				return "One tiny little Black Knight and you decide to run away!";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "Did you find the lovely bridge, it is quite a pleasant area!";
			}
		}

		public override object Complete
		{
			get
			{
				return "*oh YUCK!*<BR><BR>" +
					"You could have at least cleaned his bloody helmet up a bit before you handed it over!<BR>" +
					"Now I have blood and entrails all over by armor!<BR><BR>" +
					"Now...you did display great courage in defeating the Black Knight, so take this as a reminder of your victory.";
			}
		}

		public HolyGrailQuest3() : base()
		{
			AddObjective( new ObtainObjective( typeof( BlackKnightHelm ), "Black Knight's Helmet", 1 ) );
			AddObjective( new SlayObjective( typeof( BlackKnight ), "The Black Knight", 1 ) );
			AddReward( new BaseReward( typeof( BlackKnightStatuette ), "Scene 3 Completed" ) );
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