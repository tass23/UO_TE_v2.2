using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class AtlantisTalisman : Item
	{
		[Constructable]
		public AtlantisTalisman() : base( 3271 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
			Hue = 2767;
			Name = "an ancient talisman";
		}

		public AtlantisTalisman( Serial serial ) : base( serial )
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
				from.SendMessage( "A strange portal appears, but you are not sure where it leads." ); // Your godly powers allow you to place this vendor whereever you wish.
				AtlantisTalismanGate v = new AtlantisTalismanGate();
				v.Direction = from.Direction & Direction.Mask;
				v.MoveToWorld( from.Location, from.Map );
				this.Delete();
			}
		}
	}
}