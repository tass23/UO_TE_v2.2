
using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public class VinyardGroundAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new VinyardGroundAddonDeed(); } }

		#region Constructors
		[Constructable]
		public VinyardGroundAddon( VinyardGroundType type, int width, int height ) : this( (int)type, width, height ){}

		public VinyardGroundAddon( int type, int width, int height )
		{
			VinyardGroundInfo info = VinyardGroundInfo.GetInfo( type );

			AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.Top ).ItemID ), 0, 0, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.Right ).ItemID ), width, 0, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.Left ).ItemID ), 0, height, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.Bottom ).ItemID ), width, height, 0 );

			int w = width - 1;
			int h = height - 1;

			for ( int y = 1; y <= h; ++y )
				AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.West ).ItemID ), 0, y, 0 );

			for ( int x = 1; x <= w; ++x )
				AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.North ).ItemID ), x, 0, 0 );

			for ( int y = 1; y <= h; ++y )
				AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.East ).ItemID ), width, y, 0 );

			for ( int x = 1; x <= w; ++x )
				AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.South ).ItemID ), x, height, 0 );

			for ( int x = 1; x <= w; ++x )
				for ( int y = 1; y <= h; ++y )
					AddComponent( new AddonComponent( info.GetItemPart( GroundPosition.Center ).ItemID ), x, y, 0 );
		}

		public VinyardGroundAddon( Serial serial ) : base( serial ) { }
		#endregion

		public override void OnDoubleClick( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsCoOwner( from ) )
			{
				if ( from.InRange( GetWorldLocation(), 3 ) )
				{
                    from.SendGump(new ConfirmRemovalGump( this ));
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

	public enum VinyardGroundType
	{
		FurrowNoBorder,
		FurrowBorder,
		PlainNoBorder,
		PlainBorder
	}

	public enum GroundPosition
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

	public class VinyardGroundInfo
	{
		private GroundItemPart[] m_Entries;

		public GroundItemPart[] Entries{ get{ return m_Entries; } }

		public VinyardGroundInfo( GroundItemPart[] entries )
		{
			m_Entries = entries;
		}

		public GroundItemPart GetItemPart( GroundPosition pos )
		{
			int i = (int)pos;

			if ( i < 0 || i >= m_Entries.Length )
				i = 0;

			return m_Entries[i];
		}

		public static VinyardGroundInfo GetInfo( int type )
		{
			if ( type < 0 || type >= m_Infos.Length )
				type = 0;

			return m_Infos[type];
		}

		#region VinyardGroundInfo definitions
		private static VinyardGroundInfo[] m_Infos = new VinyardGroundInfo[] {
		new VinyardGroundInfo( new GroundItemPart[] {
						new GroundItemPart( 0x32C9, GroundPosition.Top, -1, -1 ),
						new GroundItemPart( 0x32C9, GroundPosition.Bottom, -1, -1 ),
						new GroundItemPart( 0x32C9, GroundPosition.Left, -1, -1 ),
						new GroundItemPart( 0x32C9, GroundPosition.Right, -1, -1 ),
						new GroundItemPart( 0x32C9, GroundPosition.West, -1, -1 ),
						new GroundItemPart( 0x32C9, GroundPosition.North, -1, -1 ),
						new GroundItemPart( 0x32C9, GroundPosition.East, -1, -1 ),
						new GroundItemPart( 0x32C9, GroundPosition.South, -1, -1 ),
						new GroundItemPart( 0x32C9, GroundPosition.Center, 44, 24 )
					}),
		new VinyardGroundInfo( new GroundItemPart[] {
						new GroundItemPart( 0x1B30, GroundPosition.Top, 44, 7 ),
						new GroundItemPart( 0x1B2F, GroundPosition.Bottom, 44, 68 ),
						new GroundItemPart( 0x1B31, GroundPosition.Left, 0, 35 ),
						new GroundItemPart( 0x1B32, GroundPosition.Right, 88, 32 ),
						new GroundItemPart( 0x1B29, GroundPosition.West, 22, 12 ),
						new GroundItemPart( 0x1B2A, GroundPosition.North, 66, 12 ),
						new GroundItemPart( 0x1B28, GroundPosition.East, 66, 46 ),
						new GroundItemPart( 0x1B27, GroundPosition.South, 22, 46 ),
						new GroundItemPart( 0x32C9, GroundPosition.Center, 44, 24 )
					}),
		new VinyardGroundInfo( new GroundItemPart[] {
						new GroundItemPart( 0x31F4, GroundPosition.Top, -1, -1 ),
						new GroundItemPart( 0x31F4, GroundPosition.Bottom, -1, -1 ),
						new GroundItemPart( 0x31F4, GroundPosition.Left, -1, -1 ),
						new GroundItemPart( 0x31F4, GroundPosition.Right, -1, -1 ),
						new GroundItemPart( 0x31F4, GroundPosition.West, -1, -1 ),
						new GroundItemPart( 0x31F4, GroundPosition.North, -1, -1 ),
						new GroundItemPart( 0x31F4, GroundPosition.East, -1, -1 ),
						new GroundItemPart( 0x31F4, GroundPosition.South, -1, -1 ),
						new GroundItemPart( 0x31F4, GroundPosition.Center, 44, 24 )
					}),
		new VinyardGroundInfo( new GroundItemPart[] {
						new GroundItemPart( 0x1B30, GroundPosition.Top, 44, 7 ),
						new GroundItemPart( 0x1B2F, GroundPosition.Bottom, 44, 68 ),
						new GroundItemPart( 0x1B31, GroundPosition.Left, 0, 35 ),
						new GroundItemPart( 0x1B32, GroundPosition.Right, 88, 32 ),
						new GroundItemPart( 0x1B29, GroundPosition.West, 22, 11 ),
						new GroundItemPart( 0x1B2A, GroundPosition.North, 66, 12 ),
						new GroundItemPart( 0x1B28, GroundPosition.East, 66, 46 ),
						new GroundItemPart( 0x1B27, GroundPosition.South, 22, 46 ),
						new GroundItemPart( 0x31F4, GroundPosition.Center, 44, 24 )
					})
			};
			#endregion

		public static VinyardGroundInfo[] Infos{ get{ return m_Infos; } }
	}

	public class GroundItemPart
	{
		private int m_ItemID;
		private  GroundPosition m_Info;
		private int m_OffsetX;
		private int m_OffsetY;

		public int ItemID
		{
			get{ return m_ItemID; }
		}

		public  GroundPosition GroundPosition
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

		public GroundItemPart( int itemID,  GroundPosition info, int offsetX, int offsetY )
		{
			m_ItemID = itemID;
			m_Info = info;
			m_OffsetX = offsetX;
			m_OffsetY = offsetY;
		}
	}

    public class ConfirmRemovalGump : Gump
    {
        private VinyardGroundAddon m_VGAddon;

        public ConfirmRemovalGump(VinyardGroundAddon vgaddon)
            : base(50, 50)
        {
            m_VGAddon = vgaddon;

            AddBackground(0, 0, 450, 260, 9270);

            AddAlphaRegion(12, 12, 426, 22);
            AddTextEntry(13, 13, 379, 20, 32, 0, @"Warning!");

            AddAlphaRegion(12, 39, 426, 209);

            AddHtml(15, 50, 420, 185, "<BODY>" +
"<BASEFONT COLOR=YELLOW>You are about to remove your vineyard ground addon!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Before removing, be sure to use your grapevine placement tool "+
"<BASEFONT COLOR=YELLOW>to delete any grapevines that you have placed.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Upon removal of this addon, a replacement vineyard ground addon deed " +
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

            from.AddToBackpack(new VinyardGroundAddonDeed());
            m_VGAddon.Delete();

            from.SendMessage( "Vineyard ground addon deleted" );
        }
    }
}
