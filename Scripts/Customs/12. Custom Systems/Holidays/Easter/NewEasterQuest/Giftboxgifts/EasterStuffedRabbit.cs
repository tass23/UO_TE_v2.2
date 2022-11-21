using System;
using Server;
using Server.Items;
namespace Server.Items
{
        
	public class EasterStuffedBunny :  Item
	{


		
		
                

                                [Constructable]
		public EasterStuffedBunny(): base( 0x2622 )
		{
			Weight = 1.0; 
            		Name = "Stuffed Easter Bunny 2010"; 
            		Hue = 2900;
                        ItemID = 9762;                                
                        
			}  


		public EasterStuffedBunny( Serial serial ) : base( serial )
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
}