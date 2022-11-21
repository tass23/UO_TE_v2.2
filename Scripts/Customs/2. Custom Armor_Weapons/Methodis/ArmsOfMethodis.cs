//Created by Blake Miller (wangchung)
using System;
using Server;

namespace Server.Items
{
    public class ArmsOfMethodis : DragonArms
    {
		public override int ArtifactRarity{ get{ return 100; } }
        public override int BasePhysicalResistance{ get{ return 20; } }
        public override int BaseColdResistance{ get{ return 13; } }
        public override int BaseFireResistance{ get{ return 71; } }
        public override int BaseEnergyResistance{ get{ return 32; } }
        public override int BasePoisonResistance{ get{ return 45; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public ArmsOfMethodis()
        {
            Name = "Arms Of Methodis";
            Hue = 2101;
            Attributes.BonusStr = 2;
            Attributes.AttackChance = 10;
            ArmorAttributes.DurabilityBonus = 10;
            SkillBonuses.SetValues( 0, SkillName.Healing, 50.0 );
			LootType = LootType.Cursed;
        }

        public ArmsOfMethodis(Serial serial) : base( serial )
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