using System; 
using Server;
using Server.Gumps;
using System.Collections;

namespace Server.Items
{ 
	public class CivicSign : Item 
	{
		private ArrayList m_toDelete;
		private CityManagementStone m_Stone;
		private CivicSignType m_Type;
		private Mobile m_LandlordRemove;
		
		public Mobile LandlordRemove
		{
			get{ return m_LandlordRemove; }
			set{ m_LandlordRemove = value; }
		}

		public ArrayList toDelete
		{
			get{ return m_toDelete; }
			set{ m_toDelete = value; }
		}

		public CityManagementStone Stone
		{
			get{ return m_Stone; }
			set{ m_Stone = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CivicSignType Type
		{
			get{ return m_Type; }
			set{ m_Type = value; }
		}

		public CivicSign() : base( 3023 ) 
		{ 
			Movable = false; 
			Name = "a civic building sign"; 
		} 

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( from == m_Stone.Mayor && from.InRange( this.GetWorldLocation(), 5 ) )
				from.SendGump( new DestoryCityStructureGump( this, from ) );
			else
				from.SendMessage( "You cannot access that." );
		} 

		public CivicSign( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void OnDelete()
		{
			if ( toDelete != null ) // Delete all items needed
			{
				foreach( Item i in toDelete )
				{
					i.Delete();
				}
			}

			if ( Type == CivicSignType.Bank )
			{
				m_Stone.HasBank = false;
			}
			else if ( Type == CivicSignType.Stable )
			{
				m_Stone.HasStable = false;
			}
			else if ( Type == CivicSignType.Tavern )
			{
				m_Stone.HasTavern = false;
			}
			else if ( Type == CivicSignType.Healer )
			{
				m_Stone.HasHealer = false;
			}
			else if ( Type == CivicSignType.Moongate )
			{
				m_Stone.HasMoongate = false;
			}
			else if ( Type == CivicSignType.Garden )
			{
				if ( m_Stone.Gardens.Contains( this ) )
					m_Stone.Gardens.Remove( this );
			}
			else if ( Type == CivicSignType.Park )
			{
				if ( m_Stone.Parks.Contains( this ) )
					m_Stone.Parks.Remove( this );
			}
			else if ( Type == CivicSignType.Market )
			{
				m_Stone.HasMarket = false;
				m_LandlordRemove.Delete();
			}

			base.OnDelete();
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 1 ); // version 

			writer.Write( m_LandlordRemove );
			writer.WriteItemList( m_toDelete, true );

			writer.Write( m_Stone );
			writer.Write( (int) m_Type );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
					{
						m_LandlordRemove = reader.ReadMobile();
						goto case 0;
					}
				
				
				case 0:
				{
					m_toDelete = reader.ReadItemList();
					m_Stone = (CityManagementStone)reader.ReadItem();
					m_Type = (CivicSignType)reader.ReadInt();

					break;
				}
			}
		} 
	} 
} 
