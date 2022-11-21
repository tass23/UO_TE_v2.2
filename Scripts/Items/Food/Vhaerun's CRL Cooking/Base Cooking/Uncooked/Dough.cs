using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Dough : Item
	{
		private int m_MinSkill;
		private int m_MaxSkill;
		private Food m_CookedFood;

		public int MinSkill { get{ return m_MinSkill; } }
		public int MaxSkill { get{ return m_MaxSkill; } }
		public Food CookedFood{ get{ return m_CookedFood; } }

		[Constructable]
		public Dough() : base( 0x103d )
		{
			m_MinSkill = 0;
			m_MaxSkill = 10;
			m_CookedFood = new BreadLoaf();
		}

		public Dough( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private Dough m_Item;

			public InternalTarget( Dough item ) : base( 1, false, TargetFlags.None )
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
				else if ( targeted is Eggs )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UnbakedQuiche().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UnbakedQuiche() );
					if( m_Item.Parent == null )
						new Eggshells( m_Item.Hue ).MoveToWorld(m_Item.Location, (m_Item.Map ));
					else
						from.AddToBackpack( new Eggshells( m_Item.Hue ) );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an unbaked quiche");
				}
				else if ( targeted is CheeseWedgeSmall )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UncookedPizza( "cheese" ).MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UncookedPizza( "cheese" ) );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an uncooked cheese pizza");
				}
				else if ( targeted is JarHoney )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new SweetDough().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new SweetDough() );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made a sweet dough");
				}
				else if ( targeted is ChickenLeg || targeted is RawChickenLeg )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new SweetDough().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UnbakedChickenPotPie() );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made a chicken pot pie");
				}
				else if ( targeted is Apple )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UnbakedApplePie().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UnbakedApplePie() );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an unbaked apple pie");
				}
				else if ( targeted is Peach )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UnbakedPeachCobbler().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UnbakedPeachCobbler() );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an unbaked peach cobbler");
				}
				else if ( targeted is Pumpkin )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UnbakedPumpkinPie().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UnbakedPumpkinPie() );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an unbaked pumpkin pie");
				}
				else if ( targeted is Lime )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UnbakedKeyLimePie().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UnbakedKeyLimePie() );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an unbaked key lime pie");
				}
				else if ( targeted is Dough )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UncookedFrenchBread().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UncookedFrenchBread() );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You ... add some more dough onto the dough");
				}
				else if ( targeted is UncookedFrenchBread )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UncookedDonuts().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UncookedDonuts() );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You fumble around for a bit with even more dough, and eventually make these round doughy things");
				}

try {
				/*if ( targeted is FromageDeVacheWedgeSmall )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UncookedPizza( "cheese" ).MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UncookedPizza( "cheese" ) );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an uncooked cheese pizza");
				}
				else if ( targeted is FromageDeBrebisWedgeSmall )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UncookedPizza( "sheep cheese" ).MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UncookedPizza( "sheep cheese" ) );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an uncooked sheep cheese pizza");
				}
				else if ( targeted is FromageDeChevreWedgeSmall )
				{
					if(!((Item)targeted).Movable) return;
					if(((Item)targeted).Parent == null )
						new UncookedPizza( "goat cheese" ).MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map );
					else
						from.AddToBackpack( new UncookedPizza( "goat cheese" ) );
					m_Item.Consume();
					((Item)targeted).Consume();
					from.SendMessage("You made an uncooked goat cheese pizza");
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

				public InternalTimer( Mobile from, IPoint3D p, Map map, int min, int max, Food cookedFood ) : base( TimeSpan.FromSeconds( 1.0 ) )
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
						m_From.SendLocalizedMessage( 500686 );
					}
				}
			}
		}
	}
}