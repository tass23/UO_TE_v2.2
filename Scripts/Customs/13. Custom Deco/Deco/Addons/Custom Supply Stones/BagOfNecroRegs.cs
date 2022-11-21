using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BagOfNecroRegs : Bag
	{
		[Constructable]
		public BagOfNecroRegs() : this( 25 )
		{
			Hue = 0x386;
		}

		[Constructable]
		public BagOfNecroRegs( int amount )
		{
			DropItem( new BatWing    ( amount ) );
			DropItem( new GraveDust  ( amount ) );
			DropItem( new DaemonBlood( amount ) );
			DropItem( new NoxCrystal ( amount ) );
			DropItem( new PigIron    ( amount ) );
		}

		public BagOfNecroRegs( Serial serial ) : base( serial )
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
	}
}