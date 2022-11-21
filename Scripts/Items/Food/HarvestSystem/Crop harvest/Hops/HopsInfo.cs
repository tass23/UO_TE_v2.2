
using System;
using System.Collections;

namespace Server.Items
{
	public enum HopsVariety
	{
		None = 0,
		BitterHops = 1,
		SnowHops,
		ElvenHops,
		SweetHops
	}

	public enum HopsVarietyType
	{
		None,
		Hops
	}

	public class HopsVarietyInfo
	{
		private int m_Hue;
		private int m_Number;
		private string m_Name;
		private HopsVariety m_Variety;
		private Type[] m_VarietyTypes;

		public int Hue{ get{ return m_Hue; } }
		public int Number{ get{ return m_Number; } }
		public string Name{ get{ return m_Name; } }
		public HopsVariety Resource{ get{ return m_Variety; } }
		public Type[] VarietyTypes{ get{ return m_VarietyTypes; } }

		public HopsVarietyInfo( int hue, int number, string name, HopsVariety variety, params Type[] varietyTypes )
		{
			m_Hue = hue;
			m_Number = number;
			m_Name = name;
			m_Variety = variety;
			m_VarietyTypes = varietyTypes;

			for ( int i = 0; i < varietyTypes.Length; ++i )
				BrewingResources.RegisterType( varietyTypes[i], variety );
		}
	}

	public class BrewingResources
	{
		private static HopsVarietyInfo[] m_HopsInfo = new HopsVarietyInfo[]
		{
			new HopsVarietyInfo( 0x000, 0,	"Bitter Hops",		HopsVariety.BitterHops,		typeof( BitterHops ) ),
			new HopsVarietyInfo( 0x481, 0,	"Snow Hops",		HopsVariety.SnowHops,		typeof( SnowHops ) ),
			new HopsVarietyInfo( 0x17, 0,	"Elven Hops",		HopsVariety.ElvenHops,		typeof( ElvenHops ) ),
			new HopsVarietyInfo( 0x30, 0,	"Sweet Hops",		HopsVariety.SweetHops,		typeof( SweetHops ) )
		};

		public static bool IsStandard( HopsVariety variety )
		{
			return ( variety == HopsVariety.None || variety == HopsVariety.BitterHops );
		}

		private static Hashtable m_TypeTable;

		public static void RegisterType( Type resourceType, HopsVariety variety )
		{
			if ( m_TypeTable == null )
				m_TypeTable = new Hashtable();

			m_TypeTable[resourceType] = variety;
		}

		public static HopsVariety GetFromType( Type resourceType )
		{
			if ( m_TypeTable == null )
				return HopsVariety.None;

			object obj = m_TypeTable[resourceType];

			if ( !(obj is HopsVariety) )
				return HopsVariety.None;

			return ( HopsVariety )obj;
		}

		public static HopsVarietyInfo GetInfo( HopsVariety variety )
		{
			HopsVarietyInfo[] list = null;

			switch ( GetType( variety ) )
			{
				case HopsVarietyType.Hops: list = m_HopsInfo; break;
			}

			if ( list != null )
			{
				int index = GetIndex( variety );

				if ( index >= 0 && index < list.Length )
					return list[index];
			}

			return null;
		}

		public static HopsVarietyType GetType( HopsVariety variety )
		{
			if ( variety >= HopsVariety.BitterHops && variety <= HopsVariety.SweetHops )
				return HopsVarietyType.Hops;

			return HopsVarietyType.None;
		}

		public static HopsVariety GetStart( HopsVariety variety )
		{
			switch ( GetType( variety ) )
			{
				case HopsVarietyType.Hops: return HopsVariety.BitterHops;
			}

			return HopsVariety.None;
		}

		public static int GetIndex( HopsVariety variety )
		{
			HopsVariety start = GetStart( variety );

			if ( start == HopsVariety.None )
				return 0;

			return (int)(variety - start);
		}

		public static int GetLocalizationNumber( HopsVariety variety )
		{
			HopsVarietyInfo info = GetInfo( variety );

			return ( info == null ? 0 : info.Number );
		}

		public static int GetHue( HopsVariety variety )
		{
			HopsVarietyInfo info = GetInfo( variety );

			return ( info == null ? 0 : info.Hue );
		}

		public static string GetName( HopsVariety variety )
		{
			HopsVarietyInfo info = GetInfo( variety );

			return ( info == null ? String.Empty : info.Name );
		}

		public static HopsVariety GetFromHopsInfo( HopsInfo info )
		{
			if ( info.Level == 0 )
				return HopsVariety.BitterHops;
			else if ( info.Level == 1 )
				return HopsVariety.SnowHops;
			else if ( info.Level == 2 )
				return HopsVariety.ElvenHops;
			else if ( info.Level == 3 )
				return HopsVariety.SweetHops;

			return HopsVariety.None;
		}
	}

	public class HopsInfo
	{
		public static readonly HopsInfo BitterHops	= new HopsInfo( 0, 0x000, "Bitter Hops" );
		public static readonly HopsInfo SnowHops	= new HopsInfo( 1, 0x481, "Snow Hops" );
		public static readonly HopsInfo ElvenHops	= new HopsInfo( 2, 0x17, "Elven Hops" );
		public static readonly HopsInfo SweetHops	= new HopsInfo( 3, 0x30, "Sweet Hops" );

		private int m_Level;
		private int m_Hue;
		private string m_Name;

		public HopsInfo( int level, int hue, string name )
		{
			m_Level = level;
			m_Hue = hue;
			m_Name = name;
		}

		public int Level
		{
			get
			{
				return m_Level;
			}
		}

		public int Hue
		{
			get
			{
				return m_Hue;
			}
		}

		public string Name
		{
			get
			{
				return m_Name;
			}
		}
	}
}