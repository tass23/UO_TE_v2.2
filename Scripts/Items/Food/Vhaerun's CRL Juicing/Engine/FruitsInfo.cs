using System;
using System.Collections;

namespace Server.Items
{
	public enum FruitsVariety
	{
		None = 0,
		Apple = 1,
		Banana,
		Dates,
		Grapes,
		Lemon,
		Lime,
		Orange,
		Peach,
		Pear,
		Pumpkin,
		Tomato,
		Watermelon,
		Apricot,
		Blackberry,
		Blueberry,
		Cherry,
		Cranberry,
		Grapefruit,
		Kiwi,
		Mango,
		Pineapple,
		Pomegranate,
		Strawberry,
		Almond,
		Asparagus,
		Avocado,
		Beet,
		BlackRaspberry,
		Cantaloupe,
		Carrot,
		Cauliflower,
		Celery,
		Coconut,
		Corn,
		Cucumber,
		GreenSquash,
		HoneydewMelon,
		Onion,
		Peanut,
		Pistacio,
		Potato,
		Radish,
		RedRaspberry,
		Spinach,
		Squash,
		SweetPotato,
		Turnip,
	}

	public enum FruitsVarietyType
	{
		None,
		Fruits
	}

	public class FruitsVarietyInfo
	{
		private int m_Hue;
		private int m_Number;
		private string m_Name;
		private FruitsVariety m_Variety;
		private Type[] m_VarietyTypes;

		public int Hue{ get{ return m_Hue; } }
		public int Number{ get{ return m_Number; } }
		public string Name{ get{ return m_Name; } }
		public FruitsVariety Resource{ get{ return m_Variety; } }
		public Type[] VarietyTypes{ get{ return m_VarietyTypes; } }

		public FruitsVarietyInfo( int hue, int number, string name, FruitsVariety variety, params Type[] varietyTypes )
		{
			m_Hue = hue;
			m_Number = number;
			m_Name = name;
			m_Variety = variety;
			m_VarietyTypes = varietyTypes;

			for ( int i = 0; i < varietyTypes.Length; ++i )
				JuicingResources.RegisterType( varietyTypes[i], variety );
		}
	}

	public class JuicingResources
	{
		private static FruitsVarietyInfo[] m_FruitsInfo = new FruitsVarietyInfo[]
		{
			new FruitsVarietyInfo( 0x0, 0,	"Apple",		FruitsVariety.Apple,		typeof( Apple ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Banana",		FruitsVariety.Banana,	typeof( Banana ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Date",		FruitsVariety.Dates,		typeof( Dates ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Grape",		FruitsVariety.Grapes,	typeof( Grapes ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Lemon",		FruitsVariety.Lemon,	typeof( Lemon ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Lime",		FruitsVariety.Lime,		typeof( Lime ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Orange",		FruitsVariety.Orange,	typeof( Orange ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Peach",		FruitsVariety.Peach,		typeof( Peach ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Pear",		FruitsVariety.Pear,		typeof( Pear ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Pumpkin",	FruitsVariety.Pumpkin,	typeof( Pumpkin ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Tomato",		FruitsVariety.Tomato,	typeof( Tomato ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Watermelon",	FruitsVariety.Watermelon,	typeof( Watermelon ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Apricot",		FruitsVariety.Apricot,	typeof( Apricot ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Blackberry",	FruitsVariety.Blackberry,	typeof( Blackberry ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Blueberry",	FruitsVariety.Blueberry,	typeof( Blueberry ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Cherry",		FruitsVariety.Cherry,		typeof( Cherry ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Cranberry",	FruitsVariety.Cranberry,	typeof( Cranberry ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Grapefruit",	FruitsVariety.Grapefruit,	typeof( Grapefruit ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Kiwi",		FruitsVariety.Kiwi,		typeof( Kiwi ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Mango",		FruitsVariety.Mango,	typeof( Mango ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Pineapple",	FruitsVariety.Pineapple,	typeof( Pineapple ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Pomegranate",	FruitsVariety.Pomegranate,	typeof( Pomegranate ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Strawberry",	FruitsVariety.Strawberry,	typeof( Strawberry ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Almond",		FruitsVariety.Almond,	typeof( Almond ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Asparagus",	FruitsVariety.Asparagus,	typeof( Asparagus ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Avocado",	FruitsVariety.Avocado,	typeof( Avocado ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Beet",		FruitsVariety.Beet,		typeof( Beet ) ),
			new FruitsVarietyInfo( 0x0, 0,	"BlackRaspberry",	FruitsVariety.BlackRaspberry,	typeof( BlackRaspberry ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Cantaloupe",	FruitsVariety.Cantaloupe,	typeof( Cantaloupe ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Carrot",		FruitsVariety.Carrot,		typeof( Carrot ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Cauliflower",	FruitsVariety.Cauliflower,	typeof( Cauliflower ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Celery",		FruitsVariety.Celery,		typeof( Celery ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Coconut",	FruitsVariety.Coconut,	typeof( Coconut ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Corn",		FruitsVariety.Corn,		typeof( Corn ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Cucumber",	FruitsVariety.Cucumber,	typeof( Cucumber ) ),
			new FruitsVarietyInfo( 0x0, 0,	"GreenSquash",	FruitsVariety.GreenSquash,	typeof( GreenSquash ) ),
			new FruitsVarietyInfo( 0x0, 0,	"HoneydewMelon",	FruitsVariety.HoneydewMelon,	typeof( HoneydewMelon ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Onion",		FruitsVariety.Onion,		typeof( Onion ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Peanut",		FruitsVariety.Peanut,	typeof( Peanut ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Pistacio",	FruitsVariety.Pistacio,	typeof( Pistacio ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Potato",		FruitsVariety.Potato,		typeof( Potato ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Radish",		FruitsVariety.Radish,	typeof( Radish ) ),
			new FruitsVarietyInfo( 0x0, 0,	"RedRaspberry",	FruitsVariety.RedRaspberry,	typeof( RedRaspberry ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Spinach",	FruitsVariety.Spinach,	typeof( Spinach ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Squash",		FruitsVariety.Squash,	typeof( Squash ) ),
			new FruitsVarietyInfo( 0x0, 0,	"SweetPotato",	FruitsVariety.SweetPotato,	typeof( SweetPotato ) ),
			new FruitsVarietyInfo( 0x0, 0,	"Turnip",		FruitsVariety.Turnip,		typeof( Turnip ) )
		};

		public static bool IsStandard( FruitsVariety variety )
		{
			return ( variety == FruitsVariety.None || variety == FruitsVariety.Apple );
		}

		private static Hashtable m_TypeTable;

		public static void RegisterType( Type resourceType, FruitsVariety variety )
		{
			if ( m_TypeTable == null ) m_TypeTable = new Hashtable();
			m_TypeTable[resourceType] = variety;
		}

		public static FruitsVariety GetFromType( Type resourceType )
		{
			if ( m_TypeTable == null ) return FruitsVariety.None;
			object obj = m_TypeTable[resourceType];
			if ( !(obj is FruitsVariety) ) return FruitsVariety.None;
			return ( FruitsVariety )obj;
		}

		public static FruitsVarietyInfo GetInfo( FruitsVariety variety )
		{
			FruitsVarietyInfo[] list = null;
			switch ( GetType( variety ) )
			{
				case FruitsVarietyType.Fruits: list = m_FruitsInfo; break;
			}

			if ( list != null )
			{
				int index = GetIndex( variety );
				if ( index >= 0 && index < list.Length ) return list[index];
			}
			return null;
		}

		public static FruitsVarietyType GetType( FruitsVariety variety )
		{
			if ( variety >= FruitsVariety.Apple && variety <= FruitsVariety.Turnip ) return FruitsVarietyType.Fruits;
			return FruitsVarietyType.None;
		}

		public static FruitsVariety GetStart( FruitsVariety variety )
		{
			switch ( GetType( variety ) )
			{
				case FruitsVarietyType.Fruits: return FruitsVariety.Apple;
			}
			return FruitsVariety.None;
		}

		public static int GetIndex( FruitsVariety variety )
		{
			FruitsVariety start = GetStart( variety );
			if ( start == FruitsVariety.None ) return 0;
			return (int)(variety - start);
		}

		public static int GetLocalizationNumber( FruitsVariety variety )
		{
			FruitsVarietyInfo info = GetInfo( variety );
			return ( info == null ? 0 : info.Number );
		}

		public static int GetHue( FruitsVariety variety )
		{
			FruitsVarietyInfo info = GetInfo( variety );
			return ( info == null ? 0 : info.Hue );
		}

		public static string GetName( FruitsVariety variety )
		{
			FruitsVarietyInfo info = GetInfo( variety );
			return ( info == null ? String.Empty : info.Name );
		}

		public static FruitsVariety GetFromFruitsInfo( FruitsInfo info )
		{
			if ( info.Level == 0 ) return FruitsVariety.Apple;
			else if ( info.Level == 1 ) return FruitsVariety.Banana;
			else if ( info.Level == 2 ) return FruitsVariety.Dates;
			else if ( info.Level == 3 ) return FruitsVariety.Grapes;
			else if ( info.Level == 4 ) return FruitsVariety.Lemon;
			else if ( info.Level == 5 ) return FruitsVariety.Lime;
			else if ( info.Level == 6 ) return FruitsVariety.Orange;
			else if ( info.Level == 7 ) return FruitsVariety.Peach;
			else if ( info.Level == 8 ) return FruitsVariety.Pear;
			else if ( info.Level == 9 ) return FruitsVariety.Pumpkin;
			else if ( info.Level == 10 ) return FruitsVariety.Tomato;
			else if ( info.Level == 11 ) return FruitsVariety.Watermelon;
			else if ( info.Level == 12 ) return FruitsVariety.Apricot;
			else if ( info.Level == 13 ) return FruitsVariety.Blackberry;
			else if ( info.Level == 14 ) return FruitsVariety.Blueberry;
			else if ( info.Level == 15 ) return FruitsVariety.Cherry;
			else if ( info.Level == 16 ) return FruitsVariety.Cranberry;
			else if ( info.Level == 17 ) return FruitsVariety.Grapefruit;
			else if ( info.Level == 18 ) return FruitsVariety.Kiwi;
			else if ( info.Level == 19 ) return FruitsVariety.Mango;
			else if ( info.Level == 20 ) return FruitsVariety.Pineapple;
			else if ( info.Level == 21 ) return FruitsVariety.Pomegranate;
			else if ( info.Level == 22 ) return FruitsVariety.Strawberry;
			else if ( info.Level == 23 ) return FruitsVariety.Almond;
			else if ( info.Level == 24 ) return FruitsVariety.Asparagus;
			else if ( info.Level == 25 ) return FruitsVariety.Avocado;
			else if ( info.Level == 26 ) return FruitsVariety.Beet;
			else if ( info.Level == 27 ) return FruitsVariety.BlackRaspberry;
			else if ( info.Level == 28 ) return FruitsVariety.Cantaloupe;
			else if ( info.Level == 29 ) return FruitsVariety.Carrot;
			else if ( info.Level == 30 ) return FruitsVariety.Cauliflower;
			else if ( info.Level == 31 ) return FruitsVariety.Celery;
			else if ( info.Level == 32 ) return FruitsVariety.Coconut;
			else if ( info.Level == 33 ) return FruitsVariety.Corn;
			else if ( info.Level == 34 ) return FruitsVariety.Cucumber;
			else if ( info.Level == 35 ) return FruitsVariety.GreenSquash;
			else if ( info.Level == 36 ) return FruitsVariety.HoneydewMelon;
			else if ( info.Level == 37 ) return FruitsVariety.Onion;
			else if ( info.Level == 38 ) return FruitsVariety.Peanut;
			else if ( info.Level == 39 ) return FruitsVariety.Pistacio;
			else if ( info.Level == 40 ) return FruitsVariety.Potato;
			else if ( info.Level == 41 ) return FruitsVariety.Radish;
			else if ( info.Level == 42 ) return FruitsVariety.RedRaspberry;
			else if ( info.Level == 43 ) return FruitsVariety.Spinach;
			else if ( info.Level == 44 ) return FruitsVariety.Squash;
			else if ( info.Level == 45 ) return FruitsVariety.SweetPotato;
			else if ( info.Level == 46 ) return FruitsVariety.Turnip;
			return FruitsVariety.None;
		}
	}

	public class FruitsInfo
	{
		public static readonly FruitsInfo BitterFruits = new FruitsInfo( 0, 0x000, "Bitter Fruits" );
		public static readonly FruitsInfo SnowFruits = new FruitsInfo( 1, 0x481, "Snow Fruits" );
		public static readonly FruitsInfo ElvenFruits = new FruitsInfo( 2, 0x17, "Elven Fruits" );
		public static readonly FruitsInfo SweetFruits = new FruitsInfo( 3, 0x30, "Sweet Fruits" );

		private int m_Level;
		private int m_Hue;
		private string m_Name;

		public FruitsInfo( int level, int hue, string name )
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