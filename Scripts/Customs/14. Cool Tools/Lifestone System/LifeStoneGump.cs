using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class LifeStoneGump : Gump
	{
		private Mobile m_Owner;
		
		public LifeStoneGump( Mobile owner ) : base( 60, 60 )
		{
			owner.CloseGump( typeof( LifeStoneGump ) );

			m_Owner = owner;
			
			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage( 0 );

			AddImage( 0, 0, 0x816 );
			AddButton( 34, 74, 0x81A, 0x81B, 1, GumpButtonType.Reply, 0 ); // OK
			AddButton( 88, 74, 0x995, 0x996, 2, GumpButtonType.Reply, 0 ); // Cancel

			string msg = "Tie to LifeStone?";
			AddHtml( 30, 25, 120, 40, msg, false, false );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			PlayerMobile from = ((PlayerMobile)(state.Mobile));
			from.Frozen = false;

			if ( info.ButtonID == 1 )
			{
					from.LifeStone = from.Location;

					from.LifeStoneMap = from.Map;

					from.Bound = true;
					from.FixedParticles( 0x375A, 10, 15, 5017, EffectLayer.Waist );
					from.PlaySound( 0x1EE );
					from.Animate( 17, 5, 1, true, false, 0 );

            		from.SendMessage( "LifeStone set." );
	
			}
	  	}
 	 }
}
