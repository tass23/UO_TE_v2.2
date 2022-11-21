using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.LargeMobileBOD" )]
	public abstract class LargeMobileBOD : Item
	{
		private int m_AmountMax;
		private LargeMobileBulkEntry[] m_Entries;

		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountMax{ get{ return m_AmountMax; } set{ m_AmountMax = value; InvalidateProperties(); } }

		public LargeMobileBulkEntry[] Entries{ get{ return m_Entries; } set{ m_Entries = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Complete
		{
			get
			{
				for ( int i = 0; i < m_Entries.Length; ++i )
				{
					if ( m_Entries[i].Amount < m_AmountMax )
						return false;
				}

				return true;
			}
		}

		public abstract ArrayList ComputeRewards();
		public abstract int ComputeGold();
		public abstract int ComputeFame();

		public virtual void GetRewards( out Item reward, out int gold, out int fame )
		{
			reward = null;
			gold = ComputeGold();
			fame = ComputeFame();

			ArrayList rewards = ComputeRewards();

			if ( rewards.Count > 0 )
			{
				reward = (Item)rewards[Utility.Random( rewards.Count )];

				for ( int i = 0; i < rewards.Count; ++i )
				{
					if ( rewards[i] != reward )
						((Item)rewards[i]).Delete();
				}
			}
		}

		public override int LabelNumber{ get{ return 1045151; } } // a bulk order deed

		public LargeMobileBOD( int hue, int amountMax, LargeMobileBulkEntry[] entries ) : base( Core.AOS ? 0x2258 : 0x14EF )
		{
			Weight = 1.0;
			Hue = hue; // Blacksmith: 0x44E; Tailoring: 0x483
			LootType = LootType.Blessed;

			m_AmountMax = amountMax;
			m_Entries = entries;
		}

		public LargeMobileBOD() : base( Core.AOS ? 0x2258 : 0x14EF )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060655 ); // large bulk order
			
			//string subString = "amount to tame";

			list.Add( "amount to tame: {0}", m_AmountMax.ToString() );

			for ( int i = 0; i < m_Entries.Length; ++i )
			{
				string s = m_Entries[i].Details.AnimalName;

				int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

				if( capsbreak > -1 )
				{
					string secondhalf = s.Substring( capsbreak );
 					string firsthalf = s.Substring(0, capsbreak );

					list.Add( 1060658 + i, "{0} {1}\t{2}", firsthalf, secondhalf, m_Entries[i].Amount ); // ~1_val~: ~2_val~
				}
				else
				{
					list.Add( 1060658 + i, "{0}\t{1}", m_Entries[i].Details.AnimalName, m_Entries[i].Amount ); // ~1_val~: ~2_val~
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				from.SendGump( new LargeMobileBODGump( from, this ) );
			else
				from.SendLocalizedMessage( 1045156 ); // You must have the deed in your backpack to use it.
		}

		public void BeginMobileCombine( Mobile from )
		{
			if ( !Complete )
				from.Target = new LargeMobileBODTarget( this );
			else
				from.SendLocalizedMessage( 1045166 ); // The maximum amount of requested items have already been combined to this deed.
		}

		public void EndMobileCombine( Mobile from, object o )
		{
			if ( o is Item && ((Item)o).IsChildOf( from.Backpack ) )
			{
				if ( o is SmallMobileBOD )
				{
					SmallMobileBOD small = (SmallMobileBOD)o;

					LargeMobileBulkEntry entry = null;

					for ( int i = 0; entry == null && i < m_Entries.Length; ++i )
					{
						if ( m_Entries[i].Details.Type == small.Type )
							entry = m_Entries[i];
					}

					if ( entry == null )
					{
						from.SendLocalizedMessage( 1045160 ); // That is not a bulk order for this large request.
					}
					else if ( m_AmountMax != small.AmountMax )
					{
						from.SendLocalizedMessage( 1045163 ); // The two orders have different requested amounts and cannot be combined.
					}
					else if ( small.AmountCur < small.AmountMax )
					{
						from.SendLocalizedMessage( 1045164 ); // The order to combine with is not completed.
					}
					else if ( entry.Amount >= m_AmountMax )
					{
						from.SendLocalizedMessage( 1045166 ); // The maximum amount of requested items have already been combined to this deed.
					}
					else
					{
						entry.Amount += small.AmountCur;
						small.Delete();

						from.SendLocalizedMessage( 1045165 ); // The orders have been combined.

						from.SendGump( new LargeMobileBODGump( from, this ) );

						if ( !Complete )
							BeginMobileCombine( from );
					}
				}
				else
				{
					from.SendLocalizedMessage( 1045159 ); // That is not a bulk order.
				}
			}
			else
			{
				from.SendLocalizedMessage( 1045158 ); // You must have the item in your backpack to target it.
			}
		}

		public LargeMobileBOD( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_AmountMax );

			writer.Write( (int) m_Entries.Length );

			for ( int i = 0; i < m_Entries.Length; ++i )
				m_Entries[i].Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_AmountMax = reader.ReadInt();

					m_Entries = new LargeMobileBulkEntry[reader.ReadInt()];

					for ( int i = 0; i < m_Entries.Length; ++i )
						m_Entries[i] = new LargeMobileBulkEntry( this, reader );

					break;
				}
			}

			if ( Weight == 0.0 )
				Weight = 1.0;

			if ( Core.AOS && ItemID == 0x14EF )
				ItemID = 0x2258;

			if ( Parent == null && Map == Map.Internal && Location == Point3D.Zero )
				Delete();
		}
	}
}