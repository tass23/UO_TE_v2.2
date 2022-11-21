using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server
{
	public class UnderworldEnterGump : Gump
	{
		public UnderworldEnterGump() : base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(264, 131, 257, 296, 9380);
			this.AddHtml( 289, 161, 212, 203, @"Welcome to the Underworld. In this dungeon, you will face many fearsome foes, including the Undead, daemons, demigods, and even the Gods themselves. Do not enter if thee are of faint heart. To cross the River, you must have two Ritual Coins, which can be earned by killing Wicked Priests in the Ankh Dungeon in Ilshenar. If you have the two coins and wish to pass, then press the button below.", (bool)false, (bool)true);
			this.AddButton(378, 364, 2642, 2643, (int)Buttons.EnterButton, GumpButtonType.Reply, 0);
		}
		
		public enum Buttons
		{
			Close,
			EnterButton
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			switch( info.ButtonID )
			{
				case 0:
				{ break; }
				case 1:
				{
					RitualCoin coin = from.Backpack.FindItemByType( typeof( RitualCoin )) as RitualCoin;
					if ( coin != null && coin.Amount >= 2 )
					{
						coin.Consume( 2 );
						from.MoveToWorld( new Point3D( 609, 1904, -89 ), Map.Malas );
						from.SendMessage( 1157, "You have entered the Underworld." );
						from.SendMessage( 1150, "If you wish to leave, double click the Belt of Hermes in your Backpack. You will be teleported back to the Britain Bank." );
						from.PlaySound( 581 );
						from.AddToBackpack( new BeltOfHermes() );
					}
					else
						from.SendMessage( 0, "You do not have enough Ritual Coins." );
					break;
				}
			}
		}
	}
}