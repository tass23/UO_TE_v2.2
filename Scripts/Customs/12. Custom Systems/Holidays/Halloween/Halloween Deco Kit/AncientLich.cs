using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ancient liche's corpse" )]
	public class AncientLich2 : BaseCreature
	{
		[Constructable]
		public AncientLich2() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "ancient lich" );
			Body = 78;
			BaseSoundID = 412;

			SetStr( 216, 305 );
			SetDex( 96, 115 );
			SetInt( 966, 1045 );

			SetHits( 560, 595 );

			SetDamage( 15, 27 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 25, 30 );

			SetSkill( SkillName.EvalInt, 120.1, 130.0 );
			SetSkill( SkillName.Magery, 120.1, 130.0 );
			SetSkill( SkillName.Meditation, 100.1, 101.0 );
			SetSkill( SkillName.Poisoning, 100.1, 101.0 );
			SetSkill( SkillName.MagicResist, 175.2, 200.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 75.1, 100.0 );

			Fame = 23000;
			Karma = -23000;

			VirtualArmor = 60;
			PackNecroReg( 30, 275 );
			
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override int GetIdleSound()
		{
			return 0x19D;
		}

		public override int GetAngerSound()
		{
			return 0x175;
		}

		public override int GetDeathSound()
		{
			return 0x108;
		}

		public override int GetAttackSound()
		{
			return 0xE2;
		}

		public override int GetHurtSound()
		{
			return 0x28B;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.MedScrolls, 2 );
			
			switch (Utility.Random(44))
            {
                case 0: PackItem(new GiantSpiderweb1AddonDeed()); break;
                case 1: PackItem(new GiantSpiderweb2AddonDeed()); break;
                case 2: PackItem(new GraveEastAddonDeed()); break;
                case 3: PackItem(new GraveSouthAddonDeed()); break;
                case 4: PackItem(new Gravestone1EastAddonDeed()); break;
                case 5: PackItem(new Gravestone1SouthAddonDeed()); break;
                case 6: PackItem(new Gravestone2EastAddonDeed()); break;
                case 7: PackItem(new Gravestone2SouthAddonDeed()); break;
                case 8: PackItem(new Gravestone3EastAddonDeed()); break;
                case 9: PackItem(new Gravestone3SouthAddonDeed()); break;
                case 10: PackItem(new Gravestone4EastAddonDeed()); break;
                case 11: PackItem(new Gravestone4SouthAddonDeed()); break;
                case 12: PackItem(new LayingSkeletonEastAddonDeed()); break;
                case 13: PackItem(new LayingSkeletonSouthAddonDeed()); break;
                case 14: PackItem(new MaabusCoffinEastAddonDeed()); break;
                case 15: PackItem(new MaabusCoffinSouthAddonDeed()); break;
                case 16: PackItem(new SkeletonBootsEastAddonDeed()); break;
                case 17: PackItem(new SkeletonBootsSouthAddonDeed()); break;
                case 18: PackItem(new SkeletonEastAddonDeed()); break;
                case 19: PackItem(new SkeletonSouthAddonDeed()); break;
                case 20: PackItem(new SkeletonMeatEastAddonDeed()); break;
                case 21: PackItem(new SkeletonMeatSouthAddonDeed()); break;

            }
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public AncientLich2( Serial serial ) : base( serial )
		{
		}
    
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}