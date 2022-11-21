using System;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Spells;
using Server.Gumps;

//First Circle: 4
//Second Circle: 6
//Third Circle: 9
//Fourth Circle: 11
//Fifth Circle: 14
//Sixth Circle: 20
//Seventh Circle: 40
//Eight Circle: 50

namespace Server.ACC.CSS.Systems.LightForce
{
	public class GripSpell : LightForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Grip", "A Jedi uses the Force for defense.",
		//SpellCircle.Fifth,
		218,
		9012,
		false,
		CReagent.SpringWater,
		Reagent.Bloodmoss,
		Reagent.SpidersSilk
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Sixth; }
        }

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
		public override double RequiredSkill{ get{ return 52; } }
		public override int RequiredMana{ get{ return 20; } }

		public GripSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
		
		public override bool CheckCast()
		{
			if ( Caster.Karma < 5000 )
			{
				Caster.SendMessage( "You lack the Jedi power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else
			{
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return true;
			}
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				double duration;

				// Algorithm: ((20% of AnimalTamin) + 7) seconds [- 50% if resisted] seems to work??
				duration = 7.0 + (Caster.Skills[DamageSkill].Value * 0.2);

				// Resist if Str + Dex / 2 is greater than CastSkill eg. AnimalLore seems to work??
				if ( ( Caster.Skills[CastSkill].Value ) < ( ( m.Str + m.Dex ) * 0.5 ) )
					duration *= 0.5;

				// no less than 0 seconds no more than 9 seconds
				if ( duration < 2.0 )
					duration = 2.0;
				if ( duration > 10.0 )
					duration = 10.0;

				m.PlaySound( 0x229);

				m.Paralyze( TimeSpan.FromSeconds( duration ) );
				m.FixedParticles( 0x37BE, 2, 10, 5027, 0x3D, 2, EffectLayer.Waist );

				{
					Point3D loc = new Point3D( m.X, m.Y, m.Z );

					Item item = new InternalItem( loc, Caster.Map, Caster );
				}
			}

			FinishSequence();
		}

		private class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;

			public InternalItem( Point3D loc, Map map, Mobile caster ) : base( 0xC5F )
			{
				Visible = false;
				Movable = false;

				MoveToWorld( loc, map );

				if ( caster.InLOS( this ) )
					Visible = true;
				else
					Delete();

				if ( Deleted )
					return;

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
				TimeSpan duration = reader.ReadTimeSpan();

				m_Timer = new InternalTimer( this, duration );
				m_Timer.Start();

				m_End = DateTime.Now + duration;
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

		public class InternalTarget : Target
		{
			private GripSpell m_Owner;

			public InternalTarget( GripSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
