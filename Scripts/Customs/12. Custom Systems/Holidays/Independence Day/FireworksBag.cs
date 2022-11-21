using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class FireworksBag : Bag
	{
		[Constructable]
        public FireworksBag() : this (1)
		{
            Name = "a bag of fireworks";
            Hue = Utility.RandomList( 2, 3, 37, 38, 1150, 1153 );
        }

		[Constructable]
       	public FireworksBag( int amount )
        {
            DropItem(new BlackPearlFirework());
			DropItem(new BloodMossFirework());
            DropItem(new GarlicFirework());
			DropItem(new GinsengFirework());
            DropItem(new MandrakeRootFirework());
			DropItem(new NightshadeFirework());
            DropItem(new SpiderSilkFirework());
			DropItem(new SulfurousAshFirework());
		}

        public FireworksBag(Serial serial) : base(serial)
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Independence Day" );
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