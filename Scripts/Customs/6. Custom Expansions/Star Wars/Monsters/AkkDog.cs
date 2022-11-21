using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "an akk dog corpse" )]
	public class AkkDog : BaseCreature
	{
		[Constructable]
		public AkkDog() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 134;
			Name = "an akk dog";
			Hue = 1534;
			BaseSoundID = 0x16A;
			SetStr( 301, 400 );
			SetDex( 166, 185 );
			SetInt( 161, 200 );
			SetHits( 321, 380 );

			SetDamage( 7, 14 );
			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );
			SetResistance( ResistanceType.Physical, 40, 45 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 40, 50 );
			SetSkill( SkillName.Anatomy, 55.0, 65.1 );
			SetSkill( SkillName.MagicResist, 65.1, 75.0 );
			SetSkill( SkillName.Tactics, 85.1, 95.0 );
			SetSkill( SkillName.Wrestling, 75.1, 85.0 );

			Fame = 200;
			Karma = -200;
			Tamable = false;
		}

		public override int GetIdleSound()
		{
			return 0x2CE;
		}

		public override int GetDeathSound()
		{
			return 0x2CC;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override int GetAttackSound()
		{
			return 0x2C8;
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if (Utility.RandomDouble() < 0.05)
			{
				switch ( Utility.Random( 45 ) )
				{
					case 0: c.DropItem( new AllyaExileDeed() ); break;
					case 1: c.DropItem( new AllyaRedemptionDeed() ); break;
					case 2: c.DropItem( new AnkarresDeed() ); break;
					case 3: c.DropItem( new BaasDeed() ); break;
					case 4: c.DropItem( new BarabDeed() ); break;
					case 5: c.DropItem( new BlackwingDeed() ); break;
					case 6: c.DropItem( new BondaraDeed() ); break;
					case 7: c.DropItem( new BondarDeed() ); break;
					case 8: c.DropItem( new DamindDeed() ); break;
					case 9: c.DropItem( new DODDeed() ); break;
					case 10: c.DropItem( new DragiteDeed() ); break;
					case 11: c.DropItem( new DurindfireDeed() ); break;
					case 12: c.DropItem( new EralamDeed() ); break;
					case 13: c.DropItem( new GreenAdeganDeed() ); break;
					case 14: c.DropItem( new HeartDeed() ); break;
					case 15: c.DropItem( new HurrikaineDeed() ); break;
					case 16: c.DropItem( new ImpactDeed() ); break;
					case 17: c.DropItem( new JenruaxDeed() ); break;
					case 18: c.DropItem( new KenobiDeed() ); break;
					case 19: c.DropItem( new KraytDeed() ); break;
					case 20: c.DropItem( new LambentDeed() ); break;
					case 21: c.DropItem( new LavaDeed() ); break;
					case 22: c.DropItem( new LignanDeed() ); break;
					case 23: c.DropItem( new LorridianDeed() ); break;
					case 24: c.DropItem( new MantleDeed() ); break;
					case 25: c.DropItem( new MeditationDeed() ); break;
					case 26: c.DropItem( new NextorDeed() ); break;
					case 27: c.DropItem( new PermafrostDeed() ); break;
					case 28: c.DropItem( new PhondDeed() ); break;
					case 29: c.DropItem( new QixoniDeed() ); break;
					case 30: c.DropItem( new RubatDeed() ); break;
					case 31: c.DropItem( new RuusanDeed() ); break;
					case 32: c.DropItem( new SapithDeed() ); break;
					case 33: c.DropItem( new SigilDeed() ); break;
					case 34: c.DropItem( new SolariDeed() ); break;
					case 35: c.DropItem( new StygiumDeed() ); break;
					case 36: c.DropItem( new SunriderDeed() ); break;
					case 37: c.DropItem( new SyntheticDeed() ); break;
					case 38: c.DropItem( new TyranusDeed() ); break;
					case 39: c.DropItem( new UlricRedemptionDeed() ); break;
					case 40: c.DropItem( new UltimaDeed() ); break;
					case 41: c.DropItem( new UpariDeed() ); break;
					case 42: c.DropItem( new VelmoriteDeed() ); break;
					case 43: c.DropItem( new VexxtalDeed() ); break;
					case 44: c.DropItem( new WinduDeed () ); break;
				}
			}
        }
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 1 );
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override int Scales{ get{ return 5; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Green; } }

		public AkkDog( Serial serial ) : base( serial )
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

			if ( BaseSoundID == -1 )
				BaseSoundID = 0x16A;
		}
	}
}