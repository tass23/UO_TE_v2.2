//Created by Blake Miller (wangchung)
using System;
using Server;

namespace Server.Items
{
    public class ShieldofMethodis : ChaosShield
    {
        public override int ArtifactRarity{ get{ return 100; } }
        public override int BasePhysicalResistance{ get{ return 100; } }
        public override int BaseColdResistance{ get{ return 24; } }
        public override int BaseFireResistance{ get{ return 97; } }
        public override int BaseEnergyResistance{ get{ return 14; } }
        public override int BasePoisonResistance{ get{ return 1; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public ShieldofMethodis()
        {
            Name = "Shield Of Methodis";
            Hue = 2101;
            Attributes.DefendChance = 20;
            Attributes.Luck = 25;
            ArmorAttributes.SelfRepair = 2;
            ArmorAttributes.LowerStatReq = 45;
			LootType = LootType.Cursed;
        }

        public ShieldofMethodis(Serial serial) : base( serial )
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