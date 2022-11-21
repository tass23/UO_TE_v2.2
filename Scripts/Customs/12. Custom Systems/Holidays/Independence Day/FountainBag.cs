using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class FountainBag : Bag
	{
		[Constructable]
        public FountainBag() : this (1)
		{
            Name = "a bag of fountains";
            Hue = Utility.RandomList( 2, 3, 37, 38, 1150, 1153 );
        }

		[Constructable]
       	public FountainBag( int amount )
        {
            DropItem(new BlackPearlFountain());
			DropItem(new BloodMossFountain());
            DropItem(new GarlicFountain());
			DropItem(new GinsengFountain());
            DropItem(new MandrakeRootFountain());
			DropItem(new NightshadeFountain());
            DropItem(new SpiderSilkFountain());
			DropItem(new SulfurousAshFountain());
		}

        public FountainBag(Serial serial) : base(serial)
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