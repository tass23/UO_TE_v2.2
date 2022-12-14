using System;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkFoldSpaceSpell : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Fold Space", "The true power of the Force is the Dark Side.",
		//SpellCircle.Fourth,
		239,
		9031,
		Reagent.DaemonBlood,
		Reagent.GraveDust
		);

        public override SpellCircle Circle
        {
            get { return SpellCircle.Fourth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
        public override double RequiredSkill { get { return 24; } }
        public override int RequiredMana { get { return 11; } }
		private RunebookEntry m_Entry;
		private Runebook m_Book;

		public DarkFoldSpaceSpell( Mobile caster, Item scroll ) : this( caster, scroll, null, null )
		{
		}

		public DarkFoldSpaceSpell( Mobile caster, Item scroll, RunebookEntry entry, Runebook book ) : base( caster, scroll, m_Info )
		{
			m_Entry = entry;
			m_Book = book;
		}

		public override void OnCast()
		{
			if ( m_Entry == null )
				Caster.Target = new InternalTarget( this );
			else
				Effect( m_Entry.Location, m_Entry.Map, true );
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
				if ( Caster.Criminal )
				{
					Caster.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily.
					return false;
				}
				else if ( SpellHelper.CheckCombat( Caster ) )
				{
					Caster.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
					return false;
				}
				else if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
				{
					Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
					return false;
				}
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return SpellHelper.CheckTravel( Caster, TravelCheckType.RecallFrom );
			}
		}

		public void Effect( Point3D loc, Map map, bool checkMulti )
		{
			if ( map == null || (!Core.AOS && Caster.Map != map) )
			{
				Caster.SendLocalizedMessage( 1005569 ); // You can not recall to another facet.
			}
			else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.RecallFrom ) )
			{
			}
			else if ( !SpellHelper.CheckTravel( Caster, map, loc, TravelCheckType.RecallTo ) )
			{
			}
			else if ( Caster.Kills >= 5 && map != Map.Felucca )
			{
				Caster.SendLocalizedMessage( 1019004 ); // You are not allowed to travel there.
			}
			else if ( Caster.Criminal )
			{
				Caster.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily.
			}
			else if ( SpellHelper.CheckCombat( Caster ) )
			{
				Caster.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
			}
			else if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
			{
				Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
			}
			else if ( !map.CanSpawnMobile( loc.X, loc.Y, loc.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( (checkMulti && SpellHelper.CheckMulti( loc, map )) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( m_Book != null && m_Book.CurCharges <= 0 )
			{
				Caster.SendLocalizedMessage( 502412 ); // There are no charges left on that item.
			}
			else if ( CheckSequence() )
			{
				BaseCreature.TeleportPets( Caster, loc, map, true );
				if ( m_Book != null )
					--m_Book.CurCharges;

				Caster.PlaySound( 143 );
				Caster.Map = map;
				Caster.Location = loc;
				Caster.FixedParticles( 0x3779, 1, 30, 9964, 3, 3, EffectLayer.Waist );

				IEntity from = new Entity( Serial.Zero, new Point3D( Caster.X, Caster.Y, Caster.Z ), Caster.Map );
				IEntity to = new Entity( Serial.Zero, new Point3D( Caster.X, Caster.Y, Caster.Z + 50 ), Caster.Map );
				Effects.SendMovingParticles( from, to, 0x20F2, 2, 1, false, false, 0, 3, 9501, 1, 0, EffectLayer.Head, 0x100 );
				Caster.PlaySound( 144 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private DarkFoldSpaceSpell m_Owner;

			public InternalTarget( DarkFoldSpaceSpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;

				owner.Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 501029 ); // Select Marked item.
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					RecallRune rune = (RecallRune)o;

					if ( rune.Marked )
						m_Owner.Effect( rune.Target, rune.TargetMap, true );
					else
						from.SendLocalizedMessage( 501805 ); // That rune is not yet marked.
				}
				else if ( o is Runebook )
				{
					RunebookEntry e = ((Runebook)o).Default;

					if ( e != null )
						m_Owner.Effect( e.Location, e.Map, true );
					else
						from.SendLocalizedMessage( 502354 ); // Target is not marked.
				}
				else if ( o is Key && ((Key)o).KeyValue != 0 && ((Key)o).Link is BaseBoat )
				{
					BaseBoat boat = ((Key)o).Link as BaseBoat;

					if ( !boat.Deleted && boat.CheckKey( ((Key)o).KeyValue ) )
						m_Owner.Effect( boat.GetMarkedLocation(), boat.Map, false );
					else
						from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 502357, from.Name, "" ) ); // I can not recall from that object.
				}
				else
				{
					from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 502357, from.Name, "" ) ); // I can not recall from that object.
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
