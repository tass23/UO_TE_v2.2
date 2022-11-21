using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class EvilDruid : BaseCreature
	{

		[Constructable]
		public EvilDruid() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the druid";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( Utility.RandomNeutralHue() ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			}

			SetStr( 150, 225 );
			SetDex( 94, 115 );
			SetInt( 115, 150 );

			SetHits( 225, 300 );
			SetMana( 300 );

			SetDamage( 13, 23 );

			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Magery, 77.0, 93.5 );
			SetSkill( SkillName.EvalInt, 77.0, 93.5 );
			SetSkill( SkillName.MagicResist, 77.0, 93.5 );
			SetSkill( SkillName.Meditation, 50.0, 76.5 );

			Fame = 10000;
			Karma = -10000;

			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new Robe( Utility.RandomNeutralHue() ) );
			AddItem( new LeatherChest() );
			AddItem( new LeatherGloves() );
			AddItem( new LeatherGorget() );
			AddItem( new LeatherArms() );
			AddItem( new LeatherLegs() );

			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new QuarterStaff() ); break;
				case 1: AddItem( new BlackStaff() ); break;
				case 2: AddItem( new GnarledStaff() ); break;
			}

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			PackItem( new SpringWater( Utility.RandomMinMax( 5, 10 ) ) );
			PackItem( new DestroyingAngel( Utility.RandomMinMax( 5, 10 ) ) );
			PackItem( new PetrifiedWood( Utility.RandomMinMax( 5, 10 ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public EvilDruid( Serial serial ) : base( serial )
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