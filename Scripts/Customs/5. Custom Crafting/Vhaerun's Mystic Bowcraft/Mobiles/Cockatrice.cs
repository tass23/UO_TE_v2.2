using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a cockatrice corpse" )]
	public class Cockatrice : BaseCreature
	{
		[Constructable]
		public Cockatrice() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a cockatrice";
			Body = 0xD0;
			BaseSoundID = 0x6E;
			Hue = Utility.RandomSkinHue();

			SetStr( 75, 125 );
			SetDex( 30, 90 );
			SetInt( 15, 50 );

			SetHits( 65, 100 );

			SetDamage( 4, 13 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 15, 30 );
			SetResistance( ResistanceType.Fire, 15, 30 );
			SetResistance( ResistanceType.Poison, 50, 80 );

			SetSkill( SkillName.MagicResist, 35.0, 75.0 );
			SetSkill( SkillName.Tactics, 35.0, 80.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );
			SetSkill( SkillName.Poisoning, 35.1, 50.1 );

			Fame = 950;
			Karma = -300;

			VirtualArmor = 15;

			PackItem( new PoisonFeather( 5 ) );

		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 20; } }

		public Cockatrice(Serial serial) : base(serial)
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