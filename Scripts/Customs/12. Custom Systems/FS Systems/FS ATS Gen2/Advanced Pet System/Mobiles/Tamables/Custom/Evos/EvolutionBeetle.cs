using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a bug corpse" )]
	public class EvolutionBettle : BaseCreature
	{
		[Constructable]
		public EvolutionBettle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a beetle baby";
			Body = 791;
			BaseSoundID = 0;

			SetStr( 95, 115 );
			SetDex( 55, 100 );
			SetInt( 15, 35 );

			SetHits( 75, 150 );
			SetMana( 0 );

			SetDamage( 5, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Poison, 20, 30 );

			SetSkill( SkillName.Wrestling, 50.1, 70.0 );
			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 19.3, 34.0 );

			Fame = 300;
			Karma = -300;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 65.1;

			this.F0 = true;
			this.Evolves = true;

			this.UsesForm1 = true;
			this.UsesForm2 = true;
			this.UsesForm3 = true;

			this.Form1 = 242; 	//Death Watch Beetle
			this.Form2 = 315; 	//Fleshrenderer
			this.Form3 = 244; 	//Rune Beetle

			this.Sound1 = 0;	//See Sounds Below
			this.Sound2 = 0;	//See Sounds Below
			this.Sound3 = 0;	//See Sounds Below

			int totalstats = this.Str + this.Dex + this.Int + this.HitsMax + this.StamMax + this.ManaMax + this.PhysicalResistance + this.FireResistance + this.ColdResistance + this.EnergyResistance + this.PoisonResistance + this.DamageMin + this.DamageMax + this.VirtualArmor;
			int nextlevel = totalstats * 10;

			this.NextLevel = nextlevel;
		}

		public override Poison HitPoison{ get{ return Poison.Lesser; } }

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public EvolutionBettle(Serial serial) : base(serial)
		{
		}

		public override int GetAngerSound()
		{
			if ( this.Level == 1 )
			{
				return 0x21D;
			}
			if ( this.Level == 2 )
			{
				return 0x4F4;
			}
			else if ( this.Level == 3 )
			{
				return 0x34C;
			}
			else if ( this.Level >= 4 )
			{
				return 0x4E9;
			}
			else
			{
				return 0x21D;
			}
		}

		public override int GetIdleSound()
		{
			if ( this.Level == 1 )
			{
				return 0x21D;
			}
			else if ( this.Level == 2 )
			{
				return 0x4F3;
			}
			else if ( this.Level == 3 )
			{
				return 0x34C;
			}
			else if ( this.Level >= 4 )
			{
				return 0x4E8;
			}
			else
			{
				return 0x21D;
			}
			
		}

		public override int GetAttackSound()
		{
			if ( this.Level == 1 )
			{
				return 0x21D;
			}
			else if ( this.Level == 2 )
			{
				return 0x4F2;
			}
			else if ( this.Level == 3 )
			{
				return 0x34C;
			}
			else if ( this.Level >= 4 )
			{
				return 0x4E7;
			}
			else
			{
				return 0x162;
			}
			
		}

		public override int GetHurtSound()
		{
			if ( this.Level == 1 )
			{
				return 0x21D;
			}
			else if ( this.Level == 2 )
			{
				return 0x4F5;
			}
			else if ( this.Level == 3 )
			{
				return 0x354;
			}
			else if ( this.Level >= 4 )
			{
				return 0x4EA;
			}
			else
			{
				return 0x163;
			}
			
		}

		public override int GetDeathSound()
		{
			if ( this.Level == 1 )
			{
				return 0x21D;
			}
			else if ( this.Level == 2 )
			{
				return 0x4F1;
			}
			else if ( this.Level == 3 )
			{
				return 0x354;
			}
			else if ( this.Level >= 4 )
			{
				return 0x4E6;
			}
			else
			{
				return 0x21D;
			}

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