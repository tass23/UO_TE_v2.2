using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Muggee's corpse" )]
	public class Muggee : BaseCreature
	{
		[Constructable]
		public Muggee() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Muggee";
			Body = 183;

			SetStr( 596, 615 );
			SetDex( 196, 215 );
			SetInt( 351, 365 );

			SetDamage( 31, 35 );

			SetDamageType( ResistanceType.Physical, 100 );
			
			SetResistance( ResistanceType.Physical, 90, 95 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 45, 50 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.Fencing, 110.0, 120.0 );
			SetSkill( SkillName.Macing, 109.2, 115.5 );
			SetSkill( SkillName.Poisoning, 107.0, 113.5 );
			SetSkill( SkillName.MagicResist, 118.1, 120.0 );
			SetSkill( SkillName.Swords, 155.0, 160.0 );
			SetSkill( SkillName.Tactics, 130.0, 135.5 );

			Fame = 5000;
			Karma = 5000;

			PackItem( new Bandage( Utility.RandomMinMax( 20, 35 ) ) );
			PackItem( new ChildBracelet ());
			
			Scimitar weapon = new Scimitar();
			weapon.Hue = 0x835;
			AddItem( weapon );

			LeatherJingasa helmet = new LeatherJingasa();
			helmet.Hue = 2021;
			AddItem( helmet );

			BoneArms arms = new BoneArms();
			AddItem( arms );

			LeatherNinjaJacket tunic = new LeatherNinjaJacket();
			tunic.Hue = 233;
			AddItem( tunic );
			
			LeatherNinjaPants legs = new LeatherNinjaPants();
			legs.Hue = 233;
			AddItem( legs );

			ThighBoots boots = new ThighBoots();
			boots.Movable = false;
			AddItem( boots );
			
			BodySash sash = new BodySash();
			sash.Hue = 2021;
			AddItem( sash );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public Muggee( Serial serial ) : base( serial )
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