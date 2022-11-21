using System;
using Server;

namespace Server.Items
{
	public class ActivatingRod : Item
	{	
		[Constructable]
		public ActivatingRod() : base( 3570 )
		{
			Name = "Activating Rod";
			Weight = 1;
			Hue = 1478;
		}

		public ActivatingRod( Serial serial ) : base( serial )
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