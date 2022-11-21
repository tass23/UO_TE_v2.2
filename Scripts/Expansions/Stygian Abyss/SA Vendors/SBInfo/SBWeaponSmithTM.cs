using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBWeaponSmithTM: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWeaponSmithTM() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( GargishAxe ), 22, 20, 0x48B2, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishBardiche ), 16, 20, 0x48B4, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishBattleAxe ), 16, 20, 0x48B0, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishBoneHarvester ), 28, 20, 0x48C6, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishButcherKnife ), 21, 20, 0x48B6, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishCleaver ), 19, 20, 0x48AE, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishDagger ), 20, 20, 0x902, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishDaisho ), 21, 20, 0x48D0, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishGnarledStaff ), 23, 20, 0x48B8, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishKatana ), 31, 20, 0x48BA, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishKryss ), 25, 20, 0x48BC, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishLance ), 31, 20, 0x48CA, 0 ) );
                Add( new GenericBuyInfo( typeof( GargishMaul ), 25, 20, 0x48C2, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishPike ), 27, 20, 0x48C8, 0 ) );
				Add( new GenericBuyInfo( typeof( GargishScythe ), 32, 20, 0x48C4, 0 ) );
                Add(new GenericBuyInfo(typeof(GargishTalwar), 32, 20, 0x908, 0));
                Add(new GenericBuyInfo(typeof(GargishTekagi), 31, 20, 0x48CE, 0));
                Add(new GenericBuyInfo(typeof(GargishTessen), 25, 20, 0x48CC, 0));
                Add(new GenericBuyInfo(typeof(GargishWarFork), 27, 20, 0x48BE, 0));
                Add(new GenericBuyInfo(typeof(GargishWarHammer), 32, 20, 0x48C0, 0));


                Add(new GenericBuyInfo(typeof(GargishKiteShield), 31, 20, 0x4201, 0));
                Add(new GenericBuyInfo(typeof(GargishOrderShield), 25, 20, 0x422A, 0));
                Add(new GenericBuyInfo(typeof(GargishWoodenShield), 27, 20, 0x4200, 0));
                			
	
			}
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 	
				Add( typeof( GargishAxe ), 13 );
				Add( typeof( GargishBardiche ), 26 );
				Add( typeof( GargishBattleAxe ), 15 );
				Add( typeof( GargishBoneHarvester ),16 );
				Add( typeof( GargishButcherKnife ), 11 );
				Add( typeof( GargishCleaver ), 16 );
				Add( typeof( GargishDagger ), 14 );
				Add( typeof( GargishDaisho ), 20 );
                Add( typeof( GargishGnarledStaff ), 30 );
				Add( typeof( GargishKatana ), 21 );
                Add( typeof( GargishKryss ), 7 );
				Add( typeof( GargishLance ), 7 );
				Add( typeof( GargishMaul ), 10 );
				Add( typeof( GargishPike ), 7 );
                Add( typeof( GargishScythe ), 8 );
                Add(typeof(GargishTalwar), 5);
				Add( typeof( GargishTekagi ), 13 );
				Add( typeof( GargishTessen ), 14 );
				Add( typeof( GargishWarFork ), 10 );
				Add( typeof( GargishWarHammer ), 12 );
				
                Add( typeof( GargishKiteShield ), 15 );
                Add( typeof( GargishOrderShield ), 27 );
				Add( typeof( GargishWoodenShield ), 17 );
				
				
			} 
		} 
	} 
}
