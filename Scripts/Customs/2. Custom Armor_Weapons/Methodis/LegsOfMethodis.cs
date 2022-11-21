//Created by Blake Miller (wangchung)
using System;
using Server;

namespace Server.Items
{
    public class LegsOfMethodis : StuddedSuneate
    {
        public override int ArtifactRarity{ get{ return 100; } }
        public override int BasePhysicalResistance{ get{ return 34; } }
        public override int BaseColdResistance{ get{ return 94; } }
        public override int BaseFireResistance{ get{ return 24; } }
        public override int BaseEnergyResistance{ get{ return 28; } }
        public override int BasePoisonResistance{ get{ return 73; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public LegsOfMethodis()
        {
            Name = "Legs Of Methodis";
            Hue = 2101;
            Attributes.RegenHits = 3;
            Attributes.DefendChance = 5;
            ArmorAttributes.SelfRepair = 3;
			LootType = LootType.Cursed;
        }

        public LegsOfMethodis(Serial serial) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 0 );
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }
}