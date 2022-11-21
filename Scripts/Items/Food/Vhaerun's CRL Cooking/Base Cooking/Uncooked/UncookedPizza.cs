using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class UncookedPizza : Item
	{
		public override int LabelNumber{ get{ return 1024227; } }

		private Food m_CookedFood{ get{ return new Pizza( this.Desc, this.Hue ); } }

		private int m_MinSkill;
		private int m_MaxSkill;

		private string m_Desc;

		public string Desc {
			get { return m_Desc; }
			set {
				m_Desc = value;
				Name = "uncooked " + m_Desc + " pizza";
				InvalidateProperties();
			}
		}

		public Food CookedFood{ get{ return m_CookedFood; } }

		public int MinSkill { get{ return m_MinSkill; } }
		public int MaxSkill { get{ return m_MaxSkill; } }

		[Constructable]
		public UncookedPizza() : this( "cheese" )
		{
		}

		[Constructable]
		public UncookedPizza( string desc ) : this( desc, 0 )
		{
		}

		[Constructable]
		public UncookedPizza( int Color ) : this( "cheese", Color )
		{
		}

		[Constructable]
		public UncookedPizza( string desc, int Color ) : base( 0x1083 )
		{
			if ( Color != 0 )
				Hue = Color;

			if ( desc != "" && desc != null )
			{
				Desc = desc;
			}

			m_MinSkill = 0;
			m_MaxSkill = 100;
		}

		public UncookedPizza( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( (string)m_Desc );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
					{
						m_Desc = reader.ReadString();
						break;
					}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new InternalTarget( this );
		}

		public class InternalTarget : Target
		{
			private UncookedPizza m_Item;

			public InternalTarget( UncookedPizza item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			public static bool IsHeatSource( object targeted )
			{
				int itemID;

				if ( targeted is Item )
					itemID = ((Item)targeted).ItemID & 0x3FFF;
				else if ( targeted is StaticTarget )
				itemID = ((StaticTarget)targeted).ItemID & 0x3FFF;
				else
					return false;

				if ( itemID >= 0xDE3 && itemID <= 0xDE9 )
					return true;
					else if ( itemID >= 0x461 && itemID <= 0x48E )
					return true;
					else if ( itemID >= 0x92B && itemID <= 0x96C )
					return true;
					else if ( itemID == 0xFAC )
					return true;
					else if ( itemID >= 0x398C && itemID <= 0x399F )
					return true;
					else if ( itemID == 0xFB1 )
					return true;
					else if ( itemID >= 0x197A && itemID <= 0x19A9 )
					return true;
					else if ( itemID >= 0x184A && itemID <= 0x184C )
					return true;
				else if ( itemID >= 0x184E && itemID <= 0x1850 )
				return true;

				return false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

				if ( IsHeatSource(targeted) )
				{
					if ( from.BeginAction( typeof( Item ) ) )
					{
						from.PlaySound( 0x225 );

						m_Item.Consume();

						InternalTimer t = new InternalTimer( from, targeted as IPoint3D, from.Map, m_Item.MinSkill, m_Item.MaxSkill, m_Item.CookedFood );
						t.Start();
					}
					else
					{
						from.SendLocalizedMessage( 500119 );
					}
				}
				else if ( m_Item.Desc.Length > 80 )
				{
					from.SendMessage("The pizza has enough toppings already.");
					return;
				}
				else if ( targeted is Apple )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add apple to the pizza.");
					m_Item.Desc += ", apple";
					((Item)targeted).Consume();
				}
				else if ( targeted is Banana || targeted is Bananas )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add banana to the pizza.");
					m_Item.Desc += ", banana";
					((Item)targeted).Consume();
				}
				else if ( targeted is Cantaloupe )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add cantaloupe to the pizza.");
					m_Item.Desc += ", cantaloupe";
					((Item)targeted).Consume();
				}

				else if ( targeted is Coconut || targeted is SplitCoconut )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add coconut to the pizza.");
					m_Item.Desc += ", coconut";
					((Item)targeted).Consume();
				}
				else if ( targeted is Cucumber )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add cucumber to the pizza.");
					m_Item.Desc += ", cucumber";
					((Item)targeted).Consume();
				}
				else if ( targeted is Dates )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add dates to the pizza.");
					m_Item.Desc += ", date";
					((Item)targeted).Consume();
				}
				else if ( targeted is Grapes )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add grape to the pizza.");
					m_Item.Desc += ", grape";
					((Item)targeted).Consume();
				}
				else if ( targeted is HoneydewMelon )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add honeydew melon to the pizza.");
					m_Item.Desc += ", honeydew melon";
					((Item)targeted).Consume();
				}
				else if ( targeted is Lemon )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add lemon to the pizza.");
					m_Item.Desc += ", lemon";
					((Item)targeted).Consume();
				}
				else if ( targeted is Lime )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add lime to the pizza.");
					m_Item.Desc += ", lime";
					((Item)targeted).Consume();
				}
				else if ( targeted is Orange )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add orange to the pizza.");
					m_Item.Desc += ", orange";
					((Item)targeted).Consume();
				}
				else if ( targeted is Peach )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add peach to the pizza.");
					m_Item.Desc += ", peach";
					((Item)targeted).Consume();
				}
				else if ( targeted is Pear )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add pear to the pizza.");
					m_Item.Desc += ", pear";
					((Item)targeted).Consume();
				}
				else if ( targeted is Pumpkin )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add pumpkin to the pizza.");
					m_Item.Desc += ", pumpkin";
					((Item)targeted).Consume();
				}
				else if ( targeted is Tomato )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add tomato to the pizza.");
					m_Item.Desc += ", tomato";
					((Item)targeted).Consume();
				}
				else if ( targeted is Watermelon )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add watermelon to the pizza.");
					m_Item.Desc += ", watermelon";
					((Item)targeted).Consume();
				}

				else if ( targeted is RawBacon || targeted is Bacon )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add bacon to the pizza.");
					m_Item.Desc += ", bacon";
					((Item)targeted).Consume();
				}
				else if ( targeted is ChickenLeg || targeted is RawChickenLeg )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add chicken meat to the pizza.");
					m_Item.Desc += ", chicken";
					((Item)targeted).Consume();
				}
				else if ( targeted is CookedBird || targeted is RawBird )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add bird meat to the pizza.");
					m_Item.Desc += ", bird";
					((Item)targeted).Consume();
				}
				else if ( targeted is FishSteak || targeted is RawFishSteak )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add fish to the pizza.");
					m_Item.Desc += ", fish";
					((Item)targeted).Consume();
				}
				else if ( targeted is RawHamSlices || targeted is HamSlices )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add ham to the pizza.");
					m_Item.Desc += ", ham";
					((Item)targeted).Consume();
				}
				else if ( targeted is LambLeg || targeted is RawLambLeg )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add lamb meat to the pizza.");
					m_Item.Desc += ", lamb";
					((Item)targeted).Consume();
				}
				else if ( targeted is Ribs || targeted is RawRibs )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add ribs to the pizza.");
					m_Item.Desc += ", ribs";
					((Item)targeted).Consume();
				}
				else if ( targeted is Sausage )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add sausage to the pizza.");
					m_Item.Desc += ", sausage";
					((Item)targeted).Consume();
				}

				else if ( targeted is Dough )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You make the pizza a thick crust one.");
					m_Item.Desc += ", thick crust";
					((Item)targeted).Consume();
				}
				else if ( targeted is TanMushroom || targeted is RedMushroom )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add mushrooms to the pizza.");
					m_Item.Desc += ", mushrooms";
					((Item)targeted).Consume();
				}
				else if ( targeted is Silverleaf )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add silverleaf to the pizza.");
					m_Item.Desc += ", silverleaf";
					((Item)targeted).Consume();
				}
				else if ( targeted is Spam )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add spam to the pizza.");
					m_Item.Desc += ", spam";
					((Item)targeted).Consume();
				}

				else if ( targeted is Garlic )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add garlic to the pizza.");
					m_Item.Desc += ", garlic";
					((Item)targeted).Consume();
				}
				else if ( targeted is Ginseng )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add ginseng to the pizza.");
					m_Item.Desc += ", ginseng";
					((Item)targeted).Consume();
				}

				else if ( targeted is Cabbage )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add cabbage to the pizza.");
					m_Item.Desc += ", cabbage";
					((Item)targeted).Consume();
				}
				else if ( targeted is Carrot )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add carrot to the pizza.");
					m_Item.Desc += ", carrot";
					((Item)targeted).Consume();
				}
				else if ( targeted is Corn )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add corn to the pizza.");
					m_Item.Desc += ", corn";
					((Item)targeted).Consume();
				}
				else if ( targeted is Lettuce )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add lettuce to the pizza.");
					m_Item.Desc += ", lettuce";
					((Item)targeted).Consume();
				}
				else if ( targeted is Onion )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add onion to the pizza.");
					m_Item.Desc += ", onion";
					((Item)targeted).Consume();
				}
				else if ( targeted is Turnip )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add turnip to the pizza.");
					m_Item.Desc += ", turnip";
					((Item)targeted).Consume();
				}

				else if ( targeted is BrightEggs || targeted is EasterEggs )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add an easter egg to the pizza!");
					m_Item.Desc += ", surprise!";
					m_Item.Hue = ((Item)targeted).Hue;
					from.AddToBackpack( new Eggshells( m_Item.Hue ) );
					((Item)targeted).Consume();
				}
				else if ( targeted is Eggs )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add egg to the pizza.");
					m_Item.Desc += ", egg";
					from.AddToBackpack( new Eggshells( m_Item.Hue ) );
					((Item)targeted).Consume();
				}
				else if ( targeted is FriedEggs )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add egg to the pizza.");
					m_Item.Desc += ", egg";
					((Item)targeted).Consume();
				}
				else if ( targeted is FishHeads )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add fish heads to the pizza.");
					m_Item.Desc += ", fish head";
					((Item)targeted).Consume();
				}
				else if ( targeted is CheeseWedgeSmall )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add extra cheese to the pizza.");
					m_Item.Desc += ", extra cheese";
					((Item)targeted).Consume();
				}
				else if ( targeted is RedRaspberry || targeted is BlackRaspberry )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add raspberries to the pizza.");
					m_Item.Desc += ", raspberry";
					((Item)targeted).Consume();
				}
				else if ( targeted is Strawberries )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add strawberries to the pizza.");
					m_Item.Desc += ", strawberry";
					((Item)targeted).Consume();
				}

				else if ( targeted is RawHam || targeted is Ham || targeted is RawBaconSlab || targeted is BaconSlab || targeted is CheeseWheel || targeted is CheeseWedge )
				{
					from.SendMessage("That portion is too large. Use a bladed object to cut it up first.");
					return;
				}

try {
				/*if ( targeted is FromageDeChevre || targeted is FromageDeChevreWedge || targeted is FromageDeBrebis || targeted is FromageDeBrebisWedge || targeted is FromageDeVache || targeted is FromageDeVacheWedge )
				{
					from.SendMessage("That portion is too large. Use a bladed object to cut it up first.");
					return;
				}
				else if ( targeted is FromageDeVacheWedgeSmall )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add extra cheese to the pizza.");
					m_Item.Desc += ", extra cheese";
					((Item)targeted).Consume();
				}
				else if ( targeted is FromageDeChevreWedgeSmall )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add goat cheese to the pizza.");
					m_Item.Desc += ", goat cheese";
					((Item)targeted).Consume();
				}
				else if ( targeted is FromageDeBrebisWedgeSmall )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You add sheep cheese to the pizza.");
					m_Item.Desc += ", sheep cheese";
					((Item)targeted).Consume();
				}*/
}
catch
{
}
			}

			private class InternalTimer : Timer
			{
				private Mobile m_From;
				private IPoint3D m_Point;
				private Map m_Map;
				private int Min;
				private int Max;
				private Food m_CookedFood;

				public InternalTimer( Mobile from, IPoint3D p, Map map, int min, int max, Food cookedFood ) : base( TimeSpan.FromSeconds( 3.0 ) )
				{
					m_From = from;
					m_Point = p;
					m_Map = map;
					Min = min;
					Max = max;
					m_CookedFood = cookedFood;
				}

				protected override void OnTick()
				{
					m_From.EndAction( typeof( Item ) );

					if ( m_From.Map != m_Map || (m_Point != null && m_From.GetDistanceToSqrt( m_Point ) > 3) )
					{
						m_From.SendLocalizedMessage( 500686 );
						return;
					}

					if ( m_From.CheckSkill( SkillName.Cooking, Min, Max ) )
					{
						if ( m_From.AddToBackpack( m_CookedFood ) )
							m_From.PlaySound( 0x57 );
					}
					else
					{
						m_From.PlaySound( 0x57 );
						m_From.SendLocalizedMessage( 500686 );
					}
				}
			}
		}
	}
}
