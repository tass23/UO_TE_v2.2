using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class WerewolfTome : Item
	{

		[Constructable]
		public WerewolfTome() : base( 0x0A92 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
			Hue = 1194;
			Name = "an ancient, moldy tome with large claw marks on the cover";
		}

		public WerewolfTome( Serial serial ) : base( serial )
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
				from.SendMessage( "A strange portal appears, but you're unsure where it goes." ); // Your godly powers allow you to place this vendor whereever you wish.

				WerewolfGate v = new WerewolfGate();

				v.Direction = from.Direction & Direction.Mask;
				v.MoveToWorld( from.Location, from.Map );

				this.Delete();
			}
		}
	}
}