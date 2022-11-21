/*
 *	This program is the CONFIDENTIAL and PROPRIETARY property
 *	of Tomasello Software LLC. Any unauthorized use, reproduction or
 *	transfer of this computer program is strictly prohibited.
 *
 *      Copyright (c) 2004 Tomasello Software LLC.
 *	This is an unpublished work, and is subject to limited distribution and
 *	restricted disclosure only. ALL RIGHTS RESERVED.
 *
 *			RESTRICTED RIGHTS LEGEND
 *	Use, duplication, or disclosure by the Government is subject to
 *	restrictions set forth in subparagraph (c)(1)(ii) of the Rights in
 * 	Technical Data and Computer Software clause at DFARS 252.227-7013.
 *
 *	Angel Island UO Shard	Version 1.0
 *			Release A
 *			March 25, 2004
 */

/* Items/Library.cs
 * CHANGELOG:
 *  01/02/08 - Pix
 *      Cleaned up CheckAccess() function.
 *  01/07/07, Kit
 *      moved lockdown logic into deserialization due to bug with deserialization order.
 *  01/07/07, Kit
 *      Make books set islockedown flag after server world load!! Uses ManageLockDowns which is called in basehouse.
 *  12/29/06, Kit
 *      Made books entered into library count as locked down so runebook runes cant be removed/etc by non owners.
 *  12/24/06, Kit
 *      Reverted Serialization/Deserialization to version 0, removed serialized drop sound,
 *      made opening another book or the same book close the current book gump.
 *      Added additional sanity checking
 *  12/23/06, Rhiannon
 *      Added CheckAccess() to figure out if the person opening the library has the 
 *      access to a locked down library.
 *  12/22/06, Adam
 *      rename to "library bookcase"
 *  12/22/06, Rhiannon
 *      Changed to close any previously opened LibraryGump when opening.
 *  12/21/06, Adam
 *      Switch to FakeContainer as the base class to handle bogus OnDoubleClick messages.
 *  12/12/06, Rhiannon
 *      Added name property.
 *      Added dropped sound to OnDragDrop().
 *      Changed and combined book rejection messages.
 *  12/11/06, Kit
 *      Re-designed to use internal arraylist vs container inheritance.
 *  11/23/06, Rhiannon
 *      Initial creation
 */

using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Multis;
//using Server.Scripts.Commands;

namespace Server.Items
{
    [Flipable( 0xA97, 0xA99 )]
    public class Library : FakeContainer
    {
        private static int Max_Books = 20; //only allow 100 books per library
        private static int m_DropSound = 0x42;
        private ArrayList m_Books;

        public int DropSound
        {
            get { return m_DropSound; }
        }

        public ArrayList Books
        {
            get { return m_Books; }
            set { m_Books = value; }
        }

        [Constructable]
        public Library()
            : base( 0xA97 )
        {
            m_Books = new ArrayList( );
            Weight = 12.0;
            Name = "library bookcase";
        }

        public Library( Serial serial )
            : base( serial )
        {
        }

        public void ManageLockDowns()
        {
            try
            {
                if(m_Books == null)
                {
                    m_Books = new ArrayList();
                    return;
                }

                foreach(Item x in m_Books)
                {
                    if(x != null && !x.IsLockedDown)
                        x.IsLockedDown = true;
                }
            }
            catch(Exception ex)
            {
             //   LogHelper.LogException(ex);
            }
        }
       
        public override bool OnDragDrop( Mobile from, Item dropped )
        {
            if ( !( dropped is BaseBook ) && !( dropped is Runebook )
                || ( dropped is BaseBook && ( (BaseBook)dropped ).Writable ) )
            {
                from.SendMessage( "You may only add runebooks and sealed books to a library." );
                return false;
            }

            if ( !CheckAccess( from ) )
            {
                from.SendMessage( "Only owners and co-owners can add books to a locked-down library." );
                return false;
            }

            BaseHouse house = BaseHouse.FindHouseAt( from );
           /* int lockdowns = 0;
            if ( house != null )
                lockdowns = house.LockDownCount;
            int maxbooks = lockdowns + 1;

            if ( house != null && maxbooks > house.MaxLockDowns )
            {
                from.SendMessage( "This would exceed the houses lockdown limit." );
                return false;
            }*/

            if ( this.TotalItems > Max_Books )
            {
                from.SendMessage( "The library is full." );
                return false;
            }
            //add the book everything checks out
            dropped.IsLockedDown = true;
            AddItem( dropped );
            m_Books.Add( dropped );

            from.SendSound( DropSound, GetWorldLocation( ) );

            return true;
        }

        public bool CheckAccess( Mobile from )
        {
            if (from == null)
            {
                return false;
            }

            if (!IsLockedDown || from.AccessLevel >= AccessLevel.GameMaster)
            {
                return true;
            }

            BaseHouse house = BaseHouse.FindHouseAt( this );

            if (house != null 
                && house.IsCoOwner(from)
                && house.Contains(this))
            {
                return true;
            }

            return false;
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( ReadyState( ) && from.InRange( GetWorldLocation( ), 2 ) )
            {
                BaseHouse house = BaseHouse.FindHouseAt( this );
                bool owner = false;
                if ( from.AccessLevel >= AccessLevel.GameMaster || ( house != null && ( house.IsOwner( from )
                    || house.IsCoOwner( from ) ) && house.Contains( this ) )
                    || ( ( this.RootParent is Mobile) && ( (Mobile)this.RootParent == from ) ) )
                    owner = true;

                from.CloseGump( typeof( LibraryGump ) );
                from.SendGump( new LibraryGump( from, this, owner ) );
            }
        }

        public string GetBookTitle( Item item )
        {
            if ( item is BaseBook )
                return ( (BaseBook)item ).Title;
            else if ( item is Runebook )
            {
                if ( ( (Runebook)item ).Description != null )
                    return ( (Runebook)item ).Description;

                return "a runebook";
            }
            else
                return "Error!";
        }

        public void Remove( Mobile from, Item item )
        {
            if(item == null) //sanity check 
                return;
            if ( ReadyState( ) && from.InRange( GetWorldLocation( ), 2 ) )
            {
				BaseHouse house = BaseHouse.FindHouseAt( this );
				bool owner = false;
				if ( from.AccessLevel >= AccessLevel.GameMaster || ( house != null && ( house.IsOwner( from ) || house.IsCoOwner( from ) ) && house.Contains( this ) ) )
						owner = true;
				item.IsLockedDown = false;
				RemoveItem( item );
				Books.Remove( item );
				from.AddToBackpack( item );

				from.SendMessage( "You have removed the book." );
			}
        }

        public void Open( Mobile from, Item item )
        {
            if(item is BaseBook)
            {
                from.CloseGump(typeof(BookGump));
                from.SendGump(new BookGump(from, (BaseBook)item));
            }
            else // item is Runebook
                item.OnDoubleClick(from);
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );    // version
            writer.WriteItemList( m_Books, true );
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            switch(version)
            {
                case 0:
                {
                    m_Books = reader.ReadItemList();
                    ManageLockDowns();
                    break;
                }
                    
            }
        }
        
        public class LibrarySort : IComparer
        {
            private Library m_Library;

            public Library Library
            {
                get { return m_Library; }
                set { m_Library = value; }
            }

            public LibrarySort( Library library )
            {
                m_Library = library;
            }

            int IComparer.Compare( object x, object y )
            {
                Item itemX = (Item)x;
                Item itemY = (Item)y;

                string descX = Library.GetBookTitle( itemX );
                string descY = Library.GetBookTitle( itemY );

                if ( itemX == null && itemY == null )
                    return 0;
                else if ( itemX == null && itemY != null )
                    return -1;
                else if ( itemX != null && itemY == null )
                    return 1;
                else
                {
                    return descX.CompareTo( descY );
                }
            }
        }
    }
}
