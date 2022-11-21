using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class DeadlightPetImprisonedInCrystal : BaseImprisonedMobile
	{
		public override BaseCreature Summon{ get{ return new DeadlightPet(); } }
		
		[Constructable]
		public DeadlightPetImprisonedInCrystal() : base( 0x1F1C )
		{
			Name = "a Deadlight imprisoned in crystal";
			Weight = 1.0;
			Hue = 1761;
		}

		public DeadlightPetImprisonedInCrystal( Serial serial ) : base( serial )
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

namespace Server.Mobiles
{
	[CorpseName( "a Deadlight corpse" )]
	public class DeadlightPet : BaseCreature
	{
		public override bool DeleteOnRelease{ get{ return true; } }
		
		[Constructable]
		public DeadlightPet() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "daemon" );
			Title = " a Deadlight zombie";
			Body = 3;
			BaseSoundID = 471;

			if ( 0.5 >= Utility.RandomDouble() )
				Hue = Utility.RandomAnimalHue();

			SetStr( 6, 10 );
			SetDex( 26, 38 );
			SetInt( 6, 14 );
			SetHits( 4, 6 );
			SetMana( 0 );
			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 5, 10 );
			SetSkill( SkillName.MagicResist, 5.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 15;
			Karma = 0;
			VirtualArmor = 6;
			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -18.9;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public DeadlightPet(Serial serial) : base(serial)
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1049646 ); // (summoned)
		}
		
		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}		

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}