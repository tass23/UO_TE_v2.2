using System; 
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Gumps;

namespace Server.Items 
{ 	
	public class BottleOfHolyWater : Item
	{
		private int m_Quantity;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Quantity
		{
			get { return m_Quantity; }
			set { m_Quantity = value; }
		}
		
		[Constructable]
		public BottleOfHolyWater() : base( 0xEFB )
		{
			Name = "a bottle of holy water";
			this.Weight = 0.2;
			m_Quantity = 10;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) from;
				if (m_Quantity > 0)
				{
					pm.PlaySound( Utility.RandomList( 0x2D6 ) );
					if(pm.Vampire > 0)
					{
						pm.SendMessage(0, "You feel strange...");
						pm.VampireBited = 0;
						pm.HueMod = -1;
						pm.BodyMod = 0;
						pm.Title = "";
						pm.FixedParticles( 0x375A, 10, 15, 5021, EffectLayer.Waist );
						pm.Vampire = 0;
						pm.CloseGump( typeof(VampireGump));
					}
					else
					{
						pm.SendMessage(0, "You feel a sense of peace...");
						//pm.IsInnocent = true;
						//pm.Kills = 0;
					}
					m_Quantity -=1;
					InvalidateProperties();
					if (m_Quantity == 0)
					{
						this.Delete();
					}
				}
				else
					from.SendMessage("The bottle is empty");
			}
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( GetQuantityDescription() );
		}
		
		public virtual int GetQuantityDescription()
		{
			int perc = ( m_Quantity * 100 ) / 10;

			if ( perc <= 0 )
				return 1042975; // It's empty.
			else if ( perc <= 33 )
				return 1042974; // It's nearly empty.
			else if ( perc <= 66 )
				return 1042973; // It's half full.
			else
				return 1042972; // It's full.
		}
		
		public BottleOfHolyWater( Serial serial ) : base( serial )
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