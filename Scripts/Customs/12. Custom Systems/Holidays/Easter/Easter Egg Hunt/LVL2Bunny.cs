	using System;
    
    namespace Server.Items
    {
    	public class Bunny2 : Item
    	{
    	
		[Constructable]
		public Bunny2() : base( 0x2125 )
		{
			Weight = 0.1;
			Hue = 1266;
            Name = "Easter Bunny - Level 2 Complete";
            
		}

		public Bunny2( Serial serial ) : base( serial )
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
    	

  
    
    
