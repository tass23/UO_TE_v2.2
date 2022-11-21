	using System;
    
    namespace Server.Items
    {
    	public class Eggs17 : Item
    	{
    	
		[Constructable]
		public Eggs17() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1170;
            Name = "17 - Do you know when to hold 'em?";
            
		}

		public Eggs17( Serial serial ) : base( serial )
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
    	

  
    
    
