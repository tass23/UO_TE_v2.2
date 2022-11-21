	using System;
    
    namespace Server.Items
    {
    	public class Eggs16 : Item
    	{
    	
		[Constructable]
		public Eggs16() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 1170;
            Name = "16 - You won't get hot feet, but you can blow yourself up.";
            
		}

		public Eggs16( Serial serial ) : base( serial )
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
    	

  
    
    
