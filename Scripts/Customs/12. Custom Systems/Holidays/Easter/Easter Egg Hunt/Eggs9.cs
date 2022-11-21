	using System;
    
    namespace Server.Items
    {
    	public class Eggs9 : Item
    	{
    	
		[Constructable]
		public Eggs9() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1060;
            Name = "9 - Be a champ and find the next basket.";
            
		}

		public Eggs9( Serial serial ) : base( serial )
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
    	

  
    
    
