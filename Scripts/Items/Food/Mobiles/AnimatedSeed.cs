using System;
using Server.Items;
using Server.Items.Crops;

namespace Server.Mobiles
{
	public class AnimatedSeed : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 5 ) )
			{
				default:
				case 0: return WeaponAbility.DoubleStrike;
				case 4: return WeaponAbility.ArmorIgnore;
			}
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		[Constructable]
		public AnimatedSeed() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 903;
			Hue = 0x5E2;
			Name = "Animated Seed";

			SetStr( 351, 380 );
			SetDex( 251, 280 );
			SetInt( 51, 80 );

			SetHits(478, 501 );

			SetDamage( 15, 27 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Cold, 75 );

			SetResistance( ResistanceType.Physical, 70, 100 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 60, 90 );
			SetResistance( ResistanceType.Poison, 200 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.Wrestling, 100.0, 150.0 );
			SetSkill( SkillName.Tactics, 100.0, 150.0 );
			SetSkill( SkillName.MagicResist, 200.0, 350.0 );

			VirtualArmor = 50;
			Fame = 5000;
			Karma = -5000;
		}

		public override bool OnBeforeDeath()
		{
			if ( !base.OnBeforeDeath() ) return false;

			if ( 0.25 > Utility.RandomDouble() )
			{
				Item itemloot;
				switch (Utility.RandomMinMax( 1, 90 ))
				{
					case  1: default: itemloot = new HaySeed(); break;
					case  2: itemloot = new TeaSeed(); break;
					case  3: itemloot = new SoySeed(); break;
					case  4: itemloot = new FlaxSeed(); break;
					case  5: itemloot = new OatsSeed(); break;
					case  6: itemloot = new RiceSeed(); break;
					case  7: itemloot = new CornSeed(); break;
					case  8: itemloot = new DateSeed(); break;
					case  9: itemloot = new BeetSeed(); break;
					case  10: itemloot = new PeasSeed(); break;
					case  11: itemloot = new PansySeed(); break;
					case  12: itemloot = new PoppySeed(); break;
					case  13: itemloot = new WheatSeed(); break;
					case  14: itemloot = new OnionSeed(); break;
					case  15: itemloot = new CottonSeed(); break;
					case  16: itemloot = new GarlicSeed(); break;
					case  17: itemloot = new TanGingerSeed(); break;
					case  18: itemloot = new BananaSeed(); break;
					case  19: itemloot = new CarrotSeed(); break;
					case  20: itemloot = new CelerySeed(); break;
					case  21: itemloot = new PeanutSeed(); break;
					case  22: itemloot = new PotatoSeed(); break;
					case  23: itemloot = new RadishSeed(); break;
					case  24: itemloot = new TomatoSeed(); break;
					case  25: itemloot = new SquashSeed(); break;
					case  26: itemloot = new RedRoseSeed(); break;
					case  27: itemloot = new GinsengSeed(); break;
					case  28: itemloot = new CoconutSeed(); break;
					case  29: itemloot = new CabbageSeed(); break;
					case  30: itemloot = new LettuceSeed(); break;
					case  31: itemloot = new SpinachSeed(); break;
					case  32: itemloot = new PumpkinSeed(); break;
					case  33: itemloot = new MandrakeSeed(); break;
					case  34: itemloot = new SnowHopsSeed(); break;
					case  35: itemloot = new MiniKiwiSeed(); break;
					case  36: itemloot = new BroccoliSeed(); break;
					case  37: itemloot = new EggplantSeed(); break;
					case  38: itemloot = new SnowPeasSeed(); break;
					case  39: itemloot = new CucumberSeed(); break;
					case  40: itemloot = new CranberrySeed(); break;
					case  41: itemloot = new PineappleSeed(); break;
					case  42: itemloot = new IrishRoseSeed(); break;
					case  43: itemloot = new WhiteRoseSeed(); break;
					case  44: itemloot = new SugarcaneSeed(); break;
					case  45: itemloot = new ElvenHopsSeed(); break;
					case  46: itemloot = new SweetHopsSeed(); break;
					case  47: itemloot = new FieldCornSeed(); break;
					case  48: itemloot = new SunFlowerSeed(); break;
					case  49: itemloot = new MiniAppleSeed(); break;
					case  50: itemloot = new MiniCocoaSeed(); break;
					case  51: itemloot = new MiniMangoSeed(); break;
					case  52: itemloot = new MiniPeachSeed(); break;
					case  53: itemloot = new AsparagusSeed(); break;
					case  54: itemloot = new GreenBeanSeed(); break;
					case  55: itemloot = new RedPepperSeed(); break;
					case  56: itemloot = new BlackberrySeed(); break;
					case  57: itemloot = new SnapdragonSeed(); break;
					case  58: itemloot = new SpiritRoseSeed(); break;
					case  59: itemloot = new YellowRoseSeed(); break;
					case  60: itemloot = new NightshadeSeed(); break;
					case  61: itemloot = new BitterHopsSeed(); break;
					case  62: itemloot = new MiniAlmondSeed(); break;
					case  63: itemloot = new MiniCherrySeed(); break;
					case  64: itemloot = new MiniCoffeeSeed(); break;
					case  65: itemloot = new StrawberrySeed(); break;
					case  66: itemloot = new CantaloupeSeed(); break;
					case  67: itemloot = new RedMushroomSeed(); break;
					case  68: itemloot = new TanMushroomSeed(); break;
					case  69: itemloot = new MiniApricotSeed(); break;
					case  70: itemloot = new MiniAvocadoSeed(); break;
					case  71: itemloot = new SmallBananaSeed(); break;
					case  72: itemloot = new CauliflowerSeed(); break;
					case  73: itemloot = new SweetPotatoSeed(); break;
					case  74: itemloot = new ChiliPepperSeed(); break;
					case  75: itemloot = new GreenPepperSeed(); break;
					case  76: itemloot = new GreenSquashSeed(); break;
					case  77: itemloot = new RedRaspberrySeed(); break;
					case  78: itemloot = new MiniPistacioSeed(); break;
					case  79: itemloot = new OrangePepperSeed(); break;
					case  80: itemloot = new YellowPepperSeed(); break;
					case  81: itemloot = new PinkCarnationSeed(); break;
					case  82: itemloot = new HoneydewMelonSeed(); break;
					case  83: itemloot = new BlackRaspberrySeed(); break;
					case  84: itemloot = new MiniGrapefruitSeed(); break;
					case  85: itemloot = new MiniPomegranateSeed(); break;
					case  86: itemloot = new BlueberrySeed(); break;
					case  87: itemloot = new MiniPearSeed(); break;
					case  88: itemloot = new MiniOrangeSeed(); break;
					case  89: itemloot = new WatermelonSeed(); break;
					case  90: itemloot = new TurnipSeed(); break;
				}
				if (itemloot != null)
					itemloot.MoveToWorld( Location, Map );
			}

			Effects.SendLocationEffect( Location, Map, 0x376A, 10, 1 );
			return base.OnBeforeDeath();
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public AnimatedSeed( Serial serial ) : base( serial ){}

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