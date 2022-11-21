/***************************************************************************/
/*			ResourceBox.cs | ResourceBoxGump.cs | StorageTypes.cs					*/
/*							Created by A_Li_N													*/
/*				Credits :																		*/
/*						Original Gump Layout - Lysdexic									*/
/*						Hashtable help - UOT and daat99									*/
/***************************************************************************/

using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class ResourceBoxGump : Gump
	{
		private Mobile m_From;
		private ResourceBox m_Box;
		private Pages m_Page;
		private Type[] m_Types;

		public enum Pages
		{
			Start,
			Logs,
			Boards,
			Ingots,
			Granites,
			Scales,
			Leathers,
			Misc,
			Reagents,

			MoreLogs,
			MoreBoards,
			MoreIngots,
			MoreGranites,
			MoreScales,
			MoreLeathers,
			MoreMisc,
			MoreReagents
		}

		public ResourceBoxGump( Mobile from, ResourceBox box, Pages page ) : base( 25, 25 )
		{
			m_From = from;
			m_Box = box;
			m_Page = page;

			AddPage( 0 );

			AddBackground( 50, 10, 455, 280, 83 );
			AddImageTiled( 58, 20, 438, 262, 2624 );
			AddAlphaRegion( 58, 20, 438, 262 );

			AddButton( 75, 25, 4026, 4027, 1, GumpButtonType.Reply, 0 );
			AddLabel( 110, 25, 0x8AB, "Add Resource" );

			AddPage( 1 );

			if( m_Page == Pages.Start )
			{
				AddLabel( 225, 25, 0x480, "Choose Resource" );

				AddLabel( 110, 75, 1152, "Logs" );
				AddButton( 75, 75, 4005, 4007, 10, GumpButtonType.Reply, 0 );
				AddLabel( 110, 100, 1152, "Boards" );
				AddButton( 75, 100, 4005, 4007, 11, GumpButtonType.Reply, 0 );
				AddLabel( 110, 125, 1152, "Ingots" );
				AddButton( 75, 125, 4005, 4007, 12, GumpButtonType.Reply, 0 );
				AddLabel( 110, 150, 1152, "Granites" );
				AddButton( 75, 150, 4005, 4007, 13, GumpButtonType.Reply, 0 );
				AddLabel( 110, 175, 1152, "Scales" );
				AddButton( 75, 175, 4005, 4007, 14, GumpButtonType.Reply, 0 );
				AddLabel( 110, 200, 1152, "Leathers" );
				AddButton( 75, 200, 4005, 4007, 15, GumpButtonType.Reply, 0 );
				AddLabel( 110, 225, 1152, "Misc" );
				AddButton( 75, 225, 4005, 4007, 16, GumpButtonType.Reply, 0 );
				AddLabel( 110, 250, 1152, "Reagents" );
				AddButton( 75, 250, 4005, 4007, 17, GumpButtonType.Reply, 0 );

				if( StorageTypes.Logs.Length > 16 )
				{
					AddLabel( 310, 75, 1152, "More Woods" );
					AddButton( 275, 75, 4005, 4007, 18, GumpButtonType.Reply, 0 );
				}
				if( StorageTypes.Boards.Length > 16 )
				{
					AddLabel( 310, 100, 1152, "More Boards" );
					AddButton( 275, 100, 4005, 4007, 19, GumpButtonType.Reply, 0 );
				}
				if( StorageTypes.Ingots.Length > 16 )
				{
					AddLabel( 310, 125, 1152, "More Ingots" );
					AddButton( 275, 125, 4005, 4007, 20, GumpButtonType.Reply, 0 );
				}
				if( StorageTypes.Granites.Length > 16 )
				{
					AddLabel( 310, 150, 1152, "More Granites" );
					AddButton( 275, 150, 4005, 4007, 21, GumpButtonType.Reply, 0 );
				}
				if( StorageTypes.Scales.Length > 16 )
				{
					AddLabel( 310, 175, 1152, "More Scales" );
					AddButton( 275, 175, 4005, 4007, 22, GumpButtonType.Reply, 0 );
				}
				if( StorageTypes.Leathers.Length > 16 )
				{
					AddLabel( 310, 200, 1152, "More Leathers" );
					AddButton( 275, 200, 4005, 4007, 23, GumpButtonType.Reply, 0 );
				}
				if( StorageTypes.Misc.Length > 16 )
				{
					AddLabel( 310, 225, 1152, "More Misc" );
					AddButton( 275, 225, 4005, 4007, 24, GumpButtonType.Reply, 0 );
				}
				if( StorageTypes.Reagents.Length > 16 )
				{
					AddLabel( 310, 250, 1152, "More Reagents" );
					AddButton( 275, 250, 4005, 4007, 25, GumpButtonType.Reply, 0 );
				}
			}

			else
			{
				AddLabel( 225, 25, 0x480, AddLabelsButtonsAmounts() );
				AddButton( 425, 25, 4014, 4015, 2, GumpButtonType.Reply, 0 );
				AddLabel( 460, 25, 0x8AB, "Back" );
			}
		}


		private string AddLabelsButtonsAmounts()
		{
			string Label = "";
			int Offset = 0;
			switch( (int)m_Page )
			{
				case (int)Pages.Logs:			{m_Types = StorageTypes.Logs;			Label = "Logs";		Offset = 0;		break;}
				case (int)Pages.Boards:			{m_Types = StorageTypes.Boards;		Label = "Boards";		Offset = 0;		break;}
				case (int)Pages.Ingots:			{m_Types = StorageTypes.Ingots;		Label = "Ingots";		Offset = 0;		break;}
				case (int)Pages.Granites:		{m_Types = StorageTypes.Granites;	Label = "Granites";	Offset = 0;		break;}
				case (int)Pages.Scales:			{m_Types = StorageTypes.Scales;		Label = "Scales";		Offset = 0;		break;}
				case (int)Pages.Leathers: 		{m_Types = StorageTypes.Leathers;	Label = "Leathers";	Offset = 0;		break;}
				case (int)Pages.Misc: 			{m_Types = StorageTypes.Misc;			Label = "Misc";		Offset = 0;		break;}
				case (int)Pages.Reagents: 		{m_Types = StorageTypes.Reagents;	Label = "Reagents";	Offset = 0;		break;}

				case (int)Pages.MoreLogs: 		{m_Types = StorageTypes.Logs;			Label = "More Logs";		Offset = 16;	break;}
				case (int)Pages.MoreBoards: 	{m_Types = StorageTypes.Boards;		Label = "More Boards";	Offset = 16;	break;}
				case (int)Pages.MoreIngots:	{m_Types = StorageTypes.Ingots;		Label = "More Ingots";	Offset = 16;	break;}
				case (int)Pages.MoreGranites:	{m_Types = StorageTypes.Granites;	Label = "More Granites";Offset = 16;	break;}
				case (int)Pages.MoreScales:	{m_Types = StorageTypes.Scales;		Label = "More Scales";	Offset = 16;	break;}
				case (int)Pages.MoreLeathers:	{m_Types = StorageTypes.Leathers;	Label = "More Leathers";Offset = 16;	break;}
				case (int)Pages.MoreMisc:		{m_Types = StorageTypes.Misc;			Label = "More Misc";		Offset = 16;	break;}
				case (int)Pages.MoreReagents:	{m_Types = StorageTypes.Reagents;	Label = "More Reagents";Offset = 16;	break;}
			}

			for( int i = Offset; i < m_Types.Length && i < 16 + Offset; i++ )
			{
				Type type = m_Types[i];

				if( !m_Box.Resources.ContainsKey( type ) )
					continue;

				if( (i - Offset) <= 7 )
				{
					AddLabel( 110, 75+((i-Offset)*25), 1152, type.Name );
					if( (int)m_Box.Resources[type] >= 100000 )
						AddLabel( 225, 75+((i-Offset)*25), 1152, "99999" );
					else
						AddLabel( 225, 75+((i-Offset)*25), 1152, ((int)m_Box.Resources[type]).ToString() );
					AddButton( 75, 75+((i-Offset)*25), 4029, 4030, i+100, GumpButtonType.Reply, 0 );
				}

				else
				{
					AddLabel( 310, 75+((i-8-Offset)*25), 1152, type.Name );
					if( (int)m_Box.Resources[type] >= 100000 )
						AddLabel( 225, 75+((i-8-Offset)*25), 1152, "99999" );
					else
						AddLabel( 425, 75+((i-8-Offset)*25), 1152, ((int)m_Box.Resources[type]).ToString() );
					AddButton( 275, 75+((i-8-Offset)*25), 4029, 4030, i+100, GumpButtonType.Reply, 0 );
				}
			}
			return Label;
		}

      private class ResourceBoxTarget : Target
      {
			private ResourceBox m_Box;
			private Pages m_Page;

			public ResourceBoxTarget( ResourceBox box, Pages page ) : base( 18, false, TargetFlags.None )
			{
					m_Box = box;
					m_Page = page;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Box.Deleted )
					return;
				if( targeted is Item )
				{
					if( m_Box.TryAdd( targeted as Item ) )
						from.SendMessage( "Resource added, please choose another." );
					else
						from.SendMessage( "Resource could not be added, please try another." );

					from.Target = new ResourceBoxTarget( m_Box, m_Page );
				}
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
			{
				from.SendGump( new ResourceBoxGump( from, m_Box, m_Page ) );
			}
      }

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			int BID = info.ButtonID;

			if( BID == 1 )
			{
				if ( from.InRange( m_Box.GetWorldLocation(), 3 ) )
				{
					from.SendMessage( "Target a resource to add" );
					from.Target = new ResourceBoxTarget( m_Box, m_Page );
				}
				else
				{
					from.SendMessage( "You are not in range of your Resource Box!" );
					from.CloseGump( typeof (ResourceBoxGump) );
				}
			}

			else if( BID == 2 )
			{
				from.SendGump( new ResourceBoxGump( from, m_Box, Pages.Start ) );
			}

			else if( BID >= 10 && BID < 26 )
			{
				from.SendGump( new ResourceBoxGump( from, m_Box, (Pages)(BID-9) ) );
			}

			else if( BID >= 100 && BID < 132 )
			{
				Type type = m_Types[BID-100];
				if ( from.InRange( m_Box.GetWorldLocation(), 3 ) )
				{
					from.SendMessage( "Enter Amount, you may only take out 1 granite or tool at a time." );
					from.Prompt = new ExtractPrompt( type, m_Box, m_Page );
				}
				    
				 else
				{
					from.SendMessage( "You are not in range of your Resource Box!" );
					from.CloseGump( typeof (ResourceBoxGump) );
				}   
			}
		
		}
		
		private class ExtractPrompt : Prompt
		{
			private Type m_type;
			private ResourceBox m_Box;
			private Pages m_Page;
			
			public ExtractPrompt( Type type, ResourceBox box, Pages page  )
			{
				m_type = type;
				m_Box = box;
				m_Page = page;
			}
			
			public override void OnResponse( Mobile typer, string text )
			{
				
				int amount = Utility.ToInt32( text );
				if ( amount < 10000 )
					m_Box.ExtractResource( typer, m_type, amount );
				else
					typer.SendMessage( "You may only take a max of 10,000 items at once." );
			
				typer.SendGump( new ResourceBoxGump( typer, m_Box, m_Page ) );
				
					
				
				
			}
		}
	}
}
