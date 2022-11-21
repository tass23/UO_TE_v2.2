using System;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class LifeStoneReturnGump : Gump
	{
		private Mobile m_Owner;
		
		public LifeStoneReturnGump( Mobile owner ) : base( 60, 60 )
		{
			owner.CloseGump( typeof( LifeStoneReturnGump ) );

			m_Owner = owner;

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage( 0 );

			AddImage( 0, 0, 0x816 );
			AddButton( 34, 74, 0x81A, 0x81B, 1, GumpButtonType.Reply, 0 ); // OK
			AddButton( 88, 74, 0x995, 0x996, 2, GumpButtonType.Reply, 0 ); // Cancel

			string msg = "Resurrect at LifeStone?";
			AddHtml( 30, 25, 120, 40, msg, false, false );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			PlayerMobile from = ((PlayerMobile)(state.Mobile));

			if ( info.ButtonID == 1 )
			{
				if ( from.Alive )
					from.SendMessage( "The stone cannot transport corporeal material!");
				else
				{
				  	from.Location = from.LifeStone;
					
					from.Map = from.LifeStoneMap;

					from.Corpse.Location = new Point3D( from.Location );

					from.Corpse.Map = from.LifeStoneMap;
						
					from.PlaySound( 0x214 );
					from.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
					from.Resurrect();
            		from.SendMessage( "You have been returned to your lifestone." );	
				}				
			}
			if ( info.ButtonID == 2 )
			{
				if ( from.Alive )
					from.SendMessage( "The stone cannot transport corporeal material!");
				else
				{
				  	from.Bound = false;
					from.FixedParticles( 0x375A, 10, 15, 5017, EffectLayer.Waist );
					from.PlaySound( 0x1EE );
            		from.SendMessage( "You have been unbound from the lifestone." );	
				}				
			}
	  	}	
	}
}
