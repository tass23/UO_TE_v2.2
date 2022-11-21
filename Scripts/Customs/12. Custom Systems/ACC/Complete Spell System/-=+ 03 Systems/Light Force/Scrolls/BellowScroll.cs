using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class BellowDisc : CSpellScroll
	{
		[Constructable]
		public BellowDisc() : this( 1 )
		{
		}

		[Constructable]
		public BellowDisc( int amount ) : base( typeof( BellowSpell ), 0x01CB, amount )
		{
			Name = "Force Bellow";
			Hue = 1185;
		}

		public BellowDisc( Serial serial ) : base( serial )
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
