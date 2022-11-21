using System; 
using Server;
using Server.Gumps;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{ 
	public class CityVotingStone : Item 
	{
		private DateTime m_Time;

		private Mobile m_Mob1;
		private Mobile m_Mob2;
		private int m_Votes1;
		private int m_Votes2;

		private ArrayList m_Voters;
		private CityManagementStone m_Stone;

		private Timer m_Timer;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile RunnerOne
		{
			get{ return m_Mob1; }
			set{ m_Mob1 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile RunnerTwo
		{
			get{ return m_Mob2; }
			set{ m_Mob2 = value; }
		}

		public int RunnerOneVotes
		{
			get{ return m_Votes1; }
			set{ m_Votes1 = value; }
		}

		public int RunnerTwoVotes
		{
			get{ return m_Votes2; }
			set{ m_Votes2 = value; }
		}

		public ArrayList Voters
		{
			get{ return m_Voters; }
			set{ m_Voters = value; }
		}

		public CityManagementStone Stone
		{
			get{ return m_Stone; }
			set{ m_Stone = value; }
		}

		public CityVotingStone() : base( 3804 ) 
		{ 
			Movable = false; 
			Name = "city voting stone";

			m_Time = DateTime.Now + PlayerGovernmentSystem.VoteUpdate;
			m_Timer = new CityElectionTimer( m_Time, this );
			m_Timer.Start();
		} 

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( m_Stone.Citizens.Contains( from ) || from == m_Stone.Mayor || from.AccessLevel >= AccessLevel.GameMaster )
				{
					from.SendGump( new VotingStoneGump( this, from ) );

					TimeSpan time = m_Time - DateTime.Now;

					if ( time.Days != 0 )
						from.SendMessage( 53, "This election will end in {0} days, {1} hours, {2} minutes, and {3} seconds.", time.Days, time.Hours, time.Minutes, time.Seconds );
					else if ( time.Hours != 0 )
						from.SendMessage( 53, "This election will end in {0} hours, {1} minutes, and {2} seconds.", time.Hours, time.Minutes, time.Seconds );
					else if ( time.Minutes != 0 )
						from.SendMessage( 53, "This election will end in {0} minutes, and {1} seconds.", time.Minutes, time.Seconds );
					else if ( time.Seconds != 0 )
						from.SendMessage( 53, "This election will end in {0} seconds.", time.Seconds );
				}
				else
				{
					from.SendMessage( "You must be a member of the city to vote." );
				}
			}
			else
			{
				from.SendMessage( "You are too far away to access that." );
			}
		} 

		public void TallyVotes()
		{
			this.Stone.mayorchange = false;
			PlayerMobile pm1 = (PlayerMobile)this.RunnerOne;
			PlayerMobile pm2 = (PlayerMobile)this.RunnerTwo;
			if ( this.RunnerOne != null && this.RunnerTwo != null )
			{
				if ( pm1.City != m_Stone || pm2.City != m_Stone )
				{
					foreach ( Mobile m in m_Stone.Citizens )
					{
						m.SendMessage( 53, "CITY VOTING MESSAGE: One or both of the electees are no longer a member of the city! The Current mayor will stay in office this term." );
						this.Voters = new ArrayList();
						this.RunnerOne = null;
						this.RunnerTwo = null;
						this.RunnerOneVotes = 0;
						this.RunnerTwoVotes = 0;
					}
				}
				
				
				else if ( this.RunnerOneVotes == this.RunnerTwoVotes )
				{
					foreach ( Mobile m in m_Stone.Citizens )
					{
						m.SendMessage( 53, "CITY VOTING MESSAGE: The election was a tie! The Current mayor will stay in office this term." );
					}

					this.Voters = new ArrayList();
					this.RunnerOne = null;
					this.RunnerTwo = null;
					this.RunnerOneVotes = 0;
					this.RunnerTwoVotes = 0;
				}
				else if ( this.RunnerOneVotes >= this.RunnerTwoVotes )
				{
					this.Stone.Mayor = pm1;
					pm1.CityTitle = "Mayor";
					pm2.CityTitle = "Citizen";
					

					foreach ( Mobile m in m_Stone.Citizens )
					{
						m.SendMessage( 53, "CITY VOTING MESSAGE: {0} has been elected mayor over {1}!", this.RunnerOne.Name, this.Stone.CityName );
					}

					this.Voters = new ArrayList();
					this.RunnerOne = null;
					this.RunnerTwo = null;
					this.RunnerOneVotes = 0;
					this.RunnerTwoVotes = 0;
				}
				else
				{
					
					this.Stone.Mayor = pm2;
					pm2.CityTitle = "Mayor";
					pm1.CityTitle = "Citizen";

					foreach ( Mobile m in m_Stone.Citizens )
					{
						m.SendMessage( 53, "CITY VOTING MESSAGE: {0} has been elected mayor over {1}!", this.RunnerTwo.Name, this.Stone.CityName );
					}

					this.Voters = new ArrayList();
					this.RunnerOne = null;
					this.RunnerTwo = null;
					this.RunnerOneVotes = 0;
					this.RunnerTwoVotes = 0;
				}
			}
			else
			{
				foreach ( Mobile m in m_Stone.Citizens )
				{
					m.SendMessage( 53, "CITY VOTING MESSAGE: There was not enough people running for mayor, The current mayor will stay in office this term." );
				}

				this.Voters = new ArrayList();
				this.RunnerOne = null;
				this.RunnerTwo = null;
				this.RunnerOneVotes = 0;
				this.RunnerTwoVotes = 0;
			}
		}

		public void RestartTimer()
		{
			m_Time = DateTime.Now + PlayerGovernmentSystem.VoteUpdate;
			m_Timer = new CityElectionTimer( m_Time, this );
			m_Timer.Start();
		}

		public void RestartTimer( TimeSpan delay )
		{
			
			m_Time = DateTime.Now + delay;
			m_Timer = new CityElectionTimer( m_Time, this );
			m_Timer.Start();
		}
		
		public CityVotingStone( Serial serial ) : base( serial )
		{
		} 

		public override void OnDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnDelete();
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 

			writer.WriteDeltaTime( m_Time );
			writer.Write( m_Mob1 );
			writer.Write( m_Mob2 );
			writer.Write( m_Votes1 );
			writer.Write( m_Votes2 );
			writer.WriteMobileList( m_Voters, true );
			writer.Write( m_Stone);
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 

			switch ( version )
			{
				case 0:
				{
					m_Time = reader.ReadDeltaTime();
					m_Mob1 = reader.ReadMobile();
					m_Mob2 = reader.ReadMobile();
					m_Votes1 = reader.ReadInt();
					m_Votes2 = reader.ReadInt();
					m_Voters = reader.ReadMobileList();
					m_Stone = (CityManagementStone)reader.ReadItem();

					m_Timer = new CityElectionTimer( m_Time, this );
					m_Timer.Start();

					break;
				}
			}
		} 

		private class CityElectionTimer : Timer
		{
			private CityVotingStone m_Stone;

			public CityElectionTimer( DateTime end, CityVotingStone stone ) : base( end - DateTime.Now )
			{
				m_Stone = stone;
			}

			protected override void OnTick()
			{
				m_Stone.TallyVotes();
				m_Stone.RestartTimer();

				Stop();
			}
		}
	} 
} 
