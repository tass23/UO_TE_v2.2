using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class SpellPlagueScroll : CSpellScroll
	{
		[Constructable]
		public SpellPlagueScroll() : this( 1 )
		{
		}

		[Constructable]
		public SpellPlagueScroll( int amount ) : base( typeof( SpellPlagueSpell ), 0x2DAA, amount )
		{
			Name = "Spell Plague Scroll";
		}

		public SpellPlagueScroll( Serial serial ) : base( serial )
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
