using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class LightMatterDisc : CSpellScroll
	{

		[Constructable]
		public LightMatterDisc() : base( typeof( LightMatterSpell ), 0x01CB )
		{
			Name = "Light Matter";
			Hue = 1185;
			Stackable = false;
		}

		public LightMatterDisc( Serial serial ) : base( serial )
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