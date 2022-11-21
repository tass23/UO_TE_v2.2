using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a troll corpse" )]
	public class WarTroll : BaseCreature
	{
		private Ares owner;
		[Constructable]
		public WarTroll ( Ares own ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			owner = own;
			Name = "a troll";
			Body = Utility.RandomList( 53, 54 );
			BaseSoundID = 461;

			SetStr( 176, 205 );
			SetDex( 46, 65 );
			SetInt( 46, 70 );

			SetHits( 1060, 1230 );

			SetDamage( 20, 30 );

			this.Hue = 2949;

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 15 );
			SetResistance( ResistanceType.Energy, 5, 15 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;
			int chance = Utility.Random( 1, 100 );
			if ( chance == 1 )
			{
				int chance2 = Utility.Random( 1, 6 );
				if ( chance2 == 1 )
					PackItem( new AresChest() );
				if ( chance2 == 2 )
					PackItem( new AresLegs() );
				if ( chance2 == 3 )
					PackItem( new AresArms() );
				if ( chance2 == 4 )
					PackItem( new AresCloak() );
				if ( chance2 == 5 )
					PackItem( new AresGloves() );
				if ( chance2 == 6 )
					PackItem( new AresBoots() );
			}
		}

		public override bool OnBeforeDeath()
		{
			if ( this.owner != null )
			{
				this.owner.AddKill();
			}

			return base.OnBeforeDeath();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 2; } }

		public WarTroll( Serial serial ) : base( serial )
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