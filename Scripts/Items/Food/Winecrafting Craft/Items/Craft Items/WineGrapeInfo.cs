using System;
using System.Collections;

namespace Server.Items
{
	public enum GrapeVariety
	{
		None = 0,
		CabernetSauvignon = 1,
		Chardonnay,
		CheninBlanc,
		Merlot,
		PinotNoir,
		Riesling,
		Sangiovese,
		SauvignonBlanc,
		Shiraz,
		Viognier,
		Zinfandel,
		Apple,
		Apricot,
		Cherry,
		Mango,
		Orange,
		Pear,
		Peach,
		Blackberry,
		BlackRaspberry,
		Blueberry,
		Cranberry,
		RedRaspberry,
		Strawberry,
		Watermelon,
		Rice,
		Dandelion
	}

	public enum GrapeVarietyType
	{
		None,
		Grapes
	}

	public class GrapeVarietyInfo
	{
		private int m_Hue;
		private int m_Number;
		private string m_Name;
		private GrapeVariety m_Variety;
		private Type[] m_VarietyTypes;

		public int Hue{ get{ return m_Hue; } }
		public int Number{ get{ return m_Number; } }
		public string Name{ get{ return m_Name; } }
		public GrapeVariety Resource{ get{ return m_Variety; } }
		public Type[] VarietyTypes{ get{ return m_VarietyTypes; } }

		public GrapeVarietyInfo( int hue, int number, string name, GrapeVariety variety, params Type[] varietyTypes )
		{
			m_Hue = hue;
			m_Number = number;
			m_Name = name;
			m_Variety = variety;
			m_VarietyTypes = varietyTypes;

			for ( int i = 0; i < varietyTypes.Length; ++i )
				WinemakingResources.RegisterType( varietyTypes[i], variety );
		}
	}

	public class WinemakingResources
	{
		private static GrapeVarietyInfo[] m_GrapeInfo = new GrapeVarietyInfo[]
		{
			new GrapeVarietyInfo( 0x000, 0,	"Cabernet Sauvignon",	GrapeVariety.CabernetSauvignon,	typeof( CabernetSauvignonGrapes ) ),
			new GrapeVarietyInfo( 0x1CC, 0,	"Chardonnay",		GrapeVariety.Chardonnay,		typeof( ChardonnayGrapes ) ),
			new GrapeVarietyInfo( 0x16B, 0,	"Chenin Blanc",		GrapeVariety.CheninBlanc,		typeof( CheninBlancGrapes ) ),
			new GrapeVarietyInfo( 0x2CE, 0,	"Merlot",			GrapeVariety.Merlot,		typeof( MerlotGrapes ) ),
			new GrapeVarietyInfo( 0x2CE, 0,	"Pinot Noir",		GrapeVariety.PinotNoir,		typeof( PinotNoirGrapes ) ),
			new GrapeVarietyInfo( 0x1CC, 0,	"Riesling",		GrapeVariety.Riesling,		typeof( RieslingGrapes ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Sangiovese",		GrapeVariety.Sangiovese,		typeof( SangioveseGrapes ) ),
			new GrapeVarietyInfo( 0x16B, 0,	"Sauvignon Blanc",		GrapeVariety.SauvignonBlanc,		typeof( SauvignonBlancGrapes ) ),
			new GrapeVarietyInfo( 0x2CE, 0,	"Shiraz",			GrapeVariety.Shiraz,		typeof( ShirazGrapes ) ),
			new GrapeVarietyInfo( 0x16B, 0,	"Viognier",		GrapeVariety.Viognier,		typeof( ViognierGrapes ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Zinfandel",		GrapeVariety.Zinfandel,		typeof( ZinfandelGrapes ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Apple",			GrapeVariety.Apple,			typeof( Apple ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Apricot",			GrapeVariety.Apricot,		typeof( Apricot ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Cherry",			GrapeVariety.Cherry,		typeof( Cherry ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Mango",			GrapeVariety.Mango,		typeof( Mango ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Orange",			GrapeVariety.Orange,		typeof( Orange ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Pear",			GrapeVariety.Pear,			typeof( Pear ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Peach",			GrapeVariety.Peach,		typeof( Peach ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Blackberry",		GrapeVariety.Blackberry,		typeof( Blackberry ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Black Raspberry",		GrapeVariety.BlackRaspberry,		typeof( BlackRaspberry ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Blueberry",		GrapeVariety.Blueberry,		typeof( Blueberry ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Cranberry",		GrapeVariety.Cranberry,		typeof( Cranberry ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Red Raspberry",		GrapeVariety.RedRaspberry,		typeof( RedRaspberry ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Strawberry",		GrapeVariety.Strawberry,		typeof( Strawberry ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Watermelon",		GrapeVariety.Watermelon,		typeof( Watermelon ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Rice",			GrapeVariety.Rice,			typeof( RiceSheath ) ),
			new GrapeVarietyInfo( 0x000, 0,	"Dandelion",		GrapeVariety.Dandelion,		typeof( Dandelion ) )
		};

		public static bool IsStandard( GrapeVariety variety )
		{
			return ( variety == GrapeVariety.None || variety == GrapeVariety.CabernetSauvignon );
		}

		private static Hashtable m_TypeTable;

		public static void RegisterType( Type resourceType, GrapeVariety variety )
		{
			if ( m_TypeTable == null ) m_TypeTable = new Hashtable();
			m_TypeTable[resourceType] = variety;
		}

		public static GrapeVariety GetFromType( Type resourceType )
		{
			if ( m_TypeTable == null ) return GrapeVariety.None;
			object obj = m_TypeTable[resourceType];
			if ( !(obj is GrapeVariety) ) return GrapeVariety.None;
			return (GrapeVariety)obj;
		}

		public static GrapeVarietyInfo GetInfo( GrapeVariety variety )
		{
			GrapeVarietyInfo[] list = null;

			switch ( GetType( variety ) )
			{
				case GrapeVarietyType.Grapes: list = m_GrapeInfo; break;
			}

			if ( list != null )
			{
				int index = GetIndex( variety );
				if ( index >= 0 && index < list.Length ) return list[index];
			}
			return null;
		}

		public static GrapeVarietyType GetType( GrapeVariety variety )
		{
			if ( variety >= GrapeVariety.CabernetSauvignon && variety <= GrapeVariety.Dandelion )
				return GrapeVarietyType.Grapes;
			return GrapeVarietyType.None;
		}

		public static GrapeVariety GetStart( GrapeVariety variety )
		{
			switch ( GetType( variety ) )
			{
				case GrapeVarietyType.Grapes: return GrapeVariety.CabernetSauvignon;
			}
			return GrapeVariety.None;
		}

		public static int GetIndex( GrapeVariety variety )
		{
			GrapeVariety start = GetStart( variety );
			if ( start == GrapeVariety.None ) return 0;
			return (int)(variety - start);
		}

		public static int GetLocalizationNumber( GrapeVariety variety )
		{
			GrapeVarietyInfo info = GetInfo( variety );
			return ( info == null ? 0 : info.Number );
		}

		public static int GetHue( GrapeVariety variety )
		{
			GrapeVarietyInfo info = GetInfo( variety );
			return ( info == null ? 0 : info.Hue );
		}

		public static string GetName( GrapeVariety variety )
		{
			GrapeVarietyInfo info = GetInfo( variety );
			return ( info == null ? String.Empty : info.Name );
		}

		public static GrapeVariety GetFromWineGrapeInfo( WineGrapeInfo info )
		{
			if ( info.Level == 0 ) return GrapeVariety.CabernetSauvignon;
			else if ( info.Level == 1 ) return GrapeVariety.Chardonnay;
			else if ( info.Level == 2 ) return GrapeVariety.CheninBlanc;
			else if ( info.Level == 3 ) return GrapeVariety.Merlot;
			else if ( info.Level == 4 ) return GrapeVariety.PinotNoir;
			else if ( info.Level == 5 ) return GrapeVariety.Riesling;
			else if ( info.Level == 6 ) return GrapeVariety.Sangiovese;
			else if ( info.Level == 7 ) return GrapeVariety.SauvignonBlanc;
			else if ( info.Level == 8 ) return GrapeVariety.Shiraz;
			else if ( info.Level == 9 ) return GrapeVariety.Viognier;
			else if ( info.Level == 10 ) return GrapeVariety.Zinfandel;
			else if ( info.Level == 11 ) return GrapeVariety.Apple;
			else if ( info.Level == 12 ) return GrapeVariety.Apricot;
			else if ( info.Level == 13 ) return GrapeVariety.Cherry;
			else if ( info.Level == 14 ) return GrapeVariety.Mango;
			else if ( info.Level == 15 ) return GrapeVariety.Orange;
			else if ( info.Level == 16 ) return GrapeVariety.Pear;
			else if ( info.Level == 17 ) return GrapeVariety.Peach;
			else if ( info.Level == 18 ) return GrapeVariety.Blackberry;
			else if ( info.Level == 19 ) return GrapeVariety.BlackRaspberry;
			else if ( info.Level == 20 ) return GrapeVariety.Blueberry;
			else if ( info.Level == 21 ) return GrapeVariety.Cranberry;
			else if ( info.Level == 22 ) return GrapeVariety.RedRaspberry;
			else if ( info.Level == 23 ) return GrapeVariety.Strawberry;
			else if ( info.Level == 24 ) return GrapeVariety.Watermelon;
			else if ( info.Level == 25 ) return GrapeVariety.Rice;
			else if ( info.Level == 26 ) return GrapeVariety.Dandelion;
			return GrapeVariety.None;
		}
	}

	public class WineGrapeInfo
	{
		public static readonly WineGrapeInfo CabernetSauvignon	= new WineGrapeInfo( 0, 0x000, "Cabernet Sauvignon" );
		public static readonly WineGrapeInfo Chardonnay		= new WineGrapeInfo( 1, 0x1CC, "Chardonnay" );
		public static readonly WineGrapeInfo CheninBlanc		= new WineGrapeInfo( 2, 0x16B, "Chenin Blanc" );
		public static readonly WineGrapeInfo Merlot		= new WineGrapeInfo( 3, 0x2CE, "Merlot" );
		public static readonly WineGrapeInfo PinotNoir		= new WineGrapeInfo( 4, 0x2CE, "Pinot Noir" );
		public static readonly WineGrapeInfo Riesling		= new WineGrapeInfo( 5, 0x1CC, "Riesling" );
		public static readonly WineGrapeInfo Sangiovese		= new WineGrapeInfo( 6, 0x000, "Sangiovese" );
		public static readonly WineGrapeInfo SauvignonBlanc	= new WineGrapeInfo( 7, 0x16B, "Sauvignon Blanc" );
		public static readonly WineGrapeInfo Shiraz		= new WineGrapeInfo( 8, 0x2CE, "Shiraz" );
		public static readonly WineGrapeInfo Viognier		= new WineGrapeInfo( 9, 0x16B,  "Viognier" );
		public static readonly WineGrapeInfo Zinfandel		= new WineGrapeInfo( 10, 0x000, "Zinfandel" );
		public static readonly WineGrapeInfo Apple		= new WineGrapeInfo( 11, 0x000, "Apple" );
		public static readonly WineGrapeInfo Apricot		= new WineGrapeInfo( 12, 0x000, "Apricot" );
		public static readonly WineGrapeInfo Cherry		= new WineGrapeInfo( 13, 0x000, "Cherry" );
		public static readonly WineGrapeInfo Mango		= new WineGrapeInfo( 14, 0x000, "Mango" );
		public static readonly WineGrapeInfo Orange		= new WineGrapeInfo( 15, 0x000, "Orange" );
		public static readonly WineGrapeInfo Pear		= new WineGrapeInfo( 16, 0x000, "Pear" );
		public static readonly WineGrapeInfo Peach		= new WineGrapeInfo( 17, 0x000, "Peach" );
		public static readonly WineGrapeInfo Blackberry		= new WineGrapeInfo( 18, 0x000, "Blackberry" );
		public static readonly WineGrapeInfo BlackRaspberry	= new WineGrapeInfo( 19, 0x000, "Black Raspberry" );
		public static readonly WineGrapeInfo Blueberry		= new WineGrapeInfo( 20, 0x000, "Blueberry" );
		public static readonly WineGrapeInfo Cranberry		= new WineGrapeInfo( 21, 0x000, "Cranberry" );
		public static readonly WineGrapeInfo RedRaspberry		= new WineGrapeInfo( 22, 0x000, "Red Raspberry" );
		public static readonly WineGrapeInfo Strawberry		= new WineGrapeInfo( 23, 0x000, "Strawberry" );
		public static readonly WineGrapeInfo Watermelon		= new WineGrapeInfo( 24, 0x000, "Watermelon" );
		public static readonly WineGrapeInfo Rice		= new WineGrapeInfo( 25, 0x000, "Rice" );
		public static readonly WineGrapeInfo Dandelion		= new WineGrapeInfo( 25, 0x000, "Dandelion" );

		private int m_Level;
		private int m_Hue;
		private string m_Name;

		public WineGrapeInfo( int level, int hue, string name )
		{
			m_Level = level;
			m_Hue = hue;
			m_Name = name;
		}

		public int Level { get { return m_Level; } }
		public int Hue { get { return m_Hue; } }
		public string Name { get { return m_Name; } }
	}
}