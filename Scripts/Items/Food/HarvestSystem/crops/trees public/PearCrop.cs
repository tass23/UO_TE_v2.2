using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items.Crops
{
	public class PearSapling : BaseCrop
	{
		public Timer thisTimer;
		public DateTime treeTime;

		[CommandProperty( AccessLevel.GameMaster )]
		public String FullGrown{ get{ return treeTime.ToString( "T" ); } }

		[Constructable]
		public PearSapling() : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "pear tree sapling";

			init( this );
		}

		public static void init( PearSapling plant )
		{
			TimeSpan delay = TreeHelper.SaplingTime;
			plant.treeTime = DateTime.Now + delay;

			plant.thisTimer = new TreeHelper.TreeTimer( plant, typeof(PearTree), delay );
			plant.thisTimer.Start();
		}

		public PearSapling( Serial serial ) : base( serial ){}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			init( this );
		}
	}

	public class PearTree : BaseTree
	{
		public Item i_trunk;
		private Timer chopTimer;

		private const int max = 12;
		private DateTime lastpicked;
		private int m_yield;

		public Timer regrowTimer;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Yield{ get{ return m_yield; } set{ m_yield = value; } }

		public int Capacity{ get{ return max; } }
		public DateTime LastPick{ get{ return lastpicked; } set{ lastpicked = value; } }

		[Constructable]
		public PearTree( Point3D pnt, Map map ) : base( Utility.RandomList( 0xDAA, 0xDA6 ) )
		{
			Movable = false;
			MoveToWorld( pnt, map );

			int trunkID = ((Item)this).ItemID - 2;

			i_trunk = new TreeTrunk( trunkID, this );
			i_trunk.MoveToWorld( pnt, map );

			init( this, false );
		}

		public static void init ( PearTree plant, bool full )
		{
			plant.LastPick = DateTime.Now;
			plant.regrowTimer = new FruitTimer( plant );

			if ( full )
			{
				plant.Yield = plant.Capacity;
			}
			else
			{
				plant.Yield = 0;
				plant.regrowTimer.Start();
			}
		}

		public override void OnAfterDelete()
		{
			if (( i_trunk != null ) && ( !i_trunk.Deleted ))
					i_trunk.Delete();

			base.OnAfterDelete();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Mounted && !TreeHelper.CanPickMounted )
			{
				from.SendMessage( "You cannot pick fruit while mounted." );
				return;
			}

			if ( DateTime.Now > lastpicked.AddSeconds(3) )
			{
				lastpicked = DateTime.Now;

				int cookValue = (int)from.Skills[SkillName.Cooking].Value / 30;
				if ( from.Mounted )
					++cookValue;

				if ( cookValue < 0 )
				{
					from.SendMessage( "You have no idea how to pick this fruit." );
					return;
				}

				if ( from.InRange( this.GetWorldLocation(), 2 ) )
				{
					if ( m_yield < 1 )
					{
						from.SendMessage( "There is nothing here to harvest." );
					}
					else
					{
						from.Direction = from.GetDirectionTo( this );

						from.Animate( from.Mounted ? 26:17, 7, 1, true, false, 0 );

						if ( cookValue < m_yield )
							cookValue = m_yield + 1;

						int pick = Utility.Random( cookValue );
						if ( pick == 0 )
						{
							from.SendMessage( "You do not manage to gather any fruit." );
							return;
						}

						m_yield -= pick;
						from.SendMessage( "You pick {0} pear{1}!", pick, ( pick == 1 ? "" : "s" ) );

						Pear crop = new Pear( pick );
						from.AddToBackpack( crop );

						if ( !regrowTimer.Running )
						{
							regrowTimer.Start();
						}
					}
				}
				else
				{
					from.SendLocalizedMessage( 500446 );
				}
			}
		}

		private class FruitTimer : Timer
		{
			private PearTree i_plant;

			public FruitTimer( PearTree plant ) : base( TimeSpan.FromSeconds( 900 ), TimeSpan.FromSeconds( 30 ) )
			{
				Priority = TimerPriority.OneSecond;
				i_plant = plant;
			}

			protected override void OnTick()
			{
				if ( ( i_plant != null ) && ( !i_plant.Deleted ) )
				{
					int current = i_plant.Yield;

					if ( ++current >= i_plant.Capacity )
					{
						current = i_plant.Capacity;
						Stop();
					}
					else if ( current <= 0 )
						current = 1;

					i_plant.Yield = current;

				}
				else Stop();
			}
		}

		public void Chop( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( ( chopTimer == null ) || ( !chopTimer.Running ) )
				{
					if ( ( TreeHelper.TreeOrdinance ) && ( from.AccessLevel == AccessLevel.Player ) )
					{
						if ( from.Region is Regions.GuardedRegion )
							from.CriminalAction( true );
					}

					chopTimer = new TreeHelper.ChopAction( from );

					Point3D pnt = this.Location;
					Map map =  this.Map;

					from.Direction = from.GetDirectionTo( this );
					chopTimer.Start();

					double cookValue = from.Skills[SkillName.Lumberjacking].Value / 100;
					if ( ( cookValue > .5 ) && ( Utility.RandomDouble() <= cookValue ) )
					{
						Pear fruit = new Pear( (int)Utility.Random( 13 ) + m_yield );
						from.AddToBackpack( fruit );

						int cnt = Utility.Random( (int)( cookValue * 10 ) + 1 );
						Log logs = new Log( cnt );
						from.AddToBackpack( logs );

						FruitTreeStump i_stump = new FruitTreeStump( typeof( PearTree ) );
						Timer poof = new StumpTimer( this, i_stump, from );
						poof.Start();
					}
					else from.SendLocalizedMessage( 500495 );
				}
			}
			else from.SendLocalizedMessage( 500446 );
		}

		private class StumpTimer : Timer
		{
			private PearTree i_tree;
			private FruitTreeStump i_stump;
			private Mobile m_chopper;

			public StumpTimer( PearTree Tree, FruitTreeStump Stump, Mobile from ) : base( TimeSpan.FromMilliseconds( 5500 ) )
			{
				Priority = TimerPriority.TenMS;

				i_tree = Tree;
				i_stump = Stump;
				m_chopper = from;
			}

			protected override void OnTick()
			{
				i_stump.MoveToWorld( i_tree.Location, i_tree.Map );
				i_tree.Delete();
				m_chopper.SendMessage( "You put some logs and fruit into your backpack" );
			}
		}

		public override void OnChop( Mobile from )
		{
			if ( !this.Deleted )
					Chop( from );
		}

		public PearTree( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (Item)i_trunk );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			Item item = reader.ReadItem();
			if ( item != null )
				i_trunk = item;

			init( this, true );
		}
	}
}