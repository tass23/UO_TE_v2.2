	using System;
    
    namespace Server.Items
    {
    	public class Bunny3 : Item
    	{
    	
		[Constructable]
		public Bunny3() : base( 0x2125 )
		{
			Weight = 0.1;
			Hue = 1170;
            Name = "Easter Bunny - Level 3 Complete";
            
		}

		public Bunny3( Serial serial ) : base( serial )
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
    	

  
    
    
