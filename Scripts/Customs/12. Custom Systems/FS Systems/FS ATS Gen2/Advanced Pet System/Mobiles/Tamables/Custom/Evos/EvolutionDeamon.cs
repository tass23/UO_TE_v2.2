using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a demon corpse" )]
	public class EvolutionDeamon : BaseCreature
	{
		[Constructable]
		public EvolutionDeamon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a demon baby";
			Body = 317;
			Hue = Utility.RandomRedHue();
			BaseSoundID = 624;

			SetStr( 401, 430 );
			SetDex( 133, 152 );
			SetInt( 101, 140 );

			SetHits( 241, 258 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 80 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 20, 30 );
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

			this.Form1 = 74; 	//Imp
			this.Form2 = 39; 	//Mongbat
			this.Form3 = 779; 	//Bogling
			this.Form4 = 784;  	//Demon
			this.Form5 = 792;  	//Chaos Demon
			this.Form6 = 755;	//Large Gargyole
			this.Form7 = 9;		//Deamon
			this.Form8 = 40;	//Balron
			this.Form9 = 38;	//Black Gate

			this.Sound1 = 422;
			this.Sound2 = 422;
			this.Sound3 = 422;
			this.Sound4 = 357;
			this.Sound5 = 357;
			this.Sound6 = 357;
			this.Sound7 = 357;
			this.Sound8 = 357;
			this.Sound9 = 357;

			int totalstats = this.Str + this.Dex + this.Int + this.HitsMax + this.StamMax + this.ManaMax + this.PhysicalResistance + this.FireResistance + this.ColdResistance + this.EnergyResistance + this.PoisonResistance + this.DamageMin + this.DamageMax + this.VirtualArmor;
			int nextlevel = totalstats * 10;

			this.NextLevel = nextlevel;
		}

		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public EvolutionDeamon(Serial serial) : base(serial)
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