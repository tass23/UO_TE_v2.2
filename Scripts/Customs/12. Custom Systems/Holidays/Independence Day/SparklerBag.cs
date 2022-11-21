using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class SparklerBag : Bag
	{
		[Constructable]
        public SparklerBag() : this (1)
		{
            Name = "a bag of sparklers";
            Hue = Utility.RandomList( 2, 3, 37, 38, 1150, 1153 );
        }

		[Constructable]
       	public SparklerBag( int amount )
        {
            DropItem(new BlackPearlLargeSparkler());
			DropItem(new BlackPearlSmallSparkler());
            DropItem(new BloodMossLargeSparkler());
			DropItem(new BloodMossSmallSparkler());
            DropItem(new GarlicLargeSparkler());
			DropItem(new GarlicSmallSparkler());
            DropItem(new GinsengLargeSparkler());
			DropItem(new GinsengSmallSparkler());
            DropItem(new LargeSparkler());
			DropItem(new MandrakeRootLargeSparkler());
            DropItem(new MandrakeRootSmallSparkler());
			DropItem(new NightshadeLargeSparkler());
            DropItem(new NightshadeSmallSparkler());
			DropItem(new SmallSparkler());
            DropItem(new SpiderSilkLargeSparkler());
			DropItem(new SpiderSilkSmallSparkler());
            DropItem(new SulfurousAshLargeSparkler());
			DropItem(new SulfurousAshSmallSparkler());				
		}

        public SparklerBag(Serial serial) : base(serial)
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