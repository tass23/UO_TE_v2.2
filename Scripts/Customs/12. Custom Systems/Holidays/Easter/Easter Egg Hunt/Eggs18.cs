	using System;
    
    namespace Server.Items
    {
    	public class Eggs18 : Item
    	{
    	
		[Constructable]
		public Eggs18() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1170;
            Name = "18 - 'We demand a shubbery! A nice one so we get the two-level effect.'";
            
		}

		public Eggs18( Serial serial ) : base( serial )
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
    	

  
    
    
