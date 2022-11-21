using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class CowBola : Item
	{
		[Constructable]
		public CowBola() : this( 1 )
		{
		}

		[Constructable]
		public CowBola( int amount ) : base( 0x2103 )
		{
			Weight = 4.0;
			LootType = LootType.Blessed;
			Stackable = false;
			Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "The cow mut be in your pack to use it." ); // The CowBola must be in your pack to use it.
			}
			else if ( !from.CanBeginAction( typeof( CowBola ) ) )
			{
				from.SendMessage( "You must wait a few moments before you throw another cow." ); // You have to wait a few moments before you can use another CowBola!
			}
			else if ( from.Target is CowBolaTarget )
			{
				from.SendMessage( "This cow is already being used!" ); // This CowBola is already being used.
			}
			else if ( !Core.AOS && (from.FindItemOnLayer( Layer.OneHanded ) != null || from.FindItemOnLayer( Layer.TwoHanded ) != null) )
			{
				from.SendLocalizedMessage( 1040015 ); // Your hands must be free to use this
			}
			else if ( from.Mounted )
			{
				from.SendLocalizedMessage( 1040016 ); // You cannot use this while riding a mount
			}
			else if ( Server.Spells.Ninjitsu.AnimalForm.UnderTransformation( from ) )
			{
				from.SendLocalizedMessage( 1070902 ); // You can't use this while in an animal form!
			}
			else
			{
				EtherealMount.StopMounting( from );
				Item one = from.FindItemOnLayer( Layer.OneHanded );
				Item two = from.FindItemOnLayer( Layer.TwoHanded );

				if ( one != null )
					from.AddToBackpack( one );

				if ( two != null )
					from.AddToBackpack( two );

				from.Target = new CowBolaTarget( this );
				from.PrivateOverheadMessage( MessageType.Emote, 0x3B2, false, "You begin to swing the cow over your head.", from.NetState );
				from.NonlocalOverheadMessage( MessageType.Emote, 0x3B2, 1049633, from.Name ); // ~1_NAME~ begins to menacingly swing a CowBola...
			}
		}

		private static void ReleaseCowBolaLock( object state )
		{
			((Mobile)state).EndAction( typeof( CowBola ) );
		}

		private static void FinishThrow( object state )
		{
			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			Mobile to = (Mobile)states[1];

			if ( Core.AOS )
				new CowBola().MoveToWorld( to.Location, to.Map );

			to.Damage( 1, from );

                  if ( to is ChaosDragoon || to is ChaosDragoonElite )
				from.SendLocalizedMessage( 1042047 ); // You fail to knock the rider from its mount.

			IMount mt = to.Mount;
			if ( mt != null && !( to is ChaosDragoon || to is ChaosDragoonElite ) )
				mt.Rider = null;

                to.SendLocalizedMessage( 1040023 ); // You have been knocked off of your mount!
			BaseMount.SetMountPrevention( to, BlockMountType.Dazed, TimeSpan.FromSeconds( 3.0 ) );
			Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( ReleaseCowBolaLock ), from );
		}

		private class CowBolaTarget : Target
		{
			private CowBola m_CowBola;

			public CowBolaTarget( CowBola CowBola ) : base( 8, false, TargetFlags.Harmful )
			{
				m_CowBola = CowBola;
			}

			protected override void OnTarget( Mobile from, object obj )
			{
				if ( m_CowBola.Deleted )
					return;

				if ( obj is Mobile )
				{
					Mobile to = (Mobile)obj;

					if ( !m_CowBola.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "The cow must be in your backpack to use it." ); // The CowBola must be in your pack to use it.
					}
					else if ( !Core.AOS && (from.FindItemOnLayer( Layer.OneHanded ) != null || from.FindItemOnLayer( Layer.TwoHanded ) != null) )
					{
						from.SendLocalizedMessage( 1040015 ); // Your hands must be free to use this
					}
					else if ( from.Mounted )
					{
						from.SendLocalizedMessage( 1040016 ); // You cannot use this while riding a mount
					}
					else if ( Server.Spells.Ninjitsu.AnimalForm.UnderTransformation( from ) )
					{
						from.SendLocalizedMessage( 1070902 ); // You can't use this while in an animal form!
					}
					else if ( !to.Mounted )
					{
						from.SendMessage( "You have no reason to throw a cow at that!" ); // You have no reason to throw a CowBola at that.
					}
					else if ( !from.CanBeHarmful( to ) )
					{
					}
					else if ( from.BeginAction( typeof( CowBola ) ) )
					{
						EtherealMount.StopMounting( from );
						Item one = from.FindItemOnLayer( Layer.OneHanded );
						Item two = from.FindItemOnLayer( Layer.TwoHanded );

						if ( one != null )
							from.AddToBackpack( one );

						if ( two != null )
							from.AddToBackpack( two );

						from.DoHarmful( to );

						if ( Core.AOS )
							BaseMount.SetMountPrevention( from, BlockMountType.BolaRecovery, TimeSpan.FromSeconds( 3.0 ) );

						m_CowBola.Consume();

						from.Direction = from.GetDirectionTo( to );
						from.Animate( 11, 5, 1, true, false, 0 );
						from.MovingEffect( to, 0x2103, 10, 0, false, false );

						Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), new TimerStateCallback( FinishThrow ), new object[]{ from, to } );
					}
					else
					{
						from.SendMessage( "You must wait as few moments before you can throw another cow." ); // You have to wait a few moments before you can use another CowBola!
					}
				}
				else
				{
					from.SendMessage( "You can't throw a cow a that!" ); // You cannot throw a CowBola at that.
				}
			}
		}

		public CowBola( Serial serial ) : base( serial )
		{
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

			if ( LootType == LootType.Regular );
				LootType = LootType.Blessed;
		}
	}
}