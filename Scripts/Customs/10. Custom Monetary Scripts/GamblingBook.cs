using System; 
using Server; 
using Server.Commands;
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{

	public class GamblingBook : Item
	{
		public int m_CurrentBet;
		public int m_BlankChecks;
		public int m_WinningAmount;

		[CommandProperty( AccessLevel.GameMaster )]
		public int CurrentBet{ get{ return m_CurrentBet; } set{ m_CurrentBet = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int BlankChecks{ get{ return m_BlankChecks; } set{ m_BlankChecks = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int WinningAmount{ get{ return m_WinningAmount; } set{ m_WinningAmount = value; InvalidateProperties(); } }

		[Constructable]
		public GamblingBook() : base( 0x2259 )
		{
			Movable = true;
			Weight = 1.0;
			Hue = 1255;
			Name = "The Expanse Betting Book";
			LootType = LootType.Blessed;
		}
        public override void OnDoubleClick( Mobile from )
        {

            if ( !from.InRange( GetWorldLocation(), 2 ) )
                from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
            else if ( from is PlayerMobile )
                from.SendGump( new GamblingBookGump( (PlayerMobile)from, this, 0, null, 0 ) );
        }
		public override bool OnDragDrop ( Mobile from, Item dropped)
		{
			if( dropped is Gold )
			{
				CurrentBet += dropped.Amount;
				dropped.Delete();
				return true;
			}
			else if( dropped is BankCheck )
			{
				BankCheck check = (BankCheck)dropped;
				CurrentBet += check.Worth;
				check.Delete();
				return true;
			}
			else if( dropped is BlankScroll )
			{
				if( BlankChecks + dropped.Amount > 100 )
				{
					if ( from.HasGump( typeof( GamblingBookGump ) ) )
						from.SendGump( new GamblingBookGump( (PlayerMobile)from, this, 0, "The Gambling Book can only hold 100 blank checks.", 0 ) );
					else
						from.SendMessage("The Gambling Book can only hold 100 blank checks.");

					return false;
				}
				else
				{
					BlankChecks += dropped.Amount;
					dropped.Delete();
					return true;
				}
			}
			else
			{
				from.SendMessage("That is not a valid entry.");
				return false;
			}
		}
		public GamblingBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_CurrentBet);
			writer.Write( (int) m_BlankChecks);
			writer.Write( (int) m_WinningAmount);
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_CurrentBet = reader.ReadInt();
			m_BlankChecks = reader.ReadInt();
			m_WinningAmount = reader.ReadInt();
		}
	}
}

namespace Server.Gumps
{
    public class GamblingBookGump : Gump
    {
		private PlayerMobile m_From;
		private GamblingBook m_book;
		private int m_amt;
		private string m_err;
		private int m_winamt;

		public GamblingBookGump(PlayerMobile owner, GamblingBook book, int amt, string err, int winamt) : base(50, 50)
		{
			owner.CloseGump( typeof( GamblingBookGump ) );
			m_From = owner;
			m_book = book;
			m_amt = amt;
			m_err = err;
			m_winamt = winamt;

			AddPage(0);
			
			AddBackground( 10, 10, 600, 239, 5054 ); //9300 ); //5054 );
			AddBackground( 145, 95, 125, 25, 3000 ); //Current Bet Amount
			AddBackground( 150, 175, 125, 25, 3000 ); //Withdrawal Amount
			AddBackground( 50, 95, 40, 25, 3000 ); //Blank Checks
			AddBackground(290, 95, 125, 25, 3000); //Winning Amount

			AddLabel(225, 30, 1080, "The Expanse Gambling Book");
			
			AddLabel(148, 70, 1067, "Current Bet Amount");
			AddLabel(227, 99, 0, "GP" );
			AddLabel(148, 98, 0, book.CurrentBet.ToString() );
			
			AddLabel(293, 70, 1067, "Winning Amount");
			AddLabel(372, 99, 1075, "GP");
			AddLabel(293, 98, 1075, book.WinningAmount.ToString() );
			
			AddLabel(130, 149, 1067, "Withdrawal Amount");
			AddLabel(170, 178 ,0, amt.ToString() );
			
			AddLabel(50, 70, 1067, "Blank Slips");
			AddLabel(55, 98, 0, book.BlankChecks.ToString() );

			if( err != null )	
			AddLabel(300, 215, 33, err.ToString() ); //error

			AddButton(420, 75, 212, 212, 1, GumpButtonType.Reply, 0);
			AddLabel(427, 75, 1365, "1" );
			AddButton(445, 75, 212, 212, 2, GumpButtonType.Reply, 0);
			AddLabel(450, 75, 1365, "2" );
			AddButton(470, 75, 212, 212, 3, GumpButtonType.Reply, 0);
			AddLabel(475, 75, 1365, "3" );

			AddButton(420, 100, 212, 212, 4, GumpButtonType.Reply, 0);
			AddLabel(425, 100, 1365, "4" );
			AddButton(445, 100, 212, 212, 5, GumpButtonType.Reply, 0);
			AddLabel(450, 100, 1365, "5" );
			AddButton(470, 100, 212, 212, 6, GumpButtonType.Reply, 0);
			AddLabel(475, 100, 1365, "6" );

			AddButton(420, 125, 212, 212, 7, GumpButtonType.Reply, 0);
			AddLabel(425, 125, 1365, "7" );
			AddButton(445, 125, 212, 212, 8, GumpButtonType.Reply, 0);
			AddLabel(450, 125, 1365, "8" );
			AddButton(470, 125, 212, 212, 9, GumpButtonType.Reply, 0);
			AddLabel(475, 125, 1365, "9" );
			AddButton(445, 150, 212, 212, 10, GumpButtonType.Reply, 0);
			AddLabel(450, 150, 1365, "0" );

			AddButton(375, 190, 12003, 12004, 20, GumpButtonType.Reply, 0); //clear
			AddButton(460, 190, 12000, 12001, 21, GumpButtonType.Reply, 0); //accept

            //--------------------------------------------------------------------------------------------------------------
        }

        public override void OnResponse(NetState state, RelayInfo info) //Function for GumpButtonType.Reply Buttons 
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
                {
                    //Cancel 
					//from.SendMessage("GoodLuck");
                    break;
                }
				case 1:
				{
					m_amt =( m_amt * 10) + 1;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 2:
				{
					m_amt =( m_amt * 10) + 2;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 3:
				{
					m_amt =( m_amt * 10) + 3;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 4:
				{
					m_amt =( m_amt * 10) + 4;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 5:
				{
					m_amt =( m_amt * 10) + 5;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 6:
				{
					m_amt =( m_amt * 10) + 6;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 7:
				{
					m_amt =( m_amt * 10) + 7;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 8:
				{
					m_amt =( m_amt * 10) + 8;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 9:
				{
					m_amt =( m_amt * 10) + 9;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 10:
				{
					m_amt =( m_amt * 10) + 0;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 20:
				{
					m_amt = 0;
					m_err = null;
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
				case 21:
				{
					if( m_book.m_BlankChecks < 1 )
					{
						m_err = "You are out of blank checks";
					}
					else if ( m_amt > m_book.m_WinningAmount )
					{
						m_err = "You do not have that amount";
					}
					else if ( m_amt > 1000000 )
					{
						m_err = "Bank checks can not be over 1 million";
					}
					else if( m_amt > 0 )
					{
						from.AddToBackpack( new BankCheck(m_amt) );
						m_book.m_BlankChecks -= 1;
						m_book.m_WinningAmount -= m_amt;
						m_amt = 0;
						m_err = null;
					}
					else
					{
						m_err = "A check for the amount of ZERO?  Why?";
					}
					from.SendGump( new GamblingBookGump( (PlayerMobile)from, m_book, m_amt, m_err, m_winamt ) );
					break;
				}
            }
        }
    }
}