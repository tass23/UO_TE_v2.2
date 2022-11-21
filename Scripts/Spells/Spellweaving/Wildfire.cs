using System;
using System.Collections.Generic;
using Server.Targeting;

namespace Server.Spells.Spellweaving
{
	public class WildfireSpell : ArcanistSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
                "Wildfire", "Haelyn",
				-1,				
                false
			);
			
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.5 ); } }

        public override double RequiredSkill { get { return 66.0; } }
        public override int RequiredMana { get { return 50; } }

        public WildfireSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Point3D p )
		{
			if( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if( CheckSequence() )
			{
				int level = GetFocusLevel( Caster );
				double skill = Caster.Skills[ CastSkill ].Value;

				int tiles = 2 + level;
				int damage = 15 + level;
				int duration = (int) Math.Max( 1, skill / 24 ) + level; 
				
				for ( int x = p.X - tiles; x <= p.X + tiles; x += tiles )
				{
					for ( int y = p.Y - tiles; y <= p.Y + tiles; y += tiles )
					{					
						if ( p.X == x && p.Y == y )
							continue;
							
						Point3D p3d = new Point3D( x, y, Caster.Map.GetAverageZ( x, y ) );
					
						if ( Caster.Map.CanFit( p3d, 12, true, false ) )
							new FireItem( duration ).MoveToWorld( p3d, Caster.Map );
					}
				}
				
				Effects.PlaySound( p, Caster.Map, 0x5CF );
				
				new InternalTimer( Caster, p, damage, tiles, duration ).Start();
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private WildfireSpell m_Owner;

			public InternalTarget( WildfireSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile m, object o )
			{
				if( o is IPoint3D )
				{
					m_Owner.Target( new Point3D( (IPoint3D)o ) );
				}
			}

			protected override void OnTargetFinish( Mobile m )
			{
				m_Owner.FinishSequence();
			}
		}
		
		public class InternalTimer : Timer
		{			
			private Mobile m_Owner;
			private Point3D m_Location;
			private int m_Damage;
			private int m_Range;
			private int m_LifeSpan;
		
			public InternalTimer( Mobile owner, Point3D location, int damage, int range, int duration ) : base( TimeSpan.FromSeconds( 1 ), TimeSpan.FromSeconds( 1 ), duration )
			{
				m_Owner = owner;
				m_Location = location;
				m_Damage = damage;
				m_Range = range;
				m_LifeSpan = duration;
			}			
			
			protected override void OnTick()
			{			
				if ( m_Owner == null )
					return;
					
				m_LifeSpan -= 1;
					
				foreach( Mobile m in GetTargets() )
				{
					m_Owner.DoHarmful( m );
					
					// magic ressit?
					
					if ( m_Owner.Map.CanFit( m.Location, 12, true, false ) )
						new FireItem( m_LifeSpan ).MoveToWorld( m.Location, m.Map );
						
					Effects.PlaySound( m.Location, m.Map, 0x5CF );
					
					AOS.Damage( m, m_Owner, m_Damage, 0, 100, 0, 0, 0 );	
				}	
			}
			
			private List<Mobile> GetTargets()
			{
				List<Mobile> m_Targets = new List<Mobile>();
			
				foreach( Mobile m in m_Owner.Map.GetMobilesInRange( m_Location, m_Range ) )
				{
					if ( m != m_Owner && SpellHelper.ValidIndirectTarget( m_Owner, m ) && m_Owner.CanBeHarmful( m, false ) )
						m_Targets.Add( m );
				}
				
				return m_Targets;					
			}			
		}
			
		public class FireItem : Item
		{						
			public FireItem( int duration ) : base( Utility.RandomBool() ? 0x398C : 0x3996 )
			{
				Movable = false;
				Timer.DelayCall( TimeSpan.FromSeconds( duration ), new TimerCallback( Delete ) );
			}
			
			public FireItem( Serial serial ) : base( serial )
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
}