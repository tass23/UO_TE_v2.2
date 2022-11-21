using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a yeti corpse" )]
	public class Yeti : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.CrushingBlow;
		}

		[Constructable]
		public Yeti() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a yeti";
			Body = 267;
			Hue = 1153;

			SetStr( 180, 240 );
			SetDex( 55, 72 );
			SetInt( 20, 60 );

			SetHits( 127, 185 );

			SetDamage( 7, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 45 );
			SetResistance( ResistanceType.Cold, 25, 50 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 5, 20 );

			SetSkill( SkillName.MagicResist, 40.1, 68.0 );
			SetSkill( SkillName.Tactics, 67.1, 87.0 );
			SetSkill( SkillName.Wrestling, 55.1, 80.0 );
			SetSkill( SkillName.Anatomy, 10.1, 24.0 );

			Fame = 3970;
			Karma = -3970;

			VirtualArmor = 28;

			PackItem( new YetiHair( 2 ) );
		}

		public override int GetAngerSound()
		{
			return 0x5A0;
		}

		public override int GetIdleSound()
		{
			return 0x59F;
		}

		public override int GetAttackSound()
		{
			return 0x5A1;
		}

		public override int GetHurtSound()
		{
			return 0x59E;
		}

		public override int GetDeathSound()
		{
			return 0x59D;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int Hides{ get{ return 14; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Meat{ get{ return 2; } }
		public override bool CanRummageCorpses{ get{ return true; } }

		public Yeti( Serial serial ) : base( serial )
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