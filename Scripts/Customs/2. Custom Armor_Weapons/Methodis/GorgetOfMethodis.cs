//Created by Blake Miller (wangchung)
using System;
using Server;

namespace Server.Items
{
    public class GorgetOfMethodis : PlateGorget
    {
		public override int ArtifactRarity{ get{ return 100; } }
        public override int BasePhysicalResistance{ get{ return 87; } }
        public override int BaseColdResistance{ get{ return 15; } }
        public override int BaseFireResistance{ get{ return 21; } }
        public override int BaseEnergyResistance{ get{ return 13; } }
        public override int BasePoisonResistance{ get{ return 15; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public GorgetOfMethodis()
        {
            Name = "Gorget Of Methodis";
            Hue = 2101;
            Attributes.BonusDex = 5;
            Attributes.AttackChance = 3;
            ArmorAttributes.SelfRepair = 2;
            ArmorAttributes.LowerStatReq = 64;
            Attributes.LowerRegCost = 20;
            SkillBonuses.SetValues( 0, SkillName.Parry, 10.0 );
			LootType = LootType.Cursed;
        }

        public GorgetOfMethodis(Serial serial) : base( serial )
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