using System;
using System.Collections;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Regions
{
	public class ElectricFloor : DamagingRegion
	{		
		public override TimeSpan DamageInterval{ get{ return TimeSpan.FromSeconds( 3 ); } }
		
		public ElectricFloor( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{			
		}	

		public override void Damage( Mobile m )
		{
			base.Damage( m );

			if ( m.Alive )
			{
				m.FixedParticles( 0x374A, 1, 14, 0x26BB, 0x3F, 0x7, EffectLayer.Waist );
				m.PlaySound( 0x210 );
				int damage = 0;
				damage += (int) Math.Pow( m.Location.X - 165, 0.5 );				
				damage += (int) Math.Pow( m.Location.Y - 340, 0.5 );	

				if ( damage > 20 )
					m.SendMessage( "The electrical field is much stronger here. You realize that continuing to stand on it will surely kill you." );
				else if ( damage > 10 )
					m.SendMessage( "The electrical field has grown in strength." ); // The acid river has gotten deeper. The concentration of acid is significantly stronger.
				else
					m.SendMessage( "The electrical field on the floor is frying your skin." ); // The acid river burns your skin.

				AOS.Damage( m, damage, 0, 0, 0, 0, 100 );
			}
		}	
	}
}