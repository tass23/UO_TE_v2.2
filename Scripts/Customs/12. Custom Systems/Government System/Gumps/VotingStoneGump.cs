using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class VotingStoneGump : Gump
	{
		private CityVotingStone m_Stone;

		public VotingStoneGump( CityVotingStone stone, Mobile from ) : base( 50, 50 )
		{
			m_Stone = stone;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(110, 75, 241, 192, 5120);
			AddImageTiled(115, 111, 232, 10, 5121);
			AddImageTiled(116, 223, 232, 10, 5121);
			AddHtml( 117, 80, 228, 25, @"<BASEFONT COLOR=WHITE><CENTER>City Voting</CENTER></BASEFONT>", (bool)false, (bool)false);

			if ( stone.RunnerOne == null && stone.RunnerTwo == null )
				AddHtml( 120, 116, 222, 37, @"<BASEFONT COLOR=WHITE><CENTER>No one is registered for office</CENTER></BASEFONT>", (bool)false, (bool)false);
			else if ( stone.RunnerOne != null || stone.RunnerTwo != null )
				AddHtml( 120, 116, 222, 37, @"<BASEFONT COLOR=WHITE><CENTER>Here are the polls</CENTER></BASEFONT>", (bool)false, (bool)false);

			if ( stone.RunnerOne != null )
			{
				AddButton(121, 157, 4029, 4030, 1, GumpButtonType.Reply, 0);
				AddLabel(157, 157, 1149, stone.RunnerOne.Name.ToString() + ": " + stone.RunnerOneVotes.ToString() );
			}

			if ( stone.RunnerTwo != null )
			{
				AddButton(121, 182, 4029, 4030, 2, GumpButtonType.Reply, 0);
				AddLabel(157, 182, 1149, stone.RunnerTwo.Name.ToString() + ": " + stone.RunnerTwoVotes.ToString() );
			}

			AddLabel(156, 233, 1149, @"Run For Office");
			AddButton(121, 231, 4005, 4006, 3, GumpButtonType.Reply, 0);
		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 ) // Vote
         		{
				if ( m_Stone.Voters.Contains( from ) )
				{
					from.SendMessage( "You have already voted for this election." );
				}
				else
				{
					m_Stone.RunnerOneVotes += 1;
					m_Stone.Voters.Add( from );
					from.SendMessage( "Your vote has been counted." );

					if ( PlayerGovernmentSystem.NeedsForensics )
					{
						m_Stone.RunnerOne.CheckSkill( SkillName.Forensics, 0.0, 100.0 ); // Normal Skill Check

						if ( m_Stone.RunnerOne.Skills[SkillName.Forensics].Base >= 0.0 && m_Stone.RunnerOne.Skills[SkillName.Forensics].Base <= 99.9 )
							m_Stone.RunnerOne.Skills[SkillName.Forensics].Base += 0.1;
					}
				}
			}

        		if ( info.ButtonID == 2 ) // Vote
         		{
				if ( m_Stone.Voters.Contains( from ) )
				{
					from.SendMessage( "You have already voted for this election." );
				}
				else
				{
					m_Stone.RunnerTwoVotes += 1;
					m_Stone.Voters.Add( from );
					from.SendMessage( "Your vote has been counted." );

					if ( PlayerGovernmentSystem.NeedsForensics )
					{
					
						m_Stone.RunnerTwo.CheckSkill( SkillName.Forensics, 0.0, 100.0 ); // Normal Skill Check
						
						if ( m_Stone.RunnerTwo.Skills[SkillName.Forensics].Base >= 0.0 && m_Stone.RunnerTwo.Skills[SkillName.Forensics].Base <= 99.9 )
							m_Stone.RunnerTwo.Skills[SkillName.Forensics].Base += 0.1;
					}
				}
			}
			
			if ( info.ButtonID == 3 ) // Run
			{
				if ( PlayerGovernmentSystem.CheckIfCanBeMayor( from ) )
				{
					if ( from == m_Stone.RunnerOne || from == m_Stone.RunnerTwo )
					{
						from.SendMessage( "Your already in this election." );
					}
					else if ( m_Stone.RunnerOne == null )
					{
						m_Stone.RunnerOne = from;
						from.SendMessage( "You are now in the election." );
					}
					else if ( m_Stone.RunnerTwo == null )
					{
						m_Stone.RunnerTwo = from;
						from.SendMessage( "You are now in the election." );
					}
					else
					{
						from.SendMessage( "The election is full." );
					}
				}
				else
				{
					from.SendMessage( "You lack the required skill to become a mayor at this time, You need at least 35.0 points in forensics." );
				}
			}
		}
	}
}
