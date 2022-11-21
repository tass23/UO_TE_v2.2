using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an effete putrid gargoyle corpse" )]
	public class EffetePutridGargoyle : BaseCreature
	{
		[Constructable]
		public EffetePutridGargoyle() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an Effete putrid gargoyle";
			Body = 4;
			BaseSoundID = 372;

			SetStr( 215, 220 );
			SetDex( 90, 95 );
			SetInt( 40, 45 );

			SetHits( 110, 111 );

			SetDamage( 8, 18 );

			SetDamageType( ResistanceType.Physical, 60 );
            SetDamageType( ResistanceType.Cold, 40 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 15, 25 );

			SetSkill( SkillName.Anatomy, 6.0, 8.0 );
			SetSkill( SkillName.MagicResist, 60.5, 65 );
			SetSkill( SkillName.Tactics, 65.7, 66 );
			SetSkill( SkillName.Wrestling, 69.6, 70.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 32;

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new UndyingFlesh() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls );
			AddLoot( LootPack.Gems, Utility.RandomMinMax( 1, 4 ) );
		}

		public override int Meat{ get{ return 1; } }

		public EffetePutridGargoyle( Serial serial ) : base( serial )
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