// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class Halloween : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new Halloween() );
		}

		public override DateTime Start{ get{ return new DateTime( 2007, 10, 1 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2007, 11, 1 ); } }

		public override void GiveGift( Mobile mob )
		{
			HalloweenBagAllGifts1 bag = new HalloweenBagAllGifts1();

			if ( 0.1 > Utility.RandomDouble() )
			{
				bag.DropItem( new HalloweenOuiJaBoard() );
			}

			switch ( GiveGift( mob, bag ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Happy Halloween from the Staff!  Gift items have been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Happy Halloween form the Staff!  Gift items have been placed in your bank box." );
					break;
			}
		}
	}
}
