using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a human corpse" )]
	public class YoungWitch : BaseCreature
	{
		[Constructable]
		public YoungWitch() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "female" );
			Title = "the young witch";
			Body = 401;

			SetStr( 81, 105 );
			SetDex( 91, 115 );
			SetInt( 196, 220 );

			SetHits( 249, 263 );
			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 50 );
			SetResistance( ResistanceType.Fire, 25, 500 );
			SetResistance( ResistanceType.Cold, 25, 50 );
			SetResistance( ResistanceType.Poison, 25, 50 );
			SetResistance( ResistanceType.Energy, 25, 50 );

			SetSkill( SkillName.EvalInt, 125.1, 150.0 );
			SetSkill( SkillName.Magery, 125.1, 150.0 );
			SetSkill( SkillName.MagicResist, 175.0, 197.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 90.2, 160.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 16;
			PackReg( 6 );
			AddItem( new Robe( Utility.RandomNeutralHue() ) );
			AddItem( new Sandals() );
			AddItem( new LeatherSkirt() );
			AddItem( new FemaleLeatherChest() );
			if (Utility.RandomDouble() < .25 ) PackItem( new Boline() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public YoungWitch( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}