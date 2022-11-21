using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public enum CDCDSawTrapType
	{
		WestWall,
		NorthWall,
		WestFloor,
		NorthFloor
	}
	public class CDSawTrap : BaseTrap
	{
		[CommandProperty( AccessLevel.GameMaster )]
		public CDCDSawTrapType Type
		{
			get
			{
				switch ( ItemID )
				{
					case 0x1103: return CDCDSawTrapType.NorthWall;
					case 0x1116: return CDCDSawTrapType.WestWall;
					case 0x11AC: return CDCDSawTrapType.NorthFloor;
					case 0x11B1: return CDCDSawTrapType.WestFloor;
				}

				return CDCDSawTrapType.NorthWall;
			}
			set
			{
				ItemID = GetBaseID( value );
			}
		}
		public static int GetBaseID( CDCDSawTrapType type )
		{
			switch ( type )
			{
				case CDCDSawTrapType.NorthWall: return 0x1103;
				case CDCDSawTrapType.WestWall: return 0x1116;
				case CDCDSawTrapType.NorthFloor: return 0x11AC;
				case CDCDSawTrapType.WestFloor: return 0x11B1;
			}
			return 0;
		}
		[Constructable]
		public CDSawTrap() : this( CDCDSawTrapType.NorthFloor )
		{
		}
		[Constructable]
		public CDSawTrap( CDCDSawTrapType type ) : base( GetBaseID( type ) )
		{
		}
		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.FromSeconds( 1.0 ); } }
		public override int PassiveTriggerRange{ get{ return 1; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 0.5 ); } }
		public override void OnTrigger( Mobile from )
		{
			Effects.SendLocationEffect( Location, Map, GetBaseID( this.Type ) + 1, 6, 3, GetEffectHue(), 0 );
			Effects.PlaySound( Location, Map, 0x21C );

			Spells.SpellHelper.Damage( TimeSpan.FromTicks( 1 ), from, from, Utility.RandomMinMax( 5, 15 ) );

			from.LocalOverheadMessage( MessageType.Regular, 0x22, 500853 ); // You stepped onto a blade trap!
		}
		public CDSawTrap( Serial serial ) : base( serial )
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
		}
	}
}