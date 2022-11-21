using System;
using Server;

namespace Server.Items
{
    public class GingerBreadHouseDeed : MiniHouseDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                MiniHouseAddon addon = new MiniHouseAddon(Type);
                return addon;
            }
        }

        [Constructable]
        public GingerBreadHouseDeed()
            : base(MiniHouseType.GingerBreadHouse)
        {
            LootType = LootType.Blessed;
        }

        public GingerBreadHouseDeed(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}