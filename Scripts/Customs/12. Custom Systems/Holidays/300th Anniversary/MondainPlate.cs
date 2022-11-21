using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class MondainPlate : Item
    {
        [Constructable]
        public MondainPlate() : base( 0x9D7 )
        {
            Name = "A plate decorated with a beautiful painting of Mondain's defeat as the Gem of Immortality shatters.";
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public MondainPlate( Serial serial ) : base( serial )
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