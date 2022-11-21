using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an Asp's corpse" )]
	public class Asp : BaseCreature
	{
		[Constructable]
		public Asp () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ActiveSpeed = 0.1;
			PassiveSpeed = 0.2;
			Body = 91;
			Name = "an Asp";

			Hue = 1090;
			BaseSoundID = 0xDB;
			SetStr( 205, 343 );
			SetDex( 202, 283 );
			SetInt( 88, 142 );
			SetHits( 628, 1291 );
			SetDamage( 19, 28 );
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

            Fame = 17500;
            Karma = -17500;
			PackGem( 2 );
			PackItem( new Bone() );	
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
		public override bool GivesMinorArtifact{ get{ return false; } }
		public override int Hides{ get{ return 48; } }
		public override int Meat{ get{ return 1; } }

		public Asp( Serial serial ) : base( serial )
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