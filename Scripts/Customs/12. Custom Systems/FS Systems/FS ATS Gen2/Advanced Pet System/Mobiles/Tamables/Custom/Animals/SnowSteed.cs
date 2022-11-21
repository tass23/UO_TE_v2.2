using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a snow steed corpse" )]
	public class SnowSteed : BaseMount
	{
		public override bool InitialInnocent{ get{ return true; } }

		private static int[] m_IDs = new int[]
		{
			0xC8, 0x3E9F,
			0xE4, 0x3EA1
		};

		[Constructable]
		public SnowSteed() : this( "a snow steed" )
		{
		}

		[Constructable]
		public SnowSteed( string name ) : base( name, 0xE4, 0x3EA0, AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4 )
		{
			int random = Utility.Random( 2 );

			Body = m_IDs[random * 2];
			ItemID = m_IDs[random * 2 + 1];
			BaseSoundID = 0xA8;
			Hue = Utility.RandomList( 1150, 1153 );

			SetStr( 72, 115 );
			SetDex( 66, 90 );
			SetInt( 82, 165 );

			SetHits( 150, 185 );
			SetMana( 75, 100 );

			SetDamage( 5, 7 );

			SetDamageType( ResistanceType.Cold, 100 );

			SetResistance( ResistanceType.Physical, 25, 40 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 70, 90 );
			SetResistance( ResistanceType.Energy, 10, 20 );
			SetResistance( ResistanceType.Poison, 10, 20 );

			SetSkill( SkillName.MagicResist, 52.1, 72.0 );
			SetSkill( SkillName.Tactics, 49.3, 52.0 );
			SetSkill( SkillName.Wrestling, 49.3, 52.0 );
			SetSkill( SkillName.Magery, 15.3, 25.0 );
			SetSkill( SkillName.EvalInt, 15.3, 25.0 );

			Fame = 300;
			Karma = 300;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 62.1;

			int totalstats = this.Str + this.Dex + this.Int + this.HitsMax + this.StamMax + this.ManaMax + this.PhysicalResistance + this.FireResistance + this.ColdResistance + this.EnergyResistance + this.PoisonResistance + this.DamageMin + this.DamageMax + this.VirtualArmor;
			int nextlevel = totalstats * 10;

			this.NextLevel = nextlevel;
		}

		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public SnowSteed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}