using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1E2f/*East*/, 0x1E2E/*South*/ )]
	public class SmallBlasterTarget : AddonComponent
	{
		private double m_MinSkill;
		private double m_MaxSkill;
		private int m_BlasterCartridge;
		private DateTime m_LastUse;

		[CommandProperty( AccessLevel.GameMaster )]
		public double MinSkill
		{
			get{ return m_MinSkill; }
			set{ m_MinSkill = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public double MaxSkill
		{
			get{ return m_MaxSkill; }
			set{ m_MaxSkill = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastUse
		{
			get{ return m_LastUse; }
			set{ m_LastUse = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool FacingEast
		{
			get{ return ( ItemID == 0x1E2f ); }
			set{ ItemID = value ? 0x1E2f : 0x1E2e; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int BlasterCartridge
		{
			get{ return m_BlasterCartridge; }
			set{ m_BlasterCartridge = value; }
		}

		[Constructable]
		public SmallBlasterTarget() : this( 0x1E2f )
		{
			Name = "Target";
		}

		public SmallBlasterTarget( int itemID ) : base( itemID )
		{
			m_MinSkill = -25.0;
			m_MaxSkill = +80.0;
		}

		public SmallBlasterTarget( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			Fire( from );
		}

		private static TimeSpan UseDelay = TimeSpan.FromSeconds( 2.0 );

		private class ScoreEntry
		{
			private int m_Total;
			private int m_Count;
			public int Total{ get{ return m_Total; } set{ m_Total = value; } }
			public int Count{ get{ return m_Count; } set{ m_Count = value; } }

			public void Record( int score )
			{
				m_Total += score;
				m_Count += 1;
			}

			public ScoreEntry()
			{
			}
		}

		private Hashtable m_Entries;

		private ScoreEntry GetEntryFor( Mobile from )
		{
			if ( m_Entries == null )
				m_Entries = new Hashtable();

			ScoreEntry e = (ScoreEntry)m_Entries[from];

			if ( e == null )
				m_Entries[from] = e = new ScoreEntry();

			return e;
		}

		public void Fire( Mobile from )
		{
			BaseBlaster blaster = from.Weapon as BaseBlaster;

			if ( blaster == null )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, true, "You need a blaster to practice on this." ); 
				return;
			}

			if ( DateTime.Now < (m_LastUse + UseDelay) )
				return;

			Point3D worldLoc = GetWorldLocation();

			if ( FacingEast ? from.X <= worldLoc.X : from.Y <= worldLoc.Y )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, true, "You would do better to stand in front of the target." ); 
				return;
			}

			if ( FacingEast ? from.Y != worldLoc.Y : from.X != worldLoc.X )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, true, "You aren't properly lined up with the target to get an accurate shot." ); 
				return;
			}

			if ( !from.InRange( worldLoc, 10 ) )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, true, "You are too far away from the target to get an accurate shot." ); 
				return;
			}
			else if ( from.InRange( worldLoc, 4 ) )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 500599 ); // You are too close to the target.
				return;
			}

			Container pack = from.Backpack;
			Type ammoType = blaster.AmmoType;
			bool isBlasterCartridge = ( ammoType == typeof( BlasterCartridge ) );
			bool isKnown = ( isBlasterCartridge );

			if ( pack == null || !pack.ConsumeTotal( ammoType, 1 ) )
			{
				if ( isBlasterCartridge )
					from.LocalOverheadMessage( MessageType.Regular,  0x3B2, true, "You do not have any ammo with which to practice." ); 
				else
					from.LocalOverheadMessage( MessageType.Regular,  0x3B2, true, "You need a blaster to practice on this." );
				return;
			}

			m_LastUse = DateTime.Now;
			from.Direction = from.GetDirectionTo( GetWorldLocation() );
			blaster.PlaySwingAnimation( from );
			from.MovingEffect( this, blaster.EffectID, 18, 1, false, false );
			ScoreEntry se = GetEntryFor( from );

			if ( !from.CheckSkill( blaster.Skill, m_MinSkill, m_MaxSkill ) )
			{
				from.PlaySound( blaster.MissSound );
				PublicOverheadMessage( MessageType.Regular, 0x3B2, 500604, from.Name ); // You miss the target altogether.
				se.Record( 0 );

				if ( se.Count == 1 )
					PublicOverheadMessage( MessageType.Regular, 0x3B2, 1062719, se.Total.ToString() );
				else
					PublicOverheadMessage( MessageType.Regular, 0x3B2, 1042683, String.Format( "{0}\t{1}", se.Total, se.Count ) );
				return;
			}

			Effects.PlaySound( Location, Map, blaster.HitSound );
			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
			double rand = Utility.RandomDouble();
			int area, score, splitScore;

			if ( 0.10 > rand )
			{
				area = 0; // bullseye
				score = 50;
				splitScore = 100;
			}
			else if ( 0.25 > rand )
			{
				area = 1; // inner ring
				score = 10;
				splitScore = 20;
			}
			else if ( 0.50 > rand )
			{
				area = 2; // middle ring
				score = 5;
				splitScore = 15;
			}
			else
			{
				area = 3; // outer ring
				score = 2;
				splitScore = 5;
			}

			bool split = ( isKnown && ( m_BlasterCartridge * 0.02 )   > Utility.RandomDouble() );

			if ( split )
			{
				PublicOverheadMessage( MessageType.Regular, 0x3B2, 1010027 + (isBlasterCartridge ? 0 : 4) + area, from.Name );
			}
			else
			{
				PublicOverheadMessage( MessageType.Regular, 0x3B2, 1010035 + area, from.Name );

				if ( isBlasterCartridge )
					++m_BlasterCartridge;	
			}

			se.Record( split ? splitScore : score );

			if ( se.Count == 1 )
				PublicOverheadMessage( MessageType.Regular, 0x3B2, 1062719, se.Total.ToString() );
			else
				PublicOverheadMessage( MessageType.Regular, 0x3B2, 1042683, String.Format( "{0}\t{1}", se.Total, se.Count ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( m_MinSkill );
			writer.Write( m_MaxSkill );
			writer.Write( m_BlasterCartridge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_MinSkill = reader.ReadDouble();
					m_MaxSkill = reader.ReadDouble();
					m_BlasterCartridge = reader.ReadInt();

					if ( m_MinSkill == 0.0 && m_MaxSkill == 85.0 )
					{
						m_MinSkill = -25.0;
						m_MaxSkill = +80.0;
					}
					break;
				}
			}
		}
	}

	public class SmallBlasterTargetAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new SmallBlasterTargetDeed(); } }

		[Constructable]
		public SmallBlasterTargetAddon()
		{
			AddComponent( new SmallBlasterTarget( 0x1E2f ), 0, 0, 0 );
		}

		public SmallBlasterTargetAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SmallBlasterTargetDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new SmallBlasterTargetAddon(); } }

		[Constructable]
		public SmallBlasterTargetDeed()
		{
			Name = "Small Blaster Target Deed";
		}

		public SmallBlasterTargetDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}