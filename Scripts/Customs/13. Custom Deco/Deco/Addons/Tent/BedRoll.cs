using System; 
using Server; 
using Server.Items; 
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Items 
{ 
	public class TentDestroyer : BaseAddon 
	{ 
		[Constructable] 
		public TentDestroyer( TentWalls tentwalls,TentRoof tentroof,TentFloor tentfloor,TentTrim tenttrim, PlayerMobile player, SecureTent chest) :
		this ( tentwalls, tentroof, tentfloor, tenttrim, player, chest, null ) 
		{ 
		}
	
		[Constructable] 
		public TentDestroyer( TentWalls tentwalls,TentRoof tentroof,TentFloor tentfloor,TentTrim tenttrim, PlayerMobile player, SecureTent chest, TentVerifier tentverifier) 
		{ 
			Name = "A tent carrying bag"; 
			m_Player = player; 
			m_TentRoof = tentroof; 
			m_TentWalls = tentwalls; 
			m_TentFloor = tentfloor; 
			m_TentTrim = tenttrim; 
			m_Chest = chest;
			if ( tentverifier != null )
			{
				m_TentVerifier = tentverifier;
			}
			this.ItemID = 2648; // 2645;
			this.Visible = true; 
			Hue = 277; // 1072;
		}
		private TentRoof m_TentRoof; 
		private TentWalls m_TentWalls; 
		private TentTrim m_TentTrim; 
		private TentFloor m_TentFloor; 
		private PlayerMobile m_Player; 
		private SecureTent m_Chest; 
		private TentVerifier m_TentVerifier; 

		public PlayerMobile Player
		{
			get{ return m_Player; }
		}
		
		public override void OnDoubleClick( Mobile from ) 
		{ 
			if (m_Player==from)
			{
				if ( m_Chest != null && m_Chest.Items.Count > 0 )
				{
					from.SendMessage( "You must remove the items from the travel bag before packing up your tent." );
				} 
				else 
				{ 
					from.SendGump (new TentDGump(this,from));
				}
			} 
			else 
			{ 
				from.SendMessage( "You don't appear to own this Tent." ); 
			} 
		} 

		public override void OnDelete() 
		{ 
			if ( m_TentFloor != null ) // m_TentFloor
			{
				m_TentFloor.Delete();
			}
			else
			{
				Console.WriteLine("m_TentFloor was null");
			}
			
			if ( m_TentTrim != null ) // m_TentTrim
			{
				m_TentTrim.Delete();
			}
			else
			{
				Console.WriteLine("m_TentTrim was null");
			}
			
			if ( m_TentWalls != null ) // m_TentWalls
			{
				m_TentWalls.Delete();
			}
			else
			{
				Console.WriteLine("m_TentWalls was null");
			}
			
			if ( m_TentRoof != null )  // m_TentRoof
			{
				m_TentRoof.Delete();
			}
			else
			{
				Console.WriteLine("m_TentRoof was null");
			}
			
			if ( m_Chest != null ) // m_Chest
			{
				m_Chest.Delete();
			}
			else
			{
				Console.WriteLine("m_Chest was null");
			}
			
			if ( m_TentVerifier != null ) // m_TentVerifier
			{
				m_TentVerifier.Delete();
			}
			else
			{
			//	Console.WriteLine("m_TentVerifier was null - this is okay to see if you had tent verifiers before. Comment this line out after all are cleared.");
			}		
			
		} 


		public TentDestroyer( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
			writer.Write( m_TentTrim ); 
			writer.Write( m_TentFloor ); 
			writer.Write( m_TentWalls ); 
			writer.Write( m_TentRoof ); 
			writer.Write( m_Player ); 
			writer.Write( m_Chest );
	//		writer.Write( m_TentVerifier );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 

			m_TentTrim = (TentTrim)reader.ReadItem(); 
			m_TentFloor = (TentFloor)reader.ReadItem(); 
			m_TentWalls = (TentWalls)reader.ReadItem(); 
			m_TentRoof = (TentRoof)reader.ReadItem(); 
			m_Player = (PlayerMobile)reader.ReadMobile(); 
			m_Chest = (SecureTent)reader.ReadItem();
			if ( version == 0 )
			{
				m_TentVerifier = (TentVerifier)reader.ReadItem();
			}
		} 

	} 
}
