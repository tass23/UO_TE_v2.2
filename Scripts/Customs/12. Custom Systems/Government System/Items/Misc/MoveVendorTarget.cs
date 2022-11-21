using System;
using Server;
using Server.Targeting;
using Server.Commands;
using Server.Commands.Generic;
using Server.Regions;
using Server.Mobiles;

namespace Server.Targets
{
	public class MoveVendorTarget : Target
	{
		private object m_Object;

		public MoveVendorTarget( object o ) : base( -1, true, TargetFlags.None )
		{
			m_Object = o;
		}

		protected override void OnTarget( Mobile from, object o )
		{
			IPoint3D p = o as IPoint3D;
			PlayerMobile pm = (PlayerMobile)from;
			bool market = false; 
			

			if ( p != null )
			{
				

				if ( p is Item )
					p = ((Item)p).GetWorldTop();          

				CommandLogging.WriteLine( from, "{0} {1} moving {2} to {3}", from.AccessLevel, CommandLogging.Format( from ), CommandLogging.Format( m_Object ), new Point3D( p ) );

				Point3D pt = new Point3D( p.X, p.Y, p.Z );
				Region reg = Region.Find( pt, from.Map );
				
				
				if ( reg is CityMarketRegion )
					
					if ( ((CityMarketRegion)reg).Stone == pm.City )
					market = true;
				
					
				
				if ( ( reg is PlayerCityRegion && reg == pm.City.PCRegion ) || market )
				{
					
					if ( m_Object is Item )
					{
						Item item = (Item)m_Object;

						if ( !item.Deleted )
							item.MoveToWorld( new Point3D( p ), from.Map );
					}
					else if ( m_Object is Mobile )
					{
						Mobile m = (Mobile)m_Object;
						
						if ( m is CityRentedVendor && reg is PlayerCityRegion )
							from.SendMessage( "You may not move rented vendors outside the mall!" );

						else if ( !m.Deleted  )
							m.MoveToWorld( new Point3D( p ), from.Map );
					}
				
				}
				else
					from.SendMessage( "You may only place this in a public area of your town!" );
			}
		}
	}
}
