using System;
using Server;

namespace Server.Items
{
	public class MedusaKey : MasterKey
	{
		//public override int LabelNumber{ get{ return 1113739; } } 
		public override int Lifespan{ get{ return 600; } }

        public MedusaKey(): base(0x1012)
		{
            Name = "Medusa's Lair";
			Hue = 0x481;
		}
		
		public MedusaKey( Serial serial ) : base( serial )
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
		
		public override bool CanOfferConfirmation( Mobile from )
		{
			if ( from.Region != null && from.Region.IsPartOf( "MedusasLair" ) )
				return base.CanOfferConfirmation( from );
				
			return false;
		}
	}
}