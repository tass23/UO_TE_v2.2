using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WStrengthScroll : CSpellScroll
	{

		[Constructable]
		public WStrengthScroll() : base( typeof( WStrengthSpell ), 0x0EF5 )
		{
			Name = "Inciting Howl";
			Hue = 1089;
			Stackable = true;
		}

		public WStrengthScroll( Serial serial ) : base( serial )
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
