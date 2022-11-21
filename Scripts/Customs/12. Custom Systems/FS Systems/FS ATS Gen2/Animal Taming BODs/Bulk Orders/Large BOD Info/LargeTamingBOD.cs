using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.LargeTamingBOD" )]
	public class LargeTamingBOD : LargeMobileBOD
	{

		public override int ComputeFame()
		{
			int bonus = 0;

			bonus += 50;

			return 100 + Utility.Random( bonus );
		}

		private static int[][][] m_GoldTable = new int[][][]
			{
				new int[][] // Animals
				{
					new int[]{ 5000, 7500, 10000, 12500, 15000, 17500, 20000, 22250, 25000 },
					new int[]{ 10000, 12500, 15000, 17500, 20000, 22250, 25000, 27500, 30000 },
					new int[]{ 15000, 17500, 20000, 22250, 25000, 27500, 30000, 32250, 35000 }
				},
			};

		public override int ComputeGold()
		{
			Type primaryType;

			if ( Entries.Length > 0 )
				primaryType = Entries[0].Details.Type;
			else
				return 0;

			bool isMount = ( primaryType == typeof( BaseMount ) );
			bool isCreature = ( primaryType == typeof( BaseCreature ) );

			int index;

			if ( isMount )
				index = 2;
			else if ( isCreature )
				index = 1;
			else
				index = 0;

			index *= 2;

			if ( index < m_GoldTable.Length )
			{
				int[][] table = m_GoldTable[index];

				if ( AmountMax >= 20 )
					index = 2;
				else if ( AmountMax >= 15 )
					index = 1;
				else
					index = 0;

				if ( index < table.Length )
				{
					int[] list = table[index];

					if ( index < list.Length )
						return list[index];
				}
			}

			return 0;
		}

		[Constructable]
		public LargeTamingBOD()
		{
			LargeMobileBulkEntry[] entries;

			switch ( Utility.Random( 10 ) )
			{
				default:
				case  0: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Mounts );  break;
				case  1: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.HardMounts ); break;
				case  2: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Dragons ); break;
				case  3: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.FarmAnimals ); break;
				case  4: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Spiders ); break;
				case  5: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Felines ); break;
				case  6: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Kanines ); break;
				case  7: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Bears ); break;
				case  8: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Birds ); break;
				case  9: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Rodents ); break;
			}

			int hue = 0x1CA;
			int amountMax = Utility.RandomList( 10, 15, 20, 20 );

			this.Hue = hue;
			this.AmountMax = amountMax;
			this.Entries = entries;
		}

		public LargeTamingBOD( int amountMax, LargeMobileBulkEntry[] entries )
		{
			this.Hue = 0x1CA;
			this.AmountMax = amountMax;
			this.Entries = entries;
		}

		public override ArrayList ComputeRewards()
		{
			Type primaryType;

			if ( Entries.Length > 0 )
				primaryType = Entries[0].Details.Type;
			else
				return new ArrayList();

			bool tps15 = false, tps20 = false;
			bool lps15 = false, lps20 = false;
			bool vps15 = false, vps20 = false;
			bool shoes5 = false;
			bool sandals5 = false;
			bool boots5 = false;
			bool sash5 = false;
			bool apron5 = false;
			bool robe5 = false;
			bool biotool = false;
			bool biobook = false;
			bool dhe = false, dhs = false;
			bool hbe = false, hbs = false;
			bool wh = false;
			bool pbd = false;
			bool ppsm15 = false, ppsm20 = false;
			bool ppst15 = false, ppst20 = false;
			bool ppsw15 = false, ppsw20 = false;
			bool ppsmed15 = false, ppsmed20 = false;
			bool ppsmr15 = false, ppsmr20 = false;
			bool ppse15 = false, ppse20 = false;
			bool ppsa15 = false, ppsa20 = false;

			if ( Entries.Length == 2 )
			{
				if ( AmountMax == 10 )
				{
					shoes5 = true;
					sandals5 = true;
					boots5 = true;

					if ( FSATS.EnableBioEngineer == true )
						biotool = true;
				}
				else if ( AmountMax == 15 )
				{
					sash5 = true;
					apron5 = true;
					robe5 = true;
				}
				else if ( AmountMax == 20 )
				{
					sash5 = true;
					apron5 = true;
					robe5 = true;
					shoes5 = true;
					sandals5 = true;
					boots5 = true;
				}
			}
			else if ( Entries.Length == 3 )
			{
				if ( AmountMax == 10 )
				{
					sash5 = true;
					apron5 = true;
					robe5 = true;
					shoes5 = true;
					sandals5 = true;
					boots5 = true;
				}
				else if ( AmountMax == 15 )
				{
					dhe = true;
 					dhs = true;
					wh = true;
				}
				else if ( AmountMax == 20 )
				{
					hbe = true;
 					hbs = true;
				}
			}
			else if ( Entries.Length == 4 )
			{
				if ( AmountMax == 10 )
				{
					hbe = true;
 					hbs = true;
				}
				else if ( AmountMax == 15 )
				{
					ppsm15 = true;
					ppst15 = true;
					ppsw15 = true;
					ppsmed15 = true;
					ppsmr15 = true;
					ppse15 = true;
					ppsa15 = true;
				}
				else if ( AmountMax == 20 )
				{
 					ppsm20 = true;
 					ppst20 = true;
 					ppsw20 = true;
 					ppsmed20 = true;
 					ppsmr20 = true;
 					ppse20 = true;
 					ppsa20 = true;
				}
			}
			else if ( Entries.Length == 5 )
			{
				if ( AmountMax == 10 )
				{
 					ppsm20 = true;
 					ppst20 = true;
 					ppsw20 = true;
 					ppsmed20 = true;
 					ppsmr20 = true;
 					ppse20 = true;
 					ppsa20 = true;
				}
				else if ( AmountMax == 15 )
				{
					tps15 = true;
					lps15 = true;
					vps15 = true;
				}
				else if ( AmountMax == 20 )
				{
 					tps20 = true;
 					lps20 = true;
			 		vps20 = true;
					pbd = true;

					if ( FSATS.EnableBioEngineer == true )
						biobook = true;
				}
			}
			else
			{
				if ( AmountMax == 10 )
				{
					tps15 = true;
					lps15 = true;
					vps15 = true;
				}
				else if ( AmountMax == 15 )
				{
 					tps20 = true;
 					lps20 = true;
			 		vps20 = true;
				}
				else if ( AmountMax == 20 )
				{
					pbd = true;

					if ( FSATS.EnableBioEngineer == true )
						biobook = true;
				}
			}

			ArrayList list = new ArrayList();

			if ( tps15 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 115 ) );

			if ( tps20 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 120 ) );

			if ( lps15 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 115 ) );

			if ( lps20 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 120 ) );

			if ( vps15 )
				list.Add( new PowerScroll( SkillName.Veterinary, 115 ) );

			if ( vps20 )
				list.Add( new PowerScroll( SkillName.Veterinary, 120 ) );

			if ( shoes5 )
				list.Add( new ShoesOfVeterinary( 5 ) );

			if ( sandals5 )
				list.Add( new SandalsOfVeterinary( 5 ) );

			if ( boots5 )
				list.Add( new BootsOfVeterinary( 5 ) );

			if ( sash5 )
				list.Add( new SashOfAnimalLore( 5 ) );

			if ( apron5 )
				list.Add( new HalfApronOfAnimalLore( 5 ) );

			if ( robe5 )
				list.Add( new RobeOfAnimalLore( 5 ) );

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

			if ( hbe )
				list.Add( new BardedHorseEastDeed() );

			if ( hbs )
				list.Add( new BardedHorseSouthDeed() );

			if ( biobook )
				list.Add( new BioEnginerBook() );

			if ( ppsm15 )
				list.Add( new PetPowerScroll( SkillName.Magery, 115 ) );

			if ( ppsm20 )
				list.Add( new PetPowerScroll( SkillName.Magery, 120 ) );

			if ( ppse15 )
				list.Add( new PetPowerScroll( SkillName.EvalInt, 115 ) );

			if ( ppse20 )
				list.Add( new PetPowerScroll( SkillName.EvalInt, 120 ) );

			if ( ppsmed15 )
				list.Add( new PetPowerScroll( SkillName.Meditation, 115 ) );

			if ( ppsmed20 )
				list.Add( new PetPowerScroll( SkillName.Meditation, 120 ) );

			if ( ppsw15 )
				list.Add( new PetPowerScroll( SkillName.Wrestling, 115 ) );

			if ( ppsw20 )
				list.Add( new PetPowerScroll( SkillName.Wrestling, 120 ) );

			if ( ppst15 )
				list.Add( new PetPowerScroll( SkillName.Tactics, 115 ) );

			if ( ppst20 )
				list.Add( new PetPowerScroll( SkillName.Tactics, 120 ) );

			if ( ppsa15 )
				list.Add( new PetPowerScroll( SkillName.Anatomy, 115 ) );

			if ( ppsa20 )
				list.Add( new PetPowerScroll( SkillName.Anatomy, 120 ) );

			if ( ppsmr15 )
				list.Add( new PetPowerScroll( SkillName.MagicResist, 115 ) );

			if ( ppsmr20 )
				list.Add( new PetPowerScroll( SkillName.MagicResist, 120 ) );

			return list;
		}

		public LargeTamingBOD( Serial serial ) : base( serial )
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