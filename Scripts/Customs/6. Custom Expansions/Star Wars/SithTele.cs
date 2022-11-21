using System;
using Server;
using Server.Network;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public class SithTele : Item
	{
		private bool m_Active, m_Creatures, m_CombatCheck;
		private Point3D m_PointDest;
		private Map m_MapDest;
		private bool m_SourceEffect;
		private bool m_DestEffect;
		private int m_SoundID;
		private TimeSpan m_Delay;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool SourceEffect
		{
			get{ return m_SourceEffect; }
			set{ m_SourceEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool DestEffect
		{
			get{ return m_DestEffect; }
			set{ m_DestEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SoundID
		{
			get{ return m_SoundID; }
			set{ m_SoundID = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan Delay
		{
			get{ return m_Delay; }
			set{ m_Delay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get { return m_Active; }
			set { m_Active = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D PointDest
		{
			get { return m_PointDest; }
			set { m_PointDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map MapDest
		{
			get { return m_MapDest; }
			set { m_MapDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Creatures
		{
			get { return m_Creatures; }
			set { m_Creatures = value; InvalidateProperties(); }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public bool CombatCheck
		{
			get { return m_CombatCheck; }
			set { m_CombatCheck = value; InvalidateProperties(); }
		}

		public override int LabelNumber{ get{ return 1026095; } } // SithTele

		[Constructable]
		public SithTele() : this( new Point3D( 0, 0, 0 ), null, false )
		{
		}

		[Constructable]
		public SithTele( Point3D pointDest, Map mapDest ) : this( pointDest, mapDest, false )
		{
		}

		[Constructable]
		public SithTele( Point3D pointDest, Map mapDest, bool creatures ) : base( 0x1BC3 )
		{
			Movable = false;
			Visible = false;
			m_Active = true;
			m_PointDest = pointDest;
			m_MapDest = mapDest;
			m_Creatures = creatures;
			m_CombatCheck = false;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Active )
				list.Add( 1060742 ); // active
			else
				list.Add( 1060743 ); // inactive

			if ( m_MapDest != null )
				list.Add( 1060658, "Map\t{0}", m_MapDest );

			if ( m_PointDest != Point3D.Zero )
				list.Add( 1060659, "Coords\t{0}", m_PointDest );

			list.Add( 1060660, "Creatures\t{0}", m_Creatures ? "Yes" : "No" );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( m_Active )
			{
				if ( m_MapDest != null && m_PointDest != Point3D.Zero )
					LabelTo( from, "{0} [{1}]", m_PointDest, m_MapDest );
				else if ( m_MapDest != null )
					LabelTo( from, "[{0}]", m_MapDest );
				else if ( m_PointDest != Point3D.Zero )
					LabelTo( from, m_PointDest.ToString() );
			}
			else
			{
				LabelTo( from, "(inactive)" );
			}
		}

		public virtual void StartTeleport( Mobile m )
		{
			if ( m_Delay == TimeSpan.Zero )
				DoTeleport( m );
			else
				Timer.DelayCall( m_Delay, new TimerStateCallback( DoTeleport_Callback ), m );
		}

		private void DoTeleport_Callback( object state )
		{
			DoTeleport( (Mobile) state );
		}

		public virtual void DoTeleport( Mobile m )
		{
			Map map = m_MapDest;

			if ( map == null || map == Map.Internal )
				map = m.Map;

			Point3D p = m_PointDest;

			if ( p == Point3D.Zero )
				p = m.Location;

			Server.Mobiles.BaseCreature.TeleportPets( m, p, map );
			bool sendEffect = ( !m.Hidden || m.AccessLevel == AccessLevel.Player );

			if ( m_SourceEffect && sendEffect )
				Effects.SendLocationEffect( m.Location, m.Map, 0x3728, 10, 10 );

			m.MoveToWorld( p, map );

			if ( m_DestEffect && sendEffect )
				Effects.SendLocationEffect( m.Location, m.Map, 0x3728, 10, 10 );

			if ( m_SoundID > 0 && sendEffect )
				Effects.PlaySound( m.Location, m.Map, m_SoundID );
		}

		public override bool OnMoveOver( Mobile m )
		{
			PlayerMobile from = m as PlayerMobile;
			
			if ( m_Active )
			{
				if (m.Player && (m.Karma <= -5000 & m.Title == "the Sith Apprentice" | m.Karma <= -5000 & m.Title == "the Sith Lord" | m.Karma <= -5000 & m.Title == "the Sith"))
					StartTeleport( m );
				else
					m.SendMessage("Your Karma is too high to enter here or you are not a Sith yet.");
         
				return false;

				if ( !m_Creatures && !m.Player )
					return true;
				else if ( m_CombatCheck && SpellHelper.CheckCombat( m ) )
				{
					m.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
					return true;				
				}
				StartTeleport( m );
				return false;
			}
			return true;
		}

		public SithTele( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version
			writer.Write( (bool) m_CombatCheck );
			writer.Write( (bool) m_SourceEffect );
			writer.Write( (bool) m_DestEffect );
			writer.Write( (TimeSpan) m_Delay );
			writer.WriteEncodedInt( (int) m_SoundID );
			writer.Write( m_Creatures );
			writer.Write( m_Active );
			writer.Write( m_PointDest );
			writer.Write( m_MapDest );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				{
					m_CombatCheck = reader.ReadBool();
					goto case 2;
				}
				case 2:
				{
					m_SourceEffect = reader.ReadBool();
					m_DestEffect = reader.ReadBool();
					m_Delay = reader.ReadTimeSpan();
					m_SoundID = reader.ReadEncodedInt();
					goto case 1;
				}
				case 1:
				{
					m_Creatures = reader.ReadBool();
					goto case 0;
				}
				case 0:
				{
					m_Active = reader.ReadBool();
					m_PointDest = reader.ReadPoint3D();
					m_MapDest = reader.ReadMap();
					break;
				}
			}
		}
	}
}