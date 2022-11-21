using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.SmallMobileBOD" )]
	public abstract class SmallMobileBOD : Item
	{
		private int m_AmountCur, m_AmountMax;
		private Type m_Type;
		private string m_AnimalName;
		private int m_Graphic;

		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountCur{ get{ return m_AmountCur; } set{ m_AmountCur = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountMax{ get{ return m_AmountMax; } set{ m_AmountMax = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Type Type
		{ 
			get{ return m_Type; } 
			set
			{ 
				m_Type = value; 
			} 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string AnimalName{ get{ return m_AnimalName; } set{ m_AnimalName = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Graphic{ get{ return m_Graphic; } set{ m_Graphic = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Complete{ get{ return ( m_AmountCur == m_AmountMax ); } }

		public override int LabelNumber{ get{ return 1045151; } } // a bulk order deed

		[Constructable]
		public SmallMobileBOD( int hue, int amountMax, Type type, string animalname, int graphic ) : base( Core.AOS ? 0x2258 : 0x14EF )
		{
			Weight = 1.0;
			Hue = hue; // Blacksmith: 0x44E; Tailoring: 0x483
			LootType = LootType.Blessed;

			m_AmountMax = amountMax;
			m_Type = type;
			m_AnimalName = animalname;
			m_Graphic = graphic;
		}

		public SmallMobileBOD() : base( Core.AOS ? 0x2258 : 0x14EF )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060654 ); // small bulk order

			list.Add( 1060658, "amount to tame\t{0}", m_AmountMax.ToString() );

			string s = m_AnimalName;

			int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

			if( capsbreak > -1 )
			{
				string secondhalf = s.Substring( capsbreak );
 				string firsthalf = s.Substring(0, capsbreak );

				list.Add( 1060660, "{0} {1}\t{2}", firsthalf, secondhalf, m_AmountCur ); // ~1_val~: ~2_val~
			}
			else
			{
				list.Add( 1060663, "{0}\t{1}", m_AnimalName, m_AmountCur ); // ~1_val~: ~2_val~
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				from.SendGump( new SmallMobileBODGump( from, this ) );
			else
				from.SendLocalizedMessage( 1045156 ); // You must have the deed in your backpack to use it.
		}

		public void BeginMobileCombine( Mobile from )
		{
			if ( m_AmountCur < m_AmountMax )
				from.Target = new SmallMobileBODTarget( this );
			else
				from.SendLocalizedMessage( 1045166 ); // The maximum amount of requested items have already been combined to this deed.
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

		public void EndMobileCombine( Mobile from, object o )
		{
			Type objectType = o.GetType();

			if ( m_AmountCur >= m_AmountMax )
			{
				from.SendMessage( "The maximum amount of requested creatures have already been combined to this deed." ); // The maximum amount of requested items have already been combined to this deed.
			}
			else if ( m_Type == null || (objectType != m_Type && !objectType.IsSubclassOf( m_Type )) || (!(o is BaseCreature)) )
			{
				from.SendMessage( "The creature is not in the request." ); // The item is not in the request.
			}
			else
			{
				((BaseCreature)o).Delete();
				++AmountCur;

				from.SendMessage( "The creature has been combined with the deed." ); // The item has been combined with the deed.

				from.SendGump( new SmallMobileBODGump( from, this ) );

				if ( m_AmountCur < m_AmountMax )
					BeginMobileCombine( from );
			}
		}

		public SmallMobileBOD( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_AmountCur );
			writer.Write( m_AmountMax );
			writer.Write( m_Type == null ? null : m_Type.FullName );
			writer.Write( m_AnimalName );
			writer.Write( m_Graphic );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_AmountCur = reader.ReadInt();
					m_AmountMax = reader.ReadInt();

					string type = reader.ReadString();

					if ( type != null )
						m_Type = ScriptCompiler.FindTypeByFullName( type );

					m_AnimalName = reader.ReadString();
					m_Graphic = reader.ReadInt();

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