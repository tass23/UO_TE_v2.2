using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class EyeOfNavrey : Item
    {
        public override int LabelNumber{ get{ return 1095154;}} // Eye of Navrey Night-Eyes

        [Constructable]
        public EyeOfNavrey() : base( 0x318D )
        {
            Weight = 1;
            Hue = 68;
            LootType = LootType.Blessed;
        }

        public EyeOfNavrey( Serial serial ) : base( serial )
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
