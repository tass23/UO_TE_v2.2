using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Nazi soldier's corpse" )]
	public class German : BaseCreature
	{
		[Constructable]
		public German() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Nazi Soldier";
			Body = 183;

			SetStr( 196, 215 );
			SetDex( 186, 205 );
			SetInt( 151, 165 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Fencing, 100.0, 102.5 );
			SetSkill( SkillName.Macing, 100.0, 102.5 );
			SetSkill( SkillName.Poisoning, 100.0, 102.5 );
			SetSkill( SkillName.MagicResist, 99.5, 100.0 );
			SetSkill( SkillName.Swords, 100.0, 102.5 );
			SetSkill( SkillName.Tactics, 100.0, 102.5 );

			Fame = 1000;
			Karma = -1000;

			PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );
			
			Cutlass weapon = new Cutlass();
			weapon.Hue = 0x835;
			weapon.Movable = false;
			AddItem( weapon );

			EyesOfHate glasses = new EyesOfHate();
			glasses.Movable = false;
			AddItem( glasses );

			LeatherArms arms = new LeatherArms();
			arms.Movable = false;
			arms.Hue = 2051;
			AddItem( arms );

			RingmailGloves gloves = new RingmailGloves();
			gloves.Movable = false;
			gloves.Hue = 2051;
			AddItem( gloves );

			LeatherNinjaJacket tunic = new LeatherNinjaJacket();
			tunic.Movable = false;
			tunic.Hue = 2051;
			AddItem( tunic );

			LeatherGorget gorget = new LeatherGorget();
			gorget.Movable = false;
			gorget.Hue = 2051;
			AddItem( gorget );
			
			LeatherLegs legs = new LeatherLegs();
			legs.Movable = false;
			legs.Hue = 2051;
			AddItem( legs );

			Boots boots = new Boots();
			boots.Movable = false;
			boots.Hue = 2051;
			AddItem( boots );
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

		public German( Serial serial ) : base( serial )
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