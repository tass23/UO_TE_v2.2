using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class RisingColossusScroll : CSpellScroll
	{
		[Constructable]
		public RisingColossusScroll() : this( 1 )
		{
		}

		[Constructable]
		public RisingColossusScroll( int amount ) : base( typeof( RisingColossusSpell ), 0x2DAD, amount )
		{
			Name = "Rising Colossus Scroll";
		}

		public RisingColossusScroll( Serial serial ) : base( serial )
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
