using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps;
using Server.Regions;
using Server.Engines.Underworld;
using Server.Commands;

namespace Server.Engines.Underworld
{
	public class UnderworldSystem : Item
	{
		private int m_CurrentStage;
		private int m_RegularKillCount;
		private Mobile m_CurrentBoss;
		private ArrayList m_Spawns;
		private ArrayList m_SpawnNodes;
		private ArrayList m_Gates;
		private int m_LastSpawnsCount;

		[CommandProperty( AccessLevel.GameMaster )]
		public int CurrentStage
		{
			get{ return m_CurrentStage; }
			set{ m_CurrentStage = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RegularKillCount
		{
			get{ return m_RegularKillCount; }
			set{ m_RegularKillCount = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile CurrentBoss
		{
			get{ return m_CurrentBoss; }
			set{ m_CurrentBoss = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ArrayList Spawns
		{
			get{ return m_Spawns; }
			set{ m_Spawns = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ArrayList SpawnNodes
		{
			get{ return m_Spawns; }
			set{ m_Spawns = value; }
		}

		private Map m_UnderworldMap = Map.Malas;
		private Point3D m_HadesSpawnPoint = new Point3D( 1319, 1961, -95 );
		private Point3D m_ThanatosSpawnPoint = new Point3D( 1181, 1803, -90 );
		private Point3D m_HecateSpawnPoint = new Point3D( 1153, 1803, -90 );
		private Point3D m_PyriphlegethonSpawnPoint = new Point3D( 1065, 1780, -85 );
		private Point3D m_LetheSpawnPoint = new Point3D( 924, 1800, -55 );
		private Point3D m_CocytusSpawnPoint = new Point3D( 788, 1822, -85 );
		private Point3D m_CerberusSpawnPoint = new Point3D( 609, 1836, -80 );
		private Point3D m_StyxSpawnPoint = new Point3D( 615, 1864, -90 );
		private Point3D m_AcheronSpawnPoint = new Point3D( 638, 1862, -90 );
		private Point3D m_StyxGatePoint = new Point3D( 609, 1836, -80 );
		private Point3D m_CocytusGatePoint = new Point3D( 788, 1822, -85 );
		private Point3D m_LetheGatePoint = new Point3D( 924, 1800, -55 );
		private Point3D m_PyriphlegethonGatePoint = new Point3D( 1065, 1780, -85 );
		private Point3D m_TartarusGatePoint = new Point3D( 1168, 1790, -90 );
		private Point3D m_CharonSpawnPoint = new Point3D( 610, 1922, -91 );

		[Constructable]
		public UnderworldSystem() : base( 128 )
		{
			m_LastSpawnsCount = 0;
			m_RegularKillCount = 0;
			m_CurrentStage = 1;
			m_Spawns = new ArrayList();
			m_SpawnNodes = new ArrayList();
			m_Gates = new ArrayList();
			this.Name = "Underworld Controller";
			this.Movable = false;
			this.Visible = false;
			GenerateSpawnNodes();
			Charon charon = new Charon();
			charon.MoveToWorld( m_CharonSpawnPoint, m_UnderworldMap );
			new InternalTimer( this ).Start();
		}

		public override void Delete()
		{
			WipeSpawnNodes();

			if ( this.m_CurrentBoss != null )
				this.m_CurrentBoss.Delete();

			WipeGates();
			base.Delete();
		}

		public void GenerateSpawnNodes()
		{
			UnderworldSpawnNode node = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node );

			node.MoveToWorld( new Point3D( 608, 1890, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node1 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node1 );

			node1.MoveToWorld( new Point3D( 590, 1886, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node2 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node2 );

			node2.MoveToWorld( new Point3D( 585, 1868, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node3 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node3 );

			node3.MoveToWorld( new Point3D( 618, 1874, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node4 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node4 );

			node4.MoveToWorld( new Point3D( 641, 1867, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node5 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node5 );

			node5.MoveToWorld( new Point3D( 641, 1845, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node6 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node6 );

			node6.MoveToWorld( new Point3D( 625, 1855, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node7 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node7 );

			node7.MoveToWorld( new Point3D( 594, 1863, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node8 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node8 );

			node8.MoveToWorld( new Point3D( 585, 1849, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node9 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node9 );

			node9.MoveToWorld( new Point3D( 589, 1838, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node10 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node10 );

			node10.MoveToWorld( new Point3D( 601, 1838, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node11 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node11 );

			node11.MoveToWorld( new Point3D( 636, 1838, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node12 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node12 );

			node12.MoveToWorld( new Point3D( 617, 1860, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node13 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node13 );

			node13.MoveToWorld( new Point3D( 623, 1886, -90 ), this.m_UnderworldMap );
			UnderworldSpawnNode node14 = new UnderworldSpawnNode( this );
			this.m_SpawnNodes.Add( node14 );

			node14.MoveToWorld( new Point3D( 604, 1885, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode );

			cnode.MoveToWorld( new Point3D( 820, 1866, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode1 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode1 );

			cnode1.MoveToWorld( new Point3D( 814, 1866, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode2 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode2 );

			cnode2.MoveToWorld( new Point3D( 817, 1869, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode3 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode3 );

			cnode3.MoveToWorld( new Point3D( 760, 1865, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode4 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode4 );

			cnode4.MoveToWorld( new Point3D( 766, 1865, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode5 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode5 );

			cnode5.MoveToWorld( new Point3D( 763, 1868, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode6 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode6 );

			cnode6.MoveToWorld( new Point3D( 787, 1887, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode7 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode7 );

			cnode7.MoveToWorld( new Point3D( 792, 1887, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode8 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode8 );

			cnode8.MoveToWorld( new Point3D( 787, 1891, -90 ), this.m_UnderworldMap );
			CocytusSpawnNode cnode9 = new CocytusSpawnNode( this );
			this.m_SpawnNodes.Add( cnode9 );

			cnode9.MoveToWorld( new Point3D( 782, 1891, -90 ), this.m_UnderworldMap );
			LetheSpawnNode lnode = new LetheSpawnNode( this );
			this.m_SpawnNodes.Add( lnode );

			lnode.MoveToWorld( new Point3D( 886, 1831, -90 ), this.m_UnderworldMap );
			LetheSpawnNode lnode1 = new LetheSpawnNode( this );
			this.m_SpawnNodes.Add( lnode1 );

			lnode1.MoveToWorld( new Point3D( 886, 1833, -90 ), this.m_UnderworldMap );
			LetheSpawnNode lnode2 = new LetheSpawnNode( this );
			this.m_SpawnNodes.Add( lnode2 );

			lnode2.MoveToWorld( new Point3D( 966, 1831, -90 ), this.m_UnderworldMap );
			LetheSpawnNode lnode3 = new LetheSpawnNode( this );
			this.m_SpawnNodes.Add( lnode3 );

			lnode3.MoveToWorld( new Point3D( 966, 1833, -90 ), this.m_UnderworldMap );
			LetheSpawnNode lnode4 = new LetheSpawnNode( this );
			this.m_SpawnNodes.Add( lnode4 );

			lnode4.MoveToWorld( new Point3D( 947, 1854, -90 ), this.m_UnderworldMap );
			LetheSpawnNode lnode5 = new LetheSpawnNode( this );
			this.m_SpawnNodes.Add( lnode5 );

			lnode5.MoveToWorld( new Point3D( 928, 1854, -90 ), this.m_UnderworldMap );
			LetheSpawnNode lnode6 = new LetheSpawnNode( this );
			this.m_SpawnNodes.Add( lnode6 );

			lnode6.MoveToWorld( new Point3D( 926, 1854, -90 ), this.m_UnderworldMap );
			LetheSpawnNode lnode7 = new LetheSpawnNode( this );
			this.m_SpawnNodes.Add( lnode7 );

			lnode7.MoveToWorld( new Point3D( 908, 1854, -90 ), this.m_UnderworldMap );
		}

		public void RegenSpawnNodes()
		{
			if ( m_SpawnNodes != null && m_SpawnNodes.Count > 0 && this.m_CurrentStage == 1 )
			{
				for( int i = 0; i < m_SpawnNodes.Count; i++ )
				{
					Item it = (Item)m_SpawnNodes[i];
					it.Delete();
				}
			}
			GenerateSpawnNodes();
		}

		public void WipeSpawnNodes()
		{
			if ( m_SpawnNodes != null && m_SpawnNodes.Count > 0 )
			{
				for( int i = 0; i < m_SpawnNodes.Count; i++ )
				{
					Item it = (Item)m_SpawnNodes[i];
					it.Delete();
				}
			}
		}

		public void WipeRegSpawnNodes()
		{
			if ( m_SpawnNodes != null && m_SpawnNodes.Count > 0 )
			{
				for( int i = 0; i < m_SpawnNodes.Count; i++ )
				{
					Item it = (Item)m_SpawnNodes[i];

					if ( it is UnderworldSpawnNode )
						it.Delete();
				}
			}
		}

		public void WipeCocytusSpawnNodes()
		{
			if ( m_SpawnNodes != null && m_SpawnNodes.Count > 0 )
			{
				for( int i = 0; i < m_SpawnNodes.Count; i++ )
				{
					Item it = (Item)m_SpawnNodes[i];

					if ( it is CocytusSpawnNode )
						it.Delete();
				}
			}
		}

		public void WipeLetheSpawnNodes()
		{
			if ( m_SpawnNodes != null && m_SpawnNodes.Count > 0 )
			{
				for( int i = 0; i < m_SpawnNodes.Count; i++ )
				{
					Item it = (Item)m_SpawnNodes[i];

					if ( it is LetheSpawnNode )
						it.Delete();
				}
			}
		}

		public void WipeGates()
		{
			if ( m_Gates != null && m_Gates.Count > 0 )
			{
				for( int i = 0; i < m_Gates.Count; i++ )
				{
					Item it = (Item)m_Gates[i];
					it.Delete();
				}
			}
		}
		
		public void PerformCheck()
		{
			if ( this.m_CurrentStage == 1 && this.m_RegularKillCount >= 175 )
			{
				this.m_CurrentStage = 2;
				this.WipeRegSpawnNodes();
				World.Broadcast( 1150, true, "The Underworld Gauntlet has begun!" );
				Acheron ach = new Acheron();
				ach.Home = m_AcheronSpawnPoint;
				ach.RangeHome = 1;
				ach.MoveToWorld( m_AcheronSpawnPoint, m_UnderworldMap );
				m_CurrentBoss = ach;
			}
			else if (this.m_RegularKillCount <= 174)
			{ 
				var region = Region.Find(this.Location, m_UnderworldMap);
				foreach(Mobile player in region.GetPlayers())
                {
                    if (player is PlayerMobile)
                    {
                        //Console.WriteLine("{0} was in the Region.", player.Name);
						player.CloseGump( typeof( GUnderworldGump));
                        player.SendGump(new GUnderworldGump((PlayerMobile)player, this));
                    }
                }
			}
			else
			{
				CheckBoss();
			}
		}

		public void CheckBoss()
		{
			if ( this.m_CurrentBoss != null )
			{
				if ( this.m_CurrentBoss is Acheron && !this.m_CurrentBoss.Alive )
				{
					Styx styx = new Styx();
					this.m_CurrentBoss = styx;
					styx.Home = m_StyxSpawnPoint;
					styx.MoveToWorld( m_StyxSpawnPoint, m_UnderworldMap );
					styx.RangeHome = 1;
					World.Broadcast( 1150, true, "Acheron has fallen. The River Guardian Styx has come forth!" );

				}
				else if ( this.m_CurrentBoss is Styx && !this.m_CurrentBoss.Alive )
				{
					Cerberus cerb = new Cerberus();
					this.m_CurrentBoss = cerb;
					cerb.Home = m_CerberusSpawnPoint;
					cerb.MoveToWorld( m_CerberusSpawnPoint, m_UnderworldMap );
					cerb.RangeHome = 1;
					World.Broadcast( 1150, true, "Styx has fallen. Cerberus has come forth!" );
				}
				else if ( this.m_CurrentBoss is Cerberus && !this.m_CurrentBoss.Alive )
				{
					Cocytus coc = new Cocytus();
					this.m_CurrentBoss = coc;
					coc.Home = m_CocytusSpawnPoint;
					coc.MoveToWorld( m_CocytusSpawnPoint, m_UnderworldMap );
					coc.RangeHome = 1;
					World.Broadcast( 1150, true, "Cerberus has fallen. The River Guardian Cocytus has come forth!" );
					Moongate gate = new Moongate();
					gate.Dispellable = false;
					gate.Hue = 1765;
					gate.Target = new Point3D( 791, 1913, -90 );
					gate.TargetMap = m_UnderworldMap;
					gate.MoveToWorld( m_StyxGatePoint, m_UnderworldMap );
					m_Gates.Add( gate );
				}
				else if ( this.m_CurrentBoss is Cocytus && !this.m_CurrentBoss.Alive )
				{
					WipeCocytusSpawnNodes();
					Lethe leth = new Lethe();
					this.m_CurrentBoss = leth;
					leth.Home = m_LetheSpawnPoint;
					leth.MoveToWorld( m_LetheSpawnPoint, m_UnderworldMap );
					leth.RangeHome = 1;
					World.Broadcast( 1150, true, "Cocytus has fallen. The River Guardian Lethe has come forth!" );
					Moongate gate = new Moongate();
					gate.Dispellable = false;
					gate.Hue = 1194;
					gate.Target = new Point3D( 927, 1878, -90 );
					gate.TargetMap = m_UnderworldMap;
					gate.MoveToWorld( m_CocytusGatePoint, m_UnderworldMap );
					m_Gates.Add( gate );
				}
				else if ( this.m_CurrentBoss is Lethe && !this.m_CurrentBoss.Alive )
				{
					WipeLetheSpawnNodes();
					Pyriphlegethon leth = new Pyriphlegethon();
					this.m_CurrentBoss = leth;
					leth.Home = m_PyriphlegethonSpawnPoint;
					leth.MoveToWorld( m_PyriphlegethonSpawnPoint, m_UnderworldMap );
					leth.RangeHome = 1;
					World.Broadcast( 1150, true, "Lethe has fallen. The River Guardian Pyriphlegethon has come forth!" );
					Moongate gate = new Moongate();
					gate.Dispellable = false;
					gate.Hue = 1161;
					gate.Target = new Point3D( 1065, 1823, -90 );
					gate.TargetMap = m_UnderworldMap;
					gate.MoveToWorld( m_LetheGatePoint, m_UnderworldMap );
					m_Gates.Add( gate );
				}
				else if ( this.m_CurrentBoss is Pyriphlegethon && !this.m_CurrentBoss.Alive )
				{
					Thanatos leth = new Thanatos();
					this.m_CurrentBoss = leth;
					leth.Home = m_ThanatosSpawnPoint;
					leth.MoveToWorld( m_ThanatosSpawnPoint, m_UnderworldMap );
					leth.RangeHome = 1;
					World.Broadcast( 1150, true, "Pyriphlegethon has fallen. Thanatos, the God of Death has come forth!" );
					Moongate gate = new Moongate();
					gate.Dispellable = false;
					gate.Target = new Point3D( 1167, 1815, -90 );
					gate.Hue = 1760;
					gate.TargetMap = m_UnderworldMap;
					gate.MoveToWorld( m_PyriphlegethonGatePoint, m_UnderworldMap );
					m_Gates.Add( gate );
				}
				else if ( this.m_CurrentBoss is Thanatos && !this.m_CurrentBoss.Alive )
				{
					Hecate leth = new Hecate();
					this.m_CurrentBoss = leth;
					leth.Home = m_HecateSpawnPoint;
					leth.MoveToWorld( m_HecateSpawnPoint, m_UnderworldMap );
					leth.RangeHome = 1;
					World.Broadcast( 1150, true, "Thanatos has fallen. Hecate, Goddess of Necromancy and Witchcraft has come forth!" );
				}
				else if ( this.m_CurrentBoss is Hecate && !this.m_CurrentBoss.Alive )
				{
					Hades leth = new Hades();
					this.m_CurrentBoss = leth;
					leth.Home = m_HadesSpawnPoint;
					leth.MoveToWorld( m_HadesSpawnPoint, m_UnderworldMap );
					leth.RangeHome = 1;
					World.Broadcast( 1150, true, "Hecate has fallen. The Gate to the Palace of Hades is open!" );
					Moongate gate = new Moongate();
					gate.Dispellable = false;
					gate.Target = new Point3D( 1318, 1991, -95 );
					gate.TargetMap = m_UnderworldMap;
					gate.Hue = 1758;
					gate.MoveToWorld( m_TartarusGatePoint, m_UnderworldMap );
					m_Gates.Add( gate );
				}
				else if ( this.m_CurrentBoss is Hades && !this.m_CurrentBoss.Alive )
				{
					World.Broadcast( 1150, true, "Hades has fallen. The Underworld Gauntlet is completed!" );
					WipeGates();
					this.GenerateSpawnNodes();
					this.m_RegularKillCount = 0;
					this.m_CurrentStage = 1;
					this.m_CurrentBoss = null;
				}
			}
		}

		public void DoRandomDrops( Point3D p )
		{
			ArrayList alist = new ArrayList();
			IPooledEnumerable eable = m_UnderworldMap.GetMobilesInRange( p, 10 );

			foreach( Mobile m in eable )
				alist.Add( m );

			eable.Free();

			if ( alist != null && alist.Count > 0 )
			{
				for( int i = 0; i < alist.Count; i++ )
				{
					Mobile m = (Mobile)alist[i];

					if ( m is PlayerMobile )
					{
						int dropchance = Utility.Random( 1, 10000);
						dropchance -= GetLuckBonus( m );
						if ( dropchance <= 1 )
							GiveDrop( m );
					}
				}
			}
		}

		public int GetLuckBonus( Mobile m )
		{
			int luck = m.Luck;
			if ( luck > 0 )
				luck = luck / 10;
			if ( luck > 500 )
				luck = 500;
			return luck;
		}

		public void GiveDrop( Mobile m )
		{
			int chance = Utility.Random( 1, 10000 );
			if ( chance <= 7 )
			{
				m.AddToBackpack( new ShadowCrystal() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 14 )
			{
				m.AddToBackpack( new ShadowSpike() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 21 )
			{
				m.AddToBackpack( new ShadowBox() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 28 )
			{
				m.AddToBackpack( new HellstoneObelisk() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 35 )
			{
				m.AddToBackpack( new DarkDemonStatue() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 42 )
			{
				m.AddToBackpack( new DarkBoneTable() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 49 )
			{
				m.AddToBackpack( new BoneThrone() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 56 )
			{
				m.AddToBackpack( new DaishoOfTheDamned() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 69 )
			{
				m.AddToBackpack( new FountainOfBlood() );
				m.SendMessage( 0, "You receive a special Decoration item!" );
			}
			else if ( chance <= 78 )
			{
				m.AddToBackpack( new MiniCoffin() );
				m.SendMessage( 0, "You receive a special Item!" );
			}
			else if ( chance <= 83 )
			{
				m.AddToBackpack( new HotHearthstone() );
				m.SendMessage( 0, "You receive a special Item!" );
			}
			else if ( chance <= 86 )
			{
				m.AddToBackpack( new TalismanOfFear() );
				m.SendMessage( 0, "You receive a special Talisman!" );
			}
			else if ( chance <= 92 )
			{
				m.AddToBackpack( new TalismanOfMagic() );
				m.SendMessage( 0, "You receive a special Talisman!" );
			}
			else if ( chance <= 94 )
			{
				m.AddToBackpack( new TalismanOfProtection() );
				m.SendMessage( 0, "You receive a special Talisman!" );
			}
			else if ( chance <= 96 )
			{
				m.AddToBackpack( new TalismanOfWar() );
				m.SendMessage( 0, "You receive a special Talisman!" );
			}
			else if ( chance <= 100 )
			{
				m.AddToBackpack( new CacodemonBracelet() );
				m.SendMessage( 0, "You receive a special Item!" );
			}
		}

		private class InternalTimer : Timer
		{
			private UnderworldSystem sys;

			public InternalTimer( UnderworldSystem s ) : base( TimeSpan.FromSeconds( 2.0 ))
			{
				sys = s;
			}

			protected override void OnTick()
			{
				if ( sys != null && !sys.Deleted )
				{
					sys.PerformCheck();
					new InternalTimer( sys ).Start();
				}
			}
		}

		public UnderworldSystem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (Point3D) m_HadesSpawnPoint );
			writer.Write( (Point3D) m_HecateSpawnPoint );
			writer.Write( (Point3D) m_ThanatosSpawnPoint );
			writer.Write( (Point3D) m_PyriphlegethonSpawnPoint );
			writer.Write( (Point3D) m_LetheSpawnPoint );
			writer.Write( (Point3D) m_CocytusSpawnPoint );
			writer.Write( (Point3D) m_CerberusSpawnPoint );
			writer.Write( (Point3D) m_StyxSpawnPoint );
			writer.Write( (Point3D) m_AcheronSpawnPoint );
			writer.Write( (Point3D) m_StyxGatePoint );
			writer.Write( (Point3D) m_CocytusGatePoint );
			writer.Write( (Point3D) m_LetheGatePoint );
			writer.Write( (Point3D) m_PyriphlegethonGatePoint );
			writer.Write( (Point3D) m_TartarusGatePoint );
			writer.Write( (Point3D) m_CharonSpawnPoint );
			writer.Write( (int) m_CurrentStage );
			writer.Write( (int) m_RegularKillCount );
			writer.Write( (Mobile) m_CurrentBoss );
			writer.WriteMobileList( m_Spawns );
			writer.WriteItemList( m_SpawnNodes );
			writer.WriteItemList( m_Gates );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_HadesSpawnPoint = reader.ReadPoint3D();
			m_HecateSpawnPoint = reader.ReadPoint3D();
			m_ThanatosSpawnPoint = reader.ReadPoint3D();
			m_PyriphlegethonSpawnPoint = reader.ReadPoint3D();
			m_LetheSpawnPoint = reader.ReadPoint3D();
			m_CocytusSpawnPoint = reader.ReadPoint3D();
			m_CerberusSpawnPoint = reader.ReadPoint3D();
			m_StyxSpawnPoint = reader.ReadPoint3D();
			m_AcheronSpawnPoint = reader.ReadPoint3D();
			m_StyxGatePoint = reader.ReadPoint3D();
			m_CocytusGatePoint = reader.ReadPoint3D();
			m_LetheGatePoint = reader.ReadPoint3D();
			m_PyriphlegethonGatePoint = reader.ReadPoint3D();
			m_TartarusGatePoint = reader.ReadPoint3D();
			m_CharonSpawnPoint = reader.ReadPoint3D();
			m_CurrentStage = reader.ReadInt();
			m_RegularKillCount = reader.ReadInt();
			m_CurrentBoss = reader.ReadMobile();
			m_Spawns = reader.ReadMobileList();
			m_SpawnNodes = reader.ReadItemList();
			m_Gates = reader.ReadItemList();
			new InternalTimer( this ).Start();
		}
	}
}

namespace Server.Gumps
{
	public class GUnderworldGump : Gump
	{
		public PlayerMobile m_From;
		public UnderworldSystem m_Sys;

		/* //This code was left in place in the event someone wants to allow players/staff to call the Underworld gump through a command.
		public static void Initialize()
		{
			CommandSystem.Register("GUnderworldGump", AccessLevel.Player, new CommandEventHandler(GUnderworldGump_OnCommand));
		}

		[Usage("GUnderworldGump")]
		[Description("Makes a call to your GUnderworldGump.")]
		public static void GUnderworldGump_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;

			if (from.HasGump(typeof(GUnderworldGump)))
				from.CloseGump(typeof(GUnderworldGump));
			//from.SendGump(new GUnderworldGump((PlayerMobile)player, this));
			e.Mobile.SendGump(new GUnderworldGump(e.Mobile, sys));
		}
		*/

		public GUnderworldGump(PlayerMobile m, UnderworldSystem sys) : base( 0, 0 )
		{	
			PlayerMobile m_From;
			UnderworldSystem m_Sys;
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			AddPage(0);
			AddBackground(0, 0, 170, 100, 5054);
			AddLabel(18, 9, 1080, @"Greek Underworld Kills");
			AddLabel(15, 60, 1153, @"Needed:");
			AddLabel(75, 60, 33, @"175");
			AddLabel(15, 35, 1153, @"Current:");
			AddLabel(75, 35, 1074, sys.RegularKillCount.ToString("0"));	
		}
	}
}