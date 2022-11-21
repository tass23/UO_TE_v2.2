///////////////////////////////////////////////////
//						 //
//              .~Stage Spawner~.	   	 //
//						 //
//  Creator: Father Time			 //
//  Creators email: FatherTime@TheHyperCube.Net  //
//  Creation date: 11/18/06			 //
//  Designed for: RunUO RC1			 //
//  Server: The HyperCube   			 //
//  ICQ: 146563794			         //
//  Version: 2.0				 //
//  todo: add the 3 rare spawns			 //
///////////////////////////////////////////////////

using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{
	#region Enums
	[Flags]
	public enum SpawnStages
	{
		Do1Stage,
		Do2Stage,
		Do3Stage,
		Do4Stage,
		Do5Stage,
		Do6Stage,
		Do7Stage,
		Do8Stage,
		Do9Stage
	}
	#endregion

	public class StageSpawner : Item
	{
		public Timer m_Timer;

		private string m_ToSpawnRare;

		private double m_ChanceRareSpawn;
		private double m_ChanceParagon;

		public SpawnStages m_SpawnStages;

		private int m_SpawnHue;
		private string m_SpawnTitle;
		public bool m_SpawnMurderers = false;
		public bool m_SpawnParagons = false;
		public bool m_Repeat = true;
		private int m_Stage;

		private int m_Spawn1Count;
		private int m_Spawn2Count;
		private int m_Spawn3Count;
		private int m_Spawn4Count;
		private int m_Spawn5Count;
		private int m_Spawn6Count;
		private int m_Spawn7Count;
		private int m_Spawn8Count;
		private int m_Spawn9Count;

		private string m_ToSpawn1;
		private string m_ToSpawn2;
		private string m_ToSpawn3;
		private string m_ToSpawn4;
		private string m_ToSpawn5;
		private string m_ToSpawn6;
		private string m_ToSpawn7;
		private string m_ToSpawn8;
		private string m_ToSpawn9;

		private TimeSpan m_MinDelay;
		private TimeSpan m_MaxDelay;

		private DateTime m_SpawnRate;

		private int m_Stage1Kills;
		private int m_Stage2Kills;
		private int m_Stage3Kills;
		private int m_Stage4Kills;
		private int m_Stage5Kills;
		private int m_Stage6Kills;
		private int m_Stage7Kills;
		private int m_Stage8Kills;
		private int m_Stage9Kills;

		public bool m_Active = false;
		private int m_TotalKills;
		private Rectangle2D m_SpawnArea;
		private ArrayList m_Creatures;

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawnRare
		{
			get { return m_ToSpawnRare; }
			set { m_ToSpawnRare = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double ChanceRareSpawn
		{
			get{ return m_ChanceRareSpawn; }
			set{ m_ChanceRareSpawn = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double ChanceParagon
		{
			get{ return m_ChanceParagon; }
			set{ m_ChanceParagon = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SpawnStages SpawnStages
		{
			get{ return m_SpawnStages; }
			set{ m_SpawnStages = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SpawnHue
		{
			get{ return m_SpawnHue; }
			set{ m_SpawnHue = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string SpawnTitle
		{
			get { return m_SpawnTitle; }
			set { m_SpawnTitle = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool SpawnMurderers{ get{ return m_SpawnMurderers; } set{ m_SpawnMurderers = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool SpawnParagons{ get{ return m_SpawnParagons; } set{ m_SpawnParagons = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Repeat{ get{ return m_Repeat; } set{ m_Repeat = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage
		{
			get{ return m_Stage; }
			set{ m_Stage = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn1Count
		{
			get{ return m_Spawn1Count; }
			set{ m_Spawn1Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn2Count
		{
			get{ return m_Spawn2Count; }
			set{ m_Spawn2Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn3Count
		{
			get{ return m_Spawn3Count; }
			set{ m_Spawn3Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn4Count
		{
			get{ return m_Spawn4Count; }
			set{ m_Spawn4Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn5Count
		{
			get{ return m_Spawn5Count; }
			set{ m_Spawn5Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn6Count
		{
			get{ return m_Spawn6Count; }
			set{ m_Spawn6Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn7Count
		{
			get{ return m_Spawn7Count; }
			set{ m_Spawn7Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn8Count
		{
			get{ return m_Spawn8Count; }
			set{ m_Spawn8Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Spawn9Count
		{
			get{ return m_Spawn9Count; }
			set{ m_Spawn9Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn1
		{
			get { return m_ToSpawn1; }
			set { m_ToSpawn1 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn2
		{
			get { return m_ToSpawn2; }
			set { m_ToSpawn2 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn3
		{
			get { return m_ToSpawn3; }
			set { m_ToSpawn3 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn4
		{
			get { return m_ToSpawn4; }
			set { m_ToSpawn4 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn5
		{
			get { return m_ToSpawn5; }
			set { m_ToSpawn5 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn6
		{
			get { return m_ToSpawn6; }
			set { m_ToSpawn6 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn7
		{
			get { return m_ToSpawn7; }
			set { m_ToSpawn7 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn8
		{
			get { return m_ToSpawn8; }
			set { m_ToSpawn8 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ToSpawn9
		{
			get { return m_ToSpawn9; }
			set { m_ToSpawn9 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan MinDelay
		{
			get { return m_MinDelay; }
			set { m_MinDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan MaxDelay
		{
			get { return m_MaxDelay; }
			set { m_MaxDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]// public DateTime ElectionEnd{ get{ return m_ElectionEnd; } }
		public DateTime SpawnRate
		{
			get{ return m_SpawnRate; }
			set{ m_SpawnRate = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage1Kills
		{
			get{ return m_Stage1Kills; }
			set{ m_Stage1Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage2Kills
		{
			get{ return m_Stage2Kills; }
			set{ m_Stage2Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage3Kills
		{
			get{ return m_Stage3Kills; }
			set{ m_Stage3Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage4Kills
		{
			get{ return m_Stage4Kills; }
			set{ m_Stage4Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage5Kills
		{
			get{ return m_Stage5Kills; }
			set{ m_Stage5Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage6Kills
		{
			get{ return m_Stage6Kills; }
			set{ m_Stage6Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage7Kills
		{
			get{ return m_Stage7Kills; }
			set{ m_Stage7Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage8Kills
		{
			get{ return m_Stage8Kills; }
			set{ m_Stage8Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage9Kills
		{
			get{ return m_Stage9Kills; }
			set{ m_Stage9Kills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active{ get{ return m_Active; } set{ m_Active = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int TotalKills
		{
			get{ return m_TotalKills; }
			set{ m_TotalKills = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Rectangle2D SpawnArea
		{
			get
			{
				return m_SpawnArea;
			}
			set
			{
				m_SpawnArea = value;
				InvalidateProperties();
			}
		}

		[Constructable]
		public StageSpawner() : base( 0x1bc3 ) 
		{
			Name = "Stage Spawner";
			Movable = false;
			Visible = false;
			Stage = 1;

			m_Creatures = new ArrayList();

			m_Timer = new RefreshTimer(this);
			m_Timer.Start();
		}

		public StageSpawner( Serial serial ) : base( serial )
		{
			m_Timer = new RefreshTimer(this);
			m_Timer.Start();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)1 ); // version

			writer.Write( m_ToSpawnRare );

			writer.Write( (double)m_ChanceRareSpawn );
			writer.Write( (double)m_ChanceParagon );

			writer.Write( (int) m_SpawnStages );

			writer.WriteEncodedInt( (int) m_SpawnHue );
			writer.Write( m_SpawnTitle );

			writer.Write( m_SpawnMurderers );
			writer.Write( m_SpawnParagons );
			writer.Write( m_Repeat );

			writer.WriteEncodedInt( (int) m_Stage );

			writer.WriteEncodedInt( (int) m_Spawn1Count );
			writer.WriteEncodedInt( (int) m_Spawn2Count );
			writer.WriteEncodedInt( (int) m_Spawn3Count );
			writer.WriteEncodedInt( (int) m_Spawn4Count );
			writer.WriteEncodedInt( (int) m_Spawn5Count );
			writer.WriteEncodedInt( (int) m_Spawn6Count );
			writer.WriteEncodedInt( (int) m_Spawn7Count );
			writer.WriteEncodedInt( (int) m_Spawn8Count );
			writer.WriteEncodedInt( (int) m_Spawn9Count );

			writer.Write( m_ToSpawn1 );
			writer.Write( m_ToSpawn2 );
			writer.Write( m_ToSpawn3 );
			writer.Write( m_ToSpawn4 );
			writer.Write( m_ToSpawn5 );
			writer.Write( m_ToSpawn6 );
			writer.Write( m_ToSpawn7 );
			writer.Write( m_ToSpawn8 );
			writer.Write( m_ToSpawn9 );

			writer.Write( m_MinDelay );
			writer.Write( m_MaxDelay );

			writer.Write( (DateTime) m_SpawnRate );

			writer.WriteEncodedInt( (int) m_Stage1Kills );
			writer.WriteEncodedInt( (int) m_Stage2Kills );
			writer.WriteEncodedInt( (int) m_Stage3Kills );
			writer.WriteEncodedInt( (int) m_Stage4Kills );
			writer.WriteEncodedInt( (int) m_Stage5Kills );
			writer.WriteEncodedInt( (int) m_Stage6Kills );
			writer.WriteEncodedInt( (int) m_Stage7Kills );
			writer.WriteEncodedInt( (int) m_Stage8Kills );
			writer.WriteEncodedInt( (int) m_Stage9Kills );

			writer.Write( m_Active );
			writer.WriteEncodedInt( (int) m_TotalKills );
			writer.Write( m_SpawnArea );
			writer.WriteMobileList( m_Creatures, true );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{


				case 1:
				{
					m_ToSpawnRare = Utility.Intern( reader.ReadString() );
					m_ChanceRareSpawn = reader.ReadDouble();
					m_ChanceParagon = reader.ReadDouble();

					m_SpawnStages = (SpawnStages)reader.ReadInt();
					m_SpawnHue = reader.ReadEncodedInt();
					m_SpawnTitle = Utility.Intern( reader.ReadString() );

					m_SpawnMurderers = reader.ReadBool();
					m_SpawnParagons = reader.ReadBool();
					m_Repeat = reader.ReadBool();

					m_Stage = reader.ReadEncodedInt();

					m_Spawn1Count = reader.ReadEncodedInt();
					m_Spawn2Count = reader.ReadEncodedInt();
					m_Spawn3Count = reader.ReadEncodedInt();
					m_Spawn4Count = reader.ReadEncodedInt();
					m_Spawn5Count = reader.ReadEncodedInt();
					m_Spawn6Count = reader.ReadEncodedInt();
					m_Spawn7Count = reader.ReadEncodedInt();
					m_Spawn8Count = reader.ReadEncodedInt();
					m_Spawn9Count = reader.ReadEncodedInt();

					m_ToSpawn1 = Utility.Intern( reader.ReadString() );
					m_ToSpawn2 = Utility.Intern( reader.ReadString() );
					m_ToSpawn3 = Utility.Intern( reader.ReadString() );
					m_ToSpawn4 = Utility.Intern( reader.ReadString() );
					m_ToSpawn5 = Utility.Intern( reader.ReadString() );
					m_ToSpawn6 = Utility.Intern( reader.ReadString() );
					m_ToSpawn7 = Utility.Intern( reader.ReadString() );
					m_ToSpawn8 = Utility.Intern( reader.ReadString() );
					m_ToSpawn9 = Utility.Intern( reader.ReadString() );

					m_MinDelay = reader.ReadTimeSpan();
					m_MaxDelay = reader.ReadTimeSpan();
					m_SpawnRate = reader.ReadDateTime();

					m_Stage1Kills = reader.ReadEncodedInt();
					m_Stage2Kills = reader.ReadEncodedInt();
					m_Stage3Kills = reader.ReadEncodedInt();
					m_Stage4Kills = reader.ReadEncodedInt();
					m_Stage5Kills = reader.ReadEncodedInt();
					m_Stage6Kills = reader.ReadEncodedInt();
					m_Stage7Kills = reader.ReadEncodedInt();
					m_Stage8Kills = reader.ReadEncodedInt();
					m_Stage9Kills = reader.ReadEncodedInt();

					m_Active = reader.ReadBool();
					m_TotalKills = reader.ReadEncodedInt();
					m_SpawnArea = reader.ReadRect2D();
					goto case 0;
				}
				case 0:
				{
					m_Creatures = reader.ReadMobileList();
					break;
				}
			}
		}

		public void ProcessStage()
		{	
			if( m_Stage == 1 )
			{
				if( m_TotalKills >= m_Stage1Kills )
				{
					if( m_SpawnStages >= SpawnStages.Do2Stage )
					{
						m_TotalKills = 0;
						m_Stage += 1;
					}

					else
					{
						if( m_Repeat == false )
							m_Active = false;

						m_TotalKills = 0;
					}
				}
			}

			if( m_Stage == 2 )
			{
				if( m_TotalKills >= m_Stage2Kills )
				{
					if( m_SpawnStages >= SpawnStages.Do3Stage )
					{
						m_TotalKills = 0;
						m_Stage += 1;
					}

					else
					{
						if( m_Repeat == false )
							m_Active = false;

						m_Stage = 1;
						m_TotalKills = 0;
					}
				}
			}

			if( m_Stage == 3 )
			{
				if( m_TotalKills >= m_Stage3Kills )
				{
					if( m_SpawnStages >= SpawnStages.Do4Stage )
					{
						m_TotalKills = 0;
						m_Stage += 1;
					}

					else
					{
						if( m_Repeat == false )
							m_Active = false;

						m_Stage = 1;
						m_TotalKills = 0;
					}
				}
			}

			if( m_Stage == 4 )
			{
				if( m_TotalKills >= m_Stage4Kills )
				{
					if( m_SpawnStages >= SpawnStages.Do5Stage )
					{
						m_TotalKills = 0;
						m_Stage += 1;
					}

					else
					{
						if( m_Repeat == false )
							m_Active = false;

						m_Stage = 1;
						m_TotalKills = 0;
					}
				}
			}

			if( m_Stage == 5 )
			{
				if( m_TotalKills >= m_Stage5Kills )
				{
					if( m_SpawnStages >= SpawnStages.Do6Stage )
					{
						m_TotalKills = 0;
						m_Stage += 1;
					}

					else
					{
						if( m_Repeat == false )
							m_Active = false;

						m_Stage = 1;
						m_TotalKills = 0;
					}
				}
			}

			if( m_Stage == 6 )
			{
				if( m_TotalKills >= m_Stage6Kills )
				{
					if( m_SpawnStages >= SpawnStages.Do7Stage )
					{
						m_TotalKills = 0;
						m_Stage += 1;
					}

					else
					{
						if( m_Repeat == false )
							m_Active = false;

						m_Stage = 1;
						m_TotalKills = 0;
					}
				}
			}

			if( m_Stage == 7 )
			{
				if( m_TotalKills >= m_Stage7Kills )
				{
					if( m_SpawnStages >= SpawnStages.Do8Stage )
					{
						m_TotalKills = 0;
						m_Stage += 1;
					}

					else
					{
						if( m_Repeat == false )
							m_Active = false;

						m_Stage = 1;
						m_TotalKills = 0;
					}
				}
			}

			if( m_Stage == 8 )
			{
				if( m_TotalKills >= m_Stage8Kills )
				{
					if( m_SpawnStages >= SpawnStages.Do9Stage )
					{
						m_TotalKills = 0;
						m_Stage += 1;
					}

					else
					{
						if( m_Repeat == false )
							m_Active = false;

						m_Stage = 1;
						m_TotalKills = 0;
					}
				}
			}

			if( m_Stage == 9 )
			{
				if( m_TotalKills >= m_Stage9Kills )
				{
					if( m_Repeat == false )
						m_Active = false;

					m_Stage = 1;
					m_TotalKills = 0;
				}
			}
		}

		public void TimeCheck()
		{	
			if( DateTime.Now >= m_SpawnRate )
			{
				Refresh();

				int minSeconds = (int)m_MinDelay.TotalSeconds;
				int maxSeconds = (int)m_MaxDelay.TotalSeconds;

				TimeSpan delay = TimeSpan.FromSeconds( Utility.RandomMinMax( minSeconds, maxSeconds ) );
				m_SpawnRate = DateTime.Now + delay;
			}
		}

		public void Refresh()
		{	

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				BaseCreature pet = m_Creatures[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					m_TotalKills += 1;
					m_Creatures.RemoveAt( i );
					--i;
					continue;
				}

				if ( pet.Controlled == true )
				{
					m_Creatures.RemoveAt( i );
					--i;
					continue;
				}
			}
			Spawn();
		}

		public void DoRareSpawn()
		{
			Type type = SpawnerType.GetType( (string)m_ToSpawnRare );

			if ( type != null )
			{
				try
				{
					object o = Activator.CreateInstance( type );

					if ( o is BaseCreature )
					{
						BaseCreature m = (BaseCreature)o;
						
						InvalidateProperties();

						m_Creatures.Add( m );
						m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
						Point3D loc = GetSpawnLocation();
						m.MoveToWorld( loc, this.Map );
						m.Home = m.Location;
						
						if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
							m.IsParagon = true;

						if( m_SpawnMurderers == true )
							m.Kills = 10;

						if( m_SpawnHue != 0 )
							m.Hue = m_SpawnHue;

						if( m_SpawnTitle != null )
							m.Title = m_SpawnTitle;
					}
				}
				catch
				{
				}
			}
		}

		public void Spawn()
		{
			if( m_Stage == 1 && m_SpawnStages >= SpawnStages.Do1Stage )
			{
				if( m_Creatures.Count > m_Spawn1Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn1 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;
						
							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;


						}

					}
					catch
					{
					}
				}
			}

			if( m_Stage == 2 && m_SpawnStages >= SpawnStages.Do2Stage )
			{
				if( m_Creatures.Count > m_Spawn2Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn2 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;
						
							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;
						}

					}
					catch
					{
					}
				}
			}

			if( m_Stage == 3 && m_SpawnStages >= SpawnStages.Do3Stage )
			{
				if( m_Creatures.Count > m_Spawn3Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn3 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;
						
							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;
						}

					}
					catch
					{
					}
				}
			}

			if( m_Stage == 4 && m_SpawnStages >= SpawnStages.Do4Stage )
			{
				if( m_Creatures.Count > m_Spawn4Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn4 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;
						
							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;
						}

					}
					catch
					{
					}
				}
			}

			if( m_Stage == 5 && m_SpawnStages >= SpawnStages.Do5Stage )
			{
				if( m_Creatures.Count > m_Spawn5Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn5 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;
						
							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;
						}

					}
					catch
					{
					}
				}
			}

			if( m_Stage == 6 && m_SpawnStages >= SpawnStages.Do6Stage )
			{
				if( m_Creatures.Count > m_Spawn6Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn6 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;

							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;
						}

					}
					catch
					{
					}
				}
			}

			if( m_Stage == 7 && m_SpawnStages >= SpawnStages.Do7Stage )
			{
				if( m_Creatures.Count > m_Spawn7Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn7 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;

							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;
						}

					}
					catch
					{
					}
				}
			}

			if( m_Stage == 8 && m_SpawnStages >= SpawnStages.Do8Stage )
			{
				if( m_Creatures.Count > m_Spawn8Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn8 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;

							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;
						}

					}
					catch
					{
					}
				}
			}

			if( m_Stage == 9 && m_SpawnStages >= SpawnStages.Do9Stage )
			{
				if( m_Creatures.Count > m_Spawn9Count )
					return;

				if( m_ToSpawnRare != null && m_ChanceRareSpawn > Utility.RandomDouble() )
				{
					DoRareSpawn();
					return;
				}

				Type type = SpawnerType.GetType( (string)m_ToSpawn9 );

				if ( type != null )
				{
					try
					{
						object o = Activator.CreateInstance( type );

						if ( o is BaseCreature )
						{
							BaseCreature m = (BaseCreature)o;

							InvalidateProperties();

							m_Creatures.Add( m );
							m.RangeHome = (int)(Math.Sqrt( m_SpawnArea.Width * m_SpawnArea.Width + m_SpawnArea.Height * m_SpawnArea.Height )/2);
							Point3D loc = GetSpawnLocation();
							m.MoveToWorld( loc, this.Map );
							m.Home = m.Location;
						
							if( m_SpawnParagons == true && m_ChanceParagon > Utility.RandomDouble() )
								m.IsParagon = true;

							if( m_SpawnMurderers == true )
								m.Kills = 10;

							if( m_SpawnHue != 0 )
								m.Hue = m_SpawnHue;

							if( m_SpawnTitle != null )
								m.Title = m_SpawnTitle;
						}

					}
					catch
					{
					}
				}
			}
		}

		public Point3D GetSpawnLocation()
		{
			Map map = Map;

			if( map == null )
				return Location;

			// Try 20 times to find a spawnable location.
			for( int i = 0; i < 20; i++ )
			{
				int x = Utility.Random( m_SpawnArea.X, m_SpawnArea.Width );
				int y = Utility.Random( m_SpawnArea.Y, m_SpawnArea.Height );

				int z = Map.GetAverageZ( x, y );

				if( Map.CanSpawnMobile( new Point2D( x, y ), z ) )
					return new Point3D( x, y, z );
			}

			return Location;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("There are " + m_Creatures.Count.ToString() + " monster's on the spawner's array list." );
			
			if ( from.AccessLevel >= AccessLevel.GameMaster )
				from.SendGump( new PropertiesGump( from, this ) );

			else
				from.SendMessage( "That is not accessible." );

		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				BaseCreature pet = m_Creatures[i] as BaseCreature;

				pet.Delete();
			}
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			list.Add( "stage spawn" );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if( m_Active )
			{
				list.Add( 1060742 ); // active
				
				if( m_Stage == 1 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage1Kills);

				if( m_Stage == 2 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage2Kills );

				if( m_Stage == 3 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage3Kills );

				if( m_Stage == 4 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage4Kills );

				if( m_Stage == 5 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage5Kills );

				if( m_Stage == 6 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage6Kills );

				if( m_Stage == 7 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage7Kills );

				if( m_Stage == 8 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage8Kills );

				if( m_Stage == 9 )
				list.Add( 1060660, "Kills\t{0} of {1}", m_TotalKills, m_Stage9Kills );

				list.Add( 1060661, "speed\t{0} to {1}", m_MinDelay, m_MaxDelay ); // ~1_val~: ~2_val~

				list.Add( 1060658, "Spawning paragons\t{0}", m_SpawnParagons );
				list.Add( 1060659, "Spawning murderers\t{0}", m_SpawnMurderers );
				list.Add( 1060662, "Spawn repeat\t{0}", m_Repeat );
				list.Add( 1060663, "Spawn stage\t{0}", m_Stage );
			}
			else
			{
				list.Add( 1060743 ); // inactive
			}
		}

		private class RefreshTimer : Timer
		{
			private StageSpawner m_Owner;

			public RefreshTimer( StageSpawner owner ) : base( TimeSpan.FromSeconds( 1 ), TimeSpan.FromSeconds( 1 ) )
			{
				m_Owner = owner;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				if( m_Owner.Active == true )
				{
					m_Owner.TimeCheck();
					m_Owner.ProcessStage();
				}
			}
		}
	}
}
