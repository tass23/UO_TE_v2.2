
using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items.Crops
{
	public class GrapeVineBranchEast : BaseGrapeVine
	{
		[Constructable]
		public GrapeVineBranchEast() : base( 0xD23 )
		{
			Name = "East Branch (1)";
		}

		public GrapeVineBranchEast( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVineBranchEast2 : BaseGrapeVine
	{
		[Constructable]
		public GrapeVineBranchEast2() : base( 0xD24 )
		{
			Name = "East Branch (2)";
		}

		public GrapeVineBranchEast2( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVineBranchNorth : BaseGrapeVine
	{
		[Constructable]
		public GrapeVineBranchNorth() : base( 0xD1E )
		{
			Name = "North Branch (1)";
		}

		public GrapeVineBranchNorth( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVineBranchNorth2 : BaseGrapeVine
	{
		[Constructable]
		public GrapeVineBranchNorth2() : base( 0xD1F )
		{
			Name = "North Branch (2)";
		}

		public GrapeVineBranchNorth2( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVinePoleEast : BaseGrapeVine
	{
		[Constructable]
		public GrapeVinePoleEast() : base( 0xD20 )
		{
			Name = "East Pole (End 2)";
		}

		public GrapeVinePoleEast( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVinePoleEast2 : BaseGrapeVine
	{
		[Constructable]
		public GrapeVinePoleEast2() : base( 0xD21 )
		{
			Name = "East Pole (Center)";
		}

		public GrapeVinePoleEast2( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVinePoleEast3 : BaseGrapeVine
	{
		[Constructable]
		public GrapeVinePoleEast3() : base( 0xD22 )
		{
			Name = "East Pole (End 1)";
		}

		public GrapeVinePoleEast3( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVinePoleNorth : BaseGrapeVine
	{
		[Constructable]
		public GrapeVinePoleNorth() : base( 0xD1B )
		{
			Name = "North Pole (End 2)";
		}

		public GrapeVinePoleNorth( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVinePoleNorth2 : BaseGrapeVine
	{
		[Constructable]
		public GrapeVinePoleNorth2() : base( 0xD1D )
		{
			Name = "North Pole (End 1)";
		}

		public GrapeVinePoleNorth2( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapeVinePoleNorth3 : BaseGrapeVine
	{
		[Constructable]
		public GrapeVinePoleNorth3() : base( 0xD1C )
		{
			Name = "North Pole (Center)";
		}

		public GrapeVinePoleNorth3( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}