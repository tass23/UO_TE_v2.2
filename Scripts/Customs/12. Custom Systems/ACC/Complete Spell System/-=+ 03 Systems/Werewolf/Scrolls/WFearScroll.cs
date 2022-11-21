using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WFearScroll : CSpellScroll
	{

		[Constructable]
		public WFearScroll() : base( typeof( WFearSpell ), 0x0EF5 )
		{
			Name = "Horrific Howl";
			Hue = 1089;
			Stackable = true;
		}

		public WFearScroll( Serial serial ) : base( serial )
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
