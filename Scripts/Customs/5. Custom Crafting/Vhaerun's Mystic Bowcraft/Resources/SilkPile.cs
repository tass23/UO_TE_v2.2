using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class SilkPile : Item, IDyable
	{
		[Constructable]
		public SilkPile() : this( 1 )
		{
		}

		[Constructable]
		public SilkPile( int amount ) : base( 0xF8D )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
			Name = "Silk Pile";
			Hue = 0x482;
		}

		public SilkPile( Serial serial ) : base( serial )
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
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 502655 ); // What spinning wheel do you wish to spin this on?
				from.Target = new PickWheelTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		public static void OnSpun( ISpinningWheel wheel, Mobile from, int hue )
		{
			Item item = new SilkThread( 3 );
			item.Hue = hue;

			from.AddToBackpack( item );
			from.SendLocalizedMessage( 1010576 ); // You put the balls of yarn in your backpack.
		}

		private class PickWheelTarget : Target
		{
			private SilkPile m_SilkPile;

			public PickWheelTarget( SilkPile silkpile ) : base( 3, false, TargetFlags.None )
			{
				m_SilkPile = silkpile;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_SilkPile.Deleted )
					return;

				ISpinningWheel wheel = targeted as ISpinningWheel;

				if ( wheel == null && targeted is AddonComponent )
					wheel = ((AddonComponent)targeted).Addon as ISpinningWheel;

				if ( wheel is Item )
				{
					Item item = (Item)wheel;

					if ( !m_SilkPile.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
					else if ( wheel.Spinning )
					{
						from.SendLocalizedMessage( 502656 ); // That spinning wheel is being used.
					}
					else
					{
						m_SilkPile.Consume();
						wheel.BeginSpin( new SpinCallback( SilkPile.OnSpun ), from, m_SilkPile.Hue );
					}
				}
				else
				{
					from.SendLocalizedMessage( 502658 ); // Use that on a spinning wheel.
				}
			}
		}
	}
}