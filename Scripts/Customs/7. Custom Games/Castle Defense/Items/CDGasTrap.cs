using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public enum CDCDGasTrapType
	{
		NorthWall,
		WestWall,
		Floor
	}
	public class CDGasTrap : BaseTrap
	{
		private Poison m_Poison;
		[CommandProperty( AccessLevel.GameMaster )]
		public Poison Poison
		{
			get{ return m_Poison; }
			set{ m_Poison = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public CDCDGasTrapType Type
		{
			get
			{
				switch ( ItemID )
				{
					case 0x113C: return CDCDGasTrapType.NorthWall;
					case 0x1147: return CDCDGasTrapType.WestWall;
					case 0x11A8: return CDCDGasTrapType.Floor;
				}
				return CDCDGasTrapType.WestWall;
			}
			set
			{
				ItemID = GetBaseID( value );
			}
		}
		public static int GetBaseID( CDCDGasTrapType type )
		{
			switch ( type )
			{
				case CDCDGasTrapType.NorthWall: return 0x113C;
				case CDCDGasTrapType.WestWall: return 0x1147;
				case CDCDGasTrapType.Floor: return 0x11A8;
			}
			return 0;
		}
		[Constructable]
		public CDGasTrap() : this( CDCDGasTrapType.Floor )
		{
		}
		[Constructable]
		public CDGasTrap( CDCDGasTrapType type ) : this( type, Poison.Lesser )
		{
		}
		[Constructable]
		public CDGasTrap(  Poison poison ) : this( CDCDGasTrapType.Floor, Poison.Lesser )
		{
		}
		[Constructable]
		public CDGasTrap( CDCDGasTrapType type, Poison poison ) : base( GetBaseID( type ) )
		{
			m_Poison = poison;
		}
		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.FromSeconds( 1.0 ); } }
		public override int PassiveTriggerRange{ get{ return 1; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 0.5 ); } }
		public override void OnTrigger( Mobile from )
		{
			Effects.SendLocationEffect( Location, Map, GetBaseID( this.Type ) - 2, 16, 3, GetEffectHue(), 0 );
			Effects.PlaySound( Location, Map, 0x231 );

			from.ApplyPoison( from, m_Poison );

			from.LocalOverheadMessage( MessageType.Regular, 0x22, 500855 ); // You are enveloped by a noxious gas cloud!
		}
		public CDGasTrap( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			Poison.Serialize( m_Poison, writer );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Poison = Poison.Deserialize( reader );
					break;
				}
			}
		}
	}
}