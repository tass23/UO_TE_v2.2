using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a training dragon corpse" )]
	public class TrainingDragon : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public TrainingDragon() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0, 0 )
		{
			Name = "a Training Dragon";
			Hue = 2219;
			Body = 59;
			BaseSoundID = 362;
			CantWalk = true;

			SetStr( 50, 50 );
			SetDex( 350, 350 );
			SetInt( 71, 92 );

			SetHits( 30000, 30000 );

			SetDamage( 2, 2 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Poison, 0 );
			SetDamageType( ResistanceType.Energy, 0 );

			SetResistance( ResistanceType.Physical, 150 );
			SetResistance( ResistanceType.Fire, 150 );
			SetResistance( ResistanceType.Cold, 150 );
			SetResistance( ResistanceType.Poison, 150 );
			SetResistance( ResistanceType.Energy, 150 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 350;
			ControlSlots = 1;

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

		public TrainingDragon( Serial serial ) : base( serial )
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