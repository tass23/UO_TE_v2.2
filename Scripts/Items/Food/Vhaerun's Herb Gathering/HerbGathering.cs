using System;
using Server;
using Server.Items;

namespace Server.Engines.Harvest
{
	public class HerbGathering : HarvestSystem
	{
		private static HerbGathering m_System;
		public static HerbGathering System { get { if ( m_System == null ) m_System = new HerbGathering(); return m_System; } }

		private HarvestDefinition m_Definition;

		public HarvestDefinition Definition { get{ return m_Definition; } }

		private HerbGathering()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region HerbGathering
			HarvestDefinition herb = new HarvestDefinition();

			herb.BankWidth = 1;
			herb.BankHeight = 1;

			herb.MinTotal = 1;
			herb.MaxTotal = 3;

			herb.MinRespawn = TimeSpan.FromMinutes( 5.0 );
			herb.MaxRespawn = TimeSpan.FromMinutes( 15.0 );

			herb.Skill = SkillName.Cooking;

			herb.Tiles = m_PlantTiles;

			herb.MaxRange = 2;

			herb.ConsumedPerHarvest = 1;
			herb.ConsumedPerFeluccaHarvest = 1;

			herb.EffectActions = new int[]{ 32 };
			herb.EffectSounds = new int[]{ 0x4F };
			herb.EffectCounts = new int[]{ 1, 1, 1, 1, 2 };
			herb.EffectDelay = TimeSpan.FromSeconds( 0.7 );
			herb.EffectSoundDelay = TimeSpan.FromSeconds( 0.7 );

			herb.NoResourcesMessage = "There are no herbs to harvest here.";
			herb.FailMessage = "You don't manage to gather any herbs.";
			herb.OutOfRangeMessage = 500446;
			herb.PackFullMessage = "You can't carry any more!";
			herb.ToolBrokeMessage = "You've broken your boline.";

			res = new HarvestResource[]
			{
				new HarvestResource( 37.5, 37.5, 37.5, "You put some sage in your backpack.", typeof( Sage ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some acacia in your backpack.", typeof( Acacia ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some anise in your backpack.", typeof( Anise ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some basil in your backpack.", typeof( Basil ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put a bay leaf in your backpack.", typeof( BayLeaf ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some chamomile in your backpack.", typeof( Chamomile ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some caraway in your backpack.", typeof( Caraway ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some cilantro in your backpack.", typeof( Cilantro ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some cinnamon in your backpack.", typeof( Cinnamon ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some clove in your backpack.", typeof( Clove ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some copal in your backpack.", typeof( Copal ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some coriander in your backpack.", typeof( Coriander ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some dill in your backpack.", typeof( Dill ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some dragonsblood in your backpack.", typeof( Dragonsblood ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some frankincense in your backpack.", typeof( Frankincense ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some lavender in your backpack.", typeof( Lavender ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some marjoram in your backpack.", typeof( Marjoram ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some meadowsweet in your backpack.", typeof( Meadowsweet ) ),
				new HarvestResource( 37.5, 37.5, 37.5, "You put some mint in your backpack.", typeof( Mint ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some mugwort in your backpack.", typeof( Mugwort ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some mustard in your backpack.", typeof( Mustard ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some myrrh in your backpack.", typeof( Myrrh ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some olive in your backpack.", typeof( Olive ) ),
				new HarvestResource( 37.5, 37.5, 37.5, "You put some oregano in your backpack.", typeof( Oregano ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some orris in your backpack.", typeof( Orris ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some patchouli in your backpack.", typeof( Patchouli ) ),
				new HarvestResource( 37.5, 37.5, 37.5, "You put some peppercorn in your backpack.", typeof( Peppercorn ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some rose in your backpack.", typeof( RoseHerb ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some rosemary in your backpack.", typeof( Rosemary ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some saffron in your backpack.", typeof( Saffron ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some sandelwood in your backpack.", typeof( Sandelwood ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some slippery elm in your backpack.", typeof( SlipperyElm ) ),
				new HarvestResource( 37.5, 37.5, 37.5, "You put some thyme in your backpack.", typeof( Thyme ) ),
				new HarvestResource( 87.5, 87.5, 87.5, "You put some valerian in your backpack.", typeof( Valerian ) ),
				new HarvestResource( 62.5, 62.5, 62.5, "You put some willow bark in your backpack.", typeof( WillowBark ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 5.0, 0.0, res[0], null ),
				new HarvestVein( 1.5, 0.1, res[1], res[0] ),
				new HarvestVein( 3.0, 0.1, res[2], res[0] ),
				new HarvestVein( 3.0, 0.1, res[3], res[0] ),
				new HarvestVein( 3.0, 0.1, res[4], res[0] ),
				new HarvestVein( 3.0, 0.1, res[5], res[0] ),
				new HarvestVein( 3.0, 0.1, res[6], res[0] ),
				new HarvestVein( 3.0, 0.1, res[7], res[0] ),
				new HarvestVein( 3.0, 0.1, res[8], res[0] ),
				new HarvestVein( 3.0, 0.1, res[9], res[0] ),
				new HarvestVein( 1.5, 0.1, res[10], res[0] ),
				new HarvestVein( 3.0, 0.1, res[11], res[0] ),
				new HarvestVein( 3.0, 0.1, res[12], res[0] ),
				new HarvestVein( 1.5, 0.1, res[13], res[0] ),
				new HarvestVein( 1.5, 0.1, res[14], res[0] ),
				new HarvestVein( 3.0, 0.1, res[15], res[0] ),
				new HarvestVein( 1.5, 0.1, res[16], res[0] ),
				new HarvestVein( 3.0, 0.1, res[17], res[0] ),
				new HarvestVein( 5.0, 0.1, res[18], res[0] ),
				new HarvestVein( 3.0, 0.1, res[19], res[0] ),
				new HarvestVein( 3.0, 0.1, res[20], res[0] ),
				new HarvestVein( 1.5, 0.1, res[21], res[0] ),
				new HarvestVein( 3.0, 0.1, res[22], res[0] ),
				new HarvestVein( 5.0, 0.1, res[23], res[0] ),
				new HarvestVein( 1.5, 0.1, res[24], res[0] ),
				new HarvestVein( 1.5, 0.1, res[25], res[0] ),
				new HarvestVein( 5.0, 0.1, res[26], res[0] ),
				new HarvestVein( 3.0, 0.1, res[27], res[0] ),
				new HarvestVein( 3.0, 0.1, res[28], res[0] ),
				new HarvestVein( 3.0, 0.1, res[29], res[0] ),
				new HarvestVein( 1.5, 0.1, res[30], res[0] ),
				new HarvestVein( 3.0, 0.1, res[31], res[0] ),
				new HarvestVein( 5.0, 0.1, res[32], res[0] ),
				new HarvestVein( 1.5, 0.1, res[33], res[0] ),
				new HarvestVein( 3.0, 0.1, res[34], res[0] ),
			};

			herb.Resources = res;
			herb.Veins = veins;

			m_Definition = herb;
			Definitions.Add( herb );
			#endregion
		}

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) ) return false;
			if ( tool.Parent != from ) { from.SendMessage( "You must be holding the boline for gathering herbs." ); return false; }
			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) ) return false;
			if ( tool.Parent != from ) { from.SendMessage( "You must be holding the boline for gathering herbs." ); return false; }
			return true;
		}

		public override void OnBadHarvestTarget( Mobile from, Item tool, object toHarvest )
		{
			from.SendMessage( "You can't use a boline on that." );
		}

		public static void Initialize()
		{
			Array.Sort( m_PlantTiles );
		}

		#region Tile lists
		private static int[] m_PlantTiles = new int[]
		{
			0x4CCA, 0x4CCB, 0x4CCC, 0x4CCD, 0x4CD0, 0x4CD3, 0x4CD6, 0x4CD8,
			0x4CDA, 0x4CDD, 0x4CE0, 0x4CE3, 0x4CE6, 0x4CF8, 0x4CFB, 0x4CFE,
			0x4D01, 0x4D41, 0x4D42, 0x4D43, 0x4D44, 0x4D57, 0x4D58, 0x4D59,
			0x4D5A, 0x4D5B, 0x4D6E, 0x4D6F, 0x4D70, 0x4D71, 0x4D72, 0x4D84,
			0x4D85, 0x4D86, 0x52B5, 0x52B6, 0x52B7, 0x52B8, 0x52B9, 0x52BA,
			0x52BB, 0x52BC, 0x52BD,

			0x4CCE, 0x4CCF, 0x4CD1, 0x4CD2, 0x4CD4, 0x4CD5, 0x4CD7, 0x4CD9,
			0x4CDB, 0x4CDC, 0x4CDE, 0x4CDF, 0x4CE1, 0x4CE2, 0x4CE4, 0x4CE5,
			0x4CE7, 0x4CE8, 0x4CF9, 0x4CFA, 0x4CFC, 0x4CFD, 0x4CFF, 0x4D00,
			0x4D02, 0x4D03, 0x4D45, 0x4D46, 0x4D47, 0x4D48, 0x4D49, 0x4D4A,
			0x4D4B, 0x4D4C, 0x4D4D, 0x4D4E, 0x4D4F, 0x4D50, 0x4D51, 0x4D52,
			0x4D53, 0x4D5C, 0x4D5D, 0x4D5E, 0x4D5F, 0x4D60, 0x4D61, 0x4D62,
			0x4D63, 0x4D64, 0x4D65, 0x4D66, 0x4D67, 0x4D68, 0x4D69, 0x4D73,
			0x4D74, 0x4D75, 0x4D76, 0x4D77, 0x4D78, 0x4D79, 0x4D7A, 0x4D7B,
			0x4D7C, 0x4D7D, 0x4D7E, 0x4D7F, 0x4D87, 0x4D88, 0x4D89, 0x4D8A,
			0x4D8B, 0x4D8C, 0x4D8D, 0x4D8E, 0x4D8F, 0x4D90, 0x4D95, 0x4D96,
			0x4D97, 0x4D99, 0x4D9A, 0x4D9B, 0x4D9D, 0x4D9E, 0x4D9F, 0x4DA1,
			0x4DA2, 0x4DA3, 0x4DA5, 0x4DA6, 0x4DA7, 0x4DA9, 0x4DAA, 0x4DAB,
			0x52BE, 0x52BF, 0x52C0, 0x52C1, 0x52C2, 0x52C3, 0x52C4, 0x52C5,
			0x52C6, 0x52C7, 0x647D, 0x647E, 0x6476, 0x6477, 0x624A, 0x624B,
			0x624C, 0x624D, 0x4D94, 0x4D98, 0x4D9C, 0x4DA4, 0x4DA8, 0x70A1,
			0x709C, 0x70BD, 0x70C3, 0x70D4, 0x70DA, 0xDA0,

			0x4C85, 0x4C83, 0x4C84, 0x4C86, 0x4C87, 0x4C88, 0x4C89, 0x4C8A,
			0x4C8B, 0x4C8C, 0x4C8D, 0x4C8E, 0x4C8F, 0x4C90, 0x4C91, 0x4C92,
			0x4C93, 0x4C94, 0x4C95, 0x4C96, 0x4C97, 0x4C98, 0x4C99, 0x4C9A,
			0x4C9B, 0x4C9C, 0x4C9D, 0x4C9E, 0x4C9F, 0x4CA0, 0x4CA1, 0x4CA2,
			0x4CA3, 0x4CA4, 0x4CA5, 0x4CA6, 0x4CA7, 0x4CA8, 0x4CA9, 0x4CAA,
			0x4CAB, 0x4CAC, 0x4CAD, 0x4CAE, 0x4CAF, 0x4CB0, 0x4CB1, 0x4CB2,
			0x4CB3, 0x4CB4, 0x4CB5, 0x4CB6, 0x4CB7, 0x4CB8, 0x4CB9, 0x4CBA,
			0x4CBB, 0x4CBC, 0x4CBD, 0x4CBE, 0x4CBF, 0x4CC0, 0x4CC1, 0x4CC3,
			0x4CC4, 0x4CC5, 0x4CC6, 0x4CC7, 0x4CC8, 0x4CC9, 0x4CE9, 0x4CEA,
			0x4CEB, 0x4CEC, 0x4CED, 0x4CEE, 0x4CEF, 0x4CF0, 0x4CF1, 0x4CF2,
			0x4CF3, 0x4CF4, 0x4CF5, 0x4CF6, 0x4CF7, 0x4D04, 0x4D05, 0x4D06,
			0x4D07, 0x4D08, 0x4D09, 0x4D0A, 0x4D0B, 0x4D30, 0x4D31, 0x4D32,
			0x4D33, 0x4D34, 0x4D37, 0x4D38, 0x4D3F, 0x4D40, 0x4DBC, 0x4DBD,
			0x4DBE, 0x4DC1, 0x4DC2, 0x4DC3
		};
		#endregion
	}
}