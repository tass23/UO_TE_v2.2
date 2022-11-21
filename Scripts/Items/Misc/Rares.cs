using System;
using Server.Items;

namespace Server.Items
{
	public class Rope : Item
	{
		[Constructable]
		public Rope() : this( 1 )
		{
		}

		[Constructable]
		public Rope( int amount ) : base( 0x14F8 )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public Rope( Serial serial ) : base( serial )
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

	public class IronWire : Item
	{
		[Constructable]
		public IronWire() : this( 1 )
		{
		}

		[Constructable]
		public IronWire( int amount ) : base( 0x1876 )
		{
			Stackable = true;
			Weight = 5.0;
			Amount = amount;
		}

		

		public IronWire( Serial serial ) : base( serial )
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

			if ( version < 1 && Weight == 2.0 )
				Weight = 5.0;
		}
	}

	public class SilverWire : Item
	{
		[Constructable]
		public SilverWire() : this( 1 )
		{
		}

		[Constructable]
		public SilverWire( int amount ) : base( 0x1877 )
		{
			Stackable = true;
			Weight = 5.0;
			Amount = amount;
		}

		

		public SilverWire( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 && Weight == 2.0 )
				Weight = 5.0;
		}
	}

	public class GoldWire : Item
	{
		[Constructable]
		public GoldWire() : this( 1 )
		{
		}

		[Constructable]
		public GoldWire( int amount ) : base( 0x1878 )
		{
			Stackable = true;
			Weight = 5.0;
			Amount = amount;
		}

		

		public GoldWire( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 && Weight == 2.0 )
				Weight = 5.0;
		}
	}

	public class CopperWire : Item
	{
		[Constructable]
		public CopperWire() : this( 1 )
		{
		}

		[Constructable]
		public CopperWire( int amount ) : base( 0x1879 )
		{
			Stackable = true;
			Weight = 5.0;
			Amount = amount;
		}

		

		public CopperWire( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 && Weight == 2.0 )
				Weight = 5.0;
		}
	}

	public class WhiteDriedFlowers : Item
	{
		[Constructable]
		public WhiteDriedFlowers() : this( 1 )
		{
		}

		[Constructable]
		public WhiteDriedFlowers( int amount ) : base( 0xC3C )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public WhiteDriedFlowers( Serial serial ) : base( serial )
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

	public class GreenDriedFlowers : Item
	{
		[Constructable]
		public GreenDriedFlowers() : this( 1 )
		{
		}

		[Constructable]
		public GreenDriedFlowers( int amount ) : base( 0xC3E )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public GreenDriedFlowers( Serial serial ) : base( serial )
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

	public class DriedOnions : Item
	{
		[Constructable]
		public DriedOnions() : this( 1 )
		{
		}

		[Constructable]
		public DriedOnions( int amount ) : base( 0xC40 )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public DriedOnions( Serial serial ) : base( serial )
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

	public class DriedHerbs : Item
	{
		[Constructable]
		public DriedHerbs() : this( 1 )
		{
		}

		[Constructable]
		public DriedHerbs( int amount ) : base( 0xC42 )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public DriedHerbs( Serial serial ) : base( serial )
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

	public class HorseShoes : Item
	{
		[Constructable]
		public HorseShoes() : base( 0xFB6 )
		{
			Weight = 3.0;
		}

		public HorseShoes( Serial serial ) : base( serial )
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

	public class ForgedMetal : Item
	{
		[Constructable]
		public ForgedMetal() : base( 0xFB8 )
		{
			Weight = 5.0;
		}

		public ForgedMetal( Serial serial ) : base( serial )
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

	public class Whip : Item
	{
		[Constructable]
		public Whip() : base( 0x166E )
		{
			Weight = 1.0;
		}

		public Whip( Serial serial ) : base( serial )
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

	public class PaintsAndBrush : Item
	{
		[Constructable]
		public PaintsAndBrush() : base( 0xFC1 )
		{
			Weight = 1.0;
		}

		public PaintsAndBrush( Serial serial ) : base( serial )
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

	public class PenAndInk : Item
	{
		[Constructable]
		public PenAndInk() : base( 0xFBF )
		{
			Weight = 1.0;
		}

		public PenAndInk( Serial serial ) : base( serial )
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

	public class ChiselsNorth : Item
	{
		[Constructable]
		public ChiselsNorth() : base( 0x1026 )
		{
			Weight = 1.0;
		}

		public ChiselsNorth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class ChiselsWest : Item
	{
		[Constructable]
		public ChiselsWest() : base( 0x1027 )
		{
			Weight = 1.0;
		}

		public ChiselsWest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class DirtyPan : Item
	{
		[Constructable]
		public DirtyPan() : base( 0x9E8 )
		{
			Weight = 1.0;
		}

		public DirtyPan( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class DirtySmallRoundPot : Item
	{
		[Constructable]
		public DirtySmallRoundPot() : base( 0x9E7 )
		{
			Weight = 1.0;
		}

		public DirtySmallRoundPot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class DirtyPot : Item
	{
		[Constructable]
		public DirtyPot() : base( 0x9E6 )
		{
			Weight = 1.0;
		}

		public DirtyPot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class DirtyRoundPot : Item
	{
		[Constructable]
		public DirtyRoundPot() : base( 0x9DF )
		{
			Weight = 1.0;
		}

		public DirtyRoundPot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class DirtyFrypan : Item
	{
		[Constructable]
		public DirtyFrypan() : base( 0x9DE )
		{
			Weight = 1.0;
		}

		public DirtyFrypan( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class DirtySmallPot : Item
	{
		[Constructable]
		public DirtySmallPot() : base( 0x9DD )
		{
			Weight = 1.0;
		}

		public DirtySmallPot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class DirtyKettle : Item
	{
		[Constructable]
		public DirtyKettle() : base( 0x9DC )
		{
			Weight = 1.0;
		}

		public DirtyKettle( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
	
	// ophidian bardiche - 9563 (0x255B)
	public class OphidianBardiche : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public OphidianBardiche() : base( 0x255B )
		{
			Weight = 7.0;
		}

		public OphidianBardiche( Serial serial ) : base( serial )
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

	// ogre's club - 9561 (0x2559)
	public class OgresClub : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public OgresClub() : base( 0x2559 )
		{
			Weight = 22.0;
		}

		public OgresClub( Serial serial ) : base( serial )
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

	// lizardman's staff - 9560 (0x2558)
	public class LizardmansStaff : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 5; } }
		[Constructable]
		public LizardmansStaff() : base( 0x2558 )
		{
			Weight = 6.0;
		}

		public LizardmansStaff( Serial serial ) : base( serial )
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

	// ettin hammer - 9557 (0x2555)
	public class EttinHammer : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public EttinHammer() : base( 0x2555 )
		{
			Weight = 20.0;
		}

		public EttinHammer( Serial serial ) : base( serial )
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

	// lizardman's mace - 9559 (0x2557)
	public class LizardmansMace : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public LizardmansMace() : base( 0x2557 )
		{
			Weight = 10.0;
		}

		public LizardmansMace( Serial serial ) : base( serial )
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

	// skeleton scimitar - 9568 (0x2560)
	public class SkeletonScimitar : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public SkeletonScimitar() : base( 0x2560 )
		{
			Weight = 5.0;
		}

		public SkeletonScimitar( Serial serial ) : base( serial )
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

	// skeleton axe - 9567 (0x255F)
	public class SkeletonAxe : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public SkeletonAxe() : base( 0x255F )
		{
			Weight = 4.0;
		}

		public SkeletonAxe( Serial serial ) : base( serial )
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

	// ratman sword - 9566 (0x255E)
	public class RatmanSword : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public RatmanSword() : base( 0x255E )
		{
			Weight = 6.0;
		}

		public RatmanSword( Serial serial ) : base( serial )
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

	// ratman axe - 9565 (0x255D)
	public class RatmanAxe : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public RatmanAxe() : base( 0x255D )
		{
			Weight = 5.0;
		}

		public RatmanAxe( Serial serial ) : base( serial )
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

	// orc club - 9564 (0x255C)
	public class OrcClub : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public OrcClub() : base( 0x255C )
		{
			Weight = 9.0;
		}

		public OrcClub( Serial serial ) : base( serial )
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

	// terathan staff - 9569 (0x2561)
	public class TerathanStaff : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public TerathanStaff() : base( 0x2561 )
		{
			Weight = 7.0;
		}

		public TerathanStaff( Serial serial ) : base( serial )
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

	// terathan spear - 9570 (0x2562)
	public class TerathanSpear : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public TerathanSpear() : base( 0x2562 )
		{
			Weight = 6.0;
		}

		public TerathanSpear( Serial serial ) : base( serial )
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

	// terathan mace - 9571 (0x2563)
	public class TerathanMace : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public TerathanMace() : base( 0x2563 )
		{
			Weight = 17.0;
		}

		public TerathanMace( Serial serial ) : base( serial )
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

	// troll axe - 9572 (0x2564)
	public class TrollAxe : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 5; } }
		[Constructable]
		public TrollAxe() : base( 0x2564 )
		{
			Weight = 8.0;
		}

		public TrollAxe( Serial serial ) : base( serial )
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

	// troll maul - 9573 (0x2565)
	public class TrollMaul : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 1; } }
		[Constructable]
		public TrollMaul() : base( 0x2565 )
		{
			Weight = 21.0;
		}

		public TrollMaul( Serial serial ) : base( serial )
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

	// bone mage staff - 9577 (0x2569)
	public class BoneMageStaff : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public BoneMageStaff() : base( 0x2569 )
		{
			Weight = 4.0;
		}

		public BoneMageStaff( Serial serial ) : base( serial )
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

	// orc mage staff - 9576 (0x2568)
	public class OrcMageStaff : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public OrcMageStaff() : base( 0x2568 )
		{
			Weight = 6.0;
		}

		public OrcMageStaff( Serial serial ) : base( serial )
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

	// orc lord battleaxe - 9575 (0x2567)
	public class OrcLordBattleaxe : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public OrcLordBattleaxe() : base( 0x2567 )
		{
			Weight = 12.0;
		}

		public OrcLordBattleaxe( Serial serial ) : base( serial )
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

	// frost troll club - 9574 (0x2566) 
	public class FrostTrollClub : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 8; } }
		[Constructable]
		public FrostTrollClub() : base( 0x2566 )
		{
			Weight = 19.0;
		}

		public FrostTrollClub( Serial serial ) : base( serial )
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
	
	// Demon Skull
	public class DemonSkull1 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public DemonSkull1() : base( 0x224E )
		{
			Weight = 10.0;
		}

		public DemonSkull1( Serial serial ) : base( serial )
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
	
	// Demon Skull
	public class DemonSkull2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public DemonSkull2() : base( 0x224F )
		{
			Weight = 10.0;
		}

		public DemonSkull2( Serial serial ) : base( serial )
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
	
	// Demon Skull
	public class DemonSkull3 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public DemonSkull3() : base( 0x2250 )
		{
			Weight = 10.0;
		}

		public DemonSkull3( Serial serial ) : base( serial )
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
	
	// Demon Skull
	public class DemonSkull4 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public DemonSkull4() : base( 0x2251 )
		{
			Weight = 10.0;
		}

		public DemonSkull4( Serial serial ) : base( serial )
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
	
	// Sheep Carcass
	public class SheepCarcass : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public SheepCarcass() : base( 0x1873 )
		{
			Weight = 10.0;
		}

		public SheepCarcass( Serial serial ) : base( serial )
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
	
	// Sheep 2
	public class BeefCarcass : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public BeefCarcass() : base( 0x1872 )
		{
			Weight = 10.0;
		}

		public BeefCarcass( Serial serial ) : base( serial )
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
//Custom
	/*
	public class Mushrooms1 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public Mushrooms1() : base( 3349 )
		{
			Weight = 1.0;
		}

		public Mushrooms1( Serial serial ) : base( serial )
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
	*/
	
	public class Mushrooms2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public Mushrooms2() : base( 3344 )
		{
			Weight = 1.0;
		}

		public Mushrooms2( Serial serial ) : base( serial )
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
	
	public class FlaxFlower : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 1; } }
		[Constructable]
		public FlaxFlower() : base( 6809 )
		{
			Weight = 1.0;
		}

		public FlaxFlower( Serial serial ) : base( serial )
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
	
	public class Mandrake : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public Mandrake() : base( 6365 )
		{
			Weight = 1.0;
		}

		public Mandrake( Serial serial ) : base( serial )
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
	
	public class Mandrake2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public Mandrake2() : base( 6367 )
		{
			Weight = 1.0;
		}

		public Mandrake2( Serial serial ) : base( serial )
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
//Juka
	public class Cards6 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Cards6() : base( 4002 )
		{
			Weight = 1.0;
		}

		public Cards6( Serial serial ) : base( serial )
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
	
	public class Cards7 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Cards7() : base( 4003 )
		{
			Weight = 1.0;
		}

		public Cards7( Serial serial ) : base( serial )
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
	
	public class Cards8 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Cards8() : base( 3607 )
		{
			Weight = 1.0;
		}

		public Cards8( Serial serial ) : base( serial )
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
	
	public class SkinnedRabbit : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public SkinnedRabbit() : base( 7827 )
		{
			Weight = 1.0;
		}

		public SkinnedRabbit( Serial serial ) : base( serial )
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
	
	public class RolledMap : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public RolledMap() : base( 5357 )
		{
			Weight = 1.0;
		}

		public RolledMap( Serial serial ) : base( serial )
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
	
	/*
	public class Brush : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public Brush() : base( 4976 )
		{
			Weight = 1.0;
		}

		public Brush( Serial serial ) : base( serial )
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
	*/
	
	public class Brush2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public Brush2() : base( 4077 )
		{
			Weight = 1.0;
		}

		public Brush2( Serial serial ) : base( serial )
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
	
//Spider
	public class BottleSpider : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public BottleSpider() : base( 3621 )
		{
			Weight = 1.0;
		}

		public BottleSpider( Serial serial ) : base( serial )
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
	
	public class Bottle2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 2; } }
		[Constructable]
		public Bottle2() : base( 3622 )
		{
			Weight = 1.0;
		}

		public Bottle2( Serial serial ) : base( serial )
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
	
	public class Bottle3 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Bottle3() : base( 3625 )
		{
			Weight = 1.0;
		}

		public Bottle3( Serial serial ) : base( serial )
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
	
	public class Bottle4 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public Bottle4() : base( 3627 )
		{
			Weight = 1.0;
		}

		public Bottle4( Serial serial ) : base( serial )
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
	
	public class SheetMusic : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 5; } }
		[Constructable]
		public SheetMusic() : base( 3773 )
		{
			Weight = 1.0;
		}

		public SheetMusic( Serial serial ) : base( serial )
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
	
	public class SkullMug : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public SkullMug() : base( 4091 )
		{
			Weight = 1.0;
		}

		public SkullMug( Serial serial ) : base( serial )
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
	
	public class DeadBird : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 3; } }
		[Constructable]
		public DeadBird() : base( 7811 )
		{
			Name = "A Dead Bird";
			Weight = 1.0;
		}

		public DeadBird( Serial serial ) : base( serial )
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
	
	public class DragonHeadTrophy1 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public DragonHeadTrophy1() : base( 8756 )
		{
			Name = "A Dragon Head Trophy";
			Weight = 15.0;
		}

		public DragonHeadTrophy1( Serial serial ) : base( serial )
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
	
	public class DragonHeadTrophy2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public DragonHeadTrophy2() : base( 8757 )
		{
			Name = "A Dragon Head Trophy";
			Weight = 15.0;
		}

		public DragonHeadTrophy2( Serial serial ) : base( serial )
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
	
	public class Anchor : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public Anchor() : base( 5369 )
		{
			Weight = 200.0;
		}

		public Anchor( Serial serial ) : base( serial )
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
	
	public class Anchor2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public Anchor2() : base( 5367 )
		{
			Weight = 200.0;
		}

		public Anchor2( Serial serial ) : base( serial )
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
	
	public class BirdsNest : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public BirdsNest() : base( 6868 )
		{
			Weight = 2.0;
		}

		public BirdsNest( Serial serial ) : base( serial )
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
	
	public class BirdsNest2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public BirdsNest2() : base( 6869 )
		{
			Weight = 2.0;
		}

		public BirdsNest2( Serial serial ) : base( serial )
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
	
	public class ArrowShaft : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public ArrowShaft() : base( 4132 )
		{
			Weight = 5.0;
		}

		public ArrowShaft( Serial serial ) : base( serial )
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
	
	public class ArrowShaft2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public ArrowShaft2() : base( 4133 )
		{
			Weight = 5.0;
		}

		public ArrowShaft2( Serial serial ) : base( serial )
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
	
	public class Feathers : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public Feathers() : base( 3578 )
		{
			Weight = 1.0;
		}

		public Feathers( Serial serial ) : base( serial )
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
	
	public class Feathers2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public Feathers2() : base( 7123 )
		{
			Weight = 1.0;
		}

		public Feathers2( Serial serial ) : base( serial )
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
	
	public class HoodHat : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public HoodHat() : base( 9885 )
		{
			Name = "Feathered Cap";
			Weight = 1.0;
			Hue = 1433;
		}

		public HoodHat( Serial serial ) : base( serial )
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
	
	public class HoodHat2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public HoodHat2() : base( 9886 )
		{
			Name = "Feathered Cap";
			Weight = 1.0;
			Hue = 1433;
		}

		public HoodHat2( Serial serial ) : base( serial )
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
	
	public class FeatheredMask : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public FeatheredMask() : base( 9889 )
		{
			Weight = 1.0;
		}

		public FeatheredMask( Serial serial ) : base( serial )
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
	
	public class MinotaurShackles : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public MinotaurShackles() : base( 6663 )
		{
			Weight = 1.0;
		}

		public MinotaurShackles( Serial serial ) : base( serial )
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
	
	public class MinotaurShackles2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public MinotaurShackles2() : base( 6664 )
		{
			Weight = 1.0;
		}

		public MinotaurShackles2( Serial serial ) : base( serial )
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
	
	public class MeatSkeleton : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 5; } }
		[Constructable]
		public MeatSkeleton() : base( 6941 )
		{
			Weight = 1.0;
		}

		public MeatSkeleton( Serial serial ) : base( serial )
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
	
	public class MeatSkeleton2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 5; } }
		[Constructable]
		public MeatSkeleton2() : base( 6942 )
		{
			Weight = 1.0;
		}

		public MeatSkeleton2( Serial serial ) : base( serial )
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
	
	public class OpenBook : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public OpenBook() : base( 4029 )
		{
			Weight = 1.0;
		}

		public OpenBook( Serial serial ) : base( serial )
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
	
	public class OpenBook2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public OpenBook2() : base( 4030 )
		{
			Weight = 1.0;
		}

		public OpenBook2( Serial serial ) : base( serial )
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
	
	public class SkellBlood : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public SkellBlood() : base( 5701 )
		{
			Weight = 1.0;
		}

		public SkellBlood( Serial serial ) : base( serial )
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
	
	public class SkellBlood2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public SkellBlood2() : base( 4651 )
		{
			Weight = 1.0;
		}

		public SkellBlood2( Serial serial ) : base( serial )
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
	
	public class SkellBlood3 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public SkellBlood3() : base( 4653 )
		{
			Weight = 1.0;
		}

		public SkellBlood3( Serial serial ) : base( serial )
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
	
	public class Lockpicks2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Lockpicks2() : base( 5373 )
		{
			Weight = 1.0;
		}

		public Lockpicks2( Serial serial ) : base( serial )
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
	
	public class Lockpicks3 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Lockpicks3() : base( 5374 )
		{
			Weight = 1.0;
		}

		public Lockpicks3( Serial serial ) : base( serial )
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
	
	public class EmptyToolBox : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public EmptyToolBox() : base( 7863 )
		{
			Weight = 1.0;
		}

		public EmptyToolBox( Serial serial ) : base( serial )
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
	
	public class TarotCards : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public TarotCards() : base( 4779 )
		{
			Weight = 1.0;
		}

		public TarotCards( Serial serial ) : base( serial )
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
	
	public class TarotCards2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public TarotCards2() : base( 4780 )
		{
			Weight = 1.0;
		}

		public TarotCards2( Serial serial ) : base( serial )
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
	
	public class GinsengDeco : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public GinsengDeco() : base( 6377 )
		{
			Weight = 1.0;
		}

		public GinsengDeco( Serial serial ) : base( serial )
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
	
	public class GinsengDeco2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public GinsengDeco2() : base( 6379 )
		{
			Weight = 1.0;
		}

		public GinsengDeco2( Serial serial ) : base( serial )
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
	
	public class HourGlass : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 7; } }
		[Constructable]
		public HourGlass() : base( 6163 )
		{
			Weight = 1.0;
		}

		public HourGlass( Serial serial ) : base( serial )
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
	
	public class GlowRune : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public GlowRune() : base( 3676 )
		{
			Weight = 1.0;
		}

		public GlowRune( Serial serial ) : base( serial )
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
	
	public class GlowRune2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public GlowRune2() : base( 3679 )
		{
			Weight = 1.0;
		}

		public GlowRune2( Serial serial ) : base( serial )
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
	
	public class GlowRune3 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public GlowRune3() : base( 3682 )
		{
			Weight = 1.0;
		}

		public GlowRune3( Serial serial ) : base( serial )
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
	
	public class GlowRune4 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public GlowRune4() : base( 3685 )
		{
			Weight = 1.0;
		}

		public GlowRune4( Serial serial ) : base( serial )
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
	
	public class GlowRune5 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public GlowRune5() : base( 3688 )
		{
			Weight = 1.0;
		}

		public GlowRune5( Serial serial ) : base( serial )
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
	
	public class BambooStool : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public BambooStool() : base( 4604 )
		{
			Weight = 5.0;
		}

		public BambooStool( Serial serial ) : base( serial )
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
	
	public class TapestryTitan : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public TapestryTitan() : base( 11631 )
		{
			Weight = 5.0;
		}

		public TapestryTitan( Serial serial ) : base( serial )
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
	
	public class TapestryTitan2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public TapestryTitan2() : base( 11632 )
		{
			Weight = 5.0;
		}

		public TapestryTitan2( Serial serial ) : base( serial )
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
	
	public class WallMap : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 5; } }
		[Constructable]
		public WallMap() : base( 11635 )
		{
			Weight = 5.0;
		}

		public WallMap( Serial serial ) : base( serial )
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
	
	public class WallMap2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 5; } }
		[Constructable]
		public WallMap2() : base( 11636 )
		{
			Weight = 5.0;
		}

		public WallMap2( Serial serial ) : base( serial )
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
	
	public class FruitBowlTitan : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 5; } }
		[Constructable]
		public FruitBowlTitan() : base( 11599 )
		{
			Weight = 5.0;
		}

		public FruitBowlTitan( Serial serial ) : base( serial )
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
	
	public class Blood4 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public Blood4() : base( 7572 )
		{
			Weight = 5.0;
		}

		public Blood4( Serial serial ) : base( serial )
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
	
	public class Blood5 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public Blood5() : base( 7573 )
		{
			Weight = 5.0;
		}

		public Blood5( Serial serial ) : base( serial )
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
	
	public class Blood6 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public Blood6() : base( 7574 )
		{
			Weight = 5.0;
		}

		public Blood6( Serial serial ) : base( serial )
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
	
	public class TwoPartBody : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public TwoPartBody() : base( 7402 )
		{
			Weight = 5.0;
		}

		public TwoPartBody( Serial serial ) : base( serial )
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
	
	public class TwoPartBody2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public TwoPartBody2() : base( 7395 )
		{
			Weight = 5.0;
		}

		public TwoPartBody2( Serial serial ) : base( serial )
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
	
	public class TwoPartBody3 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public TwoPartBody3() : base( 7407 )
		{
			Weight = 5.0;
		}

		public TwoPartBody3( Serial serial ) : base( serial )
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
	
	public class PillowCorpse : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public PillowCorpse() : base( 6421 )
		{
			Weight = 5.0;
		}

		public PillowCorpse( Serial serial ) : base( serial )
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
	
	public class FoldedSheet : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public FoldedSheet() : base( 2708 )
		{
			Weight = 5.0;
		}

		public FoldedSheet( Serial serial ) : base( serial )
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
	
	public class EvilTotem : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public EvilTotem() : base( 13899 )
		{
			Weight = 5.0;
		}

		public EvilTotem( Serial serial ) : base( serial )
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
	
	public class EvilTotem2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public EvilTotem2() : base( 13979 )
		{
			Weight = 5.0;
		}

		public EvilTotem2( Serial serial ) : base( serial )
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
	
	public class EvilTotem3 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public EvilTotem3() : base( 13901 )
		{
			Weight = 5.0;
		}

		public EvilTotem3( Serial serial ) : base( serial )
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
	
	public class EvilTotem4 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public EvilTotem4() : base( 13980 )
		{
			Weight = 5.0;
		}

		public EvilTotem4( Serial serial ) : base( serial )
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
	
	public class OpenBook4 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public OpenBook4() : base( 4084 )
		{
			Weight = 5.0;
		}

		public OpenBook4( Serial serial ) : base( serial )
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
	
	public class OpenBook5 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public OpenBook5() : base( 7715 )
		{
			Weight = 5.0;
		}

		public OpenBook5( Serial serial ) : base( serial )
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
	
	/*
	public class Silverware : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Silverware() : base( 2493 )
		{
			Weight = 5.0;
		}

		public Silverware( Serial serial ) : base( serial )
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
	*/
	
	public class Silverware2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 6; } }
		[Constructable]
		public Silverware2() : base( 2494 )
		{
			Weight = 5.0;
		}

		public Silverware2( Serial serial ) : base( serial )
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
	
	public class Silverware3 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Silverware3() : base( 2495 )
		{
			Weight = 5.0;
		}

		public Silverware3( Serial serial ) : base( serial )
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
	
	public class Silverware4 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public Silverware4() : base( 2496 )
		{
			Weight = 5.0;
		}

		public Silverware4( Serial serial ) : base( serial )
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
	
	public class IceCrystals : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public IceCrystals() : base( 12252 )
		{
			Name = "An Ice Crystal";
			Weight = 5.0;
			Hue = 1153;
		}

		public IceCrystals( Serial serial ) : base( serial )
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
	
	public class IceCrystals2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public IceCrystals2() : base( 12253 )
		{
			Name = "An Ice Crystal";
			Weight = 5.0;
			Hue = 1153;
		}

		public IceCrystals2( Serial serial ) : base( serial )
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
	
	public class PotOfWax : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public PotOfWax() : base( 5162 )
		{
			Weight = 5.0;
			//Hue = 1153;
		}

		public PotOfWax( Serial serial ) : base( serial )
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
	
	public class PotOfWax2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public PotOfWax2() : base( 5163 )
		{
			Weight = 5.0;
			//Hue = 1153;
		}

		public PotOfWax2( Serial serial ) : base( serial )
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
	
	public class DippingSticks : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public DippingSticks() : base( 5160 )
		{
			Weight = 5.0;
			//Hue = 1153;
		}

		public DippingSticks( Serial serial ) : base( serial )
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
	
	public class DippingSticks2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public DippingSticks2() : base( 5161 )
		{
			Weight = 5.0;
			//Hue = 1153;
		}

		public DippingSticks2( Serial serial ) : base( serial )
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
	
	public class ReaperStat : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public ReaperStat() : base( 0x3128 )
		{
			Name = "A Reaper Statue";
			Weight = 5.0;
			//Hue = 1153;
		}

		public ReaperStat( Serial serial ) : base( serial )
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
	
	public class ReaperStat1 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public ReaperStat1() : base( 0x3129 )
		{
			Name = "A Reaper Statue";
			Weight = 5.0;
			//Hue = 1153;
		}

		public ReaperStat1( Serial serial ) : base( serial )
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
	
	public class ReaperStat2 : BaseDecorationArtifact
	{
		public override int ArtifactRarity{ get{ return 4; } }
		[Constructable]
		public ReaperStat2() : base( 0x312A )
		{
			Name = "A Reaper Statue";
			Weight = 5.0;
			//Hue = 1153;
		}

		public ReaperStat2( Serial serial ) : base( serial )
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
	
	[Flipable( 4465, 4466 )]  
	public class GraveStone1: BaseDecorationArtifact
	{	
		public override int ArtifactRarity{ get{ return 4; } }

       		[Constructable]
		public GraveStone1() : base(4466) // 4 differnt stone each haveing 2 directions
		{
			Name = "A Gravestone";
			Weight = 95.0;
		}

        	public GraveStone1(Serial serial) : base( serial )
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

	[Flipable( 4476, 4475 )]  
	public class GraveStone2: BaseDecorationArtifact
	{	
		public override int ArtifactRarity{ get{ return 4; } }
       		[Constructable]
		public GraveStone2() : base(4476) // 4 differnt stone each haveing 2 directions
		{
			Name = "A Gravestone";
			Weight = 95.0;
		}

        	public GraveStone2(Serial serial) : base( serial )
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

	[Flipable( 4473, 4474 )]  
	public class GraveStone3: BaseDecorationArtifact
	{	
		public override int ArtifactRarity{ get{ return 4; } }
       		[Constructable]
		public GraveStone3() : base(4473) 
		{
			Name = "A Gravestone";
			Weight = 97.0;
		}

        	public GraveStone3(Serial serial) : base( serial )
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

	[Flipable( 4477, 4478 )]  
	public class GraveStone4: BaseDecorationArtifact
	{	
	public override int ArtifactRarity{ get{ return 4; } }
       		[Constructable]
		public GraveStone4() : base(4477) // 4 differnt stone each haveing 2 directions
		{
			Name = "A Gravestone";
			Weight = 98.0;
		}

        	public GraveStone4(Serial serial) : base( serial )
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