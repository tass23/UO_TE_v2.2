using System;
using System.IO;
using System.Collections;
using Server;

namespace Server.Engines.BulkOrders
{
	public class LargeMobileBulkEntry
	{
		private LargeMobileBOD m_Owner;
		private int m_Amount;
		private SmallMobileBulkEntry m_Details;

		public LargeMobileBOD Owner{ get{ return m_Owner; } set{ m_Owner = value; } }
		public int Amount{ get{ return m_Amount; } set{ m_Amount = value; if ( m_Owner != null ) m_Owner.InvalidateProperties(); } }
		public SmallMobileBulkEntry Details{ get{ return m_Details; } }

		public static SmallMobileBulkEntry[] Mounts
		{
			get{ return GetEntries( "Taming", "mounts" ); }
		}

		public static SmallMobileBulkEntry[] HardMounts
		{
			get{ return GetEntries( "Taming", "hardmounts" ); }
		}

		public static SmallMobileBulkEntry[] Dragons
		{
			get{ return GetEntries( "Taming", "dragons" ); }
		}

		public static SmallMobileBulkEntry[] FarmAnimals
		{
			get{ return GetEntries( "Taming", "farmanimals" ); }
		}

		public static SmallMobileBulkEntry[] Spiders
		{
			get{ return GetEntries( "Taming", "spiders" ); }
		}

		public static SmallMobileBulkEntry[] Kanines
		{
			get{ return GetEntries( "Taming", "kanines" ); }
		}

		public static SmallMobileBulkEntry[] Felines
		{
			get{ return GetEntries( "Taming", "felines" ); }
		}

		public static SmallMobileBulkEntry[] Bears
		{
			get{ return GetEntries( "Taming", "bears" ); }
		}

		public static SmallMobileBulkEntry[] Birds
		{
			get{ return GetEntries( "Taming", "birds" ); }
		}

		public static SmallMobileBulkEntry[] Rodents
		{
			get{ return GetEntries( "Taming", "rodents" ); }
		}

		public static SmallMobileBulkEntry[] Beetles
		{
			get{ return GetEntries( "Taming", "beetles" ); }
		}

		public static SmallMobileBulkEntry[] Hiryus
		{
			get{ return GetEntries( "Taming", "hiryus" ); }
		}

		private static Hashtable m_Cache;

		public static SmallMobileBulkEntry[] GetEntries( string type, string name )
		{
			if ( m_Cache == null )
				m_Cache = new Hashtable();

			Hashtable table = (Hashtable)m_Cache[type];

			if ( table == null )
				m_Cache[type] = table = new Hashtable();

			SmallMobileBulkEntry[] entries = (SmallMobileBulkEntry[])table[name];

			if ( entries == null )
				table[name] = entries = SmallMobileBulkEntry.LoadEntries( type, name );

			return entries;
		}

		public static LargeMobileBulkEntry[] ConvertEntries( LargeMobileBOD owner, SmallMobileBulkEntry[] small )
		{
			LargeMobileBulkEntry[] large = new LargeMobileBulkEntry[small.Length];

			for ( int i = 0; i < small.Length; ++i )
				large[i] = new LargeMobileBulkEntry( owner, small[i] );

			return large;
		}

		public LargeMobileBulkEntry( LargeMobileBOD owner, SmallMobileBulkEntry details )
		{
			m_Owner = owner;
			m_Details = details;
		}

		public LargeMobileBulkEntry( LargeMobileBOD owner, GenericReader reader )
		{
			m_Owner = owner;
			m_Amount = reader.ReadInt();

			Type realType = null;

			string type = reader.ReadString();

			if ( type != null )
				realType = ScriptCompiler.FindTypeByFullName( type );

			m_Details = new SmallMobileBulkEntry( realType, reader.ReadString(), reader.ReadInt() );
		}

		public void Serialize( GenericWriter writer )
		{
			writer.Write( m_Amount );
			writer.Write( m_Details.Type == null ? null : m_Details.Type.FullName );
			writer.Write( m_Details.AnimalName );
			writer.Write( m_Details.Graphic );
		}
	}
}