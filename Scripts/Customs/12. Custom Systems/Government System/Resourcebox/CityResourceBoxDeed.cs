using System;
using Server;
using Server.Mobiles;
using Server.Regions;
using Server.Targeting;
using System.Collections;

namespace Server.Items
{

	public class CityResourceBoxDeed : Item
	{
		
		
		[Constructable]
		public CityResourceBoxDeed() : base ( 0x14F0 )
		{
			Name = "a town resource box deed";
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				PlayerMobile pm = (PlayerMobile)from; 
				bool ismayor = false;
				bool isvalid = false;
				if ( pm.City != null && pm.City.Mayor == pm ) 
					ismayor = true;
				if ( PlayerGovernmentSystem.IsAtCity( from ) )
					isvalid = true;
				if ( ismayor && isvalid )
					from.Target = new CityBoxTarget( from, this );
				else
				{
					if ( !ismayor )
						from.SendMessage( "Only the Mayor may place this in their town" );
					else
						from.SendMessage( "You may only place this in city buldings or public areas.");	
				}
			}
				
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			
		}
		public CityResourceBoxDeed( Serial serial ) : base( serial )
		{
			
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
		
		
		private class CityBoxTarget : Target
		{
			private Mobile m_mob;
			private CityResourceBoxDeed m_deed;
			
			public CityBoxTarget( Mobile mob, CityResourceBoxDeed deed ) : base( 5, true, TargetFlags.None )
			{
				m_mob = mob;
				m_deed = deed;
				
			}
			
			protected override void OnTarget( Mobile from, object target ) 
			{
				
				IPoint3D pi = target as IPoint3D;
				Map map = from.Map;
				Region reg;
				Point3D p;
				PlayerMobile pm = (PlayerMobile)from;
				CityManagementStone stone = pm.City;
				
				ArrayList decore = stone.isLockedDown;
				ArrayList delete = stone.toDelete;
				
				if ( stone.CurrentDecore == stone.MaxDecore )
				{
					from.SendMessage( "You cannot secure anymore items in this city." );
					return;
				}
				
				if ( decore == null )
				{
						stone.isLockedDown = new ArrayList();
						decore = stone.isLockedDown;
				}

				
				if ( pi == null || map == null || m_deed.Deleted )
					return;
				
								
				Server.Spells.SpellHelper.GetSurfaceTop( ref pi );
				p = new Point3D( pi.X, pi.Y, pi.Z );
				
				reg = Region.Find( p, map );
				
				if ( from.Region is PlayerCityRegion && reg == from.Region && PlayerGovernmentSystem.IsAtCity( from ) )
				{
					CityResourceBox box = new CityResourceBox();
					box.Stone = stone;
					box.MoveToWorld( new Point3D( p ), map );
					box.Movable = false;
					decore.Add( box );
					delete.Add( box );
					stone.CurrentDecore += 1;
					m_deed.Delete();
				}
				else
					from.SendMessage( "You must be in your city to do this." );
				
			}
			
		}
		
		
	}
	
	
}
