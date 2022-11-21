using System;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a vampire's corpse" )]
	public class Vampire0 : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public Vampire0() : base( AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the Vampire";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{

				Body = 0x191;
				Name = NameList.RandomName( "female" );
				switch ( Utility.Random( 3 ))
				{
					case 0: AddItem( new  BloodySkirt() ); break;
					case 1: AddItem( new  BloodyKilt() ); break;
					case 2: AddItem( new  BloodyHakama() ); break;
					default: break;
				}
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				switch ( Utility.Random( 4 ))
				{
					case 0: AddItem( new  BloodyShortPants () ); break; 
					case 1: AddItem( new  BloodyLongPants  () ); break;
					case 2: AddItem( new  BloodyTattsukeHakama () ); break;
					case 3: AddItem( new  BloodyElvenPants () ); break;
					default: break;
				}
			}

			SetStr( 200, 450 );
			SetDex( 75, 225 );
			SetInt( 500, 700 );
			SetHits( 800, 1200 );
			SetDamage( 12, 24 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 60.0, 80.0 );
			SetSkill( SkillName.Magery, 40.0, 80.0 );
			SetSkill( SkillName.Necromancy, 60.0, 80.0 );
			SetSkill( SkillName.SpiritSpeak, 60.0, 100.0 );
			SetSkill( SkillName.MagicResist, 60.0, 80.0 );
			SetSkill( SkillName.Tactics, 60.0, 80.0 );
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );
			SetSkill( SkillName.DetectHidden, 85.0, 95.0 );
			SetSkill( SkillName.DetectHidden, 60.0, 70.0 );

			Fame = 800;
			Karma = -800;

			VirtualArmor = 45;
// This dresses the Vampire, Note BloodyClothes are not movable and do not show up on the corpse
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
			PackNecroReg( 12, 40 );
			Utility.AssignRandomHair( this );
		}

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
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public Vampire0( Serial serial ) : base( serial )
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
/* This secton removed since newborn vampires are ferral
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

			else if (item is VampireMasterRing)
			{
				AOS.Damage(aggressor, 200, 0, 100, 0, 0, 0);
				item.Delete();
				aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
				aggressor.PlaySound(0x307);
			}	

		}

// this keeps vampires from attacking

		public override bool IsEnemy(Mobile m)
		{
			if (m.Player && m.FindItemOnLayer(Layer.Ring) is VampireSignetRing)
				return false;

			else if (m.Player && m.FindItemOnLayer(Layer.Ring) is VampireMasterRing)
				return false;

			return base.IsEnemy(m);
		}
*/

// these two allow vampires to reveal hidden players
		private Mobile FindTarget()
		{
			foreach (Mobile m in this.GetMobilesInRange(1))
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