using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBDeedCrafter: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDeedCrafter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
			Add( new GenericBuyInfo( typeof( DeedCraftingTool ), 100000, 20, 0x0FBF, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( CraftedGoldBricks ), 10000 );
				//Add( typeof( CraftedBeardRestylingDeed ), 50000 );
				Add( typeof( CraftedClothingBlessDeed ), 50000 );
				Add( typeof( CraftedGuildDeed ), 100 );
				//Add( typeof( CraftedHairRestylingDeed ), 50000 );
				Add( typeof( CraftedNameChangeDeed ), 50000 );
				Add( typeof( CraftedPetBondingDeed ), 50000 );
				Add( typeof( CraftedPetResurrectionDeed ), 50000 );
				Add( typeof( CraftedContractOfEmployment ), 10 );
				Add( typeof( CraftedSexChangeDeed ), 50000 );
				Add( typeof( CraftedSpellChannelingDeed ), 50000 );
			}
		}
	}
}
