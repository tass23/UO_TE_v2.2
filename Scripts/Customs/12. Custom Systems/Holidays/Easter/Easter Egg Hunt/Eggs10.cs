	using System;
    
    namespace Server.Items
    {
    	public class Eggs10 : Item
    	{
    	
		[Constructable]
		public Eggs10() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1060;
            Name = "10 - Lots of neat things cropping up.";
            
		}

		public Eggs10( Serial serial ) : base( serial )
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
    	

  
    
    
