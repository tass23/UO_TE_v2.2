using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a heron corpse" )]
	public class Heron : BaseCreature
	{
		[Constructable]
		public Heron() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a heron";
			Body = 254;
			BaseSoundID = 0x4D7;

			SetStr( 20, 30 );
			SetDex( 15, 35 );
			SetInt( 10, 15 );

			SetHits( 20, 30 );

			SetDamage( 1, 2 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 5 );

			SetSkill( SkillName.MagicResist, 4.1, 5.0 );
			SetSkill( SkillName.Tactics, 5.1, 11.0 );
			SetSkill( SkillName.Wrestling, 5.1, 11.0 );

			Fame = 50;
			Karma = 100;

			VirtualArmor = 5;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 20.1;

			PackItem( new WaterFeather( 5 ) );
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 14; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }


		public override int GetAngerSound()
		{
			return 0x4D9;
		}

		public override int GetIdleSound()
		{
			return 0x4D8;
		}

		public override int GetAttackSound()
		{
			return 0x4D7;
		}

		public override int GetHurtSound()
		{
			return 0x4DA;
		}

		public override int GetDeathSound()
		{
			return 0x4D6;
		}

		public Heron(Serial serial) : base(serial)
		{
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