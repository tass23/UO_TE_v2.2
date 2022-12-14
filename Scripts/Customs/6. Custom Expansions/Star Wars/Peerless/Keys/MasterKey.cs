using System;
using Server;

namespace Server.Items
{
	public class SWKey : MasterKey
	{
		public override int Lifespan{ get{ return 600; } }
		
		public SWKey() : base( 0xE26 )
		{
			Weight = 1.0;
			Hue = 0x481;
		}
		
		public SWKey( Serial serial ) : base( serial )
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
			if ( from.Region != null && from.Region.IsPartOf( "Valley of the Sith" ) )
				return base.CanOfferConfirmation( from );
				
			return false;
		}
	}
}