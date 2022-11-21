using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class EvilBioEngineer : BaseCreature
	{
		[Constructable]
		public EvilBioEngineer() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the insane bio engineer";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

			SetStr( 125, 175 );
			SetDex( 115, 125 );
			SetInt( 115, 125 );

			SetHits( 750, 1250 );
			SetMana( 1000 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.Fencing, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );
			SetSkill( SkillName.EvalInt, 55.0, 67.5 );
			SetSkill( SkillName.Magery, 55.0, 67.5 );

			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Broadsword() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new Club() ); break;
				case 5: AddItem( new Dagger() ); break;
				case 6: AddItem( new Spear() ); break;
			}

			Fame = 5000;
			Karma = -5000;

			AddItem( new Boots() );

			int hue = Utility.RandomMinMax( 1355, 1378 );  //Random Damage Type Hue

			Item robe = new Robe();
			robe.Hue = hue;
			AddItem( robe );

			Item chest = new LeatherChest();
			chest.Hue = hue;
			AddItem( chest );

			Item gloves = new LeatherGloves();
			gloves.Hue = hue;
			AddItem( gloves );

			Item gorget = new LeatherGorget();
			gorget.Hue = hue;
			AddItem( gorget );

			Item legs = new LeatherLegs();
			legs.Hue = hue;
			AddItem( legs );

			Item arms = new LeatherArms();
			arms.Hue = hue;
			AddItem( arms );

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			PackItem( new Organics( Utility.RandomMinMax( 150, 300 ) ) );

			if ( Utility.Random( 100 ) < 25 )
			{
				switch ( Utility.Random( 2 ))
				{
					case 0: PackItem( new EmptyDNAVial() ); break;
					case 1: PackItem( new EmptyDNAVialSet() ); break;
				}
			}
			
			if ( Utility.Random( 500 ) < 3 )
			{
				switch (Utility.Random( 2 ))
				{
					case 0: PackItem( new BioTool() ); break;
					case 1: PackItem( new BioEnginerBook() ); break;
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public EvilBioEngineer( Serial serial ) : base( serial )
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