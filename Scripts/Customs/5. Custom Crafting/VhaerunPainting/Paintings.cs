using System;
using Server.Network;

namespace Server.Items
{

	public class ManPortraitSouth : Item
	{
		[Constructable]
		public ManPortraitSouth() : base( 0xEA3 )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public ManPortraitSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class ManPortraitEast : Item
	{
		[Constructable]
		public ManPortraitEast() : base( 0xEA4 )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public ManPortraitEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WomanPortraitEast : Item
	{
		[Constructable]
		public WomanPortraitEast() : base( 0xEC8 )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public WomanPortraitEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WomanPortraitEast2 : Item
	{
		[Constructable]
		public WomanPortraitEast2() : base( 0xEC9 )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public WomanPortraitEast2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WomanPortraitSouth : Item
	{
		[Constructable]
		public WomanPortraitSouth() : base( 0xE9F )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public WomanPortraitSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WomanPortraitSouth2 : Item
	{
		[Constructable]
		public WomanPortraitSouth2() : base( 0xEE7 )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public WomanPortraitSouth2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WomanBluePortrait : Item
	{
		[Constructable]
		public WomanBluePortrait() : base( 0xEA5 )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public WomanBluePortrait( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WomanRedPortrait : Item
	{
		[Constructable]
		public WomanRedPortrait() : base( 0xEA7 )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public WomanRedPortrait( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WomanGreenPortrait : Item
	{
		[Constructable]
		public WomanGreenPortrait() : base( 0xEA6 )
		{
			Weight = 10;
			Name = "Portrait";
		}

		public WomanGreenPortrait( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LargeWomanPortrait : Item
	{
		[Constructable]
		public LargeWomanPortrait() : base( 0xEA0 )
		{
			Weight = 10;
			Name = "Lady's Portrait";
		}

		public LargeWomanPortrait( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class RedPaintingSouth : Item
	{
		[Constructable]
		public RedPaintingSouth() : base( 0x240D )
		{
			Weight = 10;
			Name = "Painting";
		}

		public RedPaintingSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class RedPaintingEast : Item
	{
		[Constructable]
		public RedPaintingEast() : base( 0x240E )
		{
			Weight = 10;
			Name = "Painting";
		}

		public RedPaintingEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class TanPaintingSouth : Item
	{
		[Constructable]
		public TanPaintingSouth() : base( 0x240F )
		{
			Weight = 10;
			Name = "Painting";
		}

		public TanPaintingSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class TanPaintingEast : Item
	{
		[Constructable]
		public TanPaintingEast() : base( 0x2410 )
		{
			Weight = 10;
			Name = "Painting";
		}

		public TanPaintingEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MountPaintingSouth : Item
	{
		[Constructable]
		public MountPaintingSouth() : base( 0x2411 )
		{
			Weight = 10;
			Name = "Mountain Painting";
		}

		public MountPaintingSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MountPaintingEast : Item
	{
		[Constructable]
		public MountPaintingEast() : base( 0x2412 )
		{
			Weight = 10;
			Name = "Mountain Painting";
		}

		public MountPaintingEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WarriorPaintingSouth : Item
	{
		[Constructable]
		public WarriorPaintingSouth() : base( 0x2413 )
		{
			Weight = 10;
			Name = "Warrior Painting";
		}

		public WarriorPaintingSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WarriorPaintingEast : Item
	{
		[Constructable]
		public WarriorPaintingEast() : base( 0x2414 )
		{
			Weight = 10;
			Name = "Warrior Painting";
		}

		public WarriorPaintingEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class EasternPaintingSouth1 : Item
	{
		[Constructable]
		public EasternPaintingSouth1() : base( 0x2415 )
		{
			Weight = 10;
			Name = "Eastern Painting";
		}

		public EasternPaintingSouth1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class EasternPaintingEast1 : Item
	{
		[Constructable]
		public EasternPaintingEast1() : base( 0x2416 )
		{
			Weight = 10;
			Name = "Eastern Painting";
		}

		public EasternPaintingEast1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class EasternPaintingSouth2 : Item
	{
		[Constructable]
		public EasternPaintingSouth2() : base( 0x2417 )
		{
			Weight = 10;
			Name = "Eastern Painting";
		}

		public EasternPaintingSouth2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class EasternPaintingEast2 : Item
	{
		[Constructable]
		public EasternPaintingEast2() : base( 0x2418 )
		{
			Weight = 10;
			Name = "Eastern Painting";
		}

		public EasternPaintingEast2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SmallFancyPortrait : Item
	{
		[Constructable]
		public SmallFancyPortrait() : base( 0x2A99 )
		{
			Weight = 10;
			Name = "Small Fancy Portrait";
		}

		public SmallFancyPortrait( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class FancyPortraitSouth : Item
	{
		[Constructable]
		public FancyPortraitSouth() : base( 0x2A5D )
		{
			Weight = 10;
			Name = "Large Portrait";
		}

		public FancyPortraitSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class FancyPortraitEast : Item
	{
		[Constructable]
		public FancyPortraitEast() : base( 0x2A61 )
		{
			Weight = 10;
			Name = "Large Portrait";
		}

		public FancyPortraitEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class FancyLadyPortraitSouth1 : Item
	{
		[Constructable]
		public FancyLadyPortraitSouth1() : base( 0x2A65 )
		{
			Weight = 10;
			Name = "Fancy Lady'S Portrait";
		}

		public FancyLadyPortraitSouth1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class FancyLadyPortraitSouth2 : Item
	{
		[Constructable]
		public FancyLadyPortraitSouth2() : base( 0x2A66 )
		{
			Weight = 10;
			Name = "Fancy Lady's Portrait";
		}

		public FancyLadyPortraitSouth2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class FancyLadyPortraitEast1 : Item
	{
		[Constructable]
		public FancyLadyPortraitEast1() : base( 0x2A67 )
		{
			Weight = 10;
			Name = "Fancy Lady's Portrait";
		}

		public FancyLadyPortraitEast1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class FancyLadyPortraitEast2 : Item
	{
		[Constructable]
		public FancyLadyPortraitEast2() : base( 0x2A68 )
		{
			Weight = 10;
			Name = "Fancy Lady's Portrait";
		}

		public FancyLadyPortraitEast2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class YoungManPortraitSouth : Item
	{
		[Constructable]
		public YoungManPortraitSouth() : base( 0x2A69 )
		{
			Weight = 10;
			Name = "Young Man's Portrait";
		}

		public YoungManPortraitSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class YoungManPortraitEast : Item
	{
		[Constructable]
		public YoungManPortraitEast() : base( 0x2A6D )
		{
			Weight = 10;
			Name = "Young Man's Portrait";
		}

		public YoungManPortraitEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
