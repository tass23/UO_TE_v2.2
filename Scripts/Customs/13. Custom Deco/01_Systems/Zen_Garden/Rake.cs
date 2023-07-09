//Created with Maraks Script Creator 4
using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	[DynamicFliping]
	[Flipable(3719, 3720)]
	public class Rake : Item
  {


      [Constructable]
		public Rake()
			: base(3719)
		{
          Name = "A Rake";
		}
		
		public override void OnDoubleClick( Mobile m )
		{
		  m.SendMessage( 48, "An overwhelming sense of peace fulls you as you rake the sand." );
		  m.Hits = 150;
		}

		public Rake( Serial serial ) : base( serial )
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
