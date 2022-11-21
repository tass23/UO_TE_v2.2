using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;

#region About This Reward System
/*
 >>>>>>>>>>>>>>>>>>>>EDITED BY: A.A.R (AKA SythenSE)<<<<<<<<<<<<<<<<<<<<<
 This Reward System Will Replace The Default EA/OSI Veteran Reward System.
 It Is Genericly Named So That Admins Can Use It On Their Servers As-Is.
 I Did Not Code This System - Its The Same Default Veteran Reward System.
 All I Did Was Rename It To RewardSystem And Give It New Gumps I Designed.
 
 Please Note: All Reward Items Will Need To Be Edited So That References
 Made To 'VeteranReward' Are Changed To 'RewardSystem'. I Did This So You
 Can Use This System For More Than Just Veteran Rewards Making It Generic.
*/
#endregion About This Reward System

namespace Server.Engines.RewardSystem
{
	public class RewardChoiceGump : Gump
	{
		private Mobile m_From;

		private void RenderBackground()
		{
			AddPage( 0 );

            AddBackground(93, 62, 600, 450, 5054);
            AddAlphaRegion(104, 74, 576, 425);

            AddLabel(310, 87, 18, @"SERVER REWARD SYSTEM");

            AddImage(43, 191, 10400);
            AddImage(661, 191, 10410);

            AddButton( 147, 464, 4014, 4016, 1, GumpButtonType.Reply, 0); //Main Menu
            AddLabel(180, 464, 2036, @"MAIN MENU");

            AddButton( 551, 464, 4017, 4019, 0, GumpButtonType.Reply, 0); //Cancel
            AddLabel(585, 464, 2036, @"CANCEL");
		}

		private void RenderCategories()
		{
			TimeSpan rewardInterval = RewardSystem.RewardInterval;

			string intervalAsString;

			if ( rewardInterval == TimeSpan.FromDays( 30.0 ) )
				intervalAsString = "month";
			else if ( rewardInterval == TimeSpan.FromDays( 60.0 ) )
				intervalAsString = "two months";
			else if ( rewardInterval == TimeSpan.FromDays( 90.0 ) )
				intervalAsString = "three months";
			else if ( rewardInterval == TimeSpan.FromDays( 365.0 ) )
				intervalAsString = "year";
			else
				intervalAsString = String.Format( "{0} day{1}", rewardInterval.TotalDays, rewardInterval.TotalDays == 1 ? "" : "s" );

			AddPage( 1 );

            AddHtml(146, 126, 496, 100, @"Thank You For Being A Part Of Our Server's Community! As A Token Of Our Appreciation, You May Select From The Following In-Game Reward Items Below. The Items You Choose Will Be Attributed To The Account From Which You Are Playing. If You Run Into Any Issues While Trying To Use This System, Please Visit Our Website For More Information And/Or Email Our Support Staff And We Will Get Back To You Within 72 Hours.", (bool)true, (bool)true);

			int cur, max;

			RewardSystem.ComputeRewardInfo( m_From, out cur, out max );

            AddLabel(147, 237, 2036, @"Rewards You Can Select:");
			//AddHtmlLocalized( 60, 105, 300, 35, 1006006, false, false ); // Your current total of rewards to choose:
			AddLabel( 304, 237, 50, (max - cur).ToString() );

            AddLabel(458, 237, 2036, @"Rewards You Have Chosen:");
			//AddHtmlLocalized( 60, 140, 300, 35, 1006007, false, false ); // You have already chosen:
			AddLabel( 627, 237, 50, cur.ToString() );

			RewardCategory[] categories = RewardSystem.Categories;

			int page = 2;

			for ( int i = 0; i < categories.Length; ++i )
			{
				if ( !RewardSystem.HasAccess( m_From, categories[i] ) )
				{
					page += 1;
					continue;
				}

				AddButton( 293, 305 + (i * 40), 4011, 4013, 0, GumpButtonType.Page, page );
				page += PagesPerCategory( categories[ i ] );

				if ( categories[i].NameString != null )
                    AddHtml(333, 305 + (i * 40), 300, 20, "<BASEFONT COLOR=#B3B8BC>" + categories[i].NameString, false, false);
				else
                    AddHtmlLocalized(333, 305 + (i * 40), 300, 20, categories[i].Name, false, false);
			}

			page = 2;

			for ( int i = 0; i < categories.Length; ++i )
				RenderCategory( categories[ i ], i, ref page );
		}

		private int PagesPerCategory( RewardCategory category )
		{
			List<RewardEntry> entries = category.Entries;
			int j = 0, i = 0;

			for ( j = 0; j < entries.Count; j++ )
			{
				if ( RewardSystem.HasAccess( m_From, entries[ j ] ) )
					i++;
			}
			
			return (int) Math.Ceiling( i / 24.0 );
		}

		private int GetButtonID( int type, int index )
		{
			return 2 + (index * 20) + type;
		}

		private void RenderCategory( RewardCategory category, int index, ref int page )
		{
			AddPage( page );

			List<RewardEntry> entries = category.Entries;

			int i = 0;
			
			for ( int j = 0; j < entries.Count; ++j )
			{
				RewardEntry entry = entries[j];

				if ( !RewardSystem.HasAccess( m_From, entry ) )
					continue;

				if ( i == 24 )
                {

                    AddButton( 645, 464, 0x26B0, 0x26B1, 0, GumpButtonType.Page, ++page );
					//AddHtmlLocalized( 340, 415, 200, 20, 1011066, false, false ); // Next page

					AddPage( page );

					AddButton( 116, 464, 0x26B6, 0x26B7, 0, GumpButtonType.Page, page - 1 );
					//AddHtmlLocalized( 185, 415, 200, 20, 1011067, false, false ); // Previous page

					i = 0;
                }

        #region This Configures The Alignment Of The Reward Choices

                #region Directions On Modification
                /*
                The Button And Text Coordinates Are Read The Same As Above; The Numbers In Front Of The Equation: 155 And 150
                Example:(155 + ((i / 12) * 250), 150 + ((i % 12) * 25)
                Are The Exact Same As The First 2 Numbers In A Normal Button And Text Location; 270 And 415
                Example:( 270, 415, 0xFAE, 0xFB0, 0, GumpButtonType.Page, page - 1 ) 
                 
                The Lower you go with that first number the more to the left the text and button goes | The Higher you go with the second number the lower the text and button goes            
                */
                #endregion Directions On Modification

                AddButton(196 + ((i / 12) * 250), 140 + ((i % 12) * 25), 5540, 5541, GetButtonID(index, j), GumpButtonType.Reply, 0);

                if (entry.NameString != null)
                    AddHtml(221 + ((i / 12) * 250), 140 + ((i % 12) * 25), 250, 20, "<BASEFONT COLOR=#B3B8BC>" + entry.NameString, false, false); 
                else
                    AddHtmlLocalized(221 + ((i / 12) * 250), 140 + ((i % 12) * 25), 250, 20, entry.Name, false, false);
                ++i;

        #endregion This Configures The Alignment Of The Reward Choices
                
            }

            page += 1;
		}
 
		public RewardChoiceGump( Mobile from ) : base( 0, 0 )
		{
			m_From = from;

			from.CloseGump( typeof( RewardChoiceGump ) );

			RenderBackground();
			RenderCategories();
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonID = info.ButtonID - 1;

			if ( buttonID == 0 )
			{
				int cur, max;

				RewardSystem.ComputeRewardInfo( m_From, out cur, out max );

				if ( cur < max )
					m_From.SendGump( new RewardNoticeGump( m_From ) );
			}
			else
			{
				--buttonID;

				int type = (buttonID % 20);
				int index = (buttonID / 20);

				RewardCategory[] categories = RewardSystem.Categories;

				if ( type >= 0 && type < categories.Length )
				{
					RewardCategory category = categories[type];

					if ( index >= 0 && index < category.Entries.Count )
					{
						RewardEntry entry = category.Entries[index];

						if ( !RewardSystem.HasAccess( m_From, entry ) )
							return;

						m_From.SendGump( new RewardConfirmGump( m_From, entry ) );
					}
				}
			}
		}
	}
}