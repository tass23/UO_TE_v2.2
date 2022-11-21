using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class ChristmasDay : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new ChristmasDay() );
		}

		public override DateTime Start{ get{ return new DateTime( 2021, 12, 19 ); } }	//start
		public override DateTime Finish{ get{ return new DateTime( 2022, 1, 9 ); } }	//end

		public override void GiveGift( Mobile mob )
		{
			RedStocking bag = new RedStocking();
			bag.DropItem( new HolidayTreeDeed() );
			bag.DropItem( new GingerBreadCookie() );
			bag.DropItem( new CandyCane() );
			bag.DropItem( new HearthOfHomeFireDeed() );
			bag.DropItem( new HolidayGarland() );
			bag.DropItem( new BlueSnowflake() );
			
			RudolphStatue deer = new RudolphStatue();
			deer.Name = "Rudolph the Riendeer raised by " + mob.Name;
			bag.DropItem( deer );
			
			SantasReindeer2 deer2 = new SantasReindeer2();
			deer2.Name = "A Reindeer raised by " + mob.Name;
			bag.DropItem( deer2 );
			
			int random = Utility.Random( 100 );

			if ( random < 30 )
				bag.DropItem( new RewardScroll() );
			else
				bag.DropItem( new HeritageToken() );
			
			switch ( GiveGift( mob, bag ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Merry Christmas from The Expanse! A Red Holiday Stocking has been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Merry Christmas from The Expanse! A Red Holiday Stocking has been placed in your bank box." );
					break;
			}
			GreenStocking bag2 = new GreenStocking();
			bag2.DropItem( new GingerBreadCookie() );
			bag2.DropItem( new CandyCane() );
			bag2.DropItem( new WreathDeed() );
			bag2.DropItem( new SantasSleighDeed() );
			bag2.DropItem( new HolidayGarland() );
			bag2.DropItem( new WhiteSnowflake() );
			
			RedPoinsettia sman = new RedPoinsettia();
			sman.Name = "A Red Poinsettia grown by " + mob.Name;
			bag2.DropItem( sman );
			
			WhitePoinsettia white = new WhitePoinsettia();
			white.Name = "A White Poinsettia grown by " + mob.Name;
			bag2.DropItem( white );
			
			//int random = Utility.Random( 100 );

			if ( random < 30 )
				bag2.DropItem( new HeritageToken() );
			else
				bag2.DropItem( new RewardScroll() );
			
			switch ( GiveGift( mob, bag2 ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Merry Christmas from The Expanse! A Green Holiday Stocking has been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Merry Christmas from The Expanse! A Green Holiday Stocking has been placed in your bank box." );
					break;
			}
		}
	}
}