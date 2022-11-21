using System;

namespace Server.Engines.BulkOrders
{
	public class TamingBOBSmallEntry
	{
		private Type m_Type;
		private int m_AmountCur, m_AmountMax;
		private string m_AnimalName;
		private int m_Graphic;
		private int m_Price;

		public Type Type{ get{ return m_Type; } }
		public int AmountCur{ get{ return m_AmountCur; } }
		public int AmountMax{ get{ return m_AmountMax; } }
		public string AnimalName{ get{ return m_AnimalName; } }
		public int Graphic{ get{ return m_Graphic; } }
		public int Price{ get{ return m_Price; } set{ m_Price = value; } }

		public Item Reconstruct()
		{
			SmallMobileBOD bod = null;

			bod = new SmallTamingBOD( m_AmountCur, m_AmountMax, m_Type, m_AnimalName, m_Graphic );

			return bod;
		}

		public TamingBOBSmallEntry( SmallMobileBOD bod )
		{
			m_Type = bod.Type;

			m_AmountCur = bod.AmountCur;
			m_AmountMax = bod.AmountMax;
			m_AnimalName = bod.AnimalName;
			m_Graphic = bod.Graphic;
		}

		public TamingBOBSmallEntry( GenericReader reader )
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
					m_AmountMax = reader.ReadEncodedInt();
					m_AnimalName = reader.ReadString();
					m_Graphic = reader.ReadEncodedInt();
					m_Price = reader.ReadEncodedInt();

					break;
				}
			}
		}

		public void Serialize( GenericWriter writer )
		{
			writer.WriteEncodedInt( 0 ); // version

			writer.Write( m_Type == null ? null : m_Type.FullName );

			writer.WriteEncodedInt( (int) m_AmountCur );
			writer.WriteEncodedInt( (int) m_AmountMax );
			writer.Write( (string) m_AnimalName );
			writer.WriteEncodedInt( (int) m_Graphic );
			writer.WriteEncodedInt( (int) m_Price );
		}
	}
}