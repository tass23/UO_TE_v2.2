using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class FireDragon : BaseCreature
	{
		private static string[] m_RandName = new string[]
		{
			"a fire dragon",
			"a magma dragon",
			"a flame dragon",
			"a flare dragon"
		};

		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 5 ) )
			{
				default:
				case 1: return WeaponAbility.WhirlwindAttack;
				case 2: return WeaponAbility.Disarm;
				case 3: return WeaponAbility.Dismount;
				case 4: return WeaponAbility.BleedAttack;
				case 6: return WeaponAbility.MortalStrike;
			}
		}

		[Constructable]
		public FireDragon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = m_RandName[Utility.Random(m_RandName.Length)];
			Body = Utility.RandomList( 103, 59, 12, 60, 61 );
			Hue = Utility.RandomList( 1161, 1355, 1356, 1357, 1358, 1359, 1360 );
			BaseSoundID = 362;

			SetStr( 215, 350 );
			SetDex( 201, 220 );
			SetInt( 1001, 1040 );

			SetHits( 480, 660 );

			SetDamage( 5, 12 );

			SetDamageType( ResistanceType.Physical, 10 );
			SetDamageType( ResistanceType.Fire, 90 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 90, 100 );
			SetResistance( ResistanceType.Cold, 2, 17 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.EvalInt, 100.1, 110.0 );
			SetSkill( SkillName.Magery, 110.1, 120.0 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 50.1, 60.0 );
			SetSkill( SkillName.Wrestling, 30.1, 100.0 );

			Fame = 15000;
			Karma = 15000;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 92.1;

			int totalstats = this.Str + this.Dex + this.Int + this.HitsMax + this.StamMax + this.ManaMax + this.PhysicalResistance + this.FireResistance + this.ColdResistance + this.EnergyResistance + this.PoisonResistance + this.DamageMin + this.DamageMax + this.VirtualArmor;
			int nextlevel = totalstats * 10;

			this.NextLevel = nextlevel;

			VirtualArmor = 36;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Hides{ get{ return 20; } }
		public override int Meat{ get{ return 19; } }
		public override int Scales{ get{ return 6; } }
		public override ScaleType ScaleType{ get{ return ( Utility.RandomBool() ? ScaleType.Black : ScaleType.Red ); } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public FireDragon( Serial serial ) : base( serial )
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