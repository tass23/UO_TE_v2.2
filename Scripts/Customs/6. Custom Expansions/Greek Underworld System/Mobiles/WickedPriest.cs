using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a human corpse" )]
	public class WickedPriest : BaseCreature
	{
		private DateTime m_NextAbility;

		[Constructable]
		public WickedPriest() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "male" );
			Title = "the Wicked Priest";
			Body = 400;
			Female = false;
			Hue = 0;

			SetStr( 200 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 500 );

			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 30 );
			SetResistance( ResistanceType.Fire, 50 );
			SetResistance( ResistanceType.Cold, 40 );
			SetResistance( ResistanceType.Poison, 30 );
			SetResistance( ResistanceType.Energy, 40 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 190.0 );
			SetSkill( SkillName.Fencing, 190.0 );
			SetSkill( SkillName.Anatomy, 190.0 );
			SetSkill( SkillName.Magery, 250.0 );
			SetSkill( SkillName.EvalInt, 250.0 );

			AddItem( new HoodedShroudOfShadows() );
			AddItem( new Sandals() );
			AddItem( new DoubleBladedStaff() );

			Fame = 5000;
			Karma = -5000;
			VirtualArmor = 40;
			int chance = Utility.Random( 1, 100 );

			if ( chance <= 10 )
				PackItem( new RitualCoin( Utility.Random( 1, 5 )));
		}

		public override bool AlwaysMurderer{ get{ return true; } }


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 1 );
		}

		public WickedPriest( Serial serial ) : base( serial )
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