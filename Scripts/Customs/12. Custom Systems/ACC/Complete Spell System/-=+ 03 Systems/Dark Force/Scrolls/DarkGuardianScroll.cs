using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkGuardianDisc : CSpellScroll
	{
		[Constructable]
		public DarkGuardianDisc() : base( typeof( DarkGuardianSpell ), 0x3194 )
		{
			Name = "Dark Guardian";
			Hue = 1772;
			Stackable = false;
		}

		public DarkGuardianDisc( Serial serial ) : base( serial )
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