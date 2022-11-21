using System;
using System.Text;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class StoneGoldCounter : Item
	{
		public int EconomyMultiplier;
		private DateTime next_GoldCount;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Eco_Multiplier { get { return EconomyMultiplier; } set { EconomyMultiplier = value; InvalidateProperties(); } }
		
		[Constructable]
		public StoneGoldCounter () : base( 0x0EDE )
		{
			Movable = false;
			Hue = 6;
			Name = "Gold Counting Stone";
			EconomyMultiplier = 100;
			
			next_GoldCount = (DateTime.Now + TimeSpan.FromMinutes( 1 ));
		}

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("Counting World Gold...");
            CountGold();
        }

        private static void CountGold()
        {
            int totalgold = 0;
            int numberofchecks = 0;
            int pilesofsixtyk = 0;

            foreach (Item item in World.Items.Values)
            {
            	/*if (item is CheckBook)
            	{
            		CheckBook cb = (CheckBook)item;
            		totalgold += cb.Token;
            	}*/
                if (item is BankCheck)
                {
                    BankCheck bc = (BankCheck)item;
                    totalgold += bc.Worth;
                }
                else if (item is Gold)
                {
                    Gold g = (Gold)item;
                    totalgold += g.Amount;
                }
            }
            World.Broadcast(0x35, true, "Total Gold in world is : {0}", totalgold);
            numberofchecks = totalgold / 1000000;
            World.Broadcast(0x35, true, "This is enough for {0} one million gold bank checks", numberofchecks);

            totalgold = totalgold - 1000000 * numberofchecks;
            pilesofsixtyk = totalgold / 60000;
            World.Broadcast(0x35, true, "This is enough for {0} piles of 60K", pilesofsixtyk);

            totalgold = totalgold - 60000 * pilesofsixtyk;
            World.Broadcast(0x35, true, "And this leaves {0} spare change :)", totalgold);
        }

        public StoneGoldCounter(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            
            writer.Write( EconomyMultiplier );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            
            EconomyMultiplier = reader.ReadInt();
		}
	}
}
