using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBAnimalCrafter: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAnimalCrafter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
			Add( new GenericBuyInfo( typeof( AnimalItemCraftingTools ), 1000, 20, 0x1EB8, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( CraftableEtherealBeetle ), 100 );
				Add( typeof( CraftableEtherealHorse ), 100 );
				Add( typeof( CraftableEtherealKirin ), 100 );
				Add( typeof( CraftableEtherealLlama ), 100 );
				Add( typeof( CraftableEtherealOstard ), 100 );
				Add( typeof( CraftableEtherealRidgeback ), 100 );
				Add( typeof( CraftableEtherealSwampDragon ), 100 );
				Add( typeof( CraftableEtherealUnicorn ), 100 );
				Add( typeof( HorseShoe ), 10 );
				Add( typeof( HorseSaddle1 ), 1000 );
				Add( typeof( HorseSaddle2 ), 1000 );
				Add( typeof( Hay1 ), 500 );
				Add( typeof( TrainingDragonDeed ), 5000 );
			}
		}
	}
}
