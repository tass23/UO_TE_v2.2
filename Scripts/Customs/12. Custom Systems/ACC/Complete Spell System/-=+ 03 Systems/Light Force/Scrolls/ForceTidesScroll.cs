using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class ForceTidesDisc : CSpellScroll
	{
		[Constructable]
		public ForceTidesDisc() : base( typeof( ForceTidesSpell ), 0x01CB )
		{
			Name = "Force Tides";
			Hue = 1185;
			Stackable = false;
		}

		public ForceTidesDisc( Serial serial ) : base( serial )
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