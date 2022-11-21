using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class HailStormScroll : CSpellScroll
	{
		[Constructable]
		public HailStormScroll() : this( 1 )
		{
		}

		[Constructable]
		public HailStormScroll( int amount ) : base( typeof( HailStormSpell ), 0x2DAB, amount )
		{
			Name = "Hail Storm Scroll";
		}

		public HailStormScroll( Serial serial ) : base( serial )
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
