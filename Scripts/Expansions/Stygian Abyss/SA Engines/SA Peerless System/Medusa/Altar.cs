using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	public class MedusaAltar : PeerlessAltar
	{		
		private int m_ID;
	
		public override int KeyCount{ get{ return 3; } }
		public override MasterKey MasterKey{ get{ return new MedusaKey(); } }
		
		public override Type[] Keys{ get{ return new Type[]
		{
			typeof( RareSerpentEgg1 ), typeof( RareSerpentEgg2 ), typeof( RareSerpentEgg3 ), 
			typeof( RareSerpentEgg4 )
		}; }}
		
		public override BasePeerless Boss{ get{ return new Medusa(); } }		
	
		[Constructable]
		public MedusaAltar() : base( 0x2206 )
		{
			Visible = false;
				
			BossLocation = new Point3D( 818, 927, -15 );
			TeleportDest = new Point3D( 771, 928, 0 );
			ExitDest = new Point3D( 529, 921, -1 );
			
			m_ID = 0;
		}
	
		public MedusaAltar( Serial serial ) : base( serial )
		{
		}	
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_ID );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_ID = reader.ReadInt();
		}
		
		public int GetID()
		{
			int id = m_ID;
			m_ID += 1;
			return id;
		}
		
		public bool TryDrop( Mobile from, Item item, int id )
		{
			if ( id >= 0 && id < Keys.Length && item != null )
			{
				if ( item.GetType() == Keys[ id ] )
					return OnDragDrop( from, item );
			}
			
			return false;
		}
	}
	
	public class MedusaNest : Container
	{
		private MedusaAltar m_Altar;
		private int m_ID;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public MedusaAltar Altar
		{
			get{ return m_Altar; }
			set{ m_Altar = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int ID
		{
			get{ return m_ID; }
			set{ m_ID = value; }
		}
		
		public MedusaNest( MedusaAltar altar, int hue ) : base( 0x207B )
		{
			Hue = 2416;
			Movable = false;
		
			m_Altar = altar;
			
			if ( m_Altar != null )
				m_ID = m_Altar.GetID();
		}	
		
		public MedusaNest( Serial serial ) : base( serial )
		{
		}
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{								
			if ( m_Altar == null )
				return false;
											
			if ( m_Altar.Activated )
			{					
				from.SendLocalizedMessage( 1075213 ); // The master of this realm has already been summoned and is engaged in combat.  Your opportunity will come after he has squashed the current batch of intruders!
				return false;
			}
			
			if ( !m_Altar.TryDrop( from, dropped, m_ID ) )
			{
				from.SendLocalizedMessage( 1072682 ); // This is not the proper key.
				return false;
			}
			else
				return true;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_ID );
			writer.Write( (Item) m_Altar );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_ID = reader.ReadInt();
			m_Altar = reader.ReadItem() as MedusaAltar;
		}
	}
}