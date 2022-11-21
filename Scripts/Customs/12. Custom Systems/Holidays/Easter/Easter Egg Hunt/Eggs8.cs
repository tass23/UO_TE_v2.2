	using System;
    
    namespace Server.Items
    {
    	public class Eggs8 : Item
    	{
    	
		[Constructable]
		public Eggs8() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1060;
            Name = "8 - The end of the road for those not patched.";
            
		}

		public Eggs8( Serial serial ) : base( serial )
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
    	

  
    
    
