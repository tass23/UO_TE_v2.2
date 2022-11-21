using System;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a vampire's corpse" )]
	public class Vampire2 : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public Vampire2() : base( AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the Vampire";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				VampLegWraps0 LegWraps = new VampLegWraps0();
				LegWraps.Name = "Vampire's Legwraps";
				LegWraps.Movable = false;
				AddItem(LegWraps);
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				VampLegs0 Legs = new VampLegs0();
				Legs.Name = "Vampire's Leggings";
				Legs.Movable = false;
				AddItem(Legs);
			}

			SetStr( 600, 800 );
			SetDex( 250, 375 );
			SetInt( 700, 900 );
			SetHits( 1600, 2400 );
			SetDamage( 12, 24 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );

			SetResistance( ResistanceType.Physical, 30, 60 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 40, 70 );
			SetResistance( ResistanceType.Poison, 40, 70 );
			SetResistance( ResistanceType.Energy, 30, 60 );

			SetSkill( SkillName.EvalInt, 100.0, 140.0 );
			SetSkill( SkillName.Magery, 100.0, 140.0 );
			SetSkill( SkillName.Necromancy, 100.0, 140.0 );
			SetSkill( SkillName.SpiritSpeak, 120.0, 160.0 );
			SetSkill( SkillName.MagicResist, 80.0, 120.0 );
			SetSkill( SkillName.Tactics, 100.0, 140.0 );
			SetSkill( SkillName.Wrestling, 100.0, 140.0 );
			SetSkill( SkillName.DetectHidden, 70.0, 80.0 );

			Fame = 2000;
			Karma = -2000;
			VirtualArmor = 65;

			VampGorget0 Gorget = new VampGorget0();
			Gorget.Movable = false;
			Gorget.Name = "Vampire's Collar";
			AddItem(Gorget);

			VampChest0 Chest = new VampChest0();
			Chest.Name = "Vampire's Tunic";
			Chest.Movable = false;
			AddItem(Chest);

			VampArms0 Arms = new VampArms0();
			Arms.Name = "Vampire's Armguards";
			Arms.Movable = false;
			AddItem(Arms);

			VampGloves0 Gloves = new VampGloves0();
			Gloves.Movable = false;
			Gloves.Name = "Vampire's Gloves";
			AddItem(Gloves);

			PackNecroReg( 12, 40 );

			Utility.AssignRandomHair( this );

			Tamable = false;
			ControlSlots = 2;
			MinTameSkill = 0;

			// This dresses the Vampire, Note BloodyClothes are not movable and do not show up on the corpse
			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new  BloodySkirt() ); break;
				case 1: AddItem( new  BloodyKilt() ); break;
				case 2: AddItem( new  BloodyHakama() ); break;
				default: break;
			}
			switch ( Utility.Random( 2 ))
			{
				case 0: AddItem( new  BloodyCloak() ); break;
				default: break;
			}
			switch ( Utility.Random( 4 ))
			{
//Assassin Only			  case 0: AddItem( new  BloodyClothNinjaHood() ); break;
				case 0: AddItem( new  BloodyCap() ); break;
				case 1: AddItem( new  BloodySkullCap() ); break;
				case 2: AddItem( new  BloodyBandana() ); break;
				case 3: AddItem( new  BloodyBonnet() ); break;
// Archmage Only		  case 5: AddItem( new  BloodyMagicWizardsHat() ); break;
				  default: break;
			}
			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new  BloodyFullApron() ); break;
				case 1: AddItem( new  BloodyDoublet() ); break;
// Knight Only			  case 2: AddItem( new  BloodySurcoat() ); break;
// Knight Only			  case 3: AddItem( new  BloodyTunic() ); break;
				case 2: AddItem( new  BloodyJinBaori() ); break;
				default: break;
			}
			switch ( Utility.Random( 2 ))
			{
// Patrician Only		  case 0: AddItem( new  BloodyFancyDress () ); break;
// Patrician Only		  case 1: AddItem( new  BloodyKamishimo  () ); break;
				case 0: AddItem( new  BloodyHakamaShita  () ); break;
				case 1: AddItem( new  BloodyPlainDress  () ); break;
// Elder Only			  case 4: AddItem( new  BloodyMaleKimono  () ); break;
// Elder Only			  case 5: AddItem( new  BloodyFemaleKimono () ); break;
				default: break;
			}
			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new  BloodyFancyShirt() ); break;
//Assassin Only			  case 1: AddItem( new  BloodyClothNinjaJacket() ); break;
				case 1: AddItem( new  BloodyShirt() ); break;
				case 2: AddItem( new  BloodyElvenDarkShirt() ); break;	
				default: break;
			}
			switch ( Utility.Random( 5 ))
			{
				case 0: AddItem( new  BloodyThighBoots () ); break;
				case 1: AddItem( new  BloodySandals () ); break;
				case 2: AddItem( new  BloodyNinjaTabi  () ); break;
				case 3: AddItem( new  BloodyElvenBoots() ); break;
				case 4: AddItem( new  BloodyShoes() ); break;
				default: break;
			}
			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new  BloodyWoodlandBelt() ); break;
				case 1: AddItem( new  BloodyObi() ); break;
				case 2: AddItem( new  BloodyHalfApron () ); break;
				default: break;
			}
// End Vampire Dressing Block
			if (Utility.RandomDouble() < 0.2)
			{
				switch ( Utility.Random( 5 ))
				{
					case 0: PackItem( new   VampArms2() ); break;
					case 1: PackItem( new  VampChest2() ); break;
					case 2: PackItem( new VampGloves2() ); break;
					case 3: PackItem( new VampGorget2() ); break;
					case 4:
					{
						 if (!this.Female)
							{PackItem( new VampLegs2() ); break;}
						else
							{PackItem( new VampLegWraps2() ); break;}
					}
				}
			}

			if (Utility.RandomDouble() < 0.125)
				PackItem( new VampireSignetRing() );
		}
		public override FoodType FavoriteFood{ get{ return FoodType.Blood; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public Vampire2( Serial serial ) : base( serial )
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

// this destroys ring if a vampire is attacked

		public override void AggressiveAction(Mobile aggressor, bool criminal)
		{
			base.AggressiveAction(aggressor, criminal);
			Item item = aggressor.FindItemOnLayer(Layer.Ring);

			if (item is VampireSignetRing)
			{
				AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
				item.Delete();
				aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
				aggressor.PlaySound(0x307);
			}
			/*
			else if (item is VampireMasterRing)
			{
				AOS.Damage(aggressor, 200, 0, 100, 0, 0, 0);
				item.Delete();
				aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
				aggressor.PlaySound(0x307);
			}	
			*/
		}

// this keeps vampires from attacking

		public override bool IsEnemy(Mobile m)
		{
			if (m.Player && m.FindItemOnLayer(Layer.Ring) is VampireSignetRing)
				return false;
			/*
			else if (m.Player && m.FindItemOnLayer(Layer.Ring) is VampireMasterRing)
				return false;
			*/
			return base.IsEnemy(m);
		}
// these two allow vampires to reveal hidden players
		private Mobile FindTarget()
		{
			foreach (Mobile m in this.GetMobilesInRange(3))
			{
				if (m.Player && m.Hidden && m.AccessLevel == AccessLevel.Player)
				{
					return m;
				}
			}
			return null;
		}

		private void TryToDetectHidden()
		{
			Mobile m = FindTarget();

			if (m != null)
			{
				if (DateTime.Now >= this.NextSkillTime && UseSkill(SkillName.DetectHidden))
				{
					Target targ = this.Target;

					if (targ != null)
						targ.Invoke(this, this);

					Effects.PlaySound(this.Location, this.Map, 0x340);
				}
			}
		}

		public override void OnThink()
		{
			if (Utility.RandomDouble() < 0.2)
				TryToDetectHidden();
		}
	}
}