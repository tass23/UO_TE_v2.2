//
// Evil Castle v0.66
// jm (aka x-ray aka âåäüÌÛØ) 
// jm99[at]mail333.com
//

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Multis.Deeds;
using Server.Regions;
using Server.Network;
using Server.Targeting;
using Server.Accounting;
using Server.ContextMenus;
using Server.Gumps;

namespace Server.Engines.WarEvents
{
	public enum RespawnRate
	{
		Slow,
		Normal,
		Fast
	}

	public enum DefenderType
	{
		Archer,
		Mage,
		Warrior,
		Boss
	}

	public class EvilCastleSpawn : Item
	{	
		private const int doorNum = 10;

		private static int[] doffs = new int[]
		{
			0,15,6,0,	1,15,6,0,
			0,11,6,0,	1,11,6,0,
			0,5,6,0,	1,5,6,0,
			-1,-11,6,0,	0,-11,6,0,
			-8,-12,6,1, -8,-13,6,1,
			-13,-8,6,0, -12,-8,6,0,

			0,5,26,0,	1,5,26,0,
			1,-5,26,0,	2,-5,26,0,
			-8,1,26,1, -8,0,26,1,
			8,1,26,1,	8,0,26,1,
		};

		private static int[] aspawnoffs = new int[]
		{
			-13,0,26,
			0,13,26,
			13,0,26,
			1,-13,26,
			0,0,26,
			0,0,6,
		};

		private static int[] mspawnoffs = new int[]
		{
			4,13,6,
			-2,13,6,
			0,2,6,
			0,-2,6,
			0,2,26,
			0,-2,26,
			5,0,6,
			-5,0,6,
			5,0,26,
			-5,0,26,
			-1,-13,6,
		};

		private static int[] wspawnoffs = new int[]
		{
			0,9,0,
			0,13,6,
			0,-8,0,
			-1,-13,6,
			-11,-11,6,
			-11,-11,26,			
			0,2,26,
		};

		private static int[] bspawnoffs = new int[]
		{
			0,0,26,
		};

		private static int[] pspawnoffs = new int[]
		{
			0,18,
			-1,18,
			1,18,
		};

		private bool active;
		private bool complete;

		private DateTime completeTime;

		private int restartHours = 720;

		private EvilCastle castle;

		private ArrayList deco;

		private bool autorepair;

		private ArrayList doors;

		private Timer timer;

		private RespawnRate rate;
		private int rateValue = 67;

		private int kills;

		private int ticks;

		private const int maxArchersGroup = 6;
		private int maxArchersInGroup = 3;

		private ArrayList[] archers = new ArrayList[maxArchersGroup]
		{
			new ArrayList(), new ArrayList(),
			new ArrayList(), new ArrayList(),
			new ArrayList(), new ArrayList(),
		};

		private const int maxMagesGroup = 11;
		private int maxMagesInGroup = 1;

		private ArrayList[] mages = new ArrayList[maxMagesGroup]
		{
			new ArrayList(), new ArrayList(),
			new ArrayList(), new ArrayList(),
			new ArrayList(), new ArrayList(),
			new ArrayList(), new ArrayList(),
			new ArrayList(), new ArrayList(),
			new ArrayList(),
		};

		private const int maxWarriorsGroup = 7;
		private int maxWarriorsInGroup = 2;

		private ArrayList[] warriors = new ArrayList[maxWarriorsGroup]
		{
			new ArrayList(), new ArrayList(),
			new ArrayList(), new ArrayList(),
			new ArrayList(), new ArrayList(),
			new ArrayList(),
		};

		private int[] aalive = new int[maxArchersGroup];
		private int[] walive = new int[maxWarriorsGroup];
		private int[] malive = new int[maxMagesGroup];

		private Mobile boss;

		private ArrayList patrol1;
		private ArrayList patrol2;
		private ArrayList patrol3;

		private WayPoint w1;
		private WayPoint w2;
		private WayPoint w3;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get
			{
				return active;
			}
			set
			{
				if ( value )
					Start();
				else
					Stop();

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WayPoint Waypoint1
		{
			get
			{
				return w1;
			}
			set
			{
				w1 = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WayPoint Waypoint2
		{
			get
			{
				return w2;
			}
			set
			{
				w2 = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WayPoint Waypoint3
		{
			get
			{
				return w3;
			}
			set
			{
				w3 = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Complete
		{
			get
			{
				return complete;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Autorepair
		{
			get
			{
				return autorepair;
			}
			set
			{
				autorepair = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime CompletionTime
		{
			get
			{
				return completeTime;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RestartHours
		{
			get
			{
				return restartHours;
			}
			set
			{
				if (value > 0)
				{
					restartHours = value;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RateValue
		{
			get
			{
				return rateValue;
			}
			set
			{
				if (value > 0)
				{
					rateValue = value;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public RespawnRate Rate
		{
			get
			{
				return rate;
			}
			set
			{
				rate = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Kills
		{
			get
			{
				return kills;
			}
			set
			{
				kills = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxArchersInGroup
		{
			get
			{
				return maxArchersInGroup;
			}
			set
			{
				if (value > 0)
				{
					maxArchersInGroup = value;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxMagesInGroup
		{
			get
			{
				return maxMagesInGroup;
			}
			set
			{
				if (value > 0)
				{
					maxMagesInGroup = value;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxWarriorsInGroup
		{
			get
			{
				return maxWarriorsInGroup;
			}
			set
			{
				if (value > 0)
				{
					maxWarriorsInGroup = value;
				}
			}
		}

		[Constructable]
		public EvilCastleSpawn() : base( 0xBD2 )
		{
			Name = "EvilCastleSpawn";

			active = false;

			autorepair = true;

			rate = RespawnRate.Normal;

			Movable = false;
			Visible = false;

			kills = 0;

			deco = new ArrayList();

			doors = new ArrayList();

			patrol1 = new ArrayList();
			patrol2 = new ArrayList();
			patrol3 = new ArrayList();

			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( ConstructDeco ) );
		}

		public EvilCastleSpawn( Serial serial ) : base( serial )
		{
		}

		public void ConstructDeco()
		{
			if (Deleted) return;

			CreateCastle();
			CreateDoors();
			DecorateCastle();
		}

		public void CreateCastle()
		{
			castle = new EvilCastle();
			castle.Location = new Point3D( X, Y, Z );
			castle.Map = Map;
		}

		public void CreateDoors()
		{
			EvilCastleMetalDoor door1,door2;

			for(int i=0;i<doorNum;i++)
			{
				int offset = i * 8;

				door1 = new EvilCastleMetalDoor( doffs[offset+3] == 0 ? DoorFacing.WestCCW : DoorFacing.SouthCW );

				door1.Location = new Point3D(X+doffs[offset],Y+doffs[offset+1],Z+doffs[offset+2]);
				door1.Map = Map;

				doors.Add( door1 );

				door2 = new EvilCastleMetalDoor( doffs[offset+7] == 0 ? DoorFacing.EastCW : DoorFacing.NorthCCW );

				door2.Location = new Point3D(X+doffs[offset+4],Y+doffs[offset+5],Z+doffs[offset+6]);
				door2.Map = Map;

				doors.Add( door2 );

				door1.Link = door2;
				door2.Link = door1;
			}
		}

		public void DecorateCastle()
		{
			Item i;

			i = new DecoSignBody();
			i.Location = new Point3D( X+4, Y+16, Z );
			i.Map = Map;
			deco.Add( i );

			i = new DecoGuillotine();
			i.Location = new Point3D( X-5, Y-9, Z);
			i.Map = Map;
			deco.Add( i );

			i = new DecoGruesome();
			i.Location = new Point3D( X+3, Y+6, Z);
			i.Map = Map;
			deco.Add( i );

			i = new DecoGruesome();
			i.Location = new Point3D( X-2, Y+6, Z);
			i.Map = Map;
			deco.Add( i );

			for(int j=0;j<4;j++)
			{
				i = new DecoRuinedBookcase();
				i.Location = new Point3D( X+2+j, Y-4, Z + 6);
				i.Map = Map;
				deco.Add( i );
			}

			i = new DecoBloodSmear();
			i.Location = new Point3D( X+5, Y-2, Z + 6);
			i.Map = Map;
			deco.Add( i );			

			i = new DecoStoneChair();
			i.Location = new Point3D( X-6, Y+3, Z + 26);
			i.Map = Map;
			deco.Add( i );			

			for(int j=0;j<2;j++) 
			{
				i = new DecoBeefCarcass();
				i.Location = new Point3D( X-14, Y+10+j, Z + 26);
				i.Map = Map;
				deco.Add( i );			
			}

			i = new DecoBloodSmear2();
			i.Location = new Point3D( X-13, Y+11, Z + 26);
			i.Map = Map;
			deco.Add( i );			

			i = new DecoStump();
			i.Location = new Point3D( X+5, Y-8, Z);
			i.Map = Map;
			deco.Add( i );

			i = new DecoBloodSmear2();
			i.Location = new Point3D( X+5, Y-9, Z);
			i.Map = Map;
			deco.Add( i );

			for(int j=0;j<3;j++) 
			{
				i = new DecoBarrel();
				i.Location = new Point3D( X-7, Y+j, Z + 6);
				i.Map = Map;
				deco.Add( i );			
			}

			for(int j=0;j<2;j++) 
			{
				i = new DecoBarrel();
				i.Location = new Point3D( X-4, Y+1+j, Z + 6);
				i.Map = Map;
				deco.Add( i );			
			}

			i = new DecoBarrel();
			i.Location = new Point3D( X-3, Y+2, Z + 6);
			i.Map = Map;
			deco.Add( i );			

			i = new DecoStoneTable();
			i.Location = new Point3D( X+3, Y, Z + 6);
			i.Map = Map;
			deco.Add( i );		
	
			i = new DecoStoneTable2();
			i.Location = new Point3D( X+3, Y+1, Z + 6);
			i.Map = Map;
			deco.Add( i );			

			i = new DecoStoneTable3();
			i.Location = new Point3D( X+3, Y+2, Z + 6);
			i.Map = Map;
			deco.Add( i );			

			i = new DecoStoneChair2();
			i.Location = new Point3D( X+4, Y-3, Z + 26);
			i.Map = Map;
			deco.Add( i );			
		}

		public void Start()
		{
			if ( active || Deleted ) return;

			active = true;

			RepairDoors(true);

			complete = false;

			if ( timer != null ) timer.Stop();

			InitialSpawn();

			timer = new ECSliceTimer( this );

			timer.Start();
		}

		public void Stop()
		{
			if ( !active || Deleted ) return;

			active = false;

			ClearAll();

			if ( timer != null ) timer.Stop();

			timer = null;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( active )
			{
				list.Add( 1060742 ); // active
			}
			else
			{
				list.Add( 1060743 ); // inactive
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump( new PropertiesGump( from, this ) );
		}

		public void AddMobileToArray(ArrayList a,Mobile m)
		{
			if (a == null) return;

			a.Add( m );
		}

		public Mobile CreateDesiredMob(DefenderType mob)
		{
			switch(mob)
			{
			case DefenderType.Archer:
				return new ECOrcArcher();
				break;
			case DefenderType.Mage:
				return new ECOrcishMage();
				break;
			case DefenderType.Warrior:
				return new ECOrcishLord();
				break;
			case DefenderType.Boss:
				return new ECOrcishLeader(new Point3D(X + bspawnoffs[0], Y + bspawnoffs[1], Z + bspawnoffs[2]));
				break;
			}

			return null;
		}

		public void SetMobProps(Mobile m, Point3D l,bool onlyHome)
		{
			((BaseCreature)m).RangeHome = 7;
			((BaseCreature)m).Home = l;

			if (!onlyHome)
			{
				((BaseCreature)m).Location = l;
				((BaseCreature)m).Map = Map;			
			}
		}

		public void AddMobileToGroup(int group,DefenderType mob,int[] spawnoffs)
		{
			Mobile m = CreateDesiredMob(mob);
			
			if (m == null) return;

			int offset = (group*3);

			Point3D l = new Point3D(X + spawnoffs[offset] , Y + spawnoffs[offset+1], Z + spawnoffs[offset+2]);

			SetMobProps(m,l,false);

			Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
//			Effects.PlaySound( m, m.Map, 0x201 );

			switch(mob)
			{
			case DefenderType.Archer:
				AddMobileToArray(archers[group],m);				
				break;
			case DefenderType.Mage:
				AddMobileToArray(mages[group],m);				
				break;
			case DefenderType.Warrior:
				AddMobileToArray(warriors[group],m);				
				break;
			case DefenderType.Boss:
				boss = m;
				break;
			}
		}

		public void InitialSpawn()
		{
			AddMobileToGroup(0,DefenderType.Boss,bspawnoffs);

			for(int i=0;i<maxArchersGroup;i++)
			{
				for(int j=0;j<maxArchersInGroup;j++)
				{
					AddMobileToGroup(i,DefenderType.Archer,aspawnoffs);
				}
			}	

			for(int i=0;i<maxMagesGroup;i++)
			{
				for(int j=0;j<maxMagesInGroup;j++)
				{
					AddMobileToGroup(i,DefenderType.Mage,mspawnoffs);
				}
			}	

			for(int i=0;i<maxWarriorsGroup;i++)
			{
				for(int j=0;j<maxWarriorsInGroup;j++)
				{
					AddMobileToGroup(i,DefenderType.Warrior,wspawnoffs);
				}
			}	
		}

		public void ClearArray(ArrayList a)
		{
			for (int i=0;i<a.Count;i++)
			{
				BaseCreature bc = (BaseCreature)a[i];

				if (bc != null && !bc.Deleted)
				{
					bc.Delete();
				}

				a.RemoveAt(i);
				i--;
			}
		}

		public void ClearAll()
		{
			if (boss != null && !boss.Deleted)
			{
				boss.Delete();
				boss = null;
			}

			for (int i=0;i<maxArchersGroup;i++)
			{
				ClearArray(archers[i]);
			}

			for (int i=0;i<maxMagesGroup;i++)
			{
				ClearArray(mages[i]);
			}

			for (int i=0;i<maxWarriorsGroup;i++)
			{
				ClearArray(warriors[i]);
			}

			ClearArray(patrol1);
			ClearArray(patrol2);
			ClearArray(patrol3);
		}

		public override void OnDelete()
		{
			Stop();

			base.OnDelete();
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if (w1 != null) DeleteAllWayPoints(w1);
			if (w2 != null) DeleteAllWayPoints(w2);
			if (w3 != null) DeleteAllWayPoints(w3);

			if ( castle != null ) castle.Delete();

			if (deco != null)
			{
				for (int i=0;i<deco.Count;i++)
				{
            		Item it = (Item)deco[i];

					if (!it.Deleted)
					{
						it.Delete();
					}

					deco.RemoveAt(i);
					i--;
				}
			}

			if ( doors != null )
			{
				for (int i=0;i<doors.Count;i++)
				{
					((BaseDoor)doors[i]).Delete();				
					doors.RemoveAt(i);
					i--;
				}
			}
		}

		public void RepairThisDoor(EvilCastleMetalDoor d)
		{
			if (d.breached)
			{
				d.breachTryCount--;

				if (d.breachTryCount == 0)
				{
					if (((EvilCastleMetalDoor)d.Link).breachTryCount == 0)					
					{
						d.breached = false;
						((EvilCastleMetalDoor)d.Link).breached = false;
					}
				}
			}
		}

		public void RepairDoors(bool fullrepair)
		{
			foreach(EvilCastleMetalDoor d in doors)
			{
				if (fullrepair)
				{
					d.breachTryCount = 0;
					d.breached = false;
				}
				else
				{
					RepairThisDoor(d);
				}
			}			
		}

		public bool isCompleted()
		{
			if (boss != null && boss.Alive)
			{
				return false;
			}
			else
			{
				if (complete)
				{
					if (completeTime + TimeSpan.FromHours( restartHours ) < DateTime.Now)
					{
						complete = false;

						ClearAll();

						RepairDoors(true);

						InitialSpawn();

						return false;
					}
				}
				else
				{
					foreach(EvilCastleMetalDoor d in doors)
					{
						d.breached = true;
					}			

					complete = true;
				
					completeTime = DateTime.Now;
				}

				return true;
			}				
		}

		public int GetRateValue()
		{
			switch(rate)
			{
			case RespawnRate.Slow:
				return 190;
			case RespawnRate.Normal:
				return 120;
			case RespawnRate.Fast:
				return 50;
			}

			return 0;
		}

		public void IncKills()
		{
			if (kills < rateValue)
			{
				kills++;
			}
		}

		public int CountAlive(ArrayList a,bool countKills)
		{
			int number = 0;

			for(int i=0;i<a.Count;i++)
			{
				Mobile m = (Mobile)a[i];

				if (m != null && m.Alive && !m.Deleted)				
				{
					number++;
				}
				else
				{
					if (countKills) IncKills();

					a.RemoveAt(i);
					i--;
				}
			}

			return number;
		}

		public void CountAllAlive()
		{
			for (int i=0;i<maxArchersGroup;i++)
			{
				aalive[i] = CountAlive(archers[i],true);
			}

			for (int i=0;i<maxMagesGroup;i++)
			{
				malive[i] = CountAlive(mages[i],true);
			}

			for (int i=0;i<maxWarriorsGroup;i++)
			{
				walive[i] = CountAlive(warriors[i],true);
			}
		}

		public void RespawnGroup()
		{
			for (int i=0;i<maxArchersGroup;i++)
			{
				if (aalive[i] < maxArchersInGroup)
				{
					AddMobileToGroup(i,DefenderType.Archer,aspawnoffs);					
				}
			}

			for (int i=0;i<maxMagesGroup;i++)
			{
				if (malive[i] < maxMagesInGroup)
				{
					AddMobileToGroup(i,DefenderType.Mage,mspawnoffs);					
				}
			}

			for (int i=0;i<maxWarriorsGroup;i++)
			{
				if (walive[i] < maxWarriorsInGroup)
				{
					AddMobileToGroup(i,DefenderType.Warrior,wspawnoffs);					
				}
			}
		}

		public bool RespawnPatrolN(int j,WayPoint w,ArrayList a)
		{
			Mobile m;

			if (w != null)
			{
				int alive = CountAlive(a,false);

				if (alive == 0)
				{
//					System.Console.WriteLine("DEBUG: Spawning patrol {0} !",j);

					Point3D l;

					int offset = j*2;

					int z = Map.GetAverageZ( X + pspawnoffs[offset] , Y + pspawnoffs[offset+1] );

					if ( Map.CanFit( X + + pspawnoffs[offset], Y + pspawnoffs[offset+1], z, 16, false, false ) )
					{
						l = new Point3D(X + pspawnoffs[offset] , Y + pspawnoffs[offset+1], z);
					}
					else
					{
						return false;
					}

					Effects.SendLocationParticles( EffectItem.Create( l, Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );

					for (int i=0;i<maxArchersInGroup;i++)
					{
						m = CreateDesiredMob(DefenderType.Archer);

						if (m != null)
						{
							SetMobProps(m,l,false);
							a.Add( m );
						}
					}
	
					for (int i=0;i<maxWarriorsInGroup;i++)
					{
						m = CreateDesiredMob(DefenderType.Warrior);

						if (m != null)
						{
							SetMobProps(m,l,false);
							a.Add( m );
						}
					}	

					for (int i=0;i<maxMagesInGroup;i++)
					{
						m = CreateDesiredMob(DefenderType.Mage);

						if (m != null)
						{
							SetMobProps(m,l,false);
							a.Add( m );
						}
					}

					return true;
				}
			}

			return false;
		}

		public void RespawnPatrol()
		{
			if (RespawnPatrolN(0,w1,patrol1))
			{
				Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerCallback( PutOnPatrol1 ) );
			}
			
			if (RespawnPatrolN(1,w2,patrol2))
			{
				Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerCallback( PutOnPatrol2 ) );
			}

			if (RespawnPatrolN(2,w3,patrol3))
			{
				Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerCallback( PutOnPatrol3 ) );
			}
		}

		public void PutOnPatrolGroup(WayPoint w,ArrayList a)
		{
			if (w == null) return;

			foreach(Mobile m in a)
			{
				if (!m.Deleted)
				{
					WayPoint wl = FindLastWaypoint(w);					

					if (wl != null)
					{
						Point3D l = new Point3D( wl.Location );
						SetMobProps(m,l,true);
					}				

					((BaseCreature)m).CurrentWayPoint = w;				
				}
			}
		}

		public void PutOnPatrol1()
		{
			PutOnPatrolGroup(w1,patrol1);		
		}

		public void PutOnPatrol2()
		{
			PutOnPatrolGroup(w2,patrol2);		
		}

		public void PutOnPatrol3()
		{
			PutOnPatrolGroup(w3,patrol3);
		}

		public void Respawn()
		{
			if (Deleted) return;

			CountAllAlive();

			int respawntime = GetRateValue() + (int)((float)GetRateValue()*((float)kills/(float)rateValue));

//			System.Console.WriteLine("DEBUG: {0}/{1}",ticks,respawntime);			

			if (ticks > respawntime)
			{
				RespawnGroup();

				if (autorepair) RepairDoors(false);				

				if (kills == 0) RespawnPatrol();

				ticks = 0;

				if (kills > 0) kills--;
			}

			ticks++;
		}

		public void RemoveCorpses()
		{
			if (Deleted) return;

			ArrayList corpses = new ArrayList();

			IPooledEnumerable eable = Map.GetItemsInBounds( new Rectangle2D(X-30,Y-30,60,60) );

			foreach(Item item in eable)
			{
				if (item != null && item is Corpse && !item.Deleted)
				{
					corpses.Add( item );									
				}
			}

			foreach(Corpse c in corpses)
			{
				if ( c != null && !(c.Owner is PlayerMobile) && !(c.Owner is ECOrcishLeader) )
				{
				//	c.Delete();
				}
			}
		}

		public WayPoint FindLastWaypoint(WayPoint waypoint)
		{
			ArrayList t = new ArrayList();

			WayPoint w = waypoint;

			while (w != null && !w.Deleted)
			{
				if (w.Map != Map) break;

				if (w.NextPoint == null || w.NextPoint.Deleted) return w;

				foreach(WayPoint wpt in t)
				{
					if (w.NextPoint == wpt)
					{
						return w;
					}
				}

				t.Add(w);

				w = w.NextPoint;
			}

			return null;
		}

		public void DeleteAllWayPoints(WayPoint waypoint)
		{
			while (waypoint != null && !waypoint.Deleted)
			{
				WayPoint t = waypoint;

				waypoint = waypoint.NextPoint;

				t.Delete();
			}
		}

		public void OnSlice()
		{
			RemoveCorpses();

			if (isCompleted()) return;

			Respawn();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); //version

			writer.Write( (bool) active );
			writer.Write( (bool) autorepair );
			writer.Write( (bool) complete );
			writer.Write( (DateTime) completeTime );
			writer.Write( (int)kills );
			writer.Write( (int)maxArchersInGroup );
			writer.Write( (int)maxMagesInGroup );
			writer.Write( (int)maxWarriorsInGroup );
			writer.Write( (int)rate );
			writer.Write( (int)rateValue );
			writer.Write( (int)restartHours );
			writer.Write( (Item)w1 );
			writer.Write( (Item)w2 );
			writer.Write( (Item)w3 );

			writer.WriteItemList( doors );

			writer.WriteItemList( deco );

			writer.Write( (Mobile)boss );

			for (int i=0;i<maxArchersGroup;i++)
			{
				writer.WriteMobileList(archers[i]);
			}

			for (int i=0;i<maxMagesGroup;i++)
			{
				writer.WriteMobileList(mages[i]);
			}

			for (int i=0;i<maxWarriorsGroup;i++)
			{
				writer.WriteMobileList(warriors[i]);
			}

			writer.WriteMobileList(patrol1);
			writer.WriteMobileList(patrol2);
			writer.WriteMobileList(patrol3);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			active = reader.ReadBool();
			autorepair = reader.ReadBool();
			complete = reader.ReadBool();
			completeTime = reader.ReadDateTime();
			kills = reader.ReadInt();
			maxArchersInGroup = reader.ReadInt();
			maxMagesInGroup = reader.ReadInt();
			maxWarriorsInGroup = reader.ReadInt();
			rate = (RespawnRate)reader.ReadInt();
			rateValue = reader.ReadInt();
			restartHours = reader.ReadInt();
			w1 = reader.ReadItem() as WayPoint;
			w2 = reader.ReadItem() as WayPoint;
			w3 = reader.ReadItem() as WayPoint;

			doors = reader.ReadItemList();

			deco = reader.ReadItemList();

			boss = reader.ReadMobile();

			for (int i=0;i<maxArchersGroup;i++)
			{
				archers[i] = reader.ReadMobileList();
			}

			for (int i=0;i<maxMagesGroup;i++)
			{
				mages[i] = reader.ReadMobileList();
			}

			for (int i=0;i<maxWarriorsGroup;i++)
			{
				warriors[i] = reader.ReadMobileList();
			}

			patrol1 = reader.ReadMobileList();
			patrol2 = reader.ReadMobileList();
			patrol3 = reader.ReadMobileList();

			timer = new ECSliceTimer( this );

			timer.Start();
		}
	}
}
