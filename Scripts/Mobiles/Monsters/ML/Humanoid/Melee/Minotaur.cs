using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a minotaur corpse" )]
	public class Minotaur : BaseCreature
	{
		[Constructable]
		public Minotaur() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) // NEED TO CHECK
		{
			Name = "a minotaur";
			Body = 0x107;

			SetStr( 301, 340 );
			SetDex( 91, 110 );
			SetInt( 31, 50 );

			SetHits( 301, 340 );

			SetDamage( 11, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Meditation, 0 );
			SetSkill( SkillName.EvalInt, 0 );
			SetSkill( SkillName.Magery, 0 );
			SetSkill( SkillName.Poisoning, 0 );
			SetSkill( SkillName.Anatomy, 0 );
			SetSkill( SkillName.MagicResist, 56.1, 64.0 );
			SetSkill( SkillName.Tactics, 93.3, 97.8 );
			SetSkill( SkillName.Wrestling, 90.4, 92.1 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 28; // Don't know what it should be
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich );  // Need to verify
		}

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ParalyzingBlow;
		}

		public override void OnThink()
		{
			if ( Combatant != null )
			{
				if ( !InRange( Combatant.Location, 5 ) )
					CurrentSpeed = 0.05;
				else
					CurrentSpeed = ActiveSpeed;
			}
		}

		public override int TreasureMapLevel{ get{ return 3; } }

		public override int GetDeathSound()	{ return 0x596; }
		public override int GetAttackSound() { return 0x597; }
		public override int GetIdleSound() { return 0x598; }
		public override int GetAngerSound() { return 0x599; }
		public override int GetHurtSound() { return 0x59A; }

		public Minotaur( Serial serial ) : base( serial )
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