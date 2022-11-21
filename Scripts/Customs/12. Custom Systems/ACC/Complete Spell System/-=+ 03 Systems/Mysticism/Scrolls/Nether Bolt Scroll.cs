using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class NetherBoltScroll : CSpellScroll
	{
		[Constructable]
		public NetherBoltScroll() : this( 1 )
		{
		}

		[Constructable]
		public NetherBoltScroll( int amount ) : base( typeof( NetherBoltSpell ), 0x2D9E, amount )
		{
			Name = "Nether Bolt Scroll";
		}

		public NetherBoltScroll( Serial serial ) : base( serial )
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
