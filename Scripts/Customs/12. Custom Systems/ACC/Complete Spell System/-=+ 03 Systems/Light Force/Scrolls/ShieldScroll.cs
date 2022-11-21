using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class ShieldDisc : CSpellScroll
	{

		[Constructable]
		public ShieldDisc() : base( typeof( ShieldSpell ), 0x01CB )
		{
			Name = "Force Shield";
			Hue = 1185;
			Stackable = false;
		}

		public ShieldDisc( Serial serial ) : base( serial )
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
