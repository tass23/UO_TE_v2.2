using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13B9, 0x13Ba )]
	public class HorsemanSword : BaseSword
	{
		public virtual int Lifespan{ get{ return 21600; } }
		//public override int Lifespan{ get{ return 21600; } }
		
		private int m_Lifespan;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int TimeLeft
		{
			get{ return m_Lifespan; }
			set{ m_Lifespan = value; InvalidateProperties(); }
		}
	
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 28; } }
		public override float MlSpeed{ get{ return 3.75f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 6; } }
		public override int OldMaxDamage{ get{ return 34; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		[Constructable]
		public HorsemanSword() : base( 0x13B9 )
		{
			if ( Lifespan > 0 )
			{
				m_Lifespan = Lifespan;
				StartTimer();
			}
			Weight = 6.0;
			WeaponAttributes.HitLeechHits = 50;
			WeaponAttributes.HitLeechStam = 50;
			WeaponAttributes.SelfRepair = -15;
			Attributes.Luck = -666;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 80;
			Name = "The Horseman's Claymore";
			Hue = 886;
			Slayer = SlayerName.Exorcism;
		}
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = fire = pois = cold = chaos = direct = 0;
			nrgy = 200;
		}

		public HorsemanSword( Serial serial ) : base( serial )
		{
		}
		private Timer m_Timer;		
		
		public virtual void StartTimer()
		{
			if ( m_Timer != null )
				return;
				
			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 10 ), TimeSpan.FromSeconds( 10 ), new TimerCallback( Slice ) );
			m_Timer.Priority = TimerPriority.OneSecond;
		}
		
		public virtual void StopTimer()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}
		
		public virtual void Slice()
		{
			m_Lifespan -= 10;
			
			InvalidateProperties();
			
			if ( m_Lifespan <= 0 )
				Decay();
		}
		
		public virtual void Decay()
		{
			if ( RootParent is Mobile )
			{
				Mobile parent = (Mobile) RootParent;
				
				if ( Name == null )
					parent.SendLocalizedMessage( 1072515, "#" + LabelNumber ); // The ~1_name~ expired...
				else
					parent.SendLocalizedMessage( 1072515, Name ); // The ~1_name~ expired...
					
				Effects.SendLocationParticles( EffectItem.Create( parent.Location, parent.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( parent.Location, parent.Map, 0x201 );
			}
			else
			{
				Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( Location, Map, 0x201 );
			}			
			
			StopTimer();
			Delete();
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			if ( Lifespan > 0 )
				list.Add( 1072517, m_Lifespan.ToString() ); // Lifespan: ~1_val~ seconds
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