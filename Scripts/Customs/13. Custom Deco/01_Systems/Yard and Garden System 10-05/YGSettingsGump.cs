using System;
using System.Text;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
	public class YGSettingsGump : Gump
	{
		public YardWand m_Wand;
		public YGSettingsGump(YardWand wand, Mobile from)
			: base( 0, 0 )
		{
			m_Wand = wand;
			string xstart = m_Wand.xstart.ToString();
			string ystart = m_Wand.ystart.ToString();
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(0, 0, 200, 200, 9200);
			AddAlphaRegion(10, 9, 180, 180);
			AddLabel(71, 16, 0, @"Settings");
			AddBackground(46, 51, 108, 28, 0x2486);
			AddBackground(46, 91, 108, 28, 0x2486);
			AddTextEntry(50, 55, 100, 20, 0, (int)Buttons.XCoordTextBox, "X - " + xstart);
			AddTextEntry(50, 95, 100, 20, 0, (int)Buttons.YCoordTextBox, "Y - " + ystart);
			AddButton(68, 145, 238, 239, (int)Buttons.OK, GumpButtonType.Reply, 0);

		}

		public enum Buttons
		{
			Exit,
			XCoordTextBox,
			YCoordTextBox,
			OK,
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			switch( info.ButtonID )
			{
				case (int)Buttons.OK:
				{
					TextRelay xrelay = info.GetTextEntry( (int)Buttons.XCoordTextBox );
					TextRelay yrelay = info.GetTextEntry( (int)Buttons.YCoordTextBox );
					string xtext = ( xrelay == null ? null : xrelay.Text.Trim() );
					string ytext = ( yrelay == null ? null : yrelay.Text.Trim() );
					if( xtext == null || xtext.Length == 0 || ytext == null || ytext.Length == 0 )
					{
						from.SendMessage("You must enter an integer value in each box. (0 , 400, 245 )");
					}
					else
					{
						int x = m_Wand.xstart;
						int y = m_Wand.ystart;
						try
						{
							x = Int32.Parse(xtext);
							y = Int32.Parse(ytext);
							m_Wand.xstart = x;
							m_Wand.ystart = y;
						}
						catch
						{
							from.SendMessage("You must enter an integer value in each box. (0 , 400, 245 )");
						}
					}
					from.SendGump( new YardGump( from, m_Wand ) );
					break;
				}
			}
		}
	}
}