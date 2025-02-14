﻿using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Fairy dragon corpse" )]
	public class FairyDragon : BaseCreature
	{
		[Constructable]
		public FairyDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Fairy Dragon";
			Body = 718;
			BaseSoundID = 362;

			SetStr( 512, 558 );
			SetDex( 95, 105 );
			SetInt( 455, 501 );

			SetHits( 398, 403 );

			SetDamage( 15, 18 );

			//SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Fire, 20, 25 );
			SetDamageType( ResistanceType.Cold, 20, 25 );
			SetDamageType( ResistanceType.Poison, 20, 25 );
			SetDamageType( ResistanceType.Energy, 20, 25 );

			SetResistance( ResistanceType.Physical, 16, 30 );
			SetResistance( ResistanceType.Fire, 41, 44 );
			SetResistance( ResistanceType.Cold, 40, 49 );
			SetResistance( ResistanceType.Poison, 40, 49 );
			SetResistance( ResistanceType.Energy, 45, 47 );

			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 60.6, 68.2 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 39;
			
			PackItem( new FaeryDust() );
	// edit for arti drop?		
			if ( Utility.Random( 100 ) < 25 )
			{
				switch ( Utility.Random( 2 ))
				{
					case 0: PackItem( new FeyWings() ); break;
					case 1: PackItem( new FairyDragonWing() ); break;
				    case 3: PackItem( new SignOfChaos() ); break;
				}
		}

			Tamable = false;
			ControlSlots = 3;
			MinTameSkill = 93.9;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		//public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 9; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }
		public override double HitPoisonChance{ get{ return 0.75; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }
		
		public override int GetAttackSound()
		{
			return 0x5E9;
		}

		//public override int GetAngerSound()
		//{
		//	return 718;
		//}

		public override int GetDeathSound()
		{
			return 0x5EA;
		}

		public override int GetHurtSound()
		{
			return 0x5EB;
		}

		public override int GetIdleSound()
		{
			return 0x5EC;
		}

		public FairyDragon( Serial serial ) : base( serial )
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
