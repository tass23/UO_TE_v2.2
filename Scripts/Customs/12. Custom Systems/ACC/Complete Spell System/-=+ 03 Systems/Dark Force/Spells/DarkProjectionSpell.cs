using System;
using System.Collections;
using Server.Targeting;
using Server.Regions;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Spells;
using Server.Spells.Third;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DarkProjectionSpell : DarkForceSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Projection", "Peace is a lie, there is only passion.",
        1621,
        9002,
        Reagent.NoxCrystal,
        Reagent.DaemonBlood
        );

        public override SpellCircle Circle
        {
            get { return SpellCircle.Third; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 10; } }
        public override int RequiredMana { get { return 9; } }
		
        public DarkProjectionSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override bool CheckCast()
        {
			if ( Caster.Karma > 4999 )
			{
				Caster.SendMessage( "You lack the Sith power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return false;
			}
			else
			{
				if (Caster.Mounted)
				{
					Caster.SendLocalizedMessage(1042561); //Please dismount first.
					return false;
				}
				else if ( Factions.Sigil.ExistsOn( Caster ) )
				{
					Caster.SendLocalizedMessage( 1061632 ); // You can't do that while carrying the sigil.
					return false;
				}
				else if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
				{
					Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
					return false;
				}
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return SpellHelper.CheckTravel( Caster, TravelCheckType.TeleportFrom );
			}
        }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			IPoint3D orig = p;
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop( ref p );

			if ( Factions.Sigil.ExistsOn( Caster ) )
			{
				Caster.SendLocalizedMessage( 1061632 ); // You can't do that while carrying the sigil.
			}
			else if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
			{
				Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
			}
			else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.TeleportFrom ) )
			{
			}
			else if ( !SpellHelper.CheckTravel( Caster, map, new Point3D( p ), TravelCheckType.TeleportTo ) )
			{
			}
			else if ( map == null || !map.CanSpawnMobile( p.X, p.Y, p.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( SpellHelper.CheckMulti( new Point3D( p ), map ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, orig );

				Mobile m = Caster;

				Point3D from = m.Location;
				Point3D to = new Point3D( p );

				m.Location = to;
				m.ProcessDelta();

				if ( m.Player )
				{
					Effects.SendLocationParticles( EffectItem.Create( from, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					Effects.SendLocationParticles( EffectItem.Create(   to, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );
				}
				else
				{
					m.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
				}

				m.PlaySound( 0x1FE );

				IPooledEnumerable eable = m.GetItemsInRange( 0 );

				foreach ( Item item in eable )
				{
					if ( item is Server.Spells.Sixth.ParalyzeFieldSpell.InternalItem || item is Server.Spells.Fifth.PoisonFieldSpell.InternalItem || item is Server.Spells.Fourth.FireFieldSpell.FireFieldItem )
						item.OnMoveOver( m );
				}

				eable.Free();
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private DarkProjectionSpell m_Owner;

			public InternalTarget( DarkProjectionSpell owner ) : base( Core.ML ? 11 : 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
    }
}
