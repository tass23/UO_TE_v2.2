	using System;
    
    namespace Server.Items
    {
    	public class Eggs3 : Item
    	{
    	
		[Constructable]
		public Eggs3() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1060;
            Name = "3 - You'll have a shrining time at Uzeraan's Mansion.";
            
		}

		public Eggs3( Serial serial ) : base( serial )
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
    	

  
    
    
