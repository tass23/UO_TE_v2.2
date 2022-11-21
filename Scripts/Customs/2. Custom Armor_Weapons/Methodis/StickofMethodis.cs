using System;
using Server;

namespace Server.Items
{
    public class StickOfMethodis : Bokuto
    {
		public override int ArtifactRarity{ get{ return 100; } }
        public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public StickOfMethodis()
        {
            Name = "Stick Of Methodis";
            Hue = 2101;
            Attributes.SpellChanneling = 1;
            Attributes.BonusStr = 5;
            WeaponAttributes.HitLeechHits = 25;
            Attributes.WeaponDamage = 15;
            Attributes.WeaponSpeed = 2;
            WeaponAttributes.HitLowerDefend = 50;
			LootType = LootType.Cursed;
        }

        public StickOfMethodis(Serial serial) : base( serial )
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