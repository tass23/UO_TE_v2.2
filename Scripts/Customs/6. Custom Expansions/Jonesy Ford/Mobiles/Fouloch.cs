using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "Fouloch's corpse" )]
	public class Fouloch : BaseCreature
	{
		[Constructable]
		public Fouloch() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Fouloch";
			Body = 183;

			SetStr( 496, 515 );
			SetDex( 186, 205 );
			SetInt( 351, 365 );

			SetDamage( 29, 33 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Fencing, 110.0, 120.0 );
			SetSkill( SkillName.Macing, 109.2, 115.5 );
			SetSkill( SkillName.Poisoning, 107.0, 113.5 );
			SetSkill( SkillName.MagicResist, 118.1, 120.0 );
			SetSkill( SkillName.Swords, 155.0, 160.0 );
			SetSkill( SkillName.Tactics, 130.0, 135.5 );

			Fame = 5000;
			Karma = -5000;

			PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );
			PackItem( new QuestStaffMedallion ());
			PackItem( new FakeStaffMedallion ());
			
			Cutlass weapon = new Cutlass();
			weapon.Hue = 0x835;
			weapon.Movable = false;
			AddItem( weapon );

			StuddedMempo helmet = new StuddedMempo();
			helmet.Movable = false;
			helmet.Hue = 2051;
			AddItem( helmet );

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

		public Fouloch( Serial serial ) : base( serial )
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