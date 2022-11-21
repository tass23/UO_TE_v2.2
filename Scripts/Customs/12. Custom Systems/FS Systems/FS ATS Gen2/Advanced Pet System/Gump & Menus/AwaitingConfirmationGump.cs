using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class AwaitingConfirmationGump : Gump
	{
		private Mobile m_Pet1;
		private Mobile m_Pet2;

		public AwaitingConfirmationGump( Mobile pet1, Mobile pet2 ) : base( 0, 0 )
		{
			m_Pet1 = pet1;
			m_Pet2 = pet2;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(25, 22, 318, 324, 5054);
			AddHtml( 47, 60, 272, 253, @"<CENTER><U>Please Wait...</U><BR><BR>While the owner of the other creature confirms the request to breed the two animals.<BR><BR>Both you and the owner of the other pet will get one baby from this union.<BR><BR>If your request to breed the two animals is confirmed the and  is successful, You will get a claim ticket in your backpack, Both pets (Mother/Father) will be stabled in the animal breeders stable while they are mating and untill birth is complete. Birth will take place three days from the time of confirmation. Both pets will not be able to breed again for six days. Three days from now you should return to the animal breeder and drop the claim ticket on him. He will charge a fee of five thousand gold coins for his services. Then he will return your pet and your new baby.", (bool)false, (bool)true);
			AddLabel(52, 27, 1149, @"Awaiting Confirmation");
			AddButton(46, 321, 4005, 4006, 1, GumpButtonType.Reply, 0);
			//AddLabel(78, 322, 1149, @"Baby Stat Table");

		}
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			//Baby Stat Table
			if ( info.ButtonID == 1 )
			{
				from.CloseGump( typeof( AwaitingConfirmationGump ) );
				//from.CloseGump( typeof( BabyStatTableGump ) );

				from.SendGump( new AwaitingConfirmationGump( m_Pet1, m_Pet2 ) );
				//from.SendGump( new BabyStatTableGump( m_Pet1, m_Pet2 ) );
			}
		}
	}
}