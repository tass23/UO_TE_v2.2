using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class HolyGrailQuest6 : BaseQuest
	{
		public override bool ForceRemember{ get{ return true; } }
		public override QuestChain ChainID{ get{ return QuestChain.HolyGrailQuest; } }
		public override Type NextQuest{ get{ return typeof( HolyGrailQuest7 ); } }
		public override object Title{ get{ return "The Holy Grail-Scene 6"; } }

		public override object Description
		{
			get
			{
				return
					"It is time for you to seek out Brother Maynard.<BR><BR>" +
					"From him you must obtain The Holy Hand Grenade of Antioch!<BR><BR>" +
					"'Tis one of the sacred relics Brother Maynard carries with him!<BR><BR>" +
					"Bring Brother Maynard and the Holy Hand Grenade to me.<BR>" +
					"He and his bretheren reside in the Empath Abby.";      				
			}
		}

		public override object Refuse
		{
			get
			{
				return "Running away now?! You should be ashamed.";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "Must I do everything! Go to the Empath Abby and find Brother Maynard.";
			}
		}

		public override object Complete
		{
			get
			{
				return "*Arthur takes the Holy Hand Grenade*<BR><BR>" +
					"Did Brother Maynard tell you how to use this? It must be on the count of 5, oh wait no, I mean 3, not 2..." +
					"well I am sure you got the idea.<BR><BR>" +
					"Here, take these Holy Hand Grenades, I believe you are going to need them, for now...*directors overly dramatic theatrical pause*<BR><BR>" +
					"NOW!!! You are about to visit...Tim the Enchanter!!!!";
			}
		}

		public HolyGrailQuest6() : base()
		{
			AddObjective( new ObtainObjective( typeof( HolyHandgrenade ), "Holy Hand Grenade", 1 ) );
			AddReward( new BaseReward( typeof( KingsSatchel ), "Scene 6 Completed" ) );
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