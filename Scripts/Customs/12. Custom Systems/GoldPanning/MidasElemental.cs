using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a midas elemental corpse" )]
	public class MidasElemental : BaseCreature
	{

		[Constructable]
		public MidasElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			// TODO: Gas attack
			Name = "a midas elemental";
			Body = 112;
			BaseSoundID = 268;
			Hue = 1260;

			SetStr( 326, 355 );
			SetDex( 226, 245 );
			SetInt( 101, 122 );

			SetHits( 1136, 1153 );

			SetDamage( 33 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Cold, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 75, 85 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.MagicResist, 85.1, 110.0 );
			SetSkill( SkillName.Tactics, 95.1, 115.0 );
			SetSkill( SkillName.Wrestling, 100.1, 120.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 65;
			
			if ( 0.25 > Utility.RandomDouble() )
				PackItem(new LargeGoldNugget() );
			if ( 0.05 > Utility.RandomDouble() )
                PackItem(new GoldenWool());
			if ( 0.05 > Utility.RandomDouble() )
                PackItem(new RewardScroll());

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Gems, 4 );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			reflect = true; // Every spell is reflected back to the caster
		}

		public MidasElemental( Serial serial ) : base( serial )
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