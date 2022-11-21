using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a crazy chicken corpse" )]
	public class CrazyChicken : BaseCreature
	{
		[Constructable]
		public CrazyChicken() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Body = 226;
			BaseSoundID = 0x6E;
			Name = "a crazy chicken";
			SetStr( 1101, 1510 );
			SetDex( 171, 270 );
			SetInt( 301, 325 );
			SetHits( 801, 1200 );
			SetMana( 60, 135 );
			SetDamage( 18, 30 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 55, 60 );
			SetResistance( ResistanceType.Fire, 70, 25 );
			SetResistance( ResistanceType.Cold, 15, 90 );
			SetResistance( ResistanceType.Poison, 40, 55 );
			SetResistance( ResistanceType.Energy, 40, 55 );
			SetSkill( SkillName.Anatomy, 75.1, 85.1 );
			SetSkill( SkillName.MagicResist, 85.1, 100.0 );
			SetSkill( SkillName.Tactics, 100.1, 115.0 );
			SetSkill( SkillName.Wrestling, 105.1, 120.0 );

			Fame = 100;
			Karma = -100;
			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 98.7;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}
		
		public override int Meat{ get{ return 1; } }
		public override int Feathers{ get{ return 25; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Ostard; } }

		public CrazyChicken( Serial serial ) : base( serial )
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