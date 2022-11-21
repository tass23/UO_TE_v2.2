using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class FSDet : Item
	{
		[Constructable]
		public FSDet() : base( 0x108F )
		{
			Weight = 10;
			Name = "a Fireworks Detonator";
		}

		public FSDet( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from)
		{
			foreach ( Item item in World.Items.Values ) 
			{ 
				if (item is FireStand) 
				{ 
					FireStand fs = (FireStand)item;
					fs.BeginLaunch();
				} 
			} 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}