using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Krayt dragon corpse" )]
	public class KraytDragon : BaseCreature
	{
		[Constructable]
		public KraytDragon() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Krayt dragon";
			Body = 103;
			BaseSoundID = 362;
			Hue = 1806;
			SetStr( 311, 340 );
			SetDex( 201, 220 );
			SetInt( 701, 740 );
			SetHits( 530, 600 );

			SetDamage( 20, 30 );
			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );
			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 35, 45 );
			SetResistance( ResistanceType.Energy, 35, 45 );
			SetSkill( SkillName.EvalInt, 100.1, 110.0 );
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 100.1, 120.0 );
			SetSkill( SkillName.Wrestling, 101.1, 120.0 );

			Fame = 150;
			Karma = 150;
			VirtualArmor = 36;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if (Utility.RandomDouble() < 0.15)
				c.DropItem( new Blue4FocusingCrystal() );
				
			if (Utility.RandomDouble() < 0.10)
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
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 2 );
		}

		public override int GetIdleSound()
		{
			return 0x2C4;
		}

		public override int GetAttackSound()
		{
			return 0x2C0;
		}

		public override int GetDeathSound()
		{
			return 0x2C1;
		}

		public override int GetAngerSound()
		{
			return 0x2C4;
		}

		public override int GetHurtSound()
		{
			return 0x2C3;
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override double BonusPetDamageScalar{ get{ return (Core.SE) ? 3.0 : 1.0; } }
		public override bool AutoDispel{ get{ return true; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Hides{ get{ return 20; } }
		public override int Meat{ get{ return 19; } }
		public override int Scales{ get{ return 6; } }
		public override ScaleType ScaleType{ get{ return ( Utility.RandomBool() ? ScaleType.Black : ScaleType.White ); } }

		public KraytDragon( Serial serial ) : base( serial )
		{
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( !Core.SE && 0.2 > Utility.RandomDouble() && attacker is BaseCreature )
			{
				BaseCreature c = (BaseCreature)attacker;

				if ( c.Controlled && c.ControlMaster != null )
				{
					c.ControlTarget = c.ControlMaster;
					c.ControlOrder = OrderType.Attack;
					c.Combatant = c.ControlMaster;
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