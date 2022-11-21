using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a turkey corpse" )]
	public class Turkey : BaseCreature
	{
		[Constructable]
		public Turkey() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a turkey";
			Body = 0xD0;
			BaseSoundID = 0x6E;

			SetStr( 100 );
			SetDex( 100 );
			SetInt( 100 );

			SetHits( 1000, 3000 );

			SetDamage( 30, 45 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 95 );

			SetSkill( SkillName.EvalInt, 10.4, 50.0 );
			SetSkill( SkillName.Magery, 10.4, 50.0 );
			SetSkill( SkillName.MagicResist, 85.3, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 80.5, 92.5 );

			Fame = 14000;
			Karma = -14000;

			VirtualArmor = 60;

			Tamable = false;
			ControlSlots = 5;
			MinTameSkill = 99.9;
		}
		
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }

		public override int Feathers{ get{ return 25; } }

		public Turkey(Serial serial) : base(serial)
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			this.Say( "I will not be your dinner! Death from above!!");
			
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