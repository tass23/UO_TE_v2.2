using System;

namespace Server.Items
{
	public enum DoorFacing
	{
		WestCW,
		EastCCW,
		WestCCW,
		EastCW,
		SouthCW,
		NorthCCW,
		SouthCCW,
		NorthCW,
		//Sliding Doors
		SouthSW,
		SouthSE,
		WestSS,
		WestSN
	}

	public class IronGateShort : BaseDoor
	{
		[Constructable]
		public IronGateShort( DoorFacing facing ) : base( 0x84c + (2 * (int)facing), 0x84d + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public IronGateShort( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class IronGate : BaseDoor
	{
		[Constructable]
		public IronGate( DoorFacing facing ) : base( 0x824 + (2 * (int)facing), 0x825 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public IronGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LightWoodGate : BaseDoor
	{
		[Constructable]
		public LightWoodGate( DoorFacing facing ) : base( 0x839 + (2 * (int)facing), 0x83A + (2 * (int)facing), 0xEB, 0xF2, BaseDoor.GetOffset( facing ) )
		{
		}

		public LightWoodGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DarkWoodGate : BaseDoor
	{
		[Constructable]
		public DarkWoodGate( DoorFacing facing ) : base( 0x866 + (2 * (int)facing), 0x867 + (2 * (int)facing), 0xEB, 0xF2, BaseDoor.GetOffset( facing ) )
		{
		}

		public DarkWoodGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MetalDoor : BaseDoor
	{
		[Constructable]
		public MetalDoor( DoorFacing facing ) : base( 0x675 + (2 * (int)facing), 0x676 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public MetalDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class BarredMetalDoor : BaseDoor
	{
		[Constructable]
		public BarredMetalDoor( DoorFacing facing ) : base( 0x685 + (2 * (int)facing), 0x686 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public BarredMetalDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class BarredMetalDoor2 : BaseDoor
	{
		[Constructable]
		public BarredMetalDoor2( DoorFacing facing ) : base( 0x1FED + (2 * (int)facing), 0x1FEE + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public BarredMetalDoor2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class RattanDoor : BaseDoor
	{
		[Constructable]
		public RattanDoor( DoorFacing facing ) : base( 0x695 + (2 * (int)facing), 0x696 + (2 * (int)facing), 0xEB, 0xF2, BaseDoor.GetOffset( facing ) )
		{
		}

		public RattanDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DarkWoodDoor : BaseDoor
	{
		[Constructable]
		public DarkWoodDoor( DoorFacing facing ) : base( 0x6A5 + (2 * (int)facing), 0x6A6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
		}

		public DarkWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MediumWoodDoor : BaseDoor
	{
		[Constructable]
		public MediumWoodDoor( DoorFacing facing ) : base( 0x6B5 + (2 * (int)facing), 0x6B6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
		}

		public MediumWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MetalDoor2 : BaseDoor
	{
		[Constructable]
		public MetalDoor2( DoorFacing facing ) : base( 0x6C5 + (2 * (int)facing), 0x6C6 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public MetalDoor2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LightWoodDoor : BaseDoor
	{
		[Constructable]
		public LightWoodDoor( DoorFacing facing ) : base( 0x6D5 + (2 * (int)facing), 0x6D6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
		}

		public LightWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class StrongWoodDoor : BaseDoor
	{
		[Constructable]
		public StrongWoodDoor( DoorFacing facing ) : base( 0x6E5 + (2 * (int)facing), 0x6E6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
		}

		public StrongWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	/*Added by Raist*/
	public class TreeDoor : BaseDoor
	{
		[Constructable]
		public TreeDoor( DoorFacing facing ) : base( 0x2BE9 + (2 * (int)facing), 0x2BEA + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
			Name = "Ornate Tree Door";
		}

		public TreeDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class CarvedWoodDoor : BaseDoor
	{
		[Constructable]
		public CarvedWoodDoor( DoorFacing facing ) : base( 0x2BF9 + (2 * (int)facing), 0x2BFA + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
			Name = "Carved Wood Door";
		}

		public CarvedWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class ForgedSteelDoor : BaseDoor
	{
		[Constructable]
		public ForgedSteelDoor( DoorFacing facing ) : base( 0x2C09 + (2 * (int)facing), 0x2C0A + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
			Name = "Forged Steel Door";
		}

		public ForgedSteelDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class RedOrnateWoodDoor : BaseDoor
	{
		[Constructable]
		public RedOrnateWoodDoor( DoorFacing facing ) : base( 0x2C7E + (2 * (int)facing), 0x2C7F + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
			Name = "Ornate Door with Gold Inlay";
		}

		public RedOrnateWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class HippieWoodDoor : BaseDoor
	{
		[Constructable]
		public HippieWoodDoor( DoorFacing facing ) : base( 0x2C8E + (2 * (int)facing), 0x2C8F + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
			Name = "Wooden Hippie Door";
		}

		public HippieWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class CelestialSteelDoor : BaseDoor
	{
		[Constructable]
		public CelestialSteelDoor( DoorFacing facing ) : base( 0x2C9E + (2 * (int)facing), 0x2C9F + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
			Name = "Celestial Steel Door";
		}

		public CelestialSteelDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class WhiteDragonWoodDoor : BaseDoor
	{
		[Constructable]
		public WhiteDragonWoodDoor( DoorFacing facing ) : base( 0x2CAE + (2 * (int)facing), 0x2CAF + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
			Name = "White Dragon Mock Ivory Door";
		}

		public WhiteDragonWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class GoodLuckWoodDoor : BaseDoor
	{
		[Constructable]
		public GoodLuckWoodDoor( DoorFacing facing ) : base( 0x2CBE + (2 * (int)facing), 0x2CBF + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
			Name = "Oak Good Luck Door with Pearl Inlay";
		}

		public GoodLuckWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class PaintedRatanDoor : BaseDoor
	{
		[Constructable]
		public PaintedRatanDoor( DoorFacing facing ) : base( 0x2CCE + (2 * (int)facing), 0x2CCF + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
			Name = "Hand-Painted Palm Tree Ratan Door";
		}

		public PaintedRatanDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class TigerRatanDoor : BaseDoor
	{
		[Constructable]
		public TigerRatanDoor( DoorFacing facing ) : base( 0x2E48 + (2 * (int)facing), 0x2E49 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
			Name = "Hand-Painted Tiger Ratan Door";
		}

		public TigerRatanDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}