using System;

namespace Server.Items
{
	public class BaseHerb : Item
	{
		[Constructable]
		public BaseHerb( int amount, int itemID ) : base( itemID )
		{
			Weight = 0.1;
			Stackable = true;
			Amount = amount;
			Movable = true;
		}

		public BaseHerb( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Sage : BaseHerb
	{
		[Constructable]
		public Sage() : this( 1 ) { }

		[Constructable]
		public Sage( int amount ) : base( amount, 0xC3D )
		{
			Name = "Sage";
		}

		public Sage( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Acacia : BaseHerb
	{
		[Constructable]
		public Acacia() : this( 1 ) { }

		[Constructable]
		public Acacia( int amount ) : base( amount, 0xDE1 )
		{
			Name = "Acacia";
		}

		public Acacia( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Anise : BaseHerb
	{
		[Constructable]
		public Anise() : this( 1 ) { }

		[Constructable]
		public Anise( int amount ) : base( amount, 0xF2C )
		{
			Name = "Anise";
			Hue = 0x5E2;
		}

		public Anise( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Basil : BaseHerb
	{
		[Constructable]
		public Basil() : this( 1 ) { }

		[Constructable]
		public Basil( int amount ) : base( amount, 0xC3E )
		{
			Name = "Basil";
		}

		public Basil( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BayLeaf : BaseHerb
	{
		[Constructable]
		public BayLeaf() : this( 1 ) { }

		[Constructable]
		public BayLeaf( int amount ) : base( amount, 0xF78 )
		{
			Name = "Bay leaf";
			Hue = 0x59C;
		}

		public BayLeaf( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Chamomile : BaseHerb
	{
		[Constructable]
		public Chamomile() : this( 1 ) { }

		[Constructable]
		public Chamomile( int amount ) : base( amount, 0xF8D )
		{
			Name = "Chamoile";
			Hue = 0x36;
		}

		public Chamomile( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Caraway : BaseHerb
	{
		[Constructable]
		public Caraway() : this( 1 ) { }

		[Constructable]
		public Caraway( int amount ) : base( amount, 0xF29 )
		{
			Name = "Caraway";
			Hue = 0x5E2;
		}

		public Caraway( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Cilantro : BaseHerb
	{
		[Constructable]
		public Cilantro() : this( 1 ) { }

		[Constructable]
		public Cilantro( int amount ) : base( amount, 0x1020 )
		{
			Name = "Cilantro";
			Hue = 0x58B;
		}

		public Cilantro( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Cinnamon : BaseHerb
	{
		[Constructable]
		public Cinnamon() : this( 1 ) { }

		[Constructable]
		public Cinnamon( int amount ) : base( amount, 0xF80 )
		{
			Name = "Cinnamon";
			Hue = 0x21A;
		}

		public Cinnamon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Clove : BaseHerb
	{
		[Constructable]
		public Clove() : this( 1 ) { }

		[Constructable]
		public Clove( int amount ) : base( amount, 0xF8E )
		{
			Name = "Clove";
			Hue = 0x39A;
		}

		public Clove( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Copal : BaseHerb
	{
		[Constructable]
		public Copal() : this( 1 ) { }

		[Constructable]
		public Copal( int amount ) : base( amount, 0xF21 )
		{
			Name = "Copal";
			Hue = 0x1C7;
		}

		public Copal( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Coriander : BaseHerb
	{
		[Constructable]
		public Coriander() : this( 1 ) { }

		[Constructable]
		public Coriander( int amount ) : base( amount, 0xF15 )
		{
			Name = "Coriander";
			Hue = 0x5E2;
		}

		public Coriander( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Dill : BaseHerb
	{
		[Constructable]
		public Dill() : this( 1 ) { }

		[Constructable]
		public Dill( int amount ) : base( amount, 0xF1B )
		{
			Name = "Dill";
			Hue = 0x1D7;
		}

		public Dill( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Dragonsblood : BaseHerb
	{
		[Constructable]
		public Dragonsblood() : this( 1 ) { }

		[Constructable]
		public Dragonsblood( int amount ) : base( amount, 0xF8F )
		{
			Name = "Dragonsblood";
			Hue = 0x219;
		}

		public Dragonsblood( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Frankincense : BaseHerb
	{
		[Constructable]
		public Frankincense() : this( 1 ) { }

		[Constructable]
		public Frankincense( int amount ) : base( amount, 0xF91 )
		{
			Name = "Frankincense";
			Hue = 0x5A7;
		}

		public Frankincense( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Lavender : BaseHerb
	{
		[Constructable]
		public Lavender() : this( 1 ) { }

		[Constructable]
		public Lavender( int amount ) : base( amount, 0xC3B )
		{
			Name = "Lavender";
			Hue = 0x552;
		}

		public Lavender( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Marjoram : BaseHerb
	{
		[Constructable]
		public Marjoram() : this( 1 ) { }

		[Constructable]
		public Marjoram( int amount ) : base( amount, 0xC3E )
		{
			Name = "Marjoram";
			Hue = 0x597;
		}

		public Marjoram( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Meadowsweet : BaseHerb
	{
		[Constructable]
		public Meadowsweet() : this( 1 ) { }

		[Constructable]
		public Meadowsweet( int amount ) : base( amount, 0xF88 )
		{
			Name = "Meadowsweet";
			Hue = 0x585;
		}

		public Meadowsweet( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Mint : BaseHerb
	{
		[Constructable]
		public Mint() : this( 1 ) { }

		[Constructable]
		public Mint( int amount ) : base( amount, 0xC41 )
		{
			Name = "Mint";
			Hue = 0x593;
		}

		public Mint( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Mugwort : BaseHerb
	{
		[Constructable]
		public Mugwort() : this( 1 ) { }

		[Constructable]
		public Mugwort( int amount ) : base( amount, 0xC42 )
		{
			Name = "Mugwort";
			Hue = 0x595;
		}

		public Mugwort( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Mustard : BaseHerb
	{
		[Constructable]
		public Mustard() : this( 1 ) { }

		[Constructable]
		public Mustard( int amount ) : base( amount, 0xF2C )
		{
			Name = "Mustard";
			Hue = 0x5E2;
		}

		public Mustard( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Myrrh : BaseHerb
	{
		[Constructable]
		public Myrrh() : this( 1 ) { }

		[Constructable]
		public Myrrh( int amount ) : base( amount, 0xF7B )
		{
			Name = "Myrrh";
			Hue = 0x415;
		}

		public Myrrh( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Olive : BaseHerb
	{
		[Constructable]
		public Olive() : this( 1 ) { }

		[Constructable]
		public Olive( int amount ) : base( amount, 0xF8D )
		{
			Name = "Olive";
			Hue = 0x588;
		}

		public Olive( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Oregano : BaseHerb
	{
		[Constructable]
		public Oregano() : this( 1 ) { }

		[Constructable]
		public Oregano( int amount ) : base( amount, 0xC3D )
		{
			Name = "Oregano";
		}

		public Oregano( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Orris : BaseHerb
	{
		[Constructable]
		public Orris() : this( 1 ) { }

		[Constructable]
		public Orris( int amount ) : base( amount, 0xF85 )
		{
			Name = "Orris";
			Hue = 0x416;
		}

		public Orris( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Patchouli : BaseHerb
	{
		[Constructable]
		public Patchouli() : this( 1 ) { }

		[Constructable]
		public Patchouli( int amount ) : base( amount, 0x18E4 )
		{
			Name = "Patchouli";
			Hue = 0x597;
		}

		public Patchouli( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Peppercorn : BaseHerb
	{
		[Constructable]
		public Peppercorn() : this( 1 ) { }

		[Constructable]
		public Peppercorn( int amount ) : base( amount, 0xF87 )
		{
			Name = "Peppercorn";
			Hue = 0x3D6;
		}

		public Peppercorn( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class RoseHerb : BaseHerb
	{
		[Constructable]
		public RoseHerb() : this( 1 ) { }

		[Constructable]
		public RoseHerb( int amount ) : base( amount, 0xC3D )
		{
			Name = "Rose";
		}

		public RoseHerb( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Rosemary : BaseHerb
	{
		[Constructable]
		public Rosemary() : this( 1 ) { }

		[Constructable]
		public Rosemary( int amount ) : base( amount, 0x1020 )
		{
			Name = "Rosemary";
			Hue = 0x594;
		}

		public Rosemary( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Saffron : BaseHerb
	{
		[Constructable]
		public Saffron() : this( 1 ) { }

		[Constructable]
		public Saffron( int amount ) : base( amount, 0xC3C )
		{
			Name = "Saffron";
		}

		public Saffron( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Sandelwood : BaseHerb
	{
		[Constructable]
		public Sandelwood() : this( 1 ) { }

		[Constructable]
		public Sandelwood( int amount ) : base( amount, 0x979 )
		{
			Name = "Sandelwood";
			Hue = 0x59D;
		}

		public Sandelwood( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SlipperyElm : BaseHerb
	{
		[Constructable]
		public SlipperyElm() : this( 1 ) { }

		[Constructable]
		public SlipperyElm( int amount ) : base( amount, 0xF89 )
		{
			Name = "Slippery Elm";
		}

		public SlipperyElm( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Thyme : BaseHerb
	{
		[Constructable]
		public Thyme() : this( 1 ) { }

		[Constructable]
		public Thyme( int amount ) : base( amount, 0xC3D )
		{
			Name = "Thyme";
		}

		public Thyme( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Valerian : BaseHerb
	{
		[Constructable]
		public Valerian() : this( 1 ) { }

		[Constructable]
		public Valerian( int amount ) : base( amount, 0xF86 )
		{
			Name = "Valerian";
			Hue = 0x47E;
		}

		public Valerian( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class WillowBark : BaseHerb
	{
		[Constructable]
		public WillowBark() : this( 1 ) { }

		[Constructable]
		public WillowBark( int amount ) : base( amount, 0xF79 )
		{
			Name = "Willow Bark";
		}

		public WillowBark( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}