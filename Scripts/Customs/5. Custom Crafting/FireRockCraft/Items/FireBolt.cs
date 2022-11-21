/* Created by Hammerhand */

using System;

namespace Server.Items
{
	public class FireBolt : Item, ICommodity
	{
        int ICommodity.DescriptionNumber { get { return LabelNumber; } }
        bool ICommodity.IsDeedable { get { return true; } }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }

		[Constructable]
		public FireBolt() : this( 1 )
		{
		}

		[Constructable]
		public FireBolt( int amount ) : base( 0x1BFB )
		{
            Name = "FireBolt";
            Hue = 1359;
			Stackable = true;
			Amount = amount;

		}

        public FireBolt(Serial serial)
            : base(serial)
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