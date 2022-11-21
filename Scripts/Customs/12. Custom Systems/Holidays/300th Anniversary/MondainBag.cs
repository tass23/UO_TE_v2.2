using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class MondainBag : Container
    {
        [Constructable]
        public MondainBag() : base( 0xE76 )
        {
            Name = "Happy 300th Anniversary!";
            Hue = Utility.RandomList(0x537, 0x561, 0x8AD);
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public MondainBag( Serial serial ) : base( serial )
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