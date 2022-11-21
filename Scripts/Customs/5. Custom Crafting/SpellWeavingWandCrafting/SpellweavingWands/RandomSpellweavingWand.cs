using System;
using Server;

namespace Server.Items
{
	public class RandomSpellWeavingWand
	{
		public static BaseSpellWeavingWand CreateSpellWeavingWand()
		{
			return CreateRandomSpellWeavingWand();
		}

		public static BaseSpellWeavingWand CreateRandomSpellWeavingWand( )
		{
			switch ( Utility.Random( 14 ) )
			{
				default:
				case  0: return new DryadAllureWand();
				case  1: return new WordOfDeathWand();
				case  2: return new ThunderstormWand();
				case  3: return new SummonFiendWand();
				case  4: return new SummonFeyWand();
				case  5: return new ReaperFormWand();
				case  6: return new NatureFuryWand();
				case  7: return new GiftOfRenewalWand();
				case  8: return new GiftOfLifeWand();
				case  9: return new EtherealVoyageWand();
				case 10: return new EssenceOfWindWand();
                                case 11: return new WildfireWand();
                                case 12: return new ArcaneEmpowermentWand();
                                case 13: return new ArcaneCircleWand();
			}
		}
	}
}