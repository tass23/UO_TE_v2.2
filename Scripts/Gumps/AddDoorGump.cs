using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
	public class AddDoorGump : Gump
	{
		private int m_Type;

		public AddDoorGump() : this( -1 )
		{
		}

		public void AddBlueBack( int width, int height )
		{
			AddBackground (  0,  0, width-00, height-00, 0xE10 );
			AddBackground (  8,  5, width-16, height-11, 0x053 );
			AddImageTiled ( 15, 14, width-29, height-29, 0xE14 );
			AddAlphaRegion( 15, 14, width-29, height-29 );
		}

		public AddDoorGump( int type ) : base( 50, 40 )
		{
			m_Type = type;

			AddPage( 0 );

			if ( m_Type >= 0 && m_Type < m_Types.Length )
			{
				AddBlueBack( 155, 174 );

				int baseID = m_Types[m_Type].m_BaseID;

				AddItem( 25, 24, baseID );
				AddButton( 26, 37, 0x5782, 0x5782, 1, GumpButtonType.Reply, 0 );

				AddItem( 47, 45, baseID + 2 );
				AddButton( 43, 57, 0x5783, 0x5783, 2, GumpButtonType.Reply, 0 );

				AddItem( 87, 22, baseID + 10 );
				AddButton( 116, 35, 0x5785, 0x5785, 6, GumpButtonType.Reply, 0 );

				AddItem( 65, 45, baseID + 8 );
				AddButton( 96, 55, 0x5784, 0x5784, 5, GumpButtonType.Reply, 0 );

				AddButton( 73, 36, 0x2716, 0x2716, 9, GumpButtonType.Reply, 0 );
			}
			else
			{
				AddBlueBack( 1160, 530 );

				for ( int i = 0; i < m_Types.Length; ++i )
				{
					//AddButton( 30 + (i * 49), 13, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
					//AddItem( 22 + (i * 49), 20, m_Types[i].m_BaseID );

					if (i < 23)
					{
						AddButton( 30 + (i * 49), 13, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
						AddItem( 22 + (i * 49), 20, m_Types[i].m_BaseID );
					}
					else if (i >= 23 &&  i < 46)
					{
						//AddButton( 30 + ((i-13) * 49), 150, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
						AddButton( 30 + ((i-23) * 49), 140, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
						AddItem( 22 + ((i-23) * 49), 147, m_Types[i].m_BaseID );
					}
					else if (i >= 46 && i < 69)
					{
						//AddButton( 30 + ((i-26) * 49), 170, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
						AddButton( 30 + ((i-46) * 49), 267, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
						AddItem( 22 + ((i-46) * 49), 274, m_Types[i].m_BaseID );	
					}
					else if (i >= 69 && i < 92)
					{
						//AddButton( 30 + ((i-39) * 49), 190, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
						AddButton( 30 + ((i-69) * 49), 394, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
						AddItem( 22 + ((i-69) * 49), 401, m_Types[i].m_BaseID );	
					}
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			int button = info.ButtonID - 1;

			if ( m_Type == -1 )
			{
				if ( button >= 0 && button < m_Types.Length )
					from.SendGump( new AddDoorGump( button ) );
			}
			else
			{
				if ( button >= 0 && button < 8 )
				{
					from.SendGump( new AddDoorGump( m_Type ) );
					CommandSystem.Handle( from, String.Format( "{0}Add {1} {2}", CommandSystem.Prefix, m_Types[m_Type].m_Type.Name, (DoorFacing) button ) );
				}
				else if ( button == 8 )
				{
					from.SendGump( new AddDoorGump( m_Type ) );
					CommandSystem.Handle( from, String.Format( "{0}Link", CommandSystem.Prefix ) );
				}
				else
				{
					from.SendGump( new AddDoorGump() );
				}
			}
		}

		public static void Initialize()
		{
			CommandSystem.Register( "AddDoor", AccessLevel.GameMaster, new CommandEventHandler( AddDoor_OnCommand ) );
		}

		[Usage( "AddDoor" )]
		[Description( "Displays a menu from which you can interactively add doors." )]
		public static void AddDoor_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new AddDoorGump() );
		}

		public static DoorInfo[] m_Types = new DoorInfo[]
		{
			new DoorInfo( typeof( MetalDoor ), 0x675 ),
			new DoorInfo( typeof( MetalDoor2 ), 0x6C5 ),
			new DoorInfo( typeof( BarredMetalDoor ), 0x685 ),
			new DoorInfo( typeof( BarredMetalDoor2 ), 0x1FED ),
			new DoorInfo( typeof( RattanDoor ), 0x695 ),
			new DoorInfo( typeof( DarkWoodDoor ), 0x6A5 ),
			new DoorInfo( typeof( MediumWoodDoor ), 0x6B5 ),
			new DoorInfo( typeof( LightWoodDoor ), 0x6D5 ),
			new DoorInfo( typeof( StrongWoodDoor ), 0x6E5 ),
			new DoorInfo( typeof( IronGate ), 0x824 ),
			new DoorInfo( typeof( IronGateShort ), 0x84c ),
			new DoorInfo( typeof( LightWoodGate ), 0x839 ),
			new DoorInfo( typeof( DarkWoodGate ), 0x866 ),
			/*Added by Raist*/
			new DoorInfo( typeof( TreeDoor ), 0x2BE9),
			new DoorInfo( typeof( CarvedWoodDoor ), 0x2BF9),
			new DoorInfo( typeof( ForgedSteelDoor ), 0x2C09),
			new DoorInfo( typeof( RedOrnateWoodDoor ), 0x2C7E),
			new DoorInfo( typeof( HippieWoodDoor ), 0x2C8E),
			new DoorInfo( typeof( CelestialSteelDoor ), 0x2C9E),
			new DoorInfo( typeof( WhiteDragonWoodDoor ), 0x2CAE),
			new DoorInfo( typeof( GoodLuckWoodDoor ), 0x2CBE),
			new DoorInfo( typeof( PaintedRatanDoor ), 0x2CCE),
			new DoorInfo( typeof( TigerRatanDoor ), 0x2E48),

			//Low 10, 50
			new DoorInfo( typeof( MetalDoor_Locked ), 0x675 ),
			new DoorInfo( typeof( MetalDoor2_Locked ), 0x6C5 ),
			new DoorInfo( typeof( BarredMetalDoor_Locked ), 0x685 ),
			new DoorInfo( typeof( BarredMetalDoor2_Locked ), 0x1FED ),
			new DoorInfo( typeof( RattanDoor_Locked ), 0x695 ),
			new DoorInfo( typeof( DarkWoodDoor_Locked ), 0x6A5 ),
			new DoorInfo( typeof( MediumWoodDoor_Locked ), 0x6B5 ),
			new DoorInfo( typeof( LightWoodDoor_Locked ), 0x6D5 ),
			new DoorInfo( typeof( StrongWoodDoor_Locked ), 0x6E5 ),
			new DoorInfo( typeof( IronGate_Locked ), 0x824 ),
			new DoorInfo( typeof( IronGateShort_Locked ), 0x84c ),
			new DoorInfo( typeof( LightWoodGate_Locked ), 0x839 ),
			new DoorInfo( typeof( DarkWoodGate_Locked ), 0x866 ),
			/*Added by Raist*/
			new DoorInfo( typeof( TreeDoor_Locked ), 0x2BE9),
			new DoorInfo( typeof( CarvedWoodDoor_Locked ), 0x2BF9),
			new DoorInfo( typeof( ForgedSteelDoor_Locked ), 0x2C09),
			new DoorInfo( typeof( RedOrnateWoodDoor_Locked ), 0x2C7E),
			new DoorInfo( typeof( HippieWoodDoor_Locked ), 0x2C8E),
			new DoorInfo( typeof( CelestialSteelDoor_Locked ), 0x2C9E),
			new DoorInfo( typeof( WhiteDragonWoodDoor_Locked ), 0x2CAE),
			new DoorInfo( typeof( GoodLuckWoodDoor_Locked ), 0x2CBE),
			new DoorInfo( typeof( PaintedRatanDoor_Locked ), 0x2CCE),
			new DoorInfo( typeof( TigerRatanDoor_Locked ), 0x2E48),

			//Med 25, 75
			new DoorInfo( typeof( MetalDoor_Locked_M ), 0x675 ),
			new DoorInfo( typeof( MetalDoor2_Locked_M ), 0x6C5 ),
			new DoorInfo( typeof( BarredMetalDoor_Locked_M ), 0x685 ),
			new DoorInfo( typeof( BarredMetalDoor2_Locked_M ), 0x1FED ),
			new DoorInfo( typeof( RattanDoor_Locked_M ), 0x695 ),
			new DoorInfo( typeof( DarkWoodDoor_Locked_M ), 0x6A5 ),
			new DoorInfo( typeof( MediumWoodDoor_Locked_M ), 0x6B5 ),
			new DoorInfo( typeof( LightWoodDoor_Locked_M ), 0x6D5 ),
			new DoorInfo( typeof( StrongWoodDoor_Locked_M ), 0x6E5 ),
			new DoorInfo( typeof( IronGate_Locked_M ), 0x824 ),
			new DoorInfo( typeof( IronGateShort_Locked_M ), 0x84c ),
			new DoorInfo( typeof( LightWoodGate_Locked_M ), 0x839 ),
			new DoorInfo( typeof( DarkWoodGate_Locked_M ), 0x866 ),
			/*Added by Raist*/
			new DoorInfo( typeof( TreeDoor_Locked_M ), 0x2BE9),
			new DoorInfo( typeof( CarvedWoodDoor_Locked_M ), 0x2BF9),
			new DoorInfo( typeof( ForgedSteelDoor_Locked_M ), 0x2C09),
			new DoorInfo( typeof( RedOrnateWoodDoor_Locked_M ), 0x2C7E),
			new DoorInfo( typeof( HippieWoodDoor_Locked_M ), 0x2C8E),
			new DoorInfo( typeof( CelestialSteelDoor_Locked_M ), 0x2C9E),
			new DoorInfo( typeof( WhiteDragonWoodDoor_Locked_M ), 0x2CAE),
			new DoorInfo( typeof( GoodLuckWoodDoor_Locked_M ), 0x2CBE),
			new DoorInfo( typeof( PaintedRatanDoor_Locked_M ), 0x2CCE),
			new DoorInfo( typeof( TigerRatanDoor_Locked_M ), 0x2E48),

			//High 50, 100
			new DoorInfo( typeof( MetalDoor_Locked_H ), 0x675 ),
			new DoorInfo( typeof( MetalDoor2_Locked_H ), 0x6C5 ),
			new DoorInfo( typeof( BarredMetalDoor_Locked_H ), 0x685 ),
			new DoorInfo( typeof( BarredMetalDoor2_Locked_H ), 0x1FED ),
			new DoorInfo( typeof( RattanDoor_Locked_H ), 0x695 ),
			new DoorInfo( typeof( DarkWoodDoor_Locked_H ), 0x6A5 ),
			new DoorInfo( typeof( MediumWoodDoor_Locked_H ), 0x6B5 ),
			new DoorInfo( typeof( LightWoodDoor_Locked_H ), 0x6D5 ),
			new DoorInfo( typeof( StrongWoodDoor_Locked_H ), 0x6E5 ),
			new DoorInfo( typeof( IronGate_Locked_H ), 0x824 ),
			new DoorInfo( typeof( IronGateShort_Locked_H ), 0x84c ),
			new DoorInfo( typeof( LightWoodGate_Locked_H ), 0x839 ),
			new DoorInfo( typeof( DarkWoodGate_Locked_H ), 0x866 ),
			/*Added by Raist*/
			new DoorInfo( typeof( TreeDoor_Locked_H ), 0x2BE9),
			new DoorInfo( typeof( CarvedWoodDoor_Locked_H ), 0x2BF9),
			new DoorInfo( typeof( ForgedSteelDoor_Locked_H ), 0x2C09),
			new DoorInfo( typeof( RedOrnateWoodDoor_Locked_H ), 0x2C7E),
			new DoorInfo( typeof( HippieWoodDoor_Locked_H ), 0x2C8E),
			new DoorInfo( typeof( CelestialSteelDoor_Locked_H ), 0x2C9E),
			new DoorInfo( typeof( WhiteDragonWoodDoor_Locked_H ), 0x2CAE),
			new DoorInfo( typeof( GoodLuckWoodDoor_Locked_H ), 0x2CBE),
			new DoorInfo( typeof( PaintedRatanDoor_Locked_H ), 0x2CCE),
			new DoorInfo( typeof( TigerRatanDoor_Locked_H ), 0x2E48),
		};
	}

	public class DoorInfo
	{
		public Type m_Type;
		public int m_BaseID;

		public DoorInfo( Type type, int baseID )
		{
			m_Type = type;
			m_BaseID = baseID;
		}
	}
}