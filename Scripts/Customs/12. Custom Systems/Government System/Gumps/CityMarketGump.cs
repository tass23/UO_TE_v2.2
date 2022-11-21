using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;

namespace Server.Gumps
{
	public class CityMarketGump : Gump
	{
		private CityLandLord m_lord;
		
		public CityMarketGump( CityLandLord lord )	: base( 0, 0 )
		{
			
			m_lord = lord;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(59, 21, 286, 276, 9200);
			this.AddLabel(122, 44, 48, @"City Market Control");
			this.AddLabel(89, 111, 92, @"Rent Fee:");
			this.AddTextEntry(172, 111, 159, 20, 0, 0, @"");
			this.AddLabel(71, 84, 92, @"Current Rent:");
			this.AddLabel(195, 85, 92, String.Format( "{0}",m_lord.RentCost.ToString() ) );
			this.AddLabel(135, 130, 46, @"Rental Duration" );
			this.AddLabel(65, 150, 46, @"Changing this does not affect current vendors." );
			this.AddButton(83, 175, 209, 208, 1, GumpButtonType.Reply, 0);
			this.AddButton(83, 200, 209, 208, 2, GumpButtonType.Reply, 0);
			this.AddButton(83, 225, 209, 208, 3, GumpButtonType.Reply, 0);
			this.AddButton(83, 250, 209, 208, 4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 199, 92, @"Two Weeks");
			this.AddLabel(107, 174, 92, @"One Week");
			this.AddLabel(106, 223, 92, @"Three Weeks");
			this.AddLabel(107, 249, 92, @"Four Weeks");
			this.AddButton(297, 111, 2714, 2715, 5, GumpButtonType.Reply, 0);
			
			int index = (int)m_lord.Duration;
			switch ( index )
			{
				case 0 :
					{
						this.AddImage(233, 174, 211);
						break;
					}
				case 1:
					{
						this.AddImage(233, 199, 211);
						break;
					}
				case 2:
					{
						this.AddImage(233, 223, 211);
						break;
					}
				case 3:
					{
						this.AddImage(233, 249, 211);
						break;
					}
			}
			
			
		}
		
		public override void OnResponse( NetState state, RelayInfo info ) 
      		{
				Mobile from = state.Mobile;
				MallDuration duration;
				
				if ( from == null )
					return;
				
				if ( info.ButtonID == 5 ) //Check for amount
				{
					string rules = (string)info.GetTextEntry( 0 ).Text;
					try
					{
						int amount = Convert.ToInt32( rules );
						m_lord.RentCost = amount;
						from.SendMessage( "Rent amount changed." );
					}
					catch
					{
						from.SendMessage( "You did not enter a number or amount." );
					}
					from.CloseGump( typeof (CityMarketGump) );
					from.SendGump( new CityMarketGump( m_lord ) );
										
				}
				
				if ( info.ButtonID == 1 )
				{
					duration = MallDuration.OneWeek;
					m_lord.Duration = duration;
					from.SendMessage( "Duration Changed." );
					from.CloseGump( typeof (CityMarketGump) );
					from.SendGump( new CityMarketGump( m_lord ) );
				}
				
				if ( info.ButtonID == 2 )
				{
					duration = MallDuration.TwoWeeks;
					m_lord.Duration = duration;
					from.SendMessage( "Duration Changed." );
					from.CloseGump( typeof (CityMarketGump) );
					from.SendGump( new CityMarketGump( m_lord ) );
				}
				
				if ( info.ButtonID == 3 )
				{
					duration = MallDuration.ThreeWeeks;
					m_lord.Duration = duration;
					from.SendMessage( "Duration Changed." );
					from.CloseGump( typeof (CityMarketGump) );
					from.SendGump( new CityMarketGump( m_lord ) );
				}
				
				if ( info.ButtonID == 4 )
				{
					duration = MallDuration.FourWeeks;
					m_lord.Duration = duration;
					from.SendMessage( "Duration Changed." );
					from.CloseGump( typeof (CityMarketGump) );
					from.SendGump( new CityMarketGump( m_lord ) );
				}
			
			
			}

	}
}
