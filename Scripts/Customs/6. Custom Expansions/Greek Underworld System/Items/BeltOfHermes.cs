using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class BeltOfHermes : Item
	{
		[Constructable]
		public BeltOfHermes() : base( 10128 )
		{
			Name = "Belt of Hermes";
			Weight = 0.1;
			Hue = 1150;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.IsChildOf( from.Backpack ))
			{
				from.MoveToWorld( new Point3D( 1435, 1698, 0 ), Map.Felucca );
				from.SendMessage( 1150, "The Belt transports you from the Underworld." );
				this.Delete();
			}
		}		

		public BeltOfHermes( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}