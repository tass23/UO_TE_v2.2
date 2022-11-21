using System;
using Server.Targeting;
using Server.HuePickers;

namespace Server.Items
{
	public class FoodDyes : Item
	{
		[Constructable]
		public FoodDyes() : base( 0xFA9 )
		{
			Name = "food dyes";
		}

		public FoodDyes( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize(GenericReader reader) { base.Deserialize( reader ); int version = reader.ReadInt(); }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "Select the mixture to use the food dyes on." );
			from.Target = new InternalTarget();
		}

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 1, false, TargetFlags.None )
			{
			}

			private class InternalPicker : HuePicker
			{
				private Item m_Mixture;

				public InternalPicker( Item mixture ) : base( mixture.ItemID )
				{
					m_Mixture = mixture;
				}

				public override void OnResponse( int hue )
				{
					m_Mixture.Hue = hue;
					m_Mixture.InvalidateProperties();
				}
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is CakeMix || targeted is CookieMix || targeted is FruitCakeMix || targeted is MeatCakeMix || targeted is VegetableCakeMix )
				{
					Item mixture = (Item) targeted;

					if ( mixture.Hue == 0 )
					{
						from.SendHuePicker( new InternalPicker( mixture ) );
					}
					else
					{
						from.SendMessage( "That mixture is already dyed." );
					}
				}

				else
				{
					from.SendMessage( "Use this on an undyed mixture." );
				}
			}
		}
	}
}
