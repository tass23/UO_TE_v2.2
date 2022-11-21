using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.BulkOrders
{
	public class SmallBODGump : Gump
	{
		private SmallBOD m_Deed;
		private Mobile m_From;

		public SmallBODGump( Mobile from, SmallBOD deed ) : base( 25, 25 )
		{
			m_From = from;
			m_Deed = deed;

			m_From.CloseGump( typeof( LargeBODGump ) );
			m_From.CloseGump( typeof( SmallBODGump ) );

			AddPage( 0 );

			AddBackground( 50, 10, 455, 260, 5054 );
			AddImageTiled( 58, 20, 438, 241, 2624 );
			AddAlphaRegion( 58, 20, 438, 241 );

			AddImage( 45, 5, 10460 );
			AddImage( 480, 5, 10460 );
			AddImage( 45, 245, 10460 );
			AddImage( 480, 245, 10460 );

			AddHtmlLocalized( 225, 25, 120, 20, 1045133, 0x7FFF, false, false ); // A bulk order

			AddHtmlLocalized( 75, 48, 250, 20, 1045138, 0x7FFF, false, false ); // Amount to make:
			AddLabel( 275, 48, 1152, deed.AmountMax.ToString() );

			AddHtmlLocalized( 275, 76, 200, 20, 1045153, 0x7FFF, false, false ); // Amount finished:
			AddHtmlLocalized( 75, 72, 120, 20, 1045136, 0x7FFF, false, false ); // Item requested:

			AddItem( 410, 72, deed.Graphic );

			AddHtmlLocalized( 75, 96, 210, 20, deed.Number, 0x7FFF, false, false );
			AddLabel( 275, 96, 0x480, deed.AmountCur.ToString() );

			if ( deed.RequireExceptional || deed.Material != BulkMaterialType.None )
				AddHtmlLocalized( 75, 120, 200, 20, 1045140, 0x7FFF, false, false ); // Special requirements to meet:

			if ( deed.RequireExceptional )
				AddHtmlLocalized( 75, 144, 300, 20, 1045141, 0x7FFF, false, false ); // All items must be exceptional.

			if ( deed.Material != BulkMaterialType.None )
				//AddHtmlLocalized( 75, deed.RequireExceptional ? 168 : 144, 300, 20, GetMaterialNumberFor( deed.Material ), 0x7FFF, false, false ); // All items must be made with x material.
				#region Custom BODs
				AddHtml(75, deed.RequireExceptional ? 168 : 144, 400, 25, "<basefont color=#FF0000>All items must be crafted with " + LargeBODGump.GetMaterialStringFor(deed.Material), false, false);
				#endregion

			AddButton( 125, 192, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 192, 300, 20, 1045154, 0x7FFF, false, false ); // Combine this deed with the item requested.

			AddButton( 125, 216, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 216, 120, 20, 1011441, 0x7FFF, false, false ); // EXIT
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Deed.Deleted || !m_Deed.IsChildOf( m_From.Backpack ) )
				return;

			if ( info.ButtonID == 2 ) // Combine
			{
				m_From.SendGump( new SmallBODGump( m_From, m_Deed ) );
				m_Deed.BeginCombine( m_From );
			}
			/*else if (info.ButtonID >= 3)
            {
                bool IsFound = false;
                IPooledEnumerable eable = m_From.GetMobilesInRange(6);
                foreach (Mobile vendor in eable)
                {
                    switch (info.ButtonID)
                    {
                        case 3: IsFound = (vendor is Blacksmith || vendor is Weaponsmith || vendor is Armorer); break;
                        case 4: IsFound = (vendor is Tailor || vendor is Weaver); break;
                        case 5: IsFound = (vendor is Carpenter); break;
                        case 6: IsFound = (vendor is Bowyer); break;
                    }
                    if (IsFound == true)
                        break;
                }
                if (IsFound == false)
                    switch (info.ButtonID)
                    {
                        case 3: m_From.SendMessage("You must be near a Blacksmith, Weaponsmith or Armorer to claim that."); break;
                        case 4: m_From.SendMessage("You must be near a Tailor or Weaver to claim that."); break;
                        case 5: m_From.SendMessage("You must be near a Carpenter to claim that."); break;
                        case 6: m_From.SendMessage("You must be near a Bowyer to claim that."); break;
                    }
                else
                    daat99.daat99.ClaimBods(m_From, m_Deed);
            }
			*/
        }
		public static string GetMaterialStringFor(BulkMaterialType material)
        {
            string result = "UNKNOWN";
            switch ((int)material)
            {
                case 1: result = "dull copper ingots"; break;
                case 2: result = "shadow iron ingots"; break;
                case 3: result = "copper ingots"; break;
                case 4: result = "bronze ingots"; break;
                case 5: result = "gold ingots"; break;
                case 6: result = "agapite ingots"; break;
                case 7: result = "verite ingots"; break;
                case 8: result = "valorite ingots"; break;
                case 9: result = "spined leather"; break;
                case 10: result = "horned leather"; break;
                case 11: result = "barbed leather"; break;
				case 12: result = "regular wood"; break;
                case 13: result = "oak wood"; break;
                case 14: result = "ash wood"; break;
                case 15: result = "yew wood"; break;
                case 16: result = "heartwood"; break;
                case 17: result = "bloodwood"; break;
                case 18: result = "frostwood"; break;
            }
            return result;
        }
		/*
		public static int GetMaterialNumberFor( BulkMaterialType material )
		{
			if ( material >= BulkMaterialType.DullCopper && material <= BulkMaterialType.Valorite )
				return 1045142 + (int)(material - BulkMaterialType.DullCopper);
			else if ( material >= BulkMaterialType.Spined && material <= BulkMaterialType.Barbed )
				return 1049348 + (int)(material - BulkMaterialType.Spined);
			else if ( material >= BulkMaterialType.Oak && material <= BulkMaterialType.Frostwood )
				return "<basefont color=#FF0000>All items must be crafted with " + (int)(material - BulkMaterialType.Oak);

			return 0;
		}
		*/
	}
}