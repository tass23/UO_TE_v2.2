using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a large Nazi soldier's corpse" )]
	public class GermanBig : BaseCreature
	{
		[Constructable]
		public GermanBig() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a large Nazi Soldier";
			Body = 183;

			SetStr( 596, 615 );
			SetDex( 286, 305 );
			SetInt( 251, 265 );

			SetDamage( 34, 37 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Fencing, 200.0, 202.5 );
			SetSkill( SkillName.Macing, 200.0, 202.5 );
			SetSkill( SkillName.MagicResist, 199.5, 200.0 );
			SetSkill( SkillName.Swords, 200.0, 202.5 );
			SetSkill( SkillName.Tactics, 200.0, 202.5 );
			SetSkill( SkillName.Anatomy, 195.2, 200.0);

			Fame = 3000;
			Karma = -3000;

			PackItem( new Bandage( Utility.RandomMinMax( 100, 150 ) ) );
			
			Cutlass weapon = new Cutlass();
			weapon.Hue = 0x835;
			weapon.Movable = false;
			AddItem( weapon );

			EyesOfHate glasses = new EyesOfHate();
			glasses.Movable = false;
			AddItem( glasses );

			DragonChest chest = new DragonChest();
			chest.Resource = CraftResource.BlackScales;
			chest.Movable = false;
			AddItem( chest );

			DragonArms arms = new DragonArms();
			arms.Resource = CraftResource.BlackScales;
			arms.Movable = false;
			AddItem( arms );

			DragonGloves gloves = new DragonGloves();
			gloves.Resource = CraftResource.BlackScales;
			gloves.Movable = false;
			AddItem( gloves );

			DragonLegs legs = new DragonLegs();
			legs.Resource = CraftResource.BlackScales;
			legs.Movable = false;
			AddItem( legs );

			Boots boots = new Boots();
			boots.Movable = false;
			boots.Hue = 2051;
			AddItem( boots );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( to is Dragon || to is WhiteWyrm || to is SwampDragon || to is Drake || to is Nightmare || to is Hiryu || to is LesserHiryu || to is Daemon )
				damage *= 3;
		}

		public GermanBig( Serial serial ) : base( serial )
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