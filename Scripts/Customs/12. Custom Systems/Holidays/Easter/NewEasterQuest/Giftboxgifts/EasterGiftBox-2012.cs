using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x232A, 0x232B )]
	public class EasterGiftBox2012 : GiftBox
	{
		public int offset;
		
		[Constructable]
		public EasterGiftBox2012()
		{
			Name = "A Gift from The Easter Bunny - 2012";
			offset = Utility.Random( 0, 8 );
			
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

		public EasterGiftBox2012( Serial serial ) : base( serial )
		{
		}
		
		public override void GetProperties( ObjectPropertyList list )
	         {
	  	    base.GetProperties( list );

		    list.Add( 1007149 + offset ); 
    	     }

		public override void Serialize( GenericWriter writer ) 
	         {
	            base.Serialize( writer ); 

	            writer.Write( (int) 0 ); 
	            
	            writer.Write( (int) offset );
	         }
	
	         public override void Deserialize( GenericReader reader ) 
	         {
	            base.Deserialize( reader ); 

	            int version = reader.ReadInt(); 

		        offset = reader.ReadInt();
	         }
	}
}
