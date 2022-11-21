using System;
using Server.Spells;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
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
	public class KinesisSpell : LightForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Kinesis", "The Force can be used to manipulate objects.",
		203,
		9031,
		Reagent.Bloodmoss,
		Reagent.MandrakeRoot
		);
		
		public override SpellCircle Circle
        {
            get { return SpellCircle.Third; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
        public override double RequiredSkill { get { return 10; } }
        public override int RequiredMana { get { return 9; } }
		
		public KinesisSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

		public void Target( IKinesisable obj )
		{
			if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, obj );

				obj.OnKinesis( Caster );
			}

			FinishSequence();
		}

		public void Target( Container item )
		{
			if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, item );

				object root = item.RootParent;

				if ( !item.IsAccessibleTo( Caster ) )
				{
					item.OnDoubleClickNotAccessible( Caster );
				}
				else if ( !item.CheckItemUse( Caster, item ) )
				{
				}
				else if ( root != null && root is Mobile && root != Caster )
				{
					item.OnSnoop( Caster );
				}
				else if ( item is Corpse && !((Corpse)item).CheckLoot( Caster, null ) )
				{
				}
				else if ( Caster.Region.OnDoubleClick( Caster, item ) )
				{
					Effects.SendLocationParticles( EffectItem.Create( item.Location, item.Map, EffectItem.DefaultDuration ), 0x374A, 9, 32, 5022 );
					Effects.PlaySound( item.Location, item.Map, 0x1EB );

					item.DisplayTo( Caster );
					item.OnItemUsed( Caster, item );
				}
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private KinesisSpell m_Owner;

			public InternalTarget( KinesisSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IKinesisable )
					m_Owner.Target( (IKinesisable)o );
				else if ( o is Container )
					m_Owner.Target( (Container)o );
				else
					from.SendLocalizedMessage( 501857 ); // This spell won't work on that!
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}

namespace Server
{
	public interface IKinesisable : IPoint3D
	{
		void OnKinesis( Mobile from );
	}
}