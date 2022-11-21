using System;
using Server;

namespace Server.Items
{
	public class WaterRing : GoldRing
	{
		

		[Constructable]
		public WaterRing()
		{
			Hue = 48;
                        Name = "WaterRing";
		}

		public WaterRing( Serial serial ) : base( serial )
		{
		}
		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
			{
				((Mobile)parent).CanSwim = true;
			}
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent );

			if ( parent is Mobile )
			{
				((Mobile)parent).CanSwim = false;
			}
		}



		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			
		}
	}
}