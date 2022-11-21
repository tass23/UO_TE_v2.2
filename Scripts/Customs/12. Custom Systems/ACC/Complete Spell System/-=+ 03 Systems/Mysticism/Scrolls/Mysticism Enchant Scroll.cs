using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class MysticismEnchantScroll : CSpellScroll
	{
		[Constructable]
		public MysticismEnchantScroll() : this( 1 )
		{
		}

		[Constructable]
		public MysticismEnchantScroll( int amount ) : base( typeof( MysticismEnchantSpell ), 0x2DA5, amount )
		{
			Name = "Enchant Scroll";
		}

		public MysticismEnchantScroll( Serial serial ) : base( serial )
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
