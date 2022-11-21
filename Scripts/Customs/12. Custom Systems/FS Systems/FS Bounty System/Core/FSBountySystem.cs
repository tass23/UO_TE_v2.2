using System;
using System.IO;
using Server.Gumps;
using Server.Items;
using Server.Guilds;
using Server.Network;
using Server.Mobiles;
using Server.Factions;
using Server.Targeting;
using Server.Accounting;
using System.Collections;

namespace Server.FSBountyHunterSystem
{
	public class FSBountySystem
	{
		public static void Initialize()
		{
			new ExpireTimer( DateTime.Now + TimeSpan.FromSeconds( 5.0 ) ).Start();
			new GatherTimer( DateTime.Now + TimeSpan.FromSeconds( 5.0 ) ).Start();

			EventSink.WorldSave += new WorldSaveEventHandler( EventSink_WorldSave );
			Load();
		}

		private static void EventSink_WorldSave( WorldSaveEventArgs e )
		{
			Save();
		}

		public class ExpireTimer : Timer
		{
			public ExpireTimer( DateTime end ) : base( end - DateTime.Now )
			{
			}

			protected override void OnTick()
			{
				Expire();
				Stop();
			}
		}

		public class GatherTimer : Timer
		{
			public GatherTimer( DateTime end ) : base( end - DateTime.Now )
			{
			}

			protected override void OnTick()
			{
				Gather();
				Stop();
			}
		}

		public static void Expire()
		{
			ArrayList toCheck = new ArrayList();

			foreach ( FSBountySystem.Bounty b in FSBountySystem.BountyTable.Values )
			{
				if ( b.Expires < DateTime.Now )
					toCheck.Add( b );
			}

			foreach ( FSBountySystem.Bounty b in toCheck )
			{
				FSBountySystem.BountyTable.Remove( b.Wanted );
				FSBountySystem.BountyTable.Remove( b );
			}
		}

		public static void Gather()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in World.Mobiles.Values )
			{
				Bounty b = FindBounty( m );

				if ( b != null )
					list.Add( b );
			}
			
			GlobalBounties = list;
		}

		public static ArrayList GlobalBounties;

		private static Hashtable m_BountyTable;
		private static Hashtable BountyTable
		{
			get
			{
				if ( m_BountyTable == null )
					m_BountyTable = new Hashtable();

				return m_BountyTable;
			}
		}

		public class Bounty
		{
			private Mobile m_Wanted;
			private int m_Reward;
			private DateTime m_Expires;

			public Mobile Wanted{ get{ return m_Wanted; } }
			public int Reward{ get{ return m_Reward; } set{ m_Reward = value; } }
			public DateTime Expires{ get{ return m_Expires; } set{ m_Expires = value; } }

			public Bounty()
			{
				m_Wanted = null;
				m_Reward = 0;
				m_Expires = DateTime.MinValue;
			}
			
			public Bounty( Mobile wanted, int reward, DateTime expires )
			{
				m_Wanted = wanted;
				m_Reward = reward;
				m_Expires = expires;
			}
			
			public void Serialize( GenericWriter writer )
			{
				writer.Write( (int) 0 );

				// version 0
				writer.Write( (Mobile) m_Wanted );
				writer.Write( (int) m_Reward );
				writer.WriteDeltaTime( m_Expires );
			}

			public void Deserialize( GenericReader reader )
			{
				int version = reader.ReadInt();
				
				switch ( version )
				{
					case 0:
					{
						m_Wanted = reader.ReadMobile();
						m_Reward = reader.ReadInt();
						m_Expires = reader.ReadDeltaTime();
						break;
					}
				}
			}

			public void AddReward( int amount )
			{
				m_Reward += amount;
			}

			public void SetExpires( DateTime expires )
			{
				m_Expires = expires;
			}
		}

		public static void Load()
		{
			string idxPath = Path.Combine( "Saves/FS Systems/FSBounty", "BountyTable.idx" );
			string binPath = Path.Combine( "Saves/FS Systems/FSBounty", "BountyTable.bin" );

			if ( File.Exists( idxPath ) && File.Exists( binPath ) )
			{
				FileStream idx = new FileStream( idxPath, FileMode.Open, FileAccess.Read, FileShare.Read );
				FileStream bin = new FileStream( binPath, FileMode.Open, FileAccess.Read, FileShare.Read) ;
				BinaryReader idxReader = new BinaryReader( idx );
				BinaryFileReader binReader = new BinaryFileReader( new BinaryReader( bin ) );

				int orderCount = idxReader.ReadInt32();

				for ( int i = 0; i < orderCount; ++i )
				{
					
					Bounty ps = new Bounty();
					long startPos = idxReader.ReadInt64();
					int length = idxReader.ReadInt32();
					binReader.Seek( startPos, SeekOrigin.Begin );

					try
					{
						ps.Deserialize(binReader);

						if (binReader.Position != ( startPos + length ) )
							throw new Exception( String.Format( "***** Bad serialize on Bounty[{0}] *****", i ) );
					}
					catch
					{
					}

					if ( ps != null && ps.Wanted != null )
						BountyTable.Add( ps.Wanted, ps );
				}
      
				idxReader.Close();
				binReader.Close();
			}

		}


		public static void Save()
		{
			if (!Directory.Exists( "Saves/FS Systems/FSBounty" ) )
				Directory.CreateDirectory( "Saves/FS Systems/FSBounty" );

			string idxPath = Path.Combine( "Saves/FS Systems/FSBounty", "BountyTable.idx" );
			string binPath = Path.Combine( "Saves/FS Systems/FSBounty", "BountyTable.bin" );
							

			GenericWriter idx = new BinaryFileWriter( idxPath, false );
			GenericWriter bin = new BinaryFileWriter( binPath, true );

			idx.Write( (int)BountyTable.Values.Count );
			foreach ( Bounty b in BountyTable.Values )
			{
				long startPos = bin.Position;
				b.Serialize( bin );
				idx.Write( (long)startPos );
				idx.Write( (int)( bin.Position - startPos  ) );
			}

			idx.Close();
			bin.Close();
		}

		public static Bounty FindBounty( Mobile m )
		{
			if ( m == null )
				return null;

			Bounty b = (Bounty)BountyTable[m];

			return b;
		}

		public static void AddReward( Mobile wanted, int amount )
		{
			if ( wanted != null && amount > 0 )
			{
				Bounty b = FindBounty( wanted );

				if ( b != null )
					b.AddReward( amount );
			}
		}

		public static void SetExpires( Mobile wanted, DateTime expires )
		{
			if ( wanted != null )
			{
				Bounty b = FindBounty( wanted );

				if ( b != null )
					b.SetExpires( expires );
			}
		}

		public static void CreateBounty( Mobile wanted, int reward )
		{
			if ( wanted == null || reward < 1 )
				return;

			Bounty b = FindBounty( wanted );

			if ( b != null )
			{
				UpdateBounty( wanted, reward );
			}
			else
			{
				DateTime expires = DateTime.Now + TimeSpan.FromDays( 30.0 );

				b = new Bounty( wanted, reward, expires );
				BountyTable.Add( wanted, b );

				wanted.SendMessage( "A bounty has been placed on your head." );
			}
		}

		public static void UpdateBounty( Mobile wanted, int reward )
		{
			if ( wanted == null || reward < 1 )
				return;

			Bounty b = FindBounty( wanted );
			DateTime expires = DateTime.Now + TimeSpan.FromDays( 30.0 );

			if ( b != null )
			{
				AddReward( wanted, reward );
				SetExpires( wanted, expires );

				wanted.SendMessage( "A bounty has been placed on your head." );
			}
		}

		public static void ClearBounty( Bounty contract, Mobile wanted )
		{
			if ( contract == null || wanted == null )
				return;

			BountyTable.Remove( contract );
			BountyTable.Remove( wanted );
		}

		public static ArrayList GetAllBounties()
		{
			ArrayList bounties = new ArrayList();
 
			foreach ( Bounty b in BountyTable.Values )
			{
				bounties.Add( b );
			}
 
 			return bounties;
 		}
	}
}