	using System;
    
    namespace Server.Items
    {
    	public class Eggs19 : Item
    	{
    	
		[Constructable]
		public Eggs19() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1170;
            Name = "19 - The next basket is Meerly that far away.";
            
		}

		public Eggs19( Serial serial ) : base( serial )
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
    	

  
    
    
