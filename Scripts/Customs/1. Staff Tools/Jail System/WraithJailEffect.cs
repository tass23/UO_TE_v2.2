using System;
using Server; 
using Server.Mobiles;
using Server.Misc; 
using Server.Items; 
using Server.Targeting;
using Server.Scripts.Commands;
using Server.Network; 
using Server.Multis;

namespace Server.Commands
{
	public class WraithJailEffect
	{
		private PlayerMobile m_prisoner;
		private PlayerMobile m_jailor;
		public Mobile Prisoner
		{
			get{return m_prisoner;}
		}
		public WraithJailEffect(PlayerMobile prisoner, PlayerMobile jailor)
		{
			m_jailor=jailor;
			m_prisoner=prisoner;
			((Mobile)m_prisoner).CantWalk=true;
			((Mobile)m_prisoner).Squelched=true;
			Effects.PlaySound( jailor.Location, jailor.Map, 0x1DD );

			Point3D loc = new Point3D( prisoner.X, prisoner.Y,prisoner.Z );
			int mushx;
			int mushy;
			int mushz;
				

			InternalItem firstFlamea = new InternalItem( prisoner.Location, prisoner.Map, jailor );
			mushx=loc.X-2;
			mushy=loc.Y-2;
			mushz=loc.Z;
			Point3D mushxyz = new Point3D(mushx,mushy,mushz);
			firstFlamea.MoveToWorld( mushxyz, prisoner.Map );
         	
			InternalItem firstFlamec = new InternalItem( prisoner.Location, prisoner.Map, jailor );
			mushx=loc.X;
			mushy=loc.Y-3;
			mushz=loc.Z;
			Point3D mushxyzb = new Point3D(mushx,mushy,mushz);
			firstFlamec.MoveToWorld( mushxyzb, prisoner.Map );
         	
			InternalItem firstFlamed = new InternalItem( prisoner.Location, prisoner.Map, jailor );
			firstFlamed.ItemID=0x3709;
			mushx=loc.X+2;
			mushy=loc.Y-2;
			mushz=loc.Z;
			Point3D mushxyzc = new Point3D(mushx,mushy,mushz);
			firstFlamed.MoveToWorld( mushxyzc, prisoner.Map );
			InternalItem firstFlamee = new InternalItem( prisoner.Location, prisoner.Map, jailor );
			mushx=loc.X+3;
			firstFlamee.ItemID=0x3709;
			mushy=loc.Y;
			mushz=loc.Z;
			Point3D mushxyzd = new Point3D(mushx,mushy,mushz);
			firstFlamee.MoveToWorld( mushxyzd, prisoner.Map );
			InternalItem firstFlamef = new InternalItem( prisoner.Location, prisoner.Map, jailor );
			firstFlamef.ItemID=0x3709;
			mushx=loc.X+2;
			mushy=loc.Y+2;
			mushz=loc.Z;
			Point3D mushxyze = new Point3D(mushx,mushy,mushz);
			firstFlamef.MoveToWorld( mushxyze, prisoner.Map );
			InternalItem firstFlameg = new InternalItem( prisoner.Location, prisoner.Map, jailor );
			mushx=loc.X;
			firstFlameg.ItemID=0x3709;
			mushy=loc.Y+3;
			mushz=loc.Z;
			Point3D mushxyzf = new Point3D(mushx,mushy,mushz);
			firstFlameg.MoveToWorld( mushxyzf, prisoner.Map );
			InternalItem firstFlameh = new InternalItem( prisoner.Location, prisoner.Map, jailor );
			mushx=loc.X-2;
			firstFlameh.ItemID=0x3709;
			mushy=loc.Y+2;
			mushz=loc.Z;
			Point3D mushxyzg = new Point3D(mushx,mushy,mushz);
			firstFlameh.MoveToWorld( mushxyzg, prisoner.Map );
			InternalItem firstFlamei = new InternalItem( prisoner.Location, prisoner.Map, jailor );
			mushx=loc.X-3;
			firstFlamei.ItemID=0x3709;
			mushy=loc.Y;
			mushz=loc.Z;
			Point3D mushxyzh = new Point3D(mushx,mushy,mushz);
			firstFlamei.MoveToWorld( mushxyzh, prisoner.Map );
			new JailWraith(this,prisoner.X+15,prisoner.Y+15,m_jailor);
		}
		public void jail()
		{
			JailSystem.Jail((m_prisoner as Mobile),TimeSpan.FromDays(2), "Interefering with a Role-Playing event.", true, (m_jailor as Mobile).Name,AccessLevel.Seer);
			((Mobile)m_prisoner).CantWalk=false;
			((Mobile)m_prisoner).Squelched=false;
			(m_prisoner as Mobile).SendMessage ("You are now in jail for disrupting an event.  Do not expect to see the staff member who jailed you until after the event has ended.");
		}
		public static void Initialize()
		{
			CommandSystem.Register( "jailwraith", AccessLevel.GameMaster, new CommandEventHandler( jail_OnCommand ) );
		}
		[Usage( "jailwraith" )]
		[Description( "Places the selected player in jail by a wraith." )]
		public static void jail_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.Target = new InternalTarget();
				e.Mobile.SendLocalizedMessage( 3000218 );
			}
		}
		private class InternalTarget : Target
		{
			public InternalTarget() : base( -1, false, TargetFlags.None )
			{
			}
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( from is PlayerMobile && targeted is PlayerMobile )
				{
					new WraithJailEffect(targeted as PlayerMobile, from as PlayerMobile);
				}
			}
		}
		private class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;
			private Mobile m_Caster;

			public override bool BlocksFit{ get{ return true; } }

			public InternalItem( Point3D loc, Map map, Mobile caster ) : base( 0x3709 )
			{
				Visible = false;
				Movable = false;
				Light=LightType.Circle150;
				MoveToWorld( loc, map );
				m_Caster=caster;
				Visible = true;
				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( 30.0 ) );
				m_Timer.Start();

				m_End = DateTime.Now + TimeSpan.FromSeconds( 30.0 );
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}
			
			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 1 ); // version

				writer.Write( m_End - DateTime.Now );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				switch ( version )
				{
					case 1:
					{
						TimeSpan duration = reader.ReadTimeSpan();

						m_Timer = new InternalTimer( this, duration );
						m_Timer.Start();

						m_End = DateTime.Now + duration;

						break;
					}
					case 0:
					{
						TimeSpan duration = TimeSpan.FromSeconds( 10.0 );

						m_Timer = new InternalTimer( this, duration );
						m_Timer.Start();

						m_End = DateTime.Now + duration;

						break;
					}
				}
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
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
	[CorpseName( "a errie corpse" )]
	public class JailWraith : BaseCreature
	{
		private WraithJailEffect m_effect;
		private bool drag=false;
		private int m_endPointX;
		private int m_endPointY;
		
		public JailWraith(WraithJailEffect effect, int endPointX, int endPointY, Mobile m_jailor) : base( AIType.AI_Use_Default, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			m_endPointY=endPointY;
			m_endPointX=endPointX;
			m_effect=effect;
			Name = "a soulless demon";
			Body = 26;
			Hue = 0x4001;
			BaseSoundID = 0x482;
			this.Blessed=true;

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 28;
			this.CantWalk=true;
			new InternalTimer(this);
			this.X=m_jailor.X;
			this.Y=m_jailor.Y;
			this.Map=m_jailor.Map;
			
		}
	
		public JailWraith( Serial serial ) : base( serial )
		{
		}
		protected override void OnLocationChange(Point3D loc)
		{
			base.OnLocationChange(loc);
			m_effect.Prisoner.Hidden=false;
			this.Stam=1000;
			if (drag) 
			{
				this.Direction=this.GetDirectionTo(m_endPointX,m_endPointY);
				if ((this.X==m_endPointX) && (this.Y==m_endPointY))
				{
					this.m_effect.jail();
					this.Delete();
				}
				else
				{
					if (!(m_effect.Prisoner.Region is Regions.Jail))
					{
						m_effect.Prisoner.Location=loc;
					}
				}
			}
			else
			{
				if ((this.X==m_effect.Prisoner.X)&&(this.Y==m_effect.Prisoner.Y))
				{
					this.Direction=this.GetDirectionTo(m_endPointX,m_endPointY);
					m_effect.Prisoner.Kill();
					m_effect.Prisoner.Hidden=false;
					drag=true;
					this.PlaySound(this.GetAngerSound());
				}
				else
					this.Direction=this.GetDirectionTo(m_effect.Prisoner.Location);

			}
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
		private class InternalTimer : Timer
		{
			private JailWraith m_Item;

			public InternalTimer(JailWraith item  ) : base( TimeSpan.FromMilliseconds(200),TimeSpan.FromMilliseconds(200) )
			{
				m_Item = item;
				this.Start();
				this.Priority=TimerPriority.FiftyMS;

			}

			protected override void OnTick()
			{
				//yeah baby move me
				if (m_Item.Deleted) this.Stop();
				int x;
				int y;
				x=m_Item.X;
				y=m_Item.Y;
				switch (m_Item.Direction) 
				{
					case Direction.North:
						x = x - 1;
						break;
					case Direction.Left:
						y++;
						x = x - 1;
						break;
					case Direction.West :
						y++;
						break;
					case Direction.Down:
						y++;
						x++;
						break;
					case Direction.South:
						x++;
						break;
					case Direction.Right:
						y = y - 1;
						x++;
						break;
					case Direction.East:
						y = y - 1;
						break;
					case Direction.Up:
						y = y - 1;
						x = x - 1;
						break;
					default:
						break;
				}
				m_Item.Location=new Point3D(x,y,m_Item.Z);
			}
		}
	}
}