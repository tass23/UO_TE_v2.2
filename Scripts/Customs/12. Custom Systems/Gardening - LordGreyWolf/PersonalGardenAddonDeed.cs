
using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Multis;

namespace Server.Items
{
	public class PersonalGardenAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return null; } }

		[Constructable]
		public PersonalGardenAddonDeed()
		{
			Name = "Personal Garden Deed";
		}

		public PersonalGardenAddonDeed( Serial serial ) : base( serial ) { }

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				BoundingBoxPicker.Begin( from, new BoundingBoxCallback( BoundingBox_Callback ), null );
			else
				from.SendLocalizedMessage( 1042001 );
		}

		private void BoundingBox_Callback( Mobile from, Map map, Point3D start, Point3D end, object state )
		{
			IPoint3D p = start as IPoint3D;

			if ( p == null || map == null )
				return;

			int width = (end.X - start.X), height = (end.Y - start.Y);

			if ( width < 2 || height < 2 )
				from.SendMessage( "The bounding targets must be at least a 3x3 box." );
			else if ( IsChildOf( from.Backpack ) )
				from.SendGump( new PersonalGardenGump( this, p, map, width, height ) );
			else
				from.SendLocalizedMessage( 1042001 );
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}

namespace Server.Gumps
{
	public class PersonalGardenGump : Gump
	{
		private const int EntryCount = 3;

		private BaseAddonDeed m_Deed;
		private IPoint3D m_P3D;
		private Map m_Map;
		private int m_Width;
		private int m_Height;

		public PersonalGardenGump( BaseAddonDeed deed, IPoint3D p, Map map, int width, int height ) : base( 30, 30 )
		{
			m_Deed = deed;
			m_P3D = p;
			m_Map = map;
			m_Width = width;
			m_Height = height;

			AddPage( 0 );

			AddBackground( 0, 0, 450, 160, 9250 );

			AddAlphaRegion( 12, 12, 381, 22 );
			AddHtml( 13, 13, 379, 20, "<BASEFONT COLOR=WHITE>Choose a ground type</BASEFONT>", false, false );

			AddAlphaRegion( 398, 12, 40, 22 );
			AddAlphaRegion( 12, 39, 426, 109 );

			AddImage( 400, 16, 9766 );
			AddImage( 420, 16, 9762 );
			AddPage( 1 );

			int page = 1;

			for ( int i = 0, index = 0; i < GardenGroundInfo.Infos.Length; ++i, ++index )
			{
				if ( index >= EntryCount )
				{
					if ( (EntryCount * page) == EntryCount )
						AddImage( 400, 16, 0x2626 );

					AddButton( 420, 16, 0x15E1, 0x15E5, 0, GumpButtonType.Page, page + 1 );

					++page;
					index = 0;

					AddPage( page );

					AddButton( 400, 16, 0x15E3, 0x15E7, 0, GumpButtonType.Page, page - 1 );

					if ( (GardenGroundInfo.Infos.Length - (EntryCount * page)) < EntryCount )
						AddImage( 420, 16, 0x2622 );
				}

				GardenGroundInfo info = GardenGroundInfo.GetInfo( i );

				for ( int j = 0; j < info.Entries.Length; ++j )
				{
					if (info.Entries[j].OffsetX >= 0 && info.Entries[j].OffsetY >= 0 )
						AddItem( 20 + (index * 140 ) + info.Entries[j].OffsetX, 46 + info.Entries[j].OffsetY, info.Entries[j].ItemID );
				}

				AddButton( 20 + (index * 140 ), 46, 1209, 1210, i+1, GumpButtonType.Reply, 0);
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from  = sender.Mobile;

			if ( info.ButtonID >= 1 )
			{
				BaseAddon addon = new PersonalGardenAddon( info.ButtonID-1, m_Width, m_Height );

				Server.Spells.SpellHelper.GetSurfaceTop( ref m_P3D );

				BaseHouse house = null;

				AddonFitResult res = addon.CouldFit( m_P3D, m_Map, from, ref house );

				if ( res == AddonFitResult.Valid )
					addon.MoveToWorld( new Point3D( m_P3D ), m_Map );
				else if ( res == AddonFitResult.Blocked )
					from.SendLocalizedMessage( 500269 );
				else if ( res == AddonFitResult.NotInHouse )
					from.SendLocalizedMessage( 500274 );
				/*else if ( res == AddonFitResult.DoorsNotClosed )
					from.SendMessage( "You must close all house doors before placing this." );
				*/
				if ( res == AddonFitResult.Valid )
				{
					m_Deed.Delete();

					house.Addons.Add( addon );
				}
				else
				{
					addon.Delete();
				}
			}
		}
	}

	public class PersonalGardenPlacedGump : Gump
	{
		private BaseAddonDeed m_Deed;

		public PersonalGardenPlacedGump( BaseAddonDeed deed ) : base( 30, 30 )
		{
			m_Deed = deed;

			AddPage( 0 );

			AddBackground( 0, 0, 450, 250, 9250 );

			AddAlphaRegion( 12, 12, 426, 22 );
			AddHtml( 13, 13, 379, 20, "<BASEFONT COLOR=WHITE>Personal Garden Placement Successful</BASEFONT>", false, false );

			AddAlphaRegion( 12, 39, 426, 199 );

			AddHtml( 15, 50, 420, 185, "<BODY>" +
			"<BASEFONT COLOR=YELLOW>Your Personal Garden has been successfully placed!<BR>" +
			"<BASEFONT COLOR=YELLOW>You may now begin placing seeds in your garden using a " +
			"<BASEFONT COLOR=YELLOW>Boline tool.<BR><BR>" +
			"<BASEFONT COLOR=YELLOW>You may delete this personal garden at any time.  " +
			"<BASEFONT COLOR=YELLOW>To do so... <BR>" +
			"<BASEFONT COLOR=YELLOW>   1. Double click the garden and accept prompt to delete.<BR>" +
			"<BASEFONT COLOR=YELLOW>   *Note* You must be within 3 tiles of western corner to delete addon.<BR><BR>" +
			"</BODY>", false, false );

			AddButton( 190, 210, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 );

		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			switch ( info.ButtonID )
			{
				case 0:
				{
					from.SendMessage( "Enjoy your new personal garden." );
					break;
				}

			}
		}
	}
}
