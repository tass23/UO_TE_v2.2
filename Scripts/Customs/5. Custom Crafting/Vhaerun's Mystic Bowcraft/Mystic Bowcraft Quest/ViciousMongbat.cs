using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a Vicious Mongbat corpse" )]
	public class ViciousMongbat : BaseCreature
	{
		[Constructable]
		public ViciousMongbat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Vicious Mongbat";
			Body = 39;
			BaseSoundID = 422;

			SetStr( 65, 120 );
			SetDex( 45, 80 );
			SetInt( 35, 70 );

			SetHits( 40, 75 );

			SetDamage( 4, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 40 );

			SetSkill( SkillName.MagicResist, 25.1, 50.0 );
			SetSkill( SkillName.Tactics, 25.1, 50.0 );
			SetSkill( SkillName.Wrestling, 25.1, 50.0 );
			SetSkill( SkillName.Anatomy, 20.1, 30.0 );

			Fame = 800;
			Karma = -800;

			VirtualArmor = 22;

			if( Utility.RandomDouble() < .40 )
				PackItem( new ColdIronIngot() );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Hides{ get{ return 1; } }

		public ViciousMongbat( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}