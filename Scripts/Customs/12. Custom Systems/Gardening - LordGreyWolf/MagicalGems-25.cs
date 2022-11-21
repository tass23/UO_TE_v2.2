using System;
using Server;

namespace Server.Items
{
	public abstract class BaseMagicalGem : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		public BaseMagicalGem() : base( 0x172A ){}

		public BaseMagicalGem( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[FlipableAttribute( 0x172A, 0x172B )]
	public class MagicalRuby : BaseMagicalGem
	{
		[Constructable]
		public MagicalRuby()
		{
			Stackable = true;
			Name = "Magical Ruby";
			Hue = 33;

		}

		public MagicalRuby( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalJade : BaseMagicalGem
	{
		[Constructable]
		public MagicalJade()
		{
			Stackable = true;
			Name = "Magical Jade";
			Hue = 66;

		}

		public MagicalJade( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalSapphire : BaseMagicalGem
	{
		[Constructable]
		public MagicalSapphire()
		{
			Stackable = true;
			Name = "Magical Sapphire";
			Hue = 96;

		}

		public MagicalSapphire( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalCitrine : BaseMagicalGem
	{
		[Constructable]
		public MagicalCitrine()
		{
			Stackable = true;
			Name = "Magical Citrine";
			Hue = 45;

		}

		public MagicalCitrine( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalAmethyst : BaseMagicalGem
	{
		[Constructable]
		public MagicalAmethyst()
		{
			Stackable = true;
			Name = "Magical Amethyst";
			Hue = 320;

		}

		public MagicalAmethyst( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalBloodStone : BaseMagicalGem
	{
		[Constructable]
		public MagicalBloodStone()
		{
			Stackable = true;
			Name = "Magical Blood Stone";
			Hue = 37;

		}

		public MagicalBloodStone( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalBlueDiamond : BaseMagicalGem
	{
		[Constructable]
		public MagicalBlueDiamond()
		{
			Stackable = true;
			Name = "Magical Blue Diamond";
			Hue = 92;

		}

		public MagicalBlueDiamond( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalPearl : BaseMagicalGem
	{
		[Constructable]
		public MagicalPearl()
		{
			Stackable = true;
			Name = "Magical Pearl";
			Hue = 1153;

		}

		public MagicalPearl( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalOnyx : BaseMagicalGem
	{
		[Constructable]
		public MagicalOnyx()
		{
			Stackable = true;
			Name = "Magical Onyx";
			Hue = 802;

		}

		public MagicalOnyx( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalEmerald : BaseMagicalGem
	{
		[Constructable]
		public MagicalEmerald()
		{
			Stackable = true;
			Name = "Magical Emerald";
			Hue = 67;

		}

		public MagicalEmerald( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalStarRose : BaseMagicalGem
	{
		[Constructable]
		public MagicalStarRose()
		{
			Stackable = true;
			Name = "Magical Star Rose";
			Hue = 1166;

		}

		public MagicalStarRose( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalStarSapphire : BaseMagicalGem
	{
		[Constructable]
		public MagicalStarSapphire()
		{
			Stackable = true;
			Name = "Magical Star Sapphire";
			Hue = 1266;

		}

		public MagicalStarSapphire( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalTurquoise : BaseMagicalGem
	{
		[Constructable]
		public MagicalTurquoise()
		{
			Stackable = true;
			Name = "Magical Turquoise";
			Hue = 85;

		}

		public MagicalTurquoise( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalFireEmerald : BaseMagicalGem
	{
		[Constructable]
		public MagicalFireEmerald()
		{
			Stackable = true;
			Name = "Magical Fire Emerald";
			Hue = 1161;

		}

		public MagicalFireEmerald( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalJasper : BaseMagicalGem
	{
		[Constructable]
		public MagicalJasper()
		{
			Stackable = true;
			Name = "Magical Jasper";
			Hue = 438;

		}

		public MagicalJasper( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalDiamond : BaseMagicalGem
	{
		[Constructable]
		public MagicalDiamond()
		{
			Stackable = true;
			Name = "Magical Diamond";
			Hue = 1001;

		}

		public MagicalDiamond( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalEclipseStone : BaseMagicalGem
	{
		[Constructable]
		public MagicalEclipseStone()
		{
			Stackable = true;
			Name = "Magical Eclipse Stone";
			Hue = 947;

		}

		public MagicalEclipseStone( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalMoonStoneGem : BaseMagicalGem
	{
		[Constructable]
		public MagicalMoonStoneGem()
		{
			Stackable = true;
			Name = "Magical Moon Stone";
			Hue = 605;

		}

		public MagicalMoonStoneGem( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalSunStone : BaseMagicalGem
	{
		[Constructable]
		public MagicalSunStone()
		{
			Stackable = true;
			Name = "Magical Sun Stone";
			Hue = 56;

		}

		public MagicalSunStone( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalAmber : BaseMagicalGem
	{
		[Constructable]
		public MagicalAmber()
		{
			Stackable = true;
			Name = "Magical Amber";
			Hue = 247;

		}

		public MagicalAmber( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalOpal : BaseMagicalGem
	{
		[Constructable]
		public MagicalOpal()
		{
			Stackable = true;
			Name = "Magical Opal";
			Hue = 601;

		}

		public MagicalOpal( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class MagicalTourmaline : BaseMagicalGem
	{
		[Constructable]
		public MagicalTourmaline()
		{
			Stackable = true;
			Name = "Magical Tourmaline";
			Hue = 54;

		}

		public MagicalTourmaline( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class MagicalTopaz : BaseMagicalGem
	{
		[Constructable]
		public MagicalTopaz()
		{
			Stackable = true;
			Name = "Magical Topaz";
			Hue = 96;

		}

		public MagicalTopaz( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalFireOpal : BaseMagicalGem
	{
		[Constructable]
		public MagicalFireOpal()
		{
			Stackable = true;
			Name = "Magical Fire Opal";
			Hue = 49;

		}

		public MagicalFireOpal( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MagicalStarRuby : BaseMagicalGem
	{
		[Constructable]
		public MagicalStarRuby()
		{
			Stackable = true;
			Name = "Magical Star Ruby";
			Hue = 2953;

		}

		public MagicalStarRuby( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}