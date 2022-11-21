using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Crimson Raider's corpse" )]
	public class CrimsonRaider : BaseCreature
	{
		[Constructable]
		public CrimsonRaider() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "male" );
			Title = "a Crimson Raider";
			Body = 183;
			SetStr( 696, 715 );
			SetDex( 186, 205 );
			SetInt( 351, 365 );

			SetDamage( 13, 17 );
			SetDamageType( ResistanceType.Physical, 100 );
			SetSkill( SkillName.Fencing, 110.0, 120.0 );
			SetSkill( SkillName.Macing, 109.2, 115.5 );
			SetSkill( SkillName.Poisoning, 107.0, 113.5 );
			SetSkill( SkillName.MagicResist, 118.1, 120.0 );
			SetSkill( SkillName.Swords, 155.0, 160.0 );
			SetSkill( SkillName.Tactics, 130.0, 135.5 );

			Fame = 500;
			Karma = 500;
			
			Cutlass weapon = new Cutlass();
			weapon.Hue = 0x835;
			weapon.Movable = false;
			AddItem( weapon );

			FancyShirt tunic = new FancyShirt();
			tunic.Movable = false;
			AddItem( tunic );

			LeatherLegs legs = new LeatherLegs();
			legs.Movable = false;
			legs.Hue = 1484;
			AddItem( legs );

			Boots boots = new Boots();
			boots.Movable = false;
			boots.Hue = 2051;
			AddItem( boots );
			
			Cloak cloak = new Cloak();
			cloak.Movable = false;
			cloak.Hue = 2051;
			AddItem( cloak );

			PackGold( 50, 100 );
			
			switch( Utility.Random( 6 ) )
			{
				case 0:
					PackItem( new AncientStatue() );
					break;
				case 1:
					PackItem( new AncientBrush() );
					break;
				case 2:
					PackItem( new AncientLetter() );
					break;
				case 3:
					PackItem( new AncientSalt() );
					break;
				case 4:
					PackItem( new AncientPepper() );
					break;
				case 5:
					PackItem( new AncientJewels() );
					break;
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public CrimsonRaider( Serial serial ) : base( serial )
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