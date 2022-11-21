using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class PurgeMagicScroll : CSpellScroll
	{
		[Constructable]
		public PurgeMagicScroll() : this( 1 )
		{
		}

		[Constructable]
		public PurgeMagicScroll( int amount ) : base( typeof( PurgeMagicSpell ), 0x2DA0, amount )
		{
			Name = "Purge Magic Scroll";
		}

		public PurgeMagicScroll( Serial serial ) : base( serial )
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
