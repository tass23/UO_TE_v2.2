using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    [Flipable( 0x232A, 0x232B )]
    public class MondainPackageRare : MondainBag
    {
        [Constructable]
        public MondainPackageRare()
        {
            DropItem( new AskAndAnswer() );
            DropItem( new MondainPlate() );
            DropItem( new MondainCoin() );
            DropItem( new FireworksWand() );
            DropItem( new Jaana() );

        }

        public MondainPackageRare( Serial serial ) : base( serial )
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