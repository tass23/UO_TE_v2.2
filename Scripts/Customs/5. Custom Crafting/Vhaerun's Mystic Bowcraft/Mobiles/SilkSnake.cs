using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a silk snake corpse" )]
	public class SilkSnake : BaseCreature
	{
		[Constructable]
		public SilkSnake() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a silk snake";
			Body = 52;
			Hue = 0x47E;
			BaseSoundID = 0xDB;

			SetStr( 56, 86 );
			SetDex( 36, 65 );
			SetInt( 6, 10 );

			SetHits( 60, 85 );

			SetDamage( 2, 6 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Poison, 20, 30 );

			SetSkill( SkillName.Poisoning, 50.1, 70.0 );
			SetSkill( SkillName.MagicResist, 15.1, 40.0 );
			SetSkill( SkillName.Tactics, 19.3, 34.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 700;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 65.1;

			PackItem( new SilkThread( Utility.RandomMinMax( 1, 3 ) ) );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }

		public override bool DeathAdderCharmable{ get{ return true; } }

		public override FoodType FavoriteFood{ get{ return FoodType.Eggs; } }

		public SilkSnake(Serial serial) : base(serial)
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