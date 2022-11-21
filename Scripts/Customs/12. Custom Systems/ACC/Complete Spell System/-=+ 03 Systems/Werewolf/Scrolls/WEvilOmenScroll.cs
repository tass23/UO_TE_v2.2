using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WEvilOmenScroll : CSpellScroll
	{

		[Constructable]
		public WEvilOmenScroll() : base( typeof( WEvilOmenSpell ), 0x0EF5 )
		{
			Name = "Dreadful Howl";
			Hue = 1089;
			Stackable = true;
		}

		public WEvilOmenScroll( Serial serial ) : base( serial )
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
