using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class AtlantisMap : Item
	{
		[Constructable]
		public AtlantisMap() : base( 5355 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
			Hue = 2984;
			Name = "a copy of a map to Atlantis";
		}

		public AtlantisMap( Serial serial ) : base( serial )
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
				from.SendMessage( "A strange portal appears, but you are not sure where it leads." );
				AtlantisGate v = new AtlantisGate();
				v.Direction = from.Direction & Direction.Mask;
				v.MoveToWorld( from.Location, from.Map );
				this.Delete();
			}
		}
	}
}