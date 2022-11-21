using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkPoisonFieldDisc : CSpellScroll
	{
		[Constructable]
		public DarkPoisonFieldDisc() : base( typeof( DarkPoisonField ), 0x3194 )
		{
			Name = "Force Toxin";
			Hue = 1772;
			Stackable = false;
		}

		public DarkPoisonFieldDisc( Serial serial ) : base( serial )
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