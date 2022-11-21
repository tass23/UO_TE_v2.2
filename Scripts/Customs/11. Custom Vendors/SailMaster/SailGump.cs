using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{
	public class SailGump : Gump
	{
		private const int FieldsPerPage = 14;

		private Mobile m_From;
		private Mobile m_Mobile;
		private Point3D m_StartPoint;
		private Point3D m_SailTo;

		public SailGump ( Mobile from, Point3D startPoint) : base ( 20, 30 )
		{
			m_From = from;
			m_StartPoint = startPoint;

			AddPage ( 0 );
			AddBackground( 0, 0, 410, 310, 5054 );

			AddImageTiled( 10, 10, 390, 23, 0x52 );
			AddImageTiled( 11, 11, 388, 21, 0xBBC );

			AddLabel( 110, 11, 0, "Where would you like to sail?" );

			AddButton( 11, 35, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1 );
			AddLabel( 30, 34, 0, "Sail to Moonglow" );
			
			AddButton( 11, 57, 0x15E3, 0x15E7, 2, GumpButtonType.Reply, 1 );
			AddLabel( 30, 56, 0, "Sail to Britain" );
			
			AddButton( 11, 79, 0x15E3, 0x15E7, 3, GumpButtonType.Reply, 1 );
			AddLabel( 30, 78, 0, "Sail to J'helom" );
			
			AddButton( 11, 101, 0x15E3, 0x15E7, 4, GumpButtonType.Reply, 1 );
			AddLabel( 30, 100, 0, "Sail to Magincia" );
			
			AddButton( 11, 123, 0x15E3, 0x15E7, 5, GumpButtonType.Reply, 1 );
			AddLabel( 30, 122, 0, "Sail to Nujel'm" );
			
			//AddButton( 11, 145, 0x15E3, 0x15E7, 6, GumpButtonType.Reply, 1 );
			//AddLabel( 30, 144, 0, "Sail to Haven" );
			
			AddButton( 11, 145, 0x15E3, 0x15E7, 6, GumpButtonType.Reply, 1 );
			AddLabel( 30, 144, 0, "Sail to Skara Brae" );
			
			AddButton( 11, 167, 0x15E3, 0x15E7, 7, GumpButtonType.Reply, 1 );
			AddLabel( 30, 166, 0, "Sail to Trinsic" );
			
			AddButton( 11, 189, 0x15E3, 0x15E7, 8, GumpButtonType.Reply, 1 );
			AddLabel( 30, 188, 0, "Sail to Vesper" );
			
			AddButton( 11, 211, 0x15E3, 0x15E7, 9, GumpButtonType.Reply, 1 );
			AddLabel( 30, 210, 0, "Sail to Papua" );
			
			AddButton( 11, 233, 0x15E3, 0x15E7, 10, GumpButtonType.Reply, 1 );
			AddLabel( 30, 232, 0, "Sail to the Vendor Mall" );

		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			int xd;
			int yd;
			int Distance;
			Container pack = from.Backpack;

			switch ( info.ButtonID )
			{
				case 1: // Moonglow
				{
					xd = 4410 - m_StartPoint.X;
					yd = 1035 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(4410,1035,-4);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				case 2: // Britain
				{
					xd = 1466 - m_StartPoint.X;
					yd = 1765 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(1466,1765,-2);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				case 3: // J'helom
				{
					xd = 1503 - m_StartPoint.X;
					yd = 3705 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(1503,3705,-4);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				case 4: // Magincia
				{
					xd = 3665 - m_StartPoint.X;
					yd = 2297 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(3665,2297,-4);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				case 5: // Nujel'm
				{
					xd = 3810 - m_StartPoint.X;
					yd = 1278 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(3810,1278,-4);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				/*
				case 6: 
				{
					xd = 3665 - m_StartPoint.X;
					yd = 2678 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(3665,2678,-4);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				*/
				case 6: // Skara Brae
				{
					xd = 656 - m_StartPoint.X;
					yd = 2244 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(656,2244,-4);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				case 7: // Trinsic
				{
					xd = 2088 - m_StartPoint.X;
					yd = 2854 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(2088,2854,-4);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				case 8: // Vesper
				{
					xd = 3025 - m_StartPoint.X;
					yd = 836 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(3025,836,-4);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				case 9: // Papua
				{
					xd = 5831 - m_StartPoint.X;
					yd = 3252 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(5831,3252,0);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				case 10: // Vendor Mall
				{
					xd = 4574 - m_StartPoint.X;
					yd = 2401 - m_StartPoint.Y;
					Distance = (int)Math.Sqrt(xd*xd + yd*yd);
					m_SailTo = new Point3D(4576,2402,0);
					from.SendGump(new InternalSailGump(from,Distance,new Point3D(4419,831,-1), m_SailTo));
					
					
					break;
				}
				default:
				{
					break;
				}
			}
		}
	}
	
	public class InternalSailGump : Gump
	{
		int m_cost;
		Point3D m_sendTo;
		Point3D m_SailTo;
		
		public InternalSailGump ( Mobile from, int cost, Point3D sendTo, Point3D sailTo) : base ( 20, 30 )
		{
			m_cost = cost;
			m_sendTo = sendTo;
			m_SailTo = sailTo;
			
			AddPage ( 0 );
			AddBackground( 0, 0, 410, 107, 5054 );

			AddImageTiled( 10, 10, 390, 23, 0x52 );
			AddImageTiled( 11, 11, 388, 21, 0xBBC );

			AddLabel( 100, 11, 0, "Sailing this far will cost you "+ m_cost/4 + " Gold"  );

			AddButton( 11, 35, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1 );
			AddLabel( 30, 34, 0, "Pay it" );
			AddButton( 11, 57, 0x15E3, 0x15E7, 2, GumpButtonType.Reply, 1 );
			AddLabel( 30, 56, 0, "Show Sailboat Membershipcard" );
			AddButton( 11, 79, 0x15E3, 0x15E7, 3, GumpButtonType.Reply, 1 );
			AddLabel( 30, 78, 0, "Uhm, no thank you" );

		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			Container pack = from.Backpack;
			SailTimer waitTime;

			switch ( info.ButtonID )
			{
				case 1: 
				{
					if ( pack.ConsumeTotal( typeof( Gold ), m_cost/4 ) )
					{
						from.Location = m_sendTo;
						if (m_cost > 60)
							m_cost = 60;
						waitTime = new SailTimer(from,m_SailTo,TimeSpan.FromSeconds(m_cost/10 ));
						waitTime.Start();
						//Give items
					}
					else
					{
						from.SendMessage("HA, You cannot afford to sail with that little gold");
					}
					break;
				}
				case 2: 
				{
					if ( pack.ConsumeTotal( typeof( SailboatMembershipcard ), 0 ) )
					{
						from.Location = m_sendTo;
						if (m_cost > 60)
							m_cost = 60;
						waitTime = new SailTimer(from,m_SailTo,TimeSpan.FromSeconds(m_cost/10 ));
						waitTime.Start();
					}
					else
					{
						from.SendMessage("It might be usefull if you actualy have a membership card...");
					}
					break;
				}
				case 3: 
				{
					from.SendMessage("Perhaps later then");
					break;
				}
				
				default:
				{
					from.SendMessage("Perhaps later then");
					break;
				}
			}
		}
	}
	
	public class SailTimer : Timer
        {
		Mobile from;
		Point3D finalSpot;

		public SailTimer( Mobile m_from, Point3D m_finalSpot, TimeSpan duration ) : base( duration )
		{
			from = m_from;
			finalSpot = m_finalSpot;
		}

		protected override void OnTick()
		{
			from.Location = finalSpot;
			from.SendMessage("Hope you enjoyed your trip!");
			Stop();
		}
	}
}