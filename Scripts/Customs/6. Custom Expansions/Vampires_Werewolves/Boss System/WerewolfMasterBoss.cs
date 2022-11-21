using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a werewolf master's corpse" )]
	public class WerewolfMasterBoss: BaseExpanseBoss
	{
		private bool m_TrueForm;
		
		[Constructable]
		public WerewolfMasterBoss() : base( AIType.AI_WerewolfLord )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the Werewolf Master";
			Hue = Utility.RandomSkinHue();
			Body = 719;
			Name = "Dorval";

			SetStr( 1232, 1400 );
			SetDex( 76, 82 );
			SetInt( 576, 585 );

			SetHits( 50000 );

			SetDamage( 30, 36 );

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
			SetSkill( SkillName.SpiritSpeak, 90.0 );
			SetSkill( SkillName.Focus, 100.0 );
			SetSkill( SkillName.AnimalLore, 120.0 );
		}
        public void Morph()
        {
            if (m_TrueForm)
                return;

            m_TrueForm = true;

            Name = "Dorval";
			Title = "the Werewolf Master";
            Body = 0x190;

            SetStr(586, 700);
            Hits = 25000;
            Stam = StamMax;
            Mana = ManaMax;

            SetDamage(26, 31);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 10, 15);

			SetSkill( SkillName.Wrestling, 85.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.MagicResist, 75.0 );
			SetSkill( SkillName.Anatomy, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.0 );
			SetSkill( SkillName.Focus, 100.0 );
			SetSkill( SkillName.AnimalLore, 120.0 );

            Fame = 250;
            Karma = -2500;

            VirtualArmor = 45;
            ProcessDelta();
			
				WerewolfLegs1 Legs = new WerewolfLegs1();
				Legs.Name = "Ancient Werewolf's Leggings";
				Legs.Movable = false;
				AddItem(Legs);
				
				WerewolfGorget1 Gorget = new WerewolfGorget1();
				Gorget.Movable = false;
				Gorget.Name = "Ancient Werewolf's Collar";
				AddItem(Gorget);

				WerewolfChest1 Chest = new WerewolfChest1();
				Chest.Name = "Ancient Werewolf's Tunic";
				Chest.Movable = false;
				AddItem(Chest);

				WerewolfArms1 Arms = new WerewolfArms1();
				Arms.Name = "Ancient Werewolf's Armguards";
				Arms.Movable = false;
				AddItem(Arms);

				WerewolfGloves1 Gloves = new WerewolfGloves1();
				Gloves.Movable = false;
				Gloves.Name = "Ancient Werewolf's Gloves";
				AddItem(Gloves);
				
			PackResources( 8 );
				
			switch ( Utility.Random( 5 ))
			{
				case 0: AddItem( new  TatteredSkirt() ); break;
				case 1: AddItem( new  TatteredKilt() ); break;
				case 2: AddItem( new  TatteredHakama() ); break;
				default: break;
			}

			switch ( Utility.Random( 3 ))
			{

				case 0: AddItem( new  TatteredCloak() ); break;
				default: break;
			}
			switch ( Utility.Random( 15 ))
			{
//Assassin Only
				//case 0: AddItem( new  TatteredClothNinjaHood() ); break;
				case 1: AddItem( new  TatteredCap() ); break;
				case 2: AddItem( new  TatteredSkullCap() ); break;
				case 3: AddItem( new  TatteredBandana() ); break;
				case 4: AddItem( new  TatteredBonnet() ); break;
// Archmage Only
				//case 5: AddItem( new  TatteredMagicWizardsHat() ); break;
				default: break;
			}
			switch ( Utility.Random( 10 ))
			{
				case 0: AddItem( new  TatteredFullApron() ); break;
				case 1: AddItem( new  TatteredDoublet() ); break;
// Knight Only	case 2: AddItem( new  TatteredSurcoat() ); break;
// Knight Only	case 3: AddItem( new  TatteredTunic() ); break;
				case 4: AddItem( new  TatteredJinBaori() ); break;
				default: break;
			}
			switch ( Utility.Random( 6 ))
			{
				case 0: AddItem( new  TatteredFancyShirt() ); break;
//Assassin Only	
				//case 1: AddItem( new  TatteredClothNinjaJacket() ); break;
				case 2: AddItem( new  TatteredShirt() ); break;
				case 3: AddItem( new  TatteredElvenDarkShirt() ); break;	
				default: break;
			}
			switch ( Utility.Random( 8 ))
			{
				case 0: AddItem( new  TatteredThighBoots () ); break;
				case 1: AddItem( new  TatteredSandals () ); break;
				case 2: AddItem( new  TatteredNinjaTabi  () ); break;
				case 3: AddItem( new  TatteredElvenBoots() ); break;
				case 4: AddItem( new  TatteredShoes() ); break;
				default: break;
			}
			switch ( Utility.Random( 15 ))
			{
				case 0: AddItem( new  TatteredWoodlandBelt() ); break;
				case 1: AddItem( new  TatteredObi() ); break;
				case 2: AddItem( new  TatteredHalfApron () ); break;
				default: break;
			}
// End Werewolf Dressing Block
			if (Utility.RandomDouble() < 0.05)
			{
				switch ( Utility.Random( 6 ))
				{
					case 0: PackItem( new   WerewolfArms5() ); break;
					case 1: PackItem( new  WerewolfChest5() ); break;
					case 2: PackItem( new WerewolfGloves5() ); break;
					case 3: PackItem( new WerewolfGorget5() ); break;
					case 4: PackItem( new WerewolfLegWraps5() ); break;
					case 5: PackItem( new WerewolfAncientRobe() ); break;
				}
			}
        }

		[CommandProperty( AccessLevel.GameMaster )]
		public override int ManaMax{ get{ return 5000; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public WerewolfMasterBoss( Serial serial ) : base( serial )
		{
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 4 );
		}
		public override bool OnBeforeDeath()
		{
			if ( m_TrueForm )
			{
				return base.OnBeforeDeath();
			}
			else
			{
				Morph();
				return false;
			}
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
			
			writer.Write( m_TrueForm );	
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch ( version )
			{
				case 0:
				{
					m_TrueForm = reader.ReadBool();

					break;
				}
			}
		}
	}
}