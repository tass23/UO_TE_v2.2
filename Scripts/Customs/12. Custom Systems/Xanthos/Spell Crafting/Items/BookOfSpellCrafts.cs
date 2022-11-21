#region AuthorHeader
//
//	SpellCrafting version 3.0, by Xanthos and TheOutkastDev
//
//  Based on original ideas and code by TheOutkastDev
//
#endregion AuthorHeader
using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.SpellCrafting.Gumps;
using Server.SpellCrafting.Items;
using Server.SpellCrafting;
using Server.Commands;

namespace Server.SpellCrafting.Items
{
	public class BookOfSpellCrafts : Item
	{
		public static void Initialize()
		{
			CommandHandlers.Register( "AllCrafts", AccessLevel.GameMaster, new CommandEventHandler( AllCrafts_OnCommand ) );
		}

		private static void AllCrafts_OnCommand( CommandEventArgs e )
		{
			e.Mobile.BeginTarget( -1, false, TargetFlags.None, new TargetCallback( AllCrafts_OnTarget ) );
			e.Mobile.SendMessage( "Select the book of spellcrafts to fill." );
		}

		private static void AllCrafts_OnTarget( Mobile from, object target )
		{
			if ( target is BookOfSpellCrafts )
			{
				BookOfSpellCrafts book = target as BookOfSpellCrafts;

				book.Content = ulong.MaxValue;
				book.InvalidateProperties();
				from.SendMessage( "Book filled." );
			}
			else
			{
				from.SendMessage( "That is not a Book of Spellcrafts." );
			}
		}

		private static void AddCraft_OnTarget( Mobile from, object target, object state )
		{
			int num = (int)state;

			if ( target is BookOfSpellCrafts )
			{
				BookOfSpellCrafts book = target as BookOfSpellCrafts;

				if ( book.HasCraft( num ) )
				{
					from.SendMessage( "The book already has this craft." );
				}
				else
				{
					book.Content |= (ulong)1 << num;
					book.InvalidateProperties();
					from.SendMessage( "The spellcraft has been added to the book." );
				}
			}
			else
			{
				from.SendMessage( "That is not a Book of Spellcrafts." );
			}
		}

		private ulong m_Content;
		private int   m_Charges;

		public int Count
		{
			get { return GetCraftCount(); }
		}

		public ulong Content
		{
			get { return m_Content; }
			set { m_Content = value; InvalidateProperties(); }
		}

		public int Charges
		{
			get { return m_Charges; }
			set { m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public BookOfSpellCrafts() : base( 0x2254 )
		{
			Name = "Book of SpellCrafts";
			Hue = 0x461;
		}

		public BookOfSpellCrafts( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1060662, "{0}\t{1} of {2}", "Crafts", Count, SpellCraftConfig.LastAosCraftID + 1 );

			if ( SpellCraftConfig.UseCharges )
				list.Add( 1060741, Charges.ToString() ); // charges: ~1_val~
		}


		public bool HasCraft( int CraftID )
		{
			return ( CraftID >= 0 && CraftID <= SpellCraftConfig.LastAosCraftID && (m_Content & ((ulong)1 << CraftID)) != 0 );
		}

		private int GetCraftCount()
		{
			int count = 0;

			for( int i = 0; i <= SpellCraftConfig.LastAosCraftID; ++i )
			{
				if ( HasCraft( i ) ) ++count;
			}

			return count;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.

			from.SendGump( new SpellCraftBook( from, this, 1 ) );
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.

			if ( dropped is BaseSpellCraft && dropped.Amount == 1 )
			{
				BaseSpellCraft craft = (BaseSpellCraft)dropped;

				if ( HasCraft( craft.CraftID ) )
				{
					from.SendMessage( "This spellcraft is already present in the book." );
					return false;
				}
				else
				{
					int val = craft.CraftID;

					if ( val >= 0 && val <= SpellCraftConfig.LastAosCraftID )
					{
						m_Content |= (ulong)1 << val;
						InvalidateProperties();
						craft.Delete();
						from.SendMessage( "The book accepts the craft." );
						return true;
					}
					return false;
				}
			}
			else if ( dropped is MagicJewel )
			{
				m_Charges += dropped.Amount;
				InvalidateProperties();
				dropped.Delete();
				from.SendMessage( "The book accepts the magic jewel{0}.", dropped.Amount == 1 ? "" : "s" );
				return true;
			}
			else if ( dropped is Container )
			{
				List<Item> items = new List<Item>( ((Container)dropped).Items );

				foreach ( Item item in items )
					OnDragDrop( from, item );
			}
			return false;
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch( version )
			{
					// version 0 reads second integer, version 1 no longer
					// needs it. No going to case 0 or serialization error
					// will occur!!
				case 2:
					m_Charges = reader.ReadInt();
					goto case 1;
				case 1:
					m_Content = reader.ReadULong();
					break;
				case 0:
					m_Content = reader.ReadULong();
					int m_Count = reader.ReadInt();
					break;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)2 ); // version
			writer.Write( m_Charges );
			writer.Write( m_Content );
		}
	}
}