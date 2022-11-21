using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class TrickOrTreat : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new TrickOrTreat() );
		}

		public override DateTime Start{ get{ return new DateTime( 2015, 10, 26 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2015, 11, 1 ); } }

		public override void GiveGift( Mobile mob )
		{
			TrickOrTreatBag2013 bag = new TrickOrTreatBag2013();
			
			switch ( GiveGift( mob, bag ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Time to Trick or Treat! A bag has been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Time to Trick or Treat! A bag has been placed in your bank box." );
					break;
			}
			GiftBoxOctogon box = new GiftBoxOctogon();
			box.Name = "Trick or Treat";
			box.Hue = Utility.RandomList( 43, 1175 );
			
			box.DropItem( new ShadowToken2013() );
			
			LargeDyingPlant plant1 = new LargeDyingPlant();
			plant1.Name = "A large dying house plant killed by " + mob.Name;
			box.DropItem( plant1 );
			
			DyingPlant plant2 = new DyingPlant();
			plant2.Name = "A dying house plant killed by " + mob.Name;
			box.DropItem( plant2 );
			
			CarvedPumpkin2 pump = new CarvedPumpkin2();
			pump.Name = "A spookie pumpkin carved by " + mob.Name;
			box.DropItem( pump );
			
			CarvedPumpkin pump1 = new CarvedPumpkin();
			pump1.Name = "A spookie pumpkin carved by " + mob.Name;
			box.DropItem( pump1 );
			
			switch ( GiveGift( mob, box ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Happy Halloween from The Expanse! A Halloween Gift Box has been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Happy Halloween from The Expanse! A Halloween Gift Box has been placed in your bank box." );
					break;
			}
			
		}
	}
}
