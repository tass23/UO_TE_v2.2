using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class HealingStoneScroll : CSpellScroll
	{
		[Constructable]
		public HealingStoneScroll() : this( 1 )
		{
		}

		[Constructable]
		public HealingStoneScroll( int amount ) : base( typeof( HealingStoneSpell ), 0x2D9F, amount )
		{
			Name = "Healing Stone Scroll";
		}

		public HealingStoneScroll( Serial serial ) : base( serial )
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
