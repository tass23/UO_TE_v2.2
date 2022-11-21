using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class BagofLeatherBookCovers : Bag
    {
        private static void PlaceItemIn(Container parent, int x, int y, Item item)
        {
            parent.AddItem(item);
            item.Location = new Point3D(x, y, 0);
        }
		[Constructable]
        public BagofLeatherBookCovers()
		{
            Name = "Bag of Leather Book Covers";
            LootType = LootType.Blessed;
			Hue = 1161;
            PlaceItemIn(this, 35, 35, new BarbedBulkOrderCover());
            PlaceItemIn(this, 58, 35, new BlazeLBulkOrderCover());
            PlaceItemIn(this, 81, 35, new DaemonicBulkOrderCover());
            PlaceItemIn(this, 35, 54, new EtherealBulkOrderCover());
            PlaceItemIn(this, 60, 54, new FrostBulkOrderCover());
            PlaceItemIn(this, 75, 54, new HornedBulkOrderCover());
            PlaceItemIn(this, 93, 54, new PolarBulkOrderCover());
            PlaceItemIn(this, 35, 65, new ShadowBulkOrderCover());
            PlaceItemIn(this, 58, 65, new SpinedBulkOrderCover());
            PlaceItemIn(this, 93, 65, new SyntheticBulkOrderCover());
		}

        public BagofLeatherBookCovers(Serial serial)
            : base(serial)
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