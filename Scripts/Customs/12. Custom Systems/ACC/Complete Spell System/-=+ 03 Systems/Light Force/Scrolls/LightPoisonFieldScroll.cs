using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class LightPoisonFieldDisc : CSpellScroll
	{
		[Constructable]
		public LightPoisonFieldDisc() : base( typeof( LightPoisonField ), 0x01CB )
		{
			Name = "Force Essence";
			Hue = 1185;
			Stackable = false;
		}

		public LightPoisonFieldDisc( Serial serial ) : base( serial )
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