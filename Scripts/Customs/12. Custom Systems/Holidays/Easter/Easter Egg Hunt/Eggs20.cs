	using System;
    
    namespace Server.Items
    {
    	public class Eggs20 : Item
    	{
    	
		[Constructable]
		public Eggs20() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1170;
            Name = "20 - I've heard of Queen Bees, but a Queen Ant? Seriously.";
            
		}

		public Eggs20( Serial serial ) : base( serial )
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
    	

  
    
    
