using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class HolyGrailQuest8 : BaseQuest
	{
		public override bool ForceRemember{ get{ return true; } }
		public override QuestChain ChainID{ get{ return QuestChain.HolyGrailQuest; } }
		public override object Title{ get{ return "Holy Grail Quest-Conclusion"; } }

		public override object Description
		{
			get
			{
				return
					"But two tasks remain before you complete your Quest for the Holy Grail.<BR><BR>" +
					"At the bottom of this map it reads, 'Here may be found the last words of Joseph of" +
					" Aramathea.  He who is valiant and pure of spirit may find the Holy Grail" +
					"in the Castle Aaaaarrrrrrggghhh'.<BR><BR>" +
					"I'm not sure what that is. He must have died while carving it.<BR>" +
					"Oh well, it matters not, that's what's carved in the rock!<BR>" +
					"You must pass through the cave and defeat the legendary Black Beast of aaauuugh!.<BR><BR>" +
					"If you survive that, you must cross the Bridge of Death.<BR><BR>" +
					"There you will find the 5, sorry I mean 3 questions left behind by the old man from Scene 7. He is the keeper of the Bridge of Death.  Each traveller must answer the three questions--<BR><BR>" +
					"He who answers the three questions may cross in safety.<BR>" +
					"If you get a question wrong? Then you are cast into the --- Gorge of Eternal Peril!<BR><BR>" +
					"On the other side I am told awaits that which you seek." +
					"The old man from Scene 7 will give you a Bridge Token for passing the test at the Gorge of Eternal Peril and within The Castle Aggh lies <I>The Holy Grail!</I>" +
					"Either way, just answer the three questions as best you can and I shall await your return.<BR><BR>" +
					"Remember your tasks! Kill the Black Beast, cross the Bridge of Death, get the Bridge token from the old man in" +
					" scene 7 and retrieve the Grail.<BR>" +
					"Oh my, thats 4 tasks actually, you best get a move on!";
			}
		}

		public override object Refuse
		{
			get
			{
				return "You can't quit now!";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "Two simple tasks to complete your journey in search of the Grail, now get moving!";
			}
		}

		public override object Complete
		{
			get
			{
				return "*Arthur sees you approach and can not believe his eyes*<BR><BR>" +
					"Well!? Do you possess the Holy Grail?<BR><BR>" +
					"<I>*You explain the situation at the castle beyond the bridge*</I><BR><BR>" +
					"You ran away from a group of foul mouthed, insulting frenchmen??<BR>" +
					"The Dark Knight, vanquished. The Knight's Of Nee, silenced. The Black Beast, slain. The Bridge of Death, bested. All this to end by taunting Frenchmen?<BR><BR>" +
					"Well, I am sure you did your best and the battle has yet to be waged!<BR>" +
					"We shall be victorious in our Quest for the Holy Grail!<BR><BR>" +
					"First I must handle the situation at the Castle Anthrax, that seeems much more pressing.<BR>" +
					"Take this Golden Chest as your reward until our next battle.<BR>" +
					"Now, would you know the whereabouts of the Royal Writers Guild?<BR>" +
					"I really must speak to them about the ending of this tale.";
			}
		}

		public HolyGrailQuest8() : base()
		{
			AddObjective( new SlayObjective( typeof( BlackBeast ), "Black Beast of Aaaaarrrrrrggghhh", 1 ) );
			AddObjective( new ObtainObjective( typeof( BridgeToken ), "Bridge of Death Token", 1 ) );
			AddReward( new BaseReward( typeof( KingsChest ), "The End, or is it?" ) );
		}

		public override void GiveRewards()
		{
			base.GiveRewards();
			Owner.SendMessage( "ARTHUR: 'Congratulations! You have served me well impersonating me and have completed the final quest. Now, get back to work!'", null, 0xEF3 ); // You have demonstrated your compassion!  Your kind actions have been noted.
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