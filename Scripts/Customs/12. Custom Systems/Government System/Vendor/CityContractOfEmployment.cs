using System;
using Server;
using Server.Mobiles;
using Server.Multis;
using Server.Regions;

namespace Server.Items
{
	public class CityContractOfEmployment : Item
	{
		
		[Constructable]
		public CityContractOfEmployment() : base( 0x14F0 )
		{
			Weight = 1.0;
			Name = "A City Vendor Contract";
			
		}

		public CityContractOfEmployment( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				PlayerMobile pm = (PlayerMobile)from;
				CityManagementStone stone = (CityManagementStone)pm.City;
				
				
				if ( stone == null )
					from.SendMessage( "You must be a citizen of a city to use this!" );
				else
				{
					Region r = stone.PCRegion;
					
					if ( PlayerGovernmentSystem.CheckIfHouseInCity( from, r ) );
					{
						if ( from.Region is HouseRegion )
						{
							BaseHouse house = BaseHouse.FindHouseAt( from );
							if ( house.Owner != from )
								from.SendMessage( "You may not place vendors in a house you do not own!" );
							else
							{
								Mobile v = new CityPlayerVendor( from, stone );
								stone.Vendors.Add( v );
								CityPlayerVendor vend = (CityPlayerVendor)v;
								vend.TaxRate = stone.IncomeTax;
								vend.OriginalStone = stone;
								v.Direction = from.Direction & Direction.Mask;
								v.MoveToWorld( from.Location, from.Map );

								v.SayTo( from, 503246 ); // Ah! it feels good to be working again.

								this.Delete();
							}
						}
						
						else
						{
							bool market = false;
							if ( from.Region is CityMarketRegion )
							{
								CityMarketRegion cr = (CityMarketRegion)from.Region;
								if ( cr.Stone == stone )
									market = true;								
							}
							
							if ( PlayerGovernmentSystem.IsAtCity( from ) || market  )
							{
								Mobile v = new CityPlayerVendor( from, stone );
								stone.Vendors.Add( v );
								CityPlayerVendor vend = (CityPlayerVendor)v;
								vend.OriginalStone = stone;
								vend.TaxRate = stone.IncomeTax;
								v.Direction = from.Direction & Direction.Mask;
								v.MoveToWorld( from.Location, from.Map );

								v.SayTo( from, 503246 ); // Ah! it feels good to be working again.

								this.Delete();
							}
							else
								from.SendMessage( "You may only do this in a city you are a member of!" );
						}
					}
					
				}
				
			}
			
		}
	}
}
