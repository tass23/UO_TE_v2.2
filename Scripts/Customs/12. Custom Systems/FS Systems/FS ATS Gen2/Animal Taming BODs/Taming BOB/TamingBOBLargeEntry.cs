using System;

namespace Server.Engines.BulkOrders
{
	public class TamingBOBLargeEntry
	{
		private int m_AmountMax;
		private int m_Price;
		private TamingBOBLargeSubEntry[] m_Entries;

		public int AmountMax{ get{ return m_AmountMax; } }
		public int Price{ get{ return m_Price; } set{ m_Price = value; } }
		public TamingBOBLargeSubEntry[] Entries{ get{ return m_Entries; } }

		public Item Reconstruct()
		{
			LargeMobileBOD bod = null;

			bod = new LargeTamingBOD( m_AmountMax, ReconstructEntries() );

			for ( int i = 0; bod != null && i < bod.Entries.Length; ++i )
				bod.Entries[i].Owner = bod;

			return bod;
		}

		private LargeMobileBulkEntry[] ReconstructEntries()
		{
			LargeMobileBulkEntry[] entries = new LargeMobileBulkEntry[m_Entries.Length];

			for ( int i = 0; i < m_Entries.Length; ++i )
			{
				entries[i] = new LargeMobileBulkEntry( null, new SmallMobileBulkEntry( m_Entries[i].Type, m_Entries[i].AnimalName, m_Entries[i].Graphic ) );
				entries[i].Amount = m_Entries[i].AmountCur;
			}

			return entries;
		}

		public TamingBOBLargeEntry( LargeMobileBOD bod )
		{
			m_AmountMax = bod.AmountMax;

			m_Entries = new TamingBOBLargeSubEntry[bod.Entries.Length];

			for ( int i = 0; i < m_Entries.Length; ++i )
				m_Entries[i] = new TamingBOBLargeSubEntry( bod.Entries[i] );
		}

		public TamingBOBLargeEntry( GenericReader reader )
		{
			int version = reader.ReadEncodedInt();

			switch ( version )
			{
				case 0:
				{
					m_AmountMax = reader.ReadEncodedInt();
					m_Price = reader.ReadEncodedInt();

					m_Entries = new TamingBOBLargeSubEntry[reader.ReadEncodedInt()];

					for ( int i = 0; i < m_Entries.Length; ++i )
						m_Entries[i] = new TamingBOBLargeSubEntry( reader );

					break;
				}
			}
		}

		public void Serialize( GenericWriter writer )
		{
			writer.WriteEncodedInt( 0 ); // version

			writer.WriteEncodedInt( (int) m_AmountMax );
			writer.WriteEncodedInt( (int) m_Price );

			writer.WriteEncodedInt( (int) m_Entries.Length );

			for ( int i = 0; i < m_Entries.Length; ++i )
				m_Entries[i].Serialize( writer );
		}
	}
}