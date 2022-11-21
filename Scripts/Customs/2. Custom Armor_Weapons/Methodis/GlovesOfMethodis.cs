//Created by Blake Miller (wangchung)
using System;
using Server;

namespace Server.Items
{
    public class GlovesOfMethodis : LeatherNinjaMitts
    {
		public override int ArtifactRarity{ get{ return 100; } }
        public override int BasePhysicalResistance{ get{ return 32; } }
        public override int BaseColdResistance{ get{ return 18; } }
        public override int BaseFireResistance{ get{ return 46; } }
        public override int BaseEnergyResistance{ get{ return 63; } }
        public override int BasePoisonResistance{ get{ return 10; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public GlovesOfMethodis()
        {
            Name = "Gloves Of Methodis";
            Hue = 2101;
            Attributes.BonusStr = 5;
            Attributes.DefendChance = 12;
            ArmorAttributes.SelfRepair = 2;
            SkillBonuses.SetValues( 0, SkillName.Bushido, 15.0 );
			LootType = LootType.Cursed;
        }

        public GlovesOfMethodis(Serial serial) : base( serial )
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