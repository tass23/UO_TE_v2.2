///daat99's Token System
///Made by daat99 based on idea by Viago :)
///Thanx for Murzin for all the grammer corrections :)
using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class TokenLedger : Item
	{
		private int i_Owner, i_Tokens;
		
		[CommandProperty(AccessLevel.Administrator)]
		public int Owner { get { return i_Owner; } set { i_Owner = value; InvalidateProperties(); } }
		
		[CommandProperty(AccessLevel.Administrator)]
		public int Tokens { get { return i_Tokens; } set { i_Tokens = value; InvalidateProperties(); } }
		
		[Constructable]
		public TokenLedger() : base( 7715 )
		{
			Stackable = false;
			Name = "An Empty Token Ledger";
			Hue = 1173;
			Weight = 1.0;
			LootType = LootType.Blessed;
		}
		
		public override void OnDoubleClick(Mobile from)
		{
			if ( this.IsChildOf( from.Backpack ) )
			{
				if (i_Owner == 0)
				{
					i_Owner = from.Serial;
					Name = from.Name + "'s Token Ledger";
					i_Tokens = 0;
					from.SendGump( new TokensGump( from, this ) );
				}
				else if (from.Serial == i_Owner)
				{
					from.SendGump( new TokensGump( from, this ) );
				}
				else if (from.AccessLevel >= AccessLevel.GameMaster)
				{
					from.SendMessage(1173, "Select a new owner for this token ledger.");
					BeginSetOwner( from );
				}
				else
				{
					from.PlaySound(1074); //play no!! sound
					from.SendMessage(1173, "This book is not yours and you cannot seem to write your name in it!");
				}
			}
			else
				from.SendMessage(1173, "The token ledger must be in your backpack to be used.");
		}
		
		public void BeginSetOwner( Mobile from )
		{
			from.Target = new SetOwnerTarget( this );
		}

		public class SetOwnerTarget : Target
		{
			private TokenLedger m_TL;

			public SetOwnerTarget( TokenLedger tl ) : base( 18, false, TargetFlags.None )
			{
				m_TL = tl;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_TL.Deleted )
					return;

				m_TL.EndSetOwner( from, targeted );
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
							this.Name = m.Name + "'s Token Ledger";
							from.SendMessage(1173, "You set the new owner as: {0}", m.Name);
							m.SendMessage(1173, "You became the owner of a new token ledger book.");
						}
						else
							from.SendMessage(1173, "Your target doesn't have a name.");
					}
					else
						from.SendMessage(1173, "The ledger was deleted before you selected your target.");
				}
				else
					from.SendMessage(1173, "Your target is dead, please choose a target that is alive.");
			}
			else
				from.SendMessage(1173, "Only players can be the owners of token ledger.");
		}

		public void BeginAddTokens( Mobile from )
		{
			from.Target = new AddTokensTarget( this );
		}
		
		public class AddTokensTarget : Target
		{
			private TokenLedger m_TL;

			public AddTokensTarget( TokenLedger tl ) : base( 18, false, TargetFlags.None )
			{
				m_TL = tl;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_TL.Deleted )
					return;
				m_TL.EndAddTokens( from, targeted );
			}
		}

		public void EndAddTokens( Mobile from, object obj )
		{
			from.CloseGump( typeof( TokensGump ) );
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
								from.SendMessage(1173, "You added {0} tokens to your ledger.", oldTokens.Amount);
								oldTokens.Delete();
							}
							else
							{
								from.PlaySound(1069); //play Hey!! sound
								from.SendMessage(1173, "Hey, don't try to rob the bank!!!");
							}
						}
						else
						{
							from.PlaySound(1074); //play no!! sound
							from.SendMessage(1173, "This isn't tokens!!!");
						}
					}
					else
						from.SendMessage(1173, "The ledger rejected this item.");
				}
				else
					from.SendMessage(1173, "This isn't in your backpack.");
			}
			else
				from.SendMessage(1173, "This is not an item.");
			from.SendGump( new TokensGump( from, this ) );
		}
		
		public TokenLedger( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( i_Tokens );
			writer.Write( i_Owner );
			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					i_Tokens = reader.ReadInt();
					i_Owner = reader.ReadInt();
					break;
				}
			}
		}
	}

	public class TokensGump : Gump
	{
		private Mobile m_From;
		private TokenLedger m_TL;
		
		public TokensGump( Mobile from, Item item ) : base( 50, 50 )
		{
			from.CloseGump( typeof( TokensGump ) );
			m_From = from;
			if (!(item is TokenLedger))
				return;
			TokenLedger tl = item as TokenLedger;
			m_TL = tl;

			AddPage(0);
			
			AddBackground(40, 40, 360, 325, 5054);
			
			AddLabel(75, 75, 69, item.Name);
			AddLabel(75, 100, 88, "You have " + ((TokenLedger)tl).Tokens + " Tokens.");
			AddLabel(75, 125, 32, @"Add Tokens to your Ledger manually:");
			AddButton(307, 130, 2460, 2461, 1, GumpButtonType.Reply, 0); //add tokens
			AddLabel(75, 150, 88, @"How much tokens you want to take out?");
			
			AddBackground(125, 200, 195, 41, 9270); //text entry backgrounf
			if (((TokenLedger)tl).Tokens >= 10000)
				AddTextEntry(145, 211, 155, 21, 39, 3, "10000"); //default text entry (where we write how much tokens)
			else
				AddTextEntry(145, 211, 155, 21, 39, 3, "" + ((TokenLedger)tl).Tokens + "");

			//token check
			AddLabel(75, 180, 69, @"Write a check:");
			AddImage(70, 200, 92);
			AddButton(79, 207, 2474, 0, 2, GumpButtonType.Reply, 0);

			//token item
			AddLabel(280, 180, 69, @"Extract tokens:");
			AddImage(325, 200, 92);
			AddButton(334, 207, 2474, 0, 4, GumpButtonType.Reply, 0);

			AddImage(70, 255, 7012);
			AddImage(300, 255, 7012);
			//AddLabel(146, 277, 38, @"Daat99's Token System");
		}
		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( m_TL.Deleted )
				return;
			else if ( info.ButtonID == 1 )
			{
				if (((TokenLedger)m_TL).Tokens <= 2000000000)
					m_TL.BeginAddTokens( m_From );
				else
					m_From.SendMessage(1173, "This token ledger is full");
				m_From.SendGump( new TokensGump( m_From, m_TL ) );
			}
			else if ( info.ButtonID == 2 || info.ButtonID == 4 )
			{
				TextRelay tr_TokenAmount = info.GetTextEntry( 3 );
				if(tr_TokenAmount != null)
				{
					int i_MaxAmount = 0;
					try
					{
						i_MaxAmount = Convert.ToInt32(tr_TokenAmount.Text,10);
					} 
					catch
					{
						m_From.SendMessage(1173, "Please make sure you write only numbers.");
					}
					if(i_MaxAmount > 0) 
					{
						if (i_MaxAmount <= ((TokenLedger)m_TL).Tokens)
						{
							if (info.ButtonID == 4 )
							{
								if (i_MaxAmount <= 60000)
								{
									m_From.AddToBackpack(new Daat99Tokens(i_MaxAmount));
									m_From.SendMessage(1173, "You extracted {0} tokens from your ledger.", i_MaxAmount);
									((TokenLedger)m_TL).Tokens = (((TokenLedger)m_TL).Tokens - i_MaxAmount);
								}
								else
									m_From.SendMessage(1173, "You can't extract more then 60,000 tokens at 1 time.");
							}
							else if (i_MaxAmount >= 10000)
							{
								m_From.AddToBackpack(new TokenCheck(i_MaxAmount));
								m_From.SendMessage(1173, "You wrote a token check in the value of {0} tokens.", i_MaxAmount);
								((TokenLedger)m_TL).Tokens = (((TokenLedger)m_TL).Tokens - i_MaxAmount);
							}
							else 
								m_From.SendMessage(1173, "You can't write checks for less then 10,000 tokens.");
						}
						else
							m_From.SendMessage(1173, "You don't have that many tokens in your ledger.");
					}
					m_From.SendGump( new TokensGump( m_From, m_TL ) );
				}
			}
		}
	}

	public class TokenCheck : Item
	{
		private int i_TokensAmount;
		
		[CommandProperty(AccessLevel.Administrator)]
		public int tokensAmount { get { return i_TokensAmount; } set { i_TokensAmount = value; InvalidateProperties(); } }
		
		[Constructable]
		public TokenCheck() : this( 100 )
		{
		}

		[Constructable]
		public TokenCheck( int tokensAmount ) : base( 5359 )
		{
			Stackable = false;
			Name = "A Token Check";
			Hue = 1173;
			Weight = 1.0;
			LootType = LootType.Blessed;
			i_TokensAmount = tokensAmount;
		}
		
		public override void OnDoubleClick(Mobile from)
		{
			if ( this.IsChildOf( from.Backpack ) )
			{
				Item[] items = from.Backpack.FindItemsByType( typeof( TokenLedger ) );

				foreach( TokenLedger tl in items )
				{
					if ( tl.Owner == from.Serial )
					{
						if ((tl.Tokens + i_TokensAmount) <= 2000000000 )
						{
							if (!(this.Deleted))
							{
								from.CloseGump( typeof( TokensGump ) );
								tl.Tokens = (tl.Tokens + i_TokensAmount);
								from.SendMessage(1173, "You added {0} Tokens to your ledger using a token check.", i_TokensAmount);
								from.SendGump( new TokensGump( from, tl ) );
								this.Delete();
								break;
							}
							else
							{
								from.PlaySound(1069); //play Hey!! sound
								from.SendMessage(1173, "Hey, don't try to rob the bank!!!");
								from.SendGump( new TokensGump( from, this ) );
							}
						}
						else 
							from.SendMessage(1173, "You have a full token ledger, please make a check and store it in your bank.");
					}
				}
			}
			else
				from.SendMessage(1173, "The check must be in your backpack to be used.");
		}

		public override int LabelNumber{ get{ return 1041361; } } // A bank check

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list );

			list.Add( 1060738, i_TokensAmount.ToString() ); // value: ~1_val~
		}
		
		public TokenCheck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( i_TokensAmount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					i_TokensAmount = reader.ReadInt();
					break;
				}
			}
		}
	}
	
	public class LootTokenCheck : Item
	{
		private int i_TokensAmount;
		
		[CommandProperty(AccessLevel.Administrator)]
		public int tokensAmount { get { return i_TokensAmount; } set { i_TokensAmount = value; InvalidateProperties(); } }
		
		[Constructable]
		public LootTokenCheck() : this( 100 )
		{
		}

		[Constructable]
		public LootTokenCheck( int tokensAmount ) : base( 5359 )
		{
			Stackable = false;
			Name = "A Token Check";
			Hue = 1173;
			Weight = 1.0;
			i_TokensAmount = tokensAmount;
		}
		
		public override void OnDoubleClick(Mobile from)
		{
			if ( this.IsChildOf( from.Backpack ) )
			{
				Item[] items = from.Backpack.FindItemsByType( typeof( TokenLedger ) );

				foreach( TokenLedger tl in items )
				{
					if ( tl.Owner == from.Serial )
					{
						if ((tl.Tokens + i_TokensAmount) <= 2000000000 )
						{
							if (!(this.Deleted))
							{
								from.CloseGump( typeof( TokensGump ) );
								tl.Tokens = (tl.Tokens + i_TokensAmount);
								from.SendMessage(1173, "You added {0} Tokens to your ledger using a token check.", i_TokensAmount);
								from.SendGump( new TokensGump( from, tl ) );
								this.Delete();
								break;
							}
							else
							{
								from.PlaySound(1069); //play Hey!! sound
								from.SendMessage(1173, "Hey, don't try to rob the bank!!!");
								from.SendGump( new TokensGump( from, this ) );
							}
						}
						else 
							from.SendMessage(1173, "You have a full token ledger, please make a check and store it in your bank.");
					}
				}
			}
			else
				from.SendMessage(1173, "The check must be in your backpack to be used.");
		}

		public override int LabelNumber{ get{ return 1041361; } } // A bank check

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list );

			list.Add( 1060738, i_TokensAmount.ToString() ); // value: ~1_val~
		}
		
		public LootTokenCheck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( i_TokensAmount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					i_TokensAmount = reader.ReadInt();
					break;
				}
			}
		}
	}

	public class Daat99Tokens : Item
	{
		public Daat99Tokens( int min, int max ) : this( Utility.Random( min, max-min ) )
		{
		}

		[Constructable]
		public Daat99Tokens() : this( 1 )
		{
		}

		[Constructable]
		public Daat99Tokens( int amount ) : base( 0x103F )
		{
			Stackable = true;
			Name = "Currency Token";
			Hue = 1173;
			Weight = 0;
			Amount = amount;
			LootType = LootType.Blessed;
		}
		
		// public override Item Dupe( int amount )
		// {
			// return base.Dupe( new Daat99Tokens( amount ), amount );
		// }

		public override void OnDoubleClick(Mobile m)
		{
			Item[] items = m.Backpack.FindItemsByType( typeof( TokenLedger ) );

			foreach( TokenLedger tl in items )
			{
				if (tl.Owner == m.Serial && tl.Tokens + Amount <= 2000000000)
				{
					tl.Tokens = (tl.Tokens + Amount); //give the tokens
					Delete();
					m.SendMessage(1173, "You added {0} tokens to your token ledger.", Amount); //send a message to the player that he got tokens
					break;
				}
			}
		}
		
		public Daat99Tokens( Serial serial ) : base( serial )
		{
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

	public class GiveTokens
	{ 
		public static void CalculateTokens(Mobile m, BaseCreature creature)
		{
			if (creature.IsBonded)
				return;

			double d_Resists = ((creature.PhysicalResistance + creature.FireResistance + creature.ColdResistance + creature.PoisonResistance + creature.EnergyResistance)/50); //set the amount of resists the monster have
			if (d_Resists < 1.0) //if it have less then total on 100 resists set to 1
				d_Resists = 1.0;
			int i_Hits = (creature.HitsMax/10); //set the amount of max hp the creature had.
			double d_TempTokReward = (i_Hits + ((i_Hits * d_Resists)/10) ); //set the temp reward
						
			int i_FameKarma = creature.Fame; //set fame\karma reward bonus
			if (creature.Karma < 0)
				i_FameKarma -= creature.Karma;
			else
				i_FameKarma += creature.Karma;
			i_FameKarma = (i_FameKarma/250);
			if (i_FameKarma < 0)
				i_FameKarma = 0;
			d_TempTokReward += i_FameKarma; //add the fame\karma reward to the temp reward

			if (creature.AI == AIType.AI_Mage) //if it's mage add some tokens, it have spells
			{
				double d_Mage = ((creature.Skills[SkillName.Meditation].Value + creature.Skills[SkillName.Magery].Value + creature.Skills[SkillName.EvalInt].Value)/8);
				d_TempTokReward += d_Mage;
			}
							
			if (creature.HasBreath) //give bonus for creatures that have fire breath
			{
				double d_FireBreath = (creature.HitsMax/25);
				d_TempTokReward += d_FireBreath; //add the firebreath bonus to temp reward
			}	
							
			int i_TokReward = ((int)d_TempTokReward); //set the reward you'll actually get as half then the temp reward.
			i_TokReward = Utility.RandomMinMax((int)(i_TokReward*0.4), (int)(i_TokReward*0.5));
			if (i_TokReward < 1)
				i_TokReward = 1; //set minimum reward to 1
							
			RewardTokens(m, i_TokReward);
		}

		public static void RewardTokens(Mobile m, int amount)
		{
			if (amount < 1)
				return;

			Item[] items = m.Backpack.FindItemsByType( typeof( TokenLedger ) );

			foreach( TokenLedger tl in items )
			{
				if (tl.Owner == m.Serial && tl.Tokens <= 2000000000)
				{
					tl.Tokens = (tl.Tokens + amount); //give the tokens
					m.SendMessage(1173, "You recieved {0} tokens.",amount); //send a message to the player that he got tokens
					break;
				}
			}
		}
	}
}