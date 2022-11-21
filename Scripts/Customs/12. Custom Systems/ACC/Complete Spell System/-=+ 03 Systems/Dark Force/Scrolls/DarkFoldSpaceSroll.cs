using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkFoldSpaceDisc : CSpellScroll
	{
		[Constructable]
		public DarkFoldSpaceDisc() : base( typeof( DarkFoldSpaceSpell ), 0x3194 )
		{
			Name = "Force Fold Space";
			Hue = 1772;
			Stackable = false;
		}

		public DarkFoldSpaceDisc( Serial serial ) : base( serial )
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
