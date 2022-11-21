using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Vampire
{
	public class VBloodOathScroll : CSpellScroll
	{

		[Constructable]
		public VBloodOathScroll() : base( typeof( VBloodOathSpell ), 0x0EF5 )
		{
			Name = "Cursed Blood";
			Hue = 1464;
			Stackable = true;
		}

		public VBloodOathScroll( Serial serial ) : base( serial )
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
