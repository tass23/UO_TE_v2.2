using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class HolyGrailQuest7 : BaseQuest
	{
		public override bool ForceRemember{ get{ return true; } }
		public override QuestChain ChainID{ get{ return QuestChain.HolyGrailQuest; } }
		public override Type NextQuest{ get{ return typeof( HolyGrailQuest8 ); } }
		public override object Title{ get{ return "The Holy Grail-Scene 7"; } }

		public override object Description
		{
			get
			{
				return
					"And now to the business of Tim the Enchanter.<BR><BR>" +
					"He knows of a cave, a cave which no man has entered.<BR><BR>" +
					"Very much danger, for beyond the cave lies the Gorge of Eternal Peril, which no man has ever crossed.<BR><BR>" +
					"He will tell you your path to the Bridge of Death.<BR><BR>" +
					"Now, off you go, make haste!";
			}
		}

		public override object Refuse
		{
			get
			{
				return "What a patsy.";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "You ran away from a WHAT!?!?.";
			}
		}

		public override object Complete
		{
			get
			{
				return "*Arthur takes the map given to you by Tim the Enchanter*<BR><BR>" +
					"*He fumbles with the sodden paper...*<BR><BR>" +
					"Well yes....umm...ok this seems to be the place.<BR><BR>" +
					"Here, I am sure you know how to read a map.";
			}
		}

		public HolyGrailQuest7() : base()
		{
			AddObjective( new ObtainObjective( typeof( BridgeOfDeathScroll ), "Bridge of Death Map", 1 ) );
			AddReward( new BaseReward( typeof( HeritageToken ), "Scene 7 Completed" ) );
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