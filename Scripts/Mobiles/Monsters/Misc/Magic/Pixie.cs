using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a pixie corpse" )]
	public class Pixie : BaseCreature
	{
		public override bool InitialInnocent{ get{ return true; } }

		[Constructable]
		public Pixie() : base( AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "pixie" );
			Body = 128;
			BaseSoundID = 0x467;

			SetStr( 21, 30 );
			SetDex( 301, 400 );
			SetInt( 201, 250 );

			SetHits( 13, 18 );

			SetDamage( 9, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 10.1, 20.0 );
			SetSkill( SkillName.Wrestling, 10.1, 12.5 );

			Fame = 7000;
			Karma = 7000;

			VirtualArmor = 100;
			if ( 0.02 > Utility.RandomDouble() )
				PackStatue();				
		}
		
		public override bool OnBeforeDeath()
		{
			if ( !base.OnBeforeDeath() ) return false;

			if ( 0.50 > Utility.RandomDouble() )
			{
				Item itemloot;
				switch (Utility.RandomMinMax( 1, 25 ))
				{
					case  1: default: itemloot = new MagicalRuby(); break;
					case  2: itemloot = new MagicalJade(); break;
					case  3: itemloot = new MagicalSapphire(); break;
					case  4: itemloot = new MagicalCitrine(); break;
					case  5: itemloot = new MagicalAmethyst(); break;
					case  6: itemloot = new MagicalBloodStone(); break;
					case  7: itemloot = new MagicalBlueDiamond(); break;
					case  8: itemloot = new MagicalPearl(); break;
					case  9: itemloot = new MagicalOnyx(); break;
					case  10: itemloot = new MagicalEmerald(); break;
					case  11: itemloot = new MagicalStarRose(); break;
					case  12: itemloot = new MagicalStarSapphire(); break;
					case  13: itemloot = new MagicalTurquoise(); break;
					case  14: itemloot = new MagicalFireEmerald(); break;
					case  15: itemloot = new MagicalJasper(); break;
					case  16: itemloot = new MagicalDiamond(); break;
					case  17: itemloot = new MagicalEclipseStone(); break;
					case  18: itemloot = new MagicalMoonStoneGem(); break;
					case  19: itemloot = new MagicalSunStone(); break;
					case  20: itemloot = new MagicalAmber(); break;
					case  21: itemloot = new MagicalOpal(); break;
					case  22: itemloot = new MagicalTourmaline(); break;
					case  23: itemloot = new MagicalTopaz(); break;
					case  24: itemloot = new MagicalFireOpal(); break;
					case  25: itemloot = new MagicalStarRuby(); break;

				}
				if (itemloot != null)
					itemloot.MoveToWorld( Location, Map );
			}

			Effects.SendLocationEffect( Location, Map, 0x376A, 10, 1 );
			return base.OnBeforeDeath();
		}
		
		#region Mondain's Legacy
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
				
			if ( Utility.RandomDouble() < 0.3 )
				c.DropItem( new PixieLeg() );
		}
			#endregion

		public override void GenerateLoot()
		{
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Gems, 2 );
		}

		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Hides{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public Pixie( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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