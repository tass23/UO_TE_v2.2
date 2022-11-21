using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class Jaana : Item
    {
        [Constructable]
        public Jaana() : base( 0xF03 )
        {
            Name = "Jaana's Hangover Remedy";
            Hue = 0x54E;
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public Jaana( Serial serial ) : base( serial )
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