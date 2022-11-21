using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WParalyzeScroll : CSpellScroll
	{

		[Constructable]
		public WParalyzeScroll() : base( typeof( WParalyzeSpell ), 0x0EF5 )
		{
			Name = "Paralyzing Howl";
			Hue = 1089;
			Stackable = true;
		}

		public WParalyzeScroll( Serial serial ) : base( serial )
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
