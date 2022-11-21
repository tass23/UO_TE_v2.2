using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class IndependenceDay : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new IndependenceDay() );
		}

		public override DateTime Start{ get{ return new DateTime( 2021, 7, 1 ); } }		//start
		public override DateTime Finish{ get{ return new DateTime( 2021, 7, 6 ); } }	//end

		public override void GiveGift( Mobile mob )
		{
			IndependenceDayBag bag = new IndependenceDayBag();

			bag.DropItem( new FireworksBag() );
			bag.DropItem( new FountainBag() );
			bag.DropItem( new SparklerBag() );

			switch ( Utility.Random( 5 ) )
			{      	
				case 0: bag.DropItem(new Cake1()); break;
				case 1: bag.DropItem(new Cake2()); break;
				case 2: bag.DropItem(new Cake3()); break;
				case 3: bag.DropItem(new Cake4()); break;
				case 4: bag.DropItem(new Cake5()); break;
			}

			if ( 0.1 > Utility.RandomDouble() )
			{
				bag.DropItem( new RewardScroll () );
			}

			switch ( GiveGift( mob, bag ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "A gift has been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "A gift has been placed in your bank box." );
					break;
			}
		}
	}
}