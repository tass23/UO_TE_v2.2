	using System;
    
    namespace Server.Items
    {
    	public class Eggs11 : Item
    	{
    	
		[Constructable]
		public Eggs11() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1266;
            Name = "11 - A Bloodletter would say to 'Let it Bleed'.";
            
		}

		public Eggs11( Serial serial ) : base( serial )
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
    	

  
    
    
