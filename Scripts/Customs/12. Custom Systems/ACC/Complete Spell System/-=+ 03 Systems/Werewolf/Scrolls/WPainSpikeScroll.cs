using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WPainSpikeScroll : CSpellScroll
	{

		[Constructable]
		public WPainSpikeScroll() : base( typeof( WPainSpikeSpell ), 0x0EF5 )
		{
			Name = "Piercing Howl";
			Hue = 1089;
			Stackable = true;
		}

		public WPainSpikeScroll( Serial serial ) : base( serial )
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
