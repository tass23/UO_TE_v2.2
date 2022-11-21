using System;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.Items
{
	public class ResStone
	{
		public static void GetContextMenuEntries( Mobile from, Item item, List<ContextMenuEntry> list )
		{
			if ( from is PlayerMobile )
				list.Add( new CityLockKarmaEntry( (PlayerMobile)from ) );
	
			list.Add( new CityResurrectEntry( from, item ) );

			if ( Core.AOS )
				list.Add( new CityTitheEntry( from ) );
		}

		private class CityResurrectEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Item m_Item;

			public CityResurrectEntry( Mobile mobile, Item item ) : base( 6195, 2 )
			{
				m_Mobile = mobile;
				m_Item = item;

				Enabled = !m_Mobile.Alive;
			}

			public override void OnClick()
			{
				CityResurrectionStone resstone = (CityResurrectionStone)m_Item;

				if ( resstone.Sign.Stone.ResFee != 0 )
					m_Mobile.SendGump( new CityResFeeGump( resstone ) );
				else
					Ankhs.Resurrect( m_Mobile, m_Item );	
			}
		}

		private class CityLockKarmaEntry : ContextMenuEntry
		{
			private PlayerMobile m_Mobile;
	
			public CityLockKarmaEntry( PlayerMobile mobile ) : base( mobile.KarmaLocked ? 6197 : 6196, 2 )
			{
				m_Mobile = mobile;
			}

			public override void OnClick()
			{
				m_Mobile.KarmaLocked = !m_Mobile.KarmaLocked;
	
				if ( m_Mobile.KarmaLocked )
					m_Mobile.SendLocalizedMessage( 1060192 ); // Your karma has been locked. Your karma can no longer be raised.
				else
					m_Mobile.SendLocalizedMessage( 1060191 ); // Your karma has been unlocked. Your karma can be raised again.
			}
		}

		private class CityTitheEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;

			public CityTitheEntry( Mobile mobile ) : base( 6198, 2 )
			{
				m_Mobile = mobile;

				Enabled = m_Mobile.Alive;
			}

			public override void OnClick()
			{
				if ( m_Mobile.CheckAlive() )
					m_Mobile.SendGump( new TithingGump( m_Mobile, 0 ) );
			}
		}

		private class TitheEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;

			public TitheEntry( Mobile mobile ) : base( 6198, 2 )
			{
				m_Mobile = mobile;

				Enabled = m_Mobile.Alive;
			}

			public override void OnClick()
			{
				if ( m_Mobile.CheckAlive() )
					m_Mobile.SendGump( new TithingGump( m_Mobile, 0 ) );
			}
		}
	}

	public class CityResurrectionStone : Item
	{
		private CivicSign m_Sign;
		private Hashtable m_ghosts;
		
		public Hashtable Ghosts 
		{
			get { return m_ghosts; } 
			set { m_ghosts = value; }
		}
		
		public CivicSign Sign
		{
			get{ return m_Sign; }
			set{ m_Sign = value; }
		}

		[Constructable]
		public CityResurrectionStone() : base( 0xED4 )
		{
			Movable = false;
			Name = "city resurrection stone";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) ) 
			{
				PlayerMobile pm = (PlayerMobile)from;
				if ( m_Sign.Stone == pm.City )
				{
								
					if ( m_Sign != null )
					{
												
						if ( m_Sign.Stone.CorpseFee != 0 )
						{
							from.SendGump( new CityCorpseFeeGump( this ) );
						}
						else
						{
													
							Item corpse = from.Corpse;
	
	                      	if ( corpse != null )
							{
	                      		if ( RegCorpse( from ) )
	                      		{                      			
	                      	  		corpse.MoveToWorld( from.Location, from.Map );
									from.SendMessage( "Your corpse has been found." );
									Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 10, 30, 5052 );
									Effects.PlaySound( from.Location, from.Map, 0x201 );
	                      		}
	                      		else
	                      			from.SendMessage( "You may only use this 3 times a week" );
							}
							else
							{
								from.SendMessage( "Your corpse could not be located." );
							}
						}
					}
					else
					{
						from.SendMessage( 53, "WARNING! Stone not linked to building sign. Contact a game master." );
					}
				
				}
				else
				{
					from.SendMessage( "You must be a member of the city for corpse retrieval." );
				}
						
			}
			else
			{
				from.SendMessage( "You are not close enough to use this.");
			}
			
		}
			

		public override void OnDoubleClickDead( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) ) 
			{
				if ( m_Sign != null )
				{
					if ( m_Sign.Stone.ResFee != 0 )
						from.SendGump( new CityResFeeGump( this ) );
					else
						Ankhs.Resurrect( from, this );
				}
				else
				{
					from.SendMessage( 53, "WARNING! Stone not linked to building sign. Contact a game master." );
				}	
			}
			else
			{
				from.SendMessage( "You are not close enough to use this.");
			}
		}

		public CityResurrectionStone( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			ResStone.GetContextMenuEntries( from, this, list );
		}

		public bool RegCorpse( Mobile from )
		{
						
				if ( m_ghosts == null )
				{
			   	 m_ghosts = new Hashtable();
			   	 m_ghosts.Add( from, 1 );
			   	 from.SendMessage( "You have used 1 out of 3 weekly corpse retrievals" );
			   	 return true;			    	
				}
				else if ( m_ghosts.ContainsKey( from ) )
				{
					if ( (int)m_ghosts[from] < 3 )
					{
						m_ghosts[from] = (int)m_ghosts[from] + 1;
						from.SendMessage( "You have used {0} out of 3 weekly corpse retrievals", (int)m_ghosts[from] );
						return true;
					}
					else
						return false;
				
				}
				else 
				{
					m_ghosts.Add( from, 1 );
					from.SendMessage( "You have used 1 out of 3 weekly corpse retrievals" );
					return true;
				}
			
		}
		
		public void ClearGhosts()
		{
			if ( m_ghosts == null )
				return;
			else
			{
				m_ghosts.Clear();
			}
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			
						
			writer.Write( (int) m_ghosts.Count );
			foreach( DictionaryEntry de in m_ghosts )
			{
				writer.Write( (Mobile)de.Key);
				writer.Write( (int)de.Value );
			}
			
			writer.Write( m_Sign );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_ghosts = new Hashtable();
			
			switch ( version )
			{
				case 1:
					{
						int count = reader.ReadInt();
						for (int i=0; i < count; i++)
						{
							Mobile mob = reader.ReadMobile();
							if ( mob == null )
							{
								int bad = reader.ReadInt();
								continue;
							}
							m_ghosts.Add( mob, reader.ReadInt() );
						}
						goto case 0;
					}
				case 0:
					{
						m_Sign = (CivicSign)reader.ReadItem();
						break;
					}
			}
		
			
		}
	}
}
