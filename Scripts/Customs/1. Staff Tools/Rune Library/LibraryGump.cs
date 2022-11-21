/***************************************************************************
 *                               CREDITS
 *                         -------------------
 *                         : (C) 2004-2009 Luke Tomasello (AKA Adam Ant)
 *                         :   and the Angel Island Software Team
 *                         :   luke@tomasello.com
 *                         :   Official Documentation:
 *                         :   www.game-master.net, wiki.game-master.net
 *                         :   Official Source Code (SVN Repository):
 *                         :   http://game-master.net:8050/svn/angelisland
 *                         : 
 *                         : (C) May 1, 2002 The RunUO Software Team
 *                         :   info@runuo.com
 *
 *   Give credit where credit is due!
 *   Even though this is 'free software', you are encouraged to give
 *    credit to the individuals that spent many hundreds of hours
 *    developing this software.
 *   Many of the ideas you will find in this Angel Island version of 
 *   Ultima Online are unique and one-of-a-kind in the gaming industry! 
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

/* Scripts/Items/Books/LibraryGump.cs
 * ChangeLog
 *  01/02/08 - Pix
 *      Added security check to response of gump to check that the person
 *      removing the book is allowed to remove the book.
 *  3/25/07, Adam
 *      Comment out weaver's console messages :P
 *	03/13/07, weaver
 *		- Moved rename button from background so creation is first-page conditional
 *		- Changed rename graphic from large red diamond to smaller, blue symbol
 *  12/24/06, Adam
 *      Move rename button two pixels to the right .. matches rune books now
 *  12/24/06, Kit
 *      Changed owner bool from static, rewrote passing logic to handle change.
 *  12/23/06, Rhiannon
 *      Added button to allow owners and co-owners to rename a library.
 *  12/19/06, Adam
 *      Remove unused variable "private ArrayList m_Books" from "class LibraryGump : Gump"
 *  12/11/06, Kit
 *      Re-designed to use arraylist vs container system.
 *  11/23/06, Rhiannon
 *      Initial creation
 */

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Spells;
using Server.Spells.Fourth;
using Server.Spells.Seventh;
using Server.Prompts;

namespace Server.Gumps
{
    public class LibraryGump : Gump
    {
        private Library m_Library;
        private bool m_Owner;

        public bool IsOwner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        private void AddBackground()
        {
            AddPage( 0 );

            // Background image
            AddImage( 100, 10, 500 );
        }

        private void AddIndex( Mobile from )
        {
            int index = 0;
            int numItems = m_Library.Books.Count;
            int height = 15;
            int itemsPerPage = 20;

            // This is just in case we decide to display titles on two lines
            bool twoLines = true;
            if ( twoLines == true )
            {
                height = height * 2;
                itemsPerPage = itemsPerPage / 2;
            }

            int itemsPerHalf = itemsPerPage / 2;
            // The number of gump pages the total number of items divided by the number of items on the page
            // plus one page for the remainder.
            int totalPages = ( numItems / itemsPerPage ) + ( ( numItems % itemsPerPage == 0 ) ? 0 : 1 );

            for ( int page = 1 ; page <= totalPages ; page++ )
            {
                AddPage( page );

                for ( int i = 0 ; i <= itemsPerPage - 1 && index < m_Library.Books.Count ; i++, index++ )
                {
                    string desc = m_Library.GetBookTitle( (Item)m_Library.Books[index] );

                    int bID; // Button ID

                    // Calculate the button ID for each item on each page.
                    if ( i == 0 ) // First book
                        bID = ( ( page - 1 ) * itemsPerPage * 2 ) + 1;
                    else
                        bID = ( ( page - 1 ) * itemsPerPage * 2 ) + ( i * 2 ) + 1;

                    // Open book button
                    AddButton( 130 + ( ( i / itemsPerHalf ) * 192 ), 60 + ( ( i % itemsPerHalf ) * height ), 2361, 2361, bID, GumpButtonType.Reply, 0 );

                    bID++;

                    // If from is the house owner, add a "remove book" button and move everything over by the amount of deltaX.
                    int deltaX = 0;

                    if ( IsOwner )
                    {
                        AddButton( 145 + ( ( i / itemsPerHalf ) * 192 ), 60 + ( ( i % itemsPerHalf ) * height ), 2437, 2438, bID, GumpButtonType.Reply, 0 );
                        deltaX = 15;
                    }

                    // Description label
                    AddHtml( ( 145 + deltaX ) + ( ( i / itemsPerHalf ) * 192 ), 55 + ( ( i % itemsPerHalf ) * height ), 125 + deltaX, height + 7, desc, false, false );
                }

                // Turn page button
				if (page > 1) // On all but the first page, add a "back" button.
				{
					AddButton(100, 10, 501, 501, 0, GumpButtonType.Page, page - 1);
				}
				else
				{
					// wea: 13/mar/2007 Library bookcase rename bug 
					// (moved button from background to 1st page only)

					// Rename button (appears on *first* page)
					if ( IsOwner )
					{
						AddButton( 137, 30, 0x4B9, 0x4BA, 1001, GumpButtonType.Reply, 0 );
						AddHtml( 161, 27, 100, 18, "Rename library", false, false );
					}
				}

                if ( page < totalPages ) // On all but the last page, add a "forward" button.
                    AddButton( 456, 10, 502, 502, 0, GumpButtonType.Page, page + 1 );
            }
        }

        public LibraryGump( Mobile from, Library library, bool owner )
            : base( 150, 200 )
        {
            m_Library = library;
            m_Owner = owner;
            Closable = true;

            m_Library.Books.Sort( new Library.LibrarySort( m_Library ) );

            AddBackground();
            AddIndex( from );
        }

        private class InternalPrompt : Prompt
        {
            private Library m_Library;
            private bool m_Owner;

            public InternalPrompt( Library library, bool Owner )
            {
                m_Library = library;
                m_Owner = Owner;
            }

            public override void OnResponse( Mobile from, string text )
            {
                if ( m_Library.Deleted || !from.InRange( m_Library.GetWorldLocation( ), 2 ) )
                    return;

                if ( m_Library.CheckAccess( from ) )
                {
                    m_Library.Name = Utility.FixHtml( text.Trim( ) );

                    from.CloseGump( typeof( LibraryGump ) );
                    from.SendGump( new LibraryGump( from, m_Library, m_Owner) );

                    from.SendMessage( "The library's name has been changed." );
                }
                else
                {
                    from.SendMessage( "That cannot be done while the library is locked down." );
                }
            }

            public override void OnCancel( Mobile from )
            {
                from.SendLocalizedMessage( 502415 ); // Request cancelled.

                if ( !m_Library.Deleted && from.InRange( m_Library.GetWorldLocation( ), 1 ) )
                {
                    from.CloseGump( typeof( LibraryGump ) );
                    from.SendGump( new LibraryGump( from, m_Library, m_Owner ) );
                }
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            if (m_Library.Deleted || !from.InRange(m_Library.GetWorldLocation(), 2))
                return;

            int buttonID = info.ButtonID;
            //Console.WriteLine("buttonID = {0}", buttonID);

            int index;

            if (buttonID == 0)
            {
                //Nothing to do
            }
            else if (buttonID == 1001) // Rename library
            {
                if (m_Library.CheckAccess(from))
                {
                    from.SendMessage("Please enter a name for the library:");
                    from.Prompt = new InternalPrompt(m_Library, IsOwner);
                }
                else
                {
                    from.SendMessage("That cannot be done while the library is locked down.");
                }
            }
            else if (buttonID % 2 == 0) // If buttonID is an even number, it's a "remove book" button.
            {
                index = (buttonID / 2) - 1;

                if (index >= 0 && index < m_Library.Books.Count && m_Library.Books[index] != null) // Sanity check
                {
                    if (m_Library.CheckAccess(from))
                    {
                        m_Library.Remove(from, (Item)m_Library.Books[index]);
                    }
                    else
                    {
                        from.SendMessage("You do not have access to remove that.");
                    }
                }
                else
                {
                    from.SendMessage("That book no longer exists.");
                }
            }
            else // Otherwise, it's an "open book" button.
            {
                index = (buttonID / 2);

                if (index >= 0 && index < m_Library.Books.Count && m_Library.Books[index] != null) // Sanity check
                {
                    m_Library.Open(from, (Item)m_Library.Books[index]);
                }
                else
                {
                    from.SendMessage("That book no longer exists.");
                }
            }
        }
    }

}
