
using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public class PersonalGardenAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new PersonalGardenAddonDeed(); } }

		#region Constructors
		[Constructable]
		public PersonalGardenAddon( GardenGroundType type, int width, int height ) : this( (int)type, width, height ){}

		public PersonalGardenAddon( int type, int width, int height )
		{
			GardenGroundInfo info = GardenGroundInfo.GetInfo( type );

			AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.Top ).ItemID ), 0, 0, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.Right ).ItemID ), width, 0, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.Left ).ItemID ), 0, height, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.Bottom ).ItemID ), width, height, 0 );

			int w = width - 1;
			int h = height - 1;

			for ( int y = 1; y <= h; ++y )
				AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.West ).ItemID ), 0, y, 0 );

			for ( int x = 1; x <= w; ++x )
				AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.North ).ItemID ), x, 0, 0 );

			for ( int y = 1; y <= h; ++y )
				AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.East ).ItemID ), width, y, 0 );

			for ( int x = 1; x <= w; ++x )
				AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.South ).ItemID ), x, height, 0 );

			for ( int x = 1; x <= w; ++x )
				for ( int y = 1; y <= h; ++y )
					AddComponent( new AddonComponent( info.GetItemPart( GardenGroundPosition.Center ).ItemID ), x, y, 0 );
		}

		public PersonalGardenAddon( Serial serial ) : base( serial ) { }
		#endregion

		public override void OnDoubleClick( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsCoOwner( from ) )
			{
				if ( from.InRange( GetWorldLocation(), 3 ) )
				{
                    from.SendGump(new ConfirmGardenRemovalGump( this ));
				}
				else
				{
					from.SendLocalizedMessage( 500295 );
				}
			}
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public enum GardenGroundType
	{
		FurrowNoBorder,
		FurrowBorder,
		PlainNoBorder,
		PlainBorder
	}

	public enum GardenGroundPosition
	{
		Top,
		Bottom,
		Left,
		Right,
		West,
		North,
		East,
		South,
		Center
	}

	public class GardenGroundInfo
	{
		private GardenItemPart[] m_Entries;

		public GardenItemPart[] Entries{ get{ return m_Entries; } }

		public GardenGroundInfo( GardenItemPart[] entries )
		{
			m_Entries = entries;
		}

		public GardenItemPart GetItemPart( GardenGroundPosition pos )
		{
			int i = (int)pos;

			if ( i < 0 || i >= m_Entries.Length )
				i = 0;

			return m_Entries[i];
		}

		public static GardenGroundInfo GetInfo( int type )
		{
			if ( type < 0 || type >= m_Infos.Length )
				type = 0;

			return m_Infos[type];
		}

		#region GardenGroundInfo definitions
		private static GardenGroundInfo[] m_Infos = new GardenGroundInfo[] {
		new GardenGroundInfo( new GardenItemPart[] {
						new GardenItemPart( 0x32C9, GardenGroundPosition.Top, -1, -1 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.Bottom, -1, -1 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.Left, -1, -1 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.Right, -1, -1 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.West, -1, -1 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.North, -1, -1 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.East, -1, -1 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.South, -1, -1 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.Center, 44, 24 )
					}),
		new GardenGroundInfo( new GardenItemPart[] {
						new GardenItemPart( 0x1B30, GardenGroundPosition.Top, 44, 7 ),
						new GardenItemPart( 0x1B2F, GardenGroundPosition.Bottom, 44, 68 ),
						new GardenItemPart( 0x1B31, GardenGroundPosition.Left, 0, 35 ),
						new GardenItemPart( 0x1B32, GardenGroundPosition.Right, 88, 32 ),
						new GardenItemPart( 0x1B29, GardenGroundPosition.West, 22, 12 ),
						new GardenItemPart( 0x1B2A, GardenGroundPosition.North, 66, 12 ),
						new GardenItemPart( 0x1B28, GardenGroundPosition.East, 66, 46 ),
						new GardenItemPart( 0x1B27, GardenGroundPosition.South, 22, 46 ),
						new GardenItemPart( 0x32C9, GardenGroundPosition.Center, 44, 24 )
					}),
		new GardenGroundInfo( new GardenItemPart[] {
						new GardenItemPart( 0x31F4, GardenGroundPosition.Top, -1, -1 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.Bottom, -1, -1 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.Left, -1, -1 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.Right, -1, -1 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.West, -1, -1 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.North, -1, -1 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.East, -1, -1 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.South, -1, -1 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.Center, 44, 24 )
					}),
		new GardenGroundInfo( new GardenItemPart[] {
						new GardenItemPart( 0x1B30, GardenGroundPosition.Top, 44, 7 ),
						new GardenItemPart( 0x1B2F, GardenGroundPosition.Bottom, 44, 68 ),
						new GardenItemPart( 0x1B31, GardenGroundPosition.Left, 0, 35 ),
						new GardenItemPart( 0x1B32, GardenGroundPosition.Right, 88, 32 ),
						new GardenItemPart( 0x1B29, GardenGroundPosition.West, 22, 11 ),
						new GardenItemPart( 0x1B2A, GardenGroundPosition.North, 66, 12 ),
						new GardenItemPart( 0x1B28, GardenGroundPosition.East, 66, 46 ),
						new GardenItemPart( 0x1B27, GardenGroundPosition.South, 22, 46 ),
						new GardenItemPart( 0x31F4, GardenGroundPosition.Center, 44, 24 )
					})
			};
			#endregion

		public static GardenGroundInfo[] Infos{ get{ return m_Infos; } }
	}

	public class GardenItemPart
	{
		private int m_ItemID;
		private  GardenGroundPosition m_Info;
		private int m_OffsetX;
		private int m_OffsetY;

		public int ItemID
		{
			get{ return m_ItemID; }
		}

		public  GardenGroundPosition GardenGroundPosition
		{
			get{ return m_Info; }
		}

		public int OffsetX
		{
			get{ return m_OffsetX; }
		}

		public int OffsetY
		{
			get{ return m_OffsetY; }
		}

		public GardenItemPart( int itemID,  GardenGroundPosition info, int offsetX, int offsetY )
		{
			m_ItemID = itemID;
			m_Info = info;
			m_OffsetX = offsetX;
			m_OffsetY = offsetY;
		}
	}

    public class ConfirmGardenRemovalGump : Gump
    {
        private PersonalGardenAddon m_PGAddon;

        public ConfirmGardenRemovalGump(PersonalGardenAddon pgaddon)
            : base(50, 50)
        {
            m_PGAddon = pgaddon;

            AddBackground(0, 0, 450, 260, 9270);

            AddAlphaRegion(12, 12, 426, 22);
            AddTextEntry(13, 13, 379, 20, 32, 0, @"Warning!");

            AddAlphaRegion(12, 39, 426, 209);

            AddHtml(15, 50, 420, 185, "<BODY>" +
"<BASEFONT COLOR=YELLOW>You are about to remove your Personal Garden!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Upon removal of this garden, a replacement Personal Garden deed " +
"<BASEFONT COLOR=YELLOW>will be placed in your backpack.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Are you sure you want to remove this addon?<BR><BR>" +
                             "</BODY>", false, false);

            AddButton(13, 220, 0xFA5, 0xFA6, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(47, 222, 150, 20, 1052072, 0x7FFF, false, false);

            AddButton(350, 220, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(385, 222, 100, 20, 1060051, 0x7FFF, false, false);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 0 )
                return;

            Mobile from = sender.Mobile;

            from.AddToBackpack(new PersonalGardenAddonDeed());
            m_PGAddon.Delete();

            from.SendMessage( "Personal Garden deleted." );
        }
    }
}
