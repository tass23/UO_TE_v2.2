using System;
using Server;

namespace Server.Items
{
	public class KillingPoison : BasePoisonPotion
	{
		private int m_Quantity;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Quantity
		{
			get { return m_Quantity; }
			set { m_Quantity = value; }
		}
		
		public override Poison Poison{ get{ return Poison.Deadly; } }
		public override double MinPoisoningSkill{ get{ return 95.0; } }
		public override double MaxPoisoningSkill{ get{ return 100.0; } }

		[Constructable]
		public KillingPoison() : base( PotionEffect.PoisonDeadly )
		{
			Name = "a shot of sake";
			Hue = 1095;
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

		public KillingPoison( Serial serial ) : base( serial )
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