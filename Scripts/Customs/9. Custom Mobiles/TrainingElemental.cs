
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a training elemental corpse" )]
	public class TrainingElemental : BaseCreature
	{
		private const int kHits = 1000;
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public TrainingElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0, 0 )
		{
			Name = "a Training Elemental";
			Body = 14;
			BaseSoundID = 268;
			Hue = 0x21;
			CantWalk = true;

			SetStr( 50, 50 );
			SetDex( 350, 350 );
			SetInt( 71, 92 );

			SetHits( kHits, kHits );

			SetDamage( 0, 0 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Poison, 0 );
			SetDamageType( ResistanceType.Energy, 0 );

			SetResistance( ResistanceType.Physical, 200 );
			SetResistance( ResistanceType.Fire, 200 );
			SetResistance( ResistanceType.Cold, 200 );
			SetResistance( ResistanceType.Poison, 200 );
			SetResistance( ResistanceType.Energy, 200 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 350;

		}

		public override void GenerateLoot()
		{
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void OnThink()
		{
			if ( Hits != HitsMax )
			{
				Hits = HitsMax;
			}
		}

		public TrainingElemental( Serial serial ) : base( serial )
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