using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.Craft;
using System.Collections.Generic;
using Mat = Server.Engines.BulkOrders.BulkMaterialType;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.SmallCarpenterBOD" )]
	public class SmallCarpenterBOD : SmallBOD
	{
		public static double[] m_CarpenterMaterialChances = new double[]
			{
				0.140, // None
				0.130, // OakWood
				0.120, // AshWood
				0.110, // YewWood
				0.100, // Heartwood
				0.090, // Bloodwood
				0.080, // Frostwood
			};

		public override int ComputeFame()
		{
			return CarpenterRewardCalculator.Instance.ComputeFame( this );
		}

		public override int ComputeGold()
		{
			return CarpenterRewardCalculator.Instance.ComputeGold( this );
		}

		public override List<Item> ComputeRewards( bool full )
		{
			List<Item> list = new List<Item>();

			RewardGroup rewardGroup = CarpenterRewardCalculator.Instance.LookupRewards( CarpenterRewardCalculator.Instance.ComputePoints( this ) );

			if ( rewardGroup != null )
			{
				if ( full )
				{
					for ( int i = 0; i < rewardGroup.Items.Length; ++i )
					{
						Item item = rewardGroup.Items[i].Construct();

						if ( item != null )
							list.Add( item );
					}
				}
				else
				{
					RewardItem rewardItem = rewardGroup.AcquireItem();

					if ( rewardItem != null )
					{
						Item item = rewardItem.Construct();

						if ( item != null )
							list.Add( item );
					}
				}
			}

			return list;
		}

		public static SmallCarpenterBOD CreateRandomFor( Mobile m )
		{
			SmallBulkEntry[] entries;
			bool useMaterials;

			if ( useMaterials = Utility.RandomBool() )
				entries = SmallBulkEntry.CarpenterStaff;
			else
				entries = SmallBulkEntry.CarpenterInstrument;

			if ( entries.Length > 0 )
			{
				double theirSkill = m.Skills[SkillName.Carpentry].Base;
				int amountMax;

				if ( theirSkill >= 70.1 )
					amountMax = Utility.RandomList( 10, 15, 20, 20 );
				else if ( theirSkill >= 50.1 )
					amountMax = Utility.RandomList( 10, 15, 15, 20 );
				else
					amountMax = Utility.RandomList( 10, 10, 15, 20 );

				BulkMaterialType material = BulkMaterialType.None;

				if ( useMaterials && theirSkill >= 70.1 )
				{
					for ( int i = 0; i < 20; ++i )
					{
						BulkMaterialType check = GetRandomMaterial( BulkMaterialType.OakWood, m_CarpenterMaterialChances );
						double skillReq = 0.0;

						switch ( check )
						{
							case BulkMaterialType.OakWood:		skillReq = 65.0; break;
							case BulkMaterialType.AshWood:		skillReq = 80.0; break;
							case BulkMaterialType.YewWood:		skillReq = 95.0; break;
							case BulkMaterialType.Heartwood:	skillReq = 100.0; break;
							case BulkMaterialType.Bloodwood:	skillReq = 100.0; break;
							case BulkMaterialType.Frostwood:	skillReq = 100.0; break;
						}

						if ( theirSkill >= skillReq )
						{
							material = check;
							break;
						}
					}
				}

				double excChance = 0.0;

				if ( theirSkill >= 70.1 )
					excChance = (theirSkill + 80.0) / 200.0;

                bool reqExceptional = (excChance > Utility.RandomDouble());

				SmallBulkEntry entry = null;

				CraftSystem system = DefCarpentry.CraftSystem;
				
				//List<SmallBulkEntry> validEntries = new List<SmallBulkEntry>();

				for ( int i = 0; i < 150; ++i )
				{
					SmallBulkEntry check = entries[Utility.Random( entries.Length )];
					//CraftItem item = system.CraftItems.SearchFor( entries[i].Type );
					CraftItem item = system.CraftItems.SearchFor( check.Type );

					if ( item != null )
					{
						bool allRequiredSkills = true;
						double chance = item.GetSuccessChance( m, null, system, false, ref allRequiredSkills );

						if ( allRequiredSkills && chance >= 0.0 )
						{
                            if (reqExceptional)
                                chance = item.GetExceptionalChance(system, chance, m);

							if ( chance > 0.0 )
							{
								entry = check;
								break;
							}
						}
					}
				}

				if ( entry != null )
                    return new SmallCarpenterBOD(entry, material, amountMax, reqExceptional);
			}

			return null;
		}

        private SmallCarpenterBOD(SmallBulkEntry entry, BulkMaterialType material, int amountMax, bool reqExceptional)
		{
			this.Hue = 0x30;
			this.AmountMax = amountMax;
			this.Type = entry.Type;
			this.Number = entry.Number;
			this.Graphic = entry.Graphic;
            this.RequireExceptional = reqExceptional;
			this.Material = material;
		}

		[Constructable]
		public SmallCarpenterBOD()
		{
			SmallBulkEntry[] entries;
			bool useMaterials;

			if ( useMaterials = Utility.RandomBool() )
				entries = SmallBulkEntry.CarpenterStaff;
			else
				entries = SmallBulkEntry.CarpenterInstrument;

			if ( entries.Length > 0 )
			{
				int hue = 0x30;
				int amountMax = Utility.RandomList( 10, 15, 20 );

				BulkMaterialType material;

				if ( useMaterials )
					material = GetRandomMaterial( BulkMaterialType.OakWood, m_CarpenterMaterialChances );
				else
					material = BulkMaterialType.None;

				bool reqExceptional = Utility.RandomBool() || (material == BulkMaterialType.None);

				SmallBulkEntry entry = entries[Utility.Random( entries.Length )];

				this.Hue = hue;
				this.AmountMax = amountMax;
				this.Type = entry.Type;
				this.Number = entry.Number;
				this.Graphic = entry.Graphic;
                this.RequireExceptional = reqExceptional;
				this.Material = material;
			}
		}

        public SmallCarpenterBOD( int amountCur, int amountMax, Type type, int number, int graphic, bool reqExceptional, BulkMaterialType mat )
		{
			this.Hue = 0x30;
			this.AmountMax = amountMax;
			this.AmountCur = amountCur;
			this.Type = type;
			this.Number = number;
			this.Graphic = graphic;
            this.RequireExceptional = reqExceptional;
			this.Material = mat;
		}

		public SmallCarpenterBOD( Serial serial ) : base( serial )
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