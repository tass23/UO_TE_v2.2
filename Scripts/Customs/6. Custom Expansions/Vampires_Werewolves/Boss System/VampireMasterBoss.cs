using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a vampire master's corpse" )]
	public class VampireMasterBoss: BaseExpanseBoss
	{
		[Constructable]
		public VampireMasterBoss() : base( AIType.AI_VampireLord )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the Vampire Master";
			Hue = Utility.RandomSkinHue();
			Body = 0x190;
			Name = "Gramen";
			Kills = 666;
				VampLegs0 Legs = new VampLegs0();
				Legs.Name = "Ancient Vampire's Leggings";
				Legs.Movable = false;
				AddItem(Legs);
				
				VampGorget0 Gorget = new VampGorget0();
				Gorget.Movable = false;
				Gorget.Name = "Ancient Vampire's Collar";
				AddItem(Gorget);

				VampChest0 Chest = new VampChest0();
				Chest.Name = "Ancient Vampire's Tunic";
				Chest.Movable = false;
				AddItem(Chest);

				VampArms0 Arms = new VampArms0();
				Arms.Name = "Ancient Vampire's Armguards";
				Arms.Movable = false;
				AddItem(Arms);

				VampGloves0 Gloves = new VampGloves0();
				Gloves.Movable = false;
				Gloves.Name = "Ancient Vampire's Gloves";
				AddItem(Gloves);

			SetStr( 1232, 1400 );
			SetDex( 76, 82 );
			SetInt( 576, 585 );

			SetHits( 50000 );

			SetDamage( 27, 31 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 75, 85 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 55, 65 );
			SetResistance( ResistanceType.Energy, 50, 60 );
			
			SetSkill( SkillName.Wrestling, 100.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.MagicResist, 75.0 );
			SetSkill( SkillName.Anatomy, 110.0 );
			SetSkill( SkillName.SpiritSpeak, 120.0 );
			SetSkill( SkillName.Focus, 100.0 );
			
			PackResources( 8 );
			
			switch ( Utility.Random( 5 ))
			{
				case 0: AddItem( new  BloodySkirt() ); break;
				case 1: AddItem( new  BloodyKilt() ); break;
				case 2: AddItem( new  BloodyHakama() ); break;
				default: break;
			}

			switch ( Utility.Random( 3 ))
			{

				case 0: AddItem( new  BloodyCloak() ); break;
				default: break;
			}
			switch ( Utility.Random( 15 ))
			{
//Assassin Only
				//case 0: AddItem( new  BloodyClothNinjaHood() ); break;
				case 1: AddItem( new  BloodyCap() ); break;
				case 2: AddItem( new  BloodySkullCap() ); break;
				case 3: AddItem( new  BloodyBandana() ); break;
				case 4: AddItem( new  BloodyBonnet() ); break;
// Archmage Only
				//case 5: AddItem( new  BloodyMagicWizardsHat() ); break;
				default: break;
			}
			switch ( Utility.Random( 10 ))
			{
				case 0: AddItem( new  BloodyFullApron() ); break;
				case 1: AddItem( new  BloodyDoublet() ); break;
// Knight Only	case 2: AddItem( new  BloodySurcoat() ); break;
// Knight Only	case 3: AddItem( new  BloodyTunic() ); break;
				case 4: AddItem( new  BloodyJinBaori() ); break;
				default: break;
			}
			switch ( Utility.Random( 6 ))
			{
				case 0: AddItem( new  BloodyFancyShirt() ); break;
//Assassin Only	
				//case 1: AddItem( new  BloodyClothNinjaJacket() ); break;
				case 2: AddItem( new  BloodyShirt() ); break;
				case 3: AddItem( new  BloodyElvenDarkShirt() ); break;	
				default: break;
			}
			switch ( Utility.Random( 8 ))
			{
				case 0: AddItem( new  BloodyThighBoots () ); break;
				case 1: AddItem( new  BloodySandals () ); break;
				case 2: AddItem( new  BloodyNinjaTabi  () ); break;
				case 3: AddItem( new  BloodyElvenBoots() ); break;
				case 4: AddItem( new  BloodyShoes() ); break;
				default: break;
			}
			switch ( Utility.Random( 15 ))
			{
				case 0: AddItem( new  BloodyWoodlandBelt() ); break;
				case 1: AddItem( new  BloodyObi() ); break;
				case 2: AddItem( new  BloodyHalfApron () ); break;
				default: break;
			}
// End Vampire Dressing Block

			if (Utility.RandomDouble() < 0.2)
			{

			}
			if (Utility.RandomDouble() < 0.05)
			{
				switch ( Utility.Random( 6 ))
				{
					case 0: PackItem( new VampArms5() ); break;
					case 1: PackItem( new VampChest5() ); break;
					case 2: PackItem( new VampGloves5() ); break;
					case 3: PackItem( new VampGorget5() ); break;
					case 4: PackItem( new VampLegWraps5() ); break;
					case 5: PackItem( new VampireAncientRobe() ); break;
				}
			}
		}
		
		public VampireMasterBoss( Serial serial ) : base( serial )
		{
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 4 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		

			c.DropItem( new RewardScroll() );
			
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new RewardScroll() );
			
			if ( Utility.RandomDouble() < 0.6 )				
				c.DropItem( new ParrotItem() );
		}

		public override bool GivesMinorArtifact{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }		
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		
		public override bool CanAreaPoison{ get{ return true; } }
		public override Poison HitAreaPoison{ get{ return Poison.Lethal; } }

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			base.OnDamage( amount, from, willKill );
			
			// eats pet or summons
			if ( from is BaseCreature )
			{
				BaseCreature creature = (BaseCreature) from;
				
				if ( creature.Controlled || creature.Summoned )
				{
					Heal( creature.Hits );					
					creature.Kill();				
					
					Effects.PlaySound( Location, Map, 0x574 );
				}
			}
			
			// teleports player near
			if ( from is PlayerMobile && !InRange( from.Location, 1 ) )
			{
				Combatant = from;
				
				from.MoveToWorld( GetSpawnPosition( 1 ), Map );				
				from.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
				from.PlaySound( 0x1FE );
			}
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