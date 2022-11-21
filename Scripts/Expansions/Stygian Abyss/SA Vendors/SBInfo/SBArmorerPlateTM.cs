using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBArmorerPlateTM : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBArmorerPlateTM() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 	
				
				Add( new GenericBuyInfo( typeof( GargishPlateKilt ), 104, 20, 0x30C, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishPlateChest ), 243, 20, 0x30A, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishPlateLegs ), 218, 20, 0x30E, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishPlateArms ), 188, 20, 0x308, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishPlateWingArmor ), 155, 20, 0x457E, 0 ) );

                Add(new GenericBuyInfo(typeof(GargishPlateKilt), 104, 20, 0x30B, 0));
                Add(new GenericBuyInfo(typeof(GargishPlateChest), 243, 20, 0x309, 0));
                Add(new GenericBuyInfo(typeof(GargishPlateLegs), 218, 20, 0x30D, 0));
                Add(new GenericBuyInfo(typeof(GargishPlateArms), 188, 20, 0x307, 0));

			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				

                Add(typeof(GargishPlateKilt), 25);
                Add(typeof(GargishPlateChest), 33);
                Add(typeof(GargishPlateLegs), 60);
                Add(typeof(GargishPlateArms), 62);
                Add(typeof(GargishPlateWingArmor), 115);

                Add(typeof(GargishPlateKilt), 25);
                Add(typeof(GargishPlateChest), 33);
                Add(typeof(GargishPlateLegs), 60);
                Add(typeof(GargishPlateArms), 62);

			} 
		} 
	} 
}