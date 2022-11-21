using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class BombardScroll : CSpellScroll
	{
		[Constructable]
		public BombardScroll() : this( 1 )
		{
		}

		[Constructable]
		public BombardScroll( int amount ) : base( typeof( BombardSpell ), 0x2DA9, amount )
		{
			Name = "Bombard Scroll";
		}

		public BombardScroll( Serial serial ) : base( serial )
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
