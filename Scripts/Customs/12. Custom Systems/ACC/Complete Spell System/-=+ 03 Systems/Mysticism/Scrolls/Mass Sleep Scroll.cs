using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class MassSleepScroll : CSpellScroll
	{
		[Constructable]
		public MassSleepScroll() : this( 1 )
		{
		}

		[Constructable]
		public MassSleepScroll( int amount ) : base( typeof( MassSleepSpell ), 0x2DA7, amount )
		{
			Name = "Mass Sleep Scroll";
		}

		public MassSleepScroll( Serial serial ) : base( serial )
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
