using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class BagofIngotBookCovers : Bag
    {
        private static void PlaceItemIn(Container parent, int x, int y, Item item)
        {
            parent.AddItem(item);
            item.Location = new Point3D(x, y, 0);
        }
		[Constructable]
        public BagofIngotBookCovers()
		{
            Name = "Bag of Ingot Book Covers";
            LootType = LootType.Blessed;
			Hue = 43;
            PlaceItemIn(this, 35, 35, new AgapiteBulkOrderCover());
            PlaceItemIn(this, 58, 35, new BlazeBulkOrderCover());
            PlaceItemIn(this, 81, 35, new BronzeBulkOrderCover());
            PlaceItemIn(this, 35, 54, new CopperBulkOrderCover());
            PlaceItemIn(this, 60, 54, new DullCopperBulkOrderCover());
            PlaceItemIn(this, 75, 54, new ElectrumBulkOrderCover());
            PlaceItemIn(this, 93, 54, new GoldBulkOrderCover());
            PlaceItemIn(this, 35, 65, new IceBulkOrderCover());
            PlaceItemIn(this, 58, 65, new PlatinumBulkOrderCover());
            PlaceItemIn(this, 93, 65, new ShadowIronBulkOrderCover());
            PlaceItemIn(this, 35, 90, new ToxicBulkOrderCover());
            PlaceItemIn(this, 58, 90, new ValoriteBulkOrderCover());
            PlaceItemIn(this, 93, 90, new VeriteBulkOrderCover());
		}

        public BagofIngotBookCovers(Serial serial)
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