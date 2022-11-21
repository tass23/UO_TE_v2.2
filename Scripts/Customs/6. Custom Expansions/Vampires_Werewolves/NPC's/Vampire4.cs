using System;
using Server;
using Server.Items;
using Server.Targeting;
namespace Server.Mobiles
{
	[CorpseName( "a vampire's corpse" )]
	public class Vampire4 : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public Vampire4() : base( AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the Vampire";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				VampLegWraps0 LegWraps = new VampLegWraps0();
				LegWraps.Name = "Vampiric Patrician's Legwraps";
				LegWraps.Movable = false;
				AddItem(LegWraps);
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				VampLegs0 Legs = new VampLegs0();
				Legs.Name = "Vampiric Patrician's Leggings";
				Legs.Movable = false;
				AddItem(Legs);
			}

			SetStr( 1000, 1200 );
			SetDex( 450, 600 );
			SetInt( 1200, 1500 );
			SetHits( 2400, 3200 );
			SetDamage( 20, 32 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );

			SetResistance( ResistanceType.Physical, 50, 80 );
			SetResistance( ResistanceType.Fire, 40, 70 );
			SetResistance( ResistanceType.Cold, 60, 90 );
			SetResistance( ResistanceType.Poison, 60, 90 );
			SetResistance( ResistanceType.Energy, 50, 80 );

			SetSkill( SkillName.EvalInt, 140.0, 165.0 );
			SetSkill( SkillName.Magery, 140.0, 165.0 );
			SetSkill( SkillName.Necromancy, 130.0, 160.0 );
			SetSkill( SkillName.MagicResist, 140.0, 180.0 );
			SetSkill( SkillName.Tactics, 160.0, 200.0 );
			SetSkill( SkillName.Wrestling, 160.1, 200.0 );
			SetSkill( SkillName.DetectHidden, 85.0, 95.0 );

			Fame = 2800;
			Karma = -2800;
			VirtualArmor = 85;

			VampGorget0 Gorget = new VampGorget0();
			Gorget.Movable = false;
			Gorget.Name = "Vampiric Patrician's Collar";
			AddItem(Gorget);

			VampChest0 Chest = new VampChest0();
			Chest.Name = "Vampiric Patrician's Tunic";
			Chest.Movable = false;
			AddItem(Chest);

			VampArms0 Arms = new VampArms0();
			Arms.Name = "Vampiric Patrician's Armguards";
			Arms.Movable = false;
			AddItem(Arms);

			VampGloves0 Gloves = new VampGloves0();
			Gloves.Movable = false;
			Gloves.Name = "Vampiric Patrician's Gloves";
			AddItem(Gloves);
			PackNecroReg( 12, 40 );

			Utility.AssignRandomHair( this );

			Tamable = false;
			ControlSlots = 4;
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
			switch ( Utility.Random( 4 ))
			{
				case 0: AddItem( new  BloodyFancyDress () ); break;
				case 1: AddItem( new  BloodyKamishimo  () ); break;
				case 2: AddItem( new  BloodyHakamaShita  () ); break;
				case 3: AddItem( new  BloodyPlainDress  () ); break;
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
					case 0: PackItem( new   VampArms4() ); break;
					case 1: PackItem( new  VampChest4() ); break;
					case 2: PackItem( new VampGloves4() ); break;
					case 3: PackItem( new VampGorget4() ); break;
					case 4:
					{
						 if (!this.Female)
							{PackItem( new VampLegs4() ); break;}
						else
							{PackItem( new VampLegWraps4() ); break;}
					}
				}
			}

			if (Utility.RandomDouble() < 0.06)
				//PackItem( new VampireMasterRing() );
			//else
				PackItem( new VampireSignetRing() );
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Blood; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem(new RewardScroll());
        }
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public Vampire4( Serial serial ) : base( serial )
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
			foreach (Mobile m in this.GetMobilesInRange(6))
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