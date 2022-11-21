	using System;
    
    namespace Server.Items
    {
    	public class Eggs13 : Item
    	{
    	
		[Constructable]
		public Eggs13() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1266;
            Name = "13 - Sometimes deserts can show compassion too.";
            
		}

		public Eggs13( Serial serial ) : base( serial )
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
    	

  
    
    
