using System; 
using Server.Mobiles;

namespace Server.Items
{
    public class EtherealPolarBear : EtherealMount
    {
        [Constructable]
        public EtherealPolarBear()
            : base(11676, 0x3E92)
        {
            Name = "Ehereal Polar Bear Statuette";
            ItemID = 8417;
            MountedID = 16069;
            RegularID = 8417;
            LootType = LootType.Blessed;
        }

        public EtherealPolarBear(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            if (Name != "Ethereal Polar Bear Statuette")
                Name = "Ethereal Polar Bear Statuette";
        }
    }	
    //__________________________________________________________________________________________________________________________
    public class EtherealSkeletalSteed : EtherealMount
    {
        [Constructable]
        public EtherealSkeletalSteed()
            : base(11669, 0x3E90)
        {
            Name = "Ethereal Skeletal Steed Statuette";
            ItemID = 9751;
            MountedID = 16059;
            RegularID = 9751;
            LootType = LootType.Blessed;
        }

        public EtherealSkeletalSteed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            if (Name != "Ethereal Skeletal Steed Statuette")
                Name = "Ethereal Skeletal Steed Statuette";
        }
    }	
    //__________________________________________________________________________________________________________________________
    public class EtherealChimera : EtherealMount
    {
        [Constructable]
        public EtherealChimera()
            : base(11670, 0x3E91)
        {
            Name = "Ethereal Chimera Statuette";
            ItemID = 11669;
            MountedID = 16016;
            RegularID = 11669;
            LootType = LootType.Blessed;
        }

        public EtherealChimera(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            if (Name != "Ethereal Chimera Statuette")
                Name = "Ethereal Chimera Statuette";
        }
    }	
    //_____________________________________________________________________________________________________________________________
    public class EtherealChargerOfTheFallen : EtherealMount
    {
        [Constructable]
        public EtherealChargerOfTheFallen()
            : base(11670, 0x3E91)
        {
            Name = "Ethereal Charger Of The Fallen Statuette";
            ItemID = 11676;
            MountedID = 16018;
            RegularID = 11676;
            LootType = LootType.Blessed;
        }

        public EtherealChargerOfTheFallen(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            if (Name != "Ethereal Charger Of The Fallen Statuette")
                Name = "Ethereal Charger Of The Fallen Statuette";
        }
    }
	//__________________________________________________________________________________________________________________________
    public class EtherealHiryu : EtherealMount
    {
        [Constructable]
        public EtherealHiryu()
            : base(10090, 16020)
        {
            Name = "Ethereal Hiryu Statuette";
            ItemID = 10090;
            MountedID = 16020;
            RegularID = 10090;
            LootType = LootType.Blessed;
        }

        public EtherealHiryu(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            if (Name != "Ethereal Hiryu Statuette")
                Name = "Ethereal Hiryu Statuette";
        }
    }
}