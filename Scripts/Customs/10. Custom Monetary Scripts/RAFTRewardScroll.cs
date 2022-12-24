// Created by Raist for The Expanse (www.UOExpanse.com)
using System; 
using System.Collections;
using System.Collections.Generic;
using Server; 
using Server.Commands;
using Server.Gumps; 
using Server.Factions;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.ContextMenus;
using Server.Regions;
using Server.Spells;

namespace Server.Items
{
	public class RAFTRS : Item, TranslocationItem
	{
		public int m_CurrentBet;
		public int m_WinningAmount;
		public int i_Owner;
		public int i_Tokens;
		public int m_CurAmount;
		public int m_CurSilver;
		public int m_CurRS;
		public int m_BlankChecks;
		public int m_Charges;
        public int m_Recharges;
		public int m_MaxRecharges;
		public Mobile m_ROwner;
        
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile ROwner{ get{ return m_ROwner; } set{ m_ROwner = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int CurrentBet{ get{ return m_CurrentBet; } set{ m_CurrentBet = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int WinningAmount{ get{ return m_WinningAmount; } set{ m_WinningAmount = value; InvalidateProperties(); } }
		
		[CommandProperty(AccessLevel.Administrator)]
		public int Owner { get { return i_Owner; } set { i_Owner = value; InvalidateProperties(); } }
		
		[CommandProperty(AccessLevel.Administrator)]
		public int Tokens { get { return i_Tokens; } set { i_Tokens = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int CurAmount{ get{ return m_CurAmount; } set{ m_CurAmount = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int CurSilver{ get{ return m_CurSilver; } set{ m_CurSilver = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int CurRS{ get{ return m_CurRS; } set{ m_CurRS = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int BlankChecks{ get{ return m_BlankChecks; } set{ m_BlankChecks = value; InvalidateProperties(); } }
		
		[CommandProperty(AccessLevel.GameMaster)]
        public int Charges{ get { return m_Charges; } set{ if (value > this.MaxCharges) m_Charges = this.MaxCharges; else if (value < 0) m_Charges = 0; else m_Charges = value; InvalidateProperties(); }}

        [CommandProperty(AccessLevel.GameMaster)]
        public int Recharges{ get { return m_Recharges; } set{ if (value > this.MaxRecharges) m_Recharges = this.MaxRecharges; else if (value < 0) m_Recharges = 0; else m_Recharges = value; InvalidateProperties(); }}
		
		[CommandProperty(AccessLevel.GameMaster)]
        public int MaxCharges { get { return 100; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxRecharges { get { return 255; } }
		
		public string TranslocationItemName { get { return "RAFTRS"; } }

		[Constructable]
		public RAFTRS() : base( 8794 )
		{
			Name = "R.A.F.T.";
			Movable = true;
			Weight = 1.0;
			Hue = 1161;

			LootType = LootType.Blessed;
			m_Charges = 100;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (this.ROwner == null && from is PlayerMobile)
			{
				this.ROwner = from;
					Name = "R.A.F.T. owned by " + m_ROwner.Name.ToString();
			}

			if ( !from.InRange( GetWorldLocation(), 2 ) )
				from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			if (i_Owner == 0)
			{
				i_Owner = from.Serial;
				i_Tokens = 0;
				from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
			}
			else if (from.Serial == i_Owner)
			{
				from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
			}
		}
		
		public RAFTRS( Serial serial ) : base( serial )
		{
		}

		public override bool OnDroppedToMobile(Mobile from, Mobile target)
        {
            Item item = target.Backpack.FindItemByType(typeof(RAFTRS));

            if (item != null)
            {
                if (target == from)
                    from.SendMessage(2125, "You can only carry one R.A.F.T.");
                else
                    from.SendMessage(2125, "That player can only carry one R.A.F.T.");
                return false;
            }

            return true;
        }
		public override bool OnDroppedInto(Mobile from, Container target, Point3D p)
        {
            if (target == from.Backpack)
            {
                Item item = from.Backpack.FindItemByType(typeof(RAFTRS));

                if (item != null)
                {
                    from.SendMessage(2125, "You can only carry one R.A.F.T.");
                    return false;
                }
            }

            else if (target.IsChildOf(from.Backpack))
            {
                Item item = from.Backpack.FindItemByType(typeof(RAFTRS));

                if (item != null)
                {
                    from.SendMessage(2125, "You can only carry one R.A.F.T.");
                    return false;
                }
            }

            return target.OnDragDropInto(from, this, p);
        }

        public override bool OnDroppedOnto(Mobile from, Item target)
        {
            if (target == from.Backpack)
            {
                Item item = from.Backpack.FindItemByType(typeof(RAFTRS));

                if (item != null)
                {
                    from.SendMessage(2125, "You can only carry one R.A.F.T.");
                    return false;
                }
            }

            else if (target.IsChildOf(from.Backpack))
            {
                Item item = from.Backpack.FindItemByType(typeof(RAFTRS));

                if (item != null)
                {
                    from.SendMessage(2125, "You can only carry one R.A.F.T.");
                    return false;
                }
            }

            return target.OnDragDrop(from, this);
        }

		public override void UpdateTotals()
        {
            base.UpdateTotals();
        }
		
		public override void GetProperties(ObjectPropertyList list)
        {
			if (m_CurAmount >= 1)
				list.Add(1060738, this.m_CurAmount.ToString("#,0") + " GP"); //value: ~1_val~

            base.GetProperties(list);

			if (m_CurSilver >= 1)
				list.Add(1080443, this.m_CurSilver.ToString("#,0") + " SP");

			if (m_CurRS >= 1)
			list.Add(1115823, this.m_CurRS.ToString("#,0") + " RS");

			if (Tokens >= 1)
				list.Add(1071497, this.Tokens.ToString("#,0") + " TK");

            if (m_Charges >= 1)
                list.Add("Charges: {0}", m_Charges.ToString());
            else
                list.Add("No Charges");
        }
		
		public void BeginSetOwner( Mobile from )
		{
			from.Target = new SetOwnerTarget( this );
		}

		public class SetOwnerTarget : Target
		{
			private RAFTRS m_RFT;

			public SetOwnerTarget( RAFTRS rft ) : base( 18, false, TargetFlags.None )
			{
				m_RFT = rft;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_RFT.Deleted )
					return;

				m_RFT.EndSetOwner( from, targeted );
			}
		}
		
		public void EndSetOwner( Mobile from, object obj )
		{
			if ( obj is PlayerMobile )
			{                                
				PlayerMobile m = obj as PlayerMobile;
				if ( m.Alive )
				{
					if (!(this.Deleted))
					{
						if  (m.Name != null)
						{
							this.Owner = m.Serial;
						}
						else
							from.SendMessage(1173, "Your target does not have a name.");
					}
					else
						from.SendMessage(1173, "The R.A.F.T. was deleted before you selected your target.");
				}
				else
					from.SendMessage(1173, "Your target is dead. Please choose a target that is alive.");
			}
			else
				from.SendMessage(1173, "Only players can own a R.A.F.T.");
		}
		
		public override bool OnDragDrop ( Mobile from, Item dropped)
		{
			if( dropped is TokenCheck )
			{
				Tokens += dropped.Amount;
				dropped.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			if( dropped is Daat99Tokens )
			{
				Tokens += dropped.Amount;
				dropped.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			if( dropped is LootTokenCheck )
			{
				Tokens += dropped.Amount;
				dropped.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			if( dropped is RewardScroll )
			{
				CurRS += dropped.Amount;
				dropped.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			else if( dropped is BettingSlip )
			{
				BettingSlip slip = (BettingSlip)dropped;
				CurAmount += slip.BettingAmount;
				CurrentBet -=slip.BettingAmount;
				slip.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			else if( dropped is Gold )
			{
				CurAmount += dropped.Amount;
				dropped.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			else if( dropped is Silver )
			{
				CurSilver += dropped.Amount;
				dropped.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			else if( dropped is BankCheck )
			{
				BankCheck check = (BankCheck)dropped;
				CurAmount += check.Worth;
				check.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			else if( dropped is SilverCheck )
			{
				SilverCheck check = (SilverCheck)dropped;
				CurSilver += check.SilverAmount;
				check.Delete();
				//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
				return true;
			}
			else if( dropped is BlankScroll )
			{
				if( BlankChecks + dropped.Amount > 100 )
				{
					//if ( from.HasGump( typeof( RAFTRSGump ) ) )
						//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, "Your R.A.F.T. can only hold 100 blank notes." ) );
					//else
						from.SendMessage("Your R.A.F.T. can only hold 100 Blank Notes.");

					return false;
				}
				else
				{
					BlankChecks += dropped.Amount;
					dropped.Delete();
					//from.SendGump( new RAFTRSGump( (PlayerMobile)from, this, 0, 0, null ) );
					return true;
				}
			}
			else
			{
				from.SendMessage("That is not a valid entry.");
				return false;
			}
		}
		public void BeginAddTokens( Mobile from )
		{
			from.Target = new AddTokensTarget( this );
		}
		
		public class AddTokensTarget : Target
		{
			private RAFTRS m_raftrs;

			public AddTokensTarget( RAFTRS raftrs ) : base( 18, false, TargetFlags.None )
			{
				m_raftrs = raftrs;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_raftrs.Deleted )
					return;
				m_raftrs.EndAddTokens( from, targeted );
			}
		}

		public void EndAddTokens( Mobile from, object obj )
		{
			if ( obj is Item )
			{            
				Item oldTokens = obj as Item;
				if ( oldTokens.IsChildOf( from.Backpack ) )
				{
					if (oldTokens.Name != null && oldTokens.Stackable == true && oldTokens.Amount > 0)
					{
						if ((oldTokens.Name).ToLower().IndexOf("token") != -1)
						{
							if (!(this.Deleted) && !(oldTokens.Deleted))
							{
								this.Tokens = (this.Tokens + oldTokens.Amount);
								from.SendMessage(1173, "You added {0} tokens to your R.A.F.T.", oldTokens.Amount);
								oldTokens.Delete();
							}
							else
							{
								from.PlaySound(1069); //play Hey!! sound
								from.SendMessage(1173, "Hey, do not try to rob the bank!");
							}
						}
						else
						{
							from.PlaySound(1074); //play no!! sound
							from.SendMessage(1173, "This is not tokens!");
						}
					}
					else
						from.SendMessage(1173, "The R.A.F.T. rejected this item.");
				}
				else
					from.SendMessage(1173, "This is not in your backpack.");
			}
			else
				from.SendMessage(1173, "This is not an item.");
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (Mobile) m_ROwner );
			writer.Write( (int) m_CurrentBet);
			writer.Write( (int) m_WinningAmount);
			writer.Write( (int) i_Tokens );
			writer.Write( (int) i_Owner );
			writer.Write( (int) m_CurAmount);
			writer.Write( (int) m_CurSilver);
			writer.Write( (int) m_CurRS);
			writer.Write( (int) m_BlankChecks);

			writer.WriteEncodedInt((int)m_Recharges);
            writer.WriteEncodedInt((int)m_Charges);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_ROwner = reader.ReadMobile();
			m_CurrentBet = reader.ReadInt();
			m_WinningAmount = reader.ReadInt();
			i_Tokens = reader.ReadInt();
			i_Owner = reader.ReadInt();
			m_CurAmount = reader.ReadInt();
			m_CurSilver = reader.ReadInt();
			m_CurRS = reader.ReadInt();
			m_BlankChecks = reader.ReadInt();
			
			switch (version)
            {
				case 1:
                {
                    m_Recharges = reader.ReadEncodedInt();
                    goto case 0;
                }
                case 0:
                {
                    m_Charges = Math.Min(reader.ReadEncodedInt(), MaxCharges);
                    break;
                }
			}
		}
	}
}

namespace Server.Gumps
{
	public class RAFTRSGump : Gump
    {
		private PlayerMobile m_From;
		private RAFTRS m_book;
		private int m_Charges;
		private int m_amt;
		private string m_err;
		
		public RAFTRSGump(PlayerMobile owner, RAFTRS book, int charges, int amt, string err) : base( 50, 50 )
		{
			owner.CloseGump( typeof( RAFTRSGump ) );
			m_From = owner;
			m_book = book;
			m_Charges = charges;
			m_amt = amt;
			m_err = err;
				
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			
			AddImage(470, 100, 2440, 195);								//CurAmount BG
			AddImage(470, 130, 2440, 195);								//CurSilver BG
			AddImage(470, 190, 2440, 195);								//CurRS BG
			AddImage(470, 160, 2440, 195);								//Tokens BG
			AddImage(470, 220, 2440, 195);								//CurrentBet BG

			AddImage(422, 41, 9382);									//Top Right BG
			AddImageTiled(172, 41, 250, 140, 9381);						//Top Center BG
			AddImage(59, 41, 9380);										//Top Left BG
			AddImageTiled(58, 178, 114, 165, 9383);						//Middle Left BG
			AddImageTiled(147, 178, 275, 165, 9384);					//Middle Center BG
			AddImageTiled(422, 178, 114, 165, 9385);					//Middle Right BG
			AddImage(75, 340, 9307);									//Bottom Left BG
			AddImage(281, 340, 9307);									//Bottom Right BG
			//AddBackground(132, 116, 125, 25, 3000);					//Current Amount
			//AddBackground(132, 150, 125, 25, 3000);					//Reward Scroll Amount
			//AddBackground(132, 184, 125, 25, 3000);					//Betting Amount
			AddBackground(132, 276, 125, 25, 3000);						//Transfer Amount BG
			AddBackground(457, 106, 40, 25, 3000);						//Notes BG
			AddBackground(457, 136, 40, 35, 3000);						//Charges & Recharges BG
			AddImageTiled(97, 96, 400, 3, 96);							//Divider Center
			AddImage(88, 87, 95);										//Left Divider Tip
			AddImage(497, 87, 97);										//Right Divider Tip
			AddLabel(172, 76, 1080, @"The Expanse Registry and Funds Transfer");
			
			//AddLabel(97, 100, 0, @"Tally");							//Tally Label
			AddLabel(97, 251, 0, @"Transfer Amount");					//Transfer Amount Label
			AddLabel(417, 111, 0, @"Notes");							//Notes
			AddLabel(460, 111, 0, book.BlankChecks.ToString());			//Notes Remaining
			AddLabel(403, 136, 0, @"Charges");							//Charges
			AddLabel(390, 152, 0, @"Recharges");						//Recharges
			AddLabel(463, 136, 0, book.Charges.ToString());				//Charges count
			AddLabel(463, 152, 0, book.Recharges.ToString());			//Recharges count
			AddImageTiled(466, 153, 21, 3, 96);							//Charges Divider
			//AddLabel(262, 121, 0, @"GP");								//GP Label
			//AddLabel(262, 155, 0, @"RS");								//RS Label
			//AddLabel(262, 189, 0, @"BA");								//BA Label
			AddLabel(530, 163, 1164, book.CurRS.ToString("#,0"));		//Current Reward Scroll
			AddLabel(530, 103, 1160, book.CurAmount.ToString("#,0"));	//Current Amount
			AddLabel(530, 223, 1370, book.CurrentBet.ToString("#,0"));	//Current Betting Amount
			AddLabel(137, 281, 33, amt.ToString("#,0"));				//Amount To Transfer
			AddLabel(530, 193, 1172, book.Tokens.ToString("#,0"));		//Current Tokens
			AddLabel(530, 133, 1149, book.CurSilver.ToString("#,0"));	//Current Silver
			AddLabel(613, 163, 1164, @"RS");							//RS Label
			AddLabel(613, 103, 1160, @"GP");							//GP Label
			AddLabel(613, 223, 1370, @"GP");							//Gambling GP Label
			AddLabel(613, 193, 1172, @"TK");							//TK Label
			AddLabel(613, 133, 1149, @"SP");							//SP Label
			
			AddLabel(97, 100, 0, @"Type");
			AddRadio(132, 125, 210, 209, false, 11);					//Gold Check Button
			AddRadio(132, 145, 210, 209, false, 12);					//Silver Check Button
			AddRadio(132, 165, 210, 209, false, 13);					//Reward Scroll Button
			AddRadio(132, 185, 210, 209, false, 14);					//Token Check Button
			AddRadio(132, 205, 210, 209, false, 15);					//Betting Slip Button
			AddLabel(151, 125, 1160, @"Gold / Check");					//Gold / Check Label
			AddLabel(151, 145, 1149, @"Silver / Check");					//Silver / Check Label
			AddLabel(151, 165, 1164, @"Reward Scroll");					//Reward Scroll Label
			AddLabel(151, 185, 1172, @"Tokens");						//Tokens Label
			AddLabel(151, 205, 1370, @"Betting Slip");					//Betting Slip Label
			
		if( charges != 0)
		{
			AddLabel(275, 103, 1365, @"No Charges");					//No Charges Label
			AddBackground(271, 100, 79, 22, 3000);						//No Charges BG
		}
		else
		{
			AddButton(283, 100, 2071, 248, 22, GumpButtonType.Reply, 0);//Bank Button
			AddBackground(271, 100, 79, 22, 3000);						//Bank Button BG
			AddLabel(295, 102, 1365, @"BANK");							//Bank Button Label
		}
		if( m_book.m_CurrentBet != 0)
		{
			AddButton(395, 197, 2071, 248, 24, GumpButtonType.Reply, 0);//Stop Gambling Button
			AddBackground(381, 196, 90, 22, 3000);						//Winning Amount BG
			AddLabel(397, 197, 1370, @"Gambling");						//Gambling Label
			if (( m_book.m_CurrentBet != 0) & (m_book.m_WinningAmount !=0))
			{
				AddLabel(374, 216, 0, @"Possible Payoff");					//Payoff Label
				AddButton(393, 236, 2071, 248, 23, GumpButtonType.Reply, 0);//Withdraw Winning Amount Button
				AddBackground(381, 235, 90, 22, 3000);						//Winning Amount BG
				AddLabel(385, 236, 1370, book.WinningAmount.ToString());	//Winning Amount
			}
		}
		else
		{
			AddLabel(385, 197, 1210, @"Not Gambling");					//Not Gambling Label
		}
				
		if( err != null )	
			AddLabel( 100, 310, 1193, err.ToString() ); //error
			
			AddButton(277, 131, 208, 212, 1, GumpButtonType.Reply, 0);
			AddLabel(282, 131, 1365, @"1");
			AddButton(302, 131, 208, 212, 2, GumpButtonType.Reply, 0);
			AddLabel(307, 131, 1365, @"2");
			AddButton(327, 131, 208, 212, 3, GumpButtonType.Reply, 0);
			AddLabel(332, 131, 1365, @"3");
			AddButton(277, 156, 208, 212, 4, GumpButtonType.Reply, 0);
			AddLabel(282, 156, 1365, @"4");
			AddButton(302, 156, 208, 212, 5, GumpButtonType.Reply, 0);
			AddLabel(307, 156, 1365, @"5");
			AddButton(327, 156, 208, 212, 6, GumpButtonType.Reply, 0);
			AddLabel(332, 156, 1365, @"6");
			AddButton(277, 181, 208, 212, 7, GumpButtonType.Reply, 0);
			AddLabel(282, 181, 1365, @"7");
			AddButton(302, 181, 208, 212, 8, GumpButtonType.Reply, 0);
			AddLabel(307, 181, 1365, @"8");
			AddButton(327, 181, 208, 212, 9, GumpButtonType.Reply, 0);
			AddLabel(332, 181, 1365, @"9");
			AddButton(302, 206, 208, 212, 10, GumpButtonType.Reply, 0);
			AddLabel(307, 206, 1365, @"0");
			
			AddButton(430, 286, 12003, 12004, 20, GumpButtonType.Reply, 0);	//CANCEL
			AddBackground(430, 286, 75, 16, 3000);
			AddLabel(442, 284, 1365, @"CANCEL");
			
			AddButton(430, 312, 12000, 12001, 21, GumpButtonType.Reply, 0);	//POST
			AddBackground(430, 312, 75, 16, 3000);
			AddLabel(449, 310, 1365, @"POST");
		}

        public override void OnResponse(NetState state, RelayInfo info) //Function for GumpButtonType.Reply Buttons 
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Case uses the ActionIDs definied above. Case 0 definies the actions for the button with the action id 0 
                {
                    //Cancel 
					//from.SendMessage("GoodLuck");
                    break;
                }
				case 1:
				{
					m_amt =( m_amt * 10) + 1;
					//from.SendMessage(" Amt {0}",m_amt);
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 2:
				{
					m_amt =( m_amt * 10) + 2;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 3:
				{
					m_amt =( m_amt * 10) + 3;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 4:
				{
					m_amt =( m_amt * 10) + 4;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 5:
				{
					m_amt =( m_amt * 10) + 5;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 6:
				{
					m_amt =( m_amt * 10) + 6;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 7:
				{
					m_amt =( m_amt * 10) + 7;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 8:
				{
					m_amt =( m_amt * 10) + 8;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 9:
				{
					m_amt =( m_amt * 10) + 9;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 10:
				{
					m_amt =( m_amt * 10) + 0;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 20:
				{
					m_amt = 0;
					m_err = null;
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 21:
				{
					if (info.IsSwitched(11))
					{
						if ( m_amt > m_book.m_CurAmount )
						{
							m_err = "You do not have that much Gold.";
						}
						else if( m_amt > 0 && m_book.m_BlankChecks != 0 )
						{
							if (m_amt < 2001 )
							{
								from.AddToBackpack( new Gold(m_amt) );
							}
							else
							{
								from.AddToBackpack( new BankCheck(m_amt) );
								m_book.m_BlankChecks -= 1;
							}
							m_book.m_CurAmount -= m_amt;
							m_amt = 0;
							m_err = null;
						}
						else
						{
							m_err = "You cannot withdraw 0 Gold, or you have 0 Notes.";
						}
						from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
						break;
					}
					else if (info.IsSwitched(12))
					{
						if ( m_amt > m_book.m_CurSilver )
						{
							m_err = "You do not have that much Silver.";
						}
						else if( m_amt > 0 && m_book.m_BlankChecks != 0 )
						{
							if (m_amt < 2001 )
							{
								from.AddToBackpack( new Silver(m_amt) );
							}
							else
							{
								from.AddToBackpack( new SilverCheck(m_amt) );
								m_book.m_BlankChecks -= 1;
							}
							m_book.m_CurSilver -= m_amt;
							m_amt = 0;
							m_err = null;
						}
						else
						{
							m_err = "You cannot withdraw 0 Silver, or you have 0 Notes.";
						}
						from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
						break;
					}
					else if (info.IsSwitched(13))
					{
						if ( m_amt > m_book.m_CurRS )
						{
							m_err = "You do not have that many Reward Scrolls.";
						}
						else if( m_amt > 0 )
						{
							from.AddToBackpack( new RewardScroll(m_amt) );
							m_book.m_CurRS -= m_amt;
							m_amt = 0;
							m_err = null;
						}
						else
						{
							m_err = "You cannot withdraw 0 Reward Scrolls.";
						}
						from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
						break;
					}
					else if (info.IsSwitched(14))
					{
						if ( m_amt > m_book.i_Tokens )
						{
							m_err = "You do not have that many Tokens.";
						}
						else if( m_amt > 0 )
						{
							from.AddToBackpack( new Daat99Tokens(m_amt) );
							m_book.i_Tokens -= m_amt;
							m_amt = 0;
							m_err = null;
						}
						else
						{
							m_err = "You cannot withdraw 0 Tokens.";
						}
						from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
						break;
					}
					else if (info.IsSwitched(15))
					{
						if ( m_amt > m_book.m_CurAmount )
						{
							m_err = "You do not have that much Gold to Bet with.";
						}
						else if( m_amt > 0 )
						{
							from.AddToBackpack( new BettingSlip(m_amt) );
							m_book.m_BlankChecks -= 1;
							m_book.m_CurAmount -= m_amt;
							m_book.m_CurrentBet += m_amt;
							//m_book.m_WinningAmount += m_book.CurrentBet + m_book.m_CurAmount;
							m_amt = 0;
							m_err = null;
						}
						else
						{
							m_err = "You cannot withdraw a Betting Slip for 0 Gold.";
						}
						from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
						break;
					}
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}	
				case 22:
				{
					if (m_book.m_Charges <= 0)
					{
						from.SendMessage("You need to recharge this before you can open your Bank from here again.");
					}
					else
					{
						BankBox box = from.BankBox;
						if (box != null)
							box.Open();
						m_amt = 0;
						m_err = null;

						m_book.m_Charges--;

					}
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 23:
				{
					if ( m_book.m_WinningAmount == 0 )
					{
						m_err = "You do not have any Gold to claim from Gambling.";
					}
					else if(( m_book.m_WinningAmount > 0) & (from.Backpack.FindItemByType(typeof(GoldVoucher)) == null) & (from.Backpack.FindItemByType(typeof(SilverVoucher)) == null))
					{
						m_book.m_CurAmount += m_book.m_WinningAmount;
						m_book.m_WinningAmount = 0;
						m_book.m_CurrentBet = 0;
						m_err = null;
					}
					else if ((m_book.m_CurrentBet > 0) & (from.Backpack.FindItemByType(typeof(GoldVoucher)) == null) & (from.Backpack.FindItemByType(typeof(SilverVoucher)) == null))
					{
						m_book.m_CurAmount += m_book.m_CurrentBet;
						m_book.m_WinningAmount = 0;
						m_book.m_CurrentBet = 0;
						m_err = null;
					}
					else
					{
						from.SendMessage("You cannot withdraw 0 Gold from your Gambling Winnings, or you must throw away the Gold/Silver voucher(s) you are carrying first.");
					}
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
				case 24:
				{
					if(( m_book.m_WinningAmount > 0 ) & (from.Backpack.FindItemByType(typeof(GoldVoucher)) == null) & (from.Backpack.FindItemByType(typeof(SilverVoucher)) == null))
					{
						m_book.m_CurAmount += m_book.m_WinningAmount;
						m_book.m_WinningAmount = 0;
						m_book.m_CurrentBet = 0;
						m_err = null;
					}
					else if ((m_book.m_CurrentBet > 0) & (from.Backpack.FindItemByType(typeof(GoldVoucher)) == null) & (from.Backpack.FindItemByType(typeof(SilverVoucher)) == null))
					{
						m_book.m_CurAmount += m_book.m_CurrentBet;
						m_book.m_WinningAmount = 0;
						m_book.m_CurrentBet = 0;
						m_err = null;
					}
					else
					{
						from.SendMessage("You cannot withdraw your Gambling Winnings yet! You must throw away the Gold/Silver voucher(s) you are carrying first.");
					}
					from.SendGump( new RAFTRSGump( (PlayerMobile)from, m_book, m_Charges, m_amt, m_err ) );
					break;
				}
            }
        }
    }
}