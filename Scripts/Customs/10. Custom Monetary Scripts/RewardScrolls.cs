using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;

namespace ExpanseDelivery.Items
{
	public class ExpanseGift : Item
    {
        public static class Config
        {
            public static bool Enabled = true;                              // Item delivery enabled?
            public static int MinutesOnline = 60;                           // Gift delivery every X minutes.
            public static bool DropOnBank = true;                           // Gifts in Bankbox (true) or backpack (false)?
            public static AccessLevel MaxAccessLevel = AccessLevel.Player;  // This accesslevel and lower receives gifts.
        }

        public static void Initialize()
        {
            if (Config.Enabled)
                new ExpanseGiftAutoDistributionTimer().Start();
        }

        public ExpanseGift(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
			
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
			
            reader.ReadInt();
        }

        class ExpanseGiftAutoDistributionTimer : Timer
        {
            public ExpanseGiftAutoDistributionTimer() : base(TimeSpan.Zero, TimeSpan.FromMinutes(Config.MinutesOnline))
            {
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                foreach (NetState state in NetState.Instances)
                {
                    PlayerMobile m = state.Mobile as PlayerMobile;

                    if (m != null && m.AccessLevel <= Config.MaxAccessLevel)
                    {
                        if (Config.DropOnBank && m.BankBox != null)
                        {
                            Item rs = m.BankBox.FindItemByType(typeof(RewardScroll));

                            if (rs != null)
                            {
                                rs.Amount++;
                            }
                            else
                            {
                                m.BankBox.DropItem(new RewardScroll());
                            }
                        }
                        else if (m.Backpack != null)
                        {
                            Item rs = m.Backpack.FindItemByType(typeof(RewardScroll));

                            if (rs != null)
                            {
                                rs.Amount++;
                            }
                            else
                            {
                                m.Backpack.DropItem(new RewardScroll());
                            }
                        }
                    }
                }
            }
        }
    }
}
	
namespace Server.Items
{
	public abstract class BaseRewardScroll : Item	
	{
		public override double DefaultWeight
		{
			get { return 0.0; }
		}
		
		public BaseRewardScroll() : base( 0x2D51 )
		{
			
		}
		public BaseRewardScroll( int amount ) : base( 0x2D51 )
		{
			Name = "Reward Scroll";
			Hue = 1165;
			Stackable = true;
			Amount = amount;
			LootType = LootType.Blessed;
		}
		
		public BaseRewardScroll( Serial serial ) : base( serial )
		{
			
		}

		public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(String.Format("{0} Reward Scrolls", this.Amount));
            else
                list.Add("a Reward Scroll");
        }
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}

	public class RewardScrollDeed : Item 
	{
		private int m_Worth;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Worth
		{
			get{ return m_Worth; }
			set{ m_Worth = value; InvalidateProperties(); }
		}
		public RewardScrollDeed( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_Worth );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Worth = reader.ReadInt();
					break;
				}
			}
		}
		[Constructable]
		public RewardScrollDeed( int worth) : base (5360)
		{
			Movable = true;
			Hue = 1165;
			Name = "a Reward Scroll Deed";
			m_Worth = worth;
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			string worth;
			worth = m_Worth.ToString();

			list.Add( 1060738, worth ); // value: ~1_val~
		}
		
		public override void OnDoubleClick( Mobile from)
		{
			Container pack = from.Backpack;

			int amounts = 0;
			int toAdd = m_Worth;
			RewardScroll rewardscroll;

			if ( toAdd > 0 )
			{
				rewardscroll = new RewardScroll(); //( toAdd );
				rewardscroll.Amount = toAdd;

				if ( pack.TryDropItem( from, rewardscroll, false ) )
				{
					amounts += toAdd;
					this.Delete();
				}
				else
				{
					this.Delete();

					from.AddToBackpack( new RewardScrollDeed( toAdd ) );
				}
			}
			//from.AddToBackpack(new RewardScroll(worth));
			//this.Delete();
		}
	} 
	
	public class RewardScroll : BaseRewardScroll
	{
		/*
		[Constructable]
		public RewardScroll() :this (1)
		{
		}
		[Constructable]
		public RewardScroll( int amount ) : base( amount )
		{
		}
		*/
		[Constructable]
		public RewardScroll() : this( 1 )
		{
		}

		[Constructable]
		public RewardScroll( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public RewardScroll( int amount ) : base()
		{
			Name = "Reward Scroll";
			Hue = 1165;
			Stackable = true;
			Amount = amount;
			LootType = LootType.Blessed;
		}
		public RewardScroll( Serial serial ) : base( serial )
		{
			
		}

		public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(String.Format("{0} Reward Scrolls", this.Amount));
            else
                list.Add("a Reward Scroll");
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}