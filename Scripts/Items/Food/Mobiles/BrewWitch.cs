using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a human corpse" )]
	public class BrewWitch : BaseCreature
	{
		[Constructable]
		public BrewWitch() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "female" );
			Title = "the elder witch";
			Body = 401;

			SetStr( 81, 105 );
			SetDex( 191, 215 );
			SetInt( 226, 250 );

			SetHits( 449, 463 );

			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 60 );
			SetResistance( ResistanceType.Fire, 35, 60 );
			SetResistance( ResistanceType.Cold, 35, 60 );
			SetResistance( ResistanceType.Poison, 35, 60 );
			SetResistance( ResistanceType.Energy, 35, 60 );

			SetSkill( SkillName.EvalInt, 180.2, 200.0 );
			SetSkill( SkillName.Magery, 195.1, 200.0 );
			SetSkill( SkillName.Meditation, 127.5, 150.0 );
			SetSkill( SkillName.MagicResist, 177.5, 300.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 120.3, 180.0 );

			Fame = 10500;
			Karma = -10500;

			VirtualArmor = 16;

			PackReg( 23 );
			PackItem( new Robe( Utility.RandomMetalHue() ) );
			PackItem( new WizardsHat( Utility.RandomMetalHue() ) );

			if ( Utility.RandomBool() )
				PackItem( new ThighBoots() );
			else
				PackItem( new Sandals() );
			AddItem( new LeatherSkirt() );
			AddItem( new FemaleLeatherChest() );
			if (Utility.RandomDouble() < .25 ) PackItem( new WitchCauldron() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public BrewWitch( Serial serial ) : base( serial ){}

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