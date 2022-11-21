using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class StoneFormScroll : CSpellScroll
	{
		[Constructable]
		public StoneFormScroll() : this( 1 )
		{
		}

		[Constructable]
		public StoneFormScroll( int amount ) : base( typeof( StoneFormSpell ), 0x2DA5, amount )
		{
			Name = "Stone Form Scroll";
		}

		public StoneFormScroll( Serial serial ) : base( serial )
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
