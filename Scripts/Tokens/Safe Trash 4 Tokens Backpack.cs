/// Trash 4 Tokens Backpack v0.1
///created by Daat99 26/03/2005
using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	public class Trash4TokensBackpack : Container
	{
		public override int MaxWeight{ get{ return 0; } } // A value of 0 signals unlimited weight
		public override int DefaultGumpID{ get{ return 0x3C; } }
		public override int DefaultDropSound{ get{ return 0x50; } }

		private DateTime m_LastTrash;
		public DateTime LastTrash{ get{ return m_LastTrash; } set{ m_LastTrash = value; } }

		private int m_TotalGold;
		private int m_TotalItems;
		private int m_TotalWeight;

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 18, 105, 144, 73 ); }
		}

		// public override bool CanStore( Mobile m )
		// {
			// return true; 
		// }

		[Constructable]
		public Trash4TokensBackpack() : base( 0x9b2 )
		{
			Name = "A Safe Trash 4 Tokens Backpack"; 
			Movable = true;
			Hue = 1173;
			LootType = LootType.Blessed;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{			
			if ( ( dropped is Daat99Tokens || dropped is TokenLedger || dropped is TokenCheck || dropped is LootTokenCheck )
			&& ( !LadyLuck.TrashTokensOK ) )
			{
				from.SendMessage( "It would not be wise to trash tokens for tokens!" );
				return false;
			}
			#region XmlQuest QuestBookBag
			if ( dropped is QuestBookBag )
			{
				from.SendMessage( "You cannot throw away your QuestBook Backpack and get tokens for it!" );
				return false;
			}
			#endregion
			if ( dropped is Gold  && !LadyLuck.TrashGoldOK ) 
			{
				from.SendMessage( "You would be wiser to Tithe your Gold, rather than trash it!" );
				return false;
			}
			if ( dropped is BankCheck && !LadyLuck.TrashBankCheckOK ) 
			{
				from.SendMessage( "You would be wiser to leave your bank checks in the bank, rather than trash them!" );
				return false;
			}
			if ( dropped.LootType == LootType.Blessed || dropped.Insured == true ) 
			{
				from.SendMessage( "The item you are trying to trash is protected by Lord Brit and cannot be simply trashed!" );
				return false;
			}
			int i = LadyLuck.SafeBagMinutes;
			List<Item> items = this.Items;
			if ( items.Count > 0 && m_LastTrash <= DateTime.Now )
			{
				Empty( from );
				from.SendMessage( "{0} minutes safety was over. Clearing trash before adding more.", i.ToString() );
			}
			if ( !base.OnDragDrop( from, dropped ) )
				return false;
			m_LastTrash = ( DateTime.Now + TimeSpan.FromMinutes( i ) );
			UpdateTotals();
			return true;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			if ( ( item is Daat99Tokens || item is TokenLedger || item is TokenCheck || item is LootTokenCheck )
			&& ( !LadyLuck.TrashTokensOK ) )
			{
				from.SendMessage( "It would not be wise to trash tokens for tokens!" );
				return false;
			}
			#region XmlQuest QuestBookBag
			if ( item is QuestBookBag )
			{
				from.SendMessage( "You cannot throw away your QuestBook Backpack and get tokens for it!" );
				return false;
			}
			#endregion
			if ( item is Gold   && !LadyLuck.TrashGoldOK ) 
			{
				from.SendMessage( "You would be wiser to Tithe your Gold, rather than trash it!" );
				return false;
			}
			if ( item is BankCheck && !LadyLuck.TrashBankCheckOK ) 
			{
				from.SendMessage( "You would be wiser to leave your bank checks in the bank, rather than trash them!" );
				return false;
			}
			int i = LadyLuck.SafeBagMinutes;
			List<Item> items = this.Items;
			if ( items.Count > 0 && m_LastTrash <= DateTime.Now )
			{
				Empty( from );
				from.SendMessage( "{0} minutes safety was over. Clearing trash before adding more.", i.ToString() );
			}
			if ( !base.OnDragDropInto( from, item, p ) )
				return false;
			m_LastTrash = (DateTime.Now + TimeSpan.FromMinutes( 3 ));
			UpdateTotals();
			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			int i = LadyLuck.SafeBagMinutes;
			List<Item> items = this.Items;
			if ( items.Count > 0 && m_LastTrash <= DateTime.Now)
			{
				Empty( from );
				from.SendMessage( "The {0} minutes safety was over. You can't recover the items.", i.ToString() );
			}
			base.OnDoubleClick( from );
		}
		public override void UpdateTotal( Item sender, TotalType type, int delta )
		{
			double d = (double)( (double)( LadyLuck.SafeBagWeightPercent ) / 100.0 ); 
			base.UpdateTotal( sender, type, delta );
			if ( type == TotalType.Weight )
			{
				if ( Parent is Item )
					( Parent as Item ).UpdateTotal( sender, type, (int)( delta * d ) * -1 );
				else if ( Parent is Mobile )
					( Parent as Mobile ).UpdateTotal( sender, type, (int)( delta * d ) * -1 );
			}
		}
		public override int GetTotal( TotalType type )
		{
			double d = (double)( (double)( LadyLuck.SafeBagWeightPercent ) / 100.0 ); 
			if ( type == TotalType.Weight )
				return (int)( base.GetTotal( type ) * ( 1.0 - d ) );
			return base.GetTotal( type );
		}

		public override void OnItemRemoved( Item item ) 
		{ 
			if ( m_LastTrash <= DateTime.Now )
			{
				item.Delete();
				Empty();
			}
			else 
				base.OnItemRemoved( item );
			UpdateTotals();
		} 

		public override void OnItemAdded( Item item )
		{
			base.OnItemAdded( item );
			UpdateTotals();
		}

		public void Empty()
		{
			int m = LadyLuck.SafeBagMinutes;
			List<Item> items = this.Items;
			if ( items.Count > 0 )
			{
				Mobile from = RootParent as Mobile;
				if (from != null)
				{
					Empty( from );
					from.SendMessage( "The {0} minutes safety was over. You can't recover the items.", m.ToString() );
				}
				else
				{
					for ( int i = items.Count - 1; i >= 0; --i )
					{
						if ( i >= items.Count )
							continue;
						((Item)items[i]).Delete();
					}
				}
			}
			UpdateTotals();
		}

		public void Empty( Mobile from )
		{
			EmptyTrash( from, this );
			UpdateTotals();
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			List<Item> items = this.Items;
			if ( items.Count > 0 )
				list.Add( new EmptyTrash4TokensBackpack( from, this ) );
		}

		public Trash4TokensBackpack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_LastTrash = DateTime.Now;
		}

		public static void EmptyTrash(Mobile from, Item item)
		{
			List<Item> items = item.Items;
			if ( items.Count > 0 )
			{
				int i_Reward = 0;
				from.PlaySound(0x76);
				for ( int i = items.Count - 1; i >= 0; --i )
				{
					if ( i >= items.Count )
						continue;
					Item it = (Item)items[i] as Item;
					if ( it.Stackable == false && !(item is BaseBook) )
						i_Reward += Utility.RandomMinMax(2,5);
					((Item)items[i]).Delete();
				}
				if (i_Reward > 0)
				{
					Item[] ledgers = from.Backpack.FindItemsByType( typeof( TokenLedger ) );

					foreach( TokenLedger tl in ledgers )
					{
						if ( tl.Owner == from.Serial )
						{
							if ((tl.Tokens + i_Reward) <= 2000000000 )
							{
								tl.Tokens = (tl.Tokens + i_Reward);
								from.SendMessage(1173, "You were rewarded {0} Tokens to your ledger for cleaning the shard.", i_Reward);
								break;
							}
							else 
								from.SendMessage(1173, "You have a full token ledger, please make a check and store it in your bank.");
						}
					}
				}
			}
		}

		public class EmptyTrash4TokensBackpack : ContextMenuEntry
		{
			private Mobile m_From;
			private Item m_Item;

			public EmptyTrash4TokensBackpack( Mobile from, Item item ) : base( 0154, 5 )
			{
				m_From = from;
				m_Item = item;
			}

			public override void OnClick()
			{
				EmptyTrash(m_From, m_Item);
			}
		}
	}
}