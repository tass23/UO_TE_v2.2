using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class EvilGenetecist : BaseCreature
	{
		[Constructable]
		public EvilGenetecist() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the evil genetecist";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

			SetStr( 200, 275 );
			SetDex( 115, 125 );
			SetInt( 200, 275 );

			SetHits( 1900, 2100 );
			SetMana( 1000 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.Fencing, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );
			SetSkill( SkillName.EvalInt, 55.0, 67.5 );
			SetSkill( SkillName.Magery, 90.0, 105.5 );

			Fame = 5000;
			Karma = -5000;

			AddItem( new Boots() );

			int hue = Utility.RandomMinMax( 1410, 1450 );  //Random Zog Green

			Item robe = new Robe();
			robe.Hue = hue;
			AddItem( robe );

			Item chest = new LeatherChest();
			chest.Hue = hue;
			AddItem( chest );

			Item gloves = new LeatherGloves();
			gloves.Hue = hue;
			AddItem( gloves );

			Item gorget = new LeatherGorget();
			gorget.Hue = hue;
			AddItem( gorget );

			Item legs = new LeatherLegs();
			legs.Hue = hue;
			AddItem( legs );

			Item arms = new LeatherArms();
			arms.Hue = hue;
			AddItem( arms );

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			PackItem( new Organics( Utility.RandomMinMax( 250, 400 ) ) );

			if ( Utility.Random( 100 ) < 35 )
			{
				switch ( Utility.Random( 2 ))
				{
					case 0: PackItem( new EmptyDNAVial() ); break;
					case 1: PackItem( new EmptyDNAVialSet() ); break;
				}
			}

			if ( Utility.Random( 500 ) < 3 )
			{
				DNAItem dna = new DNAItem();

				switch ( Utility.Random( 5 ) )
				{
					case 0:
					dna.DNAName = "a dark father";
					dna.DNAType = DNAType.Prowess;
					dna.DNAQuality = DNAQuality.VeryHigh;
					dna.DNAStr = 500;
					dna.DNAHits = 30000;
					dna.DNADamageMin = 17;
					dna.DNADamageMax = 21;
					dna.DNAArmor = 64;
					dna.DNAPhysicalResist = 30; 
					dna.DNATactics = 100;
					dna.DNAWrestling = 100;
					PackItem( dna );
					break;

					case 1:
					dna.DNAName = "a fox guardian";
					dna.DNAType = DNAType.Environment;
					dna.DNAQuality = DNAQuality.VeryHigh;
					dna.DNADex = 275;
					dna.DNAStam = 300;
					dna.DNAColdResist = 30; 
					dna.DNAFireResist = 60; 
					dna.DNAPoisoning = 100;
					PackItem( dna );
					break;

					case 2:
					dna.DNAName = "Mondain The Wizard";
					dna.DNAType = DNAType.Mental;
					dna.DNAQuality = DNAQuality.VeryHigh;
					dna.DNAInt = 415;
					dna.DNAMana = 1000;
					dna.DNAEnergyResist = 70; 
					dna.DNAPoisonResist = 70;
					dna.DNAMagery = 100;
					dna.DNAEvalInt = 100;
					PackItem( dna );
					break;

					case 3:
					dna.DNAName = "a hell chicken";
					dna.DNAType = DNAType.Mimic;
					dna.DNABodyValue = 208;
					dna.DNASoundID = 110;
					dna.DNAHealAttack = true;
					dna.DNATrialByFire = true;
					PackItem( dna );
					break;
					
					case 4: PackItem( new BioEnginerBook() ); break;
					
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public EvilGenetecist( Serial serial ) : base( serial )
		{
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.25 >= Utility.RandomDouble() && attacker is BaseCreature )
			{
				BaseCreature c = (BaseCreature)attacker;

				if ( c.Controlled && c.ControlMaster != null )
				{
					c.ControlMaster.SendMessage( "The genetecist tried to extract DNA from your pet but failed killing your pet." );
					this.Combatant = c.ControlMaster;
					c.Kill();
					this.Emote( "Tries to extract DNA from {0}", c.Name );

				}
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