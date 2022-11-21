using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class CookableFood : Item
	{
		private int m_CookingLevel;

		[CommandProperty( AccessLevel.GameMaster )]
		public int CookingLevel { get { return m_CookingLevel; } set { m_CookingLevel = value; } }

		public CookableFood( int itemID, int cookingLevel ) : base( itemID ) { m_CookingLevel = cookingLevel; }

		public CookableFood( Serial serial ) : base( serial ) { }

		public abstract Food Cook();

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_CookingLevel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_CookingLevel = reader.ReadInt();
		}

//#if false
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable ) return;
			from.Target = new InternalTarget( this );
		}
//#endif

		public static bool IsHeatSource( object targeted )
		{
			int itemID;
			if ( targeted is Item ) itemID = ((Item)targeted).ItemID & 0x3FFF;
			else if ( targeted is StaticTarget ) itemID = ((StaticTarget)targeted).ItemID & 0x3FFF;
			else return false;
			if ( itemID >= 0xDE3 && itemID <= 0xDE9 ) return true;
			else if ( itemID >= 0x461 && itemID <= 0x48E ) return true;
			else if ( itemID >= 0x92B && itemID <= 0x96C ) return true;
			else if ( itemID == 0xFAC ) return true;
			else if ( itemID == 11739 ) return true;
			else if ( itemID == 11740 ) return true;
			else if ( itemID >= 0x184A && itemID <= 0x184C ) return true;
			else if ( itemID >= 0x184E && itemID <= 0x1850 ) return true;
			else if ( itemID >= 0x398C && itemID <= 0x399F ) return true;
			return false;
		}

		private class InternalTarget : Target
		{
			private CookableFood m_Item;
			public InternalTarget( CookableFood item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;
				if ( CookableFood.IsHeatSource( targeted ) )
				{
					if ( from.BeginAction( typeof( CookableFood ) ) )
					{
						from.PlaySound( 0x225 );
						m_Item.Consume();
						InternalTimer t = new InternalTimer( from, targeted as IPoint3D, from.Map, m_Item );
						t.Start();
					}
					else from.SendLocalizedMessage( 500119 );
				}
			}

			private class InternalTimer : Timer
			{
				private Mobile m_From;
				private IPoint3D m_Point;
				private Map m_Map;
				private CookableFood m_CookableFood;

				public InternalTimer( Mobile from, IPoint3D p, Map map, CookableFood cookableFood ) : base( TimeSpan.FromSeconds( 5.0 ) )
				{
					m_From = from;
					m_Point = p;
					m_Map = map;
					m_CookableFood = cookableFood;
				}

				protected override void OnTick()
				{
					m_From.EndAction( typeof( CookableFood ) );
					if ( m_From.Map != m_Map || (m_Point != null && m_From.GetDistanceToSqrt( m_Point ) > 3) )
					{
						m_From.SendLocalizedMessage( 500686 );
						return;
					}
					if ( m_From.CheckSkill( SkillName.Cooking, m_CookableFood.CookingLevel, 100 ) )
					{
						Food cookedFood = m_CookableFood.Cook();
						if ( m_From.AddToBackpack( cookedFood ) ) m_From.PlaySound( 0x57 );
					}
					else m_From.SendLocalizedMessage( 500686 );
				}
			}
		}
	}

	public class RawRibs : CookableFood
	{
		[Constructable]
		public RawRibs() : this( 1 ){}

		[Constructable]
		public RawRibs( int amount ) : base( 0x9F1, 10 )
		{
			Stackable = true;
			Amount = amount;
		}

		public RawRibs( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new Ribs(); }
	}

	public class RawLambLeg : CookableFood
	{
		[Constructable]
		public RawLambLeg() : this( 1 ){}

		[Constructable]
		public RawLambLeg( int amount ) : base( 0x1609, 10 )
		{
			Stackable = true;
			Amount = amount;
		}

		public RawLambLeg( Serial serial ) : base(serial){}
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int) 0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

		public override Food Cook() { return new LambLeg(); }
	}

	public class RawChickenLeg : CookableFood
	{
		[Constructable]
		public RawChickenLeg() : this( 1 ){}

		[Constructable]
		public RawChickenLeg(int amount) : base( 0x1607, 10 )
		{
			Stackable = true;
			Amount = amount;
		}

		public RawChickenLeg( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new ChickenLeg(); }
	}

	public class RawBird : CookableFood
	{
		[Constructable]
		public RawBird() : this( 1 ){}

		[Constructable]
		public RawBird( int amount ) : base( 0x9B9, 10 )
		{
			Stackable = true;
			Amount = amount;
		}

		public RawBird( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new CookedBird(); }
	}

	public class UnbakedPeachCobbler : CookableFood
	{
		public override int LabelNumber{ get{ return 1041335; } }

		[Constructable]
		public UnbakedPeachCobbler() : base( 0x1042, 25 )
		{
		}

		public UnbakedPeachCobbler( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new PeachCobbler(); }
	}

	public class UnbakedFruitPie : CookableFood
	{
		public override int LabelNumber{ get{ return 1041334; } }

		[Constructable]
		public UnbakedFruitPie() : base( 0x1042, 25 )
		{
		}

		public UnbakedFruitPie( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new FruitPie(); }
	}

	public class UnbakedMeatPie : CookableFood
	{
		public override int LabelNumber{ get{ return 1041338; } }

		[Constructable]
		public UnbakedMeatPie() : base( 0x1042, 25 )
		{
		}

		public UnbakedMeatPie( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new MeatPie(); }
	}

	public class UnbakedPumpkinPie : CookableFood
	{
		public override int LabelNumber{ get{ return 1041342; } }

		[Constructable]
		public UnbakedPumpkinPie() : base( 0x1042, 25 )
		{
		}

		public UnbakedPumpkinPie( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new PumpkinPie(); }
	}

	public class UnbakedApplePie : CookableFood
	{
		public override int LabelNumber{ get{ return 1041336; } }

		[Constructable]
		public UnbakedApplePie() : base( 0x1042, 25 )
		{
		}

		public UnbakedApplePie( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new ApplePie(); }
	}

	public class PizzaCrust : CookableFood
	{
		[Constructable]
		public PizzaCrust() : base( 0x1083, 20 )
		{
		      	Weight = 0.5;
			Name = "Pizza Crust";
			Hue = 0x3FF;
		}
		public PizzaCrust( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
		public override Food Cook() { return new CheesePizza(); }
	}

	[TypeAlias( "Server.Items.UncookedPizza" )]
	public class UncookedCheesePizza : CookableFood
	{
		public override int LabelNumber{ get{ return 1041341; } }

		[Constructable]
		public UncookedCheesePizza() : base( 0x1083, 20 )
		{
		}

		public UncookedCheesePizza( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

		public override Food Cook() { return new CheesePizza(); }
	}

	public class UncookedSausagePizza : CookableFood
	{
		public override int LabelNumber{ get{ return 1041337; } }

		[Constructable]
		public UncookedSausagePizza() : base( 0x1083, 20 )
		{
		}

		public UncookedSausagePizza( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new SausagePizza(); }
	}

#if false

	public class UncookedPizza : CookableFood
	{
		[Constructable]
		public UncookedPizza() : base( 0x1083, 20 )
		{
		}

		public UncookedPizza( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

		public override Food Cook() { return new Pizza(); }
	}
#endif

	public class UnbakedQuiche : CookableFood
	{
		public override int LabelNumber{ get{ return 1041339; } }

		[Constructable]
		public UnbakedQuiche() : base( 0x1042, 25 )
		{
		}

		public UnbakedQuiche( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new Quiche(); }
	}

	public class Eggs : CookableFood
	{
		[Constructable]
		public Eggs() : this( 1 ){}

		[Constructable]
		public Eggs( int amount ) : base( 0x9B5, 15 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Eggs( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

		public override Food Cook() { return new FriedEggs(); }
	}

	public class BrightlyColoredEggs : CookableFood
	{
		public override string DefaultName
		{
			get { return "brightly colored eggs"; }
		}

		[Constructable]
		public BrightlyColoredEggs() : base( 0x9B5, 15 )
		{
			Weight = 0.5;
			Hue = 3 + (Utility.Random( 20 ) * 5);
		}

		public BrightlyColoredEggs( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new FriedEggs(); }
	}

	public class EasterEggs : CookableFood
	{
		public override int LabelNumber{ get{ return 1016105; } }

		[Constructable]
		public EasterEggs() : this(1) {}

		[Constructable]
		public EasterEggs(int amount) : base( 0x9B5, 15 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 0.5;
			Hue = 3 + (Utility.Random( 20 ) * 5);
		}

		public EasterEggs( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new FriedEggs(); }
	}

	public class CookieMix : CookableFood
	{
		[Constructable]
		public CookieMix() : base( 0x103F, 20 )
		{
		}

		public CookieMix( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new Cookies(); }
	}

	public class CakeMix : CookableFood
	{
		public override int LabelNumber{ get{ return 1041002; } }

		[Constructable]
		public CakeMix() : base( 0x103F, 40 )
		{
		}

		public CakeMix( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook() { return new Cake(); }
	}

	public class RawFishSteak : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawFishSteak() : this( 1 ){}

		[Constructable]
		public RawFishSteak( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
		}

		public RawFishSteak( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new FishSteak();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class RawHalibutSteak : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawHalibutSteak() : this( 1 ){}

		[Constructable]
		public RawHalibutSteak( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
			Name = "Raw Halibut Steak";
		}

		public RawHalibutSteak( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new HalibutFishSteak();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class RawFlukeSteak : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawFlukeSteak() : this( 1 ){}

		[Constructable]
		public RawFlukeSteak( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
			Name = "Raw Fluke Steak";
		}

		public RawFlukeSteak( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new FlukeFishSteak();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class RawMahiSteak : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawMahiSteak() : this( 1 ){}

		[Constructable]
		public RawMahiSteak( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
			Name = "Raw Mahi-Mahi Steak";
		}

		public RawMahiSteak( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new MahiFishSteak();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class RawSalmonSteak : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawSalmonSteak() : this( 1 ){}

		[Constructable]
		public RawSalmonSteak( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
			Name = "Raw Salmon Steak";
		}

		public RawSalmonSteak( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new SalmonFishSteak();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class RawRedSnapperSteak : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawRedSnapperSteak() : this( 1 ){}

		[Constructable]
		public RawRedSnapperSteak( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
			Name = "Raw Red Snapper Steak";
		}

		public RawRedSnapperSteak( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new RedSnapperFishSteak();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class RawParrotFishSteak : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawParrotFishSteak() : this( 1 ){}

		[Constructable]
		public RawParrotFishSteak( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
			Name = "Raw Parrot Fish Steak";
		}

		public RawParrotFishSteak( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new ParrotFishSteak();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class RawTroutSteak : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawTroutSteak() : this( 1 ){}

		[Constructable]
		public RawTroutSteak( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
			Name = "Raw Trout Steak";
		}

		public RawTroutSteak( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new TroutFishSteak();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class RawShrimp : CookableFood
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RawShrimp() : this( 1 ){}

		[Constructable]
		public RawShrimp( int amount ) : base( 0x097A, 10 )
		{
			Stackable = true;
			Amount = amount;
			Name = "Raw Shrimp";
		}

		public RawShrimp( Serial serial ) : base( serial ) { }

		public override Food Cook()
		{
			return new CookedShrimp();
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}
