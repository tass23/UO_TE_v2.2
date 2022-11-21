using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class StealthDisc : CSpellScroll
	{

		[Constructable]
		public StealthDisc() : base( typeof( StealthSpell ), 0x01CB )
		{
			Name = "Force Stealth";
			Hue = 1185;
			Stackable = false;
		}

		public StealthDisc( Serial serial ) : base( serial )
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
