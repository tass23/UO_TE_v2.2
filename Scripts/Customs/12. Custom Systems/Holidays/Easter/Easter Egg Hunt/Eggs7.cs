	using System;
    
    namespace Server.Items
    {
    	public class Eggs7 : Item
    	{
    	
		[Constructable]
		public Eggs7() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1060;
            Name = "7 - Skeletons, mongbats, zombies and llamas, oh my!";
            
		}

		public Eggs7( Serial serial ) : base( serial )
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
    	

  
    
    
