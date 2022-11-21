using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBFlorist : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBFlorist()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{

					Add( new GenericBuyInfo( typeof( CampionFlowers1 ), 1500, 100, 3203, 0 ) );
					Add( new GenericBuyInfo( typeof( CampionFlowers2 ), 1500, 100, 3209, 0 ) );
					Add( new GenericBuyInfo( typeof( FoxgloveFlowers1 ), 1500, 100, 3204, 0 ) );
					Add( new GenericBuyInfo( typeof( FoxgloveFlowers2 ), 1500, 100, 3210, 0 ) );
					Add( new GenericBuyInfo( typeof( MorningGlories1 ), 1500, 100, 3380, 0 ) );
					Add( new GenericBuyInfo( typeof( MorningGlories2 ), 1500, 100, 7949, 0 ) );
					Add( new GenericBuyInfo( typeof( OrfluerFlowers1 ), 1500, 100, 3205, 0 ) );
					Add( new GenericBuyInfo( typeof( OrfluerFlowers2 ), 1500, 100, 3264, 0 ) );
					Add( new GenericBuyInfo( typeof( OrfluerFlowers3 ), 1500, 100, 3265, 0 ) );
					Add( new GenericBuyInfo( typeof( Poppies1 ), 1500, 100, 3262, 0 ) );
					Add( new GenericBuyInfo( typeof( Poppies2 ), 1500, 100, 3263, 0 ) );
					Add( new GenericBuyInfo( typeof( Poppies3 ), 1500, 100, 3206, 0 ) );
					Add( new GenericBuyInfo( typeof( RosesofTrinsic1 ), 1500, 100, 9036, 0 ) );
					Add( new GenericBuyInfo( typeof( RosesofTrinsic2 ), 1500, 100, 9037, 0 ) );
					Add( new GenericBuyInfo( typeof( SingleRoseOfTrinsic ), 1500, 100, 9035, 0 ) );
					Add( new GenericBuyInfo( typeof( SnowDrops ), 1500, 100, 3208, 0 ) );
					Add( new GenericBuyInfo( typeof( WhiteLillies ), 1500, 100, 3211, 0 ) );
					
					


			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{

					Add( typeof( FertileDirt ), 2 );
			}
		}
	}
}
