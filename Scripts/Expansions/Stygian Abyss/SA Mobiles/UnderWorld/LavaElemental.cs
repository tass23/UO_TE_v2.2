using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a lava elemental corpse" )]
	public class LavaElemental : BaseCreature
	{
		[Constructable]
		public LavaElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lava elemental";
			Body = 720; 

			SetStr( 446, 510 );
			SetDex( 173, 191 );
			SetInt( 369, 397 );

			SetHits( 260, 266 );

			SetDamage( 12, 18 );

			SetDamageType( ResistanceType.Physical, 10 );
			SetDamageType( ResistanceType.Fire, 90 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 0.0, 12.8 );
			SetSkill( SkillName.EvalInt, 84.8, 92.6 );
			SetSkill( SkillName.Magery, 90.1, 92.7 );
			SetSkill( SkillName.Meditation, 97.8, 102.8 );
			SetSkill( SkillName.MagicResist, 101.9, 106.2 );
			SetSkill( SkillName.Tactics, 80.3, 94.0 );
			SetSkill( SkillName.Wrestling, 71.7, 85.4 );

			PackItem( new LesserPoisonPotion() );
			PackReg( 9 );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Gems, 2 );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled

		public override int GetIdleSound() { return 1549; } 
		public override int GetAngerSound() { return 1546; } 
		public override int GetHurtSound() { return 1548; } 
		public override int GetDeathSound()	{ return 1547; }

		public LavaElemental( Serial serial ) : base( serial )
		{
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.25)
            {
                switch (Utility.Random(3))
                {
                    case 0: c.DropItem(new LavaSerpentCrust()); break;
                    case 1: c.DropItem(new TatteredAncientScroll()); break;
                    case 2: c.DropItem(new AncientPotteryFragments()); break;
                }
            }
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