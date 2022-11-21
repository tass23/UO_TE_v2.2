using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.SpellCrafting.Items;

namespace Server.Mobiles
{
	[CorpseName( "Delhi's Corpse" )]
	public class Delhi : BaseCreature
	{
		[Constructable]
		public Delhi() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Delhi";
			Title = "The Evil Spell Crafter";
			Body = 400;
			Hue = Utility.RandomSkinHue ();
			

	    	SetStr( 50 );
			SetDex( 100,150 );
			SetInt( 400 );

			SetHits( 1500 );

			SetDamage( 10, 16 );

			SetDamageType( ResistanceType.Physical, 100 );
			

			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, 60 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, 60 );

			SetSkill( SkillName.EvalInt, 90, 150 );
			SetSkill( SkillName.Magery, 100,150);
			SetSkill( SkillName.MagicResist, 70, 90 );
			
			SetSkill( SkillName.Meditation, 70, 110 );
			

			Fame = 0;
			Karma = -10000;

			VirtualArmor = 50;

			AddItem (new Robe (1392));
			AddItem (new StaffOfPower());
			AddItem (new Shoes ());
			int hairHue = 2018;
			
			switch (Utility.Random (1) )
			{
				case 0: AddItem (new LongHair ( hairHue ) ) ; break;
				
			}
			//PackItem( SpellCraft.RandomCraft( .35 ) );
			PackItem (new MagicJewel ( 10));
			PackGem();
			PackGold( 150, 300  );
			PackItem (new RareSCJewel());
			switch ( Utility.Random (16))
			{
				case 0: PackItem (new MagicJewel ()) ; break ;
				case 1: PackItem (new AttackChanceJewel ()); break;
				case 2: PackItem (new BonusDexterityJewel ()); break;
				case 3: PackItem (new BonusHitsJewel ());break;
				case 4: PackItem (new BonusIntelligenceJewel ());break;
				case 5: PackItem (new BonusManaJewel ());break;
				case 6: PackItem (new BonusManaJewel());break;
				case 7: PackItem (new BonusStaminaJewel ());break;
				case 8: PackItem (new BonusStrengthJewel ()) ; break;
				case 9: PackItem (new CastRecoveryJewel ());break;
				case 10: PackItem (new CastSpeedJewel ());break;
				case 11: PackItem (new ColdResistJewel ());break;
				case 12: PackItem (new DefendChanceJewel ()) ; break ;
				case 13: PackItem (new MagicJewel ()) ; break ;
				case 14: PackItem (new MagicJewel ()) ; break;
				case 15: PackItem (new MagicJewel ()); break;
			}
			switch (Utility.Random (48))
			{
				case 0: PackItem (new EnergyResistJewel ());break;
				case 1: PackItem (new EnhancePotionsJewel ()) ;break;
				case 2: PackItem (new MagicJewel ()) ; break;
				case 3: PackItem (new FireResistJewel ()); break;
				case 4: PackItem (new MagicJewel ()) ; break;
				case 5: PackItem (new MagicJewel ()) ; break;
				case 6: PackItem (new MagicJewel ()); break;
				case 7: PackItem (new MagicJewel ());break;
				case 8: PackItem (new MagicJewel ()) ;break;
				case 9: PackItem (new HitColdAreaJewel ()) ; break ;
				case 10: PackItem (new HitDispelJewel ());break;
				case 11: PackItem (new HitEnergyAreaJewel ());break;
				case 12: PackItem (new HitFireAreaJewel ());break;
				case 13: PackItem (new HitFireballJewel ());break;
				case 14: PackItem (new HitHarmJewel ());break;
				case 15: PackItem (new HitLeechHitsJewel ()) ;break;
				case 16: PackItem (new HitLeechManaJewel ()) ;break;
				case 17: PackItem (new HitLeechStamJewel ()) ;break;
				case 18: PackItem (new HitLeechStamJewel ()) ;break;
				case 19: PackItem (new HitLightningJewel ()) ;break;
				case 20: PackItem (new HitLowerAttackJewel ()) ;break;
				case 21: PackItem (new HitLowerDefendJewel ()) ;break;
				case 22: PackItem (new HitMagicArrowJewel ()) ;break;
				case 23: PackItem (new HitPhysicalAreaJewel ()) ;break;
				
					
			}
			switch (Utility.Random (96))
			{
				case 0: PackItem (new HitPoisonAreaJewel ()) ;break;
				case 1: PackItem (new MagicJewel ()) ;break;
				case 2: PackItem (new MagicJewel ()) ;break;
				case 3: PackItem (new LowerManaCostJewel ()) ;break;
				case 4: PackItem (new LowerReagentCostJewel ()) ;break;
				case 5: PackItem (new LowerStatRequirementsJewel ()) ;break;
				case 6: PackItem (new MagicJewel ()) ;break;
				case 7: PackItem (new LuckJewel ()) ;break;
				case 8: PackItem (new MageArmorJewel ()) ; break;
				case 9: PackItem (new MageWeaponJewel () ) ; break;
				case 10: PackItem (new MagicJewel ());break;
				case 11: PackItem (new PhysicalResistJewel () ) ;break;
				case 12: PackItem (new PoisonResistJewel () ) ;break;
				case 13: PackItem (new MagicJewel ()) ;break;
				case 14: PackItem (new ReflectPhysicalJewel ()) ;break;
				case 15: PackItem (new RegenerateHitsJewel ()) ; break ;
				case 16: PackItem (new RegenerateHitsJewel ()) ; break;
				case 17: PackItem (new RegenerateManaJewel ());break;
				case 18: PackItem (new RegenerateStaminaJewel ());break;
				case 19: PackItem (new MagicJewel ());break;
				case 20: PackItem (new SelfRepairJewel ()) ;break;
				case 21: PackItem (new SpellChannelingJewel ());break;
				case 22: PackItem (new SpellDamageJewel () );break;
				case 23: PackItem (new UseBestSkillJewel ()); break;
				case 24: PackItem (new WeaponDamageJewel ()); break;
				case 25: PackItem (new WeaponSpeedJewel ()); break;
				
					
			}
			        
			        
			
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			if ( Utility.RandomDouble() < 0.2 )			
				c.DropItem(new RewardScroll());
			if ( Utility.RandomDouble() < 0.2 )	
				c.DropItem( new RainbowMountToken3() );
		}
		public override bool AlwaysMurderer{ get{ return true; } }
		

		public Delhi( Serial serial ) : base( serial )
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
