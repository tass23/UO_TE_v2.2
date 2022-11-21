using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Network;
using Server.Regions;

namespace Server.Gumps
{
	public class DestoryCityStructureGump : Gump
	{
		private CivicSign m_Sign;

		public DestoryCityStructureGump( CivicSign sign, Mobile from ) : base( 50, 50 )
		{
			m_Sign = sign;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(45, 34, 260, 91, 5120);
			AddLabel(64, 36, 1149, @"Are You Sure You Want To Demolish");
			AddLabel(129, 53, 1149, @"This Building?");
			AddButton(84, 88, 247, 248, 1, GumpButtonType.Reply, 0);
			AddButton(200, 88, 241, 242, 2, GumpButtonType.Reply, 0);
		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 ) // Previous page
         		{
				foreach ( Item item in m_Sign.toDelete )
				{
					if ( item is WoodCityBankAddon )
					{
						from.AddToBackpack( new WoodCityBankDeed() );
					}
					else if ( item is StoneCityBankAddon )
					{
						from.AddToBackpack( new StoneCityBankDeed() );
					}
					else if ( item is PlasterCityBankAddon )
					{
						from.AddToBackpack( new PlasterCityBankDeed() );
					}
					else if ( item is FieldstoneCityBankAddon )
					{
						from.AddToBackpack( new FieldstoneCityBankDeed() );
					}
					else if ( item is SandstoneCityBankAddon )
					{
						from.AddToBackpack( new SandstoneCityBankDeed() );
					}
					else if ( item is NecroCityBankAddon )
					{
						from.AddToBackpack( new NecroCityBankDeed() );
					}
					else if ( item is MarbleCityBankAddon )
					{
						from.AddToBackpack( new MarbleCityBankDeed() );
					}
					else if ( item is AsianCityBankAddon )
					{
						from.AddToBackpack( new AsianCityBankDeed() );
					}

					if ( item is WoodCityStableAddon )
					{
						from.AddToBackpack( new WoodCityStableDeed() );
					}
					else if ( item is StoneCityStableAddon )
					{
						from.AddToBackpack( new StoneCityStableDeed() );
					}
					else if ( item is PlasterCityStableAddon )
					{
						from.AddToBackpack( new PlasterCityStableDeed() );
					}
					else if ( item is FieldstoneCityStableAddon )
					{
						from.AddToBackpack( new FieldstoneCityStableDeed() );
					}
					else if ( item is SandstoneCityStableAddon )
					{
						from.AddToBackpack( new SandstoneCityStableDeed() );
					}
					else if ( item is NecroCityStableAddon )
					{
						from.AddToBackpack( new NecroCityStableDeed() );
					}
					else if ( item is MarbleCityStableAddon )
					{
						from.AddToBackpack( new MarbleCityStableDeed() );
					}
					else if ( item is AsianCityStableAddon )
					{
						from.AddToBackpack( new AsianCityStableDeed() );
					}

					if ( item is WoodCityTavernAddon )
					{
						from.AddToBackpack( new WoodCityTavernDeed() );
					}
					else if ( item is StoneCityTavernAddon )
					{
						from.AddToBackpack( new StoneCityTavernDeed() );
					}
					else if ( item is PlasterCityTavernAddon )
					{
						from.AddToBackpack( new PlasterCityTavernDeed() );
					}
					else if ( item is FieldstoneCityTavernAddon )
					{
						from.AddToBackpack( new FieldstoneCityTavernDeed() );
					}
					else if ( item is SandstoneCityTavernAddon )
					{
						from.AddToBackpack( new SandstoneCityTavernDeed() );
					}
					else if ( item is NecroCityTavernAddon )
					{
						from.AddToBackpack( new NecroCityTavernDeed() );
					}
					else if ( item is MarbleCityTavernAddon )
					{
						from.AddToBackpack( new MarbleCityTavernDeed() );
					}
					else if ( item is AsianCityTavernAddon )
					{
						from.AddToBackpack( new AsianCityTavernDeed() );
					}

					if ( item is WoodCityHealerAddon )
					{
						from.AddToBackpack( new WoodCityHealerDeed() );
					}
					else if ( item is StoneCityHealerAddon )
					{
						from.AddToBackpack( new StoneCityHealerDeed() );
					}
					else if ( item is PlasterCityHealerAddon )
					{
						from.AddToBackpack( new PlasterCityHealerDeed() );
					}
					else if ( item is FieldstoneCityHealerAddon )
					{
						from.AddToBackpack( new FieldstoneCityHealerDeed() );
					}
					else if ( item is SandstoneCityHealerAddon )
					{
						from.AddToBackpack( new SandstoneCityHealerDeed() );
					}
					else if ( item is NecroCityHealerAddon )
					{
						from.AddToBackpack( new NecroCityHealerDeed() );
					}
					else if ( item is MarbleCityHealerAddon )
					{
						from.AddToBackpack( new MarbleCityHealerDeed() );
					}
					else if ( item is AsianCityHealerAddon )
					{
						from.AddToBackpack( new AsianCityHealerDeed() );
					}

					if ( item is WoodCityMoongateAddon )
					{
						from.AddToBackpack( new WoodCityMoongateDeed() );
					}
					else if ( item is StoneCityMoongateAddon )
					{
						from.AddToBackpack( new StoneCityMoongateDeed() );
					}
					else if ( item is SandstoneCityMoongateAddon )
					{
						from.AddToBackpack( new SandstoneCityMoongateDeed() );
					}
					else if ( item is NecroCityMoongateAddon )
					{
						from.AddToBackpack( new NecroCityMoongateDeed() );
					}
					else if ( item is MarbleCityMoongateAddon )
					{
						from.AddToBackpack( new MarbleCityMoongateDeed() );
					}
					else if ( item is AsianCityMoongateAddon )
					{
						from.AddToBackpack( new AsianCityMoongateDeed() );
					}

					else if ( item is SmallCityGardenAddon )
					{
						from.AddToBackpack( new SmallCityGardenDeed() );
					}
					else if ( item is MedCityGardenAddon )
					{
						from.AddToBackpack( new MediumCityGardenDeed() );
					}
					else if ( item is LargeCityGardenAddon )
					{
						from.AddToBackpack( new LargeCityGardenDeed() );
					}

					else if ( item is SmallCityParkAddon )
					{
						from.AddToBackpack( new SmallCityParkDeed() );
					}
					else if ( item is MedCityParkAddon )
					{
						from.AddToBackpack( new MediumCityParkDeed() );
					}
					else if ( item is LargeCityParkAddon )
					{
						from.AddToBackpack( new LargeCityParkDeed() );
					}
				}

				if ( m_Sign.toDelete != null ) // Delete all items needed
				{
					foreach( Item i in m_Sign.toDelete )
					{
						if ( i != null )
							i.Delete();
					}
				}

				if ( m_Sign.Type == CivicSignType.Bank )
				{
					m_Sign.Stone.HasBank = false;
				}
				else if ( m_Sign.Type == CivicSignType.Stable )
				{
					m_Sign.Stone.HasStable = false;
				}
				else if ( m_Sign.Type == CivicSignType.Tavern )
				{
					m_Sign.Stone.HasTavern = false;
				}
				else if ( m_Sign.Type == CivicSignType.Healer )
				{
					m_Sign.Stone.HasHealer = false;
				}
				else if ( m_Sign.Type == CivicSignType.Moongate )
				{
					m_Sign.Stone.HasMoongate = false;
				}
				else if ( m_Sign.Type == CivicSignType.Garden )
				{
					if ( m_Sign.Stone.Gardens.Contains( this ) )
						m_Sign.Stone.Gardens.Remove( this );
				}
				else if ( m_Sign.Type == CivicSignType.Park )
				{
					if ( m_Sign.Stone.Parks.Contains( this ) )
						m_Sign.Stone.Parks.Remove( this );
				}

				m_Sign.Delete();
			}
		}
	}
}