using System;
using Server;
using System.Collections;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;

namespace Server.Misc
{

	public class CityVendorDismiss : Timer
	{
		private CityPlayerVendor m_vendor;
		private Mobile m_owner;
		private DateTime m_Expire;
		
		
		public CityVendorDismiss( CityPlayerVendor vend, DateTime expire ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.5 ) )
			{
				m_vendor = vend;
				m_Expire = expire;
				//m_Expire = DateTime.Now + TimeSpan.FromSeconds( 10.0 );  Left in for Testing purposes.
				m_owner = vend.Owner;
				
			}
		
		public bool CheckVendorExist( Mobile vend )
		{
			if ( World.FindMobile( vend.Serial ) != null )
				return true;
			else
				return false;
			
		}
		
		
		
		protected override void OnTick()
			{
				if ( DateTime.Now >= m_Expire )
				{
					if ( m_vendor == null || !CheckVendorExist( m_vendor ) )
						Stop();
					
					else
					{
					
						Container pack = m_vendor.Backpack;
						if ( pack != null && pack.Items.Count > 0 )
						{
							BankBox box = m_owner.BankBox;
							List<Item> list = new List<Item>();
							list = pack.Items;
							int number = pack.Items.Count;
							for ( int i = 0; i < number; i++ )
							{
								/*if ( (Item)list[i] is Container )
								{
									
								}*/
								
								if ( box.TryDropItem( m_owner, (Item)list[i], false ) )
									continue;
								else
									//list.Remove( list[i] );
									continue;
								
								
							}
							m_vendor.Dismiss( m_owner );
							Stop();
						}
						else
						{
							m_vendor.Dismiss( m_owner );
							Stop();
						}
					}
				}
			}
	
	
	
	
	
	}
	
	
	
}
