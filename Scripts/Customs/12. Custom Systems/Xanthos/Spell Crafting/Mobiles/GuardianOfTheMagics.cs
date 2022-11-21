#region AuthorHeader
//
//	SpellCrafting version 3.0, by Xanthos and TheOutkastDev
//
//  Based on original ideas and code by TheOutkastDev
//
#endregion AuthorHeader
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.SpellCrafting.Items;

namespace Server.SpellCrafting.Mobiles
{
	[CorpseName( "a guardian of the magics corpse" )]
	public class GuardianOfTheMagics : BaseCreature
	{
		private static string[] m_Names = new string[]
		{
			"Darouc",
			"Aan'f",
			"Etrii",
			"Vaukoor",
			"Iostijeau",
			"Udotea",
			"Caeres"
		};

		private static string[] m_Titles = new string[]
		{
			"Protector of the Magics",
			"Guardian of the Magics",
			"Seer of the Craftmaker",
			"Mystic Spellcrafter",
			"Master of Divine Trinkets"
		};	
			
		[Constructable]
		public GuardianOfTheMagics() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = m_Names[Utility.Random( m_Names.Length )];
			Title = m_Titles[Utility.Random( m_Titles.Length )];
			Body = 0x190;
			Hue = Utility.RandomSkinHue();

			SetStr( 90, 100 );
			SetDex( 50, 75 );
			SetInt( 150, 250  );
			SetHits( 900, 1100 );
			SetDamage( 12, 18 );
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 50, 70 );
			SetResistance( ResistanceType.Cold, 50, 70 );
			SetResistance( ResistanceType.Poison, 50, 70 );
			SetResistance( ResistanceType.Energy, 50, 70 );

			SetSkill( SkillName.EvalInt, 95.0, 120.0 );
			SetSkill( SkillName.Magery, 95.0, 120.0 );
			SetSkill( SkillName.Meditation, 95.0, 100.0 );
			SetSkill( SkillName.MagicResist, 100.0, 120.0 );
			SetSkill( SkillName.Tactics, 95.0, 120.0 );
			SetSkill( SkillName.Wrestling, 95.0, 120.0 );

			Fame = 13000;
			Karma = -13000;

			VirtualArmor = 70;

			int hue = Utility.RandomNeutralHue();
			AddItem( new Robe( hue ) );
			AddItem( new Sandals() );
			AddItem( new WizardsHat( hue ) );

			PackGold( 400, 600 );
			PackMagicItems( 2, 4, 0.45, 0.35 );

			// Pack magic jewels and possibly SpellCraft items
			PackItem( new MagicJewel( SpellCraftConfig.GuardianOfTheMagicsMagicJewelSpawn ) );

			if ( Utility.RandomDouble() <= SpellCraftConfig.GuardianOfTheMagicsBookChance )
				PackItem( new BookOfSpellCrafts() );

			if ( Utility.RandomDouble() <= SpellCraftConfig.GuardianOfTheMagicsJewelChance )
				PackItem( SpellCraft.RandomCraft() );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public GuardianOfTheMagics( Serial serial ) : base( serial )
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