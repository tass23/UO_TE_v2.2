/* Created by Hammerhand*/

using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
    public class CrystalineFire : Item, ICommodity
	{
        int ICommodity.DescriptionNumber { get { return LabelNumber; } }
        bool ICommodity.IsDeedable { get { return true; } }
		
        [Constructable]
        public CrystalineFire() : this(1)
        {
        }
		[Constructable]
		public CrystalineFire(int amount) : base( 3191 )	//0x1F19
		{
            Stackable = true;
            Name = "Crystalline Fire";
            Hue = 1260;
			Weight = 1.0;
            Amount = amount;
		}

        public CrystalineFire(Serial serial)
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