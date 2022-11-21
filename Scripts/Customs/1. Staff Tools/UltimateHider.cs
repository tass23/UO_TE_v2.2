using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class UltimateHider : Item
	{
		private int m_UnHideEffectID;
		private int m_UnHideEffectHue;
		private int m_UnHideEffectDuration;
		private int m_UnHideEffectRenderMode;
		private int m_HideEffectID;
		private int m_HideEffectHue;
		private int m_HideEffectDuration;
		private int m_HideEffectRenderMode;
		private int m_UnHideSound;
		private int m_HideSound;
		private string m_UnHideSaying;
		private string m_HideSaying;
		private bool m_AreaEffect;
		private bool m_UnHideDelay;

		[CommandProperty( AccessLevel.GameMaster )] 
		public int UnHideEffectID 
		{ 
			get { return m_UnHideEffectID; } 
			set { m_UnHideEffectID = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int UnHideEffectHue 
		{ 
			get { return m_UnHideEffectHue; } 
			set { m_UnHideEffectHue = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int UnHideEffectDuration 
		{ 
			get { return m_UnHideEffectDuration; } 
			set { m_UnHideEffectDuration = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int UnHideEffectRenderMode
		{ 
			get { return m_UnHideEffectRenderMode; } 
			set { m_UnHideEffectRenderMode = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int HideEffectID 
		{ 
			get { return m_HideEffectID; } 
			set { m_HideEffectID = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int HideEffectHue 
		{ 
			get { return m_HideEffectHue; } 
			set { m_HideEffectHue = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int HideEffectDuration 
		{ 
			get { return m_HideEffectDuration ; } 
			set { m_HideEffectDuration  = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int HideEffectRenderMode 
		{ 
			get { return m_HideEffectRenderMode; } 
			set { m_HideEffectRenderMode = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int HideSound 
		{ 
			get { return m_HideSound; } 
			set { m_HideSound = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public int UnHideSound 
		{ 
			get { return m_UnHideSound; } 
			set { m_UnHideSound = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public string HideSaying 
		{ 
			get { return m_HideSaying; } 
			set { m_HideSaying = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public string UnHideSaying 
		{ 
			get { return m_UnHideSaying; } 
			set { m_UnHideSaying = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public bool AreaEffect 
		{ 
			get { return m_AreaEffect; } 
			set { m_AreaEffect = value; } 
		} 
		[CommandProperty( AccessLevel.GameMaster )] 
		public bool UnHideDelay 
		{ 
			get { return m_UnHideDelay; } 
			set { m_UnHideDelay = value; } 
		} 

		[Constructable]
		public UltimateHider() : base( 0x1869 )
		{
			Hue = 1;
			Name = "Ultimate Hider";
		}
		public UltimateHider( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
                        }
			else if ( from.AccessLevel == AccessLevel.Player )
			{
				from.SendMessage( "You may not use this item, it is deleted." );
				this.Delete();
                        }
			else if ( from.Hidden == false )
			{
				from.PlaySound( HideSound );
				from.Emote( "*"+ HideSaying +"*" ); 
				Item item = new InternalItem( from.Location, from.Map );

				if ( AreaEffect == false )
				{
					Effects.SendLocationParticles( EffectItem.Create( ( item ).GetWorldLocation(), item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );
				}
				else
				{
					Effects.SendLocationParticles( EffectItem.Create( ( item ).GetWorldLocation(), item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );

					Point3D loc = new Point3D( item.X - 1, item.Y, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X - 1, item.Y + 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X, item.Y + 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X + 1, item.Y + 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X + 1, item.Y, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X + 1, item.Y - 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X, item.Y - 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X - 1, item.Y - 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), HideEffectID, 1, HideEffectDuration, HideEffectHue-1, HideEffectRenderMode, 0, 0 );
				}

				new HideTimer( from, this ).Start();
			}
			else
			{
				if ( UnHideDelay == false )
				{
					from.Hidden = false;
					from.Emote( "*"+ UnHideSaying +"*" ); 
				}
				else
				{
					new UnHideTimer( from, this ).Start();
				}

				Item item = new InternalItem( from.Location, from.Map );
				from.PlaySound( UnHideSound ); 


				if ( AreaEffect == false )
				{
					Effects.SendLocationParticles( EffectItem.Create( ( item ).GetWorldLocation(), item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );
				}
				else
				{
					Effects.SendLocationParticles( EffectItem.Create( ( item ).GetWorldLocation(), item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );

					Point3D loc = new Point3D( item.X - 1, item.Y, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X - 1, item.Y + 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X, item.Y + 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X + 1, item.Y + 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X + 1, item.Y, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X + 1, item.Y - 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X, item.Y - 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );

					loc = new Point3D( item.X - 1, item.Y - 1, item.Z );
					Effects.SendLocationParticles( EffectItem.Create( loc, item.Map, EffectItem.DefaultDuration ), UnHideEffectID, 1, UnHideEffectDuration, UnHideEffectHue-1, UnHideEffectRenderMode, 0, 0 );
				}
			}
		}

		private class HideTimer : Timer
		{
			private Mobile from;
			private UltimateHider uh;

			public HideTimer( Mobile m_From, UltimateHider m_UH ) : base( TimeSpan.FromSeconds( 2.0 ) )
			{
				from = m_From;
				uh = m_UH;
			}

			protected override void OnTick()
			{
				from.Hidden = true;
				Stop();
			}
		}
		private class UnHideTimer : Timer
		{
			private Mobile from;
			private UltimateHider uh;

			public UnHideTimer( Mobile m_From, UltimateHider m_UH ) : base( TimeSpan.FromSeconds( 3.0 ) )
			{
				from = m_From;
				uh = m_UH;
			}

			protected override void OnTick()
			{
				from.Hidden = false;
				from.Emote( "*"+ uh.UnHideSaying +"*" );
				Stop();
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_UnHideEffectID );
			writer.Write( (int) m_UnHideEffectHue );
			writer.Write( (int) m_UnHideEffectDuration );
			writer.Write( (int) m_UnHideEffectRenderMode );
			writer.Write( (int) m_HideEffectID );
			writer.Write( (int) m_HideEffectHue );
			writer.Write( (int) m_HideEffectDuration );
			writer.Write( (int) m_HideEffectRenderMode );
			writer.Write( (int) m_UnHideSound );
			writer.Write( (int) m_HideSound );
			writer.Write( (string) m_HideSaying );
			writer.Write( (string) m_UnHideSaying );
			writer.Write( (bool) m_AreaEffect );
			writer.Write( (bool) m_UnHideDelay );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_UnHideEffectID = reader.ReadInt(); 
			m_UnHideEffectHue = reader.ReadInt(); 
			m_UnHideEffectDuration = reader.ReadInt(); 
			m_UnHideEffectRenderMode = reader.ReadInt(); 
			m_HideEffectID = reader.ReadInt(); 
			m_HideEffectHue = reader.ReadInt(); 
			m_HideEffectDuration = reader.ReadInt(); 
			m_HideEffectRenderMode = reader.ReadInt(); 
			m_HideSound = reader.ReadInt(); 
			m_UnHideSound = reader.ReadInt(); 
			m_HideSaying = reader.ReadString(); 
			m_UnHideSaying = reader.ReadString(); 
			m_AreaEffect = reader.ReadBool();
			m_UnHideDelay = reader.ReadBool();
		}

		private class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;

			public InternalItem( Point3D loc, Map map ) : base( 0x2199 )//Invisible LOSBlocker ID
			{
				Movable = false;
				MoveToWorld( loc, map );
				if ( Deleted )
					return;
				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( 4.0 ) );
				m_Timer.Start();

				m_End = DateTime.Now + TimeSpan.FromSeconds( 4.0 );
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version

				writer.Write( m_End - DateTime.Now );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				switch ( version )
				{
					case 0:
					{
						TimeSpan duration = reader.ReadTimeSpan();

						m_Timer = new InternalTimer(this, duration );


						m_Timer.Start();

						m_End = DateTime.Now + duration;

						break;
					}
				}
			}
			private class InternalTimer : Timer
			{
				private InternalItem m_Item;

				public InternalTimer( InternalItem item, TimeSpan duration ) : base( duration )
				{
					m_Item = item;
				}
				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}
	}
}