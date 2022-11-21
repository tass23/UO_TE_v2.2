using System;
using Server.Network;
namespace Server.Items
{
	public class AsianVegMix : Food
	{
		[Constructable]
		public AsianVegMix() : this( 1 ) { }
		[Constructable]
		public AsianVegMix( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Asian vegetable Mix";
		}
		public AsianVegMix( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlCornFlakes : Food
	{
		[Constructable]
		public BowlCornFlakes() : this( 1 ) { }
		[Constructable]
		public BowlCornFlakes( int amount ) : base( amount, 0x15FA )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Corn Flakes";
		}
		public BowlCornFlakes( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlRiceKrisps : Food
	{
		[Constructable]
		public BowlRiceKrisps() : this( 1 ) { }
		[Constructable]
		public BowlRiceKrisps( int amount ) : base( amount, 0x1602 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Rice Krisps";
		}
		public BowlRiceKrisps( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class CheeseSauce : Food
	{
		[Constructable]
		public CheeseSauce() : this( 1 ) { }
		[Constructable]
		public CheeseSauce( int amount ) : base( amount, 0x15FA )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Cheese Sauce";
		}
		public CheeseSauce( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class ChocIceCream : Food
	{
		[Constructable]
		public ChocIceCream() : this( 1 ) { }
		[Constructable]
		public ChocIceCream( int amount ) : base( amount, 0x15FA )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Chocolate Ice Cream";
		}
		public ChocIceCream( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Gravy : Food
	{
		[Constructable]
		public Gravy() : this( 1 ) { }
		[Constructable]
		public Gravy( int amount ) : base( amount, 0x15FD )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Gravy";
			this.Hue = 1012;
		}
		public Gravy( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class MixedVegetables : Food
	{
		[Constructable]
		public MixedVegetables() : this( 1 ) { }
		[Constructable]
		public MixedVegetables( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Mixed Vegetables";
		}
		public MixedVegetables( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BroccoliCheese : Food
	{
		[Constructable]
		public BroccoliCheese() : this( 1 ) { }
		[Constructable]
		public BroccoliCheese( int amount ) : base( amount, 0x15FC )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Broccoli and Cheese";
		}
		public BroccoliCheese( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BroccoliCaulCheese : Food
	{
		[Constructable]
		public BroccoliCaulCheese() : this( 1 ) { }
		[Constructable]
		public BroccoliCaulCheese( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Broccoli and Cauliflower with Cheese";
		}
		public BroccoliCaulCheese( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class ChickenNoodleSoup : Food
	{
		[Constructable]
		public ChickenNoodleSoup() : this( 1 ) { }
		[Constructable]
		public ChickenNoodleSoup( int amount ) : base( amount, 0x15FA )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Chicken Noodle Soup";
		}
		public ChickenNoodleSoup( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlBeets : Food
	{
		[Constructable]
		public BowlBeets() : this( 1 ) { }
		[Constructable]
		public BowlBeets( int amount ) : base( amount, 0x15F9 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Beets";
		}
		public BowlBeets( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlBroccoli : Food
	{
		[Constructable]
		public BowlBroccoli() : this( 1 ) { }
		[Constructable]
		public BowlBroccoli( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Broccoli";
		}
		public BowlBroccoli( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlCauliflower : Food
	{
		[Constructable]
		public BowlCauliflower() : this( 1 ) { }
		[Constructable]
		public BowlCauliflower( int amount ) : base( amount, 0x15FA )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Cauliflower";
		}
		public BowlCauliflower( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlGreenBeans : Food
	{
		[Constructable]
		public BowlGreenBeans() : this( 1 ) { }
		[Constructable]
		public BowlGreenBeans( int amount ) : base( amount, 0x15FC )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Green Beans";
		}
		public BowlGreenBeans( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlSpinach : Food
	{
		[Constructable]
		public BowlSpinach() : this( 1 ) { }
		[Constructable]
		public BowlSpinach( int amount ) : base( amount, 0x15FC )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Spinach";
		}
		public BowlSpinach( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlTurnips : Food
	{
		[Constructable]
		public BowlTurnips() : this( 1 ) { }
		[Constructable]
		public BowlTurnips( int amount ) : base( amount, 0x15F9 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Turnips";
		}
		public BowlTurnips( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlMashedPotatos : Food
	{
		[Constructable]
		public BowlMashedPotatos() : this( 1 ) { }
		[Constructable]
		public BowlMashedPotatos( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Mashed Potatos";
		}
		public BowlMashedPotatos( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class MacaroniCheese : Food
	{
		[Constructable]
		public MacaroniCheese() : this( 1 ) { }
		[Constructable]
		public MacaroniCheese( int amount ) : base( amount, 0x15FF )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Macaroni and Cheese";
		}
		public MacaroniCheese( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class CauliflowerCheese : Food
	{
		[Constructable]
		public CauliflowerCheese() : this( 1 ) { }
		[Constructable]
		public CauliflowerCheese( int amount ) : base( amount, 0x1602 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Cauliflower and Cheese";
		}
		public CauliflowerCheese( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PotatoFries : Food
	{
		[Constructable]
		public PotatoFries() : this( 1 ) { }
		[Constructable]
		public PotatoFries( int amount ) : base( amount, 0x160C )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Potato Fries";
		}
		public PotatoFries( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlOatmeal : Food
	{
		[Constructable]
		public BowlOatmeal() : this( 1 ) { }
		[Constructable]
		public BowlOatmeal( int amount ) : base( amount, 0x1602 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Oatmeal";
		}
		public BowlOatmeal( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class TomatoRice : Food
	{
		[Constructable]
		public TomatoRice() : this( 1 ) { }
		[Constructable]
		public TomatoRice( int amount ) : base( amount, 0x1606 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Tomato and Rice";
		}
		public TomatoRice( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Popcorn : Food
	{
		[Constructable]
		public Popcorn() : this( 1 ) { }
		[Constructable]
		public Popcorn( int amount ) : base( amount, 0x1602 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Popcorn";
		}
		public Popcorn( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlCookedVeggies : Food
	{
		[Constructable]
		public BowlCookedVeggies() : this( 1 ) { }
		[Constructable]
		public BowlCookedVeggies( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Cooked Bowl of Vegetables";
		}
		public BowlCookedVeggies( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlRice : Food
	{
		[Constructable]
		public BowlRice() : this( 1 ) { }
		[Constructable]
		public BowlRice( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Name = "Bowl of Rice";
		}
		public BowlRice( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class BowlOfStew : Food
	{
		[Constructable]
		public BowlOfStew() : this( 1 ) { }
		[Constructable]
		public BowlOfStew( int amount ) : base( amount, 0x1604 )
		{
			this.Weight = 0.2;
			this.FillFactor = 5;
			this.Name = "Bowl of Stew";
		}
		public BowlOfStew( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PewterBowlCabbage : Food
	{
		[Constructable]
		public PewterBowlCabbage() : this( 1 ) { }
		[Constructable]
		public PewterBowlCabbage( int amount ) : base( amount, 0x9D8 )
		{
			this.Weight = 0.2;
			this.FillFactor = 1;
			this.Name = "Bowl of Cabbage";
		}
		public PewterBowlCabbage( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PewterBowlCarrot : Food
	{
		[Constructable]
		public PewterBowlCarrot() : this( 1 ) { }
		[Constructable]
		public PewterBowlCarrot( int amount ) : base( amount, 0x15FE )
		{
			this.Weight = 0.2;
			this.FillFactor = 1;
			this.Name = "Bowl of Carrot";
		}
		public PewterBowlCarrot( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PewterBowlCorn : Food
	{
		[Constructable]
		public PewterBowlCorn() : this( 1 ) { }
		[Constructable]
		public PewterBowlCorn( int amount ) : base( amount, 0x15FF )
		{
			this.Weight = 0.2;
			this.FillFactor = 1;
			this.Name = "Bowl of Corn";
		}
		public PewterBowlCorn( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PewterBowlLettuce : Food
	{
		[Constructable]
		public PewterBowlLettuce() : this( 1 ) { }
		[Constructable]
		public PewterBowlLettuce( int amount ) : base( amount, 0x1600 )
		{
			this.Weight = 0.2;
			this.FillFactor = 1;
			this.Name = "Bowl of Lettuce";
		}
		public PewterBowlLettuce( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PewterBowlPea : Food
	{
		[Constructable]
		public PewterBowlPea() : this( 1 ) { }
		[Constructable]
		public PewterBowlPea( int amount ) : base( amount, 0x1601 )
		{
			this.Weight = 0.2;
			this.FillFactor = 1;
			this.Name = "Bowl of Pea";
		}
		public PewterBowlPea( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class TomatoSoup : Food
	{
		[Constructable]
		public TomatoSoup() : this( 1 ) { }
		[Constructable]
		public TomatoSoup( int amount ) : base( amount, 0x1606 )
		{
			this.Weight = 2;
			this.FillFactor = 4;
			this.Name = "tomato soup";
		}
		public TomatoSoup( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class WoodenBowlCabbage : Food
	{
		[Constructable]
		public WoodenBowlCabbage() : this( 1 ) { }
		[Constructable]
		public WoodenBowlCabbage( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1;
			this.FillFactor = 1;
			this.Name = "Bowl of cabbage";
		}
		public WoodenBowlCabbage( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class WoodenBowlCarrot : Food
	{
		[Constructable]
		public WoodenBowlCarrot() : this( 1 ) { }
		[Constructable]
		public WoodenBowlCarrot( int amount ) : base( amount, 0x15F9 )
		{
			this.Weight = 1;
			this.FillFactor = 1;
			this.Name = "Bowl of Carrot";
		}
		public WoodenBowlCarrot( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class WoodenBowlCorn : Food
	{
		[Constructable]
		public WoodenBowlCorn() : this( 1 ) { }
		[Constructable]
		public WoodenBowlCorn( int amount ) : base( amount, 0x15FA )
		{
			this.Weight = 1;
			this.FillFactor = 1;
			this.Name = "Bowl of Corn";
		}
		public WoodenBowlCorn( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class WoodenBowlLettuce : Food
	{
		[Constructable]
		public WoodenBowlLettuce() : this( 1 ) { }
		[Constructable]
		public WoodenBowlLettuce( int amount ) : base( amount, 0x15FB )
		{
			this.Weight = 1;
			this.FillFactor = 1;
			this.Name = "Bowl of Lettuce";
		}
		public WoodenBowlLettuce( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class WoodenBowlPea : Food
	{
		[Constructable]
		public WoodenBowlPea() : this( 1 ) { }
		[Constructable]
		public WoodenBowlPea( int amount ) : base( amount, 0x15FC )
		{
			this.Weight = 1;
			this.FillFactor = 1;
			this.Name = "Bowl of Pea";
		}
		public WoodenBowlPea( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}

	public class WoodenBowlOfCarrots : Food
	{
		[Constructable]
		public WoodenBowlOfCarrots() : base( 0x15F9 )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyWoodenBowl() );
			return true;
		}

		public WoodenBowlOfCarrots( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class WoodenBowlOfCorn : Food
	{
		[Constructable]
		public WoodenBowlOfCorn() : base( 0x15FA )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyWoodenBowl() );
			return true;
		}

		public WoodenBowlOfCorn( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class WoodenBowlOfLettuce : Food
	{
		[Constructable]
		public WoodenBowlOfLettuce() : base( 0x15FB )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyWoodenBowl() );
			return true;
		}

		public WoodenBowlOfLettuce( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class WoodenBowlOfPeas : Food
	{
		[Constructable]
		public WoodenBowlOfPeas() : base( 0x15FC )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyWoodenBowl() );
			return true;
		}

		public WoodenBowlOfPeas( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PewterBowlOfCarrots : Food
	{
		[Constructable]
		public PewterBowlOfCarrots() : base( 0x15FE )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyPewterBowl() );
			return true;
		}

		public PewterBowlOfCarrots( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PewterBowlOfCorn : Food
	{
		[Constructable]
		public PewterBowlOfCorn() : base( 0x15FF )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyPewterBowl() );
			return true;
		}

		public PewterBowlOfCorn( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PewterBowlOfLettuce : Food
	{
		[Constructable]
		public PewterBowlOfLettuce() : base( 0x1600 )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyPewterBowl() );
			return true;
		}

		public PewterBowlOfLettuce( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PewterBowlOfPeas : Food
	{
		[Constructable]
		public PewterBowlOfPeas() : base( 0x1601 )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyPewterBowl() );
			return true;
		}

		public PewterBowlOfPeas( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PewterBowlOfPotatos : Food
	{
		[Constructable]
		public PewterBowlOfPotatos() : base( 0x1602 )
		{
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyPewterBowl() );
			return true;
		}

		public PewterBowlOfPotatos( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[TypeAlias( "Server.Items.EmptyLargeWoodenBowl" )]
	public class EmptyWoodenTub : Item
	{
		[Constructable]
		public EmptyWoodenTub() : base( 0x1605 )
		{
			Weight = 2.0;
		}

		public EmptyWoodenTub( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[TypeAlias( "Server.Items.EmptyLargePewterBowl" )]
	public class EmptyPewterTub : Item
	{
		[Constructable]
		public EmptyPewterTub() : base( 0x1603 )
		{
			Weight = 2.0;
		}

		public EmptyPewterTub( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class WoodenBowlOfStew : Food
	{
		[Constructable]
		public WoodenBowlOfStew() : base( 0x1604 )
		{
			Weight = 2.0;
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyWoodenTub() );
			return true;
		}

		public WoodenBowlOfStew( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class WoodenBowlOfTomatoSoup : Food
	{
		[Constructable]
		public WoodenBowlOfTomatoSoup() : base( 0x1606 )
		{
			Weight = 2.0;
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyWoodenTub() );
			return true;
		}

		public WoodenBowlOfTomatoSoup( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}