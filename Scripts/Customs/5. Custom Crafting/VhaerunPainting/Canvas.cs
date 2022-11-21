using System;

namespace Server.Items
{
	public class Canvas : Item
	{
		[Constructable]
		public Canvas() : base( 0xF72 )
		{
		      	Weight = 12.0;
			Stackable = false;
			Name = "Canvas";
		}

		public Canvas( Serial serial ) : base( serial )
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