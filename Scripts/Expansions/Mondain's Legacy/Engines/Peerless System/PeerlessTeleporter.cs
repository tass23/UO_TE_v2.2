using System;
using Server;
using Server.Gumps;

namespace Server.Items
{
	public class PeerlessTeleporter : Teleporter
	{		
		private PeerlessAltar m_Altar;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public PeerlessAltar Altar
		{
			get{ return m_Altar; }
			set{ m_Altar = value; }
		}
		
		[Constructable]
		public PeerlessTeleporter( ) : this( null )
		{
		}
		
		public PeerlessTeleporter( PeerlessAltar altar ) : base()
		{
			m_Altar = altar;
		}
	
		public PeerlessTeleporter( Serial serial ) : base( serial )
		{
		}	
		
		public override bool OnMoveOver( Mobile m )
		{			
			if ( m.Alive )
			{
				m.CloseGump( typeof( ConfirmExitGump ) );
				m.SendGump( new ConfirmExitGump( m_Altar ) );
			}
			else if ( m_Altar != null )
			{
				m_Altar.Exit( m );
				return false;
			}
			
			return true;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (Item) m_Altar );
			
			if ( m_Altar != null && m_Altar.Map != Map )
				Map = m_Altar.Map;
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_Altar = reader.ReadItem() as PeerlessAltar;
		}
	}
}