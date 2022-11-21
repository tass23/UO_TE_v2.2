using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class CleansingWindsScroll : CSpellScroll
	{
		[Constructable]
		public CleansingWindsScroll() : this( 1 )
		{
		}

		[Constructable]
		public CleansingWindsScroll( int amount ) : base( typeof( CleansingWindsSpell ), 0x2DA8, amount )
		{
			Name = "Cleansing Winds Scroll";
		}

		public CleansingWindsScroll( Serial serial ) : base( serial )
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
