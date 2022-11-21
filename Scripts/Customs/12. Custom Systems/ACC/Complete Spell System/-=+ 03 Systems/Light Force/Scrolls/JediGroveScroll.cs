using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class JediGroveDisc : CSpellScroll
	{
		[Constructable]
		public JediGroveDisc() : base( typeof( JediGroveSpell ), 0x01CB )
		{
			Name = "Jedi Grove";
			Hue = 1185;
			Stackable = false;
		}

		public JediGroveDisc( Serial serial ) : base( serial )
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
