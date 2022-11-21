using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
	public class DoomSwitch : Item
	{
		[Constructable]
		public DoomSwitch() : base( 324 )
		{
			Name = "an unknown switch";
			Weight = 0.1;
			Hue = 1195;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.IsChildOf( from.Backpack ))
			{
				from.MoveToWorld( new Point3D( 467, 96, -1 ), Map.Malas );
				from.SendMessage( 1150, "Activating the switch has teleported you to the Secret Room." );
				this.Delete();
			}
		}

		public DoomSwitch( Serial serial ) : base( serial )
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