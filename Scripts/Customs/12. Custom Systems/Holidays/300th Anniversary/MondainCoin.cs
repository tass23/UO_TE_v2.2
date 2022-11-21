using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class MondainCoin : Item
    {
        [Constructable]
        public MondainCoin() : base( 0x186F )
        {
            Name = "In Commemoration : the 300th anniversary of Mondain's defeat.";
            Hue = 0x966;
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public MondainCoin( Serial serial ) : base( serial )
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