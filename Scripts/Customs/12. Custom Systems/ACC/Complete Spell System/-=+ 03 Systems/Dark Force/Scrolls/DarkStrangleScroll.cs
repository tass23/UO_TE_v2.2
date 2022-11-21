using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkStrangleDisc : CSpellScroll
	{
		[Constructable]
		public DarkStrangleDisc() : base( typeof( DarkStrangleSpell ), 0x3194 )
		{
			Name = "Force Choke";
			Hue = 1772;
			Stackable = false;
		}

		public DarkStrangleDisc( Serial serial ) : base( serial )
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
