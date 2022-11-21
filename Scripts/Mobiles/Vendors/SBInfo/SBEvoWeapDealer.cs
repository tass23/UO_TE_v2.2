using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBEvoWeapDealer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBEvoWeapDealer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "Evolution Vendor Ring", typeof( EvoVendorRing ), 5, 1, 0x108a, 48 ) );
				Add( new GenericBuyInfo( "Assassin Spike Of Evolution", typeof( AssassinSpikeOfEvolution ), 8750000, 1, 0x2D21, 1266 ) );
				Add( new GenericBuyInfo( "Axe Of Evolution", typeof( AxeOfEvolution ), 8650000, 1, 0xF49, 1266 ) );
				Add( new GenericBuyInfo( "Bardiche Of Evolution", typeof( BardicheOfEvolution ), 8750000, 1, 0xF4D, 1266 ) );
				Add( new GenericBuyInfo( "Battle Axe Of Evolution", typeof( BattleAxeOfEvolution ), 8450000, 1, 0xF47, 1266 ) );
				Add( new GenericBuyInfo( "Bladed Staff Of Evolution", typeof( BladedStaffOfEvolution ), 8850000, 1, 0x26BD, 1266 ) );
				Add( new GenericBuyInfo( "Bone Harvester Of Evolution", typeof( BoneHarvesterOfEvolution ), 8350000, 1, 0x26BB, 1266 ) );
				Add( new GenericBuyInfo( "Broadsword Of Evolution", typeof( BroadswordOfEvolution ), 8750000, 1, 0xF5E, 1266 ) );
				Add( new GenericBuyInfo( "Butcher Knife Of Evolution", typeof( ButcherKnifeOfEvolution ), 8850000, 1, 0x13F6, 1266 ) );
				Add( new GenericBuyInfo( "Cleaver Of Evolution", typeof( CleaverOfEvolution ), 8650000, 1, 0xEC3, 1266 ) );
				Add( new GenericBuyInfo( "Crescent Blade Of Evolution", typeof( CrescentBladeOfEvolution ), 8750000, 1, 0x26C1, 1266 ) );
				Add( new GenericBuyInfo( "Cutlass Of Evolution", typeof( CutlassOfEvolution ), 8650000, 1, 0x1441, 1266 ) );
				Add( new GenericBuyInfo( "Dagger Of Evolution", typeof( DaggerOfEvolution ), 8550000, 1, 0xF52, 1266 ) );
				Add( new GenericBuyInfo( "Daisho Of Evolution", typeof( DaishoOfEvolution ), 8350000, 1, 0x27A9, 1266 ) );
				Add( new GenericBuyInfo( "Double Axe Of Evolution", typeof( DoubleAxeOfEvolution ), 8650000, 1, 0xf4b, 1266 ) );
				Add( new GenericBuyInfo( "Double Bladed Staff Of Evolution", typeof( DoubleBladedStaffOfEvolution ), 8450000, 1, 0x26BF, 1266 ) );
				Add( new GenericBuyInfo( "Elven Spellblade Of Evolution", typeof( ElvenSpellbladeOfEvolution ), 8750000, 1, 0x2D20, 1266 ) );
				Add( new GenericBuyInfo( "Executioners Axe Of Evolution", typeof( ExecutionersAxeOfEvolution ), 8850000, 1, 0xf45, 1266 ) );
				Add( new GenericBuyInfo( "Gnarled Staff Of Evolution", typeof( GnarledStaffOfEvolution ), 8350000, 1, 0x13F8, 1266 ) );
				Add( new GenericBuyInfo( "Halberd Of Evolution", typeof( HalberdOfEvolution ), 8650000, 1, 0x143E, 1266 ) );
				Add( new GenericBuyInfo( "Hammer Pick Of Evolution", typeof( HammerPickOfEvolution ), 8750000, 1, 0x143D, 1266 ) );
				Add( new GenericBuyInfo( "Hatchet Of Evolution", typeof( HatchetOfEvolution ), 8550000, 1, 0xF43, 1266 ) );
				Add( new GenericBuyInfo( "Kama Of Evolution", typeof( KamaOfEvolution ), 8650000, 1, 0x27AD, 1266 ) );
				Add( new GenericBuyInfo( "Katana Of Evolution", typeof( KatanaOfEvolution ), 8750000, 1, 0x13FF, 1266 ) );
				Add( new GenericBuyInfo( "Kryss Of Evolution", typeof( KryssOfEvolution ), 8950000, 1, 0x1401, 1266 ) );
				Add( new GenericBuyInfo( "Lajatang Of Evolution", typeof( LajatangOfEvolution ), 8850000, 1, 0x27A7, 1266 ) );
				Add( new GenericBuyInfo( "Lance Of Evolution", typeof( LanceOfEvolution ), 8650000, 1, 0x26C0, 1266 ) );
				Add( new GenericBuyInfo( "Large Battle Axe Of Evolution", typeof( LargeBattleAxeOfEvolution ), 8750000, 1, 0x13FB, 1266 ) );
				Add( new GenericBuyInfo( "Leafblade Of Evolution", typeof( LeafbladeOfEvolution ), 8950000, 1, 0x2D22, 1266 ) );
				Add( new GenericBuyInfo( "Mace Of Evolution", typeof( MaceOfEvolution ), 8850000, 1, 0xF5C, 1266 ) );
				Add( new GenericBuyInfo( "Maul Of Evolution", typeof( MaulOfEvolution ), 8650000, 1, 0x143B, 1266 ) );
				Add( new GenericBuyInfo( "No Dachi Of Evolution", typeof( NoDachiOfEvolution ), 8750000, 1, 0x27A2, 1266 ) );
				Add( new GenericBuyInfo( "Ornate Axe Of Evolution", typeof( OrnateAxeOfEvolution ), 8850000, 1, 0x2D28, 1266 ) );
				Add( new GenericBuyInfo( "Pike Of Evolution", typeof( PikeOfEvolution ), 8650000, 1, 0x26BE, 1266 ) );
				Add( new GenericBuyInfo( "Pitchfork Of Evolution", typeof( PitchforkOfEvolution ), 8550000, 1, 0xE87, 1266 ) );
				Add( new GenericBuyInfo( "Quarter Staff Of Evolution", typeof( QuarterStaffOfEvolution ), 8450000, 1, 0xE89, 1266 ) );
				Add( new GenericBuyInfo( "Rune Blade Of Evolution", typeof( RuneBladeOfEvolution ), 8550000, 1, 0x2D32, 1266 ) );
				Add( new GenericBuyInfo( "Sai Of Evolution", typeof( SaiOfEvolution ), 8650000, 1, 0x27AF, 1266 ) );
				Add( new GenericBuyInfo( "Scepter Of Evolution", typeof( ScepterOfEvolution ), 8550000, 1, 0x26BC, 1266 ) );
				Add( new GenericBuyInfo( "Scimitar Of Evolution", typeof( ScimitarOfEvolution ), 8650000, 1, 0x13B6, 1266 ) );
				Add( new GenericBuyInfo( "Scythe Of Evolution", typeof( ScytheOfEvolution ), 8450000, 1, 0x26BA, 1266 ) );
				Add( new GenericBuyInfo( "Short Spear Of Evolution", typeof( ShortSpearOfEvolution ), 8650000, 1, 0x1403, 1266 ) );
				Add( new GenericBuyInfo( "Skinning Knife Of Evolution", typeof( SkinningKnifeOfEvolution ), 8850000, 1, 0xEC4, 1266 ) );
				Add( new GenericBuyInfo( "Spear Of Evolution", typeof( SpearOfEvolution ), 8650000, 1, 0xF62, 1266 ) );
				Add( new GenericBuyInfo( "Tekagi Of Evolution", typeof( TekagiOfEvolution ), 8750000, 1, 0x27Ab, 1266 ) );
				Add( new GenericBuyInfo( "Tessen Of Evolution", typeof( TessenOfEvolution ), 8650000, 1, 0x27A3, 1266 ) );
				Add( new GenericBuyInfo( "Thin Longsword Of Evolution", typeof( ThinLongswordOfEvolution ), 8550000, 1, 0x13B8, 1266 ) );
				Add( new GenericBuyInfo( "Two Handed Axe Of Evolution", typeof( TwoHandedAxeOfEvolution ), 8850000, 1, 0x1443, 1266 ) );
				Add( new GenericBuyInfo( "Viking Sword Of Evolution", typeof( VikingSwordOfEvolution ), 8750000, 1, 0x13B9, 1266 ) );
				Add( new GenericBuyInfo( "Wakizashi Of Evolution", typeof( WakizashiOfEvolution ), 8850000, 1, 0x27A4, 1266 ) );
				Add( new GenericBuyInfo( "War Axe Of Evolution", typeof( WarAxeOfEvolution ), 8650000, 1, 0x13B0, 1266 ) );
				Add( new GenericBuyInfo( "War Cleaver Of Evolution", typeof( WarCleaverOfEvolution ), 8550000, 1, 0x2D2F, 1266 ) );
				Add( new GenericBuyInfo( "War Fork Of Evolution", typeof( WarForkOfEvolution ), 8350000, 1, 0x1405, 1266 ) );
				Add( new GenericBuyInfo( "War Hammer Of Evolution", typeof( WarHammerOfEvolution ), 8450000, 1, 0x1439, 1266 ) );
				Add( new GenericBuyInfo( "War Mace Of Evolution", typeof( WarMaceOfEvolution ), 8550000, 1, 0x1407, 1266 ) );
				Add( new GenericBuyInfo( "Yumi Of Evolution", typeof( YumiOfEvolution ), 8650000, 1, 0x27A5, 1266 ) );
				Add( new GenericBuyInfo( "Bow Of Evolution", typeof( BowOfEvolution ), 8850000, 1, 0x13B2, 1266 ) );
				Add( new GenericBuyInfo( "Composite Bow Of Evolution", typeof( CompositeBowOfEvolution ), 8750000, 1, 0x26C2, 1266 ) );
				Add( new GenericBuyInfo( "Crossbow Of Evolution", typeof( CrossbowOfEvolution ), 8650000, 1, 0xF50, 1266 ) );
				Add( new GenericBuyInfo( "Elven Composite Longbow Of Evolution", typeof( ElvenCompositeLongbowOfEvolution ), 8850000, 1, 0x2D1E, 1266 ) );
				Add( new GenericBuyInfo( "Heavy Crossbow Of Evolution", typeof( HeavyCrossbowOfEvolution ), 8750000, 1, 0x13FD, 1266 ) );
				Add( new GenericBuyInfo( "Repeating Crossbow Of Evolution", typeof( RepeatingCrossbowOfEvolution ), 8850000, 1, 0x26C3, 1266 ) );
				Add( new GenericBuyInfo( "Shortbow Of Evolution", typeof( ShortbowOfEvolution ), 8950000, 1, 0x2D2B, 1266 ) );
																				
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{

			}
		}
	}
}
