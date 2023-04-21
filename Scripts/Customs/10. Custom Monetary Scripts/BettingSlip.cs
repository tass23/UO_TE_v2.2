using System;
using System.Globalization;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Quests;

namespace Server.Items
{
	public class BettingSlip : Item
	{
		private int m_BettingAmount;

		[CommandProperty( AccessLevel.GameMaster )]
		public int BettingAmount
		{
			get{ return m_BettingAmount; }
			set{ m_BettingAmount = value; InvalidateProperties(); }
		}

		public BettingSlip( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_BettingAmount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;
			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_BettingAmount = reader.ReadInt();
					break;
				}
			}
		}

		[Constructable]
		public BettingSlip( int BettingAmount ) : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 0x34;
			LootType = LootType.Blessed;
			Name = "a Betting Slip";
			m_BettingAmount = BettingAmount;
		}

		public override bool DisplayLootType{ get{ return Core.AOS; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			string BettingAmount;

			if ( Core.ML )
				BettingAmount = m_BettingAmount.ToString( "N0", CultureInfo.GetCultureInfo( "en-US" ) );
			else
				BettingAmount = m_BettingAmount.ToString();

			list.Add( 1060738, BettingAmount ); // value: ~1_val~
		}

		public override void OnDoubleClick( Mobile from )
		{
			BankBox box = from.FindBankNoCreate();
			if ( box != null && IsChildOf( box ) )
			{
				Delete();
				int deposited = 0;
				int toAdd = m_BettingAmount;
				Gold gold;

				while ( toAdd > 60000 )
				{
					gold = new Gold( 60000 );
					if ( box.TryDropItem( from, gold, false ) )
					{
						toAdd -= 60000;
						deposited += 60000;
					}
					else
					{
						gold.Delete();
						from.AddToBackpack( new BettingSlip( toAdd ) );
						toAdd = 0;
						break;
					}
				}

				if ( toAdd > 0 )
				{
					gold = new Gold( toAdd );
					if ( box.TryDropItem( from, gold, false ) )
					{
						deposited += toAdd;
					}
					else
					{
						gold.Delete();

						from.AddToBackpack( new BettingSlip( toAdd ) );
					}
				}

				// Gold was deposited in your account:
				from.SendLocalizedMessage( 1042672, true, " " + deposited.ToString() );
				PlayerMobile pm = from as PlayerMobile;
			}
			else
			{
				from.SendLocalizedMessage( 1047026 ); // That must be in your bank box to use it.
			}
		}
	}
	
	public class BettingVoucher : Item
	{
		public BettingVoucher( int amount ) : base( 0x14F0 )
		{
			Stackable = false;
			Amount = amount;
		}

		public BettingVoucher( Serial serial ) : base( serial )
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
	
	public class GoldVoucher : BettingVoucher
	{
		[Constructable]
		public GoldVoucher() : this( 1 )
		{
		}

		[Constructable]
		public GoldVoucher( int amount ) : base( amount )
		{
			Hue = 1161;
			Name = "a Gold Voucher";
		}

		public GoldVoucher( Serial serial ) : base( serial )
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
	public class SilverVoucher : BettingVoucher
	{
		[Constructable]
		public SilverVoucher() : this( 1 )
		{
		}

		[Constructable]
		public SilverVoucher( int amount ) : base( amount )
		{
			Hue = 2301;
			Name = "a Silver Voucher";
		}

		public SilverVoucher( Serial serial ) : base( serial )
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
}