using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "Stevens' corpse" )]
	public class Stevens : BaseCreature
	{
		[Constructable]
		public Stevens() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Stevens";
			Body = 183;

			SetStr( 696, 715 );
			SetDex( 186, 205 );
			SetInt( 351, 365 );

			SetDamage( 33, 37 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Fencing, 110.0, 120.0 );
			SetSkill( SkillName.Macing, 109.2, 115.5 );
			SetSkill( SkillName.Poisoning, 107.0, 113.5 );
			SetSkill( SkillName.MagicResist, 118.1, 120.0 );
			SetSkill( SkillName.Swords, 155.0, 160.0 );
			SetSkill( SkillName.Tactics, 130.0, 135.5 );

			Fame = 5000;
			Karma = 5000;
			
			Cutlass weapon = new Cutlass();
			weapon.Hue = 0x835;
			weapon.Movable = false;
			AddItem( weapon );

			FancyShirt tunic = new FancyShirt();
			tunic.Movable = false;
			AddItem( tunic );

			LongPants legs = new LongPants();
			legs.Movable = false;
			legs.Hue = 2051;
			AddItem( legs );

			Boots boots = new Boots();
			boots.Movable = false;
			boots.Hue = 2051;
			AddItem( boots );
			
			Cloak cloak = new Cloak();
			cloak.Movable = false;
			cloak.Hue = 2051;
			AddItem( cloak );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( to is Dragon || to is WhiteWyrm || to is SwampDragon || to is Drake || to is Nightmare || to is Hiryu || to is LesserHiryu || to is Daemon )
				damage *= 3;
		}

		public Stevens( Serial serial ) : base( serial )
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