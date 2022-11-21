	using System;
    
    namespace Server.Items
    {
    	public class Eggs12 : Item
    	{
    	
		[Constructable]
		public Eggs12() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1266;
            Name = "12 - Sometimes going to Tokuno is a waste.";
            
		}

		public Eggs12( Serial serial ) : base( serial )
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
    	

  
    
    
