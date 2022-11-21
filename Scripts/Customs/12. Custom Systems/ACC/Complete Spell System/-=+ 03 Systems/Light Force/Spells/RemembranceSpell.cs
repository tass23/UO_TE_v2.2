using System;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Regions;
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
	public class RemembranceSpell : LightForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Remembrance", "The Jedi are the guardians of civilization.",
		//SpellCircle.Sixth,
		218,
		9002,
		CReagent.SpringWater,
		CReagent.PetrifiedWood,
		CReagent.DestroyingAngel
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Fourth; }
        }

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
		public override double RequiredSkill{ get{ return 24; } }
		public override int RequiredMana{ get{ return 11; } }

		public RemembranceSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return false;
			}
			else
			{
				if ( !base.CheckCast() )
					return false;
					
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return SpellHelper.CheckTravel( Caster, TravelCheckType.Mark );
			}
		}

		public void Target( RecallRune rune )
		{
			if ( !Caster.CanSee( rune ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.Mark ) )
			{
			}
			else if ( SpellHelper.CheckMulti( Caster.Location, Caster.Map, !Core.AOS ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( !rune.IsChildOf( Caster.Backpack ) )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1062422 ); // You must have this rune in your backpack in order to mark it.
			}
			else if ( CheckSequence() )
			{
				rune.Mark( Caster );
				Effects.SendLocationEffect( Caster, Caster.Map,  14186, 16 );
				Caster.PlaySound( 0x1FA );
				Effects.SendLocationEffect( Caster, Caster.Map,  14186, 16 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private RemembranceSpell m_Owner;

			public InternalTarget( RemembranceSpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					m_Owner.Target( (RecallRune) o );
				}
				else
				{
					from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501797, from.Name, "" ) ); // I cannot mark that object.
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
