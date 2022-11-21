//
using System; 
using Server; 
using Server.Commands;
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{

	public class CheckBook : Item
	{
		public int m_GPAmount;
		public int m_BlankChecks;

		[CommandProperty( AccessLevel.GameMaster )]
		public int GPAmount{ get{ return m_GPAmount; } set{ m_GPAmount = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int BlankChecks{ get{ return m_BlankChecks; } set{ m_BlankChecks = value; InvalidateProperties(); } }

		[Constructable]
		public CheckBook() : base( 0x2259 )
		{
			Movable = true;
			Weight = 1.0;
			Hue = 1255;
			Name = "The Expanse Checkbook";
			LootType = LootType.Blessed;
		}
                public override void OnDoubleClick( Mobile from )
                {

                        if ( !from.InRange( GetWorldLocation(), 2 ) )
                                from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
                        else if ( from is PlayerMobile )
                                from.SendGump( new CheckBookGump( (PlayerMobile)from, this, 0, null ) );
                }
		public override bool OnDragDrop ( Mobile from, Item dropped)
		{
			if( dropped is Gold )
			{
				GPAmount += dropped.Amount;
				dropped.Delete();
//from.SendGump( new CheckBookGump( (PlayerMobile)from, this, 0, null ) );
				return true;
			}
			else if( dropped is BankCheck )
			{
				BankCheck check = (BankCheck)dropped;
				GPAmount += check.Worth;
				check.Delete();
//from.SendGump( new CheckBookGump( (PlayerMobile)from, this, 0, null ) );
				return true;
			}
			else if( dropped is BlankScroll )
			{
				if( BlankChecks + dropped.Amount > 100 )
				{
					if ( from.HasGump( typeof( CheckBookGump ) ) )
from.SendGump( new CheckBookGump( (PlayerMobile)from, this, 0, "Check Book can only hold 100 blank checks." ) );
					else
						from.SendMessage("Check Book can only hold 100 blank checks.");

					return false;
				}
				else
				{
					BlankChecks += dropped.Amount;
					dropped.Delete();
//from.SendGump( new CheckBookGump( (PlayerMobile)from, this, 0, null ) );
					return true;
				}
			}
			else
			{
				from.SendMessage("That is not a valid entry.");
				return false;
			}
		}
		public CheckBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_GPAmount);
			writer.Write( (int) m_BlankChecks);
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_GPAmount = reader.ReadInt();
			m_BlankChecks = reader.ReadInt();
		}
	}
}

namespace Server.Gumps
{
    public class CheckBookGump : Gump
    {
		private PlayerMobile m_From;
		private CheckBook m_book;
		private int m_amt;
		private string m_err;
	/*	public static void Initialize()
		{
			CommandSystem.Register("CheckBookGump", AccessLevel.GameMaster, new CommandEventHandler(CheckBookGump_OnCommand));
		}

		private static void CheckBookGump_OnCommand(CommandEventArgs e)
		{
			e.Mobile.SendGump(new CheckBookGump(e.Mobile));
		} */

		public CheckBookGump(PlayerMobile owner, CheckBook book, int amt, string err) : base(50, 50)
		{
			owner.CloseGump( typeof( CheckBookGump ) );
			m_From = owner;
			m_book = book;
			m_amt = amt;
			m_err = err;

			AddPage(0);
			
			AddBackground( 10, 10, 600, 239, 2620 ); //9300 ); //5054 );
			AddBackground( 20, 20, 580, 219, 9300 );
			AddBackground( 180, 95, 125, 25, 3000 ); //balance
			AddBackground( 150, 175, 125, 25, 3000 ); //check amount
			AddBackground( 50, 95, 40, 25, 3000 ); //blanks
		

			AddImage( 5, 5, 10460 );
			AddImage( 600 - 15, 5, 10460 );
			AddImage( 5, 224, 10460 );
			AddImage( 600 - 15, 224, 10460 );

		AddLabel( 225, 30, 1365, "The Expanse Checkbook");
		AddLabel( 182, 70, 0, "Check Book Balance");
		AddLabel( 145, 150, 0, "Current Check Amount");
		AddLabel( 50, 70, 0, "Checks");

		AddLabel( 280, 98, 0, "GP" );
		AddLabel( 200, 98, 0, book.GPAmount.ToString() );
		AddLabel( 55, 98, 0, book.BlankChecks.ToString() );
	AddLabel( 170, 178 ,0, amt.ToString() );

if( err != null )	
AddLabel( 300, 215, 33, err.ToString() ); //error

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
                 //       from.SendMessage("GoodLuck");
                        break;
                    }
		case 1:
		{
			m_amt =( m_amt * 10) + 1;
		//	from.SendMessage(" Amt {0}",m_amt);
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 2:
		{
			m_amt =( m_amt * 10) + 2;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 3:
		{
			m_amt =( m_amt * 10) + 3;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 4:
		{
			m_amt =( m_amt * 10) + 4;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 5:
		{
			m_amt =( m_amt * 10) + 5;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 6:
		{
			m_amt =( m_amt * 10) + 6;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 7:
		{
			m_amt =( m_amt * 10) + 7;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 8:
		{
			m_amt =( m_amt * 10) + 8;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 9:
		{
			m_amt =( m_amt * 10) + 9;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 10:
		{
			m_amt =( m_amt * 10) + 0;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 20:
		{
			m_amt = 0;
			m_err = null;
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
		case 21:
		{
			if( m_book.m_BlankChecks < 1 )
			{
				m_err = "You are out of blank checks";
			}
			else if ( m_amt > m_book.m_GPAmount )
			{
				m_err = "You do not have that amount";
			}
			else if ( m_amt > 1000000 )
			{
				m_err = "Checks can not be over 1 million";
			}
			//if( m_amt <= 1000000 && m_amt <= m_book.m_GPAmount && m_book.m_BlankChecks > 0)
			else if( m_amt > 0 )
			{
				from.AddToBackpack( new BankCheck(m_amt) );
				m_book.m_BlankChecks -= 1;
				m_book.m_GPAmount -= m_amt;
				m_amt = 0;
				m_err = null;
			}
			else
			{
				m_err = "A check for the amount of ZERO?  Why?";
			}
			from.SendGump( new CheckBookGump( (PlayerMobile)from, m_book, m_amt, m_err ) );
			break;
		}
            }
        }
    }
}

