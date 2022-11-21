using System;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a werewolf corpse" )]
	public class Werewolf0 : BaseCreature
	{
		private bool m_TrueForm;

		[Constructable]
		public Werewolf0() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a werewolf";
			Body = 23;
			SetStr( 200, 450 );
			SetDex( 75, 225 );
			SetInt( 500, 700 );
			SetHits( 800, 1200 );
			SetDamage( 12, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 60.0, 80.0 );
			SetSkill( SkillName.Magery, 40.0, 80.0 );
			SetSkill( SkillName.Necromancy, 60.0, 80.0 );
			SetSkill( SkillName.SpiritSpeak, 40.0, 60.0 );
			SetSkill( SkillName.MagicResist, 60.0, 80.0 );
			SetSkill( SkillName.Tactics, 60.0, 80.0 );
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );
			SetSkill( SkillName.DetectHidden, 85.0, 95.0 );
			SetSkill( SkillName.DetectHidden, 60.0, 70.0 );

			VirtualArmor = 45;
		}

		public void Morph()
		{
			if (m_TrueForm)
				return;
				
			m_TrueForm = true;
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				switch ( Utility.Random( 5 ))
				{
					  case 0: AddItem( new  TatteredSkirt() ); break;
					  case 1: AddItem( new  TatteredKilt() ); break;
					  case 2: AddItem( new  TatteredHakama() ); break;
					  default: break;
				}
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				switch ( Utility.Random( 5 ))
				{
					case 0: AddItem( new  TatteredShortPants () ); break; 
					case 1: AddItem( new  TatteredLongPants  () ); break;
					case 2: AddItem( new  TatteredTattsukeHakama () ); break;
					case 3: AddItem( new  TatteredElvenPants () ); break;
					default: break;
				}
			}
	
			Title = "the Werewolf";
			SpeechHue = Utility.RandomDyedHue();
			Hue = Utility.RandomSkinHue();
			SetStr( 100, 225 );
			SetDex( 75, 175 );
			SetInt( 250, 350 );
			Hits = HitsMax;
			SetDamage( 6, 12 );
			Stam = StamMax;
			Mana = ManaMax;
			
			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 60.0, 80.0 );
			SetSkill( SkillName.Magery, 40.0, 80.0 );
			SetSkill( SkillName.Necromancy, 60.0, 80.0 );
			SetSkill( SkillName.SpiritSpeak, 40.0, 60.0 );
			SetSkill( SkillName.MagicResist, 60.0, 80.0 );
			SetSkill( SkillName.Tactics, 60.0, 80.0 );
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );
			SetSkill( SkillName.DetectHidden, 85.0, 95.0 );
			SetSkill( SkillName.DetectHidden, 60.0, 70.0 );

			Fame = 800;
			Karma = -800;
			VirtualArmor = 45;
			ProcessDelta();

			// This dresses the Werewolf, Note TatteredClothes are not movable and do not show up on the corpse
			switch ( Utility.Random( 2 ))
			{
				case 0: AddItem( new  TatteredCloak() ); break;
				default: break;
			}
			switch ( Utility.Random( 4 ))
			{
			//Assassin Only
				//case 0: AddItem( new  TatteredClothNinjaHood() ); break;
				case 0: AddItem( new  TatteredCap() ); break;
				case 1: AddItem( new  TatteredSkullCap() ); break;
				case 2: AddItem( new  TatteredBandana() ); break;
				case 3: AddItem( new  TatteredBonnet() ); break;
			// Archmage Only
				//case 5: AddItem( new  TatteredMagicWizardsHat() ); break;
				default: break;
			}
			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new  TatteredFullApron() ); break;
				case 1: AddItem( new  TatteredDoublet() ); break;
				// Knight Only
				//case 2: AddItem( new  TatteredSurcoat() ); break;
				// Knight Only
				//case 3: AddItem( new  TatteredTunic() ); break;
				case 2: AddItem( new  TatteredJinBaori() ); break;
				default: break;
			}
			switch ( Utility.Random( 3 ))
			{
				// Patrician Only
				//case 0: AddItem( new  TatteredFancyDress () ); break;
				// Patrician Only
				//case 1: AddItem( new  TatteredKamishimo  () ); break;
				case 0: AddItem( new  TatteredHakamaShita  () ); break;
				case 1: AddItem( new  TatteredPlainDress  () ); break;
				// Elder Only
				//case 4: AddItem( new  TatteredMaleKimono  () ); break;
				// Elder Only
				//case 5: AddItem( new  TatteredFemaleKimono () ); break;
				default: break;
			}

			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new  TatteredFancyShirt() ); break;
				//Assassin Only
				//case 1: AddItem( new  TatteredClothNinjaJacket() ); break;
				case 1: AddItem( new  TatteredShirt() ); break;
				case 2: AddItem( new  TatteredElvenDarkShirt() ); break;	
				default: break;
			}
			switch ( Utility.Random( 5 ))
			{
				case 0: AddItem( new  TatteredThighBoots () ); break;
				case 1: AddItem( new  TatteredSandals () ); break;
				case 2: AddItem( new  TatteredNinjaTabi  () ); break;
				case 3: AddItem( new  TatteredElvenBoots() ); break;
				case 4: AddItem( new  TatteredShoes() ); break;
				default: break;
			}
			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new  TatteredWoodlandBelt() ); break;
				case 1: AddItem( new  TatteredObi() ); break;
				case 2: AddItem( new  TatteredHalfApron () ); break;
				default: break;
			}

			Utility.AssignRandomHair( this );
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax{ get{ return m_TrueForm ? 400 : 600; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public override int ManaMax{ get{ return 100; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public Werewolf0( Serial serial ) : base( serial )
		{
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
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
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

		//these two allow werewolves to reveal hidden players
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