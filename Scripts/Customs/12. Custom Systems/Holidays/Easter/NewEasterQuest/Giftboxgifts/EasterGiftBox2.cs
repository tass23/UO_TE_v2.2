using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0x232A, 0x232B )]
	public class EasterGiftBox2 : BaseContainer
	{
		public override int DefaultGumpID{ get{ return 0x102; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 35, 10, 155, 85 ); }
		}

		[Constructable]
		public EasterGiftBox2() : this( Utility.RandomDyedHue() )
		{
		}

		[Constructable]
		public EasterGiftBox2( int hue ) : base( Utility.Random( 0x232A, 2 ) )
		{
			Weight = 2.0;
			Hue = hue;
                 Name = "A Gift from The Easter Bunny - 2012";
			
			
             switch ( Utility.Random( 8 ) )
             {
             	
             		
             	case 0:
             		DropItem( new  Dyedeggeaster2()  );break;
             	case 1:
             		DropItem( new EasterMarshmellowPeep() );break;
                case 2:
             		DropItem( new EasterStuffedBunny() );break;
                case 3:
             		DropItem( new LargeChocolateEasterBunny() );break;
                 case 4:
             		DropItem( new ReesesPeanutButterEgg() );break;
                  case 5:
             		DropItem( new BunnyTrailDecoAddonDeed() );break;
                 
             		

                                }
             	
		}

		public EasterGiftBox2( Serial serial ) : base( serial )
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