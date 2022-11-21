	using System;
    
    namespace Server.Items
    {
    	public class Eggs5 : Item
    	{
    	
		[Constructable]
		public Eggs5() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1060;
            Name = "5 - Ninja is the art of invisibility.";
            
		}

		public Eggs5( Serial serial ) : base( serial )
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
    	

  
    
    
