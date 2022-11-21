using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBTailorTM : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBTailorTM() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 	
				
				Add( new GenericBuyInfo( typeof( GargishClothKilt ), 67, 20, 0x408, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishClothChest ), 137, 20, 0x406, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishClothLegs ), 114, 20, 0x40A, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishClothArms ), 95, 20, 0x404, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishClothWingArmor ), 109, 20, 0x45A4, 0 ) );

                Add(new GenericBuyInfo(typeof(GargishClothKilt), 67, 20, 0x407, 0));
                Add(new GenericBuyInfo(typeof(GargishClothChest), 137, 20, 0x405, 0));
                Add(new GenericBuyInfo(typeof(GargishClothLegs), 218, 20, 0x409, 0));
                Add(new GenericBuyInfo(typeof(GargishClothArms), 188, 20, 0x403, 0));

		Add(new GenericBuyInfo(typeof(GargishFancyRobe), 64, 20, 0x4002, 0));
		Add(new GenericBuyInfo(typeof(GargishRobe), 48, 20, 0x4000, 0));

			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				

                Add(typeof(GargishClothKilt), 15);
                Add(typeof(GargishClothChest), 23);
                Add(typeof(GargishClothLegs), 47);
                Add(typeof(GargishClothArms), 49);
                Add(typeof(GargishClothWingArmor), 81);

                Add(typeof(GargishClothKilt), 15);
                Add(typeof(GargishClothChest), 23);
                Add(typeof(GargishClothLegs), 47);
                Add(typeof(GargishClothArms), 49);

		Add(typeof(GargishFancyRobe), 30);
		Add(typeof(GargishRobe), 20);

			} 
		} 
	} 
}