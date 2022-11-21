//Chicken Fighting!
//By Tresdni  -  http://www.uofreedom.com
//Edited by Raist 2017
using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;
using Server.Mobiles;
using Server.Misc;

namespace Server.Misc
{
	public enum ThemeType
	{	
		None = 0,
		Chicken,
		Hellhound,
		Miasma,
		Pig,
		AW
	};
}
namespace Server.Items
{
    public class CockFighting : Item
    {
		public int m_MaxPlayers = 2;
		public int m_NumPlayers;
		public int m_DeadCount;
		public int m_Bet = 800;  //The default amount to play per player.  This will make the winner get 10k per player as well.
		public int m_ArenaSize = 11;  //10 tiles by default.
		public bool m_GameRunning = false;
		public bool m_Tournament = false;
		public Mobile m_LastWinner;
		public Point3D m_Corner1;
		public Point3D m_Corner2;
		public Point3D m_Corner3;
		public Point3D m_Corner4;
		public FightingChicken m_LastChicken;
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int MaxPlayers
        {
            get
            {
                return m_MaxPlayers;
            }
            set
            {
				if(value < 2)
					value = 2;
					
				m_MaxPlayers = value;
				
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int NumberOfPlayers
        {
            get
            {
                return m_NumPlayers;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_NumPlayers = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public int DeadChickens
        {
            get
            {
                return m_DeadCount;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_DeadCount = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int Bet
        {
            get
            {
                return m_Bet;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_Bet = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int ArenaSize
        {
            get
            {
                return m_ArenaSize;
            }
            set
            {
				if(value < 10)
					value = 10;
					
                m_ArenaSize = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool GameRunning
        {
            get
            {
                return m_GameRunning;
            }
            set
            {
                m_GameRunning = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool Tournament
        {
            get
            {
                return m_Tournament;
            }
            set
            {
                m_Tournament = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner1
		{
			get { return m_Corner1; }
			set { m_Corner1 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner2
		{
			get { return m_Corner2; }
			set { m_Corner2 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner3
		{
			get { return m_Corner3; }
			set { m_Corner3 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner4
		{
			get { return m_Corner4; }
			set { m_Corner4 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.Owner )]
		public Mobile LastWinner{ get{ return m_LastWinner; } set { m_LastWinner = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public FightingChicken LastChicken
		{
			get { return m_LastChicken; }
			set { m_LastChicken = value; InvalidateProperties(); }
		}
		
		[Constructable]
        public CockFighting() : base( 0xED4 )
        {
			Movable = false;
			Hue = 2767;
			Name = "Ultimate Chicken Championships";
        }
        public override void OnDoubleClick( Mobile from )
        {
			if(m_GameRunning)
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			else
			{
				Item item = from.Backpack.FindItemByType( typeof( RAFTRS ) );
				RAFTRS raftrs = item as RAFTRS;
				
				if(raftrs != null && raftrs.CurAmount >= m_Bet)
				{
					raftrs.CurAmount -= m_Bet;
					raftrs.CurrentBet += m_Bet;
					raftrs.WinningAmount += m_Bet;
					JoinGame(from, this);
					from.SendMessage("{0} has been withdrawn from your R.A.F.T.  The Chicken you are Betting on has been placed in the arena to warm up for the contest.", m_Bet);
					from.AddToBackpack (new GoldVoucher());
				}
				else if(raftrs != null && raftrs.CurrentBet > 0)
				{
					from.SendMessage("You have an outstanding Bet placed. You may only place one Bet at a time.");
				}
				else
				{
					from.SendMessage("You have to have a R.A.F.T. with enough Gold in it to place a Bet on a Chicken.");
				}
			}
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
   
			//list.Add( 1042971, String.Format("Chicken Contests - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );
			list.Add( 1042971, String.Format(this.Name.ToString() + " - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );

			if(m_Bet > 0)
			{
				list.Add( 1060659, "Cost To Play\t{0}", m_Bet );
				list.Add( 1070722, String.Format( "Winner Takes {0} to the Bank!", m_Bet * m_MaxPlayers ) );
			}

			if(m_Tournament)
				list.Add( 1060660, "Mode\t{0}", "Tournament");

			if(m_GameRunning)
				list.Add( "CONTEST IN PROGRESS");
        }
		

        public CockFighting( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();

			m_MaxPlayers = reader.ReadInt();
			m_NumPlayers = reader.ReadInt();
			m_DeadCount = reader.ReadInt();
			m_Bet = reader.ReadInt();
			m_ArenaSize = reader.ReadInt();
			m_GameRunning = reader.ReadBool();
			m_Corner1 = reader.ReadPoint3D();
			m_Corner2 = reader.ReadPoint3D();
			m_Corner3 = reader.ReadPoint3D();
			m_Corner4 = reader.ReadPoint3D();
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );

			writer.Write( (int)m_MaxPlayers);
			writer.Write( (int)m_NumPlayers);
			writer.Write( (int)m_DeadCount);
			writer.Write( (int)m_Bet);
			writer.Write( (int)m_ArenaSize);
			writer.Write( (bool)m_GameRunning);
			writer.Write( m_Corner1 );
			writer.Write( m_Corner2);
			writer.Write( m_Corner3 );
			writer.Write( m_Corner4 );
        }
		
		public void ReleaseTheChickens()
		{
			foreach (Mobile m in GetMobilesInRange( m_ArenaSize ))
			{
                if (m is FightingChicken)
				{
					FightingChicken fc = m as FightingChicken;
					fc.Blessed = false;
					fc.Frozen = false;
					
					if(fc.Owner != null)
						fc.Owner.SendMessage("Begin!");
				}
			}
		}
		
		public void JoinGame(Mobile from, CockFighting controller)
		{
			if( !controller.GameRunning )
			{
				controller.NumberOfPlayers++;
				FightingChicken fc = new FightingChicken(from, controller);
				fc.Blessed = true;
				fc.Frozen = true;
				fc.Map = from.Map;
				
				switch(Utility.Random(4) )
				{
					case 0: { fc.Location = controller.Corner1; } break;
					case 1: { fc.Location = controller.Corner2; } break;
					case 2: { fc.Location = controller.Corner3; } break;
					case 3: { fc.Location = controller.Corner4; } break;
				}
				
				if(controller.NumberOfPlayers == controller.MaxPlayers)
				{
					controller.GameRunning = true;
					controller.ReleaseTheChickens();
				}
				
				fc.Combatant = controller.LastChicken;
				controller.LastChicken = fc;
			}
			else
			{
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			}
		}
		
		public void EndGame( CockFighting controller )
		{
			ArrayList list = new ArrayList();
			//End the game, because there is only one chicken left.
			foreach (Mobile m in GetMobilesInRange( controller.ArenaSize ))
			{
				
                if (m is FightingChicken)
				{
					FightingChicken fc = m as FightingChicken;
					list.Add( fc );
				}
				
			}
			
			if( list.Count > 0 )
			{
				for( int i = 0; i < list.Count; i++ )
				{
					if( controller != null && !controller.Deleted)
					{
						if( (FightingChicken)list[i] != null && !((FightingChicken)list[i]).Deleted )
						{
							if( ((FightingChicken)list[i]).Owner != null && !((FightingChicken)list[i]).Owner.Deleted )
							{
								controller.LastWinner = ((FightingChicken)list[i]).Owner;
								if(controller.Bet > 0)
								{
									Item a = ((FightingChicken)list[i]).Owner.Backpack.FindItemByType( typeof( RAFTRS ) );
									RAFTRS raftrs = a as RAFTRS;
									Item b = ((FightingChicken)list[i]).Owner.Backpack.FindItemByType( typeof( GoldVoucher ) );
									GoldVoucher gvoucher = b as GoldVoucher;
				
									if((raftrs != null) && (raftrs.CurrentBet == controller.Bet))
									{
										raftrs.WinningAmount += (controller.MaxPlayers * controller.Bet);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										if(gvoucher != null)
										{
											((FightingChicken)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
										}
									}
									else if( gvoucher != null)
									{
										BankCheck bc = new BankCheck(controller.MaxPlayers * controller.Bet);
										((FightingChicken)list[i]).Owner.AddToBackpack(bc);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										((FightingChicken)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
									}
									else
									{
										((FightingChicken)list[i]).Delete();
									}
								}
							}
							((FightingChicken)list[i]).Delete();
						}
					}
				}
			}			
			controller.DeadChickens = 0;
			controller.NumberOfPlayers = 0;
			controller.GameRunning = false;
			controller.LastChicken = null;
			controller.LastWinner = null;
		}
    }

	public class MiasmaFighting : Item
    {
		public int m_MaxPlayers = 2;
		public int m_NumPlayers;
		public int m_DeadCount;
		public int m_Bet = 10000;  //The default amount to play per player.  This will make the winner get 10k per player as well.
		public int m_ArenaSize = 11;  //10 tiles by default.
		public bool m_GameRunning = false;
		public bool m_Tournament = false;
		public Mobile m_LastWinner;
		public Point3D m_Corner1;
		public Point3D m_Corner2;
		public Point3D m_Corner3;
		public Point3D m_Corner4;
		public FightingMiasma m_LastMiasma;

		[CommandProperty( AccessLevel.GameMaster )]
        public int MaxPlayers
        {
            get
            {
                return m_MaxPlayers;
            }
            set
            {
				if(value < 2)
					value = 2;
					
				m_MaxPlayers = value;
				
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int NumberOfPlayers
        {
            get
            {
                return m_NumPlayers;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_NumPlayers = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public int DeadChickens
        {
            get
            {
                return m_DeadCount;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_DeadCount = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int Bet
        {
            get
            {
                return m_Bet;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_Bet = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int ArenaSize
        {
            get
            {
                return m_ArenaSize;
            }
            set
            {
				if(value < 10)
					value = 10;
					
                m_ArenaSize = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool GameRunning
        {
            get
            {
                return m_GameRunning;
            }
            set
            {
                m_GameRunning = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool Tournament
        {
            get
            {
                return m_Tournament;
            }
            set
            {
                m_Tournament = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner1
		{
			get { return m_Corner1; }
			set { m_Corner1 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner2
		{
			get { return m_Corner2; }
			set { m_Corner2 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner3
		{
			get { return m_Corner3; }
			set { m_Corner3 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner4
		{
			get { return m_Corner4; }
			set { m_Corner4 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.Owner )]
		public Mobile LastWinner{ get{ return m_LastWinner; } set { m_LastWinner = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public FightingMiasma LastMiasma
		{
			get { return m_LastMiasma; }
			set { m_LastMiasma = value; InvalidateProperties(); }
		}
		
		[Constructable]
        public MiasmaFighting() : base( 0xED4 )
        {
			Movable = false;
			Hue = 2987;
			Name = "Ultimate Miasma Championships";
        }

        public override void OnDoubleClick( Mobile from )
        {
			if(m_GameRunning)
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			else
			{
				Item item = from.Backpack.FindItemByType( typeof( RAFTRS ) );
				RAFTRS raftrs = item as RAFTRS;
				
				if(raftrs != null && raftrs.CurAmount >= m_Bet)
				{
					raftrs.CurAmount -= m_Bet;
					raftrs.CurrentBet += m_Bet;
					raftrs.WinningAmount += m_Bet;
					JoinGame(from, this);
					from.SendMessage("{0} has been withdrawn from your R.A.F.T.  The Miasma you are betting on has been placed in the arena to warm up for the contest.", m_Bet);
					from.AddToBackpack (new GoldVoucher());
				}
				else if(raftrs != null && raftrs.CurrentBet > 0)
				{
					from.SendMessage("You have an outstanding Bet placed. You may only place one Bet at a time.");
				}
				else
				{
					from.SendMessage("You have to have a R.A.F.T. with enough Gold in it to place a Bet on a Miasma.");
				}
			}
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
   
			//list.Add( 1042971, String.Format("Miasma Contests - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );
			list.Add( 1042971, String.Format(this.Name.ToString() + " - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );

			if(m_Bet > 0)
			{
				list.Add( 1060659, "Cost To Play\t{0}", m_Bet );
				list.Add( 1070722, String.Format( "Winner Takes {0} to the Bank!", m_Bet * m_MaxPlayers ) );
			}

			if(m_Tournament)
				list.Add( 1060660, "Mode\t{0}", "Tournament");

			if(m_GameRunning)
				list.Add( "CONTEST IN PROGRESS");
        }
		

        public MiasmaFighting( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();

			m_MaxPlayers = reader.ReadInt();
			m_NumPlayers = reader.ReadInt();
			m_DeadCount = reader.ReadInt();
			m_Bet = reader.ReadInt();
			m_ArenaSize = reader.ReadInt();
			m_GameRunning = reader.ReadBool();
			m_Corner1 = reader.ReadPoint3D();
			m_Corner2 = reader.ReadPoint3D();
			m_Corner3 = reader.ReadPoint3D();
			m_Corner4 = reader.ReadPoint3D();
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );

			writer.Write( (int)m_MaxPlayers);
			writer.Write( (int)m_NumPlayers);
			writer.Write( (int)m_DeadCount);
			writer.Write( (int)m_Bet);
			writer.Write( (int)m_ArenaSize);
			writer.Write( (bool)m_GameRunning);
			writer.Write( m_Corner1 );
			writer.Write( m_Corner2);
			writer.Write( m_Corner3 );
			writer.Write( m_Corner4 );
        }
		
		public void ReleaseTheChickens()
		{
			foreach (Mobile m in GetMobilesInRange( m_ArenaSize ))
			{
                if (m is FightingMiasma)
				{
					FightingMiasma fm = m as FightingMiasma;
					fm.Blessed = false;
					fm.Frozen = false;
					
					if(fm.Owner != null)
						fm.Owner.SendMessage("Begin!");
				}
			}
		}
		
		public void JoinGame(Mobile from, MiasmaFighting controller)
		{
			if( !controller.GameRunning )
			{
				controller.NumberOfPlayers++;
				FightingMiasma fm = new FightingMiasma(from, controller);
				fm.Blessed = true;
				fm.Frozen = true;
				fm.Map = from.Map;
				
				switch(Utility.Random(4) )
				{
					case 0: { fm.Location = controller.Corner1; } break;
					case 1: { fm.Location = controller.Corner2; } break;
					case 2: { fm.Location = controller.Corner3; } break;
					case 3: { fm.Location = controller.Corner4; } break;
				}
				
				if(controller.NumberOfPlayers == controller.MaxPlayers)
				{
					controller.GameRunning = true;
					controller.ReleaseTheChickens();
				}
				
				fm.Combatant = controller.LastMiasma;
				controller.LastMiasma = fm;
			}
			else
			{
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			}
		}
		
		public void EndGame( MiasmaFighting controller )
		{
			ArrayList list = new ArrayList();
			//End the game, because there is only one chicken left.
			foreach (Mobile m in GetMobilesInRange( controller.ArenaSize ))
			{
				
                if (m is FightingMiasma)
				{
					FightingMiasma fm = m as FightingMiasma;
					list.Add( fm );
				}
			}
			
			if( list.Count > 0 )
			{
				for( int i = 0; i < list.Count; i++ )
				{
					if( controller != null && !controller.Deleted)
					{
						if( (FightingMiasma)list[i] != null && !((FightingMiasma)list[i]).Deleted )
						{
							if( ((FightingMiasma)list[i]).Owner != null && !((FightingMiasma)list[i]).Owner.Deleted )
							{
								controller.LastWinner = ((FightingMiasma)list[i]).Owner;
								if(controller.Bet > 0)
								{
									Item a = ((FightingMiasma)list[i]).Owner.Backpack.FindItemByType( typeof( RAFTRS ) );
									RAFTRS raftrs = a as RAFTRS;
									Item b = ((FightingMiasma)list[i]).Owner.Backpack.FindItemByType( typeof( GoldVoucher ) );
									GoldVoucher gvoucher = b as GoldVoucher;
				
									if((raftrs != null) && (raftrs.CurrentBet == controller.Bet))
									{
										raftrs.WinningAmount += (controller.MaxPlayers * controller.Bet);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										if(gvoucher != null)
										{
											((FightingMiasma)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
										}
									}
									else if( gvoucher != null)
									{
										BankCheck bc = new BankCheck(controller.MaxPlayers * controller.Bet);
										((FightingMiasma)list[i]).Owner.AddToBackpack(bc);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										((FightingMiasma)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
									}
									else
									{
										((FightingMiasma)list[i]).Delete();
									}
								}
							}
							((FightingMiasma)list[i]).Delete();
						}
					}
				}
			}			
			controller.DeadChickens = 0;
			controller.NumberOfPlayers = 0;
			controller.GameRunning = false;
			controller.LastMiasma = null;
			controller.LastWinner = null;
		}
    }
	public class HellHoundFighting : Item
    {
		public int m_MaxPlayers = 2;
		public int m_NumPlayers;
		public int m_DeadCount;
		public int m_Bet = 3000;  //The default amount to play per player.  This will make the winner get 10k per player as well.
		public int m_ArenaSize = 12;  //10 tiles by default.
		public bool m_GameRunning = false;
		public bool m_Tournament = false;
		public Mobile m_LastWinner;
		public Point3D m_Corner1;
		public Point3D m_Corner2;
		public Point3D m_Corner3;
		public Point3D m_Corner4;
		public FightingHellHound m_LastHellHound;
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int MaxPlayers
        {
            get
            {
                return m_MaxPlayers;
            }
            set
            {
				if(value < 2)
					value = 2;
					
				m_MaxPlayers = value;
				
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int NumberOfPlayers
        {
            get
            {
                return m_NumPlayers;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_NumPlayers = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public int DeadChickens
        {
            get
            {
                return m_DeadCount;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_DeadCount = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int Bet
        {
            get
            {
                return m_Bet;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_Bet = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int ArenaSize
        {
            get
            {
                return m_ArenaSize;
            }
            set
            {
				if(value < 10)
					value = 10;
					
                m_ArenaSize = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool GameRunning
        {
            get
            {
                return m_GameRunning;
            }
            set
            {
                m_GameRunning = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool Tournament
        {
            get
            {
                return m_Tournament;
            }
            set
            {
                m_Tournament = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner1
		{
			get { return m_Corner1; }
			set { m_Corner1 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner2
		{
			get { return m_Corner2; }
			set { m_Corner2 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner3
		{
			get { return m_Corner3; }
			set { m_Corner3 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner4
		{
			get { return m_Corner4; }
			set { m_Corner4 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.Owner )]
		public Mobile LastWinner{ get{ return m_LastWinner; } set { m_LastWinner = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public FightingHellHound LastHellHound
		{
			get { return m_LastHellHound; }
			set { m_LastHellHound = value; InvalidateProperties(); }
		}
		
		[Constructable]
        public HellHoundFighting() : base( 0xED4 )
        {
			Movable = false;
			Hue = 2237;
			Name = "Ultimate Hell Hound Championships";
        }
        public override void OnDoubleClick( Mobile from )
        {
			if(m_GameRunning)
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			else
			{
				Item item = from.Backpack.FindItemByType( typeof( RAFTRS ) );
				RAFTRS raftrs = item as RAFTRS;
				
				if(raftrs != null && raftrs.CurAmount >= m_Bet)
				{
					raftrs.CurAmount -= m_Bet;
					raftrs.CurrentBet += m_Bet;
					raftrs.WinningAmount += m_Bet;
					JoinGame(from, this);
					from.SendMessage("{0} has been withdrawn from your R.A.F.T.  The Hell Hound you are betting on has been placed in the arena to warm up for the contest.", m_Bet);
					from.AddToBackpack (new GoldVoucher());
				}
				else if(raftrs != null && raftrs.CurrentBet > 0)
				{
					from.SendMessage("You have an outstanding Bet placed. You may only place one Bet at a time.");
				}
				else
				{
					from.SendMessage("You have to have a R.A.F.T. with enough Gold in it to place a Bet on a Hell Hound.");
				}
			}
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
   
			//list.Add( 1042971, String.Format("Hell Hound Contests - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );
			list.Add( 1042971, String.Format(this.Name.ToString() + " - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );

			if(m_Bet > 0)
			{
				list.Add( 1060659, "Cost To Play\t{0}", m_Bet );
				list.Add( 1070722, String.Format( "Winner Takes {0} to the Bank!", m_Bet * m_MaxPlayers ) );
			}

			if(m_Tournament)
				list.Add( 1060660, "Mode\t{0}", "Tournament");

			if(m_GameRunning)
				list.Add( "CONTEST IN PROGRESS");
        }
		

        public HellHoundFighting( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();

			m_MaxPlayers = reader.ReadInt();
			m_NumPlayers = reader.ReadInt();
			m_DeadCount = reader.ReadInt();
			m_Bet = reader.ReadInt();
			m_ArenaSize = reader.ReadInt();
			m_GameRunning = reader.ReadBool();
			m_Corner1 = reader.ReadPoint3D();
			m_Corner2 = reader.ReadPoint3D();
			m_Corner3 = reader.ReadPoint3D();
			m_Corner4 = reader.ReadPoint3D();
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );

			writer.Write( (int)m_MaxPlayers);
			writer.Write( (int)m_NumPlayers);
			writer.Write( (int)m_DeadCount);
			writer.Write( (int)m_Bet);
			writer.Write( (int)m_ArenaSize);
			writer.Write( (bool)m_GameRunning);
			writer.Write( m_Corner1 );
			writer.Write( m_Corner2);
			writer.Write( m_Corner3 );
			writer.Write( m_Corner4 );
        }
		
		public void ReleaseTheChickens()
		{
			foreach (Mobile m in GetMobilesInRange( m_ArenaSize ))
			{
                if (m is FightingHellHound)
				{
					FightingHellHound fh = m as FightingHellHound;
					fh.Blessed = false;
					fh.Frozen = false;
					
					if(fh.Owner != null)
						fh.Owner.SendMessage("Begin!");
				}
			}
		}
		
		public void JoinGame(Mobile from, HellHoundFighting controller)
		{
			if( !controller.GameRunning )
			{
				controller.NumberOfPlayers++;
				FightingHellHound fh = new FightingHellHound(from, controller);
				fh.Blessed = true;
				fh.Frozen = true;
				fh.Map = from.Map;
				
				switch(Utility.Random(4) )
				{
					case 0: { fh.Location = controller.Corner1; } break;
					case 1: { fh.Location = controller.Corner2; } break;
					case 2: { fh.Location = controller.Corner3; } break;
					case 3: { fh.Location = controller.Corner4; } break;
				}
				
				if(controller.NumberOfPlayers == controller.MaxPlayers)
				{
					controller.GameRunning = true;
					controller.ReleaseTheChickens();
				}
				
				fh.Combatant = controller.LastHellHound;
				controller.LastHellHound = fh;
			}
			else
			{
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			}
		}
		
		public void EndGame( HellHoundFighting controller )
		{
			ArrayList list = new ArrayList();
			//End the game, because there is only one chicken left.
			foreach (Mobile m in GetMobilesInRange( controller.ArenaSize ))
			{
				
                if (m is FightingHellHound)
				{
					FightingHellHound fh = m as FightingHellHound;
					list.Add( fh );
				}
			}
			
			if( list.Count > 0 )
			{
				for( int i = 0; i < list.Count; i++ )
				{
					if( controller != null && !controller.Deleted)
					{
						if( (FightingHellHound)list[i] != null && !((FightingHellHound)list[i]).Deleted )
						{
							if( ((FightingHellHound)list[i]).Owner != null && !((FightingHellHound)list[i]).Owner.Deleted )
							{
								controller.LastWinner = ((FightingHellHound)list[i]).Owner;
								if(controller.Bet > 0)
								{
									Item a = ((FightingHellHound)list[i]).Owner.Backpack.FindItemByType( typeof( RAFTRS ) );
									RAFTRS raftrs = a as RAFTRS;
									Item b = ((FightingHellHound)list[i]).Owner.Backpack.FindItemByType( typeof( GoldVoucher ) );
									GoldVoucher gvoucher = b as GoldVoucher;
				
									if((raftrs != null) && (raftrs.CurrentBet == controller.Bet))
									{
										raftrs.WinningAmount += (controller.MaxPlayers * controller.Bet);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										if(gvoucher != null)
										{
											((FightingHellHound)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
										}
									}
									else if( gvoucher != null)
									{
										BankCheck bc = new BankCheck(controller.MaxPlayers * controller.Bet);
										((FightingHellHound)list[i]).Owner.AddToBackpack(bc);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										((FightingHellHound)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
									}
									else
									{
										((FightingHellHound)list[i]).Delete();
									}
								}
							}
							((FightingHellHound)list[i]).Delete();
						}
					}
				}
			}			
			controller.DeadChickens = 0;
			controller.NumberOfPlayers = 0;
			controller.GameRunning = false;
			controller.LastHellHound = null;
			controller.LastWinner = null;
		}
    }
	public class PigFighting : Item
    {
		public int m_MaxPlayers = 2;
		public int m_NumPlayers;
		public int m_DeadCount;
		public int m_Bet = 500;  //The default amount to play per player.  This will make the winner get 10k per player as well.
		public int m_ArenaSize = 11;  //10 tiles by default.
		public bool m_GameRunning = false;
		public bool m_Tournament = false;
		public Mobile m_LastWinner;
		public Point3D m_Corner1;
		public Point3D m_Corner2;
		public Point3D m_Corner3;
		public Point3D m_Corner4;
		public FightingPig m_LastPig;
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int MaxPlayers
        {
            get
            {
                return m_MaxPlayers;
            }
            set
            {
				if(value < 2)
					value = 2;
					
				m_MaxPlayers = value;
				
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int NumberOfPlayers
        {
            get
            {
                return m_NumPlayers;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_NumPlayers = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public int DeadChickens
        {
            get
            {
                return m_DeadCount;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_DeadCount = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int Bet
        {
            get
            {
                return m_Bet;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_Bet = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int ArenaSize
        {
            get
            {
                return m_ArenaSize;
            }
            set
            {
				if(value < 10)
					value = 10;
					
                m_ArenaSize = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool GameRunning
        {
            get
            {
                return m_GameRunning;
            }
            set
            {
                m_GameRunning = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool Tournament
        {
            get
            {
                return m_Tournament;
            }
            set
            {
                m_Tournament = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner1
		{
			get { return m_Corner1; }
			set { m_Corner1 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner2
		{
			get { return m_Corner2; }
			set { m_Corner2 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner3
		{
			get { return m_Corner3; }
			set { m_Corner3 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner4
		{
			get { return m_Corner4; }
			set { m_Corner4 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.Owner )]
		public Mobile LastWinner{ get{ return m_LastWinner; } set { m_LastWinner = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public FightingPig LastPig
		{
			get { return m_LastPig; }
			set { m_LastPig = value; InvalidateProperties(); }
		}
		
		[Constructable]
        public PigFighting() : base( 0xED4 )
        {
			Movable = false;
			Hue = 2973;
			Name = "Ultimate Pig Championships";
        }
        public override void OnDoubleClick( Mobile from )
        {
			if(m_GameRunning)
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			else
			{
				Item item = from.Backpack.FindItemByType( typeof( RAFTRS ) );
				RAFTRS raftrs = item as RAFTRS;
				
				if(raftrs != null && raftrs.CurAmount >= m_Bet)
				{
					raftrs.CurAmount -= m_Bet;
					raftrs.CurrentBet += m_Bet;
					raftrs.WinningAmount += m_Bet;
					JoinGame(from, this);
					from.SendMessage("{0} has been withdrawn from your R.A.F.T.  The Pig you are betting on has been placed in the arena to warm up for the contest.", m_Bet);
					from.AddToBackpack (new GoldVoucher());
				}
				else if(raftrs != null && raftrs.CurrentBet > 0)
				{
					from.SendMessage("You have an outstanding Bet placed. You may only place one Bet at a time.");
				}
				else
				{
					from.SendMessage("You have to have a R.A.F.T. with enough Gold in it to place a Bet on a Pig.");
				}
			}
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
   
			//list.Add( 1042971, String.Format("Pig Contests - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );
			list.Add( 1042971, String.Format(this.Name.ToString() + " - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );

			if(m_Bet > 0)
			{
				list.Add( 1060659, "Cost To Play\t{0}", m_Bet );
				list.Add( 1070722, String.Format( "Winner Takes {0} to the Bank!", m_Bet * m_MaxPlayers ) );
			}

			if(m_Tournament)
				list.Add( 1060660, "Mode\t{0}", "Tournament");

			if(m_GameRunning)
				list.Add( "CONTEST IN PROGRESS");
        }
		

        public PigFighting( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();

			m_MaxPlayers = reader.ReadInt();
			m_NumPlayers = reader.ReadInt();
			m_DeadCount = reader.ReadInt();
			m_Bet = reader.ReadInt();
			m_ArenaSize = reader.ReadInt();
			m_GameRunning = reader.ReadBool();
			m_Corner1 = reader.ReadPoint3D();
			m_Corner2 = reader.ReadPoint3D();
			m_Corner3 = reader.ReadPoint3D();
			m_Corner4 = reader.ReadPoint3D();
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );

			writer.Write( (int)m_MaxPlayers);
			writer.Write( (int)m_NumPlayers);
			writer.Write( (int)m_DeadCount);
			writer.Write( (int)m_Bet);
			writer.Write( (int)m_ArenaSize);
			writer.Write( (bool)m_GameRunning);
			writer.Write( m_Corner1 );
			writer.Write( m_Corner2);
			writer.Write( m_Corner3 );
			writer.Write( m_Corner4 );
        }
		
		public void ReleaseTheChickens()
		{
			foreach (Mobile m in GetMobilesInRange( m_ArenaSize ))
			{
                if (m is FightingPig)
				{
					FightingPig fp = m as FightingPig;
					fp.Blessed = false;
					fp.Frozen = false;
					
					if(fp.Owner != null)
						fp.Owner.SendMessage("Begin!");
				}
			}
		}
		
		public void JoinGame(Mobile from, PigFighting controller)
		{
			if( !controller.GameRunning )
			{
				controller.NumberOfPlayers++;
				FightingPig fp = new FightingPig(from, controller);
				fp.Blessed = true;
				fp.Frozen = true;
				fp.Map = from.Map;
				
				switch(Utility.Random(4) )
				{
					case 0: { fp.Location = controller.Corner1; } break;
					case 1: { fp.Location = controller.Corner2; } break;
					case 2: { fp.Location = controller.Corner3; } break;
					case 3: { fp.Location = controller.Corner4; } break;
				}
				
				if(controller.NumberOfPlayers == controller.MaxPlayers)
				{
					controller.GameRunning = true;
					controller.ReleaseTheChickens();
				}
				
				fp.Combatant = controller.LastPig;
				controller.LastPig = fp;
			}
			else
			{
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			}
		}
		
		public void EndGame( PigFighting controller )
		{
			ArrayList list = new ArrayList();
			//End the game, because there is only one chicken left.
			foreach (Mobile m in GetMobilesInRange( controller.ArenaSize ))
			{
				
                if (m is FightingPig)
				{
					FightingPig fp = m as FightingPig;
					list.Add( fp );
				}
			}
			
			if( list.Count > 0 )
			{
				for( int i = 0; i < list.Count; i++ )
				{
					if( controller != null && !controller.Deleted)
					{
						if( (FightingPig)list[i] != null && !((FightingPig)list[i]).Deleted )
						{
							if( ((FightingPig)list[i]).Owner != null && !((FightingPig)list[i]).Owner.Deleted )
							{
								controller.LastWinner = ((FightingPig)list[i]).Owner;
								if(controller.Bet > 0)
								{
									Item a = ((FightingPig)list[i]).Owner.Backpack.FindItemByType( typeof( RAFTRS ) );
									RAFTRS raftrs = a as RAFTRS;
									Item b = ((FightingPig)list[i]).Owner.Backpack.FindItemByType( typeof( GoldVoucher ) );
									GoldVoucher gvoucher = b as GoldVoucher;
				
									if((raftrs != null) && (raftrs.CurrentBet == controller.Bet))
									{
										raftrs.WinningAmount += (controller.MaxPlayers * controller.Bet);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										if(gvoucher != null)
										{
											((FightingPig)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
										}
									}
									else if( gvoucher != null)
									{
										BankCheck bc = new BankCheck(controller.MaxPlayers * controller.Bet);
										((FightingPig)list[i]).Owner.AddToBackpack(bc);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() +" ! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										((FightingPig)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
									}
									else
									{
										((FightingPig)list[i]).Delete();
									}
								}
							}
							((FightingPig)list[i]).Delete();
						}
					}
				}
			}			
			controller.DeadChickens = 0;
			controller.NumberOfPlayers = 0;
			controller.GameRunning = false;
			controller.LastPig = null;
			controller.LastWinner = null;
		}
    }

	public class AWFighting : Item
    {
		public int m_MaxPlayers = 2;
		public int m_NumPlayers;
		public int m_DeadCount;
		public int m_Bet = 50000;  //The default amount to play per player.  This will make the winner get 10k per player as well.
		public int m_ArenaSize = 11;  //10 tiles by default.
		public bool m_GameRunning = false;
		public bool m_Tournament = false;
		public Mobile m_LastWinner;
		public Point3D m_Corner1;
		public Point3D m_Corner2;
		public Point3D m_Corner3;
		public Point3D m_Corner4;
		public FightingAW m_LastAW;
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int MaxPlayers
        {
            get
            {
                return m_MaxPlayers;
            }
            set
            {
				if(value < 2)
					value = 2;
					
				m_MaxPlayers = value;
				
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int NumberOfPlayers
        {
            get
            {
                return m_NumPlayers;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_NumPlayers = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public int DeadChickens
        {
            get
            {
                return m_DeadCount;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_DeadCount = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int Bet
        {
            get
            {
                return m_Bet;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_Bet = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int ArenaSize
        {
            get
            {
                return m_ArenaSize;
            }
            set
            {
				if(value < 10)
					value = 10;
					
                m_ArenaSize = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool GameRunning
        {
            get
            {
                return m_GameRunning;
            }
            set
            {
                m_GameRunning = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool Tournament
        {
            get
            {
                return m_Tournament;
            }
            set
            {
                m_Tournament = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner1
		{
			get { return m_Corner1; }
			set { m_Corner1 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner2
		{
			get { return m_Corner2; }
			set { m_Corner2 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner3
		{
			get { return m_Corner3; }
			set { m_Corner3 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner4
		{
			get { return m_Corner4; }
			set { m_Corner4 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.Owner )]
		public Mobile LastWinner{ get{ return m_LastWinner; } set { m_LastWinner = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public FightingAW LastAW
		{
			get { return m_LastAW; }
			set { m_LastAW = value; InvalidateProperties(); }
		}
		
		[Constructable]
        public AWFighting() : base( 0xED4 )
        {
			Movable = false;
			Hue = 2973;
			Name = "Ultimate Ancient Wyrm Championships";
        }

        public override void OnDoubleClick( Mobile from )
        {
			if(m_GameRunning)
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			else
			{
				Item item = from.Backpack.FindItemByType( typeof( RAFTRS ) );
				RAFTRS raftrs = item as RAFTRS;
				
				if(raftrs != null && raftrs.CurAmount >= m_Bet)
				{
					raftrs.CurAmount -= m_Bet;
					raftrs.CurrentBet += m_Bet;
					JoinGame(from, this);
					from.SendMessage("{0} has been withdrawn from your R.A.F.T.  The Ancient Wyrm you are betting on has been placed in the arena to warm up for the contest.", m_Bet);
					from.AddToBackpack (new GoldVoucher());
				}
				else if(raftrs != null && raftrs.CurrentBet > 0)
				{
					from.SendMessage("You have an outstanding Bet placed. You may only place one Bet at a time.");
				}
				else
				{
					from.SendMessage("You have to have a R.A.F.T. with enough Gold in it to place a Bet on an Ancient Wyrm.");
				}
			}
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
   
			//list.Add( 1042971, String.Format("Ancient Wyrm Contests - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );
			list.Add( 1042971, String.Format(this.Name.ToString() + " - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );

			if(m_Bet > 0)
			{
				list.Add( 1060659, "Cost To Play\t{0}", m_Bet );
				list.Add( 1070722, String.Format( "Winner Takes {0} to the Bank!", m_Bet * m_MaxPlayers ) );
			}

			if(m_Tournament)
				list.Add( 1060660, "Mode\t{0}", "Tournament");

			if(m_GameRunning)
				list.Add( "CONTEST IN PROGRESS");
        }
		

        public AWFighting( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();

			m_MaxPlayers = reader.ReadInt();
			m_NumPlayers = reader.ReadInt();
			m_DeadCount = reader.ReadInt();
			m_Bet = reader.ReadInt();
			m_ArenaSize = reader.ReadInt();
			m_GameRunning = reader.ReadBool();
			m_Corner1 = reader.ReadPoint3D();
			m_Corner2 = reader.ReadPoint3D();
			m_Corner3 = reader.ReadPoint3D();
			m_Corner4 = reader.ReadPoint3D();
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );

			writer.Write( (int)m_MaxPlayers);
			writer.Write( (int)m_NumPlayers);
			writer.Write( (int)m_DeadCount);
			writer.Write( (int)m_Bet);
			writer.Write( (int)m_ArenaSize);
			writer.Write( (bool)m_GameRunning);
			writer.Write( m_Corner1 );
			writer.Write( m_Corner2);
			writer.Write( m_Corner3 );
			writer.Write( m_Corner4 );
        }
		
		public void ReleaseTheChickens()
		{
			foreach (Mobile m in GetMobilesInRange( m_ArenaSize ))
			{
                if (m is FightingAW)
				{
					FightingAW fa = m as FightingAW;
					fa.Blessed = false;
					fa.Frozen = false;
					
					if(fa.Owner != null)
						fa.Owner.SendMessage("Begin!");
				}
			}
		}
		
		public void JoinGame(Mobile from, AWFighting controller)
		{
			if( !controller.GameRunning )
			{
				controller.NumberOfPlayers++;
				FightingAW fa = new FightingAW(from, controller);
				fa.Blessed = true;
				fa.Frozen = true;
				fa.Map = from.Map;
				
				switch(Utility.Random(4) )
				{
					case 0: { fa.Location = controller.Corner1; } break;
					case 1: { fa.Location = controller.Corner2; } break;
					case 2: { fa.Location = controller.Corner3; } break;
					case 3: { fa.Location = controller.Corner4; } break;
				}
				
				if(controller.NumberOfPlayers == controller.MaxPlayers)
				{
					controller.GameRunning = true;
					controller.ReleaseTheChickens();
				}
				
				fa.Combatant = controller.LastAW;
				controller.LastAW = fa;
			}
			else
			{
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			}
		}
		
		public void EndGame( AWFighting controller )
		{
			ArrayList list = new ArrayList();
			//End the game, because there is only one chicken left.
			foreach (Mobile m in GetMobilesInRange( controller.ArenaSize ))
			{
				
                if (m is FightingAW)
				{
					FightingAW fa = m as FightingAW;
					list.Add( fa );
				}
			}
			
			if( list.Count > 0 )
			{
				for( int i = 0; i < list.Count; i++ )
				{
					if( controller != null && !controller.Deleted)
					{
						if( (FightingAW)list[i] != null && !((FightingAW)list[i]).Deleted )
						{
							if( ((FightingAW)list[i]).Owner != null && !((FightingAW)list[i]).Owner.Deleted )
							{
								controller.LastWinner = ((FightingAW)list[i]).Owner;
								if(controller.Bet > 0)
								{
									Item a = ((FightingAW)list[i]).Owner.Backpack.FindItemByType( typeof( RAFTRS ) );
									RAFTRS raftrs = a as RAFTRS;
									Item b = ((FightingAW)list[i]).Owner.Backpack.FindItemByType( typeof( GoldVoucher ) );
									GoldVoucher gvoucher = b as GoldVoucher;
				
									if((raftrs != null) && (raftrs.CurrentBet == controller.Bet))
									{
										raftrs.WinningAmount += (controller.MaxPlayers * controller.Bet);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										if(gvoucher != null)
										{
											((FightingAW)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
										}
									}
									else if( gvoucher != null)
									{
										BankCheck bc = new BankCheck(controller.MaxPlayers * controller.Bet);
										((FightingAW)list[i]).Owner.AddToBackpack(bc);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										((FightingAW)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
									}
									else
									{
										((FightingAW)list[i]).Delete();
									}	
								}
							}
							((FightingAW)list[i]).Delete();
						}
					}
				}
			}		
			controller.DeadChickens = 0;
			controller.NumberOfPlayers = 0;
			controller.GameRunning = false;
			controller.LastAW = null;
			controller.LastWinner = null;
		}
    }
	public class AtlantisFighting : Item
    {
		public int m_MaxPlayers = 2;
		public int m_NumPlayers;
		public int m_DeadCount;
		public int m_Bet = 15000;  //The default amount to play per player.  This will make the winner get 10k per player as well.
		public int m_ArenaSize = 11;  //10 tiles by default.
		public bool m_GameRunning = false;
		public bool m_Tournament = false;
		public Mobile m_LastWinner;
		public Point3D m_Corner1;
		public Point3D m_Corner2;
		public Point3D m_Corner3;
		public Point3D m_Corner4;
		public FightingIntern m_LastIntern;

		[CommandProperty( AccessLevel.GameMaster )]
        public int MaxPlayers
        {
            get
            {
                return m_MaxPlayers;
            }
            set
            {
				if(value < 2)
					value = 2;
					
				m_MaxPlayers = value;
				
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int NumberOfPlayers
        {
            get
            {
                return m_NumPlayers;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_NumPlayers = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public int DeadChickens
        {
            get
            {
                return m_DeadCount;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_DeadCount = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int Bet
        {
            get
            {
                return m_Bet;
            }
            set
            {
				if(value < 0)
					value = 0;
					
                m_Bet = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
        public int ArenaSize
        {
            get
            {
                return m_ArenaSize;
            }
            set
            {
				if(value < 10)
					value = 10;
					
                m_ArenaSize = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool GameRunning
        {
            get
            {
                return m_GameRunning;
            }
            set
            {
                m_GameRunning = value; 
				InvalidateProperties();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public bool Tournament
        {
            get
            {
                return m_Tournament;
            }
            set
            {
                m_Tournament = value; 
				InvalidateProperties();
            }
        }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner1
		{
			get { return m_Corner1; }
			set { m_Corner1 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner2
		{
			get { return m_Corner2; }
			set { m_Corner2 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner3
		{
			get { return m_Corner3; }
			set { m_Corner3 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Corner4
		{
			get { return m_Corner4; }
			set { m_Corner4 = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.Owner )]
		public Mobile LastWinner{ get{ return m_LastWinner; } set { m_LastWinner = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public FightingIntern LastIntern
		{
			get { return m_LastIntern; }
			set { m_LastIntern = value; InvalidateProperties(); }
		}
		
		[Constructable]
        public AtlantisFighting() : base( 0xED4 )
        {
			Movable = false;
			Hue = 2823;
			Name = "Atlantean Gladiator Championships";
        }
        public override void OnDoubleClick( Mobile from )
        {
			if(m_GameRunning)
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			else
			{
				Item item = from.Backpack.FindItemByType( typeof( RAFTRS ) );
				RAFTRS raftrs = item as RAFTRS;
				
				if(raftrs != null && raftrs.CurAmount >= m_Bet)
				{
					raftrs.CurAmount -= m_Bet;
					raftrs.CurrentBet += m_Bet;
					raftrs.WinningAmount += m_Bet;
					JoinGame(from, this);
					from.SendMessage("{0} has been withdrawn from your R.A.F.T.  The Intern you are betting on has been placed in the arena to warm up for the contest.", m_Bet);
					from.AddToBackpack (new GoldVoucher());
				}
				else if(raftrs != null && raftrs.CurrentBet > 0)
				{
					from.SendMessage("You have an outstanding Bet placed. You may only place one Bet at a time.");
				}
				else
				{
					from.SendMessage("You have to have a R.A.F.T. with enough Gold in it to place a Bet on an Intern.");
				}
			}
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
   
			//list.Add( 1042971, String.Format("Atlantean Contests - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );
			list.Add( 1042971, String.Format(this.Name.ToString() + " - {0} / {1}  Players Ready To Play", m_NumPlayers, m_MaxPlayers) );

			if(m_Bet > 0)
			{
				list.Add( 1060659, "Cost To Play\t{0}", m_Bet );
				list.Add( 1070722, String.Format( "Winner Takes {0} to the Bank!", m_Bet * m_MaxPlayers ) );
			}

			if(m_Tournament)
				list.Add( 1060660, "Mode\t{0}", "Tournament");

			if(m_GameRunning)
				list.Add( "CONTEST IN PROGRESS");
        }
		

        public AtlantisFighting( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();

			m_MaxPlayers = reader.ReadInt();
			m_NumPlayers = reader.ReadInt();
			m_DeadCount = reader.ReadInt();
			m_Bet = reader.ReadInt();
			m_ArenaSize = reader.ReadInt();
			m_GameRunning = reader.ReadBool();
			m_Corner1 = reader.ReadPoint3D();
			m_Corner2 = reader.ReadPoint3D();
			m_Corner3 = reader.ReadPoint3D();
			m_Corner4 = reader.ReadPoint3D();
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );

			writer.Write( (int)m_MaxPlayers);
			writer.Write( (int)m_NumPlayers);
			writer.Write( (int)m_DeadCount);
			writer.Write( (int)m_Bet);
			writer.Write( (int)m_ArenaSize);
			writer.Write( (bool)m_GameRunning);
			writer.Write( m_Corner1 );
			writer.Write( m_Corner2);
			writer.Write( m_Corner3 );
			writer.Write( m_Corner4 );
        }
		
		public void ReleaseTheChickens()
		{
			foreach (Mobile m in GetMobilesInRange( m_ArenaSize ))
			{
                if (m is FightingIntern)
				{
					FightingIntern fi = m as FightingIntern;
					fi.Blessed = false;
					fi.Frozen = false;
					
					if(fi.Owner != null)
						fi.Owner.SendMessage("Begin!");
				}
			}
		}
		
		public void JoinGame(Mobile from, AtlantisFighting controller)
		{
			if( !controller.GameRunning )
			{
				controller.NumberOfPlayers++;
				FightingIntern fi = new FightingIntern(from, controller);
				fi.Blessed = true;
				fi.Frozen = true;
				fi.Map = from.Map;
				
				switch(Utility.Random(4) )
				{
					case 0: { fi.Location = controller.Corner1; } break;
					case 1: { fi.Location = controller.Corner2; } break;
					case 2: { fi.Location = controller.Corner3; } break;
					case 3: { fi.Location = controller.Corner4; } break;
				}
				
				if(controller.NumberOfPlayers == controller.MaxPlayers)
				{
					controller.GameRunning = true;
					controller.ReleaseTheChickens();
				}
				
				fi.Combatant = controller.LastIntern;
				controller.LastIntern = fi;
			}
			else
			{
				from.SendMessage("A contest is currently in progress, please wait until it is over.");
			}
		}
		
		public void EndGame( AtlantisFighting controller )
		{
			ArrayList list = new ArrayList();
			//End the game, because there is only one chicken left.
			foreach (Mobile m in GetMobilesInRange( controller.ArenaSize ))
			{
				
                if (m is FightingIntern)
				{
					FightingIntern fi = m as FightingIntern;
					list.Add( fi );
				}
			}
			
			if( list.Count > 0 )
			{
				for( int i = 0; i < list.Count; i++ )
				{
					if( controller != null && !controller.Deleted)
					{
						if( (FightingIntern)list[i] != null && !((FightingIntern)list[i]).Deleted )
						{
							if( ((FightingIntern)list[i]).Owner != null && !((FightingIntern)list[i]).Owner.Deleted )
							{
								controller.LastWinner = ((FightingIntern)list[i]).Owner;
								if(controller.Bet > 0)
								{
									Item a = ((FightingIntern)list[i]).Owner.Backpack.FindItemByType( typeof( RAFTRS ) );
									RAFTRS raftrs = a as RAFTRS;
									Item b = ((FightingIntern)list[i]).Owner.Backpack.FindItemByType( typeof( GoldVoucher ) );
									GoldVoucher gvoucher = b as GoldVoucher;
				
									if((raftrs != null) && (raftrs.CurrentBet == controller.Bet))
									{
										raftrs.WinningAmount += (controller.MaxPlayers * controller.Bet);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										if(gvoucher != null)
										{
											((FightingIntern)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
										}
									}
									else if( gvoucher != null)
									{
										BankCheck bc = new BankCheck(controller.MaxPlayers * controller.Bet);
										((FightingIntern)list[i]).Owner.AddToBackpack(bc);
										controller.LastWinner.SendMessage("You won the " + this.Name.ToString() + "! Your winnings are available to be withdrawn from the Gambling section of your R.A.F.T.");
										((FightingIntern)list[i]).Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
									}
									else
									{
										((FightingIntern)list[i]).Delete();
									}
								}
							}
							((FightingIntern)list[i]).Delete();
						}
					}
				}
			}			
			controller.DeadChickens = 0;
			controller.NumberOfPlayers = 0;
			controller.GameRunning = false;
			controller.LastIntern = null;
			controller.LastWinner = null;
		}
    }
}