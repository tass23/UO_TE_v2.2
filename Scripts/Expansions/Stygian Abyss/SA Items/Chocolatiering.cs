using System;
using Server;
using System.Collections.Generic;

namespace Server.Items
{
	public class Chocolate : Food
	{
		private static Dictionary<Mobile, ChocolateTimer> m_ToothAches;

		public static Dictionary<Mobile, ChocolateTimer> ToothAches
		{
			get { return m_ToothAches; }
			set { m_ToothAches = value; }
		}

		public static void Initialize()
		{
			m_ToothAches = new Dictionary<Mobile, ChocolateTimer>();
		}

		public class ChocolateTimer : Timer
		{
			private int m_Eaten;
			private Mobile m_Eater;

			public Mobile Eater { get { return m_Eater; } }
			public int Eaten { get { return m_Eaten; } set { m_Eaten = value; } }

			public ChocolateTimer( Mobile eater ) : base( TimeSpan.FromSeconds( 30 ), TimeSpan.FromSeconds( 30 ) )
			{
				m_Eater = eater;
				Priority = TimerPriority.FiveSeconds;
				Start();
			}

			protected override void OnTick()
			{
				Eaten--;

				if ( Eater == null || Eater.Deleted ||  Eaten <= 0 )
				{
					Stop();
					ToothAches.Remove( Eater );
				}
				else if ( Eater.Map != Map.Internal && Eater.Alive )
				{
					if( Eaten > 60 )
					{
						Eater.Say( 1077388  + Utility.Random(5) ); // ARRGH! My tooth hurts sooo much!, etc.

						if( Utility.RandomBool() )
						{
							Eater.Animate( 32, 5, 1, true, false, 0 );
						}
					}
					else if ( Eaten == 60 )
					{
						Eater.SendLocalizedMessage( 1077393 ); // The extreme pain in your teeth subsides.
					}
				}
			}
		}
		
		[Constructable]
		public Chocolate() : base( 0x0F10 )
		{
			Hue = 0x465;
			Stackable = false;
			LootType=LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( IsChildOf( from.Backpack ) || from.InRange(this, 1) )
			{
				from.PlaySound( 0x3a + Utility.Random(3) ); 
				from.Animate( 34, 5, 1, true, false, 0 );

				if ( !ToothAches.ContainsKey( from ) )
				{
					ToothAches.Add( from, new ChocolateTimer( from ) );
				}

				ToothAches[from].Eaten += 32;

				from.SendLocalizedMessage( 1077387 ); // You feel as if you could eat as much as you wanted!
				Delete();
			}
		}

		public Chocolate( Serial serial ) : base( serial )
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
	
	public class DarkChocolate : Chocolate
	{
		public override int LabelNumber { get { return 1079994; } } // Dark chocolate
		public override double DefaultWeight { get { return 1.0; } }

		[Constructable]
		public DarkChocolate()
		{
			Hue = 0x465;
			LootType = LootType.Regular;
		}

		public DarkChocolate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class MilkChocolate : Chocolate
	{
		public override int LabelNumber { get { return 1079995; } } // Milk chocolate
		public override double DefaultWeight { get { return 1.0; } }

		[Constructable]
		public MilkChocolate()
		{
			Hue = 0x461;
			LootType = LootType.Regular;
		}

		public MilkChocolate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WhiteChocolate : Chocolate
	{
		public override int LabelNumber { get { return 1079996; } } // White chocolate
		public override double DefaultWeight { get { return 1.0; } }

		[Constructable]
		public WhiteChocolate()
		{
			Hue = 0x47E;
			LootType = LootType.Regular;
		}

		public WhiteChocolate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class CocoaLiquor : Item
	{
		public override int LabelNumber { get { return 1080007; } } // Cocoa liquor
		public override double DefaultWeight { get { return 1.0; } }

		[Constructable]
		public CocoaLiquor()
			: base( 0x103F )
		{
			Hue = 0x46A;
		}

		public CocoaLiquor( Serial serial )
			: base( serial )
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

	public class SackOfSugar : Item
	{
		public override int LabelNumber { get { return 1080003; } } // Sack of sugar
		public override double DefaultWeight { get { return 1.0; } }

		[Constructable]
		public SackOfSugar()
			: this( 1 )
		{
		}

		[Constructable]
		public SackOfSugar( int amount )
			: base( 0x1039 )
		{
			Hue = 0x461;
			Stackable = true;
			Amount = amount;
		}

		public SackOfSugar( Serial serial )
			: base( serial )
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

	public class CocoaButter : Item
	{
		public override int LabelNumber { get { return 1080005; } } // Cocoa butter
		public override double DefaultWeight { get { return 1.0; } }

		[Constructable]
		public CocoaButter()
			: base( 0x1044 )
		{
			Hue = 0x457;
		}

		public CocoaButter( Serial serial )
			: base( serial )
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

	public class Vanilla : Item
	{
		public override int LabelNumber { get { return 1080009; } } // Vanilla
		public override double DefaultWeight { get { return 1.0; } }

		[Constructable]
		public Vanilla()
			: this( 1 )
		{
		}

		[Constructable]
		public Vanilla( int amount )
			: base( 0xE2A )
		{
			Hue = 0x462;
			Stackable = true;
			Amount = amount;
		}

		public Vanilla( Serial serial )
			: base( serial )
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

	public class CocoaPulp : Item
	{
		public override int LabelNumber { get { return 1080530; } } // cocoa pulp
		public override double DefaultWeight { get { return 1.0; } }

		[Constructable]
		public CocoaPulp()
			: this( 1 )
		{
		}

		[Constructable]
		public CocoaPulp( int amount )
			: base( 0xF7C )
		{
			Hue = 0x219;
			Stackable = true;
			Amount = amount;
		}

		public CocoaPulp( Serial serial )
			: base( serial )
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