using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class EagleStrikeScroll : CSpellScroll
	{
		[Constructable]
		public EagleStrikeScroll() : this( 1 )
		{
		}

		[Constructable]
		public EagleStrikeScroll( int amount ) : base( typeof( EagleStrikeSpell ), 0x2DA3, amount )
		{
			Name = "Eagle Strike Scroll";
		}

		public EagleStrikeScroll( Serial serial ) : base( serial )
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
