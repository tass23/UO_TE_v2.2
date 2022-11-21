using System;
using Server;

namespace Server.Items
{

	public class JonesyAntidote : BaseCurePotion
	{
		private int m_Quantity;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Quantity
		{
			get { return m_Quantity; }
			set { m_Quantity = value; }
		}
		
		private static CureLevelInfo[] m_OldLevelInfo = new CureLevelInfo[]
			{
				new CureLevelInfo( Poison.Lesser,  1.00 ), // 100% chance to cure lesser poison
				new CureLevelInfo( Poison.Regular, 1.00 ), // 100% chance to cure regular poison
				new CureLevelInfo( Poison.Greater, 1.00 ), // 100% chance to cure greater poison
				new CureLevelInfo( Poison.Deadly,  1.00 ), // 100% chance to cure deadly poison
				new CureLevelInfo( Poison.Lethal,  1.00 )  // 100% chance to cure lethal poison
			};

		private static CureLevelInfo[] m_AosLevelInfo = new CureLevelInfo[]
			{
				new CureLevelInfo( Poison.Lesser,  1.00 ),
				new CureLevelInfo( Poison.Regular, 1.00 ),
				new CureLevelInfo( Poison.Greater, 1.00 ),
				new CureLevelInfo( Poison.Deadly,  1.00 ),
				new CureLevelInfo( Poison.Lethal,  1.00 )
			};

		public override CureLevelInfo[] LevelInfo{ get{ return Core.AOS ? m_AosLevelInfo : m_OldLevelInfo; } }

		[Constructable]
		public JonesyAntidote() : base( PotionEffect.CureGreater )
		{
			Name = "Jonesy's Antidote";
			this.Weight = 0.2;
			m_Quantity = 1;
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( GetQuantityDescription() );
		}
		
		public virtual int GetQuantityDescription()
		{
			int perc = ( m_Quantity * 100 ) / 100;

			if ( perc <= 0 )
				return 1042975; // It's empty.
			else
				return 1042972; // It's full.
		}

		public JonesyAntidote( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_Quantity);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Quantity = reader.ReadInt();
		}
	}
}