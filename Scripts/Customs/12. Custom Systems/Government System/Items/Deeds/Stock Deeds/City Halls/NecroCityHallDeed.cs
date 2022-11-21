using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	public class NecroCityHallDeed : CityDeed
	{
		[Constructable]
		public NecroCityHallDeed() : base( 0x1454, new Point3D( 0, 8, 0 ) )
		{
			Name = "a necro city hall deed";
			Type = CivicStrutureType.NecroCityHall;
		}

		public NecroCityHallDeed( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( PlayerGovernmentSystem.CheckIfMayor( from ) )
			{
				from.SendMessage( "You are already a mayor of another town, You cannot place another city hall." );
			}
			else if ( PlayerGovernmentSystem.CheckIfCitizen( from ) )
			{
				from.SendMessage( "You are already a member of another city, You must leave that city first." );
			}
			else if ( PlayerGovernmentSystem.CheckMapCityLimit( from ) )
			{
				from.SendMessage( "There can not be anymore cities on this facet.");
			}
			else
			{
				base.OnDoubleClick( from );
			}
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
	}
}