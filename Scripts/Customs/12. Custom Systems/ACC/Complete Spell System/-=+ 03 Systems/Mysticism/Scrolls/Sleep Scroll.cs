using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class SleepScroll : CSpellScroll
	{
		[Constructable]
		public SleepScroll() : this( 1 )
		{
		}

		[Constructable]
		public SleepScroll( int amount ) : base( typeof( SleepSpell ), 0x2DA2, amount )
		{
			Name = "Sleep Scroll";
		}

		public SleepScroll( Serial serial ) : base( serial )
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
