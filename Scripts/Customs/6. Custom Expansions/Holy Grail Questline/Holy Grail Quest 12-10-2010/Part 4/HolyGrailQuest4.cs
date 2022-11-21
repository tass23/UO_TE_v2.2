using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class HolyGrailQuest4 : BaseQuest
	{
		public override bool ForceRemember{ get{ return true; } }
		public override QuestChain ChainID{ get{ return QuestChain.HolyGrailQuest; } }
		public override Type NextQuest{ get{ return typeof( HolyGrailQuest5 ); } }
		public override object Title{ get{ return "Holy Grail Quest-Scene 4"; } }

		public override object Description
		{
			get
			{
				return
					"You have proven yourself worthy in battle and I successfully removed the entrail stains from my tunic.<BR>" +
					"It is now time to seek out the Holy Grail.<BR><BR>" +
					"There is a legend of a man who knows of such things. He is know as....<I>Tim the Enchanter!</I><BR><BR>" +
					"<I>*Arthur shields his eyes with his arm*</I><BR><BR>" +
					"Sorry...that was a little over dramatic. As I was saying, you will need to seek out this Tim fellow.<BR>" +
					"You must first pass through the sacred woods, guarded by the knights who speak such as to drive a man to madness.<BR><BR>" +
					"If you earn passage from these keepers of the sacred woods, return to me with their offering.<BR><BR>" +
					"It will be a clue useful in obtaining the whereabouts of Tim the Enchanter.";
			}
		}

		public override object Refuse
		{
			get
			{
				return "Ha! I had a feeling you were a lily-livered pansy.";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "Bring me the item given to you by the keepers of the sacred woods and quickly.";
			}
		}

		public override object Complete
		{
			get
			{
				return "*A Herring????*<BR><BR>" +
					"You must be bloody joking?! What the 'ell am I....ahh yes, well anyway, Thank You.*cough*<BR>" +
					"Now that I have a.....herring.... everything is clear to me! I know where you can find....what was his name??" +
					" Oh yes, Tim something or other.<BR><BR>" +
					"Finding the Holy Grail is within our grasp!";
			}
		}

		public HolyGrailQuest4() : base()
		{
			AddObjective( new ObtainObjective( typeof( AHerring ), "'Ni' Item", 1 ) );
			AddReward( new BaseReward( typeof( NeeLandscapingAddonDeed ), "Scene 4 Completed" ) );
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