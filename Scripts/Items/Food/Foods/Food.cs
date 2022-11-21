using System;
using System.Collections;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public abstract class Food : Item
	{
		private Mobile m_Poisoner;
		private Poison m_Poison;
		private int m_FillFactor;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Poisoner
		{
			get { return m_Poisoner; }
			set { m_Poisoner = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Poison Poison
		{
			get { return m_Poison; }
			set { m_Poison = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int FillFactor
		{
			get { return m_FillFactor; }
			set { m_FillFactor = value; }
		}

		public Food( int itemID ) : this( 1, itemID ){}

		public Food( int amount, int itemID ) : base( itemID )
		{
			Stackable = true;
			Amount = amount;
			m_FillFactor = 1;
		}

		public Food( Serial serial ) : base( serial ) { }

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
				list.Add( new ContextMenus.EatEntry( from, this ) );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				Eat( from );
			}
		}

		public virtual bool Eat( Mobile from )
		{
			if ( FillHunger( from, m_FillFactor ) )
			{
				from.PlaySound( Utility.Random( 0x3A, 3 ) );
				if ( from.Body.IsHuman && !from.Mounted ) from.Animate( 34, 5, 1, true, false, 0 );
				if ( m_Poison != null ) from.ApplyPoison( m_Poisoner, m_Poison );
				Consume();
				return true;
			}

			return false;
		}

		static public bool FillHunger( Mobile from, int fillFactor )
		{
			if ( from.Hunger >= 20 )
			{
				from.SendLocalizedMessage( 500871 );
				return true;
			}

			int iHunger = from.Hunger + fillFactor;
			if ( from.Stam < from.StamMax ) from.Stam += Utility.Random( 6, 3 ) + fillFactor/5;
			if ( iHunger >= 20 )
			{
				from.Hunger = 20;
				from.SendLocalizedMessage( 500872 );
			}
			else
			{
				from.Hunger = iHunger;
				if ( iHunger < 5 ) from.SendLocalizedMessage( 500868 );
				else if ( iHunger < 10 ) from.SendLocalizedMessage( 500869 );
				else if ( iHunger < 15 ) from.SendLocalizedMessage( 500870 );
				else from.SendLocalizedMessage( 500871 );
			}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 4 );
			writer.Write( m_Poisoner );
			Poison.Serialize( m_Poison, writer );
			writer.Write( m_FillFactor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 1:
				{
					switch ( reader.ReadInt() )
					{
						case 0: m_Poison = null; break;
						case 1: m_Poison = Poison.Lesser; break;
						case 2: m_Poison = Poison.Regular; break;
						case 3: m_Poison = Poison.Greater; break;
						case 4: m_Poison = Poison.Deadly; break;
					}

					break;
				}
				case 2:
				{
					m_Poison = Poison.Deserialize( reader );
					break;
				}
				case 3:
				{
					m_Poison = Poison.Deserialize( reader );
					m_FillFactor = reader.ReadInt();
					break;
				}
				case 4:
				{
					m_Poisoner = reader.ReadMobile();
					goto case 3;
				}
			}
		}
	}

	public class BreadLoaf : Food
	{
		[Constructable]
		public BreadLoaf() : this( 1 ){}

		[Constructable]
		public BreadLoaf( int amount ) : base( amount, 0x103B )
		{
			this.Weight = 1.0;
			this.FillFactor = 3;
		}

		public BreadLoaf( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Bacon : Food
	{
		[Constructable]
		public Bacon() : this( 1 ){}

		[Constructable]
		public Bacon( int amount ) : base( amount, 0x979 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Bacon( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class SlabOfBacon : Food
	{
		[Constructable]
		public SlabOfBacon() : this( 1 ){}

		[Constructable]
		public SlabOfBacon( int amount ) : base( amount, 0x976 )
		{
			this.Weight = 1.0;
			this.FillFactor = 3;
		}

		public SlabOfBacon( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class FishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public FishSteak() : this( 1 ){}

		[Constructable]
		public FishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
		}

		public FishSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class HalibutFishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public HalibutFishSteak() : this( 1 ){}

		[Constructable]
		public HalibutFishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
			Name = "Halibut Fish Steak";
		}

		public HalibutFishSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class FlukeFishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public FlukeFishSteak() : this( 1 ){}

		[Constructable]
		public FlukeFishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
			Name = "Fluke Fish Steak";
		}

		public FlukeFishSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class MahiFishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public MahiFishSteak() : this( 1 ){}

		[Constructable]
		public MahiFishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
			Name = "Mahi Fish Steak";
		}

		public MahiFishSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class SalmonFishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public SalmonFishSteak() : this( 1 ){}

		[Constructable]
		public SalmonFishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
			Name = "Salmon Fish Steak";
		}

		public SalmonFishSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class RedSnapperFishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RedSnapperFishSteak() : this( 1 ){}

		[Constructable]
		public RedSnapperFishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
			Name = "Red Snapper Fish Steak";
		}

		public RedSnapperFishSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class ParrotFishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public ParrotFishSteak() : this( 1 ){}

		[Constructable]
		public ParrotFishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
			Name = "Parrot Fish Fish Steak";
		}

		public ParrotFishSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class TroutFishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public TroutFishSteak() : this( 1 ){}

		[Constructable]
		public TroutFishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
			Name = "Trout Fish Steak";
		}

		public TroutFishSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
	public class CookedShrimp : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public CookedShrimp() : this( 1 ){}

		[Constructable]
		public CookedShrimp( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
			Name = "Cooked Shrimp";
		}

		public CookedShrimp( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class CheeseSlice : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public CheeseSlice() : this( 1 ){}

		[Constructable]
		public CheeseSlice( int amount ) : base( amount, 0x97C )
		{
			this.FillFactor = 1;
		}

		public CheeseSlice( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class FrenchBread : Food
	{
		[Constructable]
		public FrenchBread() : this( 1 ){}

		[Constructable]
		public FrenchBread( int amount ) : base( amount, 0x98C )
		{
			this.Weight = 2.0;
			this.FillFactor = 3;
		}

		public FrenchBread( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class CookedBird : Food
	{
		[Constructable]
		public CookedBird() : this( 1 ){}

		[Constructable]
		public CookedBird( int amount ) : base( amount, 0x9B7 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public CookedBird( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class RoastPig : Food
	{
		[Constructable]
		public RoastPig() : this( 1 ){}

		[Constructable]
		public RoastPig( int amount ) : base( amount, 0x9BB )
		{
			this.Weight = 45.0;
			this.FillFactor = 20;
		}

		public RoastPig( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Sausage : Food
	{
		[Constructable]
		public Sausage() : this( 1 ){}

		[Constructable]
		public Sausage( int amount ) : base( amount, 0x9C0 )
		{
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public Sausage( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Ham : Food
	{
		[Constructable]
		public Ham() : this( 1 ){}

		[Constructable]
		public Ham( int amount ) : base( amount, 0x9C9 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public Ham( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Cake : Food
	{
		[Constructable]
		public Cake() : base( 0x9E9 )
		{
			this.Weight = 1.0;
			this.FillFactor = 10;
		}

		public Cake( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Ribs : Food
	{
		[Constructable]
		public Ribs() : this( 1 ){}

		[Constructable]
		public Ribs( int amount ) : base( amount, 0x9F2 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public Ribs( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Cookies : Food
	{
		[Constructable]
		public Cookies() : base( 0x160b )
		{
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public Cookies( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Muffins : Food
	{
		[Constructable]
		public Muffins() : base( 0x9eb )
		{
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public Muffins( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class FruitPie : Food
	{
		public override int LabelNumber{ get{ return 1041346; } }

		[Constructable]
		public FruitPie() : base( 0x1041 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public FruitPie( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MeatPie : Food
	{
		public override int LabelNumber{ get{ return 1041347; } }

		[Constructable]
		public MeatPie() : base( 0x1041 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public MeatPie( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PumpkinPie : Food
	{
		public override int LabelNumber{ get{ return 1041348; } }

		[Constructable]
		public PumpkinPie() : base( 0x1041 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public PumpkinPie( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class ApplePie : Food
	{
		public override int LabelNumber{ get{ return 1041343; } }

		[Constructable]
		public ApplePie() : base( 0x1041 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public ApplePie( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PeachCobbler : Food
	{
		public override int LabelNumber{ get{ return 1041344; } }

		[Constructable]
		public PeachCobbler() : base( 0x1041 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public PeachCobbler( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Quiche : Food
	{
		public override int LabelNumber{ get{ return 1041345; } }

		[Constructable]
		public Quiche() : base( 0x1041 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public Quiche( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class LambLeg : Food
	{
		[Constructable]
		public LambLeg() : this( 1 ){}

		[Constructable]
		public LambLeg( int amount ) : base( amount, 0x160a )
		{
			this.Weight = 2.0;
			this.FillFactor = 5;
		}

		public LambLeg( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class ChickenLeg : Food
	{
		[Constructable]
		public ChickenLeg() : this( 1 ){}

		[Constructable]
		public ChickenLeg( int amount ) : base( amount, 0x1608 )
		{
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public ChickenLeg( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[FlipableAttribute( 0xC74, 0xC75 )]
	public class HoneydewMelon : Food
	{
		[Constructable]
		public HoneydewMelon() : this( 1 ){}

		[Constructable]
		public HoneydewMelon( int amount ) : base( amount, 0xC74 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public HoneydewMelon( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[FlipableAttribute( 0xC64, 0xC65 )]
	public class YellowGourd : Food
	{
		[Constructable]
		public YellowGourd() : this( 1 ){}

		[Constructable]
		public YellowGourd( int amount ) : base( amount, 0xC64 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public YellowGourd( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[FlipableAttribute( 0xC66, 0xC67 )]
	public class GreenGourd : Food
	{
		[Constructable]
		public GreenGourd() : this( 1 ){}

		[Constructable]
		public GreenGourd( int amount ) : base( amount, 0xC66 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public GreenGourd( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[FlipableAttribute( 0xC7F, 0xC81 )]
	public class EarOfCorn : Food
	{
		[Constructable]
		public EarOfCorn() : this( 1 ){}

		[Constructable]
		public EarOfCorn( int amount ) : base( amount, 0xC81 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public EarOfCorn( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class Turnip : Food
	{
		[Constructable]
		public Turnip() : this( 1 ){}

		[Constructable]
		public Turnip( int amount ) : base( amount, 0xD3A )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Turnip( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class SheafOfHay : Item
	{
		[Constructable]
		public SheafOfHay() : base( 0xF36 )
		{
			this.Weight = 10.0;
		}

		public SheafOfHay( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}