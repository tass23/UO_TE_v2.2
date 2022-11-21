
using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Items.Crops;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Scripts.Commands;
using Server.Targeting;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Gumps
{
	#region " Gump "
	public class AddGrapeVineGump : Gump
	{
		private Mobile m_From;
		private GumpPage m_Page;
		private int m_VarietyIndex;

		private const int LabelHue = 0x480;
		private const int LabelColor = 0x7FFF;
		private const int FontColor = 0xFFFFFF;

		private enum GumpPage
		{
			None,
			PickResource,
			PickResource2,
			DisplayItem
		}

		public AddGrapeVineGump( Mobile from, object notice, int varietyindex ) : this( from, notice, varietyindex, GumpPage.None ){}

		private AddGrapeVineGump( Mobile from, object notice, int varietyindex, GumpPage page ) : base( 40, 40 )
		{
			m_From = from;
			m_Page = page;
			m_VarietyIndex = varietyindex;

			from.CloseGump( typeof( AddGrapeVineGump ) );

			AddPage( 0 );

			AddBackground( 0, 0, 530, 407, 5054 );
			AddImageTiled( 10, 10, 510, 22, 2624 );
			AddImageTiled( 10, 292, 150, 45, 2624 );
			AddImageTiled( 165, 292, 355, 45, 2624 );
			AddImageTiled( 10, 342, 510, 55, 2624 );
			AddImageTiled( 10, 37, 245, 250, 2624 );
			AddImageTiled( 260, 37, 260, 250, 2624 );
			AddAlphaRegion( 10, 10, 510, 387 );

			AddHtml( 10, 12, 510, 20, "<basefont color="+LabelColor+"><CENTER>Add Grapevine Menu</CENTER></basefont>", false, false );

			AddHtml( 10, 37, 245, 22, "<basefont color="+LabelColor+"><CENTER>VINE TYPE</CENTER></basefont>", false, false );
			AddHtml( 250, 37, 260, 22, "<basefont color="+LabelColor+"><CENTER>VINE DETAILS</CENTER></basefont>", false, false );
			AddHtmlLocalized( 10, 302, 150, 25, 1044012, LabelColor, false, false );

			AddButton( 15, 372, 4017, 4019, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 375, 150, 18, 1011441, LabelColor, false, false );

			AddButton( 270, 372, 4005, 4007, GetButtonID( 5, 1 ), GumpButtonType.Reply, 0 );
			AddHtml( 305, 375, 150, 18, "<basefont color="+LabelColor+">MOVE</basefont>", false, false );

			AddButton( 270, 345, 4005, 4007, GetButtonID( 5, 2 ), GumpButtonType.Reply, 0 );
			AddHtml( 305, 348, 150, 18, "<basefont color="+LabelColor+">DELETE</basefont>", false, false );

			if ( notice is int && (int)notice >= 0 )
				AddHtml( 170, 295, 350, 40, "<basefont color="+LabelColor+">"+m_Types[(int)notice].m_VineName+"</basefont>", false, false );
			else if ( notice is string )
				AddHtml( 170, 295, 350, 40, String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", FontColor, notice ), false, false );

			AddButton( 15, 345, 4005, 4007, GetButtonID( 5, 0 ), GumpButtonType.Reply, 0 );
			if (m_VarietyIndex >= 0 )
			{
				AddHtml( 50, 348, 250, 18, "<basefont color="+LabelColor+">"+m_Grapes[varietyindex].m_Name+"</basefont>", false, false );
			}
			else
			{
				m_VarietyIndex = 0;
				AddHtml( 50, 348, 250, 18, "<basefont color="+LabelColor+">"+m_Grapes[0].m_Name+"</basefont>", false, false );
			}

			CreateGroupList();

			if ( page == GumpPage.PickResource )
				CreateResList( false );
			else if ( page == GumpPage.PickResource2 )
				CreateResList( true );
			else if ( page == GumpPage.DisplayItem && ( notice is int && (int)notice >= 0 ) )
				CreateItemDisplay( (int)notice );
		}

		public void CreateResList( bool opt )
		{
			for ( int i = 0; i < m_Grapes.Length; ++i )
			{
				int index = i % 10;

				if ( index == 0 )
				{
					if ( i > 0 )
						AddButton( 485, 262, 4005, 4007, 0, GumpButtonType.Page, (i / 10) + 1 );

					AddPage( (i / 10) + 1 );

					if ( i > 0 )
						AddButton( 455, 262, 4014, 4015, 0, GumpButtonType.Page, i / 10 );
				}

				AddButton( 270, 60 + (index * 20), 4005, 4007, GetButtonID( 4, i ), GumpButtonType.Reply, 0 );

				AddLabel( 305, 60 + (index * 20), LabelHue, m_Grapes[i].m_Name );
			}
		}

		public void CreateItemDisplay( int index )
		{
			AddItem( 335, 110, m_Types[index].m_BaseID );

			AddButton( 270, 260, 4005, 4007, GetButtonID( 3, index ), GumpButtonType.Reply, 0 );
			AddHtml( 305, 263, 150, 18, "<basefont color="+LabelColor+">ADD NOW</basefont>", false, false );
		}

		public int CreateGroupList()
		{
			for ( int i = 0; i < m_Types.Length; i++ )
			{
				AddButton( 15, 60 + (i * 20), 4005, 4007, GetButtonID( 1, i ), GumpButtonType.Reply, 0 );
				AddLabel( 50, 60 + (i * 20), LabelHue, m_Types[i].m_VineName );
				AddButton( 215, 60 + (i * 20), 4011, 4012, GetButtonID( 2, i ), GumpButtonType.Reply, 0 );
			}

			return m_Types.Length;
		}

		public static int GetButtonID( int type, int index )
		{
			return 1 + type + (index * 7);
		}

		public class InternalTarget : Target
		{
			private int m_IType;
			private int m_IVariety;

			public InternalTarget( int itype, int ivariety ) : base( -1, true, TargetFlags.None )
			{
				m_IType = itype;
				m_IVariety = ivariety;

				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
				{
					object obj = Activator.CreateInstance( m_Types[m_IType].m_Type );
					if ( obj is BaseGrapeVine )
					{
						if (m_IVariety < 0 || m_IVariety > m_Grapes.Length)
							m_IVariety = 0;

						BaseGrapeVine vine = (BaseGrapeVine)obj;

						vine.Movable = false;
						vine.Variety = m_Grapes[m_IVariety].m_Variety;
						if ( from.AccessLevel == AccessLevel.Player )
							vine.Placer = from;

						Point3D loc = new Point3D( p );
						if ( p is StaticTarget )
							loc.Z -= TileData.ItemTable[((StaticTarget)p).ItemID & 0x3FFF].CalcHeight;

						if ( VinePlacement.ValidatePlacement(loc, from, o) )
						{
							if ( (from.AccessLevel == AccessLevel.Player && VinePlacement.PayForVine(from)) || from.AccessLevel > AccessLevel.Player )
							{
								vine.MoveToWorld(loc, from.Map);
								string addnotice = m_Types[m_IType].m_VineName+" Added";
								from.SendGump( new AddGrapeVineGump( from, addnotice, m_IVariety, GumpPage.None ) );

							}
							else
							{
								string addnotice = "You cannot afford to place this vine!";
								from.SendGump( new AddGrapeVineGump( from, addnotice, m_IVariety, GumpPage.None ) );
							}
						}
						else
						{
							vine.Delete();
							string addnotice = "Invalid target or something is blocking vine placement.";
							from.SendGump( new AddGrapeVineGump( from, addnotice, m_IVariety, GumpPage.None ) );
						}
					}
				}
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
			{
				if ( cancelType == TargetCancelType.Canceled )
					from.SendGump( new AddGrapeVineGump( from, null, m_IVariety, GumpPage.None ) );
			}
		}

		public class PickVineMoveTarget : Target
		{
			public PickVineMoveTarget() : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( !BaseCommand.IsAccessible( from, o ) )
				{
					from.SendMessage( "That is not accessible." );
					return;
				}

				if ( o is BaseGrapeVine )
				{
					BaseGrapeVine vine = (BaseGrapeVine)o;

					if (!vine.Deleted && vine.Placer == from )
					{
						from.Target = new VineMoveToTarget( o );
					}
					else
					{
						from.SendMessage( "Invalid target.  Only grapevines that you placed can be moved." );
					}
				}
				else
				{
					from.SendMessage( "Invalid target.  Only grapevines that you placed can be moved." );
				}
			}
		}

		public class VineMoveToTarget : Target
		{
			private object m_Object;

			public VineMoveToTarget( object o ) : base( -1, true, TargetFlags.None )
			{
				m_Object = o;

				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
				{
					if ( !BaseCommand.IsAccessible( from, m_Object ) )
					{
						from.SendMessage( "That is not accessible." );
						return;
					}

					if ( p is Item )
						p = ((Item)p).GetWorldTop();

					CommandLogging.WriteLine( from, "{0} {1} moving {2} to {3}", from.AccessLevel, CommandLogging.Format( from ), CommandLogging.Format( m_Object ), new Point3D( p ) );
					if ( m_Object is Item )
					{
						Item item = (Item)m_Object;

						Point3D loc = new Point3D(p);
						if ( p is StaticTarget )
							loc.Z -= TileData.ItemTable[((StaticTarget)p).ItemID & 0x3FFF].CalcHeight;

						if ( VinePlacement.ValidatePlacement(loc, from, m_Object) )
							item.MoveToWorld( loc, from.Map );
					}
				}
			}
		}

		public class VineDeleteTarget : Target
		{
			public VineDeleteTarget() : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( !BaseCommand.IsAccessible( from, o ) )
				{
					from.SendMessage( "That is not accessible." );
					return;
				}

				if ( o is BaseGrapeVine )
				{
					BaseGrapeVine vine = (BaseGrapeVine)o;

					if ( vine.Placer == from )
					{
						if ( VinePlacement.RefundForVine(from)   )
							((Item)o).Delete();
					}
					else
					{
						from.SendMessage( "Invalid target.  Only grapevines that you placed can be deleted." );
					}
				}
				else
				{
					from.SendMessage( "Invalid target.  Only grapevines that you placed can be deleted." );
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( info.ButtonID <= 0 )
				return;

			int buttonID = info.ButtonID - 1;
			int type = buttonID % 7;
			int index = buttonID / 7;

			switch ( type )
			{
				case 0:
				{
					break;
				}
				case 1:
				{
					if ( index >= 0 && index < m_Types.Length )
					{
						m_From.Target = new InternalTarget( index, m_VarietyIndex );
					}

					break;
				}
				case 2:
				{
					if ( index >= 0 && index < m_Types.Length )
						m_From.SendGump( new AddGrapeVineGump( m_From, index, m_VarietyIndex, GumpPage.DisplayItem ) );

					break;
				}
				case 3:
				{
					if ( index >= 0 && index < m_Types.Length )
					{
						m_From.Target = new InternalTarget( index, m_VarietyIndex );
					}

					break;
				}
				case 4:
				{
					if ( m_Page == GumpPage.PickResource && index >= 0 && index < m_Grapes.Length )
					{
						m_VarietyIndex = index;

						m_From.SendGump( new AddGrapeVineGump( m_From, null, m_VarietyIndex, GumpPage.None ) );
					}
					else if ( m_Page == GumpPage.PickResource2 && index >= 0 && index < m_Grapes.Length )
					{
						m_VarietyIndex = index;

						m_From.SendGump( new AddGrapeVineGump( m_From, null, m_VarietyIndex, GumpPage.None ) );
					}

					break;
				}
				case 5:
				{
					switch ( index )
					{
						case 0:
						{
							m_From.SendGump( new AddGrapeVineGump( m_From, null, m_VarietyIndex, GumpPage.PickResource ) );

							break;
						}
						case 1:
						{
								m_From.Target = new PickVineMoveTarget();

							string movenotice = "Move Item";
							m_From.SendGump( new AddGrapeVineGump( m_From, movenotice, m_VarietyIndex, GumpPage.None ) );

							break;
						}
						case 2:
						{
								m_From.Target = new VineDeleteTarget();

							string deletenotice = "Delete Item";
							m_From.SendGump( new AddGrapeVineGump( m_From, deletenotice, m_VarietyIndex, GumpPage.None ) );

							break;
						}
					}

					break;
				}
			}
		}

		public static VineInfo[] m_Types = new VineInfo[]
		{
			new VineInfo( "East Pole (End 1)", typeof( GrapeVinePoleEast3 ), 0xD22 ),
			new VineInfo( "East Pole (Center)", typeof( GrapeVinePoleEast2 ), 0xD21 ),
			new VineInfo( "East Pole (End 2)", typeof( GrapeVinePoleEast ), 0xD20 ),
			new VineInfo( "East Branch (1)", typeof( GrapeVineBranchEast ), 0xD23 ),
			new VineInfo( "East Branch (2)", typeof( GrapeVineBranchEast2 ), 0xD24 ),
			new VineInfo( "North Pole (End 1)", typeof( GrapeVinePoleNorth2 ), 0xD1D ),
			new VineInfo( "North Pole (Center)", typeof( GrapeVinePoleNorth3 ), 0xD1C ),
			new VineInfo( "North Pole (End 2)", typeof( GrapeVinePoleNorth ), 0xD1B ),
			new VineInfo( "North Branch (1)", typeof( GrapeVineBranchNorth ), 0xD1E ),
			new VineInfo( "North Branch (2)", typeof( GrapeVineBranchNorth2 ), 0xD1F )
		};

		private static GrapeInfo[] m_Grapes = new GrapeInfo[]
		{
			new GrapeInfo( "Cabernet Sauvignon", GrapeVariety.CabernetSauvignon, typeof( CabernetSauvignonGrapes ) ),
			new GrapeInfo( "Chardonnay", GrapeVariety.Chardonnay, typeof( ChardonnayGrapes ) ),
			new GrapeInfo( "Chenin Blanc", GrapeVariety.CheninBlanc, typeof( CheninBlancGrapes ) ),
			new GrapeInfo( "Merlot", GrapeVariety.Merlot, typeof( MerlotGrapes ) ),
			new GrapeInfo( "Pinot Noir", GrapeVariety.PinotNoir, typeof( PinotNoirGrapes ) ),
			new GrapeInfo( "Riesling", GrapeVariety.Riesling, typeof( RieslingGrapes ) ),
			new GrapeInfo( "Sangiovese", GrapeVariety.Sangiovese, typeof( SangioveseGrapes ) ),
			new GrapeInfo( "Sauvignon Blanc", GrapeVariety.SauvignonBlanc, typeof( SauvignonBlancGrapes ) ),
			new GrapeInfo( "Shiraz", GrapeVariety.Shiraz, typeof( ShirazGrapes ) ),
			new GrapeInfo( "Viognier", GrapeVariety.Viognier, typeof( ViognierGrapes ) ),
			new GrapeInfo( "Zinfandel", GrapeVariety.Zinfandel, typeof( ZinfandelGrapes ) )
		};
	}
	#endregion

	#region " Info Classes "
	public class VineInfo
	{
		public string m_VineName;
		public Type m_Type;
		public int m_BaseID;

		public VineInfo( string vinename, Type type, int baseID )
		{
			m_VineName = vinename;
			m_Type = type;
			m_BaseID = baseID;
		}
	}

	public class GrapeInfo
	{
		public string m_Name;
		public GrapeVariety m_Variety;
		public Type m_VarietyType;

		public GrapeInfo( string name, GrapeVariety variety, Type varietyType )
		{
			m_Name = name;
			m_Variety = variety;
			m_VarietyType= varietyType;
		}
	}
	#endregion
}