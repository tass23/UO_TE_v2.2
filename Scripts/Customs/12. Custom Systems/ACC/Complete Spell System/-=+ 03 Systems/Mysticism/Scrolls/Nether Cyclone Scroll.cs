using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class NetherCycloneScroll : CSpellScroll
	{
		[Constructable]
		public NetherCycloneScroll() : this( 1 )
		{
		}

		[Constructable]
		public NetherCycloneScroll( int amount ) : base( typeof( NetherCycloneSpell ), 0x2DAC, amount )
		{
			Name = "Nether Cyclone Scroll";
		}

		public NetherCycloneScroll( Serial serial ) : base( serial )
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
