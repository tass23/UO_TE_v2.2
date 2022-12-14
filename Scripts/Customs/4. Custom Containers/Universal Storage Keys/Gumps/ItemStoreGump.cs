using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Solaris.ItemStore;

namespace Server.Gumps
{
	//item store gump interface for adding/removing items
	public class ItemStoreGump : Gump
	{
		PlayerMobile _Owner;
		ItemStore _Store;
		
		//used for seeking around on the page
		int _Y = 25;
		
		
		
		//used for formatting the gump columns
		static int _ItemColumn = 10;
		static int _WithdrawColumn = _ItemColumn + 40;
		static int _WithdrawDeedColumn = _WithdrawColumn + 20;
		static int _ItemNameColumn = _WithdrawDeedColumn + 20;
		static int _AmountColumn = _ItemNameColumn + 110;
		
		
		//page size
		protected int _Height;
		protected int _Width;
		
		//maximum entry listing height, for multi-page calculation
		public int MaxEntryDisplayHeight{ get{ return 400; } }
		public int ColumnWidth{ get{ return 240; } }
		
		//gump now supports multiple pages
		protected int _Page;
		
		//this is determined based on the number of entries and the maximum number to display per page
		protected int _MaxPages;
		
		//these are used to truncate the store entry listing to fit only a subset on the page
		protected int _ListingHeight;
		protected int _ListingStart;
				
		//public accessors for gump refreshing
		public Mobile Owner{ get{ return _Owner; } }
		public int Page{ get{ return _Page; } }
		//public accessor to the item store
		public ItemStore Store{ get{ return _Store; } }
		
		
		
		//static refresh method, used when withdrawing/adding
		public static bool RefreshGump( Mobile player )
		{
			return RefreshGump( player, null );
		}
		
		public static bool RefreshGump( Mobile player, ItemStore store )
		{
			//if this mobile has an item store gump up
			if( player.HasGump( typeof( ItemStoreGump ) ) )
			{
				ItemStoreGump gump = (ItemStoreGump)player.FindGump( typeof( ItemStoreGump ) );
				
				//if this gump that's up is showing this store, or if no store was specified
				if( store == null || gump.Store == store )
				{
					//then, resend this gump!
					player.SendGump( new ItemStoreGump( gump ) );
					return true;
				}
			}
			
			return false;
		}
		
		//gump refresh constructor
		public ItemStoreGump( ItemStoreGump oldgump ) : this( oldgump.Owner, oldgump.Store, oldgump.Page )
		{
		}
		
		//default first page constructor
		public ItemStoreGump( Mobile owner, ItemStore store ) : this( owner, store, 0 )
		{
		}
		
		//master constructor, with page number specified
		public ItemStoreGump( Mobile owner, ItemStore store, int page ) : base( 50, 50 )
		{
			if( !( owner is PlayerMobile ) )
			{
				return;
			}
			
			_Owner = (PlayerMobile)owner;
			_Store = store;
			
			//clear old gumps that are up
			_Owner.CloseGump( typeof( ItemStoreGump ) );
			
			//set up the page
			AddPage(0);
            
			_Page = page;
			
			//determine page layout, sizes, and what gets displayed where
			DeterminePageLayout();
			

			
			            
            AddBackground(0, 0, _Width, _Height, 5054);
            //AddImageTiled(11, 10, _Width - 23, _Height - 20, 2624); //old width -16
            //AddAlphaRegion(8, 10, _Width - 20, _Height - 20);
            //AddAlphaRegion(11, 10, _Width - 22, _Height - 20);
            
            AddTitle();
            
            if( !AddStoreListing() )
            {
	            //clear old gumps that are up
				_Owner.CloseGump( typeof( ItemStoreGump ) );
	            return;
            }
            if( _MaxPages > 1 )
            {
	            AddPageButtons();
            }
            
            AddControlButtons();
		}
		
		//this calculates all stuff needed to display the gump properly
		protected void DeterminePageLayout()
		{
			//page size
			if( _Store == null )
			{
				_Height = 100;
				_MaxPages = 1;
			}
			else
			{
				//flag this as unset
				_ListingStart = -1;
				
				//initial assumption - auto-fit everything based on the number of columns and maximum entry height
				_ListingHeight = Math.Min( _Store.EntryHeight / _Store.DisplayColumns, MaxEntryDisplayHeight );
				
				//this performs some checks on the listing, determining the max # of pages, the max column 
				///height, and the current start of the listing
				int seekpage = 0;
				
				int biggestcolumnheight = 0;
			
				int seeky = 0;
				int seekx = 0;
				int seekindex = 0;
			
				//seek thru the listings till you hit the requested page
				while( seekindex < _Store.StoreEntries.Count )
				{
					if( seekpage >= _Page && _ListingStart == -1 )
					{
						_ListingStart = seekindex;
					}
					
					seeky += _Store.StoreEntries[ seekindex ].Height;
				
					//if a new column is needed
					if( seeky >= _ListingHeight || _Store.StoreEntries[ seekindex ] is ColumnSeparationEntry )
					{
						//determine the biggest column height
						biggestcolumnheight = Math.Max( biggestcolumnheight, seeky );
						
						seeky = 0;
						seekx += 1;
						
						//if a new page is needed, and it's not the last entry in the list
						if( seekx >= _Store.DisplayColumns && seekindex < _Store.StoreEntries.Count - 1 )
						{
							
							
							seekx = 0;
							seekpage++;
						}
					}
					
					seekindex++;
				}

				_ListingStart = Math.Min( _ListingStart, _Store.StoreEntries.Count - 1 );
				
								
				_MaxPages = seekpage + 1;

				//this should hopefully never be needed
				if( _Page >= _MaxPages )
				{
					_Page = 0;
					_ListingStart = 0;
				}
				
				//correct the listing height in case it's smaller than needed
				_ListingHeight = Math.Min( _ListingHeight, biggestcolumnheight );

				_Height = 135 + ( ShowDeeds() ? 30 : 0 ) + ( _MaxPages > 1 ? 30 : 0 ) + _ListingHeight;
			}

			_Width = Math.Max( 30 + ColumnWidth * _Store.DisplayColumns, 400 );
		}
		
		//this adds the title stuff for the gump
		protected void AddTitle()
		{
			AddLabel( 20, _Y, 88, _Store.Label );
            
			_Y += 25;
		}
		
		//this adds the listing of all item stores
		protected bool AddStoreListing()
		{
			if( _Store == null || _Store.Count == 0 )
			{
				return true;
			}
			
			if( _Store.OfferDeeds && ShowDeeds() )
			{
				for( int i = 0; i < _Store.DisplayColumns; i++ )
				{
					AddItem( _WithdrawDeedColumn - 20 + i * ColumnWidth, _Y, 0x14F0, 71 );			//Commodity deed artwork
				}
				_Y += 30;
				
			}
			

			
			//save the current height to determine when to switch to a new column
			int tempy = _Y;
			
			//used for writing more than one column of contents
			int xoffset = 0;

			for( int i = _ListingStart; i < _Store.StoreEntries.Count; i++ )
			{
				StoreEntry entry  = _Store.StoreEntries[i];
				
				//check if we need a new line
				if( _Y - tempy >= _ListingHeight || entry is ColumnSeparationEntry )
				{
					xoffset += ColumnWidth;
					_Y = tempy;
					
					//if you've filled the page, then break
					if( xoffset >= _Store.DisplayColumns * ColumnWidth )
					{
						break;
					}
					
					//if this is a forced new line, don't process it any further.  go on to next entry
					if( entry is ColumnSeparationEntry )
					{
						continue;
					}
					
				}
				
				
				//draw the artwork
				Item item = entry.GetModel();
				
				//if there was a problem obtaining the graphic
				if( item == null )
				{
					_Owner.SendMessage( "Error: " + entry.ErrorMessage );
					return false;
				}
				
				//add a graphic of the item 
				AddItem( entry.X + xoffset + _ItemColumn, entry.Y + _Y, item.ItemID, item.Hue );
				
				//add the name.  Special treatment: if the entry is a ListEntry, then add a "..." to signify more info on inner gump
				AddLabel( xoffset + _ItemNameColumn, _Y, 0x486, entry.Name + ( ( entry is ListEntry ) ? "..." : "" ) );
				
				//add the amount
				AddLabel( xoffset + _AmountColumn, _Y, 0x480, entry.Amount.ToString() );
				
				//add the standard withdrawl button, indexed with i, and offset of 1 
				//the offset of 1 is used since response ID # 0 is used for right-clicking
				//on the gump to close it.  so all button values have to be above 0
				//to distinguish between a right-click and a button press
				
				//button graphic is changed by having an overridden buttonID stored in the storeentry type
				AddButton( xoffset + _WithdrawColumn + entry.ButtonX, _Y + entry.ButtonY, entry.ButtonID, entry.ButtonID + 1, i + 1, GumpButtonType.Reply, 0);
				//if the stuff can be put in a commodity deed..
				if( _Store.OfferDeeds && entry is ResourceEntry && ((ResourceEntry)entry).Deedable )
				{
					//then add a button to withdraw into commodity deed, indexed with i + _Store.StoreEntries.Count + 1
					//(where _Store.StoreEntries.Count is used in the response code to flag that the user is trying
					//to withdraw using a commodity deed) and offset of 1
					AddButton( xoffset + _WithdrawDeedColumn + entry.ButtonX, _Y + entry.ButtonY, 0x4B9, 0x4B9, _Store.StoreEntries.Count + i + 1, GumpButtonType.Reply, 0 );
				}
				
			    _Y += entry.Height;

			    //clean up the instanced item so it doesn't populate the shard with bogus items
				item.Delete();
				
			}
			
			
			
			return true;
			
		}
		
		protected void AddPageButtons()
		{
			//page buttons
			_Y = _Height - 90;
			
			if ( _Page > 0 ) 
			{
				AddButton( 20, _Y, 0x15E3, 0x15E7, _Store.StoreEntries.Count * 2 + 4, GumpButtonType.Reply, 0 ); 
			}
			else 
			{
				AddImage( 20, _Y, 0x25EA ); 
			}
			AddLabel( 40, _Y, 88, "Previous Page" );
			
			
			if ( _Page < _MaxPages - 1 ) 
			{
				AddButton( _Width - 40, _Y, 0x15E1, 0x15E5, _Store.StoreEntries.Count * 2 + 5, GumpButtonType.Reply, 0 ); 
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
			AddLabel( 15, _Y, 88, "Withdrawal Amount: " );
			
			if( _Store.LockWithdrawalAmount )
			{
				AddLabel( 150, _Y, 1153, _Store.WithdrawAmount.ToString() );
			}
			else
			{
				AddTextField( 150, _Y, 70, 20, 0, _Store.WithdrawAmount.ToString() );
			}
			
			//little toggle button, to lock/unlock withdrawal amount
			AddButton( 225, _Y + 3, 0x2C88 + ( _Store.LockWithdrawalAmount ? 10 : 0 ), 0x2C89 + ( _Store.LockWithdrawalAmount ? 10 : 0 ), _Store.StoreEntries.Count * 2 + 1, GumpButtonType.Reply, 0 );
			
			AddLabel( _Width / 2 + 70 , _Y, 1153, "Add" );
			AddButton( _Width / 2 + 50, _Y + 5, 0x4B9, 0x4BA, _Store.StoreEntries.Count * 2 + 2, GumpButtonType.Reply, 0 );
			
			_Y += 30;
			AddLabel( 15, _Y, 88, "Maximum Storage: " );
			AddLabel( 150, _Y, 1152, ItemStore.MaxAmount.ToString() );
			
			AddLabel( _Width / 2 + 70 , _Y, 1153, "Fill from backpack" );
			AddButton( _Width / 2 + 50, _Y + 5, 0x4B9, 0x4BA, _Store.StoreEntries.Count * 2 + 3, GumpButtonType.Reply, 0 );
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

		
		public bool ShowDeeds()
		{
			//check all entries for any that are deedable
			foreach( StoreEntry entry in _Store.StoreEntries )
			{
				if( entry is ResourceEntry && ((ResourceEntry)entry).Deedable )
				{
					return true;
				}
			}
			
			return false;
		}
		
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			try
			{
				//read in the text field and set the withdraw amount based on the contents
				//bounded by max storage, and 1
				if( !_Store.LockWithdrawalAmount )
				{
					_Store.WithdrawAmount = Math.Max( _Store.MinWithdrawAmount, Math.Min( ItemStore.MaxAmount, Int32.Parse( GetTextField( info, 0 ) ) ) );
				}
				
			}
			catch
			{
				_Owner.SendMessage( "Invalid entry in withdrawl amount: " + GetTextField( info, 0 ) );
			}
			
			if( !_Store.CanUse( _Owner ) )
			{
				return;
			}
			
			//store flags
			int buttonid = info.ButtonID;
			bool deed = false;
			
			//right click
			if( buttonid == 0 )
			{
				return;
			}
			
			//toggle lock button
			if( buttonid == _Store.StoreEntries.Count * 2 + 1 )
			{
				_Store.LockWithdrawalAmount = !_Store.LockWithdrawalAmount;
				_Owner.SendGump( new ItemStoreGump( this ) );
				return;
			}
			
			//add button
			if( buttonid == _Store.StoreEntries.Count * 2 + 2 )
			{
				_Store.AddItem( _Owner );
				
				_Owner.SendGump( new ItemStoreGump( this ) );
				return;
				
			}
			
			//fill from backpack button
			if( buttonid == _Store.StoreEntries.Count * 2 + 3 )
			{
				_Store.FillFromBackpack( _Owner );
				
				_Owner.SendGump( new ItemStoreGump( this ) );
				return;
			}
			
			//previous page button
			if( buttonid == _Store.StoreEntries.Count * 2 + 4 )
			{
				if( _Page > 0 )
				{
					_Owner.SendGump( new ItemStoreGump( _Owner, _Store, _Page - 1 ) );
				}
				return;
			}
			
			//next page button
			if( buttonid == _Store.StoreEntries.Count * 2 + 5 )
			{
				if( _Page < _MaxPages - 1 )
				{
					_Owner.SendGump( new ItemStoreGump( _Owner, _Store, _Page + 1 ) );
				}
				return;
			}

						
			//flag if a deed is requested
			if( buttonid > _Store.StoreEntries.Count )
			{
				deed = true;
				buttonid -= _Store.StoreEntries.Count;
			}
			
			//any button that is left is a withdraw request
			//offset of 1 between the passed value and the list index
			_Store.WithdrawItem( _Owner, buttonid - 1, deed );
			_Owner.SendGump( new ItemStoreGump( this ) );
		}
		
		
		
	}


}