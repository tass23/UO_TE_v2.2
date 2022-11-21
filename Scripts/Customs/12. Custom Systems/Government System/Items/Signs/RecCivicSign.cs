using System;
using Server;
using Server.Items;

namespace Server.Items
{

	public class RecCivicSign : CivicSign
	{
		
		private int m_level;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int CityLevel
		{
			get{ return m_level; }
			set{ m_level = value; }
			
		}
		
		public RecCivicSign() : base()
		{
			
		}
		
		public RecCivicSign( Serial serial ) : base( serial )
		{
			
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( m_level );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_level = reader.ReadInt();
		}
		
		
	}
	
}
