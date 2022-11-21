using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class AskAndAnswer : Item
    {
        [Constructable]
        public AskAndAnswer() : base( 0xE2E )
        {
            Name = "Looking into the crystall ball, thou doth see swirling mists in which words form.'Ask and be answered'.";
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public AskAndAnswer( Serial serial ) : base( serial )
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