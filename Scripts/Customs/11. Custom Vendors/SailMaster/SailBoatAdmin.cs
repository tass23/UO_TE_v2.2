using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class SailBoatAdminGump : Server.Gumps.Gump
	{
		public Mobile m_Mobile;
		public SailBoatAdminGump(Mobile m) : base ( 20, 30 )
		{

			m_Mobile = m;
			AddPage ( 0 );
			AddBackground( 0, 0, 560, 133, 5054 );

			AddImageTiled( 10, 10, 540, 23, 0x52 );
			AddImageTiled( 12, 12, 538, 21, 0xBBC );

			AddLabel( 220, 11, 0, "Sailboat Admin Panel." );

			AddLabel( 115, 35, 0, "Make stuff." );
			AddLabel( 375, 35, 0, "Delete stuff." );

			AddButton( 11, 60, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1 );
			AddLabel( 30, 58, 0, "Generate Sailboats and Nujel'm Stairs" );
			AddButton( 11, 82, 0x15E3, 0x15E7, 2, GumpButtonType.Reply, 1 );
			AddLabel( 30, 80, 0, "Generate Sailmasters" );
			AddButton( 11, 104, 0x15E3, 0x15E7, 3, GumpButtonType.Reply, 1 );
			AddLabel( 30, 102, 0, "Only Generate Sailing Boat" );

			AddButton( 271, 60, 0x15E3, 0x15E7, 4, GumpButtonType.Reply, 1 );
			AddLabel( 300, 58, 0, "Delete all SailBoats and Nujel'm Stairs" );
			AddButton( 271, 82, 0x15E3, 0x15E7, 5, GumpButtonType.Reply, 1 );
			AddLabel( 300, 80, 0, "Delete all Sailmasters" );
			AddButton( 271, 104, 0x15E3, 0x15E7, 6, GumpButtonType.Reply, 1 );
			AddLabel( 300, 102, 0, "Delete all Sailboat Membershipcards" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			switch ( info.ButtonID )
			{
				case 1: 	{  	//// Generate Sailboats
						Item gen_boats;
						Point3D gen_where;
						///Moonglow
						gen_where = new Point3D(4412,1034,-5);
						gen_boats = new SailBoatNorth2();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Britain
						gen_where = new Point3D(1464,1766,-5);
						gen_boats = new SailBoatSouth1();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///J'helom
						gen_where = new Point3D(1504,3703,-5);
						gen_boats = new SailBoatEast2();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Magincia
						gen_where = new Point3D(3663,2298,-5);
						gen_boats = new SailBoatSouth1();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Nujel'm
						gen_where = new Point3D(3812,1279,-5);
						gen_boats = new SailBoatSouth2();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Nujel'm Stairs
						gen_where = new Point3D(3808,1279,-5);
						gen_boats = new NujelmStairs();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Haven
						//gen_where = new Point3D(3667,2679,-5);
						//gen_boats = new SailBoatSouth2();
						//gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Skara Brae
						gen_where = new Point3D(654,2245,-5);
						gen_boats = new SailBoatSouth1();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Trinsic
						gen_where = new Point3D(2090,2855,-5);
						gen_boats = new SailBoatSouth2();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Vesper
						gen_where = new Point3D(3023,837,-5);
						gen_boats = new SailBoatSouth1();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Papua
						gen_where = new Point3D(5832,3250,-1);
						gen_boats = new SailBoatEast2();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						///Vendor Mall
						gen_where = new Point3D(4576,2400,-2);
						gen_boats = new SailBoatNorth2();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);
						
						///Sailing Boat						
						gen_where = new Point3D(4419,830,-2);
						gen_boats = new SailBoatSailing();
						gen_boats.MoveToWorld ( gen_where, m_Mobile.Map);						

						break; 
					}
				case 2:		{  	//// Generate SailMasters
						Mobile gen_smasters;
						Point3D gen_where;
						///Moonglow
						gen_where = new Point3D(4412,1036,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Britain
						gen_where = new Point3D(1464,1764,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///J'helom
						gen_where = new Point3D(1502,3703,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Magincia
						gen_where = new Point3D(3663,2296,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Nujel'm
						gen_where = new Point3D(3812,1277,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Haven
						//gen_where = new Point3D(3667,2677,-2);
						//gen_smasters = new SailMaster();
						//gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Skara Brae
						gen_where = new Point3D(654,2243,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Trinsic
						gen_where = new Point3D(2090,2853,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Vesper
						gen_where = new Point3D(3023,835,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Papua
						gen_where = new Point3D(5830,3250,2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);
						///Vendor Mall
						gen_where = new Point3D(4576,2402,-2);
						gen_smasters = new SailMaster();
						gen_smasters.MoveToWorld ( gen_where, m_Mobile.Map);						

						break;
					}

				case 3:		{  	//// Generate SailingSailBoat
						Item gen_sbsailing;
						Point3D gen_where;
						gen_where = new Point3D(4419,830,-2);
						gen_sbsailing = new SailBoatSailing();
						gen_sbsailing.MoveToWorld ( gen_where, m_Mobile.Map);						

						break;
					}
				case 4:	{
						m_Mobile.SendMessage( 0x35, "Removing all SailBoats and Nujel'm Stairs..." );
						ArrayList toDelete = new ArrayList();
						int sailboat_count = 0;
						foreach ( Item item in World.Items.Values )
						{
							if ( item is SailBoatSouth1 )
							{
								SailBoatSouth1 removethis = (SailBoatSouth1)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is SailBoatSouth2 )
							{
								SailBoatSouth2 removethis = (SailBoatSouth2)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is SailBoatWest1 )
							{
								SailBoatWest1 removethis = (SailBoatWest1)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is SailBoatWest2 )
							{
								SailBoatWest2 removethis = (SailBoatWest2)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is SailBoatEast1 )
							{
								SailBoatEast1 removethis = (SailBoatEast1)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is SailBoatEast2 )
							{
								SailBoatEast2 removethis = (SailBoatEast2)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is SailBoatNorth1 )
							{
								SailBoatNorth1 removethis = (SailBoatNorth1)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is SailBoatNorth2 )
							{
								SailBoatNorth2 removethis = (SailBoatNorth2)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is SailBoatSailing )
							{
								SailBoatSailing removethis = (SailBoatSailing)item;
								toDelete.Add( item );
								sailboat_count++;
							}
							if ( item is NujelmStairs )
							{
								NujelmStairs removethis = (NujelmStairs)item;
								toDelete.Add( item );
							}
						}
						for ( int i = 0; i < toDelete.Count; ++i )
						{
							if ( toDelete[i] is Item )
								((Item)toDelete[i]).Delete();									
						}
							m_Mobile.SendMessage( 0x35, "Removed {0} SailBoats and Nujel'm Stairs", sailboat_count );
						break;
					}
				case 5:	{
						m_Mobile.SendMessage( 0x35, "Removing all SailMasters..." );
						ArrayList toDelete = new ArrayList();
						int sailmaster_count = 0;
						foreach ( Mobile Mobile in World.Mobiles.Values )
						{
							if ( Mobile is SailMaster )
							{
								SailMaster removethis = (SailMaster)Mobile;
								toDelete.Add( Mobile );
								sailmaster_count++;
							}
						}
						for ( int i = 0; i < toDelete.Count; ++i )
						{
							if ( toDelete[i] is Mobile )
								((Mobile)toDelete[i]).Delete();									
						}
							m_Mobile.SendMessage( 0x35, "Removed {0} SailMasters", sailmaster_count );
						break;
					}
				case 6:	{
						m_Mobile.SendMessage( 0x35, "Removing all SailboatMembershipcards..." );
						ArrayList toDelete = new ArrayList();
						int cards_count = 0;
						foreach ( Item item in World.Items.Values )
						{
							if ( item is SailboatMembershipcard )
							{
								SailboatMembershipcard removethis = (SailboatMembershipcard)item;
								toDelete.Add( item );
								cards_count++;
							}
						}
						for ( int i = 0; i < toDelete.Count; ++i )
						{
							if ( toDelete[i] is Item )
								((Item)toDelete[i]).Delete();									
						}
							m_Mobile.SendMessage( 0x35, "Removed {0} SailboatMembershipcard", cards_count );
						break;
					}
			}
		}

	}
	public class SailBoatAdmin
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SailBoatAdmin", AccessLevel.GameMaster, new CommandEventHandler( SailBoatAdmin_OnCommand ) );
		}
		[Usage( "SailBoatAdmin" )]
		[Description( "Opens the SailBoat and Sailmaster creation Gump." )]
		private static void SailBoatAdmin_OnCommand (CommandEventArgs from)
		{
			from.Mobile.CloseGump( typeof( SailBoatAdminGump ) );
			from.Mobile.SendGump( new SailBoatAdminGump(from.Mobile) );
		}
	}
}