using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkChainLightningDisc : CSpellScroll
	{
		[Constructable]
		public DarkChainLightningDisc() : base( typeof( DarkChainLightningSpell ), 0x3194 )
		{
			Name = "Force Chain Lightning";
			Hue = 1772;
			Stackable = false;
		}

		public DarkChainLightningDisc( Serial serial ) : base( serial )
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
