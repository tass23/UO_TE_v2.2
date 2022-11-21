using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WCorpseSkinScroll : CSpellScroll
	{

		[Constructable]
		public WCorpseSkinScroll() : base( typeof( WCorpseSkinSpell ), 0x0EF5 )
		{
			Name = "Toughen Skin";
			Hue = 1089;
			Stackable = true;
		}

		public WCorpseSkinScroll( Serial serial ) : base( serial )
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
