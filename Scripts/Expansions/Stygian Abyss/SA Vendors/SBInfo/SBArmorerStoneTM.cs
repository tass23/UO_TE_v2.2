using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBArmorerStoneTM : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBArmorerStoneTM() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 	
				
				Add( new GenericBuyInfo( typeof( GargishStoneKilt ), 104, 20, 0x288, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishStoneChest ), 243, 20, 0x286, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishStoneLegs ), 218, 20, 0x28A, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishStoneArms ), 188, 20, 0x284, 0 ) );
                Add(new GenericBuyInfo(typeof(GargishStoneWingArmor), 188, 20, 0x457E, 0));

                Add(new GenericBuyInfo(typeof(GargishStoneKilt), 104, 20, 0x287, 0));
                Add(new GenericBuyInfo(typeof(GargishStoneChest), 243, 20, 0x285, 0));
                Add(new GenericBuyInfo(typeof(GargishStoneLegs), 218, 20, 0x289, 0));
                Add(new GenericBuyInfo(typeof(GargishStoneArms), 188, 20, 0x283, 0));
				
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				

                Add(typeof(GargishStoneKilt), 25);
                Add(typeof(GargishStoneChest), 33);
                Add(typeof(GargishStoneLegs), 60);
                Add(typeof(GargishStoneArms), 62);

                Add(typeof(GargishStoneKilt), 25);
                Add(typeof(GargishStoneChest), 33);
                Add(typeof(GargishStoneLegs), 60);
                Add(typeof(GargishStoneArms), 62);
				
			} 
		} 
	} 
}