using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a pyrolisk corpse" )]
	public class Pyrolisk : BaseCreature
	{
		[Constructable]
		public Pyrolisk() : base( AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a pyrolisk";
			Body = 0xD0;
			BaseSoundID = 0x6E;
			Hue = Utility.RandomRedHue();

			SetStr( 90, 155 );
			SetDex( 50, 90 );
			SetInt( 30, 75 );

			SetHits( 80, 113 );

			SetDamage( 5, 15 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 20, 40 );
			SetResistance( ResistanceType.Fire, 55, 70 );
			SetResistance( ResistanceType.Poison, 10, 20 );

			SetSkill( SkillName.MagicResist, 55.0, 85.0 );
			SetSkill( SkillName.Tactics, 45.0, 70.0 );
			SetSkill( SkillName.Wrestling, 55.1, 70.0 );

			Fame = 2500;
			Karma = -2000;

			VirtualArmor = 16;

			PackItem( new FireFeather( 5 ) );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Hides{ get{ return 3; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Feathers{ get{ return 14; } }

		public Pyrolisk(Serial serial) : base(serial)
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