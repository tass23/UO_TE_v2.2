using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class MedusaStatue : Item
	{
		public override int LabelNumber{ get{ return 1113626; } }
		
		[Constructable]
		public MedusaStatue() : base( 0x40BC )
		{
            Name = "Medusa";
			Weight = 10;
		}

		public MedusaStatue( Serial serial ) : base( serial )
		{
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