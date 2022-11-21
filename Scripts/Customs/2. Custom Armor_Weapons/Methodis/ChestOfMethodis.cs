//Created by Blake Miller (wangchung)
using System;
using Server;

namespace Server.Items
{
    public class ChestOfMethodis : ChainChest
    {
		public override int ArtifactRarity{ get{ return 100; } }
        public override int BasePhysicalResistance{ get{ return 90; } }
        public override int BaseFireResistance{ get{ return 75; } }
        public override int BaseEnergyResistance{ get{ return 86; } }
        public override int BasePoisonResistance{ get{ return 12; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public ChestOfMethodis()
        {
            Name = "Chest Of Methodis";
            Hue = 2101;
            Attributes.BonusStr = 7;
            Attributes.RegenHits = 2;
            Attributes.DefendChance = 10;
            Attributes.Luck = 25;
            SkillBonuses.SetValues( 0, SkillName.Tactics, 20.0 );
			LootType = LootType.Cursed;
        }

        public ChestOfMethodis(Serial serial) : base( serial )
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