using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a vicious raptor corpse" )]
	public class ViciousRaptor : BaseCreature
	{
		[Constructable]
		public ViciousRaptor() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			ActiveSpeed = 0.1;
			PassiveSpeed = 0.2;
			Body = 204;
			BaseSoundID = 0xDB;
			Name = "a vicious raptor";
			SetStr( 1201, 1450 );
			SetDex( 196, 270 );
			SetInt( 90, 145 );
			SetHits( 1001, 1564 );
			SetDamage( 20, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );
			SetResistance( ResistanceType.Physical, 56, 62 );
			SetResistance( ResistanceType.Fire, 25, 29 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 27, 30 );
			SetSkill( SkillName.Wrestling, 124.5, 134.5 );
			SetSkill( SkillName.Tactics, 130.2, 142.0 );
			SetSkill( SkillName.MagicResist, 102.3, 113.0 );
			SetSkill( SkillName.Anatomy, 120.8, 138.1 );
			SetSkill( SkillName.Poisoning, 110.1, 133.4 );

			Fame = 110;
			Karma = -110;
			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 102.6;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}
		
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.MortalStrike;
		}
		
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 60; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public ViciousRaptor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}