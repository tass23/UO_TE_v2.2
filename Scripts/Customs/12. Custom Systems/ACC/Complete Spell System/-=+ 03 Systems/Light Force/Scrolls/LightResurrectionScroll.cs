using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class LightResurrectionDisc : CSpellScroll
	{
		[Constructable]
		public LightResurrectionDisc() : base( typeof( LightResurrectionSpell ), 0x01CB )
		{
			Name = "Force Revival";
			Hue = 1185;
			Stackable = false;
		}

		public LightResurrectionDisc( Serial serial ) : base( serial )
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
