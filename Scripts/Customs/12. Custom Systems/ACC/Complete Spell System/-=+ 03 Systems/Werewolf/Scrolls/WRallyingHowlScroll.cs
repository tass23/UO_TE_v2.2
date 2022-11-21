using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WRallyingHowlScroll : CSpellScroll
	{

		[Constructable]
		public WRallyingHowlScroll() : base( typeof( WRallyingHowlSpell ), 0x0EF5 )
		{
			Name = "Rallying Howl";
			Hue = 1089;
			Stackable = true;
		}

		public WRallyingHowlScroll( Serial serial ) : base( serial )
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
