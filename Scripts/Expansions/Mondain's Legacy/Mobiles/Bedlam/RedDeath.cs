using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a red death corpse" )]
    public class RedDeath : BaseCreature
    {
        [Constructable]
		public RedDeath() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
        {
			Name = "a Red Death";
			Body = 0x319;
            BaseSoundID = 0xA8;
            Hue = 0x21;

			SetStr(319, 324);
			SetDex(241, 244);
			SetInt(242, 255);

			SetHits(1540, 1605);

            SetDamage(25, 29);

            SetDamageType(ResistanceType.Physical, 25);
            SetDamageType(ResistanceType.Fire, 75);

            SetResistance(ResistanceType.Physical, 60, 70);
            SetResistance(ResistanceType.Fire, 90);
            SetResistance(ResistanceType.Poison, 100);

            SetSkill(SkillName.Wrestling, 121.4, 143.7);
            SetSkill(SkillName.Tactics, 120.9, 142.2);
            SetSkill(SkillName.MagicResist, 120.1, 142.3);
            SetSkill(SkillName.Anatomy, 120.2, 144.0);

            Fame = 12500;
            Karma = 12500;

            for (int i = 0; i < 1; i++)
                if (Utility.RandomBool())
                    PackNecroScroll(Utility.RandomMinMax(5, 9));
                else
                    PackScroll(4, 7);
        }

        public static double SpeedBuff = 1.20;

        public override void GenerateLoot()
        {
			AddLoot( LootPack.AosUltraRich, 4 );
		}

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.WhirlwindAttack;
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            c.DropItem(new ResolvesBridle());

			if ( Utility.RandomDouble() < 0.25 )
				c.DropItem( new EtherealSkeletalSteed() );
        }

        public override bool GivesMinorArtifact { get { return true; } }
        public override bool HasBreath { get { return true; } } // Change to chaso breath later
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public RedDeath( Serial serial ) : base( serial )
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}