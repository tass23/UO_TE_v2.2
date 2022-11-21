using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WDivineFuryScroll : CSpellScroll
	{

		[Constructable]
		public WDivineFuryScroll() : base( typeof( WDivineFury ), 0x0EF5 )
		{
			Name = "Berserker";
			Hue = 1089;
			Stackable = true;
		}

		public WDivineFuryScroll( Serial serial ) : base( serial )
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
