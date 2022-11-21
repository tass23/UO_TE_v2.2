	using System;
    
    namespace Server.Items
    {
    	public class Bunny1 : Item
    	{
    	
		[Constructable]
		public Bunny1() : base( 0x2125 )
		{
			Weight = 0.1;
			Hue = 1060;
            Name = "Easter Bunny - Level 1 Complete";
            
		}

		public Bunny1( Serial serial ) : base( serial )
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
    	

  
    
    
