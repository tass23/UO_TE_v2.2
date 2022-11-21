	using System;
    
    namespace Server.Items
    {
    	public class Eggs15 : Item
    	{
    	
		[Constructable]
		public Eggs15() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1266;
            Name = "15 - You can look toward the Heavens, even underground.";
            
		}

		public Eggs15( Serial serial ) : base( serial )
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
    	

  
    
    
