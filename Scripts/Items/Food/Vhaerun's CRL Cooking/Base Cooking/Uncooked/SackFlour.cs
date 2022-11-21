using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[TypeAlias( "Server.Items.SackFlourOpen" )]
	public class SackFlour : Item, IHasQuantity
	{
		private int m_Quantity;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Quantity
		{
			get{ return m_Quantity; }
			set
			{
				if ( value < 0 )
					value = 0;
				else if ( value > 20 )
					value = 20;

				m_Quantity = value;

				InvalidateProperties();

				if ( m_Quantity == 0 )
					Delete();
				else if ( m_Quantity < 20 && (ItemID == 0x1039 || ItemID == 0x1045) )
					++ItemID;

			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060584, m_Quantity.ToString() );
		}

		[Constructable]
		public SackFlour() : base( 0x1039 )
		{
			m_Quantity = 20;
		}

		public SackFlour( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( (int) m_Quantity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Quantity = reader.ReadInt();
					break;
				}
				case 0:
				{
					m_Quantity = 20;
					break;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if ( (ItemID == 0x1039 || ItemID == 0x1045) )
				++ItemID;
		}

	}
}