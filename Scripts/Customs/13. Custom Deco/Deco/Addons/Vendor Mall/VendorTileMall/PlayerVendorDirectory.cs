using System;
using System.Collections;
using System.Net;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Menus; 
using Server.Menus.Questions; 
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Prompts;
using Server.Regions;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
	public class PlayerVendorDirectory : Item
	{
		[Constructable]
		public PlayerVendorDirectory() : base( 0x1E5F )
		{
			Weight = 1.0;
			Name = "The Player Vendor Directory";
			Movable = false;
			Hue = 00;
		}

		public PlayerVendorDirectory( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			 ArrayList list = new ArrayList();

            foreach ( Mobile mob in World.Mobiles.Values )
            {
                if ( mob is PlayerVendor )
                {
                    PlayerVendor pv = mob as PlayerVendor;
                    list.Add( pv );                     
                }
            }
            from.SendGump( new FindPlayersVendorsGump ( from, list, 1 ) );
		}
	}
	
	public class FindPlayersVendorsGump  : Gump
    {
		bool hideonteleport = false;        // hide players on teleport?
		bool checkvalidtravel = true;        // check for murderer/combat/criminal?
		bool allowcriminaltravel = false;    // allow/disallow criminals to travel? dependant upon checkvalidtravel
		bool allowcombattravel = false;        // allow/disallow players in combat to travel? dependant upon checkvalidtravel
		bool allowsigiltravel = false;        // allow players with the sigil in their pack to use the book?
		bool checkred = false;            // ignore murderer status for travel?
		bool preventoverweighttravel = true;    // prevent overweight players from using the book?
		public static TimeSpan CombatHeatDelay = TimeSpan.FromSeconds( 30.0 );    // combat delay
		public DateTime gumpOpened = DateTime.MinValue;
		public TimeSpan Vendordelay = TimeSpan.FromMinutes( 1 );

        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;

        private ArrayList m_List;
        private int m_DefaultIndex;
        private int m_Page;
        private Mobile m_From;

        public void AddBlackAlpha( int x, int y, int width, int height )
        {
            AddImageTiled( x, y, width, height, 2624 );
            AddAlphaRegion( x, y, width, height );
        }

        public FindPlayersVendorsGump ( Mobile from, ArrayList list, int page ) : base( 50, 40 )
        {
            from.CloseGump( typeof( FindPlayersVendorsGump  ) );
            int pvs = 0;
            m_Page = page;
            m_From = from;
            int pageCount = 0;
            m_List = list;

            AddPage( 0 );
            AddBackground( 0, 0, 645, 325, 3500 );
            AddBlackAlpha( 20, 20, 604, 277 );

            if ( m_List == null )
            {
                return;
            }
            else
            {
                pvs = list.Count;
                if ( list.Count % 12 == 0 )
                {
                    pageCount = (list.Count / 12);
                }
                else
                {
                    pageCount = (list.Count / 12) + 1;
                }
            }

            AddLabelCropped( 32, 20, 100, 20, 1152, "Shop Name" );
            AddLabelCropped( 202, 20, 120, 20, 1152, "Owner" );
            AddLabelCropped( 415, 20, 120, 20, 1152, "Location" );
            AddLabel( 27, 298, 32, String.Format( "Player-Run Vendors                   There are {0} vendors in the world.", pvs ));

            if ( page > 1 )
                AddButton( 573, 22, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0 );
            else
                AddImage( 573, 22, 0x25EA );

            if ( pageCount > page )
                AddButton( 590, 22, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0 );
            else
                AddImage( 590, 22, 0x25E6 );

            if ( m_List.Count == 0 )
                AddLabel( 180, 115, 1152, ".....::: There are no Vendors in world :::....." );

            if ( page == pageCount )
            {
                for ( int i = (page * 12) -12; i < pvs; ++i )
                    AddDetails( i );
            }
            else
            {
                for ( int i = (page * 12) -12; i < page * 12; ++ i )
                    AddDetails( i );
            }
        }

        private void AddDetails( int index )
        {    
            try
			{
                if ( index < m_List.Count )
                {    
                    int btn;
                    int row;
                    gumpOpened = DateTime.Now;
                    btn = (index) + 101;
                    row = index % 12;
                    PlayerVendor pv = m_List[index] as PlayerVendor;
                    Account a = pv.Owner.Account as Account;

                    AddLabel(32, 46 +(row * 20), 1152, String.Format( "{0}", pv.ShopName ));
                    AddLabel(202, 46 +(row * 20), 1152, String.Format( "{0}", pv.Owner.Name ));
                    AddLabel(415, 46 +(row * 20), 1152, String.Format( "{0}", pv.Map));
                    AddButton( 585, 51 +(row * 20), 2437, 2438, btn, GumpButtonType.Reply, 0 );
                    if ( pv == null )
                    {
                        Console.WriteLine("No Vendors In Shard...");
                        return;
                    }

                }
            }
            catch {}
        }
        public override void OnResponse( NetState state, RelayInfo info )
        {
            Mobile from = state.Mobile;

            int buttonID = info.ButtonID;
            if ( buttonID == 2 )
            {
                m_Page ++;
                from.CloseGump( typeof( FindPlayersVendorsGump  ) );
                from.SendGump( new FindPlayersVendorsGump ( from, m_List, m_Page ) );
            }
            if ( buttonID == 1 )
            {
                m_Page --;
                from.CloseGump( typeof( FindPlayersVendorsGump  ) );
                from.SendGump( new FindPlayersVendorsGump ( from, m_List, m_Page ) );
            }
            if ( buttonID > 100 )
            {                                  
				if ( DateTime.Now > gumpOpened + Vendordelay )
				{
					from.SendMessage("You can not use this now.");
					return;
				} 

				if ( !allowsigiltravel && Server.Factions.Sigil.ExistsOn( from ) )
				{
					from.SendMessage( "You are carrying a sigil and may not travel by this method!" );
					return;
				}

				if ( from.AccessLevel == AccessLevel.Player && checkvalidtravel )
				{
					if ( !allowcriminaltravel && from.Criminal )
					{
						from.SendMessage( "You are criminal and may not travel." );
						return;
					}
					else if ( !allowcombattravel && Server.Spells.SpellHelper.CheckCombat( from ) || from.Combatant != null )
					{
						from.SendMessage( "You may not flee from combat!" );
						return;
					}
				}

				if ( preventoverweighttravel && from.AccessLevel == AccessLevel.Player )
				{
					if ( ( Mobile.BodyWeight + from.TotalWeight ) > ( 40 + (3.5 * from.Str) + 4 ) )
					{
						from.SendMessage( "You may not travel when carrying too much!" );
						return;
					}
				}

                int index = buttonID - 101;
                PlayerVendor pv = m_List[index] as PlayerVendor;
                Point3D xyz = pv.Location;
                int x = xyz.X;
                int y = xyz.Y;
                int z = xyz.Z;

                Point3D dest = new Point3D( x, y, z );
                from.MoveToWorld( dest, pv.Map );
                from.SendGump( new FindPlayersVendorsGump ( from, m_List, m_Page ) );
            }
        }
    }
}