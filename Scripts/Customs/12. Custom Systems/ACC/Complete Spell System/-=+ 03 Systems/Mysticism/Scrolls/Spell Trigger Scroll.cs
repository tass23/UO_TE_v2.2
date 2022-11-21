using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class SpellTriggerScroll : CSpellScroll
	{
		[Constructable]
		public SpellTriggerScroll() : this( 1 )
		{
		}

		[Constructable]
		public SpellTriggerScroll( int amount ) : base( typeof( SpellTriggerSpell ), 0x2DA6, amount )
		{
			Name = "Spell Trigger Scroll";
		}

		public SpellTriggerScroll( Serial serial ) : base( serial )
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
