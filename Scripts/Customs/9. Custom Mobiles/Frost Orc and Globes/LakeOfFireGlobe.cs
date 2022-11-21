using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class LakeOfFireGlobe : SnowGlobe
	{
		[Constructable]
		public LakeOfFireGlobe() 
		{
			Name = "a Snowy Scene of the Lake of Fire";
		}

		public LakeOfFireGlobe( Serial serial ) : base( serial )
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