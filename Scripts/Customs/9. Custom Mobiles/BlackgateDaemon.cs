using System;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
	[CorpseName( "a blackgate daemon corpse" )]
	public class BlackgateDaemon : BaseCreature
	{
		public virtual double AMCLootableChance { get { return AMCLootable.LootChance; } }
		public virtual double ArtifactMapChestChance { get { return ArtifactMapChest.LootChance; } }

		public static double ChocolateIngredientChance = .65;	// Chance that a Blackgate Daemon will drop a chocolatiering ingredient

		public override double DispelDifficulty{ get{ return 1555.0; } }
		public override double DispelFocus{ get{ return 75.0; } }

		public override Faction FactionAllegiance { get { return Shadowlords.Instance; } }
		public override Ethics.Ethic EthicAllegiance { get { return Ethics.Ethic.Evil; } }

		[Constructable]
		public BlackgateDaemon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Title = "The Blackgate Daemon";
			Name = NameList.RandomName( "daemon" );
		    Body = 38;
			BaseSoundID = 357;

			SetStr( 2200 );
			SetDex( 1710, 1950 );
			SetInt( 1910, 2200 );

			SetHits( 1999);

			SetDamage( 16, 26 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 70 );
			SetResistance( ResistanceType.Fire, 70 );
			SetResistance( ResistanceType.Cold, 70 );
			SetResistance( ResistanceType.Poison, 70 );
			SetResistance( ResistanceType.Energy, 70 );

			SetSkill( SkillName.EvalInt, 120.0 );
			SetSkill( SkillName.Magery, 120.0 );
			SetSkill( SkillName.Meditation, 120.0 );
			SetSkill( SkillName.MagicResist, 90.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 90;

        	PackItem( new HoodedShroudOfShadows( ) );      
		 
			PackGem();
			PackGold( 4700, 6950 );
			switch ( Utility.Random( 50 ) ) //Rarity 10
            {
			case 0: PackItem( new TheTaskmaster( ) );
				break;
			case 1: PackItem( new ZyronicClaw( ) );
				break;
			case 2: PackItem( new BladeOfTheRighteous( ) );
				break;
			case 3: PackItem( new TitansHammer( ) );
				break;
			case 4: PackItem( new InquisitorsResolution( ) );
				break;
            } 

            switch ( Utility.Random( 70 ) ) //Rarity 11
            {
			case 0: PackItem( new AxeOfTheHeavens( ) );
				break;
			case 1: PackItem( new BladeOfInsanity( ) );
				break;
			case 2: PackItem( new BoneCrusher( ) );
				break;
			case 3: PackItem( new BreathOfTheDead( ) );
				break;
			case 4: PackItem( new Frostbringer( ) );
				break;
			case 5: PackItem( new LegacyOfTheDreadLord( ) );
				break;
			case 6: PackItem( new SerpentsFang( ) );
				break;
			case 7: PackItem( new StaffOfTheMagi( ) );
				break;
			case 8: PackItem( new TheBeserkersMaul( ) );
				break;
			case 9: PackItem( new TheDragonSlayer( ) );
				break;
			case 10: PackItem( new ArmorOfFortune( ) );
				break;
			case 11: PackItem( new OrnateCrownOfTheHarrower( ) );
				break;
			case 12: PackItem( new MidnightBracers( ) );
				break;
			case 13: PackItem( new LeggingsOfBane( ) );
				break;
			case 14: PackItem( new JackalsCollar( ) );
				break;
			case 15: PackItem( new HolyKnightsBreastplate( ) );
				break;
			case 16: PackItem( new HelmOfInsight( ) );
				break;
			case 17: PackItem( new GauntletsOfNobility( ) );
				break;
			case 18: PackItem( new VoiceOfTheFallenKing( ) );
				break;
			case 19: PackItem( new TunicOfFire( ) );
				break;
			case 21: PackItem( new BraceletOfHealth( ) );
				break;
			case 22: PackItem( new OrnamentOfTheMagician( ) );
				break;
			case 23: PackItem( new RingOfTheElements( ) );
				break;
			case 24: PackItem( new RingOfTheVile( ) );
				break;
            }
        }

		public virtual bool GivesMinorArtifact { get { return true; } }

		public virtual bool GivesSAArtifact { get { return true; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
            AddLoot( LootPack.Poor);
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			c.DropItem( new BankCheck( Utility.Random( 2500, 5000 ) ));
			c.DropItem( new RewardScroll( Utility.Random( 25, 75 ) ));
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool Uncalmable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 6; } }

		public BlackgateDaemon( Serial serial ) : base( serial )
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