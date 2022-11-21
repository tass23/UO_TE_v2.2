//Created by Blake Miller (wangchung)
using System;
using Server;

namespace Server.Items
{
    public class HelmetofMethodis : DragonHelm
    {
        public override int ArtifactRarity{ get{ return 100; } }
        public override int BasePhysicalResistance{ get{ return 74; } }
        public override int BaseColdResistance{ get{ return 35; } }
        public override int BaseFireResistance{ get{ return 49; } }
        public override int BaseEnergyResistance{ get{ return 58; } }
        public override int BasePoisonResistance{ get{ return 63; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public HelmetofMethodis()
        {
            Name = "Helmet of Methodis";
            Hue = 2101;
            Attributes.NightSight = 1;
            Attributes.BonusStam = 14;
            Attributes.DefendChance = 12;
            Attributes.LowerManaCost = 5;
            SkillBonuses.SetValues( 0, SkillName.Chivalry, 11.0 );
			LootType = LootType.Cursed;
        }

        public HelmetofMethodis(Serial serial) : base( serial )
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