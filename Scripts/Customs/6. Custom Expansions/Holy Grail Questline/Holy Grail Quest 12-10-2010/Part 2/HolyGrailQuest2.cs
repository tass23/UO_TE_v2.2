using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class HolyGrailQuest2 : BaseQuest
	{
		public override bool ForceRemember{ get{ return true; } }
		public override QuestChain ChainID{ get{ return QuestChain.HolyGrailQuest; } }
		public override Type NextQuest{ get{ return typeof( HolyGrailQuest3 ); } }
		public override object Title{ get{ return "The Holy Grail-Scene 2"; } }

		public override object Description
		{
			get
			{
				return
					"Now that you are properly prepared for distant travels across the lands,<BR>" +
					"you must head out and assert your presence as I, Arthur, King of the Britons.<BR><BR>" +
					"I will have you seek out Dennis the Peasant first and bring him back here to my chambers.<BR>" +
					"For ruling such a kingdom requires that your peasants love and adore you!<BR><BR>" +
					"Dennis is one of my most loyal peasants and will be thrilled to be escorted back here to my court.<BR><BR>" +
					"Be sure to return to me with his gift of tribute and loyalty to my crown.<BR><BR>" +
					"Dennis works in the fields along with my other subjects.";      				
			} 
		}

		public override object Refuse
		{
			get
			{
				return "<I>*Well if you REFUSE...I want my coconuts back!*</I>";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "What IS that smell?? Get back there and bring Dennis to me.";
			}
		}

		public override object Complete
		{
			get
			{
				return "Well done again, you are making quite an impression!<BR><BR>" +
					"What is that TERRIBLE odor?!? I was rather expecting gold....not....<BR>" +
					"Actually this is QUITE disgusting! Here you take it! And go clean yourself up, you smell like a pigsty.<BR><BR>" +
					"Anyway, let's move on shall we. The Grail! Yes, we must focus on finding the Holy Grail!";
			}
		}
		
		public HolyGrailQuest2() : base()
		{
			AddObjective( new ObtainObjective( typeof( DennisTribute ), "Dennis' Tribute", 1 ) );
			AddObjective( new SlayObjective( typeof( SmellyPig ), "Smelly Pigs", 5 ) );
			AddReward( new BaseReward( typeof( Manure ), "Scene 2 Completed" ) );
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