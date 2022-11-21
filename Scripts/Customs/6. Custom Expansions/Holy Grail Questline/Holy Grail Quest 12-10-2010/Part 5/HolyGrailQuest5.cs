using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class HolyGrailQuest5 : BaseQuest
	{
		public override bool ForceRemember{ get{ return true; } }
		public override QuestChain ChainID{ get{ return QuestChain.HolyGrailQuest; } }
		public override Type NextQuest{ get{ return typeof( HolyGrailQuest6 ); } }
		public override object Title{ get{ return "The Holy Grail-Scene 5"; } }

		public override object Description
		{
			get
			{
				return
					"You have reached across the lands in search of the Grail with conviction and vigor.<BR><BR>" +
					"Before you seek out Tim the Enchanter, you are gong to need help on your Quest.<BR><BR>" +
					"First you must search the lands and seek out the Knights of the Round Table and return them to the castle.<BR>" +
					"For I am sure they have clues to add to our task to find the Holy Grail.<BR><BR>" +
					"Once you have done this, you must rescue one of my purist knights who is being held captive in the Castle Anthrax.<BR><BR>" +
					"His name...Sir Galahad, the Chaste.<BR><BR>" +
					"His last message before his imprisonment within those walls was scribbled on a parchment and stated he has SEEN the Holy Grail.<BR>" +
					"He is in great peril. It is said there are over 150 holding him within that structure.<BR><BR>" +
					"With a name sooo deplorable as Anthrax, one can only imagine the torture and desperation poor Sir Galahad is facing!<BR><BR>" +
					"GO! Gather the Knight's of the Round Table and rescue my poor, valiant Knight and end the perilous suffering he has endured within such perilous-filled walls.";
			}
		}

		public override object Refuse
		{
			get
			{
				return "You really should reconsider, you are doing a bang up job pretending to be me.";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "Did you find the Knights?! Galahad? Was he there?! Oh poor, poor Sir Galahad! Make haste!";
			}
		}

		public override object Complete
		{
			get
			{
				return "*Arthur takes the grail pieces and parchment and reads the details of Galahad's imprisonment*<BR><BR>" +
					"You didn't read this did you?? *Arthur looks you over and stuffs the parchment into his pocket*<BR><BR>" +
					"Well good, the Castle Anthrax is for dealing with another day, way to perilous for you! I shall conquer that castle myself!<BR><BR>" +
					"Sir Galahad is safe and the Knights of the Round Table are reunited!";
			}
		}

		public HolyGrailQuest5() : base()
		{
			AddObjective( new ObtainObjective( typeof( GrailToken ), "Grail Report", 10 ) );
			AddObjective( new ObtainObjective( typeof( GalahadReport ), "Galahad's Report", 1 ) );
			AddObjective( new ObtainObjective( typeof( FakeGrail ), "'The' Grail", 1 ) );
			AddReward( new BaseReward( typeof( DisplayNorthMLAddonDeed ), "Scene 5 Completed" ) );
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