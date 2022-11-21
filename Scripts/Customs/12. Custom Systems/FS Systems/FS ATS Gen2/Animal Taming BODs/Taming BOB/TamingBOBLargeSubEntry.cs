using System;

namespace Server.Engines.BulkOrders
{
	public class TamingBOBLargeSubEntry
	{
		private Type m_Type;
		private int m_AmountCur;
		private string m_AnimalName;
		private int m_Graphic;

		public Type Type{ get{ return m_Type; } }
		public int AmountCur{ get{ return m_AmountCur; } }
		public string AnimalName{ get{ return m_AnimalName; } }
		public int Graphic{ get{ return m_Graphic; } }

		public TamingBOBLargeSubEntry( LargeMobileBulkEntry lmbe )
		{
			m_Type = lmbe.Details.Type;
			m_AmountCur = lmbe.Amount;
			m_AnimalName = lmbe.Details.AnimalName;
			m_Graphic = lmbe.Details.Graphic;
		}

		public TamingBOBLargeSubEntry( GenericReader reader )
		{
			int version = reader.ReadEncodedInt();

			switch ( version )
			{
				case 0:
				{
					string type = reader.ReadString();

					if ( type != null )
						m_Type = ScriptCompiler.FindTypeByFullName( type );

					m_AmountCur = reader.ReadEncodedInt();
					m_AnimalName = reader.ReadString();
					m_Graphic = reader.ReadEncodedInt();

					break;
				}
			}
		}

		public void Serialize( GenericWriter writer )
		{
			writer.WriteEncodedInt( 0 ); // version

			writer.Write( m_Type == null ? null : m_Type.FullName );

			writer.WriteEncodedInt( (int) m_AmountCur );
			writer.Write( (string) m_AnimalName );
			writer.WriteEncodedInt( (int) m_Graphic );
		}
	}
}