using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkVortexDisc : CSpellScroll
	{
		[Constructable]
		public DarkVortexDisc() : base( typeof( DarkVortexSpell ), 0x3194 )
		{
			Name = "Force Vortex";
			Hue = 1772;
			Stackable = false;
		}

		public DarkVortexDisc( Serial serial ) : base( serial )
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