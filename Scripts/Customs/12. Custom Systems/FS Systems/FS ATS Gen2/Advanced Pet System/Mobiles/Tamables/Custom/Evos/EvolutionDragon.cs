using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class EvolutionDragon : BaseCreature
	{
		[Constructable]
		public EvolutionDragon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dragon baby";
			Body = 52;
			Hue = Utility.RandomSnakeHue();
			BaseSoundID = 0xDB;

			SetStr( 401, 430 );
			SetDex( 133, 152 );
			SetInt( 101, 140 );

			SetHits( 241, 258 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Fire, 20 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );
			SetSkill( SkillName.Magery, 5.1, 10.0 );
			SetSkill( SkillName.EvalInt, 5.1, 10.0 );

			Fame = 300;
			Karma = -300;

			VirtualArmor = 20;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.1;

			this.F0 = true;
			this.Evolves = true;

			this.UsesForm1 = true;
			this.UsesForm2 = true;
			this.UsesForm3 = true;
			this.UsesForm4 = true;
			this.UsesForm5 = true;
			this.UsesForm6 = true;
			this.UsesForm7 = true;
			this.UsesForm8 = true;
			this.UsesForm9 = true;

			this.Form1 = 89; 	//Giant Serpent
			this.Form2 = 206; 	//Large Lizard
			this.Form3 = 794; 	//Swamp Dragon
			this.Form4 = 60;  	//Drake
			this.Form5 = 12;  	//Dragon
			this.Form6 = 62;	//Wvyern
			this.Form7 = 103;	//Serpinetine Dragon
			this.Form8 = 46;	//Ancient Wyrm
			this.Form9 = 172;	//Riktor

			this.Sound1 = 219;
			this.Sound2 = 0x5A;
			this.Sound3 = 0x16A;
			this.Sound4 = 362;
			this.Sound5 = 362;
			this.Sound6 = 362;
			this.Sound7 = 362;
			this.Sound8 = 362;
			this.Sound9 = 362;

			int totalstats = this.Str + this.Dex + this.Int + this.HitsMax + this.StamMax + this.ManaMax + this.PhysicalResistance + this.FireResistance + this.ColdResistance + this.EnergyResistance + this.PoisonResistance + this.DamageMin + this.DamageMax + this.VirtualArmor;
			int nextlevel = totalstats * 10;

			this.NextLevel = nextlevel;
		}

		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public EvolutionDragon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}