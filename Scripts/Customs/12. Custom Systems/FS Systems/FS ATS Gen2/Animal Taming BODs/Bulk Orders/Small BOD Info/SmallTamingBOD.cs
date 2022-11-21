using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.Craft;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.SmallTamingBOD" )]
	public class SmallTamingBOD : SmallMobileBOD
	{

		public override int ComputeFame()
		{
			int bonus = 0;

			bonus += 20;

			return 10 + Utility.Random( bonus );
		}

		public override int ComputeGold()
		{
			int bonus = 0;

			bonus += 5000;

			return 37750 + Utility.Random( bonus );
		}

		public override ArrayList ComputeRewards()
		{
			if ( Type == null )
				return new ArrayList();

			bool tps5 = false, tps10 = false;
			bool lps5 = false, lps10 = false;
			bool vps5 = false, vps10 = false;
			bool shoes3 = false;
			bool sandals3 = false;
			bool boots3 = false;
			bool sash3 = false;
			bool apron3 = false;
			bool robe3 = false;
			bool biotool = false;
			bool dhe = false, dhs = false;
			bool pbd = false;
			bool wh = false;
			bool ppsm5 = false, ppsm10 = false;
			bool ppst5 = false, ppst10 = false;
			bool ppsw5 = false, ppsw10 = false;
			bool ppsmed5 = false, ppsmed10 = false;
			bool ppsmr5 = false, ppsmr10 = false;
			bool ppse5 = false, ppse10 = false;
			bool ppsa5 = false, ppsa10 = false;

			if ( Type.IsSubclassOf( typeof( BaseCreature ) ) )
			{
				BaseCreature bc = null;

				if ( Type != null )
				{
					object o = Activator.CreateInstance( Type );
        				bc = o as BaseCreature;
				}

				if ( bc.MinTameSkill <= 25.0 )
				{
					if ( AmountMax == 10 )
					{
						if ( FSATS.EnableBioEngineer == true )
							biotool = true;

						shoes3 = true;
						sandals3 = true;
						boots3 = true;
					}
					else if ( AmountMax == 15 )
					{
						if ( FSATS.EnableBioEngineer == true )
							biotool = true;

						sash3 = true;
						apron3 = true;
						robe3 = true;
					}
					else if ( AmountMax == 20 )
					{
						sash3 = true;
						apron3 = true;
						robe3 = true;
						shoes3 = true;
						sandals3 = true;
						boots3 = true;
					}
				}
				else if ( bc.MinTameSkill <= 65.0 )
				{
					if ( AmountMax == 10 )
					{
						sash3 = true;
						apron3 = true;
						robe3 = true;
						shoes3 = true;
						sandals3 = true;
						boots3 = true;
					}
					else if ( AmountMax == 15 )
					{
						ppsm5 = true;
						ppst5 = true;
						ppsw5 = true;
						ppsmed5 = true;
						ppsmr5 = true;
						ppse5 = true;
						ppsa5 = true;
					}
					else if ( AmountMax == 20 )
					{
						ppsm10 = true;
						ppst10 = true;
						ppsw10 = true;
						ppsmed10 = true;
						ppsmr10 = true;
						ppse10 = true;
						ppsa10 = true;
					}
				}
				else if ( bc.MinTameSkill <= 80.0 )
				{
					if ( AmountMax == 10 )
					{
						ppsm10 = true;
						ppst10 = true;
						ppsw10 = true;
						ppsmed10 = true;
						ppsmr10 = true;
						ppse10 = true;
						ppsa10 = true;
					}
					else if ( AmountMax == 15 )
					{
						wh = true;
					}
					else if ( AmountMax == 20 )
					{
						wh = true;
					}
				}
				else
				{
					if ( AmountMax == 10 )
					{
						wh = true;
					}
					else if ( AmountMax == 15 )
					{
						tps5 = true;
						lps5 = true;
						vps5 = true;
					}
					else if ( AmountMax == 20 )
					{
						tps10 = true;
						lps10 = true;
						vps10 = true;
					}
				}

				bc.Delete();
			}

			ArrayList list = new ArrayList();

			if ( tps5 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 105 ) );

			if ( tps10 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 110 ) );

			if ( lps5 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 105 ) );

			if ( lps10 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 110 ) );

			if ( vps5 )
				list.Add( new PowerScroll( SkillName.Veterinary, 105 ) );

			if ( vps10 )
				list.Add( new PowerScroll( SkillName.Veterinary, 110 ) );

			if ( shoes3 )
				list.Add( new ShoesOfVeterinary( 3 ) );

			if ( sandals3 )
				list.Add( new SandalsOfVeterinary( 3 ) );

			if ( boots3 )
				list.Add( new BootsOfVeterinary( 3 ) );

			if ( sash3 )
				list.Add( new SashOfAnimalLore( 3 ) );

			if ( apron3 )
				list.Add( new HalfApronOfAnimalLore( 3 ) );

			if ( robe3 )
				list.Add( new RobeOfAnimalLore( 3 ) );

			if ( biotool )
				list.Add( new BioTool() );

			if ( dhe )
				list.Add( new DragonTorphyEastDeed() );

			if ( dhs )
				list.Add( new DragonTorphySouthDeed() );

			if ( pbd )
				list.Add( new PetBondingDeed() );

			if ( wh )
				list.Add( new WoodenHorseStatue() );

			if ( ppsm5 )
				list.Add( new PetPowerScroll( SkillName.Magery, 105 ) );

			if ( ppsm10 )
				list.Add( new PetPowerScroll( SkillName.Magery, 110 ) );

			if ( ppse5 )
				list.Add( new PetPowerScroll( SkillName.EvalInt, 105 ) );

			if ( ppse10 )
				list.Add( new PetPowerScroll( SkillName.EvalInt, 110 ) );

			if ( ppsmed5 )
				list.Add( new PetPowerScroll( SkillName.Meditation, 105 ) );

			if ( ppsmed10 )
				list.Add( new PetPowerScroll( SkillName.Meditation, 110 ) );

			if ( ppsw5 )
				list.Add( new PetPowerScroll( SkillName.Wrestling, 105 ) );

			if ( ppsw10 )
				list.Add( new PetPowerScroll( SkillName.Wrestling, 110 ) );

			if ( ppst5 )
				list.Add( new PetPowerScroll( SkillName.Tactics, 105 ) );

			if ( ppst10 )
				list.Add( new PetPowerScroll( SkillName.Tactics, 110 ) );

			if ( ppsa5 )
				list.Add( new PetPowerScroll( SkillName.Anatomy, 105 ) );

			if ( ppsa10 )
				list.Add( new PetPowerScroll( SkillName.Anatomy, 110 ) );

			if ( ppsmr5 )
				list.Add( new PetPowerScroll( SkillName.MagicResist, 105 ) );

			if ( ppsmr10 )
				list.Add( new PetPowerScroll( SkillName.MagicResist, 110 ) );

			return list;
		}

		public static SmallTamingBOD CreateRandomFor( Mobile m )
		{
			SmallMobileBulkEntry[] entries;

			entries = SmallMobileBulkEntry.TamingMounts;

			if ( entries.Length > 0 )
			{
				double theirSkill = m.Skills[SkillName.AnimalTaming].Base;
				int amountMax;

				if ( theirSkill >= 70.1 )
					amountMax = Utility.RandomList( 10, 15, 20, 20 );
				else if ( theirSkill >= 50.1 )
					amountMax = Utility.RandomList( 10, 15, 15, 20 );
				else
					amountMax = Utility.RandomList( 10, 10, 15, 20 );

				SmallMobileBulkEntry entry = entries[Utility.Random( entries.Length )];

				if ( entry != null )
					return new SmallTamingBOD( entry, amountMax );
			}

			return null;
		}

		private SmallTamingBOD( SmallMobileBulkEntry entry,  int amountMax )
		{
			this.Hue = 0x1CA;
			this.AmountMax = amountMax;
			this.Type = entry.Type;
			this.AnimalName = entry.AnimalName;
			this.Graphic = entry.Graphic;
		}

		[Constructable]
		public SmallTamingBOD()
		{
			SmallMobileBulkEntry[] entries;

			entries = SmallMobileBulkEntry.TamingMounts;

			if ( entries.Length > 0 )
			{
				int hue = 0x1CA;
				int amountMax = Utility.RandomList( 10, 15, 20 );

				SmallMobileBulkEntry entry = entries[Utility.Random( entries.Length )];

				this.Hue = hue;
				this.AmountMax = amountMax;
				this.Type = entry.Type;
				this.AnimalName = entry.AnimalName;
				this.Graphic = entry.Graphic;
			}
		}

		public SmallTamingBOD( int amountCur, int amountMax, Type type, string animalname, int graphic )
		{
			this.Hue = 0x1CA;
			this.AmountMax = amountMax;
			this.AmountCur = amountCur;
			this.Type = type;
			this.AnimalName = animalname;
			this.Graphic = graphic;
		}

		public SmallTamingBOD( Serial serial ) : base( serial )
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