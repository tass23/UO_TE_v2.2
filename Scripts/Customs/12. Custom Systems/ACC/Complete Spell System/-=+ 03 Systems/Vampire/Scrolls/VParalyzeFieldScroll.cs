using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Vampire
{
	public class VParalyzeFieldScroll : CSpellScroll
	{

		[Constructable]
		public VParalyzeFieldScroll() : base( typeof( VParalyzeFieldSpell ), 0x0EF5 )
		{
			Name = "Petrified Field";
			Hue = 1464;
			Stackable = true;
		}

		public VParalyzeFieldScroll( Serial serial ) : base( serial )
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
