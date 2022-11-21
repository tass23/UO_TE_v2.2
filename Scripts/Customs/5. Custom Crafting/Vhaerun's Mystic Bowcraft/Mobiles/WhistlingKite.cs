using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a whistling kite corpse" )]
	public class WhistlingKite : BaseCreature
	{
		[Constructable]
		public WhistlingKite() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a whistling kite";
			Body = 5;
			BaseSoundID = 0x2EE;
			Hue = 752;

			SetStr( 21, 45 );
			SetDex( 55, 75 );
			SetInt( 8, 20 );

			SetHits( 15, 30 );
			SetMana( 0 );

			SetDamage( 3, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20 );

			SetSkill( SkillName.MagicResist, 15.3, 20.0 );
			SetSkill( SkillName.Tactics, 10.1, 27.0 );
			SetSkill( SkillName.Wrestling, 20.1, 25.0 );

			Fame = 100;
			Karma = 0;

			VirtualArmor = 18;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 14.1;

			PackItem( new AirFeather( 5 ) );
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public WhistlingKite(Serial serial) : base(serial)
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