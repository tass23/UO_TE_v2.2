using System;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a vampire succubus" )]
	public class VampireStripper : BaseCreature
	{
	
		public override bool AlwaysMurderer { get { return true; } }
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public VampireStripper() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.5, 2 )
		{
			SpeechHue = Utility.RandomDyedHue();
			InitStats( 50, 50, 25 );
			//Title = "The VampireStripper";
			Hue = Utility.RandomSkinHue();
			Body = 0x191;
			Name = "The Vampire Succubus"; //NameList.RandomName( "female" );
			
			AddItem( new BunsHair( Utility.RandomRedHue() ) );
			AddItem( new Backpack() );
				switch ( Utility.Random( 2 ) )
				{
					case 0: AddItem( new Bonnet( Utility.RandomBlueHue() ) ); break;
					default: case 1: AddItem( new FeatheredHat( Utility.RandomBlueHue() ) ); break;
				}
				switch ( Utility.Random( 2 ) )
				{
					case 0: AddItem( new ThighBoots( Utility.RandomGreenHue() ) ); break;
					default: case 1: AddItem( new Sandals( Utility.RandomGreenHue() ) ); break;
				}
			AddItem( new FancyShirt( Utility.RandomBlueHue() ) );
			AddItem( new Doublet( Utility.RandomBlueHue() ) );
			AddItem( new Cloak( Utility.RandomGreenHue() ) );
			Item gloves = new LeatherGloves();
			gloves.Hue = Utility.RandomBlueHue();
			AddItem( gloves );
			AddItem( new ShortPants( Utility.RandomBlueHue() ) );
			AddItem( new VampireStrippersLingerie());
			Item skirt;
				switch ( Utility.Random( 2 ) )
				{
					case 0: skirt = new Skirt(); break;
					default: case 1: skirt = new Kilt(); break;
				}
			skirt.Hue = Utility.RandomGreenHue();
			AddItem( skirt );
			AddItem( new SilverRing() );
			AddItem( new SilverEarrings() );
			AddItem( new SilverBracelet() );
			AddItem( new SilverNecklace() ); 
		}

		public override bool ShowFameTitle{ get{ return false; } }	

		public VampireStripper( Serial serial ) : base( serial )
		{
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


//--Stripers Lingerie---------------------------------------------------------------------

namespace Server.Items
{
	[FlipableAttribute( 0x1c06, 0x1c07 )]
	public class VampireStrippersLingerie : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 25; } }
		public override int OldStrReq{ get{ return 15; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		public override bool AllowMaleWearer{ get{ return false; } }

		[Constructable]
		public VampireStrippersLingerie() : base( 0x1C06 )
		{
			Name = "Vampire Succubus Lingerie";
      		Hue = Utility.RandomList( 0x1, 0x17, 0xEE  );
			Weight = 1.0;
		}

		public VampireStrippersLingerie( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}

