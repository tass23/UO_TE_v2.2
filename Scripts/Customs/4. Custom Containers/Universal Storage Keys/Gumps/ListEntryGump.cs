using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;
using Solaris.ItemStore;

namespace Server.Gumps
{
	//list entry gump for displaying the contents of a list entry object
	public class ListEntryGump : Gump
	{
		PlayerMobile _Owner;
		ListEntry _ListEntry;
		
		//used for seeking around on the page
		int _Y = 25;
		
		
		
		//page size
		protected int _Height;
		protected int _Width;
		
		//maximum entry listing height, for multi-page calculation
		public int MaxEntryDisplayHeight{ get{ return 300; } }
		
		//line spacing between entries
		public int EntryLineSpacing{ get{ return 20; } }
		
		//page number that this gump is displaying
		protected int _Page;
		
		//this is determined based on the number of entries and the maximum number to display per page
		protected int _MaxPages;
		
		//these are used to truncate the store entry listing to fit only a subset on the page
		protected int _ListingHeight;
		protected int _ListingStart;
		protected int _ListingEnd;
		
		//a filtered entry list
		protected List<ItemListEntry> _FilteredEntries;
				
		//public accessors for gump refreshing
		public Mobile Owner{ get{ return _Owner; } }
		public int Page{ get{ return _Page; } }
		//public accessor to the list entry
		public ListEntry ListEntry{ get{ return _ListEntry; } }
		
		//static refresh method, used when withdrawing/adding
		public static bool RefreshGump( Mobile player )
		{
			return RefreshGump( player, null );
		}
		
		public static bool RefreshGump( Mobile player, ListEntry listentry )
		{
			//if this mobile has a list entry gump up
			if( player.HasGump( typeof( ListEntryGump ) ) )
			{
				ListEntryGump gump = (ListEntryGump)player.FindGump( typeof( ListEntryGump ) );
				
				//if this gump that's up is showing this list entry, or if none was specified, then refresh
				if( listentry == null || gump.ListEntry == listentry )
				{
					//then, resend this gump!
					player.SendGump( new ListEntryGump( gump ) );
					return true;
				}
			}
			
			return false;
		}
		
		//gump refresh constructor
		public ListEntryGump( ListEntryGump oldgump ) : this( oldgump.Owner, oldgump.ListEntry, oldgump.Page )
		{
		}
		
		//default first page constructor
		public ListEntryGump( Mobile owner, ListEntry listentry ) : this( owner, listentry, 0 )
		{
		}
		
		//master constructor, with page number specified
		public ListEntryGump( Mobile owner, ListEntry listentry, int page ) : base( 50, 350 )
		{
			if( !( owner is PlayerMobile ) )
			{
				return;
			}
			
			_Owner = (PlayerMobile)owner;
			_ListEntry = listentry;
			
			//clear old gumps that are up
			_Owner.CloseGump( typeof( ListEntryGump ) );
			
			//set up the page
			AddPage(0);
            
			_Page = page;
			
			ApplyFilters();
			
			
			
			//determine page layout, sizes, and what gets displayed where
			DeterminePageLayout();

			//add the background			            
            AddBackground(0, 0, _Width, _Height, 5054);
            //AddImageTiled(11, 10, _Width - 23, _Height - 20, 2624);
            //AddAlphaRegion(11, 10, _Width - 22, _Height - 20);
            
            AddTitle();
            
            if( !AddListEntryListing() )
            {
	            //clear old gumps that are up
				_Owner.CloseGump( typeof( ListEntryGump ) );
	            return;
            }
            if( _MaxPages > 1 )
            {
	            AddPageButtons();
            }
            
            AddControlButtons();
		}
		
		protected void ApplyFilters()
		{
			_FilteredEntries = new List<ItemListEntry>();
			
			
			foreach( ItemListEntry entry in _ListEntry.ItemListEntries )
			{
				bool addentry = true;
				
				for( int i = 0; i < entry.Columns.Count; i++ )
				{
					if( _ListEntry.FilterText[i] != null && _ListEntry.FilterText[i] != "" )
					{
						ItemListEntryColumn column = entry.Columns[i];
						
						if(  column.Text == null || column.Text.ToLower().IndexOf( _ListEntry.FilterText[i].ToLower() ) == -1 )
						{
							addentry = false;
							break;
						}
					}
				}
				
				if( addentry )
				{
					_FilteredEntries.Add( entry );
				}
			}
		}
		
		//this calculates all stuff needed to display the gump properly
		protected void DeterminePageLayout()
		{
			//page size
			if( _FilteredEntries == null || _FilteredEntries.Count == 0 )
			{
				_Height = 200;
				
				if( _ListEntry == null || _ListEntry.ItemListEntries == null || _ListEntry.ItemListEntries.Count == 0 )
				{
					_Width = 400;
				}
				else
				{
					_Width = _ListEntry.ItemListEntries[0].GumpWidth;
				}
				
				_MaxPages = 1;
				_Page = Math.Min( _MaxPages - 1, _Page );
			}
			else
			{
				
				//minimum spacing 20, maximum entry display height
				_ListingHeight = Math.Max( 20, Math.Min( MaxEntryDisplayHeight, _FilteredEntries.Count * EntryLineSpacing ) );
				
				//determine how many entries can fit on a given page
				int entriesperpage = MaxEntryDisplayHeight / EntryLineSpacing;
				
				//calculate max # of pages
				_MaxPages = _FilteredEntries.Count / entriesperpage + 1;
				
				_Page = Math.Min( _MaxPages - 1, _Page );
				
				_ListingStart = _Page * entriesperpage;
				_ListingEnd = (_Page + 1 ) * entriesperpage;
				
				_Height = 200 + + ( _MaxPages > 1 ? 30 : 0 ) + _ListingHeight;
				_Width = _FilteredEntries[0].GumpWidth;
			}

			
			
			
			
			
			
		}
		
		//this adds the title stuff for the gump
		protected void AddTitle()
		{
			if( _ListEntry == null )
			{
				return;
			}
			
			AddLabel( 20, _Y, 88, _ListEntry.Name );
			AddLabel( 120, _Y, 88, "Contents: " + _ListEntry.Amount.ToString() + "/" + _ListEntry.MaxAmount.ToString() );
			AddLabel( 270, _Y, 88, "Displayed: " + _FilteredEntries.Count.ToString() );
            
			_Y += 25;
		}
		
		//this adds the listing of all item stores
		protected bool AddListEntryListing()
		{
			if( _ListEntry == null || _ListEntry.ItemListEntries.Count == 0 )
			{
				return true;
			}
			
			//write the header info in
			ItemListEntry entry = _ListEntry.ItemListEntries[0];
			
			for( int j = 0; j < entry.Columns.Count; j++ )
			{
				AddLabel( 40 + entry.Columns[j].X, _Y, ( _ListEntry.FilterText[j] == null || _ListEntry.FilterText[j] == "" ? 1153 : 78 ), entry.Columns[j].Header );
				AddSortFilterControls( 40 + entry.Columns[j].X, _Y + 20, j, _ListEntry.FilterText[j] );
			}
			
			_Y += 40;
			
			
			//list off the items that can be displayed
			for( int i = _ListingStart; i < _ListingEnd && i < _FilteredEntries.Count; i++ )
			{
				entry = _FilteredEntries[i];
				
				//add withdrawal button - put buttonid offset of 100 to allow for control/sort/filter button id's uninterrupted
				AddButton( 20, _Y + 3, 0x4B9, 0x4B9, 100 + i, GumpButtonType.Reply, 0 );
				
				//Add the details about this entry
				for( int j = 0; j < entry.Columns.Count; j++ )
				{
					if( entry.Columns[j].Width == 0 )
					{
						if( j < entry.Columns.Count - 1 )
						{
							entry.Columns[j].Width = entry.Columns[j + 1].X - entry.Columns[j].X - 10;
						}
						else
						{
							entry.Columns[j].Width = _Width - entry.Columns[j].X - 10;
						}
						
					}
					AddLabelCropped( 40 + entry.Columns[j].X, _Y, entry.Columns[j].Width, 20, ( entry.Hue > 1 ? entry.Hue : 1153 ), entry.Columns[j].Text );
				}
				
				_Y += EntryLineSpacing;
			}
			
			return true;
			
		}
		
		protected void AddPageButtons()
		{
			//page buttons
			_Y = _Height - 90;
			
			if ( _Page > 0 ) 
			{
				AddButton( 20, _Y, 0x15E3, 0x15E7, 4, GumpButtonType.Reply, 0 ); 
			}
			else 
			{
				AddImage( 20, _Y, 0x25EA ); 
			}
			AddLabel( 40, _Y, 88, "Previous Page" );
			
			
			if ( _Page < _MaxPages - 1 ) 
			{
				AddButton( _Width - 40, _Y, 0x15E1, 0x15E5, 5, GumpButtonType.Reply, 0 ); 
			}
			else 
			{
				AddImage( _Width - 40, _Y, 0x25E6 ); 
			}
			AddLabel( _Width - 120, _Y, 88, "Next Page" );
			
			AddLabel( _Width / 2 - 10, _Y, 88, String.Format( "({0}/{1})", _Page + 1, _MaxPages ) );
			
		}
		
		protected void AddControlButtons()
		{
			_Y = _Height - 60;
			
			AddLabel( _Width / 2 + 70 , _Y, 1153, "Add" );
			AddButton( _Width / 2 + 50, _Y + 5, 0x4B9, 0x4BA, 1, GumpButtonType.Reply, 0 );
			
			_Y += 30;
			AddLabel( _Width / 2 + 70 , _Y, 1153, "Fill from backpack" );
			AddButton( _Width / 2 + 50, _Y + 5, 0x4B9, 0x4BA, 2, GumpButtonType.Reply, 0 );
		}
		
		
		
		
		
		//gump utilities
		
		public void AddTextField( int x, int y, int width, int height, int index, string text )
		{
			AddImageTiled( x - 2, y - 2, width + 4, height + 4, 0xA2C );
			AddAlphaRegion( x -2, y - 2, width + 4, height + 4 );
			AddTextEntry( x + 2, y + 2, width - 4, height - 4, 1153, index, text );
		}
		
		public string GetTextField( RelayInfo info, int index )
		{
			TextRelay relay = info.GetTextEntry( index );
			return ( relay == null ? null : relay.Text.Trim() );
		}
		
		
		//this adds sort/filter components at the specified column location and specified column index
		public void AddSortFilterControls( int x, int y, int index, string filtertext )
		{
			//sort buttons
			AddButton( x, y, 0x15E0, 0x15E4, 10 + 10*index, GumpButtonType.Reply, 0 );  //Ascending
			AddButton( x + 15, y, 0x15E2, 0x15E6, 10 + 10*index + 1, GumpButtonType.Reply, 0 );  //Decending
			
			y = _Height - 90;
			
			if( _MaxPages > 1 )
			{
				y -= 30;
			}
			
			AddTextField( x, y, 50, 20, index, filtertext );
			AddButton( x + 55, y, 0x15E1, 0x15E5, 10 + 10*index + 2, GumpButtonType.Reply, 0 );
		}
		

		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			
			if( _ListEntry == null || !_ListEntry.CanUse( _Owner ) )
			{
				return;
			}
			
			//store flags
			int buttonid = info.ButtonID;
			
			//right click
			if( buttonid == 0 )
			{
				return;
			}
			
			//add button
			if( buttonid == 1 )
			{

				_ListEntry.AddItem( _Owner );
				
				//refresh the gump
				_Owner.SendGump( new ListEntryGump( this ) );
				return;
				
			}
			
			//fill from backpack button
			if( buttonid == 2 )
			{
				
				_ListEntry.FillFromBackpack( _Owner );
				
				_Owner.SendGump( new ListEntryGump( this ) );
				return;
			}
			
			//previous page button
			if( buttonid == 4 )
			{
				if( _Page > 0 )
				{
					_Owner.SendGump( new ListEntryGump( _Owner, _ListEntry, _Page - 1 ) );
				}
				return;
			}
			
			//next page button
			if( buttonid == 5 )
			{
				if( _Page < _MaxPages - 1 )
				{
					_Owner.SendGump( new ListEntryGump( _Owner, _ListEntry, _Page + 1 ) );
				}
				return;
			}
			
			//sort/filter buttons
			
			if( buttonid >= 10 && buttonid < 100 )
			{
				int columnnum = ( buttonid - 10 ) / 10;
				int buttontype = ( buttonid - 10 ) % 10;
				
				//if it's a sort button
				if( buttontype < 2 )
				{
					ItemListEntry.SortIndex = columnnum;
					ItemListEntry.SortOrder = ( buttontype == 0 ? -1 : 1 );
					
					_ListEntry.ItemListEntries.Sort();
				}
				else 
				{
					//apply filters
					for( int i = 0; i < 10; i++ )
					{
						_ListEntry.FilterText[i] = GetTextField( info, i );
					}
				}
				
				
				
				
				_Owner.SendGump( new ListEntryGump( this ) );
				return;
			}
			
			
			
			
			

			//any button that is left is a withdraw request
			//offset of 100 between the passed value and the list index
			
			buttonid -= 100;
			
			if( buttonid >= 0 && buttonid < _FilteredEntries.Count )
			{
				_ListEntry.WithdrawItem( _Owner, _FilteredEntries[ buttonid ] );
			}
			
			_Owner.SendGump( new ListEntryGump( this ) );
			

		}
		
		
		
	}


}