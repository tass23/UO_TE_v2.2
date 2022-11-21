	using System;
    
    namespace Server.Items
    {
    	public class Eggs2 : Item
    	{
    	
		[Constructable]
		public Eggs2() : base( 0x9B5 )
		{
			Weight = 0.2;
			Hue = 1060;
            Name = "2 - Over the river and through the woods, to the moongate you go!";
            
		}

		public Eggs2( Serial serial ) : base( serial )
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
    	

  
    
    
